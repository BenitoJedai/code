using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Components.HTML.Images.SpriteSheet.FromAssets;

namespace ScriptCoreLib.JavaScript.Concepts
{
	public static class SectionConcept
	{
		public static SectionConcept<T> ToSectionConcept<T>(this T that, string Header) where T : ISectionConcept
		{
			var x = that.ToSectionConcept();

			x.Header = Header;

			return x;
		}

		public static SectionConcept<T> ToSectionConcept<T>(this T that) where T : ISectionConcept
		{
			return that.ToSectionConcept(
				new TreeExpandImage(),
				new TreeCollapseImage()
			);
		}

	}
}
