using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPx.Events
{
    class MessageDispatcher<T> : IMessageDispatcher
    {
        public MessageDispatcher(string eventName, T item = default(T))
        {
            this.EventName = eventName;
            this.Item = item;
        }

        public string EventName { get; private set; }

        public T Item { get; private set; }

        public void Dispatch()
        {
            MessageCenter.SendMessage(this, EventName, Item);
        }
    }
}
