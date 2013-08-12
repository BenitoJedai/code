using PHPWiki.Design;
using PHPWiki.HTML.Images.FromAssets;
using PHPWiki.HTML.Pages;
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
using System.Xml.Linq;

namespace PHPWiki
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
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault  page)
        {
            Func<Action> AtEdit =
                delegate
                {
                    page.Edit.ToggleVisible();

                    var Control = new IHTMLDiv();

                    Control.AttachToDocument();

                    var Content = page.Content.innerHTML;

                    page.Content.Clear();

                    var Editor = new TextEditor(Control);

                    Editor.InnerHTML = Content;

                    Editor.IsFadeEnabled = false;


                    TextEditor.ToolbarButton SaveChanges = null;

                    SaveChanges = Editor.AddButton(
                        new RTA_save(),
                        "Save Changes And Refresh",
                        delegate
                        {
                            SaveChanges.Button.disabled = true;
                            service.SaveChanges(
                                Native.Document.location.pathname,
                                XElement.Parse("<div>" + Editor.InnerHTML + "</div>"),
                                delegate
                                {
                                    // refresh
                                    // does not work for hash tags
                                    //Native.Document.location = Native.Document.location;

                                    Native.Document.location.replace(
                                        Native.Document.location.ToString().TakeUntilIfAny("#")
                                    );
                                }
                            );
                        }
                    );


                    Editor.BottomToolbarContainer.Add(
                        SaveChanges.Control
                    );

                    return delegate
                    {
                        Editor.Control.Orphanize();
                        page.Content.innerHTML = Content;
                        page.Edit.ToggleVisible();
                    };
                };

            page.Edit.onclick +=
                delegate
                {
                    Native.Window.history.pushState(
                       data: "",
                       title: "edit",
                       url: "#edit"
                   );

                    var revert = AtEdit();

                    Native.Window.window.onpopstate +=
                     e =>
                     {
                         if (revert != null)
                         {
                             revert();
                         }
                         revert = null;
                     };

                };

            if (Native.Document.location.hash == "#edit")
                AtEdit();

            //page.Fullscreen.onclick +=
            //    delegate
            //    {
            //        Native.Document.body.requestFullscreen();
            //    };

         
        }

    }
}
