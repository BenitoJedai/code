using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System;
using System.IO;
using System.Text;

namespace VirtualConsoleExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            var t = new TextField
            {
                text = "click on me to see console",
                autoSize = TextFieldAutoSize.LEFT
            };

            t.MoveTo(16, 16);
            t.AttachTo(this);

            t.click +=
                delegate
                {
                    t.text += "\nclicked!";

                    Console.WriteLine("clicked in flash!");
                };

            this.AtInitializeConsoleFormWriter = (
                Action<string> Console_Write,
                Action<string> Console_WriteLine
            ) =>
            {
                t.appendText("\nAtInitializeConsoleFormWriter");

                try
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

                    Console.WriteLine("flash Console.WriteLine should now appear in JavaScript form!");
                    t.appendText("\nAtInitializeConsoleFormWriter done");
                }
                catch (Exception ex)
                {
                    t.appendText("\n error: " + new { ex, Console_Write, Console_WriteLine });

                }
            };

        }

        Action<Action<string>, Action<string>> AtInitializeConsoleFormWriter;


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
            AtInitializeConsoleFormWriter(Console_Write, Console_WriteLine);
        }
        #endregion

        public void WhenReady(Action y)
        {
            y();
        }

    }

}
