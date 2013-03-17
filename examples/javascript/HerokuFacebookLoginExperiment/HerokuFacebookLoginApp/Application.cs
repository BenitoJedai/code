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

            page.InitializeOurFacebookLoginService.WhenClicked(
                delegate
                {
                    Console.WriteLine("loading... ");
                    var i = new IHTMLIFrame { src = "http://young-beach-4377.herokuapp.com/" }.AttachToDocument();


                    i.onload +=
                        delegate
                        {
                            Console.WriteLine("loading... done " + new { i.src });

                            // can we now talk to it?
                            // 
                        };
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
