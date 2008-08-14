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
	internal class __UIElementCollection
	{
		public Sprite InternalSprite;

		readonly List<UIElement> InternalItems = new List<UIElement>();

		public virtual int Add(UIElement element)
		{
			__UIElement _element = element;

			InternalSprite.addChild(_element.InternalGetDisplayObject());

			InternalItems.Add(element);

			return InternalItems.Count - 1;
		}

		public static implicit operator global::System.Windows.Controls.UIElementCollection(__UIElementCollection e)
		{
			return (global::System.Windows.Controls.UIElementCollection)(object)e;
		}
	}
}
