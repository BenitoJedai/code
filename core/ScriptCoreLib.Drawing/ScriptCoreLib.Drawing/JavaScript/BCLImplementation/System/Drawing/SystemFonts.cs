using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{

    [Script(Implements = typeof(global::System.Drawing.SystemFonts))]
    internal class __SystemFonts
    {

        public static Font DefaultFont { get { return new __Font { KnownName = "" }; } }
        public static Font CaptionFont { get { return new __Font { KnownName = "caption" }; } }
        public static Font IconTitleFont { get { return new __Font { KnownName = "icon" }; } }
        public static Font MenuFont { get { return new __Font { KnownName = "menu" }; } }
        public static Font MessageBoxFont { get { return new __Font { KnownName = "message-box" }; } }
        public static Font SmallCaptionFont { get { return new __Font { KnownName = "small-caption" }; } }
        public static Font StatusFont { get { return new __Font { KnownName = "status-bar" }; } }


    }
}
