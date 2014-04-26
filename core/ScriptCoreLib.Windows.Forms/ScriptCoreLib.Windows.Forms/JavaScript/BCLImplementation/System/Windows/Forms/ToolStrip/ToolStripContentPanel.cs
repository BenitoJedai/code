using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripContentPanel))]
    public class __ToolStripContentPanel : __Panel
    {
        // X:\jsc.svn\examples\javascript\forms\ChartExperiment\ChartExperiment\ApplicationControl.cs
        // http://msdn.microsoft.com/en-us/library/system.windows.forms.toolstripcontainer(v=vs.110).aspx

        public __ToolStripContentPanel()
        {
            this.Dock = DockStyle.Fill;

            // ??
            //this.InternalElement.style.overflow = DOM.IStyle.OverflowEnum.hidden;

        }

       
        public override bool AutoScroll { get; set; }
    }
}
