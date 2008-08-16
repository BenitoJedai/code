using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.Canvas))]
	internal class __Canvas : __Panel
	{
		public static void SetLeft(UIElement element, double length)
		{
			__UIElement _element = element;

			_element.InternalGetDisplayObject().style.left = Convert.ToInt32(length) + "px";
		}

		public static void SetTop(UIElement element, double length)
		{
			__UIElement _element = element;

			_element.InternalGetDisplayObject().style.top = Convert.ToInt32(length) + "px";
		}
	}
}
