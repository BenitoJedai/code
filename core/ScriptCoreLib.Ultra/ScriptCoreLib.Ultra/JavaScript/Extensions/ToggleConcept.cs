using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.Extensions
{
	public static class ToggleConcept
	{
		public sealed class ApplyToggleConceptTuple
		{
			public Action Hide;
			public Action Show;
		}

		// should/could the concepts be bound automatically to html pages?
		public static ApplyToggleConceptTuple ApplyToggleConcept(
			this IHTMLElement Content,
			IHTMLElement HideContent,
			IHTMLElement ShowContent
			)
		{
			var t = new ApplyToggleConceptTuple
			{
				Show = delegate
				{
					Content.Show();
					HideContent.Show();
					ShowContent.Hide();
				},

				Hide = delegate
				{
					Content.Hide();
					HideContent.Hide();
					ShowContent.Show();
				}
			};

			HideContent.onclick += eee => { eee.PreventDefault(); t.Hide(); };
			ShowContent.onclick += eee => { eee.PreventDefault(); t.Show(); };

			t.Show();

			return t;
		}
	}
}
