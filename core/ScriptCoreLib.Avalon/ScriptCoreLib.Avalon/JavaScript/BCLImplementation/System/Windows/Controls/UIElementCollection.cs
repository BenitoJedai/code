using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.UIElementCollection))]
	internal class __UIElementCollection
	{
		public IHTMLElement InternalSprite;

		readonly List<UIElement> InternalItems = new List<UIElement>();

		public FrameworkElement InternalVisualParent;

		public __UIElementCollection(FrameworkElement VisualParent)
		{
			var p = VisualParent as Panel;

			if (p == null)
				throw new Exception("VisualParent should be of type Panel instead of " + VisualParent.GetType().Name);


			this.InternalVisualParent = VisualParent;
		}

		public virtual int Add(UIElement element)
		{
			__UIElement _element = element;
			__FrameworkElement _felement = _element as __FrameworkElement;

			if (_felement == null)
				throw new NotImplementedException();

			_felement.InternalParent = this.InternalVisualParent;

			InternalSprite.appendChild(_element.InternalGetDisplayObject());

			InternalItems.Add(element);

			return InternalItems.Count - 1;
		}

		public virtual void Remove(UIElement element)
		{
			__UIElement _element = element;
			__FrameworkElement _felement = _element as __FrameworkElement;

			if (_felement == null)
				throw new NotImplementedException();

			if (_felement.InternalParent != this.InternalVisualParent)
				return;

			InternalSprite.removeChild(_element.InternalGetDisplayObject());

			InternalItems.Remove(element);

			_felement.InternalParent = null;
		}

		public static implicit operator global::System.Windows.Controls.UIElementCollection(__UIElementCollection e)
		{
			return (global::System.Windows.Controls.UIElementCollection)(object)e;
		}
	}
}
