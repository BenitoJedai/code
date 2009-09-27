using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.Component))]
	internal class __Component : IDisposable
	{
		public virtual void Dispose(bool e)
		{
		}

		public bool DesignMode { get; set; }

		public void Dispose()
		{
			Dispose(true);
		}
	}
}
