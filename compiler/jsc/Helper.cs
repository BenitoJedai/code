using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using ScriptCoreLib.Ultra.Library;

namespace jsc
{
	// do we need them anymore?

    public struct Variant<T>
    {
        public bool Dirty;

        public T Value;

        
    }

    public sealed class Helper
    {
 

        public class WorkPool
        {
            Queue<Action> q = new Queue<Action>();
            Queue<Thread> t = new Queue<Thread>();


            public bool IsThreaded;

            public int MaxThreads = 4;

            public static WorkPool operator +(WorkPool w, Action h)
            {
                w.q.Enqueue(h);

                return w;
            }

            public bool WaitOne(Action h)
            {
                q.Enqueue(h);

                return WaitOne();
            }

            public bool WaitOne()
            {
                if (q.Count == 0)
                    return false;

                //if (IsThreaded)
                //{
                //    Thread u = new Thread(
                //        delegate()
                //        {
                //            DequeueAndInvoke();
                //        }
                //    );

                //    t.Enqueue(u);

                //    u.IsBackground = true;
                //    u.Start();
                //}
                //else
                //{
                    DequeueAndInvoke();
                //}

                return true;
            }

            private void DequeueAndInvoke()
            {
                if (q.Count > 0)
                {
                    lock (q)
                    {
                        if (q.Count > 0)
                        {
                            Action h = q.Dequeue();

                            Helper.Invoke(h);
                        }
                    }
                }
            }

            public void WaitAll()
            {
                while (WaitOne());

                if (IsThreaded)
                {
                    while (t.Count > 0)
                    {
                        t.Dequeue().Join();
                    }
                }
            }



            internal void ForEach<T>(T[] e, Action<T> h)
            {
                ForEachWaitOn(e, h);

                WaitAll();

            }

            internal void ForEachWaitOn<T>(T[] e, Action<T> h)
            {
                Array.ForEach(e,
                    delegate(T var)
                    {
                        WaitOne( 
                            delegate
                            {
                                h(var);
                            }
                        );
                    });
            }
        }


        public class ConsoleStopper : Disposable
        {
            public static void Invoke(string u, Action h)
            {
                using (new ConsoleStopper(u))
                    Helper.Invoke(h);
            }

            public ConsoleStopper(string u)
            {
                long n = DateTime.Now.Ticks;

                this.Disposing +=
                    delegate
                    {
                        Console.WriteLine(u + " - " + (new TimeSpan( DateTime.Now.Ticks - n)).TotalMilliseconds + "ms");
                    };
            }
        }

        public static bool InArray<T>(T u, params T[] e)
        {
            foreach (T var in e)
            {
                if (var.Equals(u))
                    return true;
            }

            return false;
        }


        internal static void Invoke(Action h)
        {
            if (h != null) h();
        }

        internal static bool Invoke<T>(Predicate<T> p, T i)
        {
            if (p != null)
                return p(i);

            return false;
        }


        public static string GetSafeWin32FileName(string u)
        {
            // http://www.boost.org/libs/filesystem/doc/portability_guide.htm

            // Returns true for names containing only the characters 
            // specified by the Windows platform SDK as valid regardless 
            // of the file system. Allows any character except 0x0-0x1F, 
            // '<', '>', ':', '"', '/', '\', and '|'. Furthermore, names 
            // must not end with a trailing space or period.
            // Use: applications which must be portable to Windows.
            // Note: Reserved device names are not valid as file names, 
            // but are not being detected because they are still valid as 
            // a path. Specifically, CON, PRN, AUX, CLOCK$, NUL, COM[1-9],
            // LPT[1-9], and these names followed by an extension (for 
            // example, NUL.tx7).   

            const char _ = '_';

            u = u.Replace('<', _);
            u = u.Replace('>', _);
            u = u.Replace(':', _);
            u = u.Replace('"', _);
            u = u.Replace('\\', _);
            u = u.Replace('/', _);
            u = u.Replace('|', _);
            return u;
        }

        public static T AttributeOf<T>(System.Reflection.ICustomAttributeProvider v)
            where T : Attribute
        {
            object[] u = v.GetCustomAttributes(typeof(T), false);

            if (u.Length > 0)
                return (T)u[0];


            return null;
        }
    }
}
