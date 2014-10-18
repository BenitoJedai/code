
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared;

using ScriptCoreLib;
using System;
using ScriptCoreLib.JavaScript.BCLImplementation.System;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Function.webidl

    [Script(HasNoPrototype = true, ExternalTarget = "Function")]
    public class IFunction
    {
        public Expando prototype;

        #region ctor
        //[System.Obsolete("Chrome Applications will not like this unless jsc makes them literal functions via [Script(OptimizedCode...")]
        public IFunction(string body)
        {

        }

        public IFunction(string arg0, string body)
        {

        }

        public IFunction(string arg0, string arg1, string body)
        {

        }

        public IFunction(string arg0, string arg1, string arg3, string body)
        {

        }
        #endregion



        [Script(OptimizedCode = @"return new f();")]
        static Expando CreateType(IFunction f)
        {
            return default(Expando);
        }

        /// <summary>
        /// returns new object from function
        /// </summary>
        /// <returns></returns>
        [Script(DefineAsStatic = true)]
        public Expando CreateType()
        {
            return CreateType(this);
        }


        #region invoke
        [Script(OptimizedCode = @"return {arg0}({arg1})", UseCompilerConstants = true)]
        public static object Invoke(IFunction f, object a0)
        {
            return null;
        }

        [Script(DefineAsStatic = true)]
        public object Invoke(object a0)
        {
            return Invoke(this, a0);
        }

        [Script(OptimizedCode = @"return {arg0}({arg1}, {arg2}, {arg3});", UseCompilerConstants = true)]
        public static object Invoke(IFunction f, object a0, object a1, object a2)
        {
            return null;
        }


        public object apply(object o, params object[] args)
        {
            return null;
        }


        [Script(DefineAsStatic = true)]
        public object Invoke(object a0, object a1, object a2)
        {
            return Invoke(this, a0, a1, a2);
        }
        #endregion



        [Obsolete]
        public static IFunction Of(object target, string name)
        {
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs

            return Expando.Of(target).GetMember<IFunction>(name);
        }

        [Obsolete]
        public static IFunction Of(string name)
        {
            // tested by x:\jsc.svn\examples\javascript\Test\TestThreadStart\TestThreadStart\Application.cs

            return Expando.Of(Native.self).GetMember<IFunction>(name);
        }

        public static Task<IFunction> ByName(string name, object target = null)
        {
            // X:\jsc.svn\examples\javascript\Test\TestHistoryForwardEvent\TestHistoryForwardEvent\Application.cs
            Console.WriteLine("enter IFunction.ByName " + new { name });

            // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\DOM\HistoryExtensions.cs

            // tested by
            // X:\jsc.svn\examples\javascript\WorkerInsideSecondaryApplication\WorkerInsideSecondaryApplicationWithStateReplaceTwice\Application.cs

            if (target == null)
                target = Native.self;

            var x = new TaskCompletionSource<IFunction>();

            Action y = delegate
            {
                if (Expando.InternalIsMember(target, name))
                {
                    // should wo typecheck that member?
                    var f = (IFunction)Expando.InternalGetMember(target, name);

                    x.SetResult(f);
                    return;
                }

                // ?
            };

            y();

            if (!x.Task.IsCompleted)
            {
                new Timer(
                    t =>
                    {
                        y();

                        if (x.Task.IsCompleted)
                            t.Stop();
                    }
                ).StartInterval(15);
            }

            return x.Task;
        }

        // used by?
        [Obsolete]
        public static IFunction Of(System.Action h)
        {
            return ((BCLImplementation.System.__Delegate)(object)h).InvokePointer;
        }

        // used by?
        [Obsolete]
        public static IFunction Of<TArg>(System.Action<TArg> h)
        {
            return ((BCLImplementation.System.__Delegate)(object)h).InvokePointer;
        }


        public static explicit operator MulticastDelegate(IFunction f)
        {
            var x = new __MulticastDelegate(default(object), default(IntPtr)) { InternalMethodReference = f };

            return (MulticastDelegate)(object)x;
        }



        // public static implicit operator IFunction(Delegate h);
        // X:\jsc.svn\examples\javascript\synergy\webgl\WebGLEarthByBjorn\WebGLEarthByBjorn\Application.cs


        public static implicit operator IFunction(global::System.Delegate h)
        {
            return OfDelegate(h);
        }

        public static IFunction OfDelegate(global::System.Delegate h)
        {
            // called by
            // // chrome.app+runtime.add_Restarted

            if (h == null)
                return null;


            BCLImplementation.System.__Delegate hh = h;

            return hh.InvokePointer;
        }


        [Obsolete]
        [Script(DefineAsStatic = true)]
        public void Export(string name)
        {
            Expando.ExportCallback(name, this);
        }

        [Obsolete]
        public static void Export(string name, System.Action h)
        {
            IFunction.Of(h).Export(name);
        }


    }
}