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
using AsyncNestedServerCallbacks;
using AsyncNestedServerCallbacks.Design;
using AsyncNestedServerCallbacks.HTML.Pages;
using System.Threading.Tasks;

namespace AsyncNestedServerCallbacks
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

            Func<IHTMLButton, string> GetToken =
                xbutton =>
                {
                    var a = xbutton.attributes.FirstOrDefault(x => x.name == "data-onclick");
                    var token = a.value;
                    //a.value = null;

                    xbutton.removeAttribute("data-onclick");

                    return token;
                };


            new IHTMLButton { "invoke" }.AttachToDocument().WhenClicked(
                async delegate
                {
                    IHTMLButton xbutton = await service.FooButton;

                    var token = GetToken(xbutton);


                    Func<IHTMLButton, Task> FooButton_onclick =
                         button =>
                         {
                             // dynamic data
                             //button.data.

                             Action<string> set_innerText = x => button.innerText = x;

                             return service.InternalWebServiceInvokeAsync(
                                 token: token,
                                 set_innerText: set_innerText
                             );
                         };

                    xbutton.AttachToDocument().WhenClicked(FooButton_onclick);
                }
            );
        }

    }
}
