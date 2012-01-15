using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;

namespace ArduinoSpiderControlCenter
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
        public void WebMethod2(string e, StringAction y)
        {
            // Send it back to the caller.
            var x = COM46.Line;

            y(x);
        }

    }

    public static class COM46
    {
        public static string Line = "?";

        static COM46()
        {
            Line = "waiting...";

            var ports = SerialPort.GetPortNames();

            // http://binglongx.wordpress.com/2011/10/26/arduino-serial-port-communication/
            // Arduino Serial uses: Data_bits=8, Stop_bits=1, Parity=None, Flow_control=None


            var s = new SerialPort(
                portName: "COM46",
                baudRate: 9600,
                parity: Parity.None,
                dataBits: 8,
                stopBits: StopBits.One
            );

            s.Open();

            var i = 0;

            new Thread(
                delegate()
                {
                    while (true)
                    {
                        i++;
                        COM46.Line = s.ReadLine();
                    }
                }
            ).Start();


            //StartConsoleMonitoring();

        }

        private static void StartConsoleMonitoring()
        {
            new Thread(
           delegate()
           {

               while (true)
               {

                   var cache = new Dictionary<string, string>();

                   COM46.Line.Split(';').WithEach(
                       u =>
                       {
                           u.TakeUntilOrEmpty(":").Trim().With(
                               key =>
                               {
                                   var value = u.SkipUntilOrEmpty(":").Trim();

                                   // 1024 is dark



                                   if (key == "RightLR")
                                   {
                                       Console.ForegroundColor = ConsoleColor.Yellow;


                                       try
                                       {
                                           var ivalue = int.Parse(value);

                                           Console.Beep(300 + 1200 - ivalue, 500);
                                       }
                                       catch
                                       {
                                       }

                                   }
                                   else
                                       Console.ForegroundColor = ConsoleColor.Gray;

                                   Console.Write("\t" + key + "\t" + value);
                               }
                           );
                       }
                   );

                   Console.WriteLine();
               }

           }
       ).Start();
        }
    }
}
