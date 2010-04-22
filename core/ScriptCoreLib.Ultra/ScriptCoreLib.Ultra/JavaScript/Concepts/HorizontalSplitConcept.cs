using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.Concepts
{
	[Description("An interface whose all properties are IHTMLElement are considered.")]
	public interface IHorizontalSplitConcept : IUltraComponent
	{
		IHTMLDiv ContentContainer { get; set; }
		IHTMLDiv Left { get; set; }
		IHTMLDiv LeftScrollable { get; set; }
		IHTMLDiv LeftContainer { get; set; }

		IHTMLDiv Right { get; set; }
		IHTMLDiv RightScrollable { get; set; }
		IHTMLDiv RightContainer { get; set; }
		IHTMLDiv Splitter { get; set; }
	}


	[Description("An interface whose all properties are IHTMLElement are considered.")]
	public interface IHorizontalSplitAreaConcept : IUltraComponent
	{
		IHTMLDiv PageContainer { get; set; }

		IHTMLDiv Abort { get; set; }
		IHTMLDiv Target { get; set; }
	}


}
