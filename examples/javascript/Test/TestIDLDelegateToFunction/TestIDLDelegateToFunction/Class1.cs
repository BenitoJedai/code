using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestIDLDelegateToFunction
{
    [Script(HasNoPrototype = true, ExternalTarget = "Function")]
    public class IFunction
    {
    }

    [Script(InternalConstructor = true)]
    public class IWindow
    {
        //public int setTimeout(IFunction code, int time)
        //{
        //    return default(int);
        //}

        public int setTimeout(System.Action code, int time)
        {
            return default(int);
        }
    }


    [Script]
    // C# 6 shall import this static type and make members available!
    public static partial class Native
    {
        static public IWindow window;

    }

    [Script(Implements = typeof(global::System.Action))]
    internal delegate void __Action();

    [Script(Implements = typeof(global::System.Delegate))]
    internal class __Delegate
    {
        //  System.NotSupportedException: A Delegate implementation must have a target field.
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Target)]
        public object InternalTarget;

        // System.NotSupportedException: A Delegate implementation must have a method field.
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Method)]
        public global::System.IntPtr InternalMethod;


        public IFunction InternalMethodReference;

        // X:\jsc.svn\examples\javascript\test\TestIDLDelegateToFunction\TestIDLDelegateToFunction\Class1.cs
        [method: ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.AsFunction)]
        [Obsolete("called by the compiler")]
        public static IFunction AsFunction(__Delegate x)
        {
            if (x == null)
                return null;

            return x.InvokePointer;
        }

        
        // TODO: dom events and delay events do not support truly multiple targets
        public IFunction InvokePointer
        {
            get
            {
                if (InternalMethodReference == null)
                    InternalMethodReference = InternalGetAsyncInvoke(InternalTarget, InternalMethod);

                return InternalMethodReference;
            }
        }

        [Script(OptimizedCode = "return function() { return o[p].apply(o, arguments); }")]
        internal static IFunction InternalGetAsyncInvoke(object o, global::System.IntPtr p)
        {
            return default(IFunction);
        }
    }

    [Script(Implements = typeof(global::System.MulticastDelegate))]
    internal class __MulticastDelegate : __Delegate
    {
        // > System.NotSupportedException: A MulticastDelegate implementation must have a native array field.
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.List)]
        //IArray<__Delegate> list = new IArray<__Delegate>();
        object list;



    }

    [Script]
    public class Class1
    {
        public Class1()
        {
            // even if jsc adds the special magic
            // on call
            // taking a delegate of a native would for now not work for this purpose would it?

            //     AQAABACdPTOmWy468E_bStw.setTimeout(BQAABMKLvz2HMXSAHvdy6g, 24);
            //     AQAABACdPTOmWy468E_bStw.setTimeout(BgAABMKLvz2HMXSAHvdy6g.CAAABh90ODGTx1NEIsdbGw(), 24);
            Native.window.setTimeout(
                delegate
                {

                },
                24
            );

        }
    }
}
