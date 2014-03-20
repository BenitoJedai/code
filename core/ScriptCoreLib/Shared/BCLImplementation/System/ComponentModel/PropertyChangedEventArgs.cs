using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.PropertyChangedEventArgs))]
    internal class __PropertyChangedEventArgs : __EventArgs
    {
        public __PropertyChangedEventArgs(string propertyName)
        {
            this.PropertyName = propertyName;

        }

        public virtual string PropertyName { get; set; }
    }
}
