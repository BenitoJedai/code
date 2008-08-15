using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input
{
	[Script(Implements = typeof(global::System.Windows.Input.MouseEventArgs))]
	internal class __MouseEventArgs : __InputEventArgs
	{
		public double Internal_stageX;
		public double Internal_stageY;

		public Point GetPosition(IInputElement relativeTo)
		{
			__IInputElement _relativeTo = (__IInputElement)(object)relativeTo;

			var p = _relativeTo.InternalGetDisplayObjectDirect().globalToLocal(
				new flash.geom.Point(Internal_stageX, Internal_stageY)
			);

			return new Point { X = p.x, Y = p.y };
		}

		public static implicit operator MouseEventArgs(__MouseEventArgs e)
		{
			return (MouseEventArgs)(object)e;
		}

		public static implicit operator __MouseEventArgs(MouseEvent e)
		{
			return new __MouseEventArgs
			{
				Internal_stageX = e.stageX,
				Internal_stageY = e.stageY
			};
		}
	}
}
