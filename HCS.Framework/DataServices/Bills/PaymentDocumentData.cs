using System.Collections.Generic;
using HCS.Framework.Base;
using HCS.Framework.Dto.Bills;

namespace HCS.Framework.DataServices.Bills
{
    public class PaymentDocumentData : DtoData
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public bool HasSpecialKP { get; set; }
        public PaymentRequisites Requisites { get; set; }
        public List<PaymentDocumentDto> Documents { get; set; }
        public PaymentDocumentData()
        {
            Documents = new List<PaymentDocumentDto>();
        }
    }
}
