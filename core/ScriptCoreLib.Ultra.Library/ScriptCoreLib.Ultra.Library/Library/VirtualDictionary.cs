using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Runtime.CompilerServices;

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

        public int TransientTransactionCounter;



        // either override the dictionary or the resolve method itself?
        public Action<TKey> Resolve;
        //public event Action<TKey> Resolve;

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
            //[method: DebuggerStepThrough]
            [method: DebuggerHidden]
            get
            {
                return k.Select(kk => this[kk]).ToArray();
            }
        }

        // prevents reentrancy?
        //public object GetItemSync = new object();

        public TValue this[TKey k]
        {
            //[method: DebuggerStepThrough]
            [method: DebuggerHidden]
            get
            {
                //            >	ScriptCoreLib.Ultra.Library.dll!jsc.Library.VirtualDictionary<System.Reflection.ConstructorInfo,System.Reflection.ConstructorInfo>.this[System.Reflection.ConstructorInfo].get(System.Reflection.ConstructorInfo k) Line 91 + 0x10 bytes	C#
                //jsc.meta.exe!jsc.meta.Library.ILStringConversions.Prepare.AnonymousMethod__6e() + 0x277 bytes	
                //jsc.meta.exe!jsc.meta.Library.ILGeneratorExtensions.EmitWithParameter(System.Reflection.Emit.ILGenerator il, int ParameterPosition, System.Action handler) + 0x248 bytes	

                //jsc.meta.exe!jsc.meta.Library.ILStringConversions.Prepare.AnonymousMethod__6d(System.Type CacheType, jsc.meta.Library.ILStringConversions.ILStringConversionArguments e) + 0x5b8 bytes	
                //jsc.meta.exe!jsc.meta.Library.ILStringConversions.ILStringConversion..ctor.AnonymousMethod__de(jsc.meta.Library.ILStringConversions.ILStringConversionArguments e) + 0x6f bytes	
                //jsc.meta.exe!jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument.WebServiceForJavaScript.WriteGetFields(System.Reflection.Emit.ILGenerator InvokeCallback_il, System.Action fthis, System.Action fWebRequest) + 0x5b9 bytes	

                // Error	11	Operator '==' cannot be applied to operands of type 'TKey' and 'TKey'	x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs	96	21	ScriptCoreLib.Ultra.Library
                if (Comparer<TKey>.Default.Compare(k, default(TKey)) == 0)
                    Debugger.Break();


                //lock (GetItemSync)
                //{
                if (!BaseDictionary.ContainsKey(k))
                {

                    RaiseResolve(k);
                }
                //}

                // prevent deadlock?
                Thread.Yield();


                //lock (GetItemSync)
                //{
                if (!BaseDictionary.ContainsKey(k))
                {
                    // what happened? why still no exist?

                    //if (Debugger.IsAttached)
                    //    Debugger.Break();

                    return default(TValue);
                }

                return BaseDictionary[k];
                //}
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
        static int OtherThreadCounter;

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201306/20130614-ilspy
        // need reentrancy!
        //[MethodImpl(MethodImplOptions.Synchronized)]
        [method: DebuggerNonUserCode]
        [method: DebuggerHidden]
        private void RaiseResolve(TKey k)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402/scriptcorelibandroid-natives

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

                OtherThreadCounter++;

                // a nice diagram or animation here would be great :)
                // initialized only once!
                OtherThread = new Thread(
                    delegate()
                    {
                        // for reuse later
                        while (true)
                        {
                            OtherThreadWaitInput.WaitOne();
                            //Console.WriteLine("enter " + OtherThread.ManagedThreadId + " from " + CurrentThread.ManagedThreadId);
                            try
                            {
                                OtherThreadContinuation();
                            }
                            catch (Exception ex)
                            {
                                if (Debugger.IsAttached)
                                    Debugger.Break();

                                throw;
                            }
                            finally
                            {

                                //Console.WriteLine("exit " + OtherThread.ManagedThreadId + " from " + CurrentThread.ManagedThreadId);

                                lock (OtherTheadLock)
                                {
                                    OtherThreadWaitOutput.Set();
                                }
                            }
                        }
                    }
                )
                {
                    IsBackground = true,

                    // concept: continuation of a thread
                    Name = ":" + CurrentThread.ManagedThreadId + " *" + OtherThreadCounter
                };
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

            OtherThreadWaitOutput.WaitOne();
            OtherThreadContinuation = null;

            //Console.WriteLine(CurrentThread.ManagedThreadId + " join to " + OtherThread.ManagedThreadId);

            return;


        }

        public static implicit operator Func<TKey, TValue>(VirtualDictionary<TKey, TValue> e)
        {
            if (e == null)
                return null;

            return k => e[k];
        }

        public override IDisposable ToTransientTransaction()
        {
            //var BaseDictionary = this.BaseDictionary;
            var Flags = this.Flags;


            // 
            var old = new { this.BaseDictionary };

            //this.BaseDictionary = new Dictionary<TKey, TValue>(BaseDictionary);
            var n = new Dictionary<TKey, TValue>();

            // +		$exception	{"The given key was not present in the dictionary."}	System.Exception {System.Collections.Generic.KeyNotFoundException}

            foreach (KeyValuePair<TKey, TValue> item in old.BaseDictionary)
            {
                // workaround for if the object identity has changed?
                try
                {

                    n[item.Key] = item.Value;

                }
                catch
                {
                    Debugger.Break();
                }
            }

            this.BaseDictionary = n;

            this.Flags = new Dictionary<TKey, object>(Flags);
            this.TransientTransactionCounter++;

            return (Disposable)
                delegate
                {
                    this.TransientTransactionCounter--;

                    this.BaseDictionary = old.BaseDictionary;
                    this.Flags = Flags;
                };
        }
    }
}
