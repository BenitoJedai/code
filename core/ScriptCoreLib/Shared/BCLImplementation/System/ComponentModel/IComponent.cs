using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.IComponent))]
	internal interface __IComponent : IDisposable
	{
        ISite Site { get; set; }

        event EventHandler Disposed;
	}
}
