using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript
{
	public interface IUltraComponent
	{
		IHTMLImage[] Images { get; }

		IHTMLAnchor[] Anchors { get; }
	}
}
