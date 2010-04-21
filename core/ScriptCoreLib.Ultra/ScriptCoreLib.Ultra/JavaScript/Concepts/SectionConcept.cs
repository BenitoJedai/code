using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.ComponentModel;

namespace ScriptCoreLib.JavaScript.Concepts
{
	[Description("An interface whose all properties are IHTMLElement are considered.")]
	public interface ISectionConcept : IUltraComponent
	{
		IHTMLDiv Header { get; }
		IHTMLDiv Content { get; }
	}

	public static class ISectionConceptImplementation
	{
		public static SectionConcept<T> ToSectionConcept<T>(this T that, IHTMLImage TreeExpand, IHTMLImage TreeCollapse)
			where T : ISectionConcept
		{
			return new SectionConcept<T>(that, TreeExpand, TreeCollapse);
		}


	}

	public sealed class SectionConcept<T>
		where T : ISectionConcept
	{
		public readonly T Target;

	
		IHTMLSpan _Header;
		public IHTMLSpan Header
		{
			get { return _Header; }
			set
			{
				if (_Header != null)
					_Header.ReplaceWith(value);

				_Header = value;
			}
		}


		IHTMLDiv _Content;
		public IHTMLDiv Content
		{
			get { return _Content; }
			set
			{
				if (_Content != null)
					_Content.ReplaceWith(value);

				_Content = value;
			}
		}



		public SectionConcept(T Target, IHTMLImage TreeExpand, IHTMLImage TreeCollapse)
		{
			this.Target = Target;

			Content = new IHTMLDiv
			{
				Target.Content.childNodes
			};

			Header = new IHTMLSpan
			{
				Target.Header.childNodes
			};

			var Icon = new IHTMLSpan
			{
				TreeExpand,
				TreeCollapse
			};

			Icon.style.marginRight = "1em";

			Icon.AttachTo(Target.Header);
			Header.AttachTo(Target.Header);

			Content.AttachTo(Target.Content);

			Target.Header.style.cursor = ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.pointer;



			TreeExpand.Hide();





			Action onclick = delegate { };

			Target.Header.onclick +=
				delegate
				{
					onclick();
				};

		
			this.NextClickHide =
				delegate
				{
					InternalIsExpanded = false;

					Target.Content.Hide();
					TreeExpand.Show();
					TreeCollapse.Hide();

					onclick = NextClickShow;
				};

			this.NextClickShow =
				delegate
				{
					InternalIsExpanded = true;

					Target.Content.Show();
					TreeExpand.Hide();
					TreeCollapse.Show();

					onclick = NextClickHide;
				};


			onclick = NextClickHide;
		}

		Action NextClickHide;
		Action NextClickShow;


		bool InternalIsExpanded;
		public bool IsExpanded
		{
			get
			{
				return InternalIsExpanded;
			}
			set
			{
				if (value)
					this.NextClickShow();
				else
					this.NextClickHide();
			}
		}
	}
}
