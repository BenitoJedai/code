using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript
{
	/// <summary>
	/// Interfaces which inherit this interface can define concepts. Concept interfaces are
	/// automatically implemented by generated HTML classes.
	/// </summary>
	public interface IUltraComponent
	{
		IHTMLImage[] Images { get; }

		IHTMLAnchor[] Anchors { get; }
	}
}
