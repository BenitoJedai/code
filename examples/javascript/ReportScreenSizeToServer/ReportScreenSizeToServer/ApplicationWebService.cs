using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReportScreenSizeToServer
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public int ScreenWidth;
        public int ScreenHeight;

        public void ScreenChanged()
        {
            // { ScreenWidth = 962, ScreenHeight = 553 }

            Console.WriteLine(new { ScreenWidth, ScreenHeight });
        }
    }
}
