using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewCellCollection))]
    internal class __DataGridViewCellCollection : __BaseCollection
    {
        public BindingList<__DataGridViewCell> InternalItems = new BindingList<__DataGridViewCell>();

        public virtual void Add(DataGridViewCell e)
        {
            var x = (__DataGridViewCell)(object)e;

            InternalItems.Add(x);
        }

        public virtual void AddRange(params DataGridViewCell[] dataGridViewColumns)
        {
            foreach (var item in dataGridViewColumns)
            {
                Add(item);
            }
        }
    }
}
