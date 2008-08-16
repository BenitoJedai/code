using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input
{
	[Script(Implements = typeof(global::System.Windows.Input.MouseButtonEventArgs))]
	internal class __MouseButtonEventArgs : __MouseEventArgs
	{
		public MouseButton ChangedButton { get; set; }
		public static implicit operator MouseButtonEventArgs(__MouseButtonEventArgs e)
		{
			return (MouseButtonEventArgs)(object)e;
		}

		public static implicit operator __MouseButtonEventArgs(MouseEvent e)
		{
			return new __MouseButtonEventArgs
			{
				Internal_stageX = e.stageX,
				Internal_stageY = e.stageY,
				ChangedButton = MouseButton.Left
			};
		}
	}
}
