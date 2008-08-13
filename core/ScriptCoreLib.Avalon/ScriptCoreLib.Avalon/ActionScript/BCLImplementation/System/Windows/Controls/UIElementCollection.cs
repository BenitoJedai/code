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
			var AsTextField = element as TextBox;

			if (AsTextField != null)
			{
				__TextBox _TextField = AsTextField;

				InternalSprite.addChild(_TextField.InternalTextField);
			}

			InternalItems.Add(element);

			return InternalItems.Count - 1;
		}

		public static implicit operator global::System.Windows.Controls.UIElementCollection(__UIElementCollection e)
		{
			return (global::System.Windows.Controls.UIElementCollection)(object)e;
		}
	}
}
