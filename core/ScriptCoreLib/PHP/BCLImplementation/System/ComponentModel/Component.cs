using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.Component))]
	internal class __Component : __MarshalByRefObject, IDisposable, __IComponent
	{
		public event EventHandler Disposed;

		protected virtual void Dispose(bool e)
		{
		}

		public bool DesignMode { get; set; }

		public void Dispose()
		{
			Dispose(true);
		}

		#region __IComponent Members

		public global::System.ComponentModel.ISite Site
		{
			get;
			set;
		}

		#endregion
	}
}
