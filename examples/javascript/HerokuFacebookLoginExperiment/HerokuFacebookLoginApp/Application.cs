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
using HerokuFacebookLoginApp.Design;
using HerokuFacebookLoginApp.HTML.Pages;
using Abstractatech.ConsoleFormPackage.Library;

namespace HerokuFacebookLoginApp
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
            #region con
            //var con = new Abstractatech.ConsoleFormPackage.Library.ConsoleForm();
            var con = new ConsoleForm();

            con.InitializeConsoleFormWriter();

            con.Show();

            con.Left = Native.Window.Width - con.Width;
            con.Top = 0;

            Native.Window.onresize +=
                  delegate
                  {
                      con.Left = Native.Window.Width - con.Width;
                      con.Top = 0;
                  };


            con.Opacity = 0.6;



            con.HandleFormClosing = false;
            con.PopupInsteadOfClosing();
            #endregion

            Console.WriteLine("click on InitializeOurFacebookLoginService!");

            Native.Window.onmessage +=
                 e =>
                 {
                     // http://developer.klout.com/blog/read/fb_identity_lookup

                     Console.WriteLine("onmessage: " + e.data);
                 };

            // wont work as chrome app!
            page.InitializeOurFacebookLoginService.WhenClicked(
                delegate
                {
                    Console.WriteLine("loading... ");

                    // http://caniuse.com/iframe-sandbox
                    // http://security.stackexchange.com/questions/15146/using-iframes-to-sandbox-untrusted-code
                    var i = new IHTMLIFrame { };

                    // http://www.w3schools.com/tags/att_iframe_sandbox.asp
                    // The value of the sandbox attribute can either be an empty string (all the restrictions is applied), or a space-separated list of pre-defined values that will REMOVE particular restrictions.

                    // http://www.html5rocks.com/en/tutorials/security/sandboxed-iframes/

                    // this will break our popups and cookies for facebook!
                    //i.setAttribute("sandbox", "allow-scripts allow-forms allow-popups");
                    i.src = "http://young-beach-4377.herokuapp.com/";
                    i.AttachToDocument();

                    i.onload +=
                        delegate
                        {
                            Console.WriteLine("loading... done " + new { i.src });

                            // can we now talk to it?
                            // 
                        };
                }
            );

            page.InitializeOurFacebookLoginServiceViaWindow.WhenClicked(
               delegate
               {
                   Console.WriteLine("loading... ");
                   //var i = new IWindow { };
                   //i.document.location.href = "http://young-beach-4377.herokuapp.com/";

                   var i = Native.Window.open("http://young-beach-4377.herokuapp.com/", "_blank", 400, 225);


                   // doesnt tell us when loaded?
                   i.onload +=
                       delegate
                       {
                           Console.WriteLine("loading... done...");
                           //Console.WriteLine("loading... done " + new { i.document.title });
                           //Console.WriteLine("loading... done " + new { i.document.location.href });

                           // can we now talk to it?
                           // 
                       };

                   // popup will be blocked
                   //new IHTMLButton { innerText = "send DoLogin" }.AttachToDocument().WhenClicked(
                   //     delegate
                   //     {
                   //         Console.WriteLine("send DoLogin");

                   //         i.postMessage(
                   //             new XElement("DoLogin", new XAttribute("tag", "foo")).ToString()
                   //         );


                   //     }
                   //);
               }
           );


            page.InitializeOurFacebookLoginServiceViaWindowAndClose.WhenClicked(
              delegate
              {
                  Console.WriteLine("loading... ");
                  //var i = new IWindow { };
                  //i.document.location.href = "http://young-beach-4377.herokuapp.com/";

                  var i = Native.Window.open("http://young-beach-4377.herokuapp.com/#c", "_blank", 400, 225);


                  // doesnt tell us when loaded?
                  i.onload +=
                      delegate
                      {
                          Console.WriteLine("InitializeOurFacebookLoginServiceViaWindowAndClose loading... done...");
                          //Console.WriteLine("loading... done " + new { i.document.title });
                          //Console.WriteLine("loading... done " + new { i.document.location.href });

                          // can we now talk to it?
                          // 
                      };

                  i.onbeforeunload +=
                      delegate
                      {
                          Console.WriteLine("InitializeOurFacebookLoginServiceViaWindowAndClose onbeforeunload");

                      };

                  // popup will be blocked
                  //new IHTMLButton { innerText = "send DoLogin" }.AttachToDocument().WhenClicked(
                  //     delegate
                  //     {
                  //         Console.WriteLine("send DoLogin");

                  //         i.postMessage(
                  //             new XElement("DoLogin", new XAttribute("tag", "foo")).ToString()
                  //         );


                  //     }
                  //);
              }
          );


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
