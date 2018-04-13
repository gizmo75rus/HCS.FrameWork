using HCS.Service.Async.Bills.v11_10_0_13;
using HCS.Framework.Interfaces;
using HCS.Framework.Enums;

namespace HCS.Framework.RequestBuilders.Bills
{
    public class ExportPaymentDocument : BaseBuilder, IRequestBuilder<exportPaymentDocumentDataRequest>
    {
        public exportPaymentDocumentDataRequest Build(BuilderOption option)
        {
            return new exportPaymentDocumentDataRequest {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                exportPaymentDocumentRequest = new exportPaymentDocumentRequest {
                    Id = RequestID,
                    ItemsElementName = new ItemsChoiceType5[] {
                        ItemsChoiceType5.FIASHouseGuid,
                        ItemsChoiceType5.Year,
                        ItemsChoiceType5.Month

                    },
                    Items = new object[] {
                        option.Get(ParametrType.FIASHouseGUID),
                        (short)option.Get(ParametrType.Year),
                        (int)option.Get(ParametrType.Month)
                    }
                }
            };
        }
    }
}
