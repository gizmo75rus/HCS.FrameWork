using System;
using System.Collections.Generic;
using System.Threading;

using HCS.BaseTypes;
using HCS.Framework.Base;
using HCS.Framework.Enums;
using HCS.Framework.Interfaces;
using HCS.Globals;
using HCS.Helpers;
using HCS.Interfaces;
using HCS.Providers;

namespace HCS.Framework.Core
{
    internal class ServiceProviderCore
    {
        Dictionary<EndPoints, IProvider> _providerLocator;
        Polices.SoupFaultPolicy _faultPolicy;

        public delegate void ErrorHandler(string error,IMessageType message);
        public delegate void ActionHandler(int count);
        public delegate void SendCompliteHandler(IMessageType message);
        public delegate void GetResultCompliteHandler(IMessageType message);


       
        public event ErrorHandler SendErrorEvent = delegate { };
        public event ErrorHandler GetResultErrorEvent = delegate { };
        public event ActionHandler OnAction = delegate { };
        public event SendCompliteHandler SendCompliteEvent = delegate { };
        public event GetResultCompliteHandler GetResultCompliteEvent = delegate { };

        Tuple<int, int>[] ATTEMS = new Tuple<int, int>[] {
            new Tuple<int, int>(1,60),
            new Tuple<int, int>(2,180),
            new Tuple<int, int>(3,300),
            new Tuple<int, int>(4,600),
            new Tuple<int, int>(5,900)
        };

        public ServiceProviderCore(ClientConfig config)
        {
            _providerLocator = new Dictionary<EndPoints, IProvider>();
            _providerLocator.Add(EndPoints.HouseManagementAsync, new HouseManagmentProvider(config));
            _providerLocator.Add(EndPoints.BillsAsync, new BillsProvider(config));
            _providerLocator.Add(EndPoints.PaymentsAsync, new PaymentsProvider(config));
            _providerLocator.Add(EndPoints.DeviceMeteringAsync, new DeviceMeteringProvider(config));
            _providerLocator.Add(EndPoints.LicensesAsync, new LicensesProvider(config));
            _providerLocator.Add(EndPoints.NsiAsync, new NsiProvider(config));
            _providerLocator.Add(EndPoints.NsiCommonAsync, new NsiCommonProvider(config));
            _providerLocator.Add(EndPoints.OrgRegistryAsync, new OrganizationsRegistryProvider(config));
            _providerLocator.Add(EndPoints.OrgRegistryCommonAsync, new OrganizationsRegistryCommonProvider(config));

            _faultPolicy = new Polices.SoupFaultPolicy();
        }

        public void Send(ref IMessageType message)
        {
            try {
                var provider = _providerLocator[message.EndPoint];
                if (provider == null)
                    throw new ArgumentOutOfRangeException($"Для конечной точки {Enum.GetName(typeof(EndPoints), message.EndPoint)} отсутсвует зарегистрированный провайдер");

                // Отправить запрос
                var ack = provider.Send(message.Request);
                message.SendDate = DateTime.Now;
                message.ResponceGUID = Guid.Parse(ack.MessageGUID);
                message.Status = MessageStatuses.SendOk;
                SendCompliteEvent(message);
            }
            catch (System.ServiceModel.FaultException<IFault> ex) {
                // Выбрать поведение в зависимости от кода ошибки soap ГИС 
                var action = _faultPolicy.GetAction(ex.Detail.ErrorCode);
                string errorMessage = ex.Detail.ErrorCode + " " + ex.Detail.ErrorMessage;

                switch (action) {
                    case Polices.Actions.NeedException:
                        message.Status = MessageStatuses.SendCriticalError;
                        throw new Exception(ex.Message);
                    case Polices.Actions.Abort:
                        message.Status = MessageStatuses.SendCriticalError;
                        break;
                    case Polices.Actions.TryAgain:
                        message.Status = MessageStatuses.SendError;
                        break;
                    default:
                        message.Status = MessageStatuses.SendCriticalError;
                        errorMessage = $"Поведение для действия {Enum.GetName(typeof(Polices.Actions), action)} не реализовано";
                        break;
                }
                SendErrorEvent($"При отправке запроса в ГИС ЖКХ произошла ошибка {errorMessage}", message);
            }
            catch (TimeoutException) {
                message.Status = MessageStatuses.SendTimeout;
                SendErrorEvent("Запрос не отправлен, превышен интервал ожидания: ", message);
            }
            catch (Exception ex) {
                message.Status = MessageStatuses.SendCriticalError;
                
                SendErrorEvent("При отправке запроса произошло не обработанное исключение: " + ExceptionHelper.GetBaseException(ex), message);
            }
        }

        public void GetResult(ref IMessageType message)
        {
            try {
                var provider = _providerLocator[message.EndPoint];
                if (provider == null)
                    throw new ArgumentOutOfRangeException($"Для конечной точки {Enum.GetName(typeof(EndPoints), message.EndPoint)} отсутсвует зарегистрированный провайдер");

                IAck ack = new Ack {
                    MessageGUID = message.ResponceGUID.ToString().ToLower()
                };

                IGetStateResult stateResult = new StateResult();

                foreach(var i in ATTEMS) {
                    OnAction(i.Item1);
                    if (provider.TryGetResult(ack, out stateResult)) {
                        message.CompliteDate = DateTime.Now;
                        message.Status = MessageStatuses.GetResultOk;
                        message.Result = stateResult;
                        GetResultCompliteEvent(message);
                        return;
                    }
                    Thread.Sleep(i.Item2*500);
                }
                message.Status = MessageStatuses.GetResultTimeout;
            }
            catch (System.ServiceModel.FaultException<IFault> ex) {
                var action = _faultPolicy.GetAction(ex.Detail.ErrorCode);
                string errorMessage = ex.Detail.ErrorCode + " " + ex.Detail.ErrorMessage;
                switch (action) {
                    case Polices.Actions.NeedException:
                        message.Status = MessageStatuses.GetResultCriticalError;
                        throw new Exception($"При отправке запроса в ГИС ЖКХ произошла ошибка {errorMessage}");
                    case Polices.Actions.Abort:
                        message.Status = MessageStatuses.GetResultCriticalError;
                        break;
                    case Polices.Actions.TryAgain:
                        message.Status = MessageStatuses.GetResultError;
                        break;
                    default:
                        message.Status = MessageStatuses.GetResultCriticalError;
                        errorMessage = $"Поведение для действия {Enum.GetName(typeof(Polices.Actions), action)} не реализовано";
                        break;
                }
                GetResultErrorEvent($"При получении результата из ГИС ЖКХ произошла ошибка {errorMessage}",message);
            }
            catch (TimeoutException) {
                message.Status = MessageStatuses.GetResultTimeout;
                GetResultErrorEvent($"При получении результата из ГИС ЖКХ превышен интервал ожидания", message);
            }
            catch (Exception ex) {
                message.Status = MessageStatuses.SendCriticalError;
                GetResultErrorEvent("При получении результата произошло не обработанное исключение: " + ExceptionHelper.GetBaseException(ex), message);
            }
        }
    }
}
