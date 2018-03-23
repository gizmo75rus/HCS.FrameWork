using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Base;

namespace HCS.Framework.Interfaces
{
    public interface IMessageStory
    {
        Result Add(IMessageType message);
        Result Update(IMessageType message);
        Result Delete(IMessageType message);
        List<IMessageType> GetMessageForSend();
        List<IMessageType> GetForResultMessage();
        List<IMessageType> GetCompliteMessage();
        List<IMessageType> GetBrokenMessage();

    }
}
