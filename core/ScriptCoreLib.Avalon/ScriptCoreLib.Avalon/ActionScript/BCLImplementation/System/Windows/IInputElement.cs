using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.IInputElement))]
	internal interface __IInputElement
	{
		InteractiveObject InternalGetDisplayObjectDirect();

		event MouseEventHandler MouseMove;
	}
}
