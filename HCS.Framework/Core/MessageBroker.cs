using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.BaseTypes;
using HCS.Framework.Helpers;
using HCS.Framework.Implement;
using HCS.Framework.Interfaces;
using HCS.Globals;
using HCS.Interfaces;

namespace HCS.Framework.Core
{
    public delegate void ResultHandler(IEnumerable<object> items,IMessageType message);

    public class MessageBroker
    {
        /// <summary>
        /// Количество количество соединенией (потоков) на 1 сервис ГИС
        /// </summary>
        const int NUMBER_OF_CONNECTIONS = 5; 
        ServiceProviderCore _core;
        IMessageStory _story;
        Dictionary<Type, ResultHandler> _handlers;

        /// <summary>
        /// Брокер сообщений
        /// </summary>
        /// <param name="config"></param>
        /// <param name="story"></param>
        public MessageBroker(ClientConfig config,IMessageStory story)
        {
            _story = story;
            _core = new ServiceProviderCore(config);
            _core.SendErrorEvent += _core_SendErrorEvent;
            _core.GetResultErrorEvent += _core_GetResultErrorEvent;
            _core.OnAction += _core_OnAction;
            _core.SendCompliteEvent += _core_SendCompliteEvent;
            _core.GetResultCompliteEvent += _core_GetResultCompliteEvent;
            _handlers = new Dictionary<Type, ResultHandler>();
        }

        /// <summary>
        /// Добавить обработчик для типа вовращаемого объекта в GetStateResult/Item
        /// </summary>
        /// <param name="type"></param>
        /// <param name="handler"></param>
        public void AddHanbler(Type type, ResultHandler handler)
        {
            if (_handlers.Keys.Contains(type))
                return;

            _handlers.Add(type, handler);
        }

        /// <summary>
        /// Создать сообщение для конечной точки
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="endPoint"></param>
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

        /// <summary>
        /// Отправить сообщения
        /// </summary>
        public void SendMessage()
        {
            Parallel.ForEach(_story.GetMessageForSend().GroupBy(x => x.EndPoint), (messages) => {
                Parallel.ForEach(messages.ToArray().ToNBatches(NUMBER_OF_CONNECTIONS), (batch) => {
                    batch.ForEach(message => {
                        _core.Send(ref message);
                    });
                });
            });
        }

        /// <summary>
        /// Проверить на наличие результатов на ГИС
        /// </summary>
        public void CheckResult()
        {
            Parallel.ForEach(_story.GetForResultMessage().GroupBy(x => x.EndPoint), (messages) => {
                Parallel.ForEach(messages.ToArray().ToNBatches(NUMBER_OF_CONNECTIONS), (batchs) => {
                    batchs.ForEach(message => {
                        _core.GetResult(ref message);
                    });
                });
            });
        }

        /// <summary>
        /// Обработать результаты
        /// </summary>
        public void Process()
        {
            foreach(var message in _story.GetCompliteMessage()) {
                var type = message.Result.GetType();

                var result = message.Result as IGetStateResult;
                foreach(var item in result.Items.GroupBy(x => x.GetType())) {
                    if (_handlers.Keys.Contains(item.Key)) {
                        Task.Factory.StartNew(() => {
                            _handlers[item.Key]?.Invoke(item.AsEnumerable(),message);
                        });
                    }
                }
            }
        }

        #region CoreEventHandlers
        private void _core_GetResultCompliteEvent(Interfaces.IMessageType message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Сообщение  {message.MessageGUID} получено");
            Console.ResetColor();
        }
        private void _core_SendCompliteEvent(Interfaces.IMessageType message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Сообщение  {message.MessageGUID} отправлено");
            Console.ResetColor();
        }
        private void _core_OnAction(int count)
        {
            Console.WriteLine("Получение результата, попытка " + count);
        }
        private void _core_GetResultErrorEvent(string error, Interfaces.IMessageType message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Не удалось получить ответ на сообщение: {message.MessageGUID},произошла ошибка:{error}");
            Console.ResetColor();
        }
        private void _core_SendErrorEvent(string error, Interfaces.IMessageType message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Не удалось отправить сообщение: {message.MessageGUID},произошла ошибка:{error}");
            Console.ResetColor();
        }
        #endregion
    }
}
