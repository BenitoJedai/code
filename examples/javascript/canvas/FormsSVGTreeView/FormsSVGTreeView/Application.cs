using FormsSVGTreeView;
using FormsSVGTreeView.Design;
using FormsSVGTreeView.HTML.Pages;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

//using ScriptCoreLib.JavaScript.Windows.Forms.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace FormsSVGTreeView
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Native.body.style.margin = "0px";

            //content.
            // ? AttachToControl
            new IHTMLContent { }.AttachTo(
                content.splitContainer1.Panel2.GetHTMLTargetContainer()
            );

            //content.AttachControlToDocument();
            //content.AttachControlTo(
            //    Native.shadow.firstChild
            //    );

            content.GetHTMLTarget().AttachTo(Native.shadow);
            content.Visible = true;



            content.BackColor = Color.Transparent;

            // will this do the trick?
            content.Dock = DockStyle.Fill;
            //Native.shadow.



            // can we get some svg magic?


            //var canvas = (IHTMLCanvas)content.treeView1.GetHTMLTarget();

            // need to move the operator?
            var canvas = (IHTMLCanvas)(IHTMLDiv)content.treeView1.GetHTMLTarget();

            canvas.AttachToDocument();


        }

    }
}
