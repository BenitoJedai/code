using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.Container))]
    internal class __Container : IContainer
    {
        #region IContainer Members

        public void Add(IComponent component, string name)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Add(IComponent component)
        {
        }

        public ComponentCollection Components
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public void Remove(IComponent component)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
