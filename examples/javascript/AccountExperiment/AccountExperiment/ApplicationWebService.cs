using AccountExperiment.Schema;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
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
        static AccountExperiment.Schema.MyAccountQueries.Insert ref0;
        static AccountExperiment.Schema.MySessionQueries.Insert ref1;

        public void Authenticate(
            string user,
            string password,

            Action<string> yield_session)
        {


            // create a new session audit object for us to use
            yield_session("foo");
        }

        // future versions will let client use this directly.
        public global::GravatarExperiment.ApplicationWebService gravatar = new GravatarExperiment.ApplicationWebService();


        public void gravatar_Gravatar(string e, Action<string> avatar, Action<string> profile)
        {
            gravatar.Gravatar(e, avatar, profile);
        }

        MyAccount account = new MyAccount();
        MySession session = new MySession();


        public void WhatsMyEmail(string session, Action<string> yield)
        {
            Console.WriteLine("WhatsMyEmail: " + new { session });

            account.SelectByCookie(
                new MyAccountQueries.SelectByCookie { cookie = session },
                r =>
                {
                    long id = r.id;
                    string email = r.email;

                    yield(email);

                }
            );
        }


        string CreateSession(long account, long ticks)
        {
            // should encrypt this
            var cookie = Convert.ToBase64String(Encoding.UTF8.GetBytes(new { ticks, account, comment = "we shall SHA1 this!" }.ToString())).TakeUntilIfAny("=");

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

            #region /login
            if (h.Context.Request.Path == "/login")
            {
                #region POST
                if (h.Context.Request.HttpMethod == "POST")
                {
                    // browser sending us the credentials.

                    var email = h.Context.Request.Form["email"];
                    var password = h.Context.Request.Form["password"];

                    Action yield = delegate
                    {
                        // slow it down!
                        Thread.Sleep(1000);

                        h.Context.Response.SetCookie(new System.Web.HttpCookie("message", "check credentials!"));


                        h.Context.Response.Redirect(h.Context.Request.UrlReferrer.ToString());
                        h.CompleteRequest();
                    };

                    account.SelectByPassword(
                        new MyAccountQueries.SelectByPassword { email = email, password = password },
                        id =>
                        {

                            yield = delegate
                            {
                                var now = DateTime.Now;
                                var ticks = now.Ticks;

                                var cookie = CreateSession(id, ticks);


                                h.Context.Response.SetCookie(new System.Web.HttpCookie("session", cookie));

                                h.Context.Response.Redirect(h.Context.Request.UrlReferrer.ToString().TakeUntilLastOrNull("/login"));
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
            var session_cookie = h.Context.Request.Cookies["session"];
            if (session_cookie != null)
            {
                if (h.IsDefaultPath)
                {
                    h.Applications.Single(k => k.TypeName == "Dashboard").With(
                        app =>
                        {
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
            else
            {
                Console.WriteLine(new { session_cookie, h.Context.Request.Path });
            }
            #endregion
        }

    }
}
