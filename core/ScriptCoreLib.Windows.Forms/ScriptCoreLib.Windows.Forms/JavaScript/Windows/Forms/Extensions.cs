using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.Windows.Forms
{

    [Script]
    public static class Extensions
    {
        static public void ApplyBorderStyle(this IHTMLElement e, BorderStyle value)
        {
            if (value == BorderStyle.None)
            {
                e.style.border = "";
            }
            else if (value == BorderStyle.FixedSingle)
            {
                e.style.borderStyle = "solid";
                e.style.borderWidth = "1px";
                e.style.borderColor = Shared.Drawing.Color.System.ActiveBorder;
            }
            else
            {
                e.style.borderStyle = "inset";
                e.style.borderWidth = "1px";
                e.style.borderColor = Shared.Drawing.Color.System.ActiveBorder;
            }
        }

        static internal bool IsTypeOf(this object e, Type t)
        {
			// wouldn't Type.Equals work in javascript?

            var x = e.GetType();
            var a = x.TypeHandle.Value;
            var b = t.TypeHandle.Value;

            return a == b;
        }

        static public MouseButtons GetMouseButton(this IEvent e)
        {
            var button = MouseButtons.None;


            if (e.MouseButton == IEvent.MouseButtonEnum.Left) button = MouseButtons.Left;
            else if (e.MouseButton == IEvent.MouseButtonEnum.Middle) button = MouseButtons.Middle;
            else if (e.MouseButton == IEvent.MouseButtonEnum.Right) button = MouseButtons.Right;

            return button;
        }

        static public MouseEventArgs GetMouseEventHandler(this IEvent e, MouseButtons button)
        {


            var clicks = 0;
            var x = e.CursorX;
            var y = e.CursorY;
            var delta = 0;

            return new MouseEventArgs(button, clicks, x, y, delta);
        }

        static public IHTMLElement GetHTMLTarget(this Control e)
        {
            __Control x = e;

            var r = x.HTMLTargetRef;

            if (r == null)
                throw new Exception("HTML element has not been set for this control.");

            return r;
        }

        static public IHTMLElement GetHTMLTargetContainer(this Control e)
        {
            __Control x = e;

            var r = x.HTMLTargetContainerRef;

            if (r == null)
                throw new Exception("HTML element has not been set for this control.");

            return r;
        }

    }
}
