// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.Ultra.Library.Delegates;
using ScriptCoreLib.PHP;
using System.Diagnostics;

namespace PromotionApplicationP
{
 
    static class PHP
    {
        public static void Invoke(Action e)
        {
        }
    }

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript</param>
        /// <param name="y">A callback to javascript</param>
        public void WebMethod2(string e, StringAction y)
        {
            // Send it to the caller.
            y(e + GetArray()[0]);

            // this call will be omitted for non PHP platforms
            PHP.Invoke(PHPImplementation.Method1);
        }

     

        private static string[] GetArray()
        {
            var s = new[] { "jsc" };
            return s;
        }

    }

    class PHPImplementation
    {
        public static void Method1()
        {
            
            Native.API.chr(3);
        }
    }
}
