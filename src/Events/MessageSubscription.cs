using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPx.Events
{
    class MessageSubscription<T>
    {
        public MessageSubscription(/*bool bySession, */MessageEventHandler<T> handler)
        {
            //this.BySession = bySession;
            this.Handler = handler;
        }

        //public bool BySession { get; set; }

        public MessageEventHandler<T> Handler { get; set; }
    }
}
