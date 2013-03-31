using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using com.abstractatech.appmanager.Design;
using com.abstractatech.appmanager.HTML.Pages;
using com.abstractatech.appmanager.windows;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Runtime;

namespace com.abstractatech.appmanager
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
        public Application(IBeforeLogin page)
        {
            "My Appz".ToDocumentTitle();




            #region LoginButton
            page.LoginButton.onclick +=
                    delegate
                    {

                        // ask for credentials for new ui

                        var s = new IHTMLScript { src = "/a" };

                        // http://stackoverflow.com/questions/538745/how-to-tell-if-a-script-tag-failed-to-load
                        s.onload +=
                            delegate
                            {
                                page.LoginButton.Orphanize();
                            };

                        s.AttachToDocument();

                    };
            #endregion

            #region LaunchMyAppz
            var about = new Cookie("about");

            if (!about.BooleanValue)
            {
                var a = new com.abstractatech.appmanager.about.HTML.Pages.App();

                a.Container.AttachToDocument();

                a.LaunchMyAppz.onclick +=
                    delegate
                    {
                        about.BooleanValue = true;

                        a.Container.Orphanize();
                    };


                return;
            }
            #endregion


        }

        public sealed class a
        {
            public readonly ApplicationWebService service = new ApplicationWebService();


            public a(IBeforeLogin ee)
            {
                FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;

                var ff = new Form { FormBorderStyle = FormBorderStyle.None };


                var ScrollArea = new App().ScrollArea.AttachTo(ff.GetHTMLTargetContainer());



                //ScrollArea.style.backgroundColor = "#185D7B";
                //ScrollArea.style.backgroundColor = "#185D7B";
                ScrollArea.style.backgroundColor = "#105070";

                var SidebarWidth = 172;

                ff.MoveTo(SidebarWidth, 0);

                Action AtResize = delegate
                {

                    ff.SizeTo(Native.Window.Width - SidebarWidth, Native.Window.Height);
                };

                Native.Window.onresize +=
                    delegate
                    {
                        AtResize();

                    };

                AtResize();

                var iii = global::CSSMinimizeFormToSidebar.ApplicationExtension.InitializeSidebarBehaviour(
              ff, HandleClosed: true
          );
                iii.SidebarText.className = "AppPreviewText";
                //iii.SidebarText.innerText = "My Appz";
                //iii.SidebarText.innerText = "Synchronizing...";
                var finish = iii.SidebarText.ToASCIIStyledLoadAnimation("My Appz");


                //Native.Document.body.style.backgroundColor = "#105070";
                Native.Document.body.style.backgroundColor = "#185D7B";

                Native.Window.onresize +=
                    delegate
                    {
                        ff.Show();
                    };


                //var page = new App();

                //page.ScrollArea.AttachToDocument();

                var count = 0;

                var yield_BringToFront = false;

                var icon_throttle = 0;

                #region yield
                yield_ACTION_MAIN yield = (
                            packageName,
                            name,
                            __IsCoreAndroidWebServiceActivity,
                            label
                        ) =>
                {

                    if (string.IsNullOrEmpty(label))
                        label = packageName;

                    var IsCoreAndroidWebServiceActivity = System.Convert.ToBoolean(__IsCoreAndroidWebServiceActivity);

                    count++;

                    var a = new AppPreview();

                    #region icon
                    if (packageName != "foo")
                    {
                        icon_throttle += 900;

                        new ScriptCoreLib.JavaScript.Runtime.Timer(
                            delegate
                            {
                                var i = new IHTMLImage { src = "/icon/" + packageName };
                                i.InvokeOnComplete(
                                    delegate
                                    {
                                        a.Icon.src = i.src;
                                    }
                                );
                            }
                        ).StartTimeout(icon_throttle);
                    }
                    #endregion


                    //a.Icon.src = "/icon/" + packageName;

                    //a.Icon.src = "data:image/png;base64," + icon_base64;
                    a.Label.innerText = label;

                    if (yield_BringToFront)
                    {
                        Console.WriteLine("yield_BringToFront " + new { packageName });

                        ScrollArea.insertBefore(a.Container, ScrollArea.firstChild);
                    }
                    else
                    {
                        a.Container.AttachTo(ScrollArea);
                    }

                    #region onclick
                    Action<bool> onclick =
                        CanAutoLaunch =>
                        {


                            Console.WriteLine(new { label });
                            var content = new ApplicationControl();


                            var f = new Form { Text = label };
                            f.ClientSize = content.Size;
                            f.Show();

                            Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions.PopupInsteadOfClosing(
                                f,
                                HandleFormClosing: false,
                                SpecialCloseOnLeft: delegate
                                {
                                    Console.WriteLine("SpecialCloseOnLeft");

                                    service.Launch(
                                        packageName,
                                        name,
                                        DisableCallbackToken: "true"
                                    );

                                }
                            );

                            if (CanAutoLaunch && IsCoreAndroidWebServiceActivity)
                            {

                                f.Opacity = 0.5;

                                var w = new WebBrowser();
                                w.Dock = DockStyle.Fill;

                                f.Controls.Add(w);

                                service.Launch(
                                    packageName,
                                    name,

                                    yield_port:
                                        // we should remember the port
                                        // to launch it offline via AppCache
                                        port =>
                                        {
                                            // close to left sidebar!
                                            ff.Close();


                                            f.Opacity = 1.0;

                                            var uri = Native.Document.location.protocol
                                                + "//"
                                                + Native.Document.location.host.TakeUntilIfAny(":")
                                                + ":" + port;


                                            w.Navigate(uri);

                                            f.ClientSize = content.Size;
                                        }
                                );
                            }
                            else
                            {
                                f.Controls.Add(content);
                                content.Dock = DockStyle.Fill;




                                content.Label.Text = label;
                                content.Package.Text = packageName;

                                a.Icon.cloneNode(true).AttachTo(

                                    ScriptCoreLib.JavaScript.Windows.Forms.Extensions.GetHTMLTargetContainer(content.Icon)

                                );

                                content.Uninstall.Click +=
                                    delegate
                                    {
                                        service.Remove(
                                           packageName,
                                           name
                                        );

                                        f.Hide();

                                        // http://www.w3schools.com/cssref/pr_text_text-decoration.asp
                                        a.Label.style.textDecoration = "line-through";
                                    };

                                content.Launch.Click +=
                                    delegate
                                    {
                                        // level 1
                                        // run on android

                                        // level 2
                                        // run as float

                                        // level 3 
                                        // run here as iframe

                                        // level 4
                                        // run here as js import

                                        service.Launch(
                                            packageName,
                                            name,

                                            yield_port:
                                                port =>
                                                {
                                                    // close to left sidebar!
                                                    ff.Close();


                                                    var uri = Native.Document.location.protocol
                                                        + "//"
                                                        + Native.Document.location.host.TakeUntilIfAny(":")
                                                        + ":" + port;

                                                    var w = new WebBrowser();

                                                    f.Controls.Add(w);
                                                    w.Dock = DockStyle.Fill;
                                                    w.Navigate(uri);

                                                    f.ClientSize = content.Size;
                                                }
                                        );

                                    };
                            }
                        };
                    #endregion

                    a.Clickable.oncontextmenu +=
                       e =>
                       {
                           e.preventDefault();

                           onclick(false);
                       };

                    a.Clickable.onclick +=
                        e =>
                        {
                            e.preventDefault();

                            onclick(true);
                        };

                };
                #endregion


                #region more
                var skip = 0;
                var take = 32;



                {


                    Action done = delegate { };


                    Action MoveNext = delegate
                    {
                        icon_throttle = 0;

                        //more.disabled = true;
                        //more.innerText = "checking for more...";

                        Console.WriteLine("MoveNext: " + new { skip, take });

                        service.queryIntentActivities(
                            yield,
                            skip: "" + skip,
                            take: "" + take,
                            yield_done: done

                        );


                        //service.File_list("",
                        //    ydirectory: ydirectory,
                        //    yfile: yfile,
                        //    sskip: skip.ToString(),
                        //    stake: take.ToString(),
                        //    done: done
                        //);

                        skip += take;

                    };

                    done = delegate
                    {
                        //more.innerText = getmore;
                        //more.disabled = false;

                        if (count == skip)
                        {
                            //Native.Document.body.With(
                            //     body =>
                            //     {
                            //         if (more.disabled)
                            //             return;

                            //         if (body.scrollHeight - 1 <= Native.Window.Height + body.scrollTop)
                            //         {
                            MoveNext();
                            //          }

                            //      }
                            //);
                        }
                        else
                        {
                            finish();
                            yield_BringToFront = true;

                            service.oninstall(yield);

                        }
                    };



                    MoveNext();

                    //more.onclick += delegate
                    //{
                    //    MoveNext();
                    //};



                    //Native.Window.onscroll +=
                    //      e =>
                    //      {

                    //          Native.Document.body.With(
                    //              body =>
                    //              {
                    //                  if (more.disabled)
                    //                      return;

                    //                  if (body.scrollHeight - 1 <= Native.Window.Height + body.scrollTop)
                    //                  {
                    //                      MoveNext();
                    //                  }

                    //              }
                    //        );

                    //      };
                }
                //);
                #endregion


            }
        }
    }
}
