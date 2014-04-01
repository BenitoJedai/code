using MSVSFormStyle.Design;
using MSVSFormStyle.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Lambda;
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
using Abstractatech.JavaScript.FormAsPopup;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using System.Windows.Forms;

namespace MSVSFormStyle
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

            content.button1.Click +=
                delegate
                {
                    FormStyler.AtFormCreated = FormStyler.LikeWindowsClassic;
                    new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
                };

            content.button2.Click +=
               delegate
               {
                   FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;
                   new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
               };

            content.button3.Click +=
                 delegate
                 {
                     FormStyler.AtFormCreated = FormStyler.LikeWindows3;
                     new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
                 };


            content.button4.Click +=
                 delegate
                 {
                     FormStyler.AtFormCreated = s =>
                        {


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

                     new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
                 };

            content.button8.Click +=
               delegate
               {
                   FormStyler.AtFormCreated = s =>
                   {
                       // X:\jsc.svn\examples\javascript\IsometricTycoonViewWithToolbar\IsometricTycoonViewWithToolbar\Application.cs
                       // X:\jsc.internal.svn\core\com.abstractatech.web\com.abstractatech.web\Domains\discover.xavalon.net\discover_xavalon_net.cs


                       s.TargetOuterBorder.style.boxShadow = "rgba(0, 122, 0, 0.3) 0px 0px 6px 3px";
                       s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(0, 122, 0);

                       s.TargetInnerBorder.style.borderWidth = "0px";

                       s.CloseButton.style.color = JSColor.White;
                       s.CloseButton.style.backgroundColor = JSColor.None;
                       s.CloseButton.style.borderWidth = "0px";
                       s.CloseButtonContent.style.borderWidth = "0px";

                       s.TargetResizerPadding.style.left = "0px";
                       s.TargetResizerPadding.style.top = "0px";
                       s.TargetResizerPadding.style.right = "0px";
                       s.TargetResizerPadding.style.bottom = "0px";

                       s.Caption.style.backgroundColor = JSColor.FromRGB(0, 122, 0);
                   };

                   new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
               };

            content.button9.Click +=
               delegate
               {
                   FormStyler.AtFormCreated = s =>
                   {
                       // X:\jsc.svn\examples\javascript\IsometricTycoonViewWithToolbar\IsometricTycoonViewWithToolbar\Application.cs
                       // X:\jsc.internal.svn\core\com.abstractatech.web\com.abstractatech.web\Domains\discover.xavalon.net\discover_xavalon_net.cs


                       s.Caption.style.backgroundColor = JSColor.FromRGB(154, 108, 70);
                       s.TargetOuterBorder.style.boxShadow = "rgba(154, 108, 70, 0.3) 0px 0px 6px 3px";
                       s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(154, 108, 70);

                       s.TargetInnerBorder.style.borderWidth = "0px";

                       s.CloseButton.style.color = JSColor.White;
                       s.CloseButton.style.backgroundColor = JSColor.None;
                       s.CloseButton.style.borderWidth = "0px";
                       s.CloseButtonContent.style.borderWidth = "0px";

                       s.TargetResizerPadding.style.left = "0px";
                       s.TargetResizerPadding.style.top = "0px";
                       s.TargetResizerPadding.style.right = "0px";
                       s.TargetResizerPadding.style.bottom = "0px";

                   };

                   new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
               };

            content.button6.Click +=
               delegate
               {
                   FormStyler.AtFormCreated = FormStyler.LikeWindows98;
                   new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
               };


            content.button7.Click +=
             delegate
             {
                 FormStyler.AtFormCreated = FormStylerLikeAero.LikeAero;

                 new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
             };


            content.button10.Click +=
                 delegate
                 {
                     FormStyler.AtFormCreated = s =>
                     {
                         // border> 8E9BBC
                         // caption 4D6082

                         s.TargetOuterBorder.style.boxShadow = "rgba(0, 0, 0, 0.2) 0px 0px 6px 0px";
                         s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(142, 155, 188);

                         s.TargetInnerBorder.style.borderWidth = "0px";

                         s.CloseButton.style.color = JSColor.FromRGB(206, 212, 221);
                         s.CloseButton.style.backgroundColor = JSColor.None;
                         s.CloseButton.style.borderWidth = "0px";
                         s.CloseButtonContent.style.borderWidth = "0px";

                         s.TargetResizerPadding.style.left = "0px";
                         s.TargetResizerPadding.style.top = "0px";
                         s.TargetResizerPadding.style.right = "0px";
                         s.TargetResizerPadding.style.bottom = "0px";

                         s.Caption.style.backgroundColor = JSColor.FromRGB(77, 96, 130);
                         s.CaptionShadow.style.backgroundColor = JSColor.FromRGB(77, 96, 130);
                     };

                     new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
                 };



            content.button11.Click +=
                 delegate
                 {
                     FormStyler.AtFormCreated = s =>
                     {
                         // border> 8E9BBC
                         // caption 41

                         s.TargetOuterBorder.style.boxShadow = "rgba(0, 0, 0, 0.2) 0px 0px 6px 0px";
                         s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(12, 32, 45);

                         s.TargetInnerBorder.style.borderWidth = "0px";

                         s.CloseButton.style.color = JSColor.White;
                         s.CloseButton.style.backgroundColor = JSColor.None;
                         s.CloseButton.style.borderWidth = "0px";
                         s.CloseButtonContent.style.borderWidth = "0px";

                         s.TargetResizerPadding.style.left = "0px";
                         s.TargetResizerPadding.style.top = "0px";
                         s.TargetResizerPadding.style.right = "0px";
                         s.TargetResizerPadding.style.bottom = "0px";

                         s.Caption.style.backgroundColor = JSColor.FromRGB(41, 57, 85);
                         s.CaptionShadow.style.backgroundColor = JSColor.FromRGB(41, 57, 85);
                     };

                     new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
                 };


            content.button12.Click +=
             delegate
             {
                 FormStyler.AtFormCreated = FormStylerLikeChrome.LikeChrome;

                 new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
             };


            content.AttachControlToDocument();

            //content.AttachControlTo(page.Content);
            //content.AutoSizeControlTo(page.ContentSize);

            @"Style".ToDocumentTitle();


            new IHTMLAnchor { "drag me" }.AttachTo(Native.document.documentElement).With(
                dragme =>
                {
                    dragme.style.position = IStyle.PositionEnum.@fixed;
                    dragme.style.left = "1em";
                    dragme.style.bottom = "1em";

                    dragme.style.zIndex = 1000;

                    dragme.AllowToDragAsApplicationPackage();
                }
            );

        }

    }
}
