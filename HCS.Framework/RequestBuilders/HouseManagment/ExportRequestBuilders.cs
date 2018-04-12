using HCS.Framework.Enums;
using HCS.Framework.Interfaces;
using HCS.Service.Async.HouseManagement.v11_10_0_13;

namespace HCS.Framework.RequestBuilders.HouseManagment
{
    public class ExportHouseDataRequestBuilder : BaseBuilder, IRequestBuilder<exportHouseDataRequest>
    {
        public exportHouseDataRequest Build(BuilderOption option)
        {
            return new exportHouseDataRequest {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Params[ParametrType.OrgPPAGUID]),
                exportHouseRequest = new exportHouseRequest {
                    Id = RequestID,
                    FIASHouseGuid = option.Params[ParametrType.FIASHouseGUID].ToString().ToLower()
                }
            };
        }
    }

    public class ExportAccountRequestBuilder : BaseBuilder, IRequestBuilder<exportAccountDataRequest>
    {
        public exportAccountDataRequest Build(BuilderOption option)
        {
            return new exportAccountDataRequest {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Params[ParametrType.OrgPPAGUID]),
                exportAccountRequest = new exportAccountRequest {
                    Id = RequestID,
                    FIASHouseGuid = option.Params[ParametrType.FIASHouseGUID].ToString().ToLower()
                }
            };
        }
    }

    public class ExportCAChRequestBuilder : BaseBuilder, IRequestBuilder<exportCAChDataRequest>
    {
        public exportCAChDataRequest Build(BuilderOption option)
        {
            return new exportCAChDataRequest {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Params[ParametrType.OrgPPAGUID]),
                exportCAChAsyncRequest = new exportCAChAsyncRequest {
                    Criteria = new exportCAChRequestCriteriaType[] {
                        new exportCAChRequestCriteriaType{
                            ItemsElementName = new ItemsChoiceType17[]{ ItemsChoiceType17.FIASHouseGuid },
                            Items = new object[]{ option.Params[ParametrType.FIASHouseGUID].ToString().ToLower()}
                        }
                    }
                }
            };
        }
    }

    public class ExportSuppliesContractRequestBuilder : BaseBuilder, IRequestBuilder<exportSupplyResourceContractDataRequest>
    {
        public exportSupplyResourceContractDataRequest Build(BuilderOption option)
        {
            return new exportSupplyResourceContractDataRequest {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Params[ParametrType.OrgPPAGUID]),
                exportSupplyResourceContractRequest = new exportSupplyResourceContractRequest {
                    ItemsElementName = new ItemsChoiceType19[] { ItemsChoiceType19.FIASHouseGuid },
                    Items = new object[] { option.Params[ParametrType.FIASHouseGUID].ToString().ToLower() }
                }
            };
        }
    }
}
