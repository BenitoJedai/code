using MSVSFormStyle.Design;
using MSVSFormStyle.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MSVSFormStyle
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            FormStyler.AtFormCreated =
                s =>
                {
                    if (content.checkBox1.Checked)
                        return;

                    s.TargetOuterBorder.style.boxShadow = "rgba(255, 122, 204, 0.3) 0px 0px 6px 3px";
                    s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(255, 122, 204);

                    s.TargetInnerBorder.style.borderWidth = "0px";

                    s.CloseButton.style.color = JSColor.White;
                    s.CloseButton.style.backgroundColor = JSColor.None;
                    s.CloseButton.style.borderWidth = "0px";
                    s.CloseButtonContent.style.borderWidth = "0px";

                    s.TargetResizerPadding.style.left = "0px";
                    s.TargetResizerPadding.style.top = "0px";
                    s.TargetResizerPadding.style.right = "0px";
                    s.TargetResizerPadding.style.bottom = "0px";

                    s.Caption.style.backgroundColor = JSColor.FromRGB(255, 122, 204);
                };

            content.AttachControlTo(page.Content);
            content.AutoSizeControlTo(page.ContentSize);
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
