using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript;
using System;


namespace ScriptCoreLib.JavaScript.Runtime
{
    [Script]
    public class Timer
    {
        public event System.Action<Timer> Tick;

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

        public Timer(System.Action<Timer> e)
        {
            Tick += e;
        }

        public Timer(System.Action<Timer> e, int duetime, int interval)
        {
            Tick += e;

            if (duetime > 0)
            {


                Native.setTimeout(
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




        // static method for enum?

        public bool TimeToLiveExceeded
        {
            get
            {
                if (TimeToLive > 0)
                    if (Counter > TimeToLive)
                        return true;

                return false;
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

        public static Timer Interval(System.Action<Timer> e, int i)
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
            id = Native.setInterval(Invoke, i);
        }

        public void StartTimeout()
        {
            StartTimeout(DefaultTimeout);
        }

        public void StartTimeout(int i)
        {
            Stop();
            isTimeout = true;


            // tested by
            // X:\jsc.svn\examples\javascript\AsyncInlineWorkerDocumentExperiment\AsyncInlineWorkerDocumentExperiment\Application.cs

            id = Native.setTimeout(Invoke, i);

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
                Native.clearTimeout(id);

            if (isInterval)
                Native.clearInterval(id);

            isInterval = false;
            isTimeout = false;
            id = 0;

            Counter = 0;
        }

        public static void Do(ScriptCoreLib.JavaScript.DOM.IArray<System.Action> dx, int duetime, int interval)
        {
            new Timer(
                delegate(Timer timer)
                {
                    if (dx.length > 0)
                    {
                        var h = dx.shift();

                        if (h != null)
                            h();

                    }
                    else
                        timer.Stop();

                }, duetime, interval);
        }

        public static void DoAsync(System.Action h)
        {
            new Timer(delegate { h(); }, 1, 0);

        }

        public const int DefaultTriggerTTL = 30;

        public static Timer Trigger(System.Action<Predicate> p, System.Action h)
        {
            Timer timer = null;

            System.Action<Timer> tick =
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



    }
}
