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
        public void WebMethod2(string po, StringAction y)
        {
            // Send it back to the caller.
            COM46.po = int.Parse(po);
            var x = COM46.Line;
            COM46.Rx++;
            //Console.WriteLine("WebMethod2: #" + COM46.Rx + " @" + Thread.CurrentThread.ManagedThreadId);

            y(x);
        }

        public void AtFocus()
        {
            COM46.AtFocus();
        }

        public void AtBlur()
        {
            COM46.AtBlur();
        }
    }

    public static class COM46
    {
        public static int po = 0;
        public static int Rx;
        public static string Line = "not connected..";

        public static Action AtBlur = delegate { };
        public static Action AtFocus = delegate { };

        static void InitAtFocus()
        {
            AtFocus = delegate
            {
                Line = "connect...";

                var ports = SerialPort.GetPortNames();

                // http://binglongx.wordpress.com/2011/10/26/arduino-serial-port-communication/
                // Arduino Serial uses: Data_bits=8, Stop_bits=1, Parity=None, Flow_control=None
                var portName = "COM46";

                if (!ports.Contains(portName))
                {
                    Line = "no port...";

                    return;
                }

                AtFocus = delegate
                {
                };

                var s = new SerialPort(
                    portName: portName,
                    baudRate: 9600,
                    parity: Parity.None,
                    dataBits: 8,
                    stopBits: StopBits.One
                );

                // Access to the port 'COM46' is denied.
                s.Open();
                Console.Title = "Connect";

                var y = true;

                AtBlur = delegate
                {
                    Console.Title = "Disconnect";
                    y = false;
                    AtBlur = delegate
                    {

                    };
                };

                var i = 0;

                new Thread(
                    delegate()
                    {
                        int po_prev = 0;

                        try
                        {
                            while (y)
                            {
                                i++;

                                if (po_prev != po)
                                {
                                    po_prev = po;

                                    s.BaseStream.WriteByte((byte)po);
                                    s.BaseStream.Flush();
                                }

                                COM46.Line = s.ReadLine();
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            Console.Title = "Closed";
                            s.Close();

                            s.Dispose();
                        }

                        Line = "closed...";

                        InitAtFocus();
                    }
                ).Start();
            };

        }

        static COM46()
        {
            InitAtFocus();


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
