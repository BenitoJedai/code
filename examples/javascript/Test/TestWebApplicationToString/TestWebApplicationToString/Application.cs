using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestWebApplicationToString;
using TestWebApplicationToString.Design;
using TestWebApplicationToString.HTML.Pages;

namespace TestWebApplicationToString
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

            page.Content = (this + 33).ToString();


            new ContentContainer
            {
                Content = this.GetItem(44)
            }.AttachToDocument();

       


            ((IHTMLButton)"Refresh").AttachToDocument().WhenClicked(
                async Refresh =>
                {
                    Console.WriteLine("before " + new { this.Count });
                    Refresh.innerText = "before " + new { this.Count }.ToString();

                    await this.Refresh();


                    Refresh.innerText = "after " + new { this.Count }.ToString();
                    Console.WriteLine("after " + new { this.Count });



                    Action yield =
                           delegate
                           {
                               //<T> and async dont mix yet?
                               foreach (var item in this.GetItems())
                               {
                                   new ContentContainer
                                   {
                                       Content = item
                                   }.AttachToDocument();

                               }
                           };


                    yield();


                }
            );
        }



    }
}
