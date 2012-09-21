using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewColumnCollection))]
    internal class __DataGridViewColumnCollection : __BaseCollection
    {
        public BindingList<__DataGridViewColumn> InternalItems = new BindingList<__DataGridViewColumn>();

        public virtual int Add(DataGridViewColumn e)
        {
            var x = (__DataGridViewColumn)(object)e;

            InternalItems.Add(x);
            return InternalItems.Count - 1;
        }

        public virtual void AddRange(params DataGridViewColumn[] dataGridViewColumns)
        {
            foreach (var item in dataGridViewColumns)
            {
                Add(item);
            }
        }
    }
}
