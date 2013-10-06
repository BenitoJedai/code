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
using System.Threading.Tasks;
using System.Xml.Linq;
using WebServiceWithStringFields;
using WebServiceWithStringFields.Design;
using WebServiceWithStringFields.HTML.Pages;

namespace WebServiceWithStringFields
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

            this.With(
                async delegate
                {
                    base.Text = "look, a field!";
                    base.MakeYellow = delegate
                    {
                        Native.document.body.style.backgroundColor = "yellow";
                    };

                    var z = await base["look, an index!"];


                    new IHTMLPre { innerText = new { base.Text, z }.ToString() }.AttachToDocument();


                }
            );
        }

    }
}
