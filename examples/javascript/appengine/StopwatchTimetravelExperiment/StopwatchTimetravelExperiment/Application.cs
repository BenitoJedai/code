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
using StopwatchTimetravelExperiment;
using StopwatchTimetravelExperiment.Design;
using StopwatchTimetravelExperiment.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.Lambda;

namespace StopwatchTimetravelExperiment
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
            // should jsc autobind these? which way?
            page.output = this.output;
            this.head = page.head;
            //this.title = page.title;


            //this.output.AsHTMLElement().AttachToDocument();
            //this.output.AttachToDocument();

            //page.output = this.output.AsHTMLElement();


            new IHTMLHeader1 { 
                //() => new { this.Sessionwatch.ElapsedMilliseconds, this.Sessionwatch.IsRunning } 
                //async
                delegate
                {
                    if (this.Sessionwatch == null)
                        return new { Sessionwatch = "null" };

                    //this.SessionwatchReflection = null;
                    ////await this.yield();

                    ////Console.WriteLine(
                    ////    new { Sessionwatch, SessionwatchReflection }
                    ////    );

                    //if (SessionwatchReflection == null)
                    //    return new { SessionwatchReflection = "null"};

                    return new { 
                        Sessionwatch = this.Sessionwatch.ElapsedMilliseconds,
                        //SessionwatchReflection = this.SessionwatchReflection.ElapsedMilliseconds 
                    };
                }
            
            }.AttachToDocument();


            //this.output.


            //this.output.AsHTMLElement().AttachToDocument().With(
            //    output =>
            //    {

            //        this.output = output;
            //    }
            //);


            new IHTMLButton { "DoDatabase" }.AttachToDocument().WhenClicked(this.DoDatabase);
            new IHTMLButton { "ShowTitle" }.AttachToDocument().WhenClicked(this.ShowTitle);
            new IHTMLButton { "SetTitle" }.AttachToDocument().WhenClicked(this.SetTitle);



            new IHTMLButton { "click" }.AttachToDocument().WhenClicked(
                delegate
                {
                    new Stopwatch().With(
                        async Watch1 =>
                        {

                            Watch1.Start();
                            Console.WriteLine("+0 " + new { Watch1.ElapsedMilliseconds });
                            await 1;
                            Console.WriteLine("+1 " + new { Watch1.ElapsedMilliseconds });

                            await 100;
                            Console.WriteLine("+100 " + new { Watch1.ElapsedMilliseconds });

                            await 222;
                            Console.WriteLine("+222 " + new { Watch1.ElapsedMilliseconds });

                            var e = await this.WebMethod2(
                                new TheWatchers
                                {
                                    Watch1 = Watch1
                                }
                            );

                            Native.document.title = new { e.Watch1.ElapsedMilliseconds }.ToString();

                            Native.document.body.style.borderLeft = "1em solid red";
                            //await e.Watch1;
                            await (int)e.Watch1.ElapsedMilliseconds;

                            Native.document.body.style.borderLeft = "1em solid yellow";

                        }
                    );
                }
            );



        }

    }


}
