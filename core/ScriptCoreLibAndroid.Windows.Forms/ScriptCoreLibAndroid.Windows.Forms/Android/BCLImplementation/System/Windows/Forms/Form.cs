using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Form))]
    internal class __Form : __ContainerControl
    {
        // tested by
        // X:\jsc.svn\examples\javascript\forms\FormsConfiguredAtWebService\FormsConfiguredAtWebService\ApplicationWebService.cs
        // X:\jsc.svn\examples\java\android\forms\FormsMessageBox\FormsMessageBox\ApplicationActivity.cs

        public override string Text { get; set; }

        public event EventHandler Load;

        public Size ClientSize { get; set; }
    }
}
