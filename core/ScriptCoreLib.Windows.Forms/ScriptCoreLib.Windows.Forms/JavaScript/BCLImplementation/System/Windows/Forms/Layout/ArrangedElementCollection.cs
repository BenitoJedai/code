using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.Layout
{
    [Script(Implements = typeof(global::System.Windows.Forms.Layout.ArrangedElementCollection))]
    internal class __ArrangedElementCollection 
    {
        public virtual int Count
        {
            get
            {
                throw new Exception("not implemented");
            }
        }

        public virtual IEnumerator GetEnumerator()
        {
            throw new Exception("not implemented");
        }
    }
}
