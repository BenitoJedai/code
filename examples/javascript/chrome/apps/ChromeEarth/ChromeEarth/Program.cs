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


            //02000031 ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__Form
            //{ Location =
            // assembly: X:\jsc.svn\examples\javascript\chrome\apps\ChromeEarth\ChromeEarth\bin\Debug\ScriptCoreLib.Windows.Forms.dll
            // type: ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__Form, ScriptCoreLib.Windows.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x0357
            //  method:Void set_WindowState(System.Windows.Forms.FormWindowState) }
            //script: error JSC1000: Method: set_WindowState, Type: ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__Form; emmiting failed : System.MissingMethodException: Method not found: 'Void ScriptCoreLib.JavaScript.DOM.IWindow.add_onframe(System.Action`1<Int32>)'.

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
