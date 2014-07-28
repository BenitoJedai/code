using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel.Design
{
    // http://referencesource.microsoft.com/#System.Design/System/ComponentModel/Design/ComponentDesigner.cs

    [Script(Implements = typeof(global::System.ComponentModel.Design.ComponentDesigner))]
    public class __ComponentDesigner : __IDesigner, IDisposable
    {
        // tested by ?

        public IComponent Component { get; set; }

        public virtual void Initialize(IComponent component)
        {
        }

        public virtual void DoDefaultAction()
        {
        }

        public virtual DesignerVerbCollection Verbs { get; set; }

        public void Dispose()
        {

        }
    }
}
