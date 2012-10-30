﻿using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.Layout;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripItemCollection))]
    internal class __ToolStripItemCollection : __ArrangedElementCollection
    {
        public List<ToolStripItem> InternalItems = new List<ToolStripItem>();

        public void AddRange(ToolStripItem[] toolStripItems)
        {
            InternalItems.AddRange(toolStripItems.AsEnumerable());
        }
    }
}
