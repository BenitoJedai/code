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
using TestCharLower;
using TestCharLower.Design;
using TestCharLower.HTML.Pages;

namespace TestCharLower
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
            var any = "abC".ToCharArray().Select(
                c =>
                {
                    //13ms { cAsInteger = a } view-source:35573
                    //13ms { c = a } 

                    var cAsInteger = (int)c;

                    Console.WriteLine(new { cAsInteger });

                    //8ms GetInternalFields load fromlocalstorage!  view-source:35573
                    //38ms { c = a } view-source:35573
                    //39ms { cToString = a } view-source:35573
                    //39ms { cAsString =  } view-source:35573
                    //40ms { cAsString =  } view-source:35573
                    //40ms { any = true }

                    Console.WriteLine(new { c });

                    var cToString = c.ToString();

                    // 111
                    Console.WriteLine(new { cToString });

                    var cAsString = new string(c, 1);

                    Console.WriteLine(new { cAsString });

                    var isLower = cAsString.ToLower() == cAsString;

                    Console.WriteLine(new { cAsString });

                    return isLower;
                }
            ).Any();

            Console.WriteLine(new { any });


            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //this.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
