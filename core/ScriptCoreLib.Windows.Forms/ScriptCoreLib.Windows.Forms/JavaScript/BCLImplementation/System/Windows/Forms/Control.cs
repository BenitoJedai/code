using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Control))]
    internal class __Control
    {
        [Script(Implements = typeof(global::System.Windows.Forms.Control.ControlCollection))]
        internal class __ControlCollection
        {
            public void Add(Control e)
            {
            }
        }

        public void PerformLayout()
        {

        }
        public void SuspendLayout()
        {

        }
        public void ResumeLayout(bool b)
        {

        }


        public Control.ControlCollection Controls { get; set; }
        public Point Location { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Size Size { get; set; }
        public int TabIndex { get; set; }
        public bool AutoSize { get; set; }
        public Color ForeColor { get; set; }
    }
}
