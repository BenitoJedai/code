
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared;

using ScriptCoreLib;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "Function")]
    public class IFunction
    {
        public Expando prototype;

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

        public static IFunction Of(object target, string name)
        {
            return Expando.Of(target).GetMember<IFunction>(name);
        }

        public static IFunction Of(string name)
        {
            return Expando.Of(Native.window).GetMember<IFunction>(name);
        }

        public static IFunction Of(System.Action h)
        {
            return ((BCLImplementation.System.__Delegate)(object)h).InvokePointer;
        }

        public static IFunction Of<TArg>(System.Action<TArg> h)
        {
            return ((BCLImplementation.System.__Delegate)(object)h).InvokePointer;
        }




        public static implicit operator IFunction(global::System.Delegate h)
        {
            return OfDelegate(h);
        }

        public static IFunction OfDelegate(global::System.Delegate h)
        {
            return ((BCLImplementation.System.__Delegate)(object)h).InvokePointer;
        }


        [Script(DefineAsStatic = true)]
        public void Export(string name)
        {
            Expando.ExportCallback(name, this);
        }

        public static void Export(string name, System.Action h)
        {
            IFunction.Of(h).Export(name);
        }


    }
}