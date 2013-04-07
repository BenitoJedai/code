using AccountExperiment.MyDevicesComponent;
using AccountExperiment.Schema;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace AccountExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        //static AccountExperiment.Schema.MyAccountQueries.Insert ref0;
        //static AccountExperiment.Schema.MySessionQueries.Insert ref1;


        // is it ever used?
        public void Authenticate(
            string user,
            string password,

            Action<string> yield_session)
        {


            // create a new session audit object for us to use
            yield_session("foo");
        }

        // future versions will let client use this directly.
        // this should be a ComponentModel component
        public global::GravatarExperiment.ApplicationWebService gravatar = new GravatarExperiment.ApplicationWebService();


        public void gravatar_Gravatar(string e, Action<string> avatar, Action<string> profile)
        {
            gravatar.Gravatar(e, avatar, profile);
        }

        // java will complain about non public fields. we use lambdas and jsc is not doing the magic yet.
        public MyAccount account = new MyAccount();
        public MySession session = new MySession();

        #region MyDevices
        public global::AccountExperiment.MyDevicesComponent.ApplicationWebService MyDevices = new MyDevicesComponent.ApplicationWebService();

        public void MyDevices_Insert(string session, string name, string value, Action<string> yield)
        {
            Console.WriteLine("enter MyDevices_Insert");
            this.account.SelectByCookie(
                new MyAccountQueries.SelectByCookie { cookie = session },
                r =>
                {
                    long account = r.id;


                    this.MyDevices.MyDevices_Insert(
                        "" + account,
                        name,
                        value,
                        yield
                    );
                }
            );
            Console.WriteLine("exit MyDevices_Insert");
        }

        public void MyDevices_SelectByAccount(string session, Action<string, string, string> yield, Action done)
        {
            // ex = {"{ Message = database is locked\r\ndatabase is locked, StackTrace =    at System.Data.SQLite.SQLite3.Step(SQLiteStatement stmt)\r\n   at System.Data.SQLite.SQLiteDataReader.NextResult()\r\n   at System.Data.SQLite.SQLiteDataReader..ctor(SQLiteCommand cmd, ...

            Console.WriteLine("enter MyDevices_SelectByAccount");

            this.account.SelectByCookie(
               new MyAccountQueries.SelectByCookie { cookie = session },
               r =>
               {
                   long account = r.id;

                   this.MyDevices.MyDevices_SelectByAccount(
                       "" + account, yield, done
                   );
               }
           );
            Console.WriteLine("exit MyDevices_SelectByAccount");
        }

        public void MyDevices_Update(string session, string id, string name, string value, Action done)
        {
            Console.WriteLine("enter MyDevices_Update");
            this.account.SelectByCookie(
                 new MyAccountQueries.SelectByCookie { cookie = session },
                 r =>
                 {
                     long account = r.id;


                     //Additional information: database is locked

                     //database is locked


                     Console.WriteLine("before this.MyDevices.MyDevices_Update");
                     this.MyDevices.MyDevices_Update(
                         "" + account,
                         id,
                         name,
                         value,
                         done
                     );

                     Console.WriteLine("after this.MyDevices.MyDevices_Update");

                 }
             );
            Console.WriteLine("exit MyDevices_Update");
        }
        #endregion

        public ApplicationWebService()
        {
            // tell our third party component what datasource we are using
            this.MyDevices.devices.csb.DataSource = this.account.csb.DataSource;

            // how will it know to initialize then?
            this.MyDevices.devices.WithConnectionOverride = this.account.WithConnection;
        }

        public void account_SelectCount(Action<string> y)
        {
            y("" + account.SelectCount());
        }

        public void account_SelectByCookie(string session, Action<string> yield)
        {
            Console.WriteLine("enter account_SelectByCookie");

            account.SelectByCookie(
                new MyAccountQueries.SelectByCookie { cookie = session },
                r =>
                {
                    long id = r.id;
                    string email = r.email;

                    yield(email);

                }
            );

            Console.WriteLine("exit account_SelectByCookie");
        }

        // yes the client can change session at will. essentially spoof.
        public void page_TellServerToDropMySession_onclick(string session, Action done)
        {
            Console.WriteLine("enter page_TellServerToDropMySession_onclick");
            this.session.DeleteByCookie(
                new MySessionQueries.DeleteByCookie { cookie = session }
            );
            Console.WriteLine("exit page_TellServerToDropMySession_onclick");

            done();
        }

        public void SinceIAmNowLggedInTellMeHowManyActiveSessionsAreThere(string session, Action<string> yield)
        {
            var count = this.session.SelectCount();

            yield(new { count }.ToString());
        }

        string CreateSession(long account, long ticks)
        {
            // X:\jsc.svn\examples\javascript\appengine\SHA1Experiment\SHA1Experiment\ApplicationWebService.cs

            // should encrypt this
            var cookie =
                new
                {
                    ticks,
                    account,
                    comment = "we shall SHA1 this!"
                }.ToString().ToSHA1().ToHexString();
            // be aware: http://www.sha1-lookup.com/index.php?q=28da025b51ac352ea11bee23b56bb04c7766d942


            session.Insert(
                new MySessionQueries.Insert
                {
                    ticks = ticks,
                    cookie = cookie,
                    account = account
                }
            );

            Console.WriteLine("CreateSession: " + new { cookie });

            return cookie;
        }

        public void CreateAccount(

            string name,
            string web,
            string email,
            string password,
            string skype,

            Action<string> yield_session
            )
        {
            var now = DateTime.Now;
            var ticks = now.Ticks;

            var id = account.Insert(
                new MyAccountQueries.Insert
                {
                    ticks = ticks,

                    name = name,
                    web = web,
                    email = email,
                    password = password,
                    skype = skype
                }
            );


            var cookie = CreateSession(id, ticks);

            // yay, we can now log in!

            yield_session(cookie);
        }


        public void Handler(WebServiceHandler h)
        {
            #region /view-source
            h.Applications.Where(k => h.Context.Request.Path == "/" + k.TypeName.ToLower() + "/view-source").WithEach(
                x => h.WriteSource(x)
            );
            #endregion

            #region /register
            if (h.Context.Request.Path == "/register")
            {
                h.Applications.Single(k => k.TypeName == "Register").With(
                    app =>
                    {
                        var html = XElement.Parse(app.PageSource);

                        html.Add(
                            new XElement("script",
                                new XAttribute("src", "/register/view-source"),
                                " "
                            )
                        );

                        h.Context.Response.Write(html.ToString());
                        h.CompleteRequest();
                    }
                );
            }
            #endregion

            #region /register
            if (h.Context.Request.Path == "/registerr")
            {
                h.Applications.Single(k => k.TypeName == "Registerr").With(
                    app =>
                    {
                        var html = XElement.Parse(app.PageSource);

                        html.Add(
                            new XElement("script",
                                new XAttribute("src", "/registerr/view-source"),
                                " "
                            )
                        );

                        h.Context.Response.Write(html.ToString());
                        h.CompleteRequest();
                    }
                );
            }
            #endregion


            #region /login
            if (h.Context.Request.Path == "/login")
            {
                Console.WriteLine("will try to log in... " + new { h.Context.Request.HttpMethod });

                #region POST
                if (h.Context.Request.HttpMethod == "POST")
                {
                    // browser sending us the credentials.

                    var email = h.Context.Request.Form["email"];
                    var password = h.Context.Request.Form["password"];

                    Action yield = delegate
                    {
                        Console.WriteLine("check credentials!");

                        // slow it down!
                        Thread.Sleep(1000);

                        h.Context.Response.SetCookie(new System.Web.HttpCookie("message", "check credentials!"));


                        //h.Context.Response.Redirect(h.Context.Request.UrlReferrer.ToString());
                        h.Context.Response.Redirect("/boo");
                        h.CompleteRequest();
                    };

                    account.SelectByPassword(
                        new MyAccountQueries.SelectByPassword { email = email, password = password },
                        id =>
                        {

                            yield = delegate
                            {
                                Console.WriteLine("hello! " + id);

                                var now = DateTime.Now;
                                var ticks = now.Ticks;

                                var cookie = CreateSession(id, ticks);


                                h.Context.Response.SetCookie(new System.Web.HttpCookie("session", cookie));

                                //h.Context.Response.Redirect(h.Context.Request.UrlReferrer.ToString().TakeUntilLastOrNull("/login"));
                                h.Context.Response.Redirect("/dashboard");
                                h.CompleteRequest();
                            };
                        }
                    );

                    yield();


                    return;
                }
                #endregion

                h.Applications.Single(k => k.TypeName == "Login").With(
                    app =>
                    {
                        var html = XElement.Parse(app.PageSource);

                        html.Add(
                            new XElement("script",
                                new XAttribute("src", "/login/view-source"),
                                " "
                            )
                        );

                        h.Context.Response.Write(html.ToString());
                        h.CompleteRequest();
                    }
                );
            }
            #endregion



            #region /dashboard



            h.Context.Request.Cookies["session"].With(
                session_cookie =>
                {
                    // does that cookie even exist?

                    Console.WriteLine("are you really logged in?");


                    this.account.SelectByCookie(
                        new MyAccountQueries.SelectByCookie { cookie = session_cookie.Value },
                        r =>
                        {
                            Console.WriteLine("ok, you should see the dashboard...");

                            long id = r.id;
                            string email = r.email;

                            if (h.IsDefaultPath)
                            {
                                h.Context.Response.Redirect("/dashboard");
                                h.CompleteRequest();
                            }
                            else if (h.Context.Request.Path == "/dashboard")
                            {
                                h.Applications.Single(k => k.TypeName == "Dashboard").With(
                                     app =>
                                     {
                                         Console.WriteLine("ok, you should see the dashboard... here it is...");

                                         var html = XElement.Parse(app.PageSource);

                                         html.Add(
                                             new XElement("script",
                                                 new XAttribute("src", "/dashboard/view-source"),
                                                 " "
                                             )
                                         );

                                         h.Context.Response.Write(html.ToString());
                                         h.CompleteRequest();
                                     }
                                 );
                            }

                        }
                    );



                }
            );

            #endregion
        }



    }


}
