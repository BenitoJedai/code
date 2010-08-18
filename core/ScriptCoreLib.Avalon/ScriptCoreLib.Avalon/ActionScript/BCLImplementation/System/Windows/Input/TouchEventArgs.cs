using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;
using System.Windows;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input
{

    [Script(ImplementsViaAssemblyQualifiedName = "System.Windows.Input.TouchEventArgs, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
    internal class __TouchEventArgs : __InputEventArgs
    {
        internal TouchEvent InternalValue;

        // While targeting .NET 3.5 framework we still need to let jsc know 
        // that if it is running under .NET 4.0 framework we have some interests.


        internal __TouchDevice InternalTouchDevice;
        public __TouchDevice TouchDevice
        {
            get
            {
                if (InternalTouchDevice == null)
                {
                    InternalTouchDevice = new __TouchDevice
                    {
                        Id = InternalValue.touchPointID   
                    };
                }

                return InternalTouchDevice;
            }
        }

        public __TouchPoint GetTouchPoint(IInputElement relativeTo)
        {
            __IInputElement _relativeTo = (__IInputElement)(object)relativeTo;

            var p = _relativeTo.InternalGetDisplayObjectDirect().globalToLocal(
                new flash.geom.Point(this.InternalValue.stageX, this.InternalValue.stageY)
            );


            return new __TouchPoint
            {
                TouchDevice = TouchDevice,
                Position = new Point(
                    p.x,
                    p.y
                )
            };
        }
    }
}
