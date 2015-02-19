using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/TreeView.cs,babfd7e2e150fae9,references
    // X:\jsc.svn\core\ScriptCoreLibJava.Windows.Forms\ScriptCoreLibJava.Windows.Forms\BCLImplementation\System\Windows\Forms\TreeView.cs

    [Script(Implements = typeof(global::System.Windows.Forms.TreeView))]
    internal class __TreeView : __Control
    {
        // would we be able to render it into svg for vr?
        // could a NDK do a window and host this?


        // can we databind to the three.js visual tree for diagnostics?
        // https://zproxy.wordpress.com/2009/10/21/extending-scriptcorelib/#more-1445
        // 5 years later:D!
        // drag n drop may be important.
        // where is gx2?


        // X:\jsc.svn\examples\javascript\forms\Test\TestTreeView\TestTreeView\ApplicationControl.cs

        public TreeNodeCollection Nodes { get; }
    }
}
