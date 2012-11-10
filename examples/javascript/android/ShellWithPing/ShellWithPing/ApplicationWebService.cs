using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace ShellWithPing
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component, PING
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

        // jsc cannot see explicit interfaces just yet.
        public void PING_InvokeAsync(string host, Action<string> y)
        {
            Thread.Sleep(500);

            y("simulated ping to [" + host + "] complete.");
        }

        public void EchoAsync(string e, Action<string> y)
        {
            if (e == "jsc")
            {
                y("What do you want to create today?");
                y("create.jsc-solutions.net");
                return;
            }

            if (e.StartsWith("ping "))
            {
                PING_InvokeAsync(e.SkipUntilOrEmpty("ping "), y);
                return;
            }
            y("echo: " + e);
        }
    }

    interface PING
    {
        void PING_InvokeAsync(string host, Action<string> y);
    }
}
