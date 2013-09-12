using android.content;
using android.hardware;
using java.net;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace AndroidToChromeShakeNotification
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
        public void DoActivateDetection(string e, Action<string> yield)
        {
            double qx = 0.0, qy = 0.0, qz = 0.0;
            var sw = new Stopwatch();



            #region Send
            int c = 0;
            Action<string, string, string, string> Send = (string reason, string data, string preview, string nn) =>
            {
                /// http://www.daniweb.com/software-development/java/threads/424998/udp-client-server-in-java

                c++;

                //var n = c + " hello world";
                var message =
                    new XElement("string",
                        new XAttribute("reason", reason),
                        new XAttribute("c", "" + c),
                        new XAttribute("preview", preview),
                        new XAttribute("n", nn),
                        data
                    ).ToString();

                Console.WriteLine(new { message });

                new Thread(
                    delegate()
                    {
                        try
                        {
                            var socket = new DatagramSocket(); //construct a datagram socket and binds it to the available port and the localhos
                            byte[] b = Encoding.UTF8.GetBytes(message.ToString());    //creates a variable b of type byte
                            var dgram = new DatagramPacket((sbyte[])(object)b, b.Length, InetAddress.getByName("239.1.2.3"), 40404);//sends the packet details, length of the packet,destination address and the port number as parameters to the DatagramPacket  

                            socket.send(dgram); //send the datagram packet from this port
                        }
                        catch
                        {
                            System.Console.WriteLine("server error");
                        }
                    }
                )
                {

                    Name = "server"
                }.Start();
            };
            #endregion

            var FilterToShakePerSecond = new Stopwatch();

            // skip the first 1 sec...
            FilterToShakePerSecond.Start();

            //0001 0200003d AndroidToChromeShakeNotification.ApplicationWebService
            //script: error JSC1000: Java : unable to emit newobj at 'AndroidToChromeShakeNotification.ApplicationWebService+<>c__DisplayClass6.<DoActivateDetection>b__2'#0044: multiple stack entries instead of one
            //   at jsc.ILFlowStackItem.get_SingleStackInstruction() in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILFlow.cs:line 131
            //   at jsc.Script.CompilerCLike.WriteParameters(Prestatement p, MethodBase _method, ILFlowStackItem[] s, Int32 offset, ParameterInfo[] pi, Boolean pWritten, String op) in x:\jsc.internal.svn\compiler\jsc\Languages\CompilerCLike.cs:line 291
            //   at jsc.Script.CompilerCLike.WriteParameterInfoFromStack(MethodBase m, Prestatement p, ILFlowStackItem[] s, Int32 offset) in x:\jsc.internal.svn\compiler\jsc\Languages\CompilerCLike.cs:line 231
            //   at jsc.Languages.Java.JavaCompiler.<CreateInstructionHandlers>b__16a(CodeEmitArgs e) in x:\jsc.internal.svn\compiler\jsc\Languages\Java\JavaCompiler.OpCodes.cs:line 2124

            #region vec3
            Action<float, float, float> vec3 =
                (x, y, z) =>
                {
                    if (qx == x)
                        if (qy == y)
                            if (qz == z)
                                return;

                    //Console.WriteLine(new { x, y, z });

                    var shake =
                        Math.Abs(qx - x)
                        + Math.Abs(qy - y)
                        + Math.Abs(qz - z);

                    if (sw.IsRunning)
                    {
                        // lets wai for a pretty strong shake
                        if (shake >= 2.2)
                        {
                            Console.WriteLine(new { sw.ElapsedMilliseconds, shake });


                            //Implementation not found for type import :
                            //type: System.Diagnostics.Stopwatch
                            //method: Void Reset()
                            //Did you forget to add the [Script] attribute?
                            //Please double check the signature!

                            if (FilterToShakePerSecond.ElapsedMilliseconds > 1500)
                                FilterToShakePerSecond = new Stopwatch();

                            if (!FilterToShakePerSecond.IsRunning)
                            {
                                // send one without image too...
                                Send(
                                    "shake",
                                    "Visit me at 127.0.0.1:80",
                                    "",
                                    "foo.bar.shake"
                                );

                                FilterToShakePerSecond.Start();
                            }
                        }

                    }
                    else
                    {
                        sw.Start();
                    }

                    qx = x;
                    qy = y;
                    qz = z;


                };
            #endregion

            #region registerListener
            (ThreadLocalContextReference.CurrentContext.getSystemService(Context.SENSOR_SERVICE) as SensorManager).With(
                sensorManager =>
                {
                    var value = new MySensorEventListener { onaccelerometer = vec3 };

                    try
                    {
                        Console.WriteLine("registerListener");
                        sensorManager.registerListener(
                            value,
                            //sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER),
                            sensorManager.getDefaultSensor(Sensor.TYPE_ORIENTATION),
                            SensorManager.SENSOR_DELAY_NORMAL
                        );

                        //Thread.Sleep(10000);
                    }
                    catch
                    {
                        //Console.WriteLine("unregisterListener");
                        //sensorManager.unregisterListener(value);
                    }
                }
            );
            #endregion

        }

    }

    class MySensorEventListener : SensorEventListener
    {
        public Action<float, float, float> onaccelerometer;

        public void onAccuracyChanged(Sensor sensor, int accuracy)
        {

        }
        public void onSensorChanged(SensorEvent e)
        {

            // check sensor type
            //if (e.sensor.getType() == Sensor.TYPE_ACCELEROMETER)
            if (e.sensor.getType() == Sensor.TYPE_ORIENTATION)
            {
                //                values[0]: Azimuth - (the compass bearing east of magnetic north)
                //values[1]: Pitch, rotation around x-axis (is the phone leaning forward or back)
                //values[2]: Roll, rotation around y-axis (is the phone leaning over on its left or right side)

                // this is different than the browser device orientation.
                float x = e.values[0];
                float y = e.values[1];
                float z = e.values[2];

                if (onaccelerometer != null)
                    onaccelerometer(x, y, z);
            }
        }
    }

}
