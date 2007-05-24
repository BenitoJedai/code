using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.SystemColors))]
    internal class __SystemColors
    {
        static __SystemColors()
        {
            ButtonFace = new __Color { Value = Shared.Drawing.Color.System.ButtonFace };
        }

        static public __Color ButtonFace { get; set; }
    }
}
