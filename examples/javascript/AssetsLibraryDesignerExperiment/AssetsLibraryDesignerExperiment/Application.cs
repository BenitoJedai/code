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
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AssetsLibraryDesignerExperiment.Design;
using AssetsLibraryDesignerExperiment.HTML.Pages;
using System.ComponentModel;
using AssetsLibraryDesignerExperiment.Components;

namespace AssetsLibraryDesignerExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed partial class Application : Component
    {
        // inspired by http://codepen.io/FWeinb/pen/BeJLo

        public readonly ApplicationWebService service = new ApplicationWebService();



        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp app = null)
            : this()
        {
            this.button1.AttachControlTo(Native.Document.body);

            new Form1().Show();

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("hey");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            applicationWebService1.WebMethod2("foo",
                y =>
                {

                }
            );
        }




    }
}
