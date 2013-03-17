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
using HerokuFacebookLoginExperiment.Design;
using HerokuFacebookLoginExperiment.HTML.Pages;

namespace HerokuFacebookLoginExperiment
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
            var con = new Abstractatech.ConsoleFormPackage.Library.ConsoleForm();

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
            //con.PopupInsteadOfClosing();
            #endregion

            Console.WriteLine("loading facebook api... ");
            new IHTMLScript { src = "//connect.facebook.net/en_US/all.js" }.AttachToHead().onload +=
                delegate
                {
                    Console.WriteLine("loading facebook api... done");

                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201303/20130317-facebook

                    //var x = new dynamic { appID = "x" };
                    dynamic a = new object();

                    a.appId = "625051627510580";
                    a.status = true;
                    a.cookie = true;


                    //    FB.init({
                    //  appId      : 'YOUR_APP_ID', // App ID
                    //  channelUrl : '//WWW.YOUR_DOMAIN.COM/channel.html', // Channel File
                    //  status     : true, // check login status
                    //  cookie     : true, // enable cookies to allow the server to access the session
                    //  xfbml      : true  // parse XFBML
                    //});

                    object o = a;

                    // Given URL is not allowed by the Application configuration.: One or more of the given URLs is not allowed by the App's settings.  It must match the Website URL or Canvas URL, or the domain must be a subdomain of one of the App's domains. 
                    new IFunction("e", "return FB.init(e);").apply(null, o);
                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
