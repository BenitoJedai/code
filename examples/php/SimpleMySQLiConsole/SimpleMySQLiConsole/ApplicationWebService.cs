using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace SimpleMySQLiConsole
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/2012/20121217

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

    }

    [Script(IsNative = true)]
    class mysqli
    {
        // http://php.net/manual/en/mysqli.quickstart.prepared-statements.php
        // http://php.net/manual/en/mysqlinfo.concepts.buffering.php

        public mysqli(
            string host,
            string user,
            string password,
            string datasource
            )
        {

        }
    }
}
