﻿using System;
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
        public BindingList<__DataGridViewRow> InternalItems = new BindingList<__DataGridViewRow>();

        public virtual int Add(DataGridViewRow e)
        {
            var x = (__DataGridViewRow)(object)e;

            InternalItems.Add(x);
            return InternalItems.Count - 1;
        }

        public virtual void AddRange(params DataGridViewRow[] dataGridViewColumns)
        {
            foreach (var item in dataGridViewColumns)
            {
                Add(item);
            }
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


        public IEnumerator GetEnumerator()
        {
            return this.InternalItems.GetEnumerator();
        }

        public DataGridViewRow this[int index]
        {
            get
            {
                return this.InternalItems[index];
            }
        }
    }
}
