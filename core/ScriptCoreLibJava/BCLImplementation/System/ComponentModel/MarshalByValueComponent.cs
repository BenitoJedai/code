using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.ComponentModel;

namespace ScriptCoreLibJava.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.MarshalByValueComponent))]
	internal class __MarshalByValueComponent : IComponent, IDisposable, IServiceProvider
	{

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

		#region IDisposable Members

		void IDisposable.Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IServiceProvider Members

		public object GetService(Type serviceType)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
