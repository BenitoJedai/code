﻿using System;
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

		public virtual int Add(UIElement element)
		{
			__UIElement _element = element;

			InternalSprite.appendChild(_element.InternalGetDisplayObject());

			InternalItems.Add(element);

			return InternalItems.Count - 1;
		}

		public static implicit operator global::System.Windows.Controls.UIElementCollection(__UIElementCollection e)
		{
			return (global::System.Windows.Controls.UIElementCollection)(object)e;
		}
	}
}
