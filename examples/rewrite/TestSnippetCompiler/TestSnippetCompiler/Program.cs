using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestSnippetCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.FirstChanceException +=
       (s, ex) =>
       {

       };


   //               at TestSnippetCompiler.Program.<Main>b__0(Object s, FirstChanceExceptionEventArgs ex) in x:\jsc.svn\examples\rewrite\TestSnippetCompiler\TestSnippetCompiler\Program.cs:line 25
   //at System.StubHelpers.ObjectMarshaler.ConvertToManaged(IntPtr pSrcVariant)
   //at IWshRuntimeLibrary.IWshShell_ClassClass.CreateShortcut(String PathLink)
   //at sliver.SnippetCompiler.Controls.Options.ShortcutsControl.CreateShortcut(DirectoryInfo directory)
   //at sliver.SnippetCompiler.Controls.Options.ShortcutsControl.CreateSendToMenu()
   //at sliver.SnippetCompiler.Program.Main(String[] args)
   //at System.RuntimeMethodHandle.InvokeMethod(Object target, Object[] arguments, Signature sig, Boolean constructor)
   //at System.Reflection.RuntimeMethodInfo.UnsafeInvokeInternal(Object obj, Object[] parameters, Object[] arguments)
   //at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   //at System.Reflection.MethodBase.Invoke(Object obj, Object[] parameters)
   //at TestSnippetCompiler.Program.Main(String[] args) in x:\jsc.svn\examples\rewrite\TestSnippetCompiler\TestSnippetCompiler\Program.cs:line 35
   //at System.AppDomain._nExecuteAssembly(RuntimeAssembly assembly, String[] args)
   //at System.AppDomain.ExecuteAssembly(String assemblyFile, Evidence assemblySecurity, String[] args)
   //at Microsoft.VisualStudio.HostingProcess.HostProc.RunUsersAssembly()
   //at System.Threading.ThreadHelper.ThreadStart_Context(Object state)
   //at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
   //at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
   //at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
   //at System.Threading.ThreadHelper.ThreadStart()

            // RewriteToAssembly error: System.InvalidOperationException: OpCodes.Ldtoken

            //var t = typeof(sliver.SnippetCompiler.IAddIn).Assembly.GetTypes();
            //// {Name = "Program" FullName = "sliver.SnippetCompiler.Program"}

            ////            >	SnippetCompiler.exe!sliver.SnippetCompiler.Controls.Options.ShortcutsControl.CreateShortcut(System.IO.DirectoryInfo directory) + 0x84 bytes	

            ////Additional Information: An invalid VARIANT was detected during a conversion from an 
            //// unmanaged VARIANT to a managed object. Passing invalid VARIANTs to the CLR can cause unexpected exceptions, 
            //// corruption or data loss.

   
            //var Main = typeof(sliver.SnippetCompiler.IAddIn).Assembly.GetType("sliver.SnippetCompiler.Program").GetMethod("Main",

            //    BindingFlags.Static | BindingFlags.NonPublic
            //    );


            //Main.Invoke(null, new[] { args });
        }
    }
}
