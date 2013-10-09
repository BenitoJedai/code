using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ButterFlyWithInteractiveInt32Offset
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {


        public async Task<string> File_ReadLine(string CallerFilePath, int CallerLineNumber)
        {
            Console.WriteLine(new { CallerFilePath, CallerLineNumber });

            return (File.ReadAllLines(CallerFilePath)[CallerLineNumber - 1]);

        }

        public void File_WriteLine(
            string CallerFilePath,
            int CallerLineNumber,

            string value)
        {
            Console.WriteLine(new { CallerFilePath, CallerLineNumber, value });
            var Lines = File.ReadAllLines(CallerFilePath);

            Lines[CallerLineNumber - 1] = value;

            File.WriteAllLines(CallerFilePath, Lines);


        }
    }
}
