using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    public partial class __Control
    {
        public virtual Size GetPreferredSize(Size proposedSize)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\forms\Test\TestGrowingGrid\TestGrowingGrid\ApplicationControl.cs

            // are we supposed to calculate the children?

            var s = this.Size;

            if (this.Controls.Count > 0)
            {
                foreach (Control item in this.Controls)
                {
                    var cps = item.PreferredSize;

                    // what if DockStyle is non default?
                    s.Width = Math.Max(s.Width, cps.Width + item.Left);
                    s.Height = Math.Max(s.Height, cps.Height + item.Top);
                }
            }

            return s;
        }

        public Size PreferredSize
        {
            get
            {
                var proposedSize = new Size(0, 0);

                return GetPreferredSize(proposedSize);
            }
        }
    }
}
