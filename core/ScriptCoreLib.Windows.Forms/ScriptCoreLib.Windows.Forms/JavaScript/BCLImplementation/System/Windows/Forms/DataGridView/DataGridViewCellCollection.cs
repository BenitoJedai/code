using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewCellCollection))]
    public class __DataGridViewCellCollection : __BaseCollection
    {
        public BindingListWithEvents<__DataGridViewCell> InternalItemsX = new BindingListWithEvents<__DataGridViewCell>();
        public readonly BindingList<__DataGridViewCell> InternalItems;

        public __DataGridViewRow InternalRow;

        public __DataGridViewCellCollection()
        {
            this.InternalItems = InternalItemsX.Source;
        }



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

        public void RemoveAt(int i)
        {
            this.InternalItemsX.Source.RemoveAt(i);
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

        public DataGridViewCell this[string columnName]
        {
            get
            {
                //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridViewElement.get_DataGridView()]

                //return this.InternalItems.FirstOrDefault(k => k.OwningRow.DataGridView.Columns[k.ColumnIndex].Name == columnName);


                var c = this.InternalRow.InternalContext.InternalColumns.InternalItems.FirstOrDefault(x => x.Name == columnName);

                return this[c.Index];
            }
        }

        public DataGridViewCell this[int index]
        {
            get
            {
                //Console.WriteLine("enter get DataGridViewCell");
                var value = this.InternalItems[index];
                //Console.WriteLine("exit get DataGridViewCell");
                return value;
            }
        }
    }
}
