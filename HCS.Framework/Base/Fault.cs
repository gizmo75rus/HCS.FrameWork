using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Interfaces;

namespace HCS.Framework.Base
{
    public class Fault : IFault
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
