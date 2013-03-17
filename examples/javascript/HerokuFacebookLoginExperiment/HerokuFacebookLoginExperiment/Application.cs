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
using Abstractatech.ConsoleFormPackage.Library;

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
            //#region con
            ////var con = new Abstractatech.ConsoleFormPackage.Library.ConsoleForm();
            //var con = new ConsoleForm();

            //con.InitializeConsoleFormWriter();


            //con.Left = Native.Window.Width - con.Width;
            //con.Top = 0;

            //Native.Window.onresize +=
            //      delegate
            //      {
            //          con.Left = Native.Window.Width - con.Width;
            //          con.Top = 0;
            //      };


            //con.Opacity = 0.6;



            //con.HandleFormClosing = false;
            //con.PopupInsteadOfClosing();

            //con.Show();
            //#endregion

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201303/20130317-facebook
            Console.WriteLine("HerokuFacebookLoginExperiment " + new
            {
                Native.Document.location.href,
                Native.Document.location.hash,
                Native.Document.domain
            });

            var snd_click = new HerokuFacebookLoginExperiment.HTML.Audio.FromAssets.snd_click();
            snd_click.load();
            var snd_SelectWeapon = new HerokuFacebookLoginExperiment.HTML.Audio.FromAssets.snd_SelectWeapon();
            snd_SelectWeapon.load();


            #region Greet
            Action Greet = delegate
            {


                Action<dynamic> AtAPI =
                    response =>
                    {
                        // https://developers.facebook.com/docs/reference/api/user/
                        string name = response.name;
                        string id = response.id;
                        string third_party_id = response.third_party_id;

                        var xml = new XElement("response",
                                   new XAttribute("name", name),
                                   new XAttribute("id", id),
                                   new XAttribute("third_party_id", third_party_id)
                               );

                        Console.WriteLine("Good to see you, " + name + " " + new { id, third_party_id });


                        Native.Window.opener.With(
                            opener =>
                            {


                                opener.postMessage(
                                   xml.ToString()
                                );

                                if (Native.Document.location.hash == "#c")
                                {
                                    page.FacebookLogin.Hide();
                                    page.FacebookLogout.Hide();

                                    // allow the sound to complete..
                                    Native.Window.setTimeout(
                                        delegate
                                        {
                                            Native.Window.close();
                                        },
                                        300
                                    );
                                }
                            }
                        );


                    };

                snd_SelectWeapon.play();

                Console.WriteLine("Welcome!  Fetching your information.... ");

                // http://stackoverflow.com/questions/7365110/get-facebook-third-party-id-from-uid-in-javascript
                // ?fields=third_party_id
                // https://developers.facebook.com/docs/reference/login/public-profile-and-friend-list/
                new IFunction("e", "return FB.api('/me?fields=name,third_party_id', e);").apply(null, IFunction.OfDelegate(AtAPI));
            };
            #endregion

            #region DoLogin
            Action DoLogin = delegate
            {
                //page.FacebookLogin.style.color = "blue";

                Action<dynamic> AtLogin =
                    response =>
                    {
                        //page.FacebookLogin.style.color = "";

                        dynamic authResponse = response.authResponse;


                        Console.WriteLine("AtLogin: " + new { authResponse });
                        string status = response.status;
                        Console.WriteLine("AtLogin: " + new { status });


                        if (status == "connected")
                        {
                            //   // connected
                            Greet();


                            page.FacebookLogout.Show();
                            page.FacebookLogin.Hide();


                            //response.status === 'connected' will be true whenever the User viewing the page is both logged into Facebook and has already previously authorized the current app.

                        }
                    };



                // !!
                // Under normal circumstances you should attach this FB.login() call to a Javascript onClick event 
                // as the call results in a popup window being opened, which will be blocked by most browsers.
                Console.WriteLine("FB.login...");
                new IFunction("e", "return FB.login(e);").apply(null, IFunction.OfDelegate(AtLogin));
            };
            #endregion

            Native.Window.onmessage +=
                e =>
                {
                    Console.WriteLine("onmessage: " + e.data);

                    try
                    {

                        var xml = XElement.Parse((string)e.data);

                        if (xml.Name.LocalName == "DoLogin")
                        {
                            DoLogin();
                        }
                    }
                    catch
                    {

                    }
                };

            Native.Window.opener.With(
                        opener =>
                        {
                            opener.postMessage(
                               new XElement("ready", new XAttribute("tag", "foo")).ToString()
                           );

                        }
                    );


            page.FacebookLogin.WhenClicked(
                delegate
                {
                    snd_click.play();
                    DoLogin();
                }
            );


            page.FacebookLogout.WhenClicked(
                delegate
                {
                    snd_click.play();

                    Action<dynamic> AtLogout =
                     response =>
                     {


                         Console.WriteLine("AtLogout!");


                         page.FacebookLogout.Hide();
                         page.FacebookLogin.Show();

                     };

                    // Now whenever the Log out button is clicked, the user will be logged out of your app, their session cleared and also logged out of Facebook. 
                    // They will not, however, have authorization for your app revoked.
                    Console.WriteLine("FB.logout...");
                    new IFunction("e", "return FB.logout(e);").apply(null, IFunction.OfDelegate(AtLogout));


                }
            );




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

                    // !! need to run on heroku!
                    // Given URL is not allowed by the Application configuration.: One or more of the given URLs is not allowed by the App's settings.  It must match the Website URL or Canvas URL, or the domain must be a subdomain of one of the App's domains. 
                    new IFunction("e", "return FB.init(e);").apply(null, o);


                    Action<dynamic> AtLoginStatus =
                        response =>
                        {
                            string status = response.status;

                            // AtLoginStatus: { status = not_authorized }
                            Console.WriteLine("AtLoginStatus: " + new { status });

                            page.FacebookLogin.Hide();
                            page.FacebookLogout.Hide();

                            if (status == "connected")
                            {
                                //   // connected
                                Greet();

                                // now what?

                                page.FacebookLogout.Show();

                                //response.status === 'connected' will be true whenever the User viewing the page is both logged into Facebook and has already previously authorized the current app.

                            }
                            else if (status == "not_authorized")
                            {
                                //   // not_authorized

                                //response.status === 'not_authorized' is true whenever the User viewing the page is logged into Facebook, but has not yet authorized the current app. In this case, the FB.login() code shown in Step 4 can be used to prompt them to authenticate.
                                page.FacebookLogin.Show();

                            }
                            else
                            {
                                page.FacebookLogin.Show();
                                //   // not_logged_in


                                //The final else statement is true when the User viewing the page is not logged into Facebook, and therefore the state of their authorization of the app is unknown. In this case, the FB.login() code in Step 4 will prompt them to log in to Facebook and then again with the Login Dialog if they have not yet authorized, or with the response object described above if they have.

                            }


                        };

                    Console.WriteLine("before getLoginStatus...");

                    new IFunction("e", "return FB.getLoginStatus(e);").apply(null, IFunction.OfDelegate(AtLoginStatus));


                    //                   FB.getLoginStatus(function(response) {

                    //});

                };

            @"Operation Heat Zeeker".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"Operation Heat Zeeker",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
