using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPx.Data
{
    public class Batch
    {
        public Batch()
        {

        }

        //public void Test()
        //{
        //    var t = new { Name = "A" };
        //    var ts = new object[] { t };
        //    var te = (IEnumerable<object>)ts;

        //    using (var entity = new EntityEmul())
        //    {
        //        var result = Batch.Run(x =>
        //        {
        //            return x.Update(entity.Update, t) &&
        //                x.Update(entity.Update, ts) &&
        //                x.Update(entity.Update, te);
        //        });
        //    }
        //}

        public bool Create<T>(Func<T, bool> func, params T[] t)
        {
            return Call(Events.StandardMessages.Created, func, t);
        }

        public bool Update<T>(Func<T, bool> func, params T[] t)
        {
            return Call(Events.StandardMessages.Updated, func, t);
        }

        public bool Delete<T>(Func<T, bool> func, params T[] t)
        {
            return Call(Events.StandardMessages.Deleted, func, t);
        }

        public bool Call<T>(string eventName, Func<T, bool> func, params T[] t)
        {
            if (t != null && t.Length > 0)
                foreach (var item in t)
                    if (!func(item))
                        return false;
                    else
                        this.Add(Events.StandardMessages.Updated, item);
            return true;
        }

        private List<Events.IMessageDispatcher> messages = new List<Events.IMessageDispatcher>();

        private void Add<T>(string eventName, T item)
        {
            messages.Add(new Events.MessageDispatcher<T>(eventName, item));
        }

        public static bool Run(Func<Batch, bool> action)
        {
            var batch = new Batch();
            try
            {
                var result = action(batch);
                if (result)
                    batch.Complete();
                return result;
            }
            finally
            {
            }
        }

        private void Complete()
        {
            if (messages != null && messages.Count > 0)
                foreach (var message in messages)
                    message.Dispatch();
        }
    }
    //public class EntityEmul : IDisposable
    //{
    //    public EntityEmul()
    //    {

    //    }

    //    public void Complete()
    //    {

    //    }

    //    public void Dispose()
    //    {
    //        this.Complete();
    //    }
    //}

    //public static class EntityExtension
    //{
    //    public static bool Update<T>(this EntityEmul emul, T instance)
    //    {
    //        return true;
    //    }
    //    public static bool Update<T>(this EntityEmul emul, IEnumerable<T> instances)
    //    {
    //        return true;
    //    }
    //}

}
