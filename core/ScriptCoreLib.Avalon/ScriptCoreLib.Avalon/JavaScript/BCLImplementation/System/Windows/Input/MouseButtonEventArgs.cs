using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input
{
	[Script(Implements = typeof(global::System.Windows.Input.MouseButtonEventArgs))]
	internal class __MouseButtonEventArgs : __MouseEventArgs
	{
		public MouseButton ChangedButton { get; set; }
		public static implicit operator MouseButtonEventArgs(__MouseButtonEventArgs e)
		{
			return (MouseButtonEventArgs)(object)e;
		}

		public static implicit operator __MouseButtonEventArgs(IEvent e)
		{
			return new __MouseButtonEventArgs
			{
				Internal_stageX = e.CursorX,
				Internal_stageY = e.CursorY,
				ChangedButton = MouseButton.Left
			};
		}
	}
}
