using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.BaseTypes;

namespace HCS.Framework.Core
{
    public delegate void MessageBrokerThread();

    public class MessageBroker
    {
        static MessageBroker _insance;
        static Object _this = new Object();
        bool _latch = false;
        ServiceProviderCore _core;
        MessageStory _story;

        MessageBroker(ClientConfig config)
        {
            _story = new MessageStory();
            _core = new ServiceProviderCore(config);
            _core.SendMessageErrorEvent += _core_SendMessageErrorEvent;

        }

        private void _core_SendMessageErrorEvent(string error, Interfaces.IMessageType message)
        {
            throw new NotImplementedException();
        }

        public static MessageBroker GetInstance(ClientConfig config)
        {
            if(_insance == null) {
                lock(_this)
                    _insance = new MessageBroker(config);
            }
            return _insance;
        }
        public static MessageBroker Instance { get => _insance; }
        public void Run()
        {
            MessageBrokerThread thread = ProccessMessage;
            IAsyncResult result = thread.BeginInvoke(CallBack, thread);
        }
        void CallBack(IAsyncResult asr)
        {
            MessageBrokerThread thread = asr.AsyncState as MessageBrokerThread;
            thread.EndInvoke(asr);
            // Уведомление
        }
        void ProccessMessage()
        {
            if (_latch)
                return;
            _latch = true;

            _story.GetMessageForSend()?.ForEach(x => {
                _core.Send(ref x); 
            });

            _story.GetForResultMessage()?.ForEach(x => {
                _core.GetResult(ref x);
            });

            _latch = false;
        }

    }
}
