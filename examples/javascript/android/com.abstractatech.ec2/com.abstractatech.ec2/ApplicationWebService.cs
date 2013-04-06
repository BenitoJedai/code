using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace com.abstractatech.ec2
{
    sealed class __dictionary_definedbyidl
    {
        public string id;
        public string value;

        public dynamic extra;
    }

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        // what does static mean?
        // can we restore static values in db?
        static List<__dictionary_definedbyidl> data = new List<__dictionary_definedbyidl>();


        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            var c = new com.amazonaws.AmazonWebServiceClient();

            // Send it back to the caller.
            y(e);
        }

    }
}
