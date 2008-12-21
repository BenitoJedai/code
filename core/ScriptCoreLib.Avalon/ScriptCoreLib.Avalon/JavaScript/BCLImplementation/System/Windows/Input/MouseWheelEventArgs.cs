using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input
{
	[Script(Implements = typeof(global::System.Windows.Input.MouseWheelEventArgs))]
	internal class __MouseWheelEventArgs : __MouseEventArgs
	{
		public int Delta { get; set; }

		public static implicit operator MouseWheelEventArgs(__MouseWheelEventArgs e)
		{
			return (MouseWheelEventArgs)(object)e;
		}

		public static implicit operator __MouseWheelEventArgs(IEvent e)
		{
			return new __MouseWheelEventArgs
			{
				Internal_OffsetX = e.OffsetX,
				Internal_OffsetY = e.OffsetY,
				Internal_Element = (IHTMLElement)e.Element,
				Delta = e.WheelDirection
			};
		}
	}
}
