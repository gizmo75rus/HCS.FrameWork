using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Base;
using HCS.Framework.Enums;
using HCS.Framework.Implement;
using HCS.Framework.Interfaces;

namespace HCS.Framework.Core
{
    public class MessageStory
    {
        public MessageStory()
        {
            _collection = new List<IMessageType>();
        }
        List<IMessageType> _collection;
        MessageStatuses[] _sendCritera = new MessageStatuses[] {
            MessageStatuses.Ready,
            MessageStatuses.SendTimeout,
            MessageStatuses.SendError
        };
        MessageStatuses[] _getResultCriteria = new MessageStatuses[] {
            MessageStatuses.SendOk,
            MessageStatuses.GetResultTimeout,
            MessageStatuses.GetResultError
        };
        MessageStatuses[] _clearBrokenCriteria = new MessageStatuses[] {
            MessageStatuses.SendCriticalError,
            MessageStatuses.GetResultCriticalError,
            MessageStatuses.EndLive
        };

        public Result Add(IMessageType message) {
            try {
                var idx = _collection.FindIndex(x => x.MessageGUID == message.MessageGUID);
                return new Result("В хранилище уже имеется сообщение: " + message.MessageGUID);
            }
            catch (ArgumentNullException) {
                _collection.Add(message);
                return new Result();
            }
        }
        public Result Update(IMessageType message)
        {
            try {
                var idx = _collection.FindIndex(x => x.MessageGUID == message.MessageGUID);
                _collection[idx] = message;
                return new Result();
            }
            catch (ArgumentNullException) {
                return new Result("В хранилище не найдено сообщение: "+ message.MessageGUID);
            }
        }
        public Result Delete(IMessageType message)
        {
            try {
                var idx = _collection.FindIndex(x => x.MessageGUID == message.MessageGUID);
                _collection.RemoveAt(idx);
                return new Result();
            }
            catch (ArgumentNullException) {
                return new Result("В хранилище не найдено сообщение: " + message.MessageGUID);
            }

        }
        public List<IMessageType> GetMessageForSend()
        {
            return _collection.Where(x => _sendCritera.Contains(x.Status)).ToList();
        }
        public List<IMessageType> GetForResultMessage()
        {
            return _collection.Where(x => _getResultCriteria.Contains(x.Status)).ToList();
        }
        public List<IMessageType> GetBrokeMessage()
        {
            return _collection.Where(x => _clearBrokenCriteria.Contains(x.Status)).ToList();
        }
        
    }
}
