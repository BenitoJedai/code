using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.Layout;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripItemCollection))]
    public class __ToolStripItemCollection : __ArrangedElementCollection
    {
        public List<__ToolStripItem> InternalItems = new List<__ToolStripItem>();
        
        public __ToolStrip InternalOwner;

        public __ToolStripItemCollection()
        {
            
        }

        public void AddRange(ToolStripItem[] toolStripItems)
        {
            foreach (__ToolStripItem item in toolStripItems)
            {
                InternalItems.Add(item);

                item.InternalSetOwner(this.InternalOwner);
            }
        }
    }
}
