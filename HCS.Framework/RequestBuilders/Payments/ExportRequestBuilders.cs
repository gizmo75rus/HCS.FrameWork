using HCS.Service.Async.Payment.v11_10_0_13;
using HCS.Framework.Interfaces;
using HCS.Framework.Enums;
using HCS.Framework.Helpers;

namespace HCS.Framework.RequestBuilders.Payments
{
    public class ExportPaymentDocumentDetails : BaseBuilder, IRequestBuilder<exportPaymentDocumentDetailsRequest1>
    {
        public exportPaymentDocumentDetailsRequest1 Build(BuilderOption option)
        {
            return new exportPaymentDocumentDetailsRequest1 {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                exportPaymentDocumentDetailsRequest = new exportPaymentDocumentDetailsRequest {
                    Id = RequestID,
                    ItemsElementName = new ItemsChoiceType4[] {
                        ItemsChoiceType4.FIASHouseGuid,
                        ItemsChoiceType4.Year,
                        ItemsChoiceType4.Month
                    },
                    Items = new object[] {
                        option.Get(ParametrType.FIASHouseGUID).ToGisString(),
                        (short)option.Get(ParametrType.Year),
                        (int)option.Get(ParametrType.Month)
                    }
                }
            };
        }
    }
}
