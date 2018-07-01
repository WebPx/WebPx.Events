using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPx.Events
{
    public static class MessageExtensions
    {
        public static bool OnSuccess<TEntity>(this bool result, object sender, string eventName)
        {
            if (result)
                MessageCenter.SendMessage<TEntity>(sender, eventName);
            return result;
        }
    }
}
