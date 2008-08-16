using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input
{
	[Script(Implements = typeof(global::System.Windows.Input.MouseWheelEventArgs))]
	internal class __MouseWheelEventArgs : __MouseEventArgs
	{
		public int Delta { get; set; }

		public static implicit operator MouseWheelEventArgs(__MouseWheelEventArgs e)
		{
			return (MouseWheelEventArgs)(object)e;
		}

		public static implicit operator __MouseWheelEventArgs(MouseEvent e)
		{
			return new __MouseWheelEventArgs
			{
				Internal_stageX = e.stageX,
				Internal_stageY = e.stageY,
				Delta = e.delta
			};
		}
	}
}
