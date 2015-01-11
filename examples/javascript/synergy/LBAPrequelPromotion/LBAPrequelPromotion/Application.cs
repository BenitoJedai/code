// android will use servers name for package id
using com.abstractatech.gamification.lbapp;
using LBAPrequelPromotion.Design;
using LBAPrequelPromotion.HTML.Audio.FromAssets;
using LBAPrequelPromotion.HTML.Images.FromAssets;
using LBAPrequelPromotion.HTML.Pages;
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
using ThreeDStuff.js;


// server will look at the first app and then try to find a preview from assets
namespace LBAPrequelPromotion
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
        public Application(IApp page)
        {
            // what is roslyn doing that is breaking async for us?


            //           02000002 LBAPrequelPromotion.Application
            //           script: error JSC1000: unsupported flow detected, try to simplify.
            //Assembly V:\LBAPrequelPromotion.Application.exe
            //DeclaringType LBAPrequelPromotion.Application, LBAPrequelPromotion.Application, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null
            //            OwnerMethod.ctor
            //            Offset 0065
            //            .Try ommiting the return, break or continue instruction.
            // not ready for roslyn!

#if false

            #region AtFormCreated
            FormStyler.AtFormCreated =
                 s =>
                 {
                     s.Context.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                     var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDrag().AttachTo(s.Context.GetHTMLTarget());
                 };
            #endregion



            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultTitle = "LBA Redux";
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;

                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text,
                    AtFormCreated: FormStyler.AtFormCreated
                );

                return;
            }
            #endregion
#endif


            // https://docs.google.com/a/jsc-solutions.net/spreadsheet/ccc?key=0AjBm0oHdZ3DwdFhMb2UxVnBYUnNnUE16LUNiUzJJSVE#gid=0
            //<iframe src="https://docs.google.com/a/jsc-solutions.net/spreadsheet/embeddedform?formkey=dFhMb2UxVnBYUnNnUE16LUNiUzJJSVE6MQ" width="760" height="692" frameborder="0" marginheight="0" marginwidth="0">Loading...</iframe>

            var onpause = new SAMPLES036 { volume = 0.3, autobuffer = true };
            var onunpause = new SAMPLES219 { autobuffer = true };

            onunpause.load();

            page.p96.InvokeOnComplete(
              delegate
              {

                  page.p512.InvokeOnComplete(
                      delegate
                      {




                          var diagnostics = new IHTMLIFrame { src = "https://docs.google.com/a/jsc-solutions.net/spreadsheet/embeddedform?formkey=dFhMb2UxVnBYUnNnUE16LUNiUzJJSVE6MQ", frameBorder = "0" };

                          diagnostics.style.backgroundColor = "rgba(255, 255, 255, 0)";
                          diagnostics.style.position = IStyle.PositionEnum.absolute;
                          diagnostics.style.left = "0px";
                          diagnostics.style.top = "-100%";
                          diagnostics.style.width = "100%";
                          diagnostics.style.height = "100%";
                          diagnostics.style.zIndex = 9000;
                          diagnostics.AttachToDocument();

                          // http://www.w3schools.com/css3/css3_transitions.asp
                          diagnostics.style.transition = "all 0.2s ease-in-out";
                          diagnostics.style.transition = "all 0.3s ease-in-out";


                          Action Hide =
                             delegate
                             {
                                 diagnostics.style.top = "-100%";
                                 diagnostics.style.backgroundColor = "rgba(255, 255, 255, 0)";

                                 onpause.play();
                                 onpause = new SAMPLES036 { volume = 0.3, autobuffer = true };
                             };

                          Action Show =
                              delegate
                              {
                                  diagnostics.style.top = "0%";
                                  diagnostics.style.backgroundColor = "rgba(255, 255, 255, 0.9)";

                                  onpause.play();
                                  onpause = new SAMPLES036 { volume = 0.3, autobuffer = true };
                              };

                          Action Toggle =
                              delegate
                              {
                                  if (diagnostics.style.top == "0%")
                                  {
                                      Hide();

                                  }
                                  else
                                  {
                                      Show();

                                  }
                              };
                          Action<int> AtKeyCode =
                              KeyCode =>
                              {
                                  //new { KeyCode }.ToString().ToDocumentTitle();

                                  if (KeyCode == 27)
                                  {
                                      Hide();

                                  }

                                  // EE
                                  if (KeyCode == 222)
                                  {
                                      Toggle();
                                  }

                                  // US
                                  if (KeyCode == 192)
                                  {
                                      Toggle();
                                  }
                              };


                          Native.Document.onkeyup +=
                              e =>
                              {
                                  AtKeyCode(e.KeyCode);
                              };

                          //Native.window.onorientationchange +=
                          //    delegate
                          //    {
                          //        Toggle();

                          //    };
                          page.fund.href = diagnostics.src;
                          page.fund.onclick +=
                              e =>
                              {
                                  e.preventDefault();
                                  e.stopPropagation();

                                  Toggle();
                              };

                          #region fundraiser
                          Action fundraiser = null;

                          fundraiser = delegate
                          {
                              var snd = new SAMPLES037 { volume = 0.01 + 0.1.Random() };

                              new Timer(
                                     delegate
                                     {
                                         page.fundspan.style.Opacity = 0.3;


                                         new Timer(
                                              delegate
                                              {
                                                  page.fundspan.style.Opacity = 1;
                                                  snd.play();

                                                  new Timer(
                                                      delegate
                                                      {
                                                          page.fundspan.style.Opacity = 0.7;


                                                          new Timer(
                                                              delegate
                                                              {
                                                                  page.fundspan.style.Opacity = 1;

                                                                  new Timer(
                                                                     delegate
                                                                     {
                                                                         page.fundspan.style.Opacity = 0.7;


                                                                         new Timer(
                                                                             delegate
                                                                             {
                                                                                 page.fundspan.style.Opacity = 1;

                                                                                 new Timer(
                                                                                    delegate
                                                                                    {
                                                                                        page.fundspan.style.Opacity = 0.7;


                                                                                        new Timer(
                                                                                            delegate
                                                                                            {
                                                                                                page.fundspan.style.Opacity = 1;

                                                                                                new Timer(
                                                                                                    delegate
                                                                                                    {
                                                                                                        fundraiser();
                                                                                                    }
                                                                                                 ).StartTimeout(3000);
                                                                                            }
                                                                                        ).StartTimeout(300);
                                                                                    }
                                                                                ).StartTimeout(300);
                                                                             }
                                                                         ).StartTimeout(300);
                                                                     }
                                                                 ).StartTimeout(300);
                                                              }
                                                          ).StartTimeout(300);
                                                      }
                                                  ).StartTimeout(300);
                                              }
                                          ).StartTimeout(10000 + 60000.Random());
                                     }
                                 ).StartTimeout(300);
                          };


                          fundraiser();
                          #endregion



                          Native.Document.title = "LBA Redux";
                          #region newicon
                          Action newicon = delegate
                          {

                              Native.Document.getElementsByTagName("link").AsEnumerable().ToList().WithEach(
                                  e =>
                                  {
                                      var link = (IHTMLLink)e;

                                      if (link.rel == "icon")
                                      {
                                          if (link.type == "image/png")
                                          {

                                              link.href = new fullbox().src;
                                          }
                                          else
                                          {
                                              link.Orphanize();
                                          }
                                      }
                                  }
                              );

                          };

                          newicon();
                          #endregion


                          #region music
                          var musicnext = new _01_Track_1 { volume = 0.1, autobuffer = true };
                          var music = default(_01_Track_1);

                          Action loop = null;

                          loop = delegate
                          {
                              music = musicnext.AttachToDocument();
                              musicnext = new _01_Track_1 { volume = 0.1, autobuffer = true };

                              music.onended +=
                                  delegate
                                  {
                                      Console.WriteLine(" music.onended ");
                                      music.Orphanize();

                                      loop();
                                  };

                              music.play();

                          };

                          loop();
                          #endregion

                          Native.window.onscroll +=
                              e =>
                              {
                                  if (e.Element != Native.Document)
                                      return;

                                  e.preventDefault();
                                  e.stopPropagation();

                                  Native.window.scrollTo(0, 0);
                              };


                          //new gong().AttachToDocument().play();
                          new ThreeDStuff.js.Tycoon4(
                              t =>
                              {
                                  page.Preview.onclick +=
                                      delegate
                                      {
                                          t.toolbar_btn_pause.RaiseClicked();
                                      };

                                  t.toolbar_btn_pause.Clicked +=
                                      delegate
                                      {


                                          if (t.toolbar_btn_pause.IsActivated)
                                          {

                                              onpause.play();
                                              onpause = new SAMPLES036 { volume = 0.3, autobuffer = true };

                                              t.PauseFogTween.Value = 0.7;
                                              music.volume = 0.01;
                                              page.Preview.Show();
                                          }
                                          else
                                          {
                                              //PauseFogTween.Value = 0;
                                              t.PauseFogTween.Value = 0;

                                              //gong.play();

                                              onunpause.play();
                                              onunpause = new SAMPLES219 { autobuffer = true };

                                              page.Preview.FadeOut();
                                              music.volume = 0.1;
                                          }
                                      };

                                  t.toolbar_btn_pause.RaiseClicked();

                                  new Timer(
                                         delegate
                                         {
                                             t.toolbar_btn_pause.RaiseClicked();
                                         }
                                     ).StartTimeout(4000);
                              }
                          );

                      }
                  );
              }
          );
        }

    }
}
