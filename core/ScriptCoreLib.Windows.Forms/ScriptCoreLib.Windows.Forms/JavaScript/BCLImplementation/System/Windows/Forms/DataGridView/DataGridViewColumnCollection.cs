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
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewColumnCollection))]
    public class __DataGridViewColumnCollection : __BaseCollection
    {
        public readonly BindingListWithEvents<__DataGridViewColumn> InternalItemsX = new BindingListWithEvents<__DataGridViewColumn>();

        public readonly BindingList<__DataGridViewColumn> InternalItems;

        public __DataGridViewColumnCollection()
        {
            this.InternalItems = this.InternalItemsX.Source;
        }


        public virtual int Add(string name, string header)
        {
            return Add(new DataGridViewColumn { Name = name, HeaderText = header });
        }

        public virtual int Add(DataGridViewColumn e)
        {
            InternalItems.Add(e);
            return InternalItems.Count - 1;
        }
        //script: error JSC1000: No implementation found for this native method, please implement [System.Data.InternalDataCollectionBase.get_Count()]
        public override int Count
        {
            get
            {
                return this.InternalItems.Count;
            }
            set
            {
                // now what?
            }
        }

        public virtual void Clear()
        {
            while (this.Count > 0)
                this.RemoveAt(this.Count - 1);
        }

        public void RemoveAt(int i)
        {
            this.InternalItems.RemoveAt(i);
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

        public DataGridViewColumn this[string c]
        {
            get
            {
                // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\Application.cs

                return this.InternalItems.FirstOrDefault(x => x.Name == c);
            }
        }
    }
}
