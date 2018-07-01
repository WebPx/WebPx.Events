using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPx.Events
{
    public delegate void MessageEventHandler(object sender, MessageEventArgs args);
    public delegate void MessageEventHandler<TEntity>(object sender, MessageEventArgs<TEntity> args);
}
