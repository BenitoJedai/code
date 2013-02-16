using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ContainerControl))]
    public class __ContainerControl : __ScrollableControl
    {
        public Form ParentForm { get; set; }

        internal void InternalAssignParentForm(Form f)
        {
            this.ParentForm = f;

            //foreach (var item in this.Controls)
            //{


            //}
        }

        protected override void OnParentChanged(EventArgs e)
        {
            var f = this.Parent as Form;
            if (f != null)
            {
                InternalAssignParentForm(f);
            }

            RaiseParentChanged(e);
        }

        protected void Dispose(bool disposing)
        {

        }

        public SizeF AutoScaleDimensions { get; set; }
        public AutoScaleMode AutoScaleMode { get; set; }

    }
}
