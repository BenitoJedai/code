using TestTextBoxPadding;
using TestTextBoxPadding.Design;
using TestTextBoxPadding.HTML.Pages;
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

namespace TestTextBoxPadding
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
            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.TextBox.set_CharacterCasing(System.Windows.Forms.CharacterCasing)]

            content.AttachControlToDocument();

            content.trackBar1.ValueChanged +=
                delegate
                {
                    IStyleSheet.all[
                        typeof(TextBox)
                    ][IHTMLElement.HTMLElementEnum.input].style.paddingLeft = content.trackBar1.Value + "px";
                };

        }

    }
}
