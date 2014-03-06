using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace ChromeEarth
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //2a04:01:01 RewriteToAssembly error: System.MissingMethodException: Method not found: 'ScriptCoreLib.JavaScript.DOM.IFunction ScriptCoreLib.JavaScript.DOM.IFunction.op_Implicit(System.Action)'.
            //   at System.ModuleHandle.ResolveMethod(RuntimeModule module, Int32 methodToken, IntPtr* typeInstArgs, Int32 typeInstCount, IntPtr* methodInstArgs, Int32 methodInstCount)
            //   at System.ModuleHandle.ResolveMethodHandleInternalCore(RuntimeModule module, Int32 methodToken, IntPtr[] typeInstantiationContext, Int32 typeInstCount, IntPtr[] methodInstantiationContext, Int32 methodInstCount)
            //   at System.ModuleHandle.ResolveMethodHandleInternal(RuntimeModule module, Int32 methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
            //   at System.Reflection.RuntimeModule.ResolveMethod(Int32 metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
