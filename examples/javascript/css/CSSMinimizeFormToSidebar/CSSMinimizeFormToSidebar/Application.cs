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
using System.Windows.Forms;
using CSSMinimizeFormToSidebar.Design;
using CSSMinimizeFormToSidebar.HTML.Pages;
using CSSMinimizeFormToSidebar.Library;
using ScriptCoreLib.JavaScript.Windows.Forms;

namespace ScriptCoreLib.Extensions
{
    public static class CSSMinimizeFormToSidebarExtensions
    {

    }
}

namespace CSSMinimizeFormToSidebar
{
    public static class ApplicationExtension
    {
        // advertise in ScriptCoreLib.Extensions
        public static global::CSSMinimizeFormToSidebar.HTML.Pages.IApp InitializeSidebarBehaviour(
            Form f,
            bool HandleClosed = true,
            bool HandleDragToLeft = true)
        {

            // can we do a dynamic upgrade?

            var newlayout = new global::CSSMinimizeFormToSidebar.HTML.Pages.App();

            newlayout.AddMoreText.Hide();

            var newlayoutnodes = newlayout.body.childNodes.ToArray();

            //newlayout.Sidebar.name = "Sidebar";
            // where is this guy??
            //newlayout.SidebarOverlay.name = "SidebarOverlay";

            var oldcontent = Native.document.body.childNodes.ToArray();

            Native.Document.body.Clear();
            Native.Document.body.appendChild(newlayoutnodes);

            Native.Document.body.setAttribute("style",
                newlayout.Container.getAttribute("style")
            );


            var mycontainer = new IHTMLDiv().AttachTo((IHTMLElement)newlayout.ScrollContainer.parentNode);
            newlayout.ScrollContainer.Orphanize();


            mycontainer.style.position = IStyle.PositionEnum.absolute;
            mycontainer.style.left = "10em";
            mycontainer.style.top = "0px";
            mycontainer.style.right = "0px";
            mycontainer.style.bottom = "0px";

            mycontainer.appendChild(oldcontent);


            //Native.Document.body.Orphanize();
            //newlayout.Container.AttachTo(Native.Document.documentElement);
            //Native.Document.body = (IHTMLBody)(object)newlayout.Container;


            // reparent
            f.GetHTMLTarget().Orphanize().AttachToDocument();

            global::CSSMinimizeFormToSidebar.ApplicationExtension.InitializeSidebarBehaviour(
                newlayout, f, HandleClosed: HandleClosed, HandleDragToLeft: HandleDragToLeft
            );

            return newlayout;
        }

        
        // problems with roslyn build?
        public static void InitializeSidebarBehaviour(
            IApp page, 
            Form f, 
            bool HandleClosed = true,
            bool HandleDragToLeft = true
            )
        {
            var tt = f.GetHTMLTarget();

            //page.SidebarInfo.style.tr
            page.SidebarInfo.style.With(
                (dynamic s) => s.webkitTransition = "all 0.3s linear"
            );


            var IsMinimized = false;


            #region Minimize
            Action Minimize =
                delegate
                {
                    if (IsMinimized)
                        return;


                    var t = tt;
                    dynamic style = t.style;


                    var old = new { f.Left, f.Top, f.Height };

                    // http://developer.apple.com/library/safari/documentation/AudioVideo/Reference/WebKitTransitionEventClassReference/WebKitTransitionEvent/WebKitTransitionEvent.html

                    var cleartransition = new ScriptCoreLib.JavaScript.Runtime.Timer(
                        delegate
                        {
                            style.webkitTransition = "";
                            style = null;
                            IsMinimized = false;

                            f.MoveTo(
                                old.Left.Max(page.Sidebar.clientWidth + 12)
                                , old.Top);

                            // prevent drawing artifacts
                            tt.style.transform = "";

                        }
                    );

                    Action DoRestore = null;

                    DoRestore = delegate
                    {
                        if (t == null)
                            return;
                        DoRestore = null;

                        Console.WriteLine("DoRestore");

                        style.webkitTransition = "all 0.3s linear";

                        t.style.transform = "scale(1)";

                        t.style.left = old.Left.Max(page.Sidebar.clientWidth + 12) + "px";
                        t.style.top = old.Top + "px";

                        //t.style.Opacity = 1;
                        f.Opacity = 1;
                        t = null;
                        page.SidebarInfo.style.marginTop = (0) + "px";
                    };



                    var clicktorestore = new ScriptCoreLib.JavaScript.Runtime.Timer(
                        delegate
                        {
                            style.webkitTransition = "";

                            page.SidebarOverlay.onclick +=
                                delegate
                                {

                                    if (DoRestore != null)
                                        DoRestore();

                                };

                            f.Resize +=
                                delegate
                                {
                                    if (DoRestore != null)
                                        DoRestore();
                                };
                        }
                    );

                    t.addEventListener("webkitTransitionEnd",
                           ee =>
                           {
                               if (t == null)
                               {
                                   // script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, System.Linq.Expressions.ExpressionType, System.Type, System.Collections.Generic.IEnumerable`1[[Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]])]

                                   if ((object)style != null)
                                       cleartransition.StartTimeout(300);

                                   return;
                               }

                               Console.WriteLine("webkitTransitionEnd");

                               clicktorestore.Stop();
                               clicktorestore.StartTimeout(300);
                           }
                       );

                    // http://www.the-art-of-web.com/css/css-animation/
                    t.style.transformOrigin = "0% 0%";

                    var scale = (double)(page.Sidebar.clientWidth - 10) / (double)f.Width;
                    page.SidebarInfo.style.marginTop = (f.Height * scale + 20) + "px";


                    style.webkitTransition = "all 0.2s linear";

                    t.style.transform = "scale(" + scale + ")";
                    t.style.left = "0.2em";
                    t.style.top = "0.2em";
                    //t.style.Opacity = 0.5;

                    f.Opacity = 0.5;

                    IsMinimized = true;

                };
            #endregion

            if (HandleDragToLeft)
                new ScriptCoreLib.JavaScript.Runtime.Timer(
                    delegate
                    {
                        // popup mode?
                        if (f.GetHTMLTarget().parentNode == null)
                            return;



                        if (IsMinimized)
                        {
                            return;
                        }


                        if (tt.offsetLeft < page.Sidebar.clientWidth)
                        {
                            page.SidebarOverlay.style.Opacity = 0.2;

                            var scale = (double)(page.Sidebar.clientWidth - 10) / ((double)tt.clientHeight);
                            Console.WriteLine(new { scale });
                            page.SidebarInfo.style.marginTop = (f.Height * scale + 20) + "px";


                            if (f.Capture)
                                return;

                            Console.WriteLine(new { f.WindowState });

                            if (f.WindowState != FormWindowState.Normal)
                                return;


                            Minimize();

                        }
                        else
                        {
                            page.SidebarInfo.style.marginTop = (0) + "px";
                        }
                        page.SidebarOverlay.style.Opacity = 0;
                    }
                ).StartInterval(100);


            if (HandleClosed)
            {
                f.FormClosing += (s, e) =>
                {
                    if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing)
                    {
                        e.Cancel = true;

                        Minimize();
                    }
                };

            }


            dynamic xstyle = page.SidebarOverlay.style;
            xstyle.webkitTransition = "all 0.3s linear";
            page.SidebarOverlay.onmouseout +=
                delegate
                {
                    page.SidebarOverlay.style.Opacity = 0;

                };
            page.SidebarOverlay.onmouseover +=
             delegate
             {
                 page.SidebarOverlay.style.Opacity = 0.3;
             };

            page.SidebarOverlay.onclick +=
                    delegate
                    {
                        if (IsMinimized)
                            return;
                        Minimize();



                    };


            #region enforce overflow:hidden
            Native.window.onscroll +=
                e =>
                {
                    e.PreventDefault();
                    e.stopPropagation();

                    //Console.WriteLine("Window onscroll ");

                    Native.Document.body.scrollTop = 0;
                    Native.Document.body.scrollLeft = 0;
                };
            #endregion

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
            FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;

            var f = new Form1();

            f.Show();

            f.webBrowser1.Navigate("http://discover.xavalon.net");


            ApplicationExtension.InitializeSidebarBehaviour(page, f);


            #region AddMoreText
            Action AddMoreText = delegate
            {
                new Lorem().Container.AttachTo(page.Content).With(
                    (es) =>
                    {
                        var s = es.style;

                        s.color = "yellow";

                        dynamic ss = s;

                        ss.webkitTransition = "all 3s linear";

                        Native.window.requestAnimationFrame +=
                            delegate
                            {
                                s.color = "";
                            };

                        page.ScrollContainer.ScrollToBottom();
                    }
                );

            };
            #endregion


            page.Content.Clear();
            AddMoreText();

            page.AddMoreText.onclick +=
                delegate
                {
                    AddMoreText();
                };
        }


    }
}
