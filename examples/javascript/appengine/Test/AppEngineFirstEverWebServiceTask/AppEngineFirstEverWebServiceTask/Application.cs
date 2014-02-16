using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AppEngineFirstEverWebServiceTask;
using AppEngineFirstEverWebServiceTask.Design;
using AppEngineFirstEverWebServiceTask.HTML.Pages;

namespace AppEngineFirstEverWebServiceTask
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {


            this.AsyncVoid();

            new IHTMLButton { "AsyncTask" }.AttachToDocument().WhenClicked(
                async button =>
                {
                    //02000012 AppEngineFirstEverWebServiceTask.Application+ctor>b__1>d__3+<MoveNext>0600000b
                    //{ Location =
                    // assembly: V:\AppEngineFirstEverWebServiceTask.Application.exe
                    // type: AppEngineFirstEverWebServiceTask.Application+ctor>b__1>d__3+<MoveNext>0600000b, AppEngineFirstEverWebServiceTask.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
                    // offset: 0x002f
                    //  method:Int32 <0035> ldarg.0.try(<MoveNext>0600000b, ctor>b__1>d__3 ByRef, System.Runtime.CompilerServices.TaskAwaiter ByRef, System.Runtime.CompilerServices.TaskAwaiter ByRef) }
                    //script: error JSC1000: Method: <0035> ldarg.0.try, Type: AppEngineFirstEverWebServiceTask.Application+ctor>b__1>d__3+<MoveNext>0600000b; emmiting failed : System.NotImplementedException: { ParameterType = AppEngineFirstEverWebServiceTask.Application+ctor>b__1>d__3&, p = [0x002f] call       +0 -3{[0x001b] ldflda     +1 -1{[0x001a] ldind.ref  +1 -1{[0x0018] ldarg.s    +1 -0} } } {[0x002a] ldarg.s    +1 -0} {[0x002e] ldind.ref  +1 -1{[0x002c] ldarg.s    +1 -0} } , m = Void AwaitUnsafeOnCompleted[TaskAwaiter,ctor>b__1>d__3](System.Runtime.CompilerServices.TaskAwaiter ByRef, ctor>b__1>d__3 ByRef) }

                    await AsyncTask();
                }
            );
        }

    }
}
