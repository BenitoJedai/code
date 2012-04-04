using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace AsyncResearch
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        public void WebMethod2(string e, Action<string> y)
        {
            #region noticeable slow down so that windows will display (Not Responding)
            // http://technet.microsoft.com/en-us/library/cc978614.aspx
            // http://social.msdn.microsoft.com/Forums/en-US/vsdebug/thread/688e4769-9c84-4826-9b01-9e1af0bdeb67/
            // debugger cannot be present
            // ctrl+F5 to run without debugger.

            var i = DateTime.Now;


            while (true)
            {
                Console.Write(".");

                var cpu_burn = DateTime.Now;

                if ((cpu_burn - i).TotalSeconds > 10)
                    break;
            }

            #endregion


            // Send it back to the caller.
            y("s: " + e);
        }


        public async void WebMethod4(string e, Action<string> y)
        {
            // ah this is like checked exceptions
            // we can only build on top of already existing ones.
            // ScriptCoreLib needs to provide some then!

            // if the caller did not use "await" then they will continue now.
            await 10.Seconds();

            // Send it back to the caller.
            y("s: " + e);
        }

        public async Task WebMethod8(string e, Action<string> y)
        {
            // see also: http://www.i-programmer.info/programming/c/1514-async-await-and-the-ui-problem.html

            await 10.Seconds();

            // Send it back to the caller.
            y("s: " + e);

            // see -- no "return" here!!
        }

        public async Task<int> WebMethod16(string e, Action<string> y)
        {
            // see also: http://www.i-programmer.info/programming/c/1514-async-await-and-the-ui-problem.html

            await 10.Seconds();

            // Send it back to the caller.
            y("s: " + e);

            // we have our anwser!
            return 42;
        }

    }

    public static class Test
    {
        // see: https://msmvps.com/blogs/jon_skeet/archive/2010/10/31/c-5-async-experimenting-with-member-resolution-getawaiter-beginawait-endawait.aspx

        public static TimeSpan Seconds(this int value)
        {

            return TimeSpan.FromSeconds(value);

        }

        public static TaskAwaiter GetAwaiter(this TimeSpan timespan)
        {

            return Task.Delay(timespan).GetAwaiter();

        }

        public static TaskAwaiter GetAwaiter(this DateTime date)
        {

            return (date - DateTime.Now).GetAwaiter();

        }

    }
}
