using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows;
using ScriptCoreLib.ActionScript.flash.display;
using System.Windows.Controls;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.UIElementCollection))]
	internal class __UIElementCollection : IEnumerable
	{
		public Sprite InternalSprite;

		public FrameworkElement InternalVisualParent;

		public __UIElementCollection(FrameworkElement VisualParent)
		{
			this.InternalVisualParent = VisualParent;
		}

		readonly List<UIElement> InternalItems = new List<UIElement>();

		public virtual int Add(UIElement element)
		{
			__UIElement _element = element;
			__FrameworkElement _felement = _element as __FrameworkElement;

			if (_felement == null)
				throw new NotImplementedException();

			_felement.InternalParent = this.InternalVisualParent;

			InternalSprite.addChild(_element.InternalGetDisplayObject());

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

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return this.InternalItems.GetEnumerator();
		}

		#endregion
	}
}
