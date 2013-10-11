using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace IdentityTokenFromWebService
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // GuardTime? :P
        public int IdentityToken;
        public int ContextIdentityToken;

        public ApplicationWebService()
        {
            IdentityToken = new Random().Next();

            // should jsc reset IdentityToken to whatever the client
            // thinks it has?
            // only if the data is signed by us?
        }

#if future
        public TaskAwaiter<ApplicationWebService> GetAwaiter()
        {
            // http://stackoverflow.com/questions/18765251/how-do-i-use-a-custom-taskawaiter-for-tasks-on-net-4-5

            return new ApplicationWebService { }.ToTaskResult().GetAwaiter();
        }
#endif

        public async Task<ApplicationWebService> yield()
        {
            // has the field overwritten?
            //return this;

            var ContextIdentityToken = this.IdentityToken;

            IdentityToken = new Random().Next();

            // return clean state
            return new ApplicationWebService { ContextIdentityToken = ContextIdentityToken };
        }

        public void Handler(WebServiceHandler h)
        {
            if (h.Context.Request.Path == "/view-source")
            {

                // the client is interesed in code
                // lets also pass on the signed default fields for any serviceit uses
                // lets start by one
                var c = new HttpCookie("_fields");

                // fields
                //IdentityToken=107584385
                //192.168.43.252
                ///
                //Session
                //30
                c["IdentityToken"] = "" + this.IdentityToken;
                c["foo"] = "bar";

                //                Set-Cookie:_fields=IdentityToken=1783346370&foo=bar; path=/
                //X-AspNet-Version:4.0.30319
                //X-Reference-0:ScriptCoreLib.dll.js 1345580
                //X-Reference-1:IdentityTokenFromWebService.Application.exe.js 80656

                h.Context.Response.AppendCookie(c);
                h.Context.Response.AppendCookie(new HttpCookie("xx", "yy"));
            }
        }
    }
}
