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

        public virtual int Add(string name, string header)
        {
            return Add(new DataGridViewColumn { Name = name, HeaderText = header });
        }

        public virtual int Add(DataGridViewColumn e)
        {
            InternalItems.Add(e);
            return InternalItems.Count - 1;
        }

        public override int Count
        {
            get
            {
                return this.InternalItems.Count;
            }
            set
            {
            }
        }

        public virtual void AddRange(params DataGridViewColumn[] dataGridViewColumns)
        {
            foreach (var item in dataGridViewColumns)
            {
                Add(item);
            }
        }

        public DataGridViewColumn this[int index]
        {
            get
            {
                return this.InternalItems[index];
            }
        }
    }
}
