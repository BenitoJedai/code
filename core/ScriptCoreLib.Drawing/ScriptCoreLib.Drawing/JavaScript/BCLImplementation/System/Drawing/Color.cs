using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Color))]
    internal class __Color
    {
        // link with
        // X:\jsc.svn\core\ScriptCoreLibJava.Drawing\ScriptCoreLibJava.Drawing\BCLImplementation\System\Drawing\Color.cs

        public Shared.Drawing.Color Value;


        public byte R
        {
            get { return (byte)Value.R; }
        }

        public byte G
        {
            get { return (byte)Value.G; }
        }

        public byte B
        {
            get { return (byte)Value.B; }
        }


        #region operators
        static public implicit operator Color(__Color e)
        {
            return (Color)(object)e;
        }

        static public implicit operator __Color(Color e)
        {
            return (__Color)(object)e;
        }

        static public implicit operator __Color(int e)
        {
            return new __Color { Value = e };
        }

        static public implicit operator __Color(global::ScriptCoreLib.Shared.Drawing.Color e)
        {
            return new __Color { Value = e };
        }
        #endregion



        public int ToArgb()
        {
            var e = this;

            return e.B + (e.G << 8) + (e.R << 16);
        }

        // [static System.Drawing.Color.FromArgb(System.Int32, System.Int32, System.Int32)
        public static Color FromArgb(int red, int green, int blue)
        {
            return new __Color { Value = Shared.Drawing.Color.FromRGB(red, green, blue) };
        }

        public static Color FromArgb(int alpha, int red, int green, int blue)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\Forms\FormsGridCellStyle\FormsGridCellStyle\Application.cs

            return new __Color
            {
                Value = Shared.Drawing.Color.FromKnownName(
                    "rgba(" + red + ", " + green + ", " + blue + ", " + (alpha / 255.0) + ")"
                )
            };
        }

        static __Color()
        {

        }

        static public readonly __Color Empty = ScriptCoreLib.Shared.Drawing.Color.None;

        static public __Color Lime { get { return 0x00ff00; } }
        static public __Color Green { get { return 0x00ff00; } }
        static public __Color Red { get { return 0xff0000; } }
        static public __Color Yellow { get { return Shared.Drawing.Color.Yellow; } }
        static public __Color Blue { get { return 0x0000ff; } }
        static public __Color Black { get { return 0x000000; } }
        static public __Color White { get { return 0xffffff; } }
        static public __Color Transparent { get { return Shared.Drawing.Color.Transparent; } }
        static public __Color Navy { get { return 0x000080; } }
        static public __Color Silver { get { return 0xC0C0C0; } }
        static public __Color Gray { get { return 0x808080; } }

        // http://simple.wikipedia.org/wiki/Teal_(color)
        static public __Color Teal { get { return 0x008080; } }



        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
