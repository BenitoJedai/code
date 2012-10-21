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

            if (!string.IsNullOrEmpty(AuthorizationLiteralCredentials.user))
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
                h.CompleteRequest();
                return;
            }

            h.Context.Response.StatusCode = 401;
            h.Context.Response.AddHeader(
                "WWW-Authenticate",
                "Basic realm=\"foo@jsc-solutions.net\""
            );


            // android flush headers?
            h.Context.Response.Write("");

            //h.Context.Response.Write("http://en.wikipedia.org/wiki/Basic_access_authentication");
            h.CompleteRequest();
        }
    }
}
