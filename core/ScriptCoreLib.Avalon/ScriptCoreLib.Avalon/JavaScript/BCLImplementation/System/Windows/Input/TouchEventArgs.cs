using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Windows.Input.TouchEventArgs, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
    internal class __TouchEventArgs : __InputEventArgs
    {
        internal ITouchEvent InternalValue;

        // While targeting .NET 3.5 framework we still need to let jsc know 
        // that if it is running under .NET 4.0 framework we have some interests.

        __TouchDevice InternalTouchDevice;
        public __TouchDevice TouchDevice
        {
            get
            {
                if (InternalTouchDevice == null)
                {
                    InternalTouchDevice = new __TouchDevice
                    {
                        Id = this.InternalValue.streamId
                    };
                }
                return InternalTouchDevice;
            }
        }

        internal Func<IHTMLElement, double> GetTouchPointX;
        internal Func<IHTMLElement, double> GetTouchPointY;

        public __TouchPoint GetTouchPoint(IInputElement relativeTo)
        {
            __IInputElement _relativeTo = (__IInputElement)(object)relativeTo;

            var s = _relativeTo.InternalGetDisplayObjectDirect();

            var Position = new Point(
                    this.InternalValue.GetOffsetX(s),
                    this.InternalValue.GetOffsetY(s)
                );

            return new __TouchPoint
            {
                TouchDevice = TouchDevice,
                Position = Position
            };
        }
    }
}
