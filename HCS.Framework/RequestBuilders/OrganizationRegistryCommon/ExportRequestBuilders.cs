using HCS.Framework.Interfaces;
using HCS.Service.Async.OrganizationsRegistryCommon.v11_10_0_13;

namespace HCS.Framework.RequestBuilders.OrganizationRegistryCommon
{
    public class ExportOrgRegistry : BaseBuilder, IRequestBuilder<exportOrgRegistryRequest1>
    {
        public exportOrgRegistryRequest1 Build(BuilderOption option)
        {
            return new exportOrgRegistryRequest1 {
                ISRequestHeader = Create<ISRequestHeader>(option.IsOperator,option.Params[Enums.ParametrType.OrgPPAGUID]),
                exportOrgRegistryRequest = new exportOrgRegistryRequest {
                    Id = RequestID,
                    lastEditingDateFromSpecified = false,
                    SearchCriteria = new exportOrgRegistryRequestSearchCriteria[] {
                        new exportOrgRegistryRequestSearchCriteria {
                            ItemsElementName = new ItemsChoiceType3[]{
                                ItemsChoiceType3.OGRN
                            },
                            Items = new string[]{
                                option.Params[Enums.ParametrType.OGRN].ToString()
                            }
                        }
                    }
                }
            };
        }
    }
}

