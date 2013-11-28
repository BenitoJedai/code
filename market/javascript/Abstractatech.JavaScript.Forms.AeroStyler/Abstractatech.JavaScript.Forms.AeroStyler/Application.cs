using Abstractatech.JavaScript.Forms.AeroStyler.Design;
using Abstractatech.JavaScript.Forms.AeroStyler.HTML.Images.FromAssets;
using Abstractatech.JavaScript.Forms.AeroStyler.HTML.Pages;
using Abstractatech.JavaScript.Forms.AeroStyler.Library;
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
using System.Windows.Forms;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.Windows.Forms
{
    public static class FormStylerLikeAero
    {
        static s_bg bg = new s_bg();

        public static void LikeAero(FormStyler s)
        {
            // border-radius:25px;

            dynamic TargetOuterBorder_style = s.TargetOuterBorder.style;
            dynamic TargetInnerBorder_style = s.TargetInnerBorder.style;

            var TargetBackground = new IHTMLDiv();

            dynamic TargetBackground_style = TargetBackground.style;


            TargetBackground.style.position = IStyle.PositionEnum.absolute;
            TargetBackground.style.left = "0px";
            TargetBackground.style.top = "0px";
            TargetBackground.style.right = "0px";
            TargetBackground.style.bottom = "0px";
            TargetBackground.style.Opacity = 0.7;

            bg.ToBackground(TargetBackground.style);

            s.TargetOuterBorder.parentNode.insertBefore(TargetBackground, s.TargetOuterBorder.parentNode.firstChild);


            TargetOuterBorder_style.borderRadius = "8px";
            TargetInnerBorder_style.borderRadius = "8px";
            TargetBackground_style.borderRadius = "8px";

            s.TargetOuterBorder.style.boxShadow = "rgba(0, 0, 0, 0.8) 0px 0px 12px 0px";
            s.TargetOuterBorder.style.borderColor = "black";
            s.TargetOuterBorder.style.borderWidth = "1px";


            s.CaptionContent.style.textShadow = "0px 0px 16px white";

            s.TargetInnerBorder.style.borderWidth = "1px";
            s.TargetInnerBorder.style.borderBottomColor = "cyan";
            s.TargetInnerBorder.style.borderRightColor = "cyan";

            s.CloseButton.style.color = JSColor.White;
            s.CloseButton.style.backgroundColor = JSColor.None;
            s.CloseButton.style.borderWidth = "0px";
            s.CloseButtonContent.style.borderWidth = "0px";


            s.TargetResizerPadding.style.left = "4px";
            s.TargetResizerPadding.style.top = "4px";
            s.TargetResizerPadding.style.right = "4px";
            s.TargetResizerPadding.style.bottom = "4px";


            s.TargetInnerBorder.style.backgroundColor = "";

            s.Context.LocationChanged +=
                delegate
                {
                    if (s.Context.WindowState == FormWindowState.Normal)
                        s.ContentContainerPadding.style.backgroundColor = "";
                    else
                    {
                        var cc = s.Context.BackColor;

                        s.ContentContainerPadding.style.backgroundColor = cc.ToString();
                    }
                };
            s.ContentContainerPadding.style.backgroundColor = "";

            s.Caption.style.backgroundColor = "transparent";
        }
    }
}
namespace Abstractatech.JavaScript.Forms.AeroStyler
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
            content.AttachControlTo(page.Content);
            //content.AutoSizeControlTo(page.ContentSize);


            FormStyler.AtFormCreated = FormStylerLikeAero.LikeAero;

            new ExampleForm { Text = "Hello World" }.Show();
        }

    }
}
