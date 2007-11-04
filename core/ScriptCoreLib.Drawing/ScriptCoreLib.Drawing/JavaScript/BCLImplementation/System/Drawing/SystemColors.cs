using System;
using System.Collections.Generic;
using System.Text;

using MySystemColors = ScriptCoreLib.Shared.Drawing.Color.System;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    

    [Script(Implements = typeof(global::System.Drawing.SystemColors))]
    internal class __SystemColors
    {
        static __Color Get(ScriptCoreLib.Shared.Drawing.Color c)
        {
            return new __Color { Value = c };
        }

        static __SystemColors()
        {
            //Func<ScriptCoreLib.Shared.Drawing.Color, __Color> Get = 
            //    c => new __Color { Value = c };

            // http://www.w3.org/TR/css3-color/#css2-system

            __SystemColors.ActiveBorder = Get( MySystemColors.ActiveBorder );
            __SystemColors.ActiveCaption = Get( MySystemColors.ActiveCaption );
            __SystemColors.ActiveCaptionText = Get( MySystemColors.CaptionText );
            __SystemColors.AppWorkspace = Get( MySystemColors.AppWorkspace );
            __SystemColors.ButtonFace = Get( MySystemColors.ButtonFace );
            __SystemColors.ButtonHighlight = Get( MySystemColors.ButtonHighlight );
            __SystemColors.ButtonShadow = Get( MySystemColors.ButtonShadow );
            __SystemColors.Control = Get( MySystemColors.ThreeDFace );
            __SystemColors.ControlDark = Get( MySystemColors.ThreeDShadow );
            __SystemColors.ControlDarkDark = Get( MySystemColors.ThreeDDarkShadow);
            __SystemColors.ControlLight = Get( MySystemColors.ThreeDLightShadow );
            __SystemColors.ControlLightLight = Get( MySystemColors.ThreeDHighlight );
            __SystemColors.ControlText = Get( MySystemColors.ButtonText );
            __SystemColors.Desktop = Get( MySystemColors.Background );
            //__SystemColors.GradientActiveCaption = Get( MySystemColors.GradientActiveCaption );
            //__SystemColors.GradientInactiveCaption = Get( MySystemColors.GradientInactiveCaption );
            __SystemColors.GrayText = Get( MySystemColors.GrayText );
            __SystemColors.Highlight = Get( MySystemColors.Highlight );
            __SystemColors.HighlightText = Get( MySystemColors.HighlightText );
            //__SystemColors.HotTrack = Get( MySystemColors.HotTrack );
            __SystemColors.InactiveBorder = Get( MySystemColors.InactiveBorder );
            __SystemColors.InactiveCaption = Get( MySystemColors.InactiveCaption );
            __SystemColors.InactiveCaptionText = Get( MySystemColors.InactiveCaptionText );
            __SystemColors.Info = Get( MySystemColors.InfoBackground );
            __SystemColors.InfoText = Get( MySystemColors.InfoText );
            __SystemColors.Menu = Get( MySystemColors.Menu );
            //__SystemColors.MenuBar = Get( MySystemColors.MenuBar );
            //__SystemColors.MenuHighlight = Get( MySystemColors.MenuHighlight );
            __SystemColors.MenuText = Get( MySystemColors.MenuText );
            __SystemColors.ScrollBar = Get( MySystemColors.Scrollbar );
            __SystemColors.Window = Get( MySystemColors.Window );
            __SystemColors.WindowFrame = Get( MySystemColors.WindowFrame );
            __SystemColors.WindowText = Get( MySystemColors.WindowText );
        }



      // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the active window's
        //     border.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the active window's border.
        public static __Color ActiveBorder { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the background
        //     of the active window's title bar.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the active window's title bar.
        public static __Color ActiveCaption { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the text in the
        //     active window's title bar.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the text in the active window's
        //     title bar.
        public static __Color ActiveCaptionText { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the application
        //     workspace.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the application workspace.
        public static __Color AppWorkspace { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the face color of a 3-D element.
        //
        // Returns:
        //     A System.Drawing.Color that is the face color of a 3-D element.
        public static __Color ButtonFace { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the highlight color of a 3-D
        //     element.
        //
        // Returns:
        //     A System.Drawing.Color that is the highlight color of a 3-D element.
        public static __Color ButtonHighlight { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the shadow color of a 3-D element.
        //
        // Returns:
        //     A System.Drawing.Color that is the shadow color of a 3-D element.
        public static __Color ButtonShadow { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the face color of a 3-D element.
        //
        // Returns:
        //     A System.Drawing.Color that is the face color of a 3-D element.
        public static __Color Control { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the shadow color of a 3-D element.
        //
        // Returns:
        //     A System.Drawing.Color that is the shadow color of a 3-D element.
        public static __Color ControlDark { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the dark shadow color of a
        //     3-D element.
        //
        // Returns:
        //     A System.Drawing.Color that is the dark shadow color of a 3-D element.
        public static __Color ControlDarkDark { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the light color of a 3-D element.
        //
        // Returns:
        //     A System.Drawing.Color that is the light color of a 3-D element.
        public static __Color ControlLight { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the highlight color of a 3-D
        //     element.
        //
        // Returns:
        //     A System.Drawing.Color that is the highlight color of a 3-D element.
        public static __Color ControlLightLight { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of text in a 3-D
        //     element.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of text in a 3-D element.
        public static __Color ControlText { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the desktop.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the desktop.
        public static __Color Desktop { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the lightest color in the color
        //     gradient of an active window's title bar.
        //
        // Returns:
        //     A System.Drawing.Color that is the lightest color in the color gradient of
        //     an active window's title bar.
        public static __Color GradientActiveCaption { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the lightest color in the color
        //     gradient of an inactive window's title bar.
        //
        // Returns:
        //     A System.Drawing.Color that is the lightest color in the color gradient of
        //     an inactive window's title bar.
        public static __Color GradientInactiveCaption { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of dimmed text.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of dimmed text.
        public static __Color GrayText { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the background
        //     of selected items.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the background of selected items.
        public static __Color Highlight { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the text of selected
        //     items.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the text of selected items.
        public static __Color HighlightText { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color used to designate
        //     a hot-tracked item.
        //
        // Returns:
        //     A System.Drawing.Color that is the color used to designate a hot-tracked
        //     item.
        public static __Color HotTrack { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of an inactive window's
        //     border.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of an inactive window's border.
        public static __Color InactiveBorder { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the background
        //     of an inactive window's title bar.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the background of an inactive
        //     window's title bar.
        public static __Color InactiveCaption { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the text in an
        //     inactive window's title bar.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the text in an inactive window's
        //     title bar.
        public static __Color InactiveCaptionText { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the background
        //     of a ToolTip.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the background of a ToolTip.
        public static __Color Info { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the text of a
        //     ToolTip.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the text of a ToolTip.
        public static __Color InfoText { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of a menu's background.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of a menu's background.
        public static __Color Menu { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the background
        //     of a menu bar.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the background of a menu bar.
        public static __Color MenuBar { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color used to highlight
        //     menu items when the menu appears as a flat menu.
        //
        // Returns:
        //     A System.Drawing.Color that is the color used to highlight menu items when
        //     the menu appears as a flat menu.
        public static __Color MenuHighlight { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of a menu's text.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of a menu's text.
        public static __Color MenuText { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the background
        //     of a scroll bar.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the background of a scroll bar.
        public static __Color ScrollBar { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the background
        //     in the client area of a window.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the background in the client
        //     area of a window.
        public static __Color Window { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of a window frame.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of a window frame.
        public static __Color WindowFrame { get; private set; }
        //
        // Summary:
        //     Gets a System.Drawing.Color structure that is the color of the text in the
        //     client area of a window.
        //
        // Returns:
        //     A System.Drawing.Color that is the color of the text in the client area of
        //     a window.
        public static __Color WindowText { get; private set; }
    }
}
