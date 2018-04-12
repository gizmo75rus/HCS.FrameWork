using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Framework.Base
{
    public class ErrorEventArgs : EventArgs
    {
        bool _isSingle;
        bool _isCore;
        Fault _fault;
        string _message;

        Dictionary<string, Fault> _faults;
        public bool IsSingle { get=>_isSingle; set=>_isSingle = value; }
        public Fault Fault { get=>_fault; set=>_fault = value; }
        public string Message { get => _message; }
        public Dictionary<string, Fault> Faults { get=>_faults; set=>_faults = value; }

        public ErrorEventArgs(Fault fault)
        {
            _isSingle = true;
            _isCore = false;
            _fault = fault;
            _message = _fault.ErrorCode + " " + _fault.ErrorMessage;
        }

        public ErrorEventArgs(bool isCore,string message)
        {
            _isSingle = true;
            _isCore = isCore;
            _message = message;
        }

        public ErrorEventArgs(Dictionary<string, Fault> faults)
        {
            _isSingle = false;
            _isCore = false;
            _faults = faults;
            
        }
    }
}
