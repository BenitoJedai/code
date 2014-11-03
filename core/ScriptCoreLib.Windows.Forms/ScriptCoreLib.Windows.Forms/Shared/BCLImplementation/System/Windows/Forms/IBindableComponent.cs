using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/IBindableComponent.cs

    [Script(Implements = typeof(global::System.Windows.Forms.IBindableComponent))]
    public interface __IBindableComponent : __IComponent, IDisposable
    {
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\Control\ControlBindingsCollection.cs
        // X:\jsc.svn\core\ScriptCoreLibJava.Windows.Forms\ScriptCoreLibJava.Windows.Forms\BCLImplementation\System\Windows\Forms\ControlBindingsCollection.cs

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141103
        // X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\FormsDataBindingForEnabled\ApplicationControl.cs

        ControlBindingsCollection DataBindings { get; }


    }
}
