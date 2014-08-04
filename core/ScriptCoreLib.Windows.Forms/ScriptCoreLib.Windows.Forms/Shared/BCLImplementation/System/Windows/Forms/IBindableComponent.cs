using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.IBindableComponent))]
    public interface __IBindableComponent : __IComponent, IDisposable
    {
        // X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\FormsDataBindingForEnabled\ApplicationControl.cs

        ControlBindingsCollection DataBindings { get; }


    }
}
