using System;
using System.Collections.Generic;
using System.Linq;
using HCS.Service.Async.Bills.v11_10_0_13;
using HCS.Framework.Interfaces;
using HCS.Framework.Dto.Bills;
using HCS.Framework.Enums;
using HCS.Framework.DataServices.Bills;

namespace HCS.Framework.RequestBuilders.Bills
{
    public class ImportPaymentDocument : BaseBuilder, IRequestBuilder<importPaymentDocumentDataRequest, PaymentDocumentData>
    {
        public importPaymentDocumentDataRequest Build(BuilderOption option, PaymentDocumentData dto)
        {
            if (dto.Documents?.Count() > LIMIT_BY_REQUEST)
                throw new ArgumentOutOfRangeException("Превышено максималое кол-во объектов PaymentDocumentDto для одного запроса");

            var documents = new List<object>() {
                true, //<<------- ConfirmAmountsCorrect
                dto.Month,
                dto.Year,
                PaymentInformation(dto.Requisites)
            };

            
            switch (option.Command) {
                case RequestCMD.Create:
                    documents.AddRange(dto.Documents.Select(doc => PaymentDocument(dto.HasSpecialKP, doc, dto.Requisites.RequsiteKEY)));
                    break;
                case RequestCMD.Delete:
                    documents.AddRange(dto.Documents.Select(doc => RevokeDocument(doc)));
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new importPaymentDocumentDataRequest {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                importPaymentDocumentRequest = new importPaymentDocumentRequest {
                    Id = RequestID,
                    Items = documents.ToArray()
                }
            };
        }

        /// <summary>
        /// Реквизиты ПД
        /// </summary>
        /// <param name="requisites"></param>
        /// <returns></returns>
        importPaymentDocumentRequestPaymentInformation PaymentInformation(PaymentRequisites requisites)
        {
            return new importPaymentDocumentRequestPaymentInformation {
                TransportGUID = requisites.RequsiteKEY,
                BankBIK = requisites.BIK,
                operatingAccountNumber = requisites.BankAccountNumber
            };
        }

        /// <summary>
        /// Платежный документ
        /// </summary>
        /// <returns></returns>
        private importPaymentDocumentRequestPaymentDocument PaymentDocument(bool usedSpecialCapRepair, PaymentDocumentDto dto, string paymentInformationKey)
        {
            var document = new importPaymentDocumentRequestPaymentDocument {
                TransportGUID = dto.TransportGuid,
                AccountGuid = dto.AccountGUID,

                Item = true,
                ItemElementName = ItemChoiceType4.Expose,

                PaymentInformationKey = paymentInformationKey,

                // Авансы
                AdvanceBllingPeriodSpecified = false,

                //Долг
                DebtPreviousPeriodsSpecified = false,

                //Итог к оплате с учетом рассрочек и.т.п.
                totalPiecemealPaymentSumSpecified = false,
                TotalByPenaltiesAndCourtCostsSpecified = false,
                TotalPayableByChargeInfoSpecified = false,
                TotalPayableByPDWithDebtAndAdvanceSpecified = false,
                TotalPayableByPDSpecified = false,

                PaidCashSpecified = false,
                DateOfLastReceivedPaymentSpecified = false

            };

            if (dto.DebtPreviousPeriods > 0) {
                document.DebtPreviousPeriodsSpecified = true;
                document.DebtPreviousPeriods = dto.DebtPreviousPeriods;
            }

            if (dto.DateOfLastPay.HasValue) {
                document.PaidCashSpecified = true;
                document.PaidCash = dto.Pay;
                document.DateOfLastReceivedPaymentSpecified = true;
                document.DateOfLastReceivedPayment = dto.DateOfLastPay.Value;
            }

            var charges = new List<object>();

            if (dto.ChargesInfos.Any()) {

                charges.AddRange(dto.ChargesInfos.Select(charge => ChargeInfo(charge)).ToArray());

                // в случае наличия спец.счета кап.ремонта
                if (usedSpecialCapRepair && dto.CapitalRepairs?.Count() == 0)
                    charges.Add(new CapitalRepairImportType { });
            };

            if (dto.CapitalRepairs.Any()) {

                charges.AddRange(dto.CapitalRepairs.Select(x => new CapitalRepairImportType {
                    Contribution = x.Contribution,
                    AccountingPeriodTotal = x.AccountingPeriodTotal,
                    MoneyRecalculation = x.Recalcultion,
                    TotalPayable = x.Total
                }).ToArray());
            }

            if (dto.Penies.Any()) {
                charges.Add(new PaymentDocumentTypePenaltiesAndCourtCosts {
                    Cause = dto.Penies.FirstOrDefault().Clause,
                    ServiceType = dto.Penies.Select(x => new nsiRef { Code = x.Code, GUID = x.Guid }).FirstOrDefault(),
                    TotalPayable = dto.Penies.Sum(x => x.Total)
                });

            }

            document.Items = charges.ToArray();
            return document;
        }

        /// <summary>
        /// Отзыв платежного документа
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private importPaymentDocumentRequestWithdrawPaymentDocument RevokeDocument(PaymentDocumentDto dto)
        {
            var document = new importPaymentDocumentRequestWithdrawPaymentDocument {
                TransportGUID = dto.TransportGuid,
                PaymentDocumentID = dto.UniqueNumber
            };
            return document;
        }


        /// <summary>
        /// Начисление по услуги
        /// </summary>
        /// <returns></returns>
        private PaymentDocumentTypeChargeInfo ChargeInfo(ChargeInfo charge)
        {
            var result = new PaymentDocumentTypeChargeInfo();
            switch (charge.ServiceType) {
                // Жилищные услуги
                case ServiceType.HousingService:
                    result.Item = new PDServiceChargeTypeHousingService {
                        ServiceType = new nsiRef { Code = charge.Code, GUID = charge.Guid },
                        Rate = charge.Rate,
                        TotalPayable = charge.Total,
                        AccountingPeriodTotal = charge.PeriodTotal,

                        ServiceCharge = new ServiceChargeImportType {
                            MoneyDiscountSpecified = false,
                            MoneyRecalculationSpecified = true,
                            MoneyRecalculation = charge.ReCalc,
                        },

                        MunicipalResource = charge.SubCharges != null ? charge.SubCharges.Select(x => new PDServiceChargeTypeHousingServiceMunicipalResource {
                            ServiceType = new nsiRef { Code = x.Code, GUID = x.Guid },
                            Rate = x.Rate,
                            MunicipalServiceCommunalConsumptionPayable = x.Total,
                            AccountingPeriodTotalSpecified = true,
                            AccountingPeriodTotal = x.PeriodTotal,
                            Consumption = new PDServiceChargeTypeHousingServiceMunicipalResourceConsumption {
                                Volume = new PDServiceChargeTypeHousingServiceMunicipalResourceConsumptionVolume {
                                    Value = charge.Value,
                                    determiningMethodSpecified = true,
                                    determiningMethod = PDServiceChargeTypeHousingServiceMunicipalResourceConsumptionVolumeDeterminingMethod.N,
                                    typeSpecified = true,
                                    type = PDServiceChargeTypeHousingServiceMunicipalResourceConsumptionVolumeType.O
                                }
                            }
                        }).ToArray() : new PDServiceChargeTypeHousingServiceMunicipalResource[] { }

                    };

                    break;
                case ServiceType.AdditionalService:
                    // Дополнительные услуги
                    result.Item = new PDServiceChargeTypeAdditionalService {
                        //Услуга
                        ServiceType = new nsiRef { Code = charge.Code, GUID = charge.Guid },

                        //Тариф
                        Rate = charge.Rate,

                        //Всего
                        TotalPayable = charge.Total,

                        AccountingPeriodTotal = charge.PeriodTotal,

                        ServiceCharge = new ServiceChargeImportType {
                            MoneyDiscountSpecified = false,
                            MoneyRecalculationSpecified = true,
                            MoneyRecalculation = charge.ReCalc,
                        },

                        //Порядок расчетов
                        CalcExplanation = "Автоматический"

                    };
                    break;
                case ServiceType.MunicipalSerive:
                    //Коммунальные услуги
                    result.Item = new PDServiceChargeTypeMunicipalService {
                        //Услуга
                        ServiceType = new nsiRef { Code = charge.Code, GUID = charge.Guid },

                        //Тариф
                        Rate = charge.Rate,

                        //Всего
                        TotalPayable = charge.Total,

                        //Итого с учетом перерасчетов
                        AccountingPeriodTotal = charge.PeriodTotal,

                        //Порядок расчетов
                        CalcExplanation = "Автоматический",


                        // К оплате за потребление комм услуги ОДН
                        MunicipalServiceCommunalConsumptionPayableSpecified = false,

                        // К оплате за потребление комм услуги индв.
                        MunicipalServiceIndividualConsumptionPayableSpecified = false,


                        // Перерасчеты
                        ServiceCharge = new ServiceChargeImportType {
                            MoneyRecalculationSpecified = true,
                            MoneyRecalculation = charge.ReCalc
                        },

                        // Объем услуг
                        Consumption = new PDServiceChargeTypeMunicipalServiceVolume[] { new PDServiceChargeTypeMunicipalServiceVolume {

                            determiningMethodSpecified = true,
                            //Способ определения объема: M - счетчик, N - Норматив, O - Другое

                            determiningMethod = (PDServiceChargeTypeMunicipalServiceVolumeDeterminingMethod)charge.DeterminingMethod,

                            typeSpecified = true,
                            //Тип усуги: I - индивидуальная, O - Общедомовая
                            type = (PDServiceChargeTypeMunicipalServiceVolumeType)charge.VolumeType,

                            //Объем
                            Value = charge.Value,
                        } },


                        ServiceInformation = new ServiceInformation {
                            individualConsumptionCurrentValueSpecified = true,
                            individualConsumptionCurrentValue = 5
                        }

                    };
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
