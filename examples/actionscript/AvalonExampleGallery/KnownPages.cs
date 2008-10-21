using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace AvalonExampleGallery.Shared
{
	[Script]
	public static class KnownPages
	{
		public static Dictionary<string, Type> Value
		{
			get
			{
				return new Dictionary<string, Type>
				{
					{ 
						global::NavigationButtons.Assets.Shared.KnownAssets.Path.Assets, 
						typeof(NavigationButtons.Code.MyCanvas) },
					{ 
						global::TextSuggestions.Shared.KnownAssets.Path.Assets, 
						typeof(global::TextSuggestions.Shared.TextSuggestionsCanvas) },
				};
			}
		}
	}
}
