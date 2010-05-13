using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Ultra.Studio;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;

namespace ScriptCoreLib.JavaScript.Components
{
	public static class SolutionFileDesignerExtensions
	{
		public static void Add(this SolutionFileDesigner that, SolutionFileDesignerHTMLElementTabs e)
		{
			that.Add(e.HTMLDesignerTab);
			that.Add(e.HTMLSourceTab);

			that.Content.Add(e.HTMLDesignerContent);
			that.Content.Add(e.HTMLSourceView.Container);

		}
	}
}
