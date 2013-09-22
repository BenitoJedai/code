using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ButterFlyWithInteractiveInt32Offset
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript. JSC supports string data type for all platforms.</param>
        /// <param name="y">A callback to javascript. In the future all platforms will allow Action&lt;XElementConvertable&gt; delegates.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }


        public void File_ReadLine(string CallerFilePath, string CallerLineNumber, Action<string> y)
        {
            Console.WriteLine(new { CallerFilePath, CallerLineNumber });

            y(File.ReadAllLines(CallerFilePath)[int.Parse(CallerLineNumber) - 1]);

        }

        public void File_WriteLine(
            string CallerFilePath,
            string CallerLineNumber,

            string value, Action<string> y)
        {
            Console.WriteLine(new { CallerFilePath, CallerLineNumber, value });
            var Lines = File.ReadAllLines(CallerFilePath);

            Lines[int.Parse(CallerLineNumber) - 1] = value;

            File.WriteAllLines(CallerFilePath, Lines);


        }
    }
}
