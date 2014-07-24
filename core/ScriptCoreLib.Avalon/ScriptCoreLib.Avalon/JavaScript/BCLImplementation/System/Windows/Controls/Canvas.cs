using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
    // http://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/Windows/Controls/Canvas.cs

	[Script(Implements = typeof(global::System.Windows.Controls.Canvas))]
	internal class __Canvas : __Panel
	{
		// http://blogs.msdn.com/devdave/archive/2008/05/21/why-i-don-t-like-canvas.aspx
	
		public static double GetLeft(UIElement element)
		{
			__UIElement _element = element;

			var n = _element.InternalGetDisplayObject();

            //return n.Bounds.Left;
            return _element.InternalLeft;
        }

		public static double GetTop(UIElement element)
		{
			__UIElement _element = element;

            //var n = _element.InternalGetDisplayObject();

            //return n.Bounds.Top;

            return _element.InternalTop;
		}


		public static void SetLeft(UIElement element, double length)
		{
			__UIElement _element = element;

            _element.InternalLeft = length;

			var n = _element.InternalGetDisplayObject();

			n.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			n.style.left = Convert.ToInt32(length) + "px";
		}

		public static void SetTop(UIElement element, double length)
		{
			__UIElement _element = element;

            _element.InternalTop = length;

			var n = _element.InternalGetDisplayObject();

			n.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			n.style.top = Convert.ToInt32(length) + "px";
		}
	}
}
