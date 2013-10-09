using FormsConfiguredAtWebService.Library;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FormsConfiguredAtWebService
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
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


        public async Task<string> SpecialMessage()
        {
            return "hi from server";
        }

        public async Task<Goo> CreateServerGoo()
        {
            return new Goo
            {
                GooTitle = "foo",
                GooButtonMessage = "foo message",


                // a new copy
                service = new ApplicationWebService()
            };
        }

        // jsc should also allow static methods
        // non void non Task methods should have a copy on the client side unless they are extension methods?
    }
}
