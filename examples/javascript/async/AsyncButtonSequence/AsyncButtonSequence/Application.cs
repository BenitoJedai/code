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
using AsyncButtonSequence;
using AsyncButtonSequence.Design;
using AsyncButtonSequence.HTML.Pages;
using System.Threading.Tasks;

namespace AsyncButtonSequence
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

            page.Button1.WhenClicked(
                async button =>
            {
                page.Button1.style.color = "red";

                await page.Button1.async.onclick;
                //await page.Button1;

                page.Button1.style.color = "blue";
                page.Button2.style.color = "red";



                // Error	3	'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton' 
                // does not contain a definition for 'GetAwaiter' 
                // and the best extension method overload 
                // 'ScriptCoreLib.JavaScript.DOM.IXMLHttpRequestAsyncExtensions
                // .GetAwaiter(ScriptCoreLib.JavaScript.DOM.IXMLHttpRequest)' 
                // requires a receiver of type 
                // 'ScriptCoreLib.JavaScript.DOM.IXMLHttpRequest'	X:\jsc.svn\examples\javascript\async\AsyncButtonSequence\AsyncButtonSequence\Application.cs	45	27	AsyncButtonSequence


                await page.Button2;

                page.Button2.style.color = "blue";
                page.Button3.style.color = "red";

                await page.Button3;

                for (int i = 0; i < 4; i++)
                {
                    Native.document.title = new { i }.ToString();

                    page.Button1.style.color = "green";
                    page.Button2.style.color = "green";
                    page.Button3.style.color = "green";

                    await Task.Delay(300);


                    page.Button1.style.color = "red";
                    page.Button2.style.color = "red";
                    page.Button3.style.color = "red";

                    await Task.Delay(300);

                }


                page.Button1.style.color = "";
                page.Button2.style.color = "";
                page.Button3.style.color = "";

            }
            );
        }

    }
}
