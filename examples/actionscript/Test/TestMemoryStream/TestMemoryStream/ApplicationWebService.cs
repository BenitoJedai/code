using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestMemoryStream
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
            var m = new MemoryStream();

            m.WriteByte((byte)'H');
            m.WriteByte((byte)'E');
            m.WriteByte((byte)'L');
            m.WriteByte((byte)'L');
            m.WriteByte((byte)'O');

            var a = m.ToArray();


            var w = new StringBuilder();

            foreach (var item in a)
            {
                w.Append(item.ToString("x2"));
            }

            w.Append(", " + Convert.ToBase64String(a));

            // {48454c4c4f, SEVMTE8=}
            e = w.ToString();

            // Send it back to the caller.
            y(e);
        }

    }
}
