using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripPanel))]
    public class __ToolStripPanel : __ContainerControl
    {
        // X:\jsc.svn\examples\javascript\forms\ChartExperiment\ChartExperiment\ApplicationControl.cs
        // http://msdn.microsoft.com/en-us/library/system.windows.forms.toolstripcontainer(v=vs.110).aspx




        public IHTMLDiv InternalElement = typeof(__ToolStripPanel);


        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }

    }
}
