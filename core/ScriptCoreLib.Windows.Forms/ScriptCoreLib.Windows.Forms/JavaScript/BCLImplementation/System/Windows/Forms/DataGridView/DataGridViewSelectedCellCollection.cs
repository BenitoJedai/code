using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewSelectedCellCollection))]
    public class __DataGridViewSelectedCellCollection : __BaseCollection, IEnumerable, ICollection, IList
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

        public IEnumerator GetEnumerator()
        {
            return this.InternalItems.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public override int Count
        {
            get { return this.InternalItems.Count; }
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public int Add(object value)
        {
            Add((DataGridViewCell)value);
            return 0;
        }

        public void Clear()
        {
            this.InternalItems.Clear();
        }

        public bool Contains(object value)
        {
            return this.InternalItems.Contains(value);
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            this.InternalItems.RemoveAt(index);
        }

        public object this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
