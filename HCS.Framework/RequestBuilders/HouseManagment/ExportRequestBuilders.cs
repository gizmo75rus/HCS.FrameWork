using HCS.Framework.Enums;
using HCS.Framework.Helpers;
using HCS.Framework.Interfaces;
using HCS.Service.Async.HouseManagement.v11_10_0_13;

namespace HCS.Framework.RequestBuilders.HouseManagment
{
    public class ExportHouse : BaseBuilder, IRequestBuilder<exportHouseDataRequest>
    {
        public exportHouseDataRequest Build(BuilderOption option)
        {
            return new exportHouseDataRequest {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                exportHouseRequest = new exportHouseRequest {
                    Id = RequestID,
                    FIASHouseGuid = option.Params[ParametrType.FIASHouseGUID].ToGisString()
                }
            };
        }
    }

    public class ExportAccount : BaseBuilder, IRequestBuilder<exportAccountDataRequest>
    {
        public exportAccountDataRequest Build(BuilderOption option)
        {
            return new exportAccountDataRequest {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                exportAccountRequest = new exportAccountRequest {
                    Id = RequestID,
                    FIASHouseGuid = option.Params[ParametrType.FIASHouseGUID].ToGisString()
                }
            };
        }
    }

    public class ExportCACh : BaseBuilder, IRequestBuilder<exportCAChDataRequest>
    {
        public exportCAChDataRequest Build(BuilderOption option)
        {
            return new exportCAChDataRequest {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                exportCAChAsyncRequest = new exportCAChAsyncRequest {
                    Id = RequestID,
                    Criteria = new exportCAChRequestCriteriaType[] {
                        new exportCAChRequestCriteriaType{
                            ItemsElementName = new ItemsChoiceType17[]{
                                ItemsChoiceType17.FIASHouseGuid,
                                ItemsChoiceType17.UOGUID,
                                ItemsChoiceType17.LastVersionOnly,
                            },
                            Items = new object[]{
                                option.Params[ParametrType.FIASHouseGUID].ToGisString(),
                                option.Params[ParametrType.UOGUID].ToGisString(),
                                true
                            }
                        }
                    }
                }
            };
        }
    }

    public class ExportSupplieResourceContract : BaseBuilder, IRequestBuilder<exportSupplyResourceContractDataRequest>
    {
        public exportSupplyResourceContractDataRequest Build(BuilderOption option)
        {
            return new exportSupplyResourceContractDataRequest {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                exportSupplyResourceContractRequest = new exportSupplyResourceContractRequest {
                    Id = RequestID,
                    ItemsElementName = new ItemsChoiceType19[] {
                        ItemsChoiceType19.FIASHouseGuid
                    },
                    Items = new object[] {
                        option.Params[ParametrType.FIASHouseGUID].ToGisString()
                    }
                }
            };
        }
    }

    public class ExportMetering : BaseBuilder, IRequestBuilder<exportMeteringDeviceDataRequest1>
    {
        public exportMeteringDeviceDataRequest1 Build(BuilderOption option)
        {
            return new exportMeteringDeviceDataRequest1 {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                exportMeteringDeviceDataRequest = new exportMeteringDeviceDataRequest {
                    Id = RequestID,
                    ItemsElementName = new ItemsChoiceType3[] {
                        ItemsChoiceType3.FIASHouseGuid,
                    },
                    Items = new object[] {
                        option.Params[ParametrType.FIASHouseGUID].ToGisString()
                    }
                }

            };
        }
    }


}
