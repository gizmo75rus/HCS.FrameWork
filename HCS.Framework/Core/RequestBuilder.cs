using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Base;
using HCS.Framework.Dto.HouseManagment;
using HCS.Framework.Enums;
using HCS.Globals;
using HCS.Service.Async.HouseManagement.v11_10_0_13;

namespace HCS.Framework.Core
{
    public abstract class BaseBuilder
    {
        public event EventHandler<ErrorEventArgs> BuildError;
        public THeaderType Create<THeaderType>(OrganizationRole role, string orgPPAGUID) where THeaderType : class
        {
            try {
                var tInstance = Activator.CreateInstance(typeof(THeaderType));

                var props = tInstance.GetType().GetProperties();

                foreach (var prop in props) {
                    switch (prop.Name) {
                        case "Item":
                            if (string.IsNullOrEmpty(orgPPAGUID)) {
                                throw new ApplicationException("Не указан идентификатор организации в ГИС");
                            }
                            prop.SetValue(tInstance, orgPPAGUID);
                            break;
                        case "ItemElementName":
                            prop.SetValue(tInstance, Enum.Parse(prop.PropertyType, "orgPPAGUID"));
                            break;
                        case "MessageGUID":
                            prop.SetValue(tInstance, Guid.NewGuid().ToString());
                            break;
                        case "Date":
                            prop.SetValue(tInstance, DateTime.Now);
                            break;
                        case "IsOperatorSignatureSpecified":
                            if (role == OrganizationRole.RC)
                                prop.SetValue(tInstance, true);
                            break;
                        case "IsOperatorSignature":
                            if (role == OrganizationRole.RC)
                                prop.SetValue(tInstance, true);
                            break;
                    }
                }

                return tInstance as THeaderType;
            }
            catch (SystemException exc) {
                BuildError?.Invoke(this, new ErrorEventArgs(new Base.Fault("", "При сборке запроса произошла ошибка:" + exc.GetBaseException().Message)));
            }
            catch (Exception exc) {
                BuildError?.Invoke(this, new ErrorEventArgs(new Base.Fault("", "При сборке запроса произошла ошибка:" + exc.GetBaseException().Message)));
            }

            return null;
        }

        protected string RequestID = "signed-data-container";

    }
    public abstract class RequestBuilder<TRequest> : BaseBuilder where TRequest : class
    {
        public abstract TRequest Build(RequestOption option);
        public abstract T Builds<T>(RequestOption option) where T : class
    }

    public class ExportHouseDataRequestBuilder : RequestBuilder<exportAccountDataRequest>
    {
        public override exportAccountDataRequest Build(RequestOption option)
        {
            return new exportAccountDataRequest {
                RequestHeader = Create<RequestHeader>(option.Role, option.Criteries[CriteriaType.OrgPPAGUID].ToString()),
                exportAccountRequest = new exportAccountRequest {

                }
            };
        }

        public override T Builds<T>(RequestOption option)
        {

           

            throw new NotImplementedException();
        }
    }

}
