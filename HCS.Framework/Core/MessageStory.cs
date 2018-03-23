using System;
using System.Collections.Generic;
using System.Linq;
using HCS.Framework.Base;
using HCS.Framework.Enums;
using HCS.Framework.Interfaces;

namespace HCS.Framework.Core
{
    public class MessageStory:IMessageStory
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
        MessageStatuses[] _brokenCriteria = new MessageStatuses[] {
            MessageStatuses.SendCriticalError,
            MessageStatuses.GetResultCriticalError,
            MessageStatuses.EndLive
        };

        public Result Add(IMessageType message) {
            var idx = _collection.FindIndex(x => x.MessageGUID == message.MessageGUID);
            if (idx != -1)
                return new Result("В хранилище уже имеется сообщение: " + message.MessageGUID);

            _collection.Add(message);
            return new Result();
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
        public List<IMessageType> GetCompliteMessage()
        {
            return _collection.Where(x => x.Status == MessageStatuses.GetResultOk).ToList();
        }
        public List<IMessageType> GetBrokenMessage()
        {
            return _collection.Where(x => _brokenCriteria.Contains(x.Status)).ToList();
        }
        
    }
}
