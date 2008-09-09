using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.Canvas))]
	internal class __Canvas : __Panel
	{
		public static double GetLeft(UIElement element)
		{
			__UIElement _element = element;
			return _element.InternalGetDisplayObject().x;
		}

		public static double GetTop(UIElement element)
		{
			__UIElement _element = element;
			return _element.InternalGetDisplayObject().y;
		}

		public static void SetLeft(UIElement element, double length)
		{
			__UIElement _element = element;

			_element.InternalGetDisplayObject().x = length;
		}

		public static void SetTop(UIElement element, double length)
		{
			__UIElement _element = element;

			_element.InternalGetDisplayObject().y = length;
		}
	}
}
