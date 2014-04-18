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
using ScriptCoreLib.JavaScript.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{

    using DOMHandler = global::System.Action<DOM.IEvent>;
    using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
    using ScriptCoreLib.JavaScript.DOM;

    public partial class __Control : __Component
    {

        public DockStyle InternalDock;
        public virtual DockStyle Dock
        {
            get { return InternalDock; }
            set
            {
                InternalDock = value;

                //Console.WriteLine(new { InternalDock });

                // X:\jsc.svn\examples\javascript\forms\HashForBindingSource\HashForBindingSource\ApplicationControl.cs
                InternalChildrenAnchorUpdate(
                    clientWidth,
                    clientHeight,
                    0,
                    0,

                    this

                    );

            }
        }

        //public void InternalChildrenAnchorUpdate(int width, int height, int dx, int dy, Control c)


    }
}
