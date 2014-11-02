using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace WebWorkerExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public  class ApplicationWebService
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





        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
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

            var Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":");

            var path = h.Context.Request.Path;

            System.Console.WriteLine(
                new
                {
                    AuthorizationLiteralCredentials,
                    Host,
                    //h.Context.Request.UserHostAddress,
                }.ToString());

            #region /w
            var a = h.Applications.FirstOrDefault(k => k.TypeName == "w");

            if (h.Context.Request.Path == "/view-source/w")
            {
                var OK = false;


                OK = true;



                if (OK)
                {

                    h.Context.Response.ContentType = "text/javascript";
                    h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");


                    //Implementation not found for type import :
                    //type: System.Web.HttpResponse
                    //method: Void AppendCookie(System.Web.HttpCookie)
                    // not working on android?
                    h.Context.Response.SetCookie(
                        new HttpCookie("foo", "bar")
                    );

                    h.WriteSource(a);
                    h.CompleteRequest();
                    return;
                }

                h.Context.Response.StatusCode = 401;
                h.Context.Response.AddHeader(
                    "WWW-Authenticate",
                    "Basic realm=\"Android\""
                );

                // flush?
                h.Context.Response.Write(" ");
                h.CompleteRequest();

                return;
            }
            #endregion


        }

    }
}
