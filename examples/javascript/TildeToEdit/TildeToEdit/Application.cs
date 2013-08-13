using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using TildeToEdit;
using TildeToEdit.Design;
using TildeToEdit.HTML.Pages;
using Abstractatech.JavaScript.FormAsPopup;

namespace TildeToEdit
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="document">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp document)
        {
            // 
            //document.body.AsXElement().Elements("script").Remove();
            document.body.AsXElement().Elements("script").WithEach(k => k.Remove());

            var f = new Form { Text = "Visual Editor" };

            f.PopupInsteadOfClosing(HandleFormClosing: true);

            f.Width = 600;

            var diagnostics = new IHTMLDiv().AttachTo(document.body.parentNode);
            //var diagnostics = new IHTMLBody().AttachTo(Native.document.body.parentNode);

            diagnostics.style.backgroundColor = "rgba(0, 0, 0, 0)";
            diagnostics.style.position = IStyle.PositionEnum.absolute;
            diagnostics.style.overflow = IStyle.OverflowEnum.hidden;

            diagnostics.style.left = "0px";
            diagnostics.style.top = "-100%";
            diagnostics.style.width = "100%";
            diagnostics.style.height = "100%";



            f.Show();
            f.GetHTMLTarget().AttachTo(diagnostics);

            //Uncaught TypeError: Cannot call method 'write' of null 
            var editor = new TextEditor(f.GetHTMLTargetContainer());

            var snd = new HTML.Audio.FromAssets.SAMPLES036();
            snd.load();

            var snd2 = new HTML.Audio.FromAssets.Hammertime();
            snd2.load();


            Action reverse = delegate { };

            Action Hide =
                delegate
                {
                    //
                    (document.body.style as dynamic).webkitFilter = "";

                    diagnostics.style.top = "-100%";
                    diagnostics.style.backgroundColor = "rgba(0, 0, 0, 0)";

                    snd2.play();
                    snd2 = new HTML.Audio.FromAssets.Hammertime();
                    snd2.load();

                    reverse();
                };

            Action Show =
                delegate
                {
                    if (diagnostics.style.top != "-100%")
                        return;

                    // { -webkit-filter: grayscale(0.5) blur(10px);
                    (document.body.style as dynamic).webkitFilter = "grayscale(0.5) blur(2px)";

                    diagnostics.style.top = "0%";
                    diagnostics.style.backgroundColor = "rgba(0, 0, 0, 0.5)";


                    snd.play();
                    snd = new HTML.Audio.FromAssets.SAMPLES036();
                    snd.load();


                    // using undo context? save load and store ops to revert them
                    editor.InnerHTML = document.body.innerHTML;

                    reverse = delegate
                    {
                        document.body.innerHTML = editor.InnerHTML;

                        reverse = delegate { };
                    };
                };



            Hide();

            // http://www.w3schools.com/css3/css3_transitions.asp
            diagnostics.style.With(
                       (dynamic s) => s.webkitTransition = "all 0.2s ease-in-out"
                 );
            diagnostics.style.With(
              (dynamic s) => s.transition = "all 0.2s ease-in-out"
            );




            Action Toggle =
                delegate
                {
                    if (diagnostics.style.top != "-100%")
                    {
                        Hide();

                    }
                    else
                    {
                        Show();

                    }
                };

            diagnostics.onclick +=
                e =>
                {
                    if (e.Element == diagnostics)
                        Hide();

                };

            Action<IEvent> AtKeyCode =
               e =>
               {
                   var KeyCode = e.KeyCode;

                   new { KeyCode }.ToString().ToDocumentTitle();

                   if (KeyCode == 27)
                   {
                       e.preventDefault();
                       e.stopPropagation();

                       Hide();

                   }


                   // e
                   if (KeyCode == 69)
                   {
                       e.preventDefault();
                       e.stopPropagation();

                       Show();

                   }

                   // US
                   if (KeyCode == 222)
                   {
                       e.preventDefault();
                       e.stopPropagation();


                       Toggle();
                   }
                   // EE
                   if (KeyCode == 192)
                   {
                       e.preventDefault();
                       e.stopPropagation();


                       Toggle();
                   }
               };

            // rosyln could pick up change requests from comments
            // should not see this event for popup action
            //f.FormClosing +=
            //    (s, e) =>
            //    {
            //        e.Cancel = true;

            //        //Hide();
            //    };

            Native.document.onkeyup +=
                e =>
                {
                    AtKeyCode(e);
                };

            // what if it is reloaded? popup
            editor.Document.WhenContentReady(
                delegate
                {
                    editor.Document.onkeyup += e => AtKeyCode(e);
                }
            );


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
