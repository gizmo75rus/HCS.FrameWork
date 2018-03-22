using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.BaseTypes;
using HCS.Framework.Implement;
using HCS.Globals;

namespace HCS.Framework.Core
{
    public class MessageBroker
    {
        ServiceProviderCore _core;
        MessageStory _story;
        Dictionary<Type, Delegate> _subscribers;

        public MessageBroker(ClientConfig config)
        {
            _story = new MessageStory();
            _core = new ServiceProviderCore(config);
            _core.SendErrorEvent += _core_SendErrorEvent;
            _core.GetResultErrorEvent += _core_GetResultErrorEvent;
            _core.OnAction += _core_OnAction;
            _core.SendCompliteEvent += _core_SendCompliteEvent;
            _core.GetResultCompliteEvent += _core_GetResultCompliteEvent;
            _subscribers = new Dictionary<Type, Delegate>();

        }

        public void Register<T>(Action<T> handler) where T:class
        {
            if (_subscribers.Keys.Contains(typeof(T)))
                return;

            _subscribers.Add(typeof(T), handler);
        }

        public void CreateMessage<T>(T request,EndPoints endPoint) where T:class
        {
            _story.Add(new MessageType {
                MessageGUID = Guid.NewGuid(),
                EndPoint = endPoint,
                Request = request,
                RequestType = request.GetType(),
                Status = Enums.MessageStatuses.Ready
            });
        }

        public void SendMessage()
        {
            _story.GetMessageForSend()?.ForEach(x => {
                _core.Send(ref x);
                _story.Update(x);
            });
        }
        public void CheckResult()
        {
            _story.GetForResultMessage()?.ForEach(x => {
                _core.GetResult(ref x);
                _story.Update(x);
            });
        }

        public void Process()
        {
            foreach(var message in _story.GetCompliteMessage()) {
                var type = message.Result.GetType();
                if (_subscribers.Keys.Contains(type)) {
                    Task.Factory.StartNew(() => _subscribers[type]?.DynamicInvoke(message.Result));
                }
            }
        }

        #region CoreEventHandlers
        private void _core_GetResultCompliteEvent(Interfaces.IMessageType message)
        {
            Console.WriteLine($"Сообщение  {message.MessageGUID} получено");
        }
        private void _core_SendCompliteEvent(Interfaces.IMessageType message)
        {
            Console.WriteLine($"Сообщение  {message.MessageGUID} отправлено");
        }
        private void _core_OnAction(int count)
        {
            Console.WriteLine("Получение результата, попытка " + count);
        }
        private void _core_GetResultErrorEvent(string error, Interfaces.IMessageType message)
        {
            Console.WriteLine($"Не удалось получить ответ на сообщение: {message.MessageGUID},произошла ошибка:{error}");
        }
        private void _core_SendErrorEvent(string error, Interfaces.IMessageType message)
        {
            Console.WriteLine($"Не удалось отправить сообщение: {message.MessageGUID},произошла ошибка:{error}");
        }
        #endregion
    }
}
