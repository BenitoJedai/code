using FormsPictureBox;
using FormsPictureBox.Design;
using FormsPictureBox.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FormsPictureBox
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static Application()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131217-picturebox
            ScriptCoreLib.Shared.BCLImplementation.System.Resources.__ResourceManager.InternalGetObject =
                (string baseName, Assembly assembly, string name) =>
                {
                    var x = default(object);

                    // this is what jsc should we for us
                    if (name == "debitcard")
                    {
                        x = new ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing.__Bitmap
                        {
                            InternalImage = new IHTMLImage("assets/FormsPictureBox/debitcard.jpg")
                        };
                    }

                    return x;
                };
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {


            var content = new ApplicationControl();

            content.AttachControlToDocument();
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            this.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
