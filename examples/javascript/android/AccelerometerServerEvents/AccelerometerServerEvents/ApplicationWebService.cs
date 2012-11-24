using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;

namespace AccelerometerServerEvents
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
            // http://www.sitepoint.com/server-sent-events/
            //Object '/d07dea9a_2384_49f8_8c01_270582d093dc/rwqwygw27obp9zsviyqgjhqt_27.rem' has been disconnected or does not exist at the server.

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

                    Action<double, double, double> vec3 =
                        (x, y, z) =>
                        {
                            if (qx == x)
                                if (qy == y)
                                    if (qz == z)
                                        return;

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

                        vec3(p.X, p.Y, 0);
                        Thread.Sleep(1000 / 30);
                    }
#endif



                    h.Context.Response.Write("retry: 1\n\n");
                    h.Context.Response.Flush();

                    h.CompleteRequest();
                    return;
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
