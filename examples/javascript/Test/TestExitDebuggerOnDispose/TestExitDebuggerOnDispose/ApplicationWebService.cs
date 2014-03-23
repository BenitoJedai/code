using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestExitDebuggerOnDispose
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService : IDisposable
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }


        public void Dispose()
        {
            if (Debugger.IsAttached)
            {
                //Not all privileges or groups referenced are assigned to the caller
                //Process.LeaveDebugMode();

                Process.GetCurrentProcess().Kill();




                //z.DebugActiveProcessStop(
                //    Process.GetCurrentProcess().Id
                //);

            }
        }


    }

    //static class z
    //{

    //    //at jsc.ILBlock.get_First()
    //    //at jsc.ILBlock.get_Prestatements()
    //    //at  .    .    (     , MethodInfo , ScriptAttribute )
    //    //at  .    .    (     , MethodBase[] )
    //    //at  .    .    (     , Type[] , Boolean , ScriptAttribute , Assembly , CompileSessionInfo )
    //    //at jsc.Program.ConvertAssamblySpawnedWithinContext(String , ScriptType , CompileSessionInfo )
    //    //at jsc.Program.ConvertAssamblySpawned(String , ScriptType , CompileSessionInfo )
    //    //at jsc.Program.InternalMain(CompileSessionInfo )
    //    // http://stackoverflow.com/questions/7480518/programmatically-detach-debugger

    //    [DllImport("kernel32.dll")]
    //    [return: MarshalAs(UnmanagedType.Bool)]
    //    public static extern bool DebugActiveProcessStop([In] int Pid);
    //}
}
