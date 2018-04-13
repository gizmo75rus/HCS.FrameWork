using HCS.Framework.Enums;
using HCS.Framework.Helpers;
using HCS.Framework.Interfaces;
using HCS.Service.Async.Licenses.v11_10_0_13;

namespace HCS.Framework.RequestBuilders.Licenses
{
    public class ExportRequestBuilders : BaseBuilder, IRequestBuilder<exportLicenseRequest1>
    {
        public exportLicenseRequest1 Build(BuilderOption option)
        {
            return new exportLicenseRequest1 {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Params[ParametrType.OrgPPAGUID]),
                exportLicenseRequest = new exportLicenseRequest {
                    Id = RequestID,
                    Items = new object[] {
                            new exportLicenseRequestLicenseOrganization {
                                ItemElementName = ItemChoiceType.OGRN,
                                Item = option.Params[ParametrType.OGRN].ToGisString()
                        }
                    }

                }
            };
        }
    }
}
