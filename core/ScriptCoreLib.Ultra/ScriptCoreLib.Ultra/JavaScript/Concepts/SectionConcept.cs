using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.Concepts
{
	
	public interface ISectionConcept
	{
		IHTMLDiv Header { get; }
		IHTMLDiv Content { get; }
	}

	public static class ISectionConceptImplementation
	{
		public static SectionConcept<T> ToSectionConcept<T>(this T that)
			where T : ISectionConcept
		{
			return new SectionConcept<T>(that);
		}


	}

	internal class SectionConcept : ISectionConcept
	{

		#region ISectionConcept Members

		public IHTMLDiv Header
		{
			get { throw new NotImplementedException(); }
		}

		public IHTMLDiv Content
		{
			get { throw new NotImplementedException(); }
		}

		#endregion
	}

	public class SectionConcept<T> : ISectionConcept, IDisposable
		where T : ISectionConcept
	{
		public readonly T Target;

		public SectionConcept(T Target)
		{
			this.Target = Target;

			this.Header.style.cursor = ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.pointer;
		}

		public IHTMLDiv Header { get { return Target.Header; } }
		public IHTMLDiv Content { get { return Target.Content; } }

		#region IDisposable Members

		public void Dispose()
		{
			// undo? :)
		}

		#endregion
	}
}
