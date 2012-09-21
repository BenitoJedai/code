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

        public virtual int Add(DataGridViewCell e)
        {
            var x = (__DataGridViewCell)(object)e;

            InternalItems.Add(x);

            return InternalItems.Count - 1;
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
