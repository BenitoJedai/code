using com.abstractatech.wiki.Design;
using com.abstractatech.wiki.HTML.Images.FromAssets;
using com.abstractatech.wiki.HTML.Pages;
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
//using AvalonPromotionBrandIntro;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.Runtime;

namespace com.abstractatech.wiki
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
            //#region introOnce
            //var introOnce = new Cookie("intro");

            //if (!introOnce.BooleanValue)
            //{
            //    var canvas = new ApplicationCanvas();

            //    canvas.TriggerOnClick = false;
            //    canvas.Background = Brushes.Transparent;

            //    canvas.AnimationAllWhite +=
            //        delegate
            //        {
            //            Native.Document.body.style.backgroundColor = JSColor.None;
            //        };

            //    canvas.AnimationCompleted +=
            //        delegate
            //        {
            //            ScriptCoreLib.JavaScript.Extensions.AvalonExtensions.ToHTMLElement(
            //                canvas
            //            ).Orphanize();

            //            introOnce.BooleanValue = true;
            //        };

            //    canvas.AttachToContainer(Native.Document.body);

            //    canvas.AutoSizeTo(Native.Document.body);
            //}
            //else
            //{
            Native.Document.body.style.backgroundColor = JSColor.None;
            //}
            //#endregion

            //canvas.AutoSizeTo(page.ContentSize);

            page.HiddenContent.ToggleVisible();
            //page.SubHeader.innerText = Native.Window.unescape(Native.Document.location.pathname);


            #region AtEdit
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
                            var xml = Editor.InnerHTML;

                            // android seems to use this
                            xml = xml.Replace("&nbsp;", " ");

                            SaveChanges.Button.disabled = true;
                            service.SaveChanges(
                                 Native.window.unescape(Native.Document.location.pathname),
                                XElement.Parse("<div>" + xml + "</div>"),
                                delegate
                                {
                                    // refresh
                                    // does not work for hash tags
                                    //Native.Document.location = Native.Document.location;

                                    Native.Document.location.replace(
                                        Native.window.unescape(Native.Document.location.pathname)
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
            #endregion

            #region Edit.onclick
            page.Edit.onclick +=
                delegate
                {
                    Native.window.history.pushState(
                       data: "",
                       title: "edit",
                       url: "#edit"
                   );

                    var revert = AtEdit();

                    Native.window.window.onpopstate +=
                     e =>
                     {
                         if (revert != null)
                         {
                             revert();
                         }
                         revert = null;
                     };

                };
            #endregion

            #region onhashchange
            Native.window.onhashchange +=
                e =>
                {
                    Console.WriteLine(new { onhashchange = new { e.newURL, e.oldURL } });

                    if (e.newURL.EndsWith("#edit"))
                    {
                        AtEdit();
                    }

                };
            #endregion

            if (Native.Document.location.hash == "#edit")
                AtEdit();

            //page.Fullscreen.onclick +=
            //    delegate
            //    {
            //        Native.Document.body.requestFullscreen();
            //    };

            var UpdatedRevisionNumber = page.RevisionNumber.innerText;

            new Timer(
                delegate
                {
                    service.CountItems(
                        Native.window.unescape(Native.Document.location.pathname),
                        RevisionNumber =>
                        {
                            UpdatedRevisionNumber = "Revision " + RevisionNumber;

                        }
                    );
                }
            ).StartInterval(5000);

            new Timer(
                tt =>
                {
                    if (page.RevisionNumber.innerText != UpdatedRevisionNumber)
                    {
                        if (tt.Counter % 2 == 0)
                        {
                            page.RevisionNumber.style.color = JSColor.Red;
                            return;
                        }
                    }
                    page.RevisionNumber.style.color = JSColor.Gray;
                }
            ).StartInterval(300);

            page.Header.style.cursor = IStyle.CursorEnum.pointer;
            page.Header.style.textDecoration = "underline";
            page.Header.onclick +=
                delegate
                {
                    Native.Document.location.replace(
                                      Native.window.unescape(Native.Document.location.pathname)
                                  );
                };

            page.RevisionNumber.style.cursor = IStyle.CursorEnum.pointer;
            page.RevisionNumber.style.textDecoration = "underline";
            page.RevisionNumber.onclick +=
                delegate
                {
                    Native.Document.location.replace(
                                      Native.window.unescape(Native.Document.location.pathname)
                                  );
                };



            @"Wiki".ToDocumentTitle();
        }

    }
}
