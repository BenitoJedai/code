using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.PHP.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.IComponent))]
	internal interface __IComponent : IDisposable
	{
		ISite Site { get; set; }

		event EventHandler Disposed;
	}
}
