using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AuthenticationExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        public void Handler(WebServiceHandler h)
        {
            // SSL certifactes seem way more likely useful.

            var HostUri = new
            {
                Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                Port = h.Context.Request.Headers["Host"].SkipUntilIfAny(":")
            };
#if DEBUG
            Console.WriteLine(h.Context.Request.HttpMethod + " " + h.Context.Request.Path);

            h.Context.Request.Headers.AllKeys.WithEach(
                k => Console.WriteLine(k + ": " + h.Context.Request.Headers[k])
            );
#endif

            // http://tools.ietf.org/html/rfc2617#section-3.2.1

            var Authorization = h.Context.Request.Headers["Authorization"];

            var AuthorizationLiteralEncoded = Authorization.SkipUntilOrEmpty("Basic ");
            var AuthorizationLiteral = Encoding.ASCII.GetString(
                Convert.FromBase64String(AuthorizationLiteralEncoded)
            );

            var AuthorizationLiteralCredentials = new
            {
                user = AuthorizationLiteral.TakeUntilOrEmpty(":"),
                password = AuthorizationLiteral.SkipUntilOrEmpty(":"),
            };

            Console.WriteLine(new { AuthorizationLiteralCredentials }.ToString());

            Action AlternativeCredentials =
                delegate
                {
                    h.Context.Response.Write(
                  new XElement("body",
                      new XElement("pre",
                          new { AuthorizationLiteralCredentials }
                      ),

                              new XElement("hr"),
                      new XElement("a", new XAttribute("href",
                          "/login"),
                          "/login"
                      ),

                           new XElement("hr"),
                      new XElement("a", new XAttribute("href",
                          "/secure"),
                          "/secure"
                      ),
                      new XElement("a", new XAttribute("href",
                          "/secure-foo"),
                          "/secure-foo"
                      ),
                      new XElement("hr"),
                      new XElement("a", new XAttribute("href",
                          "//xoo:zar@" + HostUri.Host + ":" + HostUri.Port + "/secure"),
                          "//xoo:zar@" + HostUri.Host + ":" + HostUri.Port + "/secure"
                      ),
                        new XElement("hr"),
                          new XElement("a", new XAttribute("href",
                              "//yoo:yar@" + HostUri.Host + ":" + HostUri.Port + "/secure"),
                              "//yoo:yar@" + HostUri.Host + ":" + HostUri.Port + "/secure"
                          ),
                        new XElement("hr"),
                          new XElement("a", new XAttribute("href",
                              "//zoo:@" + HostUri.Host + ":" + HostUri.Port + "/secure"),
                              "//zoo:@" + HostUri.Host + ":" + HostUri.Port + "/secure"
                          ),
                          new XElement("hr"),
                          new XElement("a", new XAttribute("href",
                              "/logout"),
                              "/logout"
                          )
                      )
              );
                };

            if (h.IsDefaultPath)
            {
                AlternativeCredentials();
                h.CompleteRequest();
                return;
            }

            if (h.Context.Request.Path == "/login")
            {
                h.Context.Response.AddHeader("Refresh", "1;url=/secure");
                h.Context.Response.Write(
                   new XElement("body",
                       new XElement("h1", "Hey!")
                     )
                );

                h.CompleteRequest();

                return;
            }

            if (h.Context.Request.Path == "/logout")
            {
                h.Context.Response.AddHeader("Refresh", "1;url=//logout:@" + HostUri.Host + ":" + HostUri.Port + "/godspeed");
                h.Context.Response.Write(
                   new XElement("body",
                       new XElement("h1", "Bye!")
                     )
                );

                h.CompleteRequest();

                return;
            }

            if (h.Context.Request.Path == "/godspeed")
            {
                h.Context.Response.AddHeader("Refresh", "4;url=/");
                AlternativeCredentials();
                h.Context.Response.Write(
                   new XElement("body",
                       new XElement("h1", "Godspeed!")
                     )
                );

                h.CompleteRequest();

                return;
            }


            if (h.Context.Request.Path == "/jsc")
            {
                h.Diagnostics();
                h.CompleteRequest();

                return;
            }





            if (!string.IsNullOrEmpty(AuthorizationLiteralCredentials.user))
                if (!string.IsNullOrEmpty(AuthorizationLiteralCredentials.password))
                {
                    var xml = XElement.Parse(global::AuthenticationExperiment.HTML.Pages.DefaultPageSource.Text);

#if DEBUG
                    // linq for andoid? when can we have it?

                    xml.Descendants("data-user").ReplaceContentWith(AuthorizationLiteralCredentials.user);
                    xml.Descendants("data-password").ReplaceContentWith(AuthorizationLiteralCredentials.password);
#endif
                    // what are the defalts on different platforms?
                    h.Context.Response.ContentType = "text/html";

                    h.Context.Response.Write(xml.ToString());


                    AlternativeCredentials();


                    h.CompleteRequest();
                    return;
                }

            h.Context.Response.StatusCode = 401;
            h.Context.Response.AddHeader(
                "WWW-Authenticate",
                "Basic realm=\"foo@jsc-solutions.net\""
            );

            h.Context.Response.AddHeader("Refresh", "4;url=/");

            //AlternativeCredentials();
            // android flush headers?
            //h.Context.Response.Write("");
            h.Context.Response.Write(
              new XElement("body",
                  new XElement("h1", "Have we met?"),
                  new XElement("hr"),
                  new XElement("a", new XAttribute("href", "/login"), "/login")


              )
           );

            //h.Context.Response.Write("http://en.wikipedia.org/wiki/Basic_access_authentication");
            h.CompleteRequest();
        }
    }
}
