using TestFlowDataGridPadding;
using TestFlowDataGridPadding.Design;
using TestFlowDataGridPadding.HTML.Pages;
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
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;

namespace TestFlowDataGridPadding
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
            content.AttachControlToDocument();

            content.trackBar1.ValueChanged +=
                delegate
                {

                    content.f.dataGridView1.GetHTMLTarget().style.marginLeft = -content.trackBar1.Value + "px";

                    __DataGridView gg = content.f.dataGridView1;

                    gg.__ContentTable.css
                        [IHTMLElement.HTMLElementEnum.tbody]
                        [IHTMLElement.HTMLElementEnum.tr]
                        .first.child
                        [IHTMLElement.HTMLElementEnum.div]
                        [IHTMLElement.HTMLElementEnum.span].style.paddingLeft = content.trackBar1.Value + "px";
                };
        }

    }
}
