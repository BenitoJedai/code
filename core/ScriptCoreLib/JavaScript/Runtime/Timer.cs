using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript;


namespace ScriptCoreLib.JavaScript.Runtime
{
    [Script]
    public class Timer
    {
        public event EventHandler<Timer> Tick;

        int id;

        bool isTimeout;
        bool isInterval;

        public const int DefaultTimeout = 300;

        public int Counter;
        public int Step = 1;
        public int TimeToLive = 0;

        public Timer()
        {

        }

        public Timer(EventHandler<Timer> e)
        {
            Tick += e;
        }

        public Timer(EventHandler<Timer> e, int duetime, int interval)
        {
            Tick += e;

            if (duetime > 0)
            {
                Native.Window.setTimeout(
                    delegate
                    {
                        if (interval > 0)
                            this.StartInterval(interval);
                        else
                            this.Invoke();
                    }, duetime);
            }
            else
            {
                if (interval > 0)
                    this.StartInterval(interval);
                else
                    this.Invoke();
            }

        }

        public bool TimeToLiveExceeded
        {
            get
            {
                return TimeToLive > 0 && Counter > TimeToLive;
            }
        }
        public void Invoke()
        {
            if (Enabled)
            {
                Helper.Invoke(Tick, this);

                Counter += Step;

                if (TimeToLiveExceeded)
                {
                    Stop();
                }
            }
        }

        public static Timer Interval(EventHandler<Timer> e, int i)
        {
            Timer t = new Timer();

            t.Tick += e;
            t.StartInterval(i);

            return t;
        }

        public void StartInterval(int i, int ttl)
        {
            TimeToLive = ttl;
            StartInterval(i);
        }

        /// <summary>
        /// starts the timer with the default timeout (300 ms)
        /// </summary>
        public void StartInterval()
        {
            StartInterval(DefaultTimeout);
        }

        public void StartInterval(int i)
        {
            Stop();
            isInterval = true;
            id = Native.Window.setInterval(Invoke, i);
        }

        public void StartTimeout()
        {
            StartTimeout(DefaultTimeout);
        }

        public void StartTimeout(int i)
        {
            Stop();
            isTimeout = true;
            id = Native.Window.setTimeout(Invoke, i);
        }

        public bool Enabled = true;

        public bool IsAlive
        {
            get
            {
                return id != 0;
            }
        }

        public void Stop()
        {
            if (isTimeout)
                Native.Window.clearTimeout(id);

            if (isInterval)
                Native.Window.clearInterval(id);

            isInterval = false;
            isTimeout = false;
            id = 0;

            Counter = 0;
        }

        public static void Do(ScriptCoreLib.JavaScript.DOM.IArray<EventHandler> dx, int duetime, int interval)
        {
            new Timer(
                delegate(Timer timer)
                {
                    if (dx.length > 0)
                    {
                        EventHandler h = dx.shift();

                        if (h != null)
                            h();

                    }
                    else
                        timer.Stop();
                    
                }, duetime, interval);
        }

        public static void DoAsync(EventHandler h)
        {
            new Timer(delegate { h();  }, 1, 0);

        }

        public const int DefaultTriggerTTL = 30;

        public static Timer Trigger(EventHandler<Predicate> p, EventHandler h)
        {
            Timer timer = null;

            EventHandler<Timer> tick =
                delegate
                {
                    if (Predicate.Is(p))
                    {
                        timer.Stop();

                        Helper.Invoke(h);
                    }
                };

            timer = new Timer(tick, 100, 100);

            return timer;
        }


        internal static void While(Func<bool> condition, Action done, int interval)
        {
            Timer t = null;

            t = new Timer(
                delegate
                {
                   if (!condition())
                   {
                       t.Stop();

                       done();
                   }
                }
            );

            t.StartInterval(interval);
        }
    }
}
