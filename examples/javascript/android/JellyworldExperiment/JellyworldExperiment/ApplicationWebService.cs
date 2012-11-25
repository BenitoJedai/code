using android.content;
using android.hardware;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;

namespace JellyworldExperiment
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
            // Send it back to the caller.
            y(e);
        }


        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            var path = h.Context.Request.Path;

            #region undo asp.net
            if (path == "/default.htm")
                path = "/";
            #endregion

            #region Diagnostics /jsc
            if (h.Context.Request.Path == "/jsc")
            {
                h.Diagnostics();
                h.CompleteRequest();

                return;
            }
            #endregion


            #region text/event-stream
            // http://www.w3.org/Protocols/HTTP/HTRQ_Headers.html
            var Accepts = h.Context.Request.Headers["Accept"];

            if (Accepts != null)
                if (Accepts.Contains("text/event-stream"))
                {
                    h.Context.Response.ContentType = "text/event-stream";

                    Action<XElement> data =
                        xml =>
                        {
                            h.Context.Response.Write("data: " + xml.ToString() + "\n\n");
                            h.Context.Response.Flush();
                        };

                    double qx = 0.0, qy = 0.0, qz = 0.0;

                    Action<float, float, float> vec3 =
                        (x, y, z) =>
                        {
                            if (qx == x)
                                if (qy == y)
                                    if (qz == z)
                                        return;

                            //Console.WriteLine(new { x, y, z });

                            qx = x;
                            qy = y;
                            qz = z;

                            data(
                                new XElement("onaccelerometer",
                                    new XAttribute("x", "" + x),
                                    new XAttribute("y", "" + y),
                                    new XAttribute("z", "" + z)
                                )
                            );
                        };


#if DEBUG
                    for (int i = 0; i < 32; i++)
                    {
                        // http://stackoverflow.com/questions/1316681/getting-mouse-position-in-c-sharp
                        var p = user32.GetCursorPosition();

                        vec3(0, -(p.Y - 200), -p.X);
                        Thread.Sleep(1000 / 30);
                    }
#endif

#if Android

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
                                    SensorManager.SENSOR_DELAY_GAME
                                );

                                Thread.Sleep(10000);
                            }
                            finally
                            {
                                Console.WriteLine("unregisterListener");
                                sensorManager.unregisterListener(value);
                            }
                        }
                    );

#endif



                    h.Context.Response.Write("retry: 1\n\n");
                    h.Context.Response.Flush();

                    h.CompleteRequest();
                    return;
                }
            #endregion


            var apps = new Dictionary<string, string> 
            {
                {"", "Application"},
                {"HardwareDetection", "Application_HardwareDetection"},
                {"DualViewWithCamera", "Application_DualViewWithCamera"},

                

            };

            var appname = apps[""];

            var appsrc = "/view-source";
            var apppath = path.SkipUntilIfAny("/").TakeUntilIfAny("/view-source");

            if (!string.IsNullOrEmpty(apppath))
                if (apps.ContainsKey(apppath))
                {
                    appname = apps[apppath];
                    appsrc = "/" + apppath + appsrc;
                }

            var app = h.Applications.Single(k => k.TypeName == appname);

            #region /view-source

            var IsViewSource = h.Context.Request.Path.EndsWith("/view-source");
            if (IsViewSource)
            {
                h.Context.Response.ContentType = "text/javascript";

                // Accept-Encoding: gzip,deflate,sdch
                foreach (var item in app.References)
                {
                    h.Context.Response.Write("/* " + new { item.AssemblyFile, bytes = 1 } + " */\r\n");
                }

                foreach (var item in app.References)
                {
                    // asp.net needs absolute paths
                    h.Context.Response.WriteFile("/" + item.AssemblyFile + ".js");
                }


                h.CompleteRequest();
                return;
            }
            #endregion

            #region text/html
            h.Context.Response.ContentType = "text/html";

            var xxml = XElement.Parse(app.PageSource);


            xxml.Add(
                new XElement("script",
                    new XAttribute("src", appsrc),

                    // android otherwise closes the tag?
                    " "
                )
            );

            h.Context.Response.Write(xxml.ToString());

            h.CompleteRequest();
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

    static class user32
    {
        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            //bool success = User32.GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }
    }

}
