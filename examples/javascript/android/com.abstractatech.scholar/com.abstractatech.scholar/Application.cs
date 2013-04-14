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
using com.abstractatech.scholar.Design;
using com.abstractatech.scholar.HTML.Pages;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System.Diagnostics;

namespace com.abstractatech.scholar
{
    public static class XXX
    {
        public static void MoveNodeToFirst(this INode e)
        {
            var p = e.parentNode;
            e.Orphanize();

            p.insertBefore(
                e, p.firstChild);

        }


        public static object scrollTo_sync = new object();

        public static void scrollTo(this IWindow w, int x, int y, TimeSpan time)
        {
            var sync = new object();
            scrollTo_sync = sync;

            var s = new Stopwatch();

            s.Start();

            var start = new { Native.Document.body.scrollLeft, Native.Document.body.scrollTop };
            var check = start;


            Action loop = null;

            loop = delegate
            {
                if (sync != scrollTo_sync)
                {
                    return;
                }

                // scrollTo { check = { scrollLeft = 106, scrollTop = 0 }, current = { scrollLeft = 0, scrollTop = 0 } }


                var a = 1.0;

                if (s.ElapsedMilliseconds < time.TotalMilliseconds)
                {
                    a = s.ElapsedMilliseconds / time.TotalMilliseconds;

                    Native.Window.requestAnimationFrame += loop;

                }

                var scrollLeft = (int)Math.Ceiling(start.scrollLeft + (double)(x - start.scrollLeft) * a);

                // this was expen
                var scrollTop = (int)Math.Ceiling(start.scrollTop + (double)(y - start.scrollTop) * a);

                //Console.WriteLine(new { a, x, xx = scrollLeft, s.Elapsed });

                w.scrollTo(scrollLeft, scrollTop);

                // scrollTo { check = { scrollLeft = 0, scrollTop = 0 }, current = { scrollLeft = 77, scrollTop = 0 } }



            };

            Native.Window.requestAnimationFrame += loop;
        }
    }

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
        public Application(IApp page)
        {

            FormStyler.AtFormCreated = FormStylerLikeFloat.LikeFloat;

            #region  I want animated background!

            WebGLClouds.Application.Loaded +=
                a =>
                {
                    Native.Document.body.parentNode.insertBefore(
                         a.container.Orphanize(),
                          Native.Document.body
                    );
                    a.container.style.position = IStyle.PositionEnum.@fixed;

                };


            new WebGLClouds.Application();
            #endregion

            //var minsize = new IHTMLDiv().AttachToDocument();

            //minsize.style.SetSize(4000, 2000);




            var f = new Form
            {
                Text = "My Files",
                StartPosition = FormStartPosition.Manual,
                SizeGripStyle = SizeGripStyle.Hide
            };

            #region w
            var ff = new Form
            {
                StartPosition = FormStartPosition.Manual,
                SizeGripStyle = SizeGripStyle.Hide

            };

            var w = new WebBrowser
            {
                Dock = DockStyle.Fill
            }.AttachTo(ff);
            w.GetHTMLTarget().name = "view";

            w.Navigating +=
                delegate
                {
                    ff.Text = "Navigating";


                    if (Native.Window.Width < 1024)
                        // docked?
                        if (ff.GetHTMLTarget().parentNode != null)
                            Native.Window.scrollTo(ff.Left - 8, ff.Top - 8, TimeSpan.FromMilliseconds(300));

                };



            w.Navigated +=
                delegate
                {
                    if (w.Url.ToString() == "about:blank")
                    {

                        Native.Window.scrollTo(0, 0, TimeSpan.FromMilliseconds(200));

                        ff.Text = "...";

                        "Web Files".ToDocumentTitle();

                        return;
                    }

                    //ff.Text = w.DocumentTitle;
                    ff.Text = Native.Window.unescape(
                        w.Url.ToString().SkipUntilLastIfAny("/").TakeUntilLastIfAny(".")
                        );

                    ff.Text.ToDocumentTitle();


                };

            ff.FormClosing +=
                (sender, e) =>
                {
                    Console.WriteLine(new { e.CloseReason });

                    if (e.CloseReason == CloseReason.UserClosing)
                    {
                        e.Cancel = true;

                        w.Navigate("about:blank");
                    }

                };
            #endregion



            var content = f.GetHTMLTargetContainer();




            var hh = new HorizontalSplit
            {
                Minimum = 0.05,
                Maximum = 0.95,
                Value = 0.4,
            };

            hh.Container.AttachToDocument();
            hh.Container.style.position = IStyle.PositionEnum.absolute;
            hh.Container.style.left = "0px";
            hh.Container.style.top = "0px";
            hh.Container.style.right = "0px";
            hh.Container.style.bottom = "0px";

            hh.Split.Splitter.style.backgroundColor = "rgba(0,0,0,0.0)";


            #region AtResize
            Action AtResize = delegate
           {
               Native.Document.getElementById("feedlyMiniIcon").Orphanize();

               Native.Document.body.style.minWidth = "";

               if (ff.GetHTMLTarget().parentNode == null)
               {
                   Native.Window.scrollTo(0, 0);
                   f.MoveTo(8, 8).SizeTo(Native.Window.Width - 16, Native.Window.Height - 16);

                   return;
               }

               if (f.GetHTMLTarget().parentNode == null)
               {
                   Native.Window.scrollTo(0, 0);
                   ff.MoveTo(8, 8).SizeTo(Native.Window.Width - 16, Native.Window.Height - 16);

                   return;
               }

               if (Native.Window.Width < 1024)
               {
                   Native.Document.body.style.minWidth = (Native.Window.Width * 2) + "px";


                   f.MoveTo(8, 8).SizeTo(Native.Window.Width - 16, Native.Window.Height - 16);

                   ff.MoveTo(Native.Window.Width + 8, 8).SizeTo(Native.Window.Width - 16, Native.Window.Height - 16);

                   // already scrolled...
                   if (w.Url.ToString() != "about:blank")
                       // docked?
                       if (ff.GetHTMLTarget().parentNode != null)
                           Native.Window.scrollTo(ff.Left - 8, ff.Top - 8);

                   return;
               }




               f.MoveTo(16, 64).SizeTo(hh.LeftContainer.clientWidth - 32, Native.Window.Height - 128);


               ff.MoveTo(
                   Native.Window.Width - hh.RightContainer.clientWidth + 16

                   , 64).SizeTo(hh.RightContainer.clientWidth - 32, Native.Window.Height - 128);

               //Console.WriteLine("LeftContainer " + new { hh.LeftContainer.clientWidth });
               //Console.WriteLine("RightContainer " + new { hh.RightContainer.clientWidth });
           };

            hh.ValueChanged +=
          delegate
          {
              AtResize();
          };

            Native.Window.onresize +=
             delegate
             {
                 AtResize();
             };

            Native.Window.requestAnimationFrame +=
        delegate
        {
            AtResize();
        };
            #endregion


            //hh.Split.LeftScrollable = new IHTMLDiv { className = "SidebarForButtons" };


            ff.Show();
            f.Show();

            f.PopupInsteadOfClosing(SpecialNoMovement: true, NotifyDocked: AtResize);
            ff.PopupInsteadOfClosing(SpecialNoMovement: true, HandleFormClosing: false, NotifyDocked: AtResize);


            var layout = new Abstractatech.JavaScript.FileStorage.HTML.Pages.App();

            layout.Container.AttachTo(content);

            Abstractatech.JavaScript.FileStorage.ApplicationContent.Target = "view";


            new Abstractatech.JavaScript.FileStorage.ApplicationContent(
                layout,
                service
            );







            "Web Files".ToDocumentTitle();
        }

    }
}
