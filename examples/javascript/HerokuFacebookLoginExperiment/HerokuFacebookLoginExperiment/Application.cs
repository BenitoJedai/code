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
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.net;

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
        public Application(FlashHeatZeeker.AndroidCTA.HTML.Pages.IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201303/20130317-facebook
            // !!!
            // B:\>git add -A & git commit -am "changed greeting" & git push heroku master

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

            var snd_SelectWeapon = new HerokuFacebookLoginExperiment.HTML.Audio.FromAssets.snd_SelectWeapon();


            // http://stackoverflow.com/questions/388646/debugging-javascript-in-safari-for-windows
            // safari bombs here
            // TypeError: 'undefined' is not a function (evaluating 'd.snd_click.load()')
            try
            {
                snd_click.load();
                snd_SelectWeapon.load();
            }
            catch
            {
            }



            var CloseMode = Native.Document.location.hash.StartsWith("#c");
            var CloseModeCallback = "";

            if (CloseMode)
            {
                // If you implement communication between SWF files in different domains, you specify 
                // a connectionName parameter that begins with an underscore. Specifying the underscore 
                // makes the SWF file with the receiving LocalConnection object more portable between 
                // domains. Here are the two possible cases

                CloseModeCallback = "_Invoke" + Native.Document.location.hash.Substring("#c".Length);
                Console.WriteLine(new { CloseModeCallback });
            }

            var sprite = default(ApplicationSprite);

            if (!string.IsNullOrEmpty(CloseModeCallback))
            {
                sprite = new ApplicationSprite();
                sprite.AttachSpriteToDocument();

            }

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

                        // http://stackoverflow.com/questions/4758770/how-to-get-access-token-from-fb-login-method-in-javascript-sdk
                        var accessToken = (string)new IFunction("return FB.getAuthResponse()['accessToken'];").apply(null);


                        var xml = new XElement("response",
                                   new XAttribute("name", name),
                                   new XAttribute("id", id),
                                   new XAttribute("third_party_id", third_party_id),
                                   new XAttribute("accessToken", accessToken)
                               );

                        Console.WriteLine("Good to see you, " + name + " " + new { id, third_party_id, accessToken });

                        if (CloseMode)
                        {
                            page.FacebookLogin.Hide();
                            page.FacebookLogout.Hide();
                        }

                        Native.window.opener.With(
                             opener => opener.postMessage(xml.ToString())
                         );


                        if (sprite != null)
                        {
                            // http://livedocs.adobe.com/flex/3/html/help.html?content=17_Networking_and_communications_4.html
                            Console.WriteLine("lc.send(connectionName: " + CloseModeCallback + ", data: " + xml.ToString() + ")");

                            sprite.yield += ack_data =>
                            {
                                Console.WriteLine(new { ack_data });

                                // allow the sound to complete..

                                
                                Native.window.setTimeout(
                                    delegate
                                    {
                                        Native.window.close();
                                    },
                                    300
                                );
                            };

                            sprite.Invoke(
                                connectionName: CloseModeCallback,
                                data: xml.ToString()
                            );
                        }
                        else
                        {
                            if (CloseMode)
                            {

                                // allow the sound to complete..
                                Native.window.setTimeout(
                                    delegate
                                    {
                                        Native.window.close();
                                    },
                                    300
                                );
                            }
                        }
                    };

                try
                {
                    snd_SelectWeapon.play();
                }
                catch
                {
                    // TypeError: 'undefined' is not a function (evaluating 'a.snd_click.play()')
                    // no safari
                }

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

            Native.window.onmessage +=
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

            if (sprite != null)
                sprite.Invoke(
                    connectionName: CloseModeCallback,
                    data: new XElement("ready", new XAttribute("tag", "foo")).ToString()
                );


            Native.window.opener.With(
                opener =>
                {
                    opener.postMessage(
                        new XElement("ready", new XAttribute("tag", "foo")).ToString()
                    );

                }
            );


            #region FacebookLogin
            page.FacebookLogin.WhenClicked(
                delegate
                {
                    try
                    {
                        snd_click.play();
                    }
                    catch
                    { }

                    DoLogin();
                }
            );
            #endregion


            #region FacebookLogout
            page.FacebookLogout.WhenClicked(
                delegate
                {
                    try
                    {
                        snd_click.play();
                    }
                    catch
                    { }


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
            #endregion




            Console.WriteLine("loading facebook api... ");

            #region fb
            var fb = new IHTMLScript { src = "//connect.facebook.net/en_US/all.js" };

            Console.WriteLine("will load " + new { fb.src });
            fb.onload +=
                delegate
                {
                    Console.WriteLine("loaded " + new { fb.src });


                    Console.WriteLine("loading facebook api... done");

                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201303/20130317-facebook

                    //var x = new dynamic { appID = "x" };
                    dynamic a = new object();

                    a.appId = "625051627510580";
                    a.status = true;
                    a.cookie = true;
                    a.oauth = true;
                    a.xfbml = true;

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

                                if (CloseMode)
                                {
                                }
                                else
                                {
                                    page.FacebookLogout.Show();
                                }

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

            fb.AttachToHead();
            #endregion



            @"Operation Heat Zeeker".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"Operation Heat Zeeker",
            //    value => value.ToDocumentTitle()
            //);

            if (!CloseMode)
                page.story.Show();

        }

    }
}
