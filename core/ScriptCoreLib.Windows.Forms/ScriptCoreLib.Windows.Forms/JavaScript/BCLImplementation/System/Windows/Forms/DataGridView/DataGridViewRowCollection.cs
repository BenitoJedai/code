using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewRowCollection))]
    public class __DataGridViewRowCollection : __BaseCollection, IEnumerable
    {
        public __DataGridView InternalContext;

        public BindingListWithEvents<__DataGridViewRow> InternalItems = new BindingListWithEvents<__DataGridViewRow>();

        public virtual int Add()
        {
            var r = new DataGridViewRow();

            return Add(r);
        }
        public virtual int Add(DataGridViewRow e)
        {
            var x = (__DataGridViewRow)(object)e;

            InternalItems.Source.Add(x);
            return InternalItems.Source.Count - 1;
        }

        public virtual void AddRange(params DataGridViewRow[] dataGridViewColumns)
        {
            foreach (var item in dataGridViewColumns)
            {
                Add(item);
            }
        }

        public void Clear()
        {
            // keep newrow
            while (this.Count > 1)
                this.InternalItems.Source.RemoveAt(0);

            //this.int

            InternalContext.__ContentTableBody.appendChild(InternalContext.InternalNewRow.InternalTableRow);

        }

        public void Remove(DataGridViewRow r)
        {
            RemoveAt(r.Index);
        }

        public void RemoveAt(int i)
        {

            this.InternalItems.Source.RemoveAt(i);
        }

        public override int Count
        {
            get
            {
                return this.InternalItems.Source.Count;
            }
            set
            {
            }
        }


        public IEnumerator GetEnumerator()
        {
            return this.InternalItems.Source.GetEnumerator();
        }

        public DataGridViewRow this[int index]
        {
            get
            {
                return this.InternalItems.Source[index];
            }
        }
    }
}
