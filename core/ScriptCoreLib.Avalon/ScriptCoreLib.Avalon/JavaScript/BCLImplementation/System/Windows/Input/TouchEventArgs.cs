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
        public ScriptCoreLib.JavaScript.DOM.Touch InternalValue;
        public ScriptCoreLib.JavaScript.DOM.TouchEvent InternalEvent;


        __TouchDevice InternalTouchDevice;
        public __TouchDevice TouchDevice
        {
            get
            {
                if (InternalTouchDevice == null)
                {
                    InternalTouchDevice = new __TouchDevice
                    {
                    };

                    if (this.InternalValue != null)
                        InternalTouchDevice.Id = this.InternalValue.identifier;


                }
                return InternalTouchDevice;
            }
        }



        public __TouchPoint GetTouchPoint(IInputElement relativeTo)
        {
            __IInputElement _relativeTo = (__IInputElement)(object)relativeTo;

            var s = _relativeTo.InternalGetDisplayObjectDirect();

            var m = new IEventExtensions.__MouseEventArgs 
            {
                Internal_OffsetX = this.InternalValue.clientX,
				Internal_OffsetY = this.InternalValue.clientY,
				Internal_Element = (IHTMLElement)this.InternalEvent.Element
            };
            var p = m.GetPosition(_relativeTo.InternalGetDisplayObjectDirect());

            var Position = new Point(
                    p.X,
                    p.Y
                );

            return new __TouchPoint
            {
                TouchDevice = TouchDevice,
                Position = Position
            };
        }
    }
}
