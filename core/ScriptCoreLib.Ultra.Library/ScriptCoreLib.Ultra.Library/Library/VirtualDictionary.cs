﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace jsc.Library
{
    [Obfuscation(Exclude = true)]
    public abstract class VirtualDictionaryBase
    {
        public abstract IDisposable ToTransientTransaction();


        public static VirtualDictionary<TKey, TValue> Create<TKey, TValue>(Func<TKey, TValue> handler)
        {
            var v = new VirtualDictionary<TKey, TValue>();

            v.Resolve +=
                e =>
                {
                    v[e] = handler(e);
                };

            return v;
        }
    }

    //class InternalTypeGUIDComparer : IEqualityComparer<Type>
    //{
    //    public bool Equals(Type x, Type y)
    //    {
    //        return GetHashCode(x) == GetHashCode(y);
    //    }

    //    public int GetHashCode(Type SourceType)
    //    {
    //        if (SourceType.IsGenericType)
    //            if (!SourceType.IsGenericTypeDefinition)
    //                return SourceType.GetHashCode();

    //        return SourceType.GUID.GetHashCode();
    //    }
    //}

    [Obfuscation(Exclude = true)]
    public class VirtualDictionary<TKey, TValue> : VirtualDictionaryBase
    {
        public Dictionary<TKey, TValue> BaseDictionary = new Dictionary<TKey, TValue>();

        // refactor to upper level type?
        public Dictionary<TKey, object> Flags = new Dictionary<TKey, object>();


        public event Action<TKey> Resolve;

        public object Tag;

        public VirtualDictionary()
        {
            //if (typeof(TKey) == typeof(Type))
            //{
            //    this.BaseDictionary = new Dictionary<TKey, TValue>((IEqualityComparer<TKey>)(object)new InternalTypeGUIDComparer());
            //}
        }

        public TValue[] this[TKey[] k]
        {
            [method: DebuggerStepThrough]
            get
            {
                return k.Select(kk => this[kk]).ToArray();
            }
        }


        public object GetItemSync = new object();

        public TValue this[TKey k]
        {
            //[method: DebuggerStepThrough]
            get
            {
                lock (GetItemSync)
                {
                    if (!BaseDictionary.ContainsKey(k))
                    {

                        RaiseResolve(k);
                    }

                    return BaseDictionary[k];
                }
            }
            set
            {
                // Index was outside the bounds of the array.
                // ??
                BaseDictionary[k] = value;
            }
        }

        Thread CurrentThread;
        Thread OtherThread;
        Action OtherThreadContinuation;
        AutoResetEvent OtherThreadWaitInput;
        AutoResetEvent OtherThreadWaitOutput;
        object OtherTheadLock = new object();

        private void RaiseResolve(TKey k)
        {
            var FrameCount = new StackTrace().FrameCount;
            if (FrameCount < 0x400)
            {
                if (Resolve != null)
                    Resolve(k);

                return;
            }

            // switch to the other thread

            #region OtherThread
            if (OtherThread == null)
            {
                CurrentThread = Thread.CurrentThread;

                OtherThreadWaitInput = new AutoResetEvent(false);
                OtherThreadWaitOutput = new AutoResetEvent(false);
                OtherThread = new Thread(
                    delegate()
                    {
                        while (true)
                        {
                            OtherThreadWaitInput.WaitOne();
                            //Console.WriteLine("enter " + OtherThread.ManagedThreadId + " from " + CurrentThread.ManagedThreadId);
                            try
                            {
                                OtherThreadContinuation();
                                //Console.WriteLine("exit " + OtherThread.ManagedThreadId + " from " + CurrentThread.ManagedThreadId);
                            }
                            catch (Exception ex)
                            {
                                if (Debugger.IsAttached)
                                    Debugger.Break();

                                throw;
                            }
                            finally
                            {
                                lock (OtherTheadLock)
                                {
                                    OtherThreadWaitOutput.Set();
                                }
                            }
                        }
                    }
                ) { IsBackground = true };
                OtherThread.Start();
            }
            #endregion

            if (CurrentThread != Thread.CurrentThread)
            {
                // we are being called from a random thead!
                // time to upgrade to third thread?

                if (Resolve != null)
                    Resolve(k);

                return;
            }

            OtherThreadContinuation = delegate
            {
                if (Resolve != null)
                    Resolve(k);
            };

            // will this help?
            lock (OtherTheadLock)
            {
                //Console.WriteLine(CurrentThread.ManagedThreadId + " switch to " + OtherThread.ManagedThreadId);
                OtherThreadWaitInput.Set();
                Thread.Yield();
            }
            Thread.Yield();

            //Console.WriteLine(CurrentThread.ManagedThreadId + " join to " + OtherThread.ManagedThreadId);
            OtherThreadWaitOutput.WaitOne();
            OtherThreadContinuation = null;

            return;


        }

        public static implicit operator Func<TKey, TValue>(VirtualDictionary<TKey, TValue> e)
        {
            return k => e[k];
        }

        public override IDisposable ToTransientTransaction()
        {
            var BaseDictionary = this.BaseDictionary;
            var Flags = this.Flags;

            this.BaseDictionary = new Dictionary<TKey, TValue>(BaseDictionary);
            this.Flags = new Dictionary<TKey, object>(Flags);

            return (Disposable)
                delegate
                {
                    this.BaseDictionary = BaseDictionary;
                    this.Flags = Flags;
                };
        }
    }
}
