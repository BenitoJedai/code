using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripContainer))]
    public class __ToolStripContainer : __ContainerControl
    {
        // X:\jsc.svn\examples\javascript\forms\ChartExperiment\ChartExperiment\ApplicationControl.cs
        // http://msdn.microsoft.com/en-us/library/system.windows.forms.toolstripcontainer(v=vs.110).aspx
        // ToolStripContainer does not support Multiple Document Interface (MDI) applications. Use ToolStripPanel for MDI applications.

        public IHTMLDiv InternalElement = typeof(__ToolStripContainer);


        static IStyle InternalStyle = new IStyle(typeof(__ToolStripContainer))
        {
            overflow = IStyle.OverflowEnum.hidden
        };


        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }


        public ToolStripPanel TopToolStripPanel { get; set; }


        public bool BottomToolStripPanelVisible { get; set; }
        public bool TopToolStripPanelVisible { get; set; }
        public bool LeftToolStripPanelVisible { get; set; }
        public bool RightToolStripPanelVisible { get; set; }


        public ToolStripContentPanel ContentPanel { get; set; }


        public __ToolStripContainer()
        {
            // Uncaught Error: HTML element has not been set for this control.

            this.TopToolStripPanel = new ToolStripPanel
            {
                // size to content?
                Height = 25,

                Dock = DockStyle.Top,


                //BackColor = global::System.Drawing.Color.Red
            };

            this.TopToolStripPanel.ControlAdded +=
                (sender, args) =>
                {
                    // we do not yet support autosize nor dragging!
                    args.Control.Dock = DockStyle.Top;
                };

            this.ContentPanel = new ToolStripContentPanel
            {

                Left = 0,

                // css position absolute
                Top = 25
            };



            this.Controls.Add(this.TopToolStripPanel);
            this.Controls.Add(this.ContentPanel);





            //this.TopToolStripPanel.GetHTMLTarget().AttachTo(this.InternalElement);
            //this.ContentPanel.GetHTMLTarget().AttachTo(this.InternalElement);
        }
    }
}
