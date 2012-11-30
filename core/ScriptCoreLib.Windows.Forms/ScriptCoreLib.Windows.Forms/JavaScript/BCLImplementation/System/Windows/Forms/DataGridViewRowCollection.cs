﻿using ScriptCoreLib.Shared.Lambda;
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
    internal class __DataGridViewRowCollection : __BaseCollection, IEnumerable
    {
        public BindingListWithEvents<__DataGridViewRow> InternalItems = new BindingListWithEvents<__DataGridViewRow>();

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

        }

        public void RemoveAt(int i)
        {
 
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
