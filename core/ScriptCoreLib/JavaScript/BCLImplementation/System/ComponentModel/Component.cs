using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.Component))]
	internal class __Component : __MarshalByRefObject, IDisposable, __IComponent
    {
		public event EventHandler Disposed;

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
