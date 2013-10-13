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
using TestYieldReturn;
using TestYieldReturn.Design;
using TestYieldReturn.HTML.Pages;

namespace TestYieldReturn
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
            ((IHTMLButton)"show").AttachToDocument().WhenClicked(
                async delegate
                {

                    var i = this.GetItems().ToArray();
                }
            );

        }

        public IEnumerable<object> GetItems()
        {
            Console.WriteLine("enter GetItems");
            for (int i = 0; i < 5; i++)
            {
                yield return new object();
            }
            Console.WriteLine("exit GetItems");
        }
    }
}
