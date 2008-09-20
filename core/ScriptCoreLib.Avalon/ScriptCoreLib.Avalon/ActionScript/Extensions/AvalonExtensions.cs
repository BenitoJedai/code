using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls;
using System.Windows;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows;
using System.Windows.Controls;

namespace ScriptCoreLib.ActionScript.Extensions
{
	[Script]
	public static class AvalonExtensions
	{
		public static Sprite ToSprite(this Panel e)
		{
			__Panel c = e;

			return c.InternalSprite;
		}

		public static void InvokeWhenStageIsReady(this UIElement e, Action<Stage> h)
		{
			__UIElement x = e;

			var z = x.InternalGetDisplayObject();

			z.addedToStage +=
				delegate
				{
					h(z.stage);
				};
		}

		public static T AttachToContainer<T>(this T e, DisplayObjectContainer c) where T : global::System.Windows.Controls.Panel
		{
			__Panel p = e;

			c.addChild(p.InternalSprite);

			return e;
		}
	}
}
