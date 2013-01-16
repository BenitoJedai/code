using android.content;
using android.hardware;
using JellyworldExperiment.Library;
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

namespace com.abstractatech.gamification.jwe
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService
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
            
            var Accepts = h.Context.Request.Headers["Accept"];

            if (Accepts != null)
                if (Accepts.Contains(EventSourceGenerator.ContentType))
                {
                    // handled in the other handler?
                    return;
                }

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



            var apps = new Dictionary<string, string> 
            {
                {"", "Application"},
                {"HardwareDetection", "__HardwareDetection"},
                {"DualViewWithCamera", "__WithCamera"},

                {"__Templates", "__Templates"}
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
                h.WriteSource(app);

                //h.Context.Response.ContentType = "text/javascript";

                //h.WriteSource(app);

                //// Accept-Encoding: gzip,deflate,sdch
                //foreach (var item in app.References)
                //{
                //    h.Context.Response.Write("/* " + new { item.AssemblyFile, bytes = 1 } + " */\r\n");
                //}

                //foreach (var item in app.References)
                //{
                //    // asp.net needs absolute paths
                //    h.Context.Response.WriteFile("/" + item.AssemblyFile + ".js");
                //}


                //h.CompleteRequest();
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
