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
using TestCoalescingOperator;
using TestCoalescingOperator.Design;
using TestCoalescingOperator.HTML.Pages;

namespace TestCoalescingOperator
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static void y(string innerText)
        {
            var x = innerText ?? "server returned null";
            //var x = innerText;

            Native.document.body.innerText = x;
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Native.document.body.Clear();



            new Action(
                async delegate
                {

                    while (true)
                    {
                        var innerText = await this.GetString();

                        ////Console.WriteLine(new { innerText });
                        //y(innerText);

                        //                        02000013 TestCoalescingOperator.Application+ctor>b__0>d__2+<MoveNext>06000006
                        //script: error JSC1000: *** stack is empty, invalid pop?
                        //script: error JSC1000: error at TestCoalescingOperator.Application+ctor>b__0>d__2+<MoveNext>06000006.<00c5> pop.try,
                        // assembly: V:\TestCoalescingOperator.Application.exe
                        // type: TestCoalescingOperator.Application+ctor>b__0>d__2+<MoveNext>06000006, TestCoalescingOperator.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

                        //Action y = delegate
                        //{
                        //    var z = innerText ?? "server returned null";

                        //    Native.document.body.innerText = z;
                        //};

                        //y();

                        y(innerText);


                        await Task.Delay(1000);
                    }

                }
            )();
        }

    }
}
