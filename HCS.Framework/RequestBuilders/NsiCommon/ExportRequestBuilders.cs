using HCS.Service.Async.NsiCommon.v11_10_0_13;
using HCS.Framework.Enums;
using HCS.Framework.Interfaces;

namespace HCS.Framework.RequestBuilders.NsiCommon
{
    public class ExportNsiList : BaseBuilder, IRequestBuilder<exportNsiListRequest1>
    {
        public exportNsiListRequest1 Build(BuilderOption option)
        {
            return new exportNsiListRequest1 {
                ISRequestHeader = Create<ISRequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                exportNsiListRequest = new exportNsiListRequest {
                    Id = RequestID,
                    ListGroupSpecified = false
                }
            };
        }
    }

    public class ExportNsiItem : BaseBuilder, IRequestBuilder<exportNsiItemRequest1>
    {
        public exportNsiItemRequest1 Build(BuilderOption option)
        {
            return new exportNsiItemRequest1 {
                ISRequestHeader = Create<ISRequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                exportNsiItemRequest = new exportNsiItemRequest {
                    Id = RequestID,
                    RegistryNumber = option.Get(ParametrType.RegistryNumber).ToString()
                }
            };
        }
    }

}
