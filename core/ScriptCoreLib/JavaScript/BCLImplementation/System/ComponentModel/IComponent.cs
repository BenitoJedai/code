using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.IComponent))]
	internal interface __IComponent : IDisposable
	{
		event EventHandler Disposed;
	}
}
