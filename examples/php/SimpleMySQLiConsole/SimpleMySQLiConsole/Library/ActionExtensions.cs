using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using System.IO;

namespace SimpleMySQLiConsole.Library
{
    public static class ActionExtensions
    {
        class ConsoleWriter : TextWriter
        {
            public Action<string> y;

            public override void Write(string value)
            {

            }

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }
        public static void ToConsoleOut(this Action<string> y)
        {
            var w = new ConsoleWriter { y = y };

            Console.SetOut(w);
        }
    }
}
