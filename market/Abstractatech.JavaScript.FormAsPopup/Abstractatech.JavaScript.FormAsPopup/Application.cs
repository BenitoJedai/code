using Abstractatech.JavaScript.FormAsPopup.Design;
using Abstractatech.JavaScript.FormAsPopup.HTML.Pages;
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
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;


// X:\jsc.svn\examples\javascript\forms\FakeWindowsLoginExperiment\FakeWindowsLoginExperiment\Application.cs

//namespace Abstractatech.JavaScript.FormAsPopup
namespace Abstractatech.JavaScript.FormAsPopup
{
    static class X
    {
        [Obsolete("MessageChannel may not work across webview boundary yet..")]
        public static IWindow postMessage(this IWindow w, XElement sendxml, Action<XElement> yield)
        {
            if (w == null)
                return null;

            // lets not send a message to self
            if (w == Native.window)
                return w;

            // http://www.w3.org/TR/webmessaging/#introduction-0
            var ch = new MessageChannel();

            ch.port1.onmessage += new Action<MessageEvent>(
                m =>
                {
                    //Console.WriteLine("MessageChannel onmessage: " + new { m.data });

                    var xml = XElement.Parse((string)m.data);


                    yield(xml);
                }
            );

            ch.port1.start();
            ch.port2.start();

            w.postMessage(
                   sendxml.ToString(),
                   "*",
                   ch.port2
            );




            return w;
        }
    }

    public class FormAsPopupExtensionsForConsoleFormPackageMediator
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/2013
        // tested by X:\jsc.svn\examples\javascript\android\com.abstractatech.adminshell\com.abstractatech.adminshell\Application.cs
        public static bool InternalPopupHasFrame
        {
            get
            {
                // what if we are in web worker?


                // does the value exist?
                bool value = (Native.self as dynamic).__FormAsPopupExtensions_InternalPopupHasFrame;

                return !!value;
            }

            set
            {
                // this value should stay for app inline reloads
                (Native.self as dynamic).__FormAsPopupExtensions_InternalPopupHasFrame = value;
            }
        }

        #region postMessage
        public static Action<IWindow, XElement> postMessage =
          (w, x) =>
          {
              Console.WriteLine(new { x });

              var data = w.escape(x.ToString());

              var ww = w.open("http://hack-wtf-postmessage/" + data);
          };
        #endregion


        static Action later = delegate { };

        public static Action<IWindow, MessageEvent> onmessage = (w, m) =>
            {
                try
                {
                    var xml = XElement.Parse((string)m.data);

                    // the caller should be a nested iframe 
                    if (xml.Value == "Did you want to pop with your own frame?")
                    {
                        // { ports = 1, InternalPopupHasFrame = 1 } 
                        Console.WriteLine(
                            xml.Value + new
                            {
                                ports = m.ports.Length,
                                FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame
                            }
                        );

                        Action reply = delegate
                        {
                            // tell our iframe what we know. 
                            m.ports.WithEach(port =>
                                port.postMessage(
                                    new XElement("re", "yes i have my own frame!").ToString()
                                //null
                                )
                            );
                        };
                        // reply!
                        if (FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame)
                        {
                            reply();
                        }
                        else
                        {
                            later += delegate
                            {
                                if (m == null)
                                    return;

                                if (FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame)
                                {
                                    reply();
                                }

                                m = null;
                            };
                        }

                    }

                    // if there are no ports we need to use the newwindow hack
                    // Do you want to pop with your own frame?{ ports = 0, InternalPopupHasFrame = 1 }
                    // the caller should be AppWindow webview
                    if (xml.Value == "Do you want to pop with your own frame?")
                    {
                        FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame = true;


                        Console.WriteLine(xml.Value
                            + new
                            {
                                ports = m.ports.Length,
                                FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame
                            }
                        );

                        m.source.postMessage(
                            new XElement("re", "yes i have my own frame!").ToString(),
                            m.origin
                        );

                        m.ports.WithEach(port =>
                                port.postMessage(
                                new XElement("re", "yes i have my own frame!").ToString()
                                //null
                                )
                            );

                        // alternative hack as the one above does not yet work from html to webview
                        postMessage(w, new XElement("re", "yes i have my own frame!"));

                        later();

                    }
                }
                catch
                {
                }
            };


        // what? DNS_PROBE_FINISHED_NO_INTERNET
        static FormAsPopupExtensionsForConsoleFormPackageMediator()
        {
            // X:\jsc.internal.svn\examples\javascript\chrome\ChromeMyJscSolutionsNet\ChromeMyJscSolutionsNet\Application.cs

            if (Native.window == null)
            {
                // no DOM

                Console.WriteLine("FormAsPopupExtensionsForConsoleFormPackageMediator skipped due to no DOM...");

                return;
            }


            // Uncaught SecurityError: Blocked a frame with origin "http://192.168.1.101:26097" from accessing a frame with origin "http://192.168.1.101:5682". Protocols, domains, and ports must match. 
            // each inline app has its own version of this yet we need to keep sngle variable?

            // Can we pop with our own frame ? { InternalPopupHasFrame = , opener = false, parent = true, top = true }
            Console.WriteLine("Can we pop with our own frame ? "
                + new
                {
                    FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame,

                    // iframe need to know where it stands inside webview if any
                    opener = null != Native.window.opener,
                    parent = null != Native.window.parent,
                    top = null != Native.window.top,
                }
            );

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/20130729-newwin
            // only the top has access to webview?

            // the caller should be a nested iframe 

            #region  Native.window.top.postMessage
            Native.window.top.postMessage(
                new XElement("re", "Did you want to pop with your own frame?"),
                xml =>
                {
                    if (xml.Value == "yes i have my own frame!")
                    {


                        // too late?
                        FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame = true;

                        Console.WriteLine(
                            new
                            {
                                FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame
                            }
                        );

                    }
                }
            );
            #endregion




            Native.window.onmessage += m => onmessage(Native.window, m);

        }
    }

    // intellisense friendly :) discoverability
    public static class FormAsPopupExtensionsForConsoleFormPackage
    {
        public static T PopupInsteadOfClosing<T>(
            this T f,
            bool HandleFormClosing = true,
            Action SpecialCloseOnLeft = null,

            // as if we were docked
            bool SpecialNoMovement = false,


            Action NotifyDocked = null,
            Action NotifyFloat = null
            ) where T : Form
        {
            Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions.PopupInsteadOfClosing(
                f,
                HandleFormClosing,
                SpecialCloseOnLeft,
                SpecialNoMovement,
                NotifyDocked,
                NotifyFloat
            );

            return f;

        }
    }
}



namespace Abstractatech.JavaScript.FormAsPopup
{

    public static class FormAsPopupExtensions
    {





        // { ExceptionObject = System.MissingMethodException: Method not found: 'Void Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions.PopupInsteadOfClosing(System.Windows.Forms.Form, Boolean, System.Action)'.
        public static void PopupInsteadOfClosing(
            this Form f,
            bool HandleFormClosing,
            Action SpecialCloseOnLeft,

            bool SpecialNoMovement = false,
            Action NotifyDocked = null,
            Action NotifyFloat = null

            )
        {
            __Form __f = f;

            __f.InternalCloseButtonContent.title = "Popup";

            var content = new { f };


            #region AtClose
            Action AtClose = delegate
            {
                Console.WriteLine("AtClose!");


                // cant be minimized
                content.f.WindowState = FormWindowState.Normal;

                var w = Native.window.open("about:blank", "_blank",
                          content.f.Width,
                          content.f.Height,
                          false
                      );

                //var w = new IWindow();

                var HTMLTargetContainer_parent = (IHTMLElement)content.f.GetHTMLTargetContainer().parentNode;
                var HTMLTarget_parent = (IHTMLElement)content.f.GetHTMLTarget().parentNode;

                //Native.Window.onbeforeunload +=
                // chrome webview only supports onunload
                #region Native.Window.onunload
                Native.window.onunload +=
                    delegate
                    {
                        HTMLTarget_parent = null;
                        HTMLTargetContainer_parent = null;

                        if (w == null)
                            return;

                        w.close();
                    };
                #endregion

                var old = new { content.f.SizeGripStyle };

                content.f.SizeGripStyle = SizeGripStyle.Hide;

                content.f.GetHTMLTarget().Orphanize();

                if (NotifyDocked != null)
                    NotifyDocked();

                w.onload +=
                    delegate
                    {

                        // keep relative links working..
                        new IHTMLBase { href = Native.document.location.href }.AttachTo(w.document.body);

                        __Form ff = content.f;

                        var ff_TargetOuterBorder_parent = (IHTMLElement)ff.TargetOuterBorder.parentNode;

                        if (FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame)
                        {
                            ff.TargetOuterBorder.Orphanize().AttachTo(
                                w.document.body
                            );
                        }
                        else
                        {
                            content.f.GetHTMLTargetContainer().Orphanize().AttachTo(
                                w.document.body
                            );
                        }


                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/20130729-newwin
                        // lets enable popup within a popup.
                        // notice that events are in reverse tming
                        //popup: <re>Did you want to pop with your own frame?</re>
                        //popup: <re>Do you want to pop with your own frame?</re>
                        //popup: <re>Do you want to pop with your own frame?</re>
                        w.onmessage += m => FormAsPopupExtensionsForConsoleFormPackageMediator.onmessage(w, m);




                        #region title
                        w.document.title = content.f.Text;

                        f.TextChanged +=
                            delegate
                            {
                                if (w == null)
                                    return;


                                w.document.title = content.f.Text;
                            };
                        #endregion

                        #region onresize
                        // does this work for chrome?
                        w.onresize +=
                            delegate
                            {
                                if (w == null)
                                    return;

                                // not supposed to resize?
                                if (old.SizeGripStyle == SizeGripStyle.Hide)
                                    return;

                                var cs = content.f.ClientSize;

                                cs.Width = w.Width;
                                cs.Height = w.Height;

                                content.f.ClientSize = cs;

                                //__f.InternalRaiseClientSizeChanged(
                                //    new EventArgs()
                                //);
                            };
                        #endregion


                        ff.InternalBeforeFormClosing +=
                           (e) =>
                           {
                               if (w != null)
                               {
                                   e.Cancel = true;

                                   Console.WriteLine("InternalBeforeFormClosing!");

                                   if (FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame)
                                   {
                                       FormAsPopupExtensionsForConsoleFormPackageMediator.postMessage(Native.window, new XElement("re", "close this window!"));
                                   }

                                   w.close();
                               }
                           };
                        //w.onbeforeunload +=

                        #region w.onunload
                        w.onunload +=
                            delegate
                            {

                                if (HTMLTargetContainer_parent == null)
                                    return;

                                // undo

                                f.SizeGripStyle = old.SizeGripStyle;

                                if (FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame)
                                {
                                    ff.TargetOuterBorder.Orphanize().AttachTo(
                                        ff_TargetOuterBorder_parent
                                    );
                                }
                                else
                                {
                                    content.f.GetHTMLTargetContainer().Orphanize().AttachTo(
                                        HTMLTargetContainer_parent
                                    );
                                }

                                content.f.GetHTMLTarget().Orphanize().AttachTo(
                                    HTMLTarget_parent
                                );

                                w = null;

                                if (NotifyDocked != null)
                                    NotifyDocked();
                            };
                        #endregion

                        Native.window.requestAnimationFrame +=
                            delegate
                            {
                                // chrome clips to white?
                                w.resizeTo(content.f.Width + 16, content.f.Height);
                            };


                        if (NotifyFloat != null)
                            NotifyFloat();

                    };
            };
            #endregion


            var undo_x = 0;
            var undo_y = 0;

            //var fOpacity = 1.0;

            #region DragStart
            __f.InternalCaptionDrag.DragStart +=
                delegate
                {
                    //fOpacity = __f.Opacity;
                    undo_x = __f.Left;


                    undo_y = __f.Top;
                };
            #endregion


            var SpecialClose = false;

            #region DragMove
            __f.InternalCaptionDrag.DragMove +=
                delegate
                {

                    var IsNotOnLeft = !(f.Left < -f.Width / 3);
                    var IsNotOnRight = !(f.Right > (__f.InternalHostWidth + f.Width / 3));

                    var z = new { f.Right, f.Left };

                    if (SpecialNoMovement)
                    {
                        var dx = Math.Abs(undo_x - f.Left);
                        var dy = Math.Abs(undo_y - f.Top);


                        if (dx < 8)
                            if (dy < 8)
                            {
                                __f.Opacity = 1;

                                return;

                            }


                    }
                    else
                    {


                        if (IsNotOnLeft)
                            if (IsNotOnRight)
                            {
                                __f.Opacity = 1;

                                return;
                            }

                    }



                    __f.Opacity = 0.5;
                };
            #endregion


            #region DragStop
            __f.InternalCaptionDrag.DragStop +=
                delegate
                {
                    var IsNotOnLeft = !(f.Left < -f.Width / 3);
                    var IsNotOnRight = !(f.Right > (__f.InternalHostWidth + f.Width / 3));


                    __f.Opacity = 1;

                    var z = new { f.Right, f.Left };

          
                    if (SpecialNoMovement)
                    {
                        var dx = Math.Abs(undo_x - f.Left);
                        var dy = Math.Abs(undo_y - f.Top);

                        Console.WriteLine("SpecialNoMovement snap? " + new { dx, dy });

                        if (dx < 8)
                            if (dy < 8)
                            {
                                f.Left = undo_x;
                                f.Top = undo_y;
                                return;

                            }


                    }
                    else
                    {


                        if (IsNotOnLeft)
                            if (IsNotOnRight)
                            {
                                Console.WriteLine("still in window: " + z);
                                // still in the window!
                                // what about popups?
                                return;
                            }

                        if (!IsNotOnLeft)
                            if (SpecialCloseOnLeft != null)
                            {
                                AtClose = null;

                                SpecialCloseOnLeft();
                                f.Close();

                                return;
                            }

                    }

                    Console.WriteLine("close to popup");
                    f.Left = undo_x;
                    f.Top = undo_y;

                    SpecialClose = true;
                    f.Close();
                };
            #endregion

            #region FormClosing
            content.f.FormClosing +=
                (sender, e) =>
                {
                    if (AtClose == null)
                        return;

                    //if (FormClosingMeansDock)
                    //{
                    //    return;
                    //}

                    if (!HandleFormClosing)
                    {
                        if (!SpecialClose)
                        {
                            //if (e.CloseReason == CloseReason.UserClosing)
                            return;
                        }

                        SpecialClose = false;
                    }

                    Console.WriteLine("FormClosing!");

                    e.Cancel = true;

                    AtClose();
                };
            #endregion

        }

        // error: System.MissingMethodException: Method not found: 'Void Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions.PopupInsteadOfClosing(System.Windows.Forms.Form, Boolean, System.Action)'.
        public static void PopupInsteadOfClosing(
            this Form f,
            bool HandleFormClosing = true
            )
        {
            PopupInsteadOfClosing(f, HandleFormClosing, null);
        }

    }
}

namespace Abstractatech.JavaScript.FormAsPopup
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
            //content.AttachControlTo(page.Content);
            //content.AutoSizeControlTo(page.ContentSize);

            content.AttachControlToDocument();

            content.f.PopupInsteadOfClosing(
                HandleFormClosing: false
                //, SpecialNoMovement: true
                );
        }

    }
}
