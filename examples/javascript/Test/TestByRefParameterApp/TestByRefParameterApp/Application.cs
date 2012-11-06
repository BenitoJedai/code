using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestByRefParameterApp.Design;
using TestByRefParameterApp.HTML.Pages;

namespace TestByRefParameterApp
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        static void foo(ref string e)
        {
            e = "ref: " + e;
        }
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var u = "foo";

            foo(ref u);


            Native.Window.alert(u);

            var uu = new _button_Click1_d__0();

            uu.MoveNext();
        }

    }

    public struct _button_Click1_d__0 : __IAsyncStateMachine
    {
        __AsyncTaskMethodBuilder builder;

        public int i;

        // Error	1	Structs cannot contain explicit parameterless constructors	
        // X:\jsc.svn\examples\javascript\Test\TestByRefParameterApp\TestByRefParameterApp\Application.cs	55	16	
        // TestByRefParameterApp


        public _button_Click1_d__0(int ii = 0)
        {
            i = ii;
        }

        public void MoveNext()
        {
            i = 5;

            builder = new __AsyncTaskMethodBuilder();
            builder.Start(ref this);

            Console.WriteLine(new { i });
        }

        public void SetStateMachine(__IAsyncStateMachine stateMachine)
        {
            i = 6;
        }
    }


    public interface __IAsyncStateMachine
    {
        void MoveNext();

        void SetStateMachine(
            __IAsyncStateMachine stateMachine
        );
    }

    public struct __AsyncTaskMethodBuilder
    {
        public void Start<TStateMachine>(
          ref  TStateMachine stateMachine
      )
            where TStateMachine : __IAsyncStateMachine
        {
            // we need ref support in JSC!

            stateMachine.SetStateMachine(stateMachine);
        }
    }
}
