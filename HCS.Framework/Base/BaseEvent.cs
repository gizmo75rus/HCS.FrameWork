using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Interfaces;
using HCS.Interfaces;

namespace HCS.Framework.Base
{
    public class BaseEvent
    {
        public event EventHandler<BaseEventArgs> ErrorEvent;
        public event EventHandler<BaseEventArgs> CompliteEvent;
        public void OnError(IMessageType message,IFault fault)
        {
            BaseEventArgs args = new BaseEventArgs();
            if(ErrorEvent != null) {
                args.Message = message;
                args.ErrorCode = fault.ErrorCode;
                args.Description = fault.ErrorMessage;
                ErrorEvent(this, args);
            }
        }

        public void OnComplite(IMessageType message)
        {
            BaseEventArgs args = new BaseEventArgs();
            if(CompliteEvent != null) {
                args.Message = message;
                CompliteEvent(this, args);
            }
        }
        
    }
}
