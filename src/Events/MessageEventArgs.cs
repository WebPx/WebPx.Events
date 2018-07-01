using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPx.Events
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string message)
        {
            this.Message = message;
        }

        public string Message { get; private set; }
    }

    public class MessageEventArgs<TEntity> : MessageEventArgs
    {
        public MessageEventArgs(TEntity instance, string message, /*bool bySession, */dynamic arguments) : base(message)
        {
            this.Instance = instance;
            //this.BySession = bySession;
            this.Arguments = arguments;
        }

        public TEntity Instance { get; private set; }
        //public bool BySession { get; }

        public dynamic Arguments { get; }
    }
}
