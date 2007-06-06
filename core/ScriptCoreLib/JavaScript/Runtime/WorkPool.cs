using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.JavaScript.DOM;

using System.Collections.Generic;

namespace ScriptCoreLib.JavaScript.Runtime
{
    /// <summary>
    /// helps to prevent recurring events to be discarded
    /// </summary>
    [Script]
    public class WorkPool
    {
        [Script]
        class EntryItem
        {
            public string Key;
            public EventHandler Handler;
        }

        List<EntryItem> List = new List<EntryItem>();

        Timer Worker = new Timer();

        public int Interval = 100;

        public int Timeout = 5000;

        /// <summary>
        /// creates a new work pool, each next job performed at interval
        /// </summary>
        /// <param name="Interval"></param>
        public WorkPool(int Interval) : this()
        {
            this.Interval = Interval;
        }

        public WorkPool()
        {
            Worker.Tick += new EventHandler<Timer>(Worker_Tick);
        }

        /// <summary>
        /// abort event will be called if the last job entry took longer than the time specified in the timeout field
        /// </summary>
        public event EventHandler<WorkPool> Abort;
        public event EventHandler<global::System.Exception> Error;

        void Worker_Tick(Timer e)
        {
            try
            {
                EntryItem i = List[0];

                List.RemoveAt(0);

                double z = IDate.Now.getTime();

                i.Handler();

                if (IDate.Now.getTime() - z > Timeout)
                {
                    Console.LogError("workpool timeout exceeded");

                    

                    Helper.Invoke(Abort, this);

                    List.Clear();
                }
            }
            catch (global::System.Exception ex)
            {
                if (Error != null)
                    Error(ex);
            }

            Touch();
        }

        public static WorkPool operator +(WorkPool a, EventHandler h)
        {
            a.Add(h);

            return a;
        }

        public void Add(EventHandler h)
        {
            EntryItem i = new EntryItem();

            i.Handler = h;

            List.Add(i);

            Touch();
        }

        /// <summary>
        /// replaces work item
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public EventHandler this[string key]
        {
            set
            {
                Remove(key);
                Add(value, key);
            }
        }

        /// <summary>
        /// will add a new work item
        /// </summary>
        /// <param name="h"></param>
        /// <param name="key"></param>
        public void Add(EventHandler h, string key)
        {
            EntryItem i = new EntryItem();

            i.Handler = h;
            i.Key = key;

            List.Add(i);

            Touch();
        }

        /// <summary>
        /// deletes all earlier work items by key
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            List.RemoveAll(
                delegate(EntryItem t)
                {
                    return t.Key == key;
                }
            );

            Touch();
        }

        /// <summary>
        /// resets timeout if some work is left to do
        /// </summary>
        private void Touch()
        {
            if (List.Count > 0)
                Worker.StartTimeout(Interval);
            else
                Worker.Stop();
        }




    }

}
