using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Interfaces;

namespace HCS.Framework.Base
{
    public class BaseEventArgs : EventArgs
    {
        public string ErrorCode { get; set; }
        public string Description { get; set; }
        public IMessageType Message { get; set; }
    }
}
