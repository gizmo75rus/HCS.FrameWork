using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Interfaces;

namespace HCS.Framework.DataServices.Bills
{
    class PaymentDocumentDataService : IDataService<PaymentDocumentData>
    {
        public IEnumerable<PaymentDocumentData> Load(params object[] param)
        {
            throw new NotImplementedException();
        }
    }
}
