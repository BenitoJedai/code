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
using AsyncButtonExperiment;
using AsyncButtonExperiment.Design;
using AsyncButtonExperiment.HTML.Pages;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace AsyncButtonExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // http://stackoverflow.com/questions/13974610/error-cannot-find-all-types-required-by-the-async-modifier-are-you-targeting


            // Error	1	Cannot find all types required by the 'async' modifier. Are you targeting the wrong framework version, or missing a reference to an assembly?	x:\jsc.svn\examples\javascript\AsyncButtonExperiment\AsyncButtonExperiment\Application.cs	36	17	AsyncButtonExperiment



            new IHTMLButton { innerText = "do async work" }.AttachToDocument().WhenClicked(
                async btn =>
                {

                    //var AtClick = new TaskCompletionSource<IHTMLButton>();

                    //btn.WhenClicked(AtClick.SetResult);

                    //await AtClick.Task;


                    btn.disabled = true;


                    Console.WriteLine("delay... ");
                    await Task.Delay(50);
                    Console.WriteLine("delay... done");

                    var task = await Task.Factory.StartNew(
                        new { goo = "goo " },
                        state =>
                        {
                            Console.WriteLine("will do some work in the background... "
                                + new
                                {
                                    System.Threading.Thread.CurrentThread.IsBackground,
                                    System.Threading.Thread.CurrentThread.ManagedThreadId
                                }

                            );

                            //Task.Delay
                            Thread.Sleep(3000);



                            Console.WriteLine("will do some work in the background... done!");

                            return "done";
                        }
                    );

                    new IHTMLPre { innerText = task }.AttachToDocument();

                    btn.disabled = false;

                }
            );

        }

    }
}
