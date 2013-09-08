using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading
{
    [Script(Implements = typeof(global::System.Threading.Thread))]
    internal class __Thread
    {
        public java.lang.Thread InternalValue;

        [Script]
        class RunnableHandler : java.lang.Runnable
        {
            public Action Handler;

            public void run()
            {
                Handler();
            }
        }

        #region ctor
        internal __Thread()
        {

        }

        public __Thread(global::System.Threading.ThreadStart t)
        {
            InternalValue = new java.lang.Thread(
                new RunnableHandler
                {
                    Handler =
                        delegate
                        {
                            t();
                        }
                }
            );
        }

        public __Thread(global::System.Threading.ParameterizedThreadStart t)
        {
            InternalValue = new java.lang.Thread(
                      new RunnableHandler
                      {
                          Handler =
                              delegate
                              {
                                  t(InternalParameterizedThreadStart);
                              }
                      }
                  );
        }
        #endregion


        public static implicit operator global::System.Threading.Thread(__Thread e)
        {
            return (global::System.Threading.Thread)(object)e;
        }

        public int ManagedThreadId
        {
            get
            {
                return (int)this.InternalValue.getId();
            }
        }

//Implementation not found for type import :
//type: System.Threading.Thread
//method: System.Threading.Thread get_CurrentThread()
//Did you forget to add the [Script] attribute?
//Please double check the signature!


        public static global::System.Threading.Thread CurrentThread
        {
            get
            {
                return new __Thread { InternalValue = java.lang.Thread.currentThread() };

            }
        }

        public static void Sleep(int millisecondsTimeout)
        {
            try
            {
                java.lang.Thread.sleep(millisecondsTimeout);
            }
            catch
            {

                throw;
            }
        }

        public object InternalParameterizedThreadStart;

        public void Start(object parameter)
        {
            InternalParameterizedThreadStart = parameter;
            InternalValue.start();
        }

        public void Start()
        {
            InternalValue.start();
        }

        public string Name
        {
            get
            {
                return InternalValue.getName();
            }
            set
            {
                InternalValue.setName(value);
            }
        }

        public bool IsAlive
        {
            get
            {
                return InternalValue.isAlive();
            }
        }

        public bool IsBackground { get { return InternalValue.isDaemon(); } set { InternalValue.setDaemon(value); } }

        public void Join()
        {
            try
            {
                InternalValue.join();
            }
            catch
            {
                throw;
            }
        }

        public bool Join(int ms)
        {
            try
            {
                InternalValue.join(ms);
            }
            catch
            {
                throw;
            }

            return !InternalValue.isAlive();
        }

        public void Abort()
        {
            InternalValue.stop();
        }
    }
}
