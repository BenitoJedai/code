using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.ComponentModel;

namespace ScriptCoreLibJava.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.Container))]
	internal class __Container : __IContainer, IDisposable
	{
		__ComponentCollection InternalComponents;

		public __Container()
		{
			this.InternalComponents = new __ComponentCollection();
		}

		#region __IContainer Members

		public virtual ComponentCollection Components { get { return (ComponentCollection)(object)InternalComponents; } }

		public void Add(global::System.ComponentModel.IComponent component)
		{
			this.InternalComponents.InternalElements.Add(component);
		}

		public void Add(global::System.ComponentModel.IComponent component, string name)
		{
			throw new NotImplementedException();
		}

		public void Remove(global::System.ComponentModel.IComponent component)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}
