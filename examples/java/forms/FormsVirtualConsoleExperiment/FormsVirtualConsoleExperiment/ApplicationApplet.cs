using java.applet;
using ScriptCoreLib.Java.Extensions;
using System;
using System.IO;
using System.Text;

namespace FormsVirtualConsoleExperiment
{
    public sealed class ApplicationApplet : Applet
    {
        public readonly ApplicationControl content = new ApplicationControl();

        public override void init()
        {
            content.AttachTo(this);
            content.AutoSizeTo(this);
            this.EnableVisualStyles();
        }


        #region InitializeConsoleFormWriter
        class __OutWriter : TextWriter
        {
            public Action<string> AtWrite;
            public Action<string> AtWriteLine;

            public override void Write(string value)
            {
                AtWrite(value);
            }

            public override void WriteLine(string value)
            {
                AtWriteLine(value);
            }

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        public void InitializeConsoleFormWriter(
            Action<string> Console_Write,
            Action<string> Console_WriteLine
        )
        {
            var w = new __OutWriter();

            var o = Console.Out;

            var __reentry = false;

            w.AtWrite =
                x =>
                {
                    o.Write(x);

                    if (!__reentry)
                    {
                        __reentry = true;
                        Console_Write(x);
                        __reentry = false;
                    }
                };

            w.AtWriteLine =
                x =>
                {
                    o.WriteLine(x);

                    if (!__reentry)
                    {
                        __reentry = true;
                        Console_WriteLine(x);
                        __reentry = false;
                    }
                };

            Console.SetOut(w);
        }
        #endregion


        public void WhenReady(Action y)
        {
            y();
        }
    }
}
