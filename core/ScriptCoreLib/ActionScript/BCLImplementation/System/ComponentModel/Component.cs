using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.Component))]
    internal class __Component : IComponent
    {
        public virtual void Dispose(bool e)
        {
        }

        public bool DesignMode { get; set; }

		#region IComponent Members

		public event EventHandler Disposed;

		public ISite Site
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
