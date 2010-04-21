using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.Concepts
{
	[Description("An interface whose all properties are IHTMLElement are considered.")]
	public interface IHorizontalSplitConcept : IUltraComponent
	{
		IHTMLDiv ContentContainer { get; set; }
		IHTMLDiv LeftContainer { get; set; }
		IHTMLDiv RightContainer { get; set; }

	}


	[Description("An interface whose all properties are IHTMLElement are considered.")]
	public interface IHorizontalSplitAreaConcept : IUltraComponent
	{
		IHTMLDiv Abort { get; set; }
		IHTMLDiv Target { get; set; }
	}
}
