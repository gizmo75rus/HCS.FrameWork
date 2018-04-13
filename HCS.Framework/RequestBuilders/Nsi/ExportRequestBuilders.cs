using HCS.Service.Async.Nsi.v11_10_0_13;
using HCS.Framework.Interfaces;
using HCS.Framework.Enums;

namespace HCS.Framework.RequestBuilders.Nsi
{
    public class ExportAdditionalServices : BaseBuilder, IRequestBuilder<exportDataProviderNsiItemRequest1>
    {
        public exportDataProviderNsiItemRequest1 Build(BuilderOption option)
        {
            return new exportDataProviderNsiItemRequest1 {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                exportDataProviderNsiItemRequest = new exportDataProviderNsiItemRequest {
                    Id = RequestID,
                    RegistryNumber = exportDataProviderNsiItemRequestRegistryNumber.Item1
                }
            };
        }
    }

    public class ExportMunicipalservices : BaseBuilder, IRequestBuilder<exportDataProviderNsiItemRequest1>
    {
        public exportDataProviderNsiItemRequest1 Build(BuilderOption option)
        {
            return new exportDataProviderNsiItemRequest1 {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                exportDataProviderNsiItemRequest = new exportDataProviderNsiItemRequest {
                    Id = RequestID,
                    RegistryNumber = exportDataProviderNsiItemRequestRegistryNumber.Item51
                }
            };
        }
    }

    public class ExportOrganizationWorks : BaseBuilder, IRequestBuilder<exportDataProviderNsiItemRequest1>
    {
        public exportDataProviderNsiItemRequest1 Build(BuilderOption option)
        {
            return new exportDataProviderNsiItemRequest1 {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                exportDataProviderNsiItemRequest = new exportDataProviderNsiItemRequest {
                    Id = RequestID,
                    RegistryNumber = exportDataProviderNsiItemRequestRegistryNumber.Item59
                }
            };
        }
    }

}
