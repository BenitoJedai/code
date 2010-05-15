using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Components;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.Extensions
{
	public static class UltraComponentsExtensions
	{
		public static void WhenActivated(this ISupportsActivation source, IHTMLElement element)
		{
			WhenActivated(source, element, IStyle.DisplayEnum.empty);
		}

		public static void WhenActivated(this ISupportsActivation source, IHTMLElement element, IStyle.DisplayEnum display)
		{
			element.style.display = IStyle.DisplayEnum.none;

			source.Activated +=
				delegate
				{
					element.style.display = display;
				};

			source.Deactivated +=
				delegate
				{
					element.style.display = IStyle.DisplayEnum.none;
				};
		}
	}
}
