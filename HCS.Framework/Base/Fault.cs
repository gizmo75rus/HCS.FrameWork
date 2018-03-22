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
        string _code;
        string _message;
        public string ErrorCode { get=>_code;  set=>_code = value; }
        public string ErrorMessage { get => _message; set=>_message = value; }
        public Fault(string code,string message)
        {
            _code = code;
            _message = message;
        }
    }
}
