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
        Fault _fault;
        Dictionary<string, Fault> _faults;
        public bool IsSingle { get=>_isSingle; set=>_isSingle = value; }
        public Fault Fault { get=>_fault; set=>_fault = value; }
        public Dictionary<string, Fault> Faults { get=>_faults; set=>_faults = value; }

        public ErrorEventArgs(Fault fault)
        {
            _fault = fault;
            _isSingle = true;
        }

        public ErrorEventArgs(Dictionary<string, Fault> faults)
        {
            _faults = faults;
            _isSingle = false;
        }
    }
}
