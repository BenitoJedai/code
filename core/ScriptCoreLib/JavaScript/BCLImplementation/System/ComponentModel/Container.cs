using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel
{
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
			//this.InternalComponents.InternalElements.Add(component);
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
