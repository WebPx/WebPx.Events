using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPx.Events
{
    class MessageSubscribers
    {
        public MessageSubscribers()
        {
            _channels = new Dictionary<Type, Dictionary<string, object>>();
        }

        private Dictionary<Type, Dictionary<string, object>> _channels = null;

        public Dictionary<Type, Dictionary<string, object>> Channels
        {
            get
            {
                return _channels;
            }
        }
    }
}
