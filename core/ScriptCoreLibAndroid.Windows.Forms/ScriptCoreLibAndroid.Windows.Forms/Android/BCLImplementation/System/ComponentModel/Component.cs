using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.Component))]
    public class __Component : __MarshalByRefObject, __IContainer
    {
        public __Component()
        { }

        public bool DesignMode { get; set; }

        //public event EventHandler Disposed;

        public void Dispose()
        { }
        protected virtual void Dispose(bool e) { }
    }
}
