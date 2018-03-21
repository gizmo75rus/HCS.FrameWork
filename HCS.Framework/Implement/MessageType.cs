using System;
using HCS.Framework.Enums;
using HCS.Framework.Interfaces;
using HCS.Globals;

namespace HCS.Framework.Implement
{
    public class MessageType : IMessageType
    {
        public EndPoints EndPoint { get; set; }
        public Type RequestType { get; set; }
        public object Request { get; set; }
        public object Result { get; set; }
        public Guid MessageGUID { get; set; }
        public Guid ResponceGUID { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime CompliteDate { get; set; }
        public MessageStatuses Status { get; set; }
    }
}
