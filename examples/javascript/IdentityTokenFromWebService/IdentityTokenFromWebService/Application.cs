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
using IdentityTokenFromWebService;
using IdentityTokenFromWebService.Design;
using IdentityTokenFromWebService.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace IdentityTokenFromWebService
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
            //            { key = IdentityToken, value = 1553509128 }
            //{ key = foo, value = bar }

            new IHTMLButton { innerText = "Get IdentityToken" }.AttachToDocument().WhenClicked(
                async delegate
                {
                    var PreviousIdentityToken = this.IdentityToken;

                    // next frame
                    //var xx = await this;

                    var x = await yield();

                    new
                    {
                        this.IdentityToken,
                        x = new { x.IdentityToken, x.ContextIdentityToken },
                        PreviousIdentityToken
                    }.ToString().ToDocumentTitle();
                }
            );

            new { this.IdentityToken }.ToString().ToDocumentTitle();
        }

    }
}
