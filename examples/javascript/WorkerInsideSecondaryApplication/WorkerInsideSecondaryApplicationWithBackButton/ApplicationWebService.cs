using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WorkerInsideSecondaryApplicationWithBackButton
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

            #region /a


            var ApplicationTypeName = h.Context.Request.Headers["X-Application"];

            Console.WriteLine(new { ApplicationTypeName });
            var Application = h.Applications.FirstOrDefault(k => k.TypeName == ApplicationTypeName);

            if (Application != null)
            {
                #region OK
                var OK = false; // chrome webview cannot do 401 unless provided


                if (Host == h.Context.Request.UserHostAddress)
                    OK = true;

                if (!string.IsNullOrEmpty(AuthorizationLiteralCredentials.user))
                    if (!string.IsNullOrEmpty(AuthorizationLiteralCredentials.password))
                        OK = true;
                #endregion

                if (OK)
                {
                    Application.DiagnosticsMakeItSlowAndAddSalt = true;

                    // { Name = x, loaded = 762880, total = 0 } 
                    h.WriteSource(Application);

                    h.CompleteRequest();
                    return;
                }


                #region 401
                h.Context.Response.StatusCode = 401;
                h.Context.Response.AddHeader(
                    "WWW-Authenticate",
                    "Basic realm=\"Android\""
                );

                // flush?
                h.Context.Response.Write(" ");
                h.CompleteRequest();
                #endregion

                return;
            }
            #endregion



        }

    }
}
