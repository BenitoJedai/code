using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WebApplication1
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService : IDisposable
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



        public Stopwatch elapsed = Stopwatch.StartNew();
        public XElement button1 = new XElement("button", "hi carlo");

        //public int MyProperty { get; set; }

        //public event Action<string> yield;
        public Action<string> yield;

        public async Task onclick()
        {
            MessageBox.Show("you clicked me " + new { elapsed.ElapsedMilliseconds });

            yield("clicked at " + new { elapsed.ElapsedMilliseconds });
        }



        public void Dispose()
        {
            MessageBox.Show("you used me " + new { elapsed.ElapsedMilliseconds });
            Process.GetCurrentProcess().Kill();

        }
    }
}
