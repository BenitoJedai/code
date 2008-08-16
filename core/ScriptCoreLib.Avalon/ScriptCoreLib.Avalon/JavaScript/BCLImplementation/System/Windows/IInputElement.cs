using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.IInputElement))]
	internal interface __IInputElement
	{
		IHTMLElement InternalGetDisplayObjectDirect();

		event MouseEventHandler MouseMove;
	}
}
