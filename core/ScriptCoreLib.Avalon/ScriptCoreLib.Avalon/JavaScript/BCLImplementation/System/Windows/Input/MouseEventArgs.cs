using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input
{
	[Script(Implements = typeof(global::System.Windows.Input.MouseEventArgs))]
	internal class __MouseEventArgs : __InputEventArgs
	{
		public double Internal_stageX;
		public double Internal_stageY;

		public Point GetPosition(IInputElement relativeTo)
		{
			//__IInputElement _relativeTo = (__IInputElement)(object)relativeTo;

			//var p = _relativeTo.InternalGetDisplayObjectDirect().globalToLocal(
			//    new flash.geom.Point(Internal_stageX, Internal_stageY)
			//);

			// add offset from the control

			return new Point { X = Internal_stageX, Y = Internal_stageY };
		}

		public static implicit operator MouseEventArgs(__MouseEventArgs e)
		{
			return (MouseEventArgs)(object)e;
		}

		public static implicit operator __MouseEventArgs(IEvent e)
		{
			return new __MouseEventArgs
			{
				Internal_stageX = e.OffsetX,
				Internal_stageY = e.OffsetY
			};
		}
	}
}
