using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using java.util;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    public partial class __Control
    {
        [Script(Implements = typeof(global::System.Windows.Forms.Control.ControlCollection))]
        public class __ControlCollection // : Layout.__ArrangedElementCollection
        {
            public __Control InternalContainer;

            public readonly ArrayList InternalItems = new ArrayList();

            public void Add(Control e)
            {
                InternalItems.add(e);

                ((__Control)(object)e).Parent = InternalContainer;
            }
        }

    }
}
