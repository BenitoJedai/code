using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestWebMethodIPAddress
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
    
        //[NFC, QR]
        public void WebMethod2(string e, Action<string> y)
        {
            Console.WriteLine("WebMethod2 " + new { h.Context.Request.UserHostAddress });

            // { UserHostAddress = 127.0.0.1, ClientCertificate = System.Web.HttpClientCertificate }

            // http://www.piotrwalat.net/client-certificate-authentication-in-asp-net-web-api-and-windows-store-apps/
            // http://stackoverflow.com/questions/10638272/microsoft-http-server-api-using-ssl-how-to-demand-client-certificate
            // http://www.ibm.com/developerworks/lotus/library/ls-SSL_client_authentication/
            // http://www.w3.org/TR/WebCryptoAPI/

            y(

                new
                {
                    // the only id we get?
                    h.Context.Request.UserHostAddress,

                    //h.Context.Request.ClientCertificate.Subject,
                    //h.Context.Request.ClientCertificate.Cookie,
                    h.Context.Request.ClientCertificate.IsPresent,
                    //h.Context.Request.ClientCertificate.Issuer,
                    //h.Context.Request.ClientCertificate.ValidUntil
                }.ToString()
            );
        }

        public ApplicationWebService()
        {
            Console.WriteLine("ApplicationWebService .ctor");
        }





        [Obsolete("experimental")]
        public WebServiceHandler h { set; get; }

        [Obsolete("experimental")]
        public /* will not be part of web service itself */ void Handler(WebServiceHandler handler)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.cs
            // http://support.microsoft.com/kb/186812

            //Accept client certificates 
            // http://en.wikipedia.org/wiki/HTTP_403
            // 403.7 - Client certificate required.

            // http://xhr.spec.whatwg.org/
            // http://www.derkeiler.com/Newsgroups/microsoft.public.inetserver.iis.security/2005-05/0021.html
            if (handler.WebMethod != null)
            {
                // http://blogs.msdn.com/b/friis/archive/2011/11/15/troubleshooting-403-7-client-certificate-required-errors-amp-step-by-step-to-make-sure-your-client-certificate-is-displayed-and-selected.aspx
                // https://code.google.com/p/chromium/issues/detail?id=261677
                // https://groups.google.com/forum/#!topic/chromium-discuss/PPD4O121ado
                // http://www.tbs-certificats.com/FAQ/en/installer_certificat_client_google_chrome.html
                // http://stackoverflow.com/questions/11285905/access-to-client-certificates-and-replacing-certificate-selection-dialog-in-chro

                Console.WriteLine("before " + new { handler.WebMethod.MethodName });

                // This operation requires IIS integrated pipeline mode.
                // HTTP status string is not valid.
                //handler.Context.Response.Status = "403 - Client certificate required";
                //handler.Context.Response.Status = "403 - Client certificate required";
                //handler.Context.Response.SubStatusCode = 7;
                //handler.Context.Response.StatusCode = 401;

                //handler.CompleteRequest();
            }
        }
    }
}
