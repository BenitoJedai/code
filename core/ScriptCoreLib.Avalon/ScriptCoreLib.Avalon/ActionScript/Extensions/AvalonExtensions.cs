using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls;

namespace ScriptCoreLib.ActionScript.Extensions
{
	[Script]
	public static class AvalonExtensions
	{
		public static T AttachToContainer<T>(this T e, DisplayObjectContainer c) where T : global::System.Windows.Controls.Panel
		{
			__Panel p = e;

			c.addChild(p.InternalSprite);

			return e;
		}
	}
}
