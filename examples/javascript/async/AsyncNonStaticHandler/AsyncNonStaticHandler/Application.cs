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
using AsyncNonStaticHandler;
using AsyncNonStaticHandler.Design;
using AsyncNonStaticHandler.HTML.Pages;
using System.Threading;

namespace AsyncNonStaticHandler
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
            // what if we wanted to talk to DOM from the other thread? would we need to build call proxies?

            new IHTMLButton { "invoke" }.AttachToDocument().onclick += async delegate
            {
                // Uncaught Error: InvalidOperationException: { MethodToken = AQAABh_bM8z6My5NDh7GXlQ } function is not available at

                //                0:16345ms InternalInitializeInlineWorker { Target = [object Object], TargetType = < Namespace >.InternalTaskExtensionsScope_2 }
                //                view - source:40541
                //0:16347ms { InternalTaskExtensionsScope_function = [object Object] }

                // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Extensions\TaskExtensions.cs
                var z = await Task.Factory.StartNew(WorkerHandler, state: this);


                //Task.Run(

                new IHTMLPre { "done " + new { z } }.AttachToDocument();
            };
        }

        //public static string WorkerHandler(Application that)
        //public string WorkerHandler(Application that)
        public string WorkerHandler(object that)
        {
            // look we are in a background thread! {{ this = [Application] {{ Field1 = Field1 }}, that = [Application] {{ Field1 = Field1 }} }}0:1399ms
            // look we are in a background thread! {{ this = [Application] {{ Field1 = null }}, that = [Application] {{ Field1 = Field1 }} }}0:8413ms  
            //                href = http://192.168.43.252:28846/view-source#worker, MethodTargetTypeIndex = type$GV0nCx_bM8z6My5NDh7GXlQ, MethodTargetType = <Namespace>.Application, MethodToken = AQAABh_bM8z6My5NDh7GXlQ, MethodType = FuncOfObjectToObject, IsIProgress = false, IsTuple2_Item1_IsIProgress = false, ManagedThreadId = 10, stateTypeHandleIndex = type$GV0nCx_bM8z6My5NDh7GXlQ, stateType = <Namespace>.Application, state = [object Object] }0:5639ms  view-source:40612
            //look we are in a background thread!0:5643ms

            // done {{ z = {{ x = do we send typeinfo back already? do we support Task<>? what about non static handler? {{ ManagedThreadId = 10 }} }} }}

            //0:21733ms InternalInitializeInlineWorker { Target = [object Object] }
            //0:21734ms { InternalTaskExtensionsScope_function = [object Object] }


            Console.WriteLine("look we are in a background thread! " + new { @this = this, that });

            return new
            {
                x = "do we send typeinfo back already? do we support Task<>? what about non static handler? " + new { Thread.CurrentThread.ManagedThreadId }
            }.ToString();
        }

        public override string ToString()
        {
            return "[Application] " + new { Field1 };
        }
    }
}
