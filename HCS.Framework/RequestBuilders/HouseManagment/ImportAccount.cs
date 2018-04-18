using System;
using System.Collections.Generic;
using System.Linq;
using HCS.Service.Async.HouseManagement.v11_10_0_13;
using HCS.Framework.Interfaces;
using HCS.Framework.Enums;
using HCS.Framework.Dto.HouseManagment;
using HCS.Framework.Helpers;
using HCS.Framework.DataServices.HouseManagment;

namespace HCS.Framework.RequestBuilders.HouseManagment
{
    public class ImportAccount : BaseBuilder, IRequestBuilder<importAccountDataRequest, AccountData>
    {
        public importAccountDataRequest Build(BuilderOption option, AccountData data)
        {
            if (data.Values.Count() > LIMIT_BY_REQUEST)
                throw new ArgumentOutOfRangeException("Превышено макисмальное кол-во лицевых в один запрос");

            var accounts = data.Values.Select(dto => new importAccountRequestAccount {
                TransportGUID = dto.TransportGuid,
                ItemElementName = GetAccountType(dto.AccountType),
                Item = true,
                Accommodation = new AccountTypeAccommodation[] {
                    new AccountTypeAccommodation {
                        ItemElementName = GetAccommodationType(dto.AccommodationType),
                        Item = dto.AccommodationGuid,
                    }
                },
                CreationDateSpecified = false,
                AccountNumber = dto.AccountName,
                AccountGUID = string.IsNullOrEmpty(dto.Guid) ? null : dto.Guid.ToLower(),
                Closed = dto.Closed == null ? null : GetClosedAttribute(dto.Closed),
                TotalSquareSpecified = true,
                TotalSquare = dto.TotalSquare,
                ResidentialSquareSpecified = true,
                ResidentialSquare = dto.ResidentialSquare,
                AccountReasons = GetReason(dto),
                PayerInfo = dto.Payer == null ? new AccountTypePayerInfo { } : GetPayerInfo(dto.Payer)
            });

            return new importAccountDataRequest {
                RequestHeader = Create<RequestHeader>(option.IsOperator,option.Get(ParametrType.OrgGUID)),
                importAccountRequest = new importAccountRequest {
                    Id = RequestID,
                    Account = accounts.ToArray()
                }
            };
        }

        private ItemChoiceType10 GetAccommodationType(AccommodationType type)
        {
            switch (type) {
                case AccommodationType.FIASHouseGuid:
                    return ItemChoiceType10.FIASHouseGuid;
                case AccommodationType.LivingRoomGUID:
                    return ItemChoiceType10.LivingRoomGUID;
                case AccommodationType.PremisesGUID:
                    return ItemChoiceType10.PremisesGUID;
                default:
                    throw new NotImplementedException();
            }
        }

        private ItemChoiceType9 GetAccountType(AccountTypes type)
        {
            switch (type) {
                case AccountTypes.UK:
                    return ItemChoiceType9.isUOAccount;
                case AccountTypes.RSO:
                    return ItemChoiceType9.isRSOAccount;
                case AccountTypes.CRA:
                    return ItemChoiceType9.isCRAccount;
                case AccountTypes.RC:
                    return ItemChoiceType9.isRCAccount;
                default:
                    throw new NotImplementedException();
            }
        }

        private ClosedAccountAttributesType GetClosedAttribute(AccountClose close)
        {
            return new ClosedAccountAttributesType {
                CloseDate = close.Date,
                Description = close.Description,
                CloseReason = new nsiRef {
                    Code = close.Reason.Code,
                    GUID = close.Reason.Guid.ToGisString()
                }
            };
        }

        private AccountTypePayerInfo GetPayerInfo(PayerInfo payer)
        {
            switch (payer.Type) {
                case PayerType.Indv:
                    return new AccountTypePayerInfo {
                        Item = new AccountIndType {
                            Item = payer.SNILS
                        }
                    };
                case PayerType.Legal:
                    return new AccountTypePayerInfo {
                        Item = new RegOrgVersionType {
                            orgVersionGUID = payer.OrgVersionGuid
                        }
                    };
                default:
                    return new AccountTypePayerInfo { };
            }
        }

        private AccountReasonsImportType GetReason(AccountDto dto)
        {
            switch (dto.Reason) {
                case AccountReason.SupplyContract:
                    return new AccountReasonsImportType {
                        SupplyResourceContract = new AccountReasonsImportTypeSupplyResourceContract[] {
                            new AccountReasonsImportTypeSupplyResourceContract{
                                ItemsElementName = new ItemsChoiceType8[]{
                                    ItemsChoiceType8.ContractGUID
                                },
                                Items = new object[]{
                                    dto.ReasonGuid
                                }
                            }
                        }
                    };
                case AccountReason.SocialContract:
                    return new AccountReasonsImportType {
                        SocialHireContract = new AccountReasonsImportTypeSocialHireContract{
                            ItemsElementName = new ItemsChoiceType9[] {
                                ItemsChoiceType9.ContractGUID
                            },
                            Items = new object[] {
                                dto.ReasonGuid
                            }
                        }
                    };
                default:
                    return new AccountReasonsImportType { };
            }
        }
    }
}
