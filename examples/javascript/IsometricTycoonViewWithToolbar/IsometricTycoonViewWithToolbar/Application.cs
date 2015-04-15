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
using System.Xml.Linq;
using IsometricTycoonViewWithToolbar.Design;
using IsometricTycoonViewWithToolbar.HTML.Pages;
using IsometricTycoonViewWithToolbar.HTML.Audio.FromAssets;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;

namespace IsometricTycoonViewWithToolbar
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        //public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {


            // Unhandled Exception: System.InvalidOperationException: Method: get_InternalSiblingsIncludingThis, 
            // Type: ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__Control; emmiting failed : System.Exception: recursion detected at stack 32



            #region AtFormCreated
            FormStyler.AtFormCreated =
                 s =>
                 {


                     // now shadow
                     s.TargetOuterBorder.style.boxShadow = "";
                     //s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(0, 122, 0);
                     //toolbar_color = Color.FromRGB(0, 0x80, 0)
                     Toolbar.JavaScript.Extensions.SetDialogColor(
                        s.TargetOuterBorder,
                        ScriptCoreLib.Shared.Drawing.Color.FromRGB(0, 0x80, 0),
                        true
                     );



                     s.TargetInnerBorder.style.borderWidth = "0px";
                     s.TargetInnerBorder.style.backgroundColor = JSColor.None;


                     s.CloseButton.style.color = JSColor.FromRGB(0, 80, 0);
                     s.CloseButton.style.backgroundColor = JSColor.None;
                     s.CloseButton.style.borderWidth = "0px";
                     s.CloseButtonContent.style.borderWidth = "0px";

                     s.TargetResizerPadding.style.left = "0px";
                     s.TargetResizerPadding.style.top = "0px";
                     s.TargetResizerPadding.style.right = "0px";
                     s.TargetResizerPadding.style.bottom = "0px";

                     // browser popup will use this color
                     ((__Form)s.Context).HTMLTargetContainerRef.style.backgroundColor = JSColor.FromRGB(0, 0x80, 0);

                     s.Caption.style.backgroundColor = JSColor.None;


                     //FormStyler.LikeVisualStudioMetro(s);
                 };
            #endregion




            #region TheServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {

                FormStyler.AtFormCreated =
                  s =>
                  {
                      s.Context.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                      //var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDrag().AttachTo(s.Context.GetHTMLTarget());
                      var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDragWithShadow().AttachTo(s.Context.GetHTMLTarget());
                  };

                chrome.Notification.DefaultTitle = "IsometricTycoonViewWithToolbar";
                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    DefaultSource.Text,
                    AtFormCreated: FormStyler.AtFormCreated
                );


                return;
            }
            #endregion


            //global::DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();


            new gong().AttachToDocument().play();
            new ThreeDStuff.js.Tycoon4();


        }

    }
}
