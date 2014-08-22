using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
    // http://referencesource.microsoft.com/#System/compmod/system/componentmodel/Container.cs
    // https://github.com/mono/mono/blob/effa4c07ba850bedbe1ff54b2a5df281c058ebcb/mcs/class/System/System.ComponentModel/Container.cs

    [Script(Implements = typeof(global::System.ComponentModel.Container))]
	internal class __Container : __IContainer, IDisposable
    {
		__ComponentCollection InternalComponents = new __ComponentCollection();

        #region IContainer Members

        public void Add(IComponent component, string name)
        {
			throw new NotImplementedException();
        }

        public void Add(IComponent component)
        {
            this.InternalComponents.InternalElements.Add(component);
        }

		public virtual ComponentCollection Components { get { return (ComponentCollection)(object)InternalComponents; } }


        public void Remove(IComponent component)
        {
			throw new NotImplementedException();
            
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
