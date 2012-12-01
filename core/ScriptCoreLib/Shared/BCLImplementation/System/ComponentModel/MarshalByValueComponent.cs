using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.MarshalByValueComponent))]
	public class __MarshalByValueComponent : IComponent, IDisposable, IServiceProvider
	{
		public __MarshalByValueComponent()
		{
		}

		public ISite Site { get; set; }

		public event EventHandler Disposed;

		public void Dispose()
		{
		}

		public object GetService(Type serviceType)
		{
			return null;
		}
	}
}
