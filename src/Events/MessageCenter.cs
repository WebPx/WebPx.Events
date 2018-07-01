using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebPx.Events
{
    public static class MessageCenter
    {
        private static MessageSubscribers _channels = null;

        public static void Subscribe<T>(MessageEventHandler<T> messageEventHandler, string eventName = null/*, bool bySession = true*/)
        {
            var channelType = typeof(T);
            var channels = Subscriptions.Channels;
            Dictionary<string, object> channel = null;
            if (!channels.ContainsKey(channelType))
            {
                channel = new Dictionary<string, object>();
                channels.Add(channelType, channel);
            }
            else
                channel = channels[channelType];
            if (channel != null)
            {
                eventName = eventName ?? string.Empty;
                if (!channel.ContainsKey(eventName))
                    channel.Add(eventName, new List<MessageSubscription<T>>());
                var subs = channel[eventName];
                if (subs != null)
                {
                    var subscribers = (List<MessageSubscription<T>>)subs;
                    subscribers.Add(new MessageSubscription<T>(/*bySession, */messageEventHandler));
                }
            }
        }

        private static MessageSubscribers Subscriptions
        {
            get
            {
                //var context = HttpContext.Current;
                //if (context != null)
                //{
                //    var instance = context.Items["messageCenter"] as MessageSubscribers;
                //    if (instance == null)
                //    {
                //        instance = new MessageSubscribers();
                //        context.Items["messageCenter"] = instance;
                //    }
                //    return instance;
                //}
                if (_channels == null)
                    _channels = new MessageSubscribers();
                return _channels;
            }
        }

        public static void SendMessage<T>(object sender, string eventName, T instance = default(T), dynamic arguments = null)
        {
            var channelType = typeof(T);
            var channels = Subscriptions.Channels;
            if (channels.ContainsKey(channelType))
            {
                var channel = channels[typeof(T)];
                MessageEventArgs<T> args = null;
                if (channel != null)
                {
                    if (!string.IsNullOrEmpty(eventName) && channel.ContainsKey(eventName))
                    {
                        var subs = channel[eventName];
                        Distribute(sender, eventName, instance, ref args, arguments, subs);
                    }
                    if (channel.ContainsKey(string.Empty))
                    {
                        var subs = channel[string.Empty];
                        Distribute(sender, eventName, instance, ref args, arguments, subs);
                    }
                }
            }
        }

        private static void Distribute<T>(object sender, string eventName, T instance, ref MessageEventArgs<T> args, dynamic arguments, object subs)
        {
            if (subs != null)
            {
                var subscribers = (List<MessageSubscription<T>>)subs;
                foreach (var subscriber in subscribers)
                {
                    if (args == null)
                        args = new MessageEventArgs<T>(instance, eventName, /*subscriber.BySession, */arguments);
                    subscriber.Handler(sender, args);
                }
            }
        }

        public static bool Assert<T>(bool result, object sender, string message, T instance = default(T), dynamic arguments = null)
        {
            if (result)
                SendMessage<T>(sender, message, instance, arguments);
            return result;
        }
    }
}
