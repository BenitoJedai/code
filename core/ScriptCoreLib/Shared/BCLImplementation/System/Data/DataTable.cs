using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    // http://referencesource.microsoft.com/#System.Data/data/System/Data/DataTable.cs
    // https://github.com/Microsoft/referencesource/blob/master/System.Data/System/Data/DataTable.cs

    [Script(Implements = typeof(global::System.Data.DataTable))]
    public class __DataTable : __MarshalByValueComponent, __IListSource
    {
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView.cs

        //        Implementation not found for type import :
        //type: System.Data.DataTable
        //method: Void Merge(System.Data.DataTable)

        public DataView DefaultView
        {
            get
            {
                return new DataView(this);
            }
        }

        public void Merge(DataTable table)
        {
            // tested by
            // "X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\ApplicationWebService.cs"


            //at ScriptCoreLib.Shared.BCLImplementation.System.Data.__DataTable.Merge(__DataTable.java:108)

            //Console.WriteLine("Merge");

            foreach (DataColumn item in table.Columns)
            {
                //Console.WriteLine("Merge " + new { item.ColumnName });

                if (!this.Columns.Contains(item.ColumnName))
                    this.Columns.Add(item.ColumnName);

            }

            foreach (DataRow item in table.Rows)
            {
                var r = this.NewRow();
                this.Rows.Add(r);

                foreach (DataColumn c in table.Columns)
                {
                    var value = item[c];

                    //Console.WriteLine("Merge add " + new { c.ColumnName, value });

                    r[c.ColumnName] = item[c];
                }
            }
        }



        #region ColumnChanged
        public event DataColumnChangeEventHandler ColumnChanged;
        public void raise_ColumnChanged(DataColumnChangeEventArgs e)
        {
            if (this.ColumnChanged != null)
                this.ColumnChanged(this, e);
        }
        #endregion


        public event DataTableNewRowEventHandler TableNewRow;
        public event DataRowChangeEventHandler RowDeleted;


        public event DataRowChangeEventHandler RowDeleting;
        public void RaiseRowDeleting(object s, DataRowChangeEventArgs a)
        {
            if (RowDeleting != null)
                RowDeleting(s, a);
        }

        public string TableName { get; set; }

        public DataColumnCollection Columns { get; internal set; }

        public __DataTable()
        {
            this.Columns = new __DataColumnCollection();
            this.Rows = new __DataRowCollection { InternalDataTable = this };
        }

        public DataRowCollection Rows { get; internal set; }

        public DataRow NewRow()
        {
            var r = new __DataRow { Table = this };

            //Console.WriteLine("raise this.TableNewRow");

            if (this.TableNewRow != null)
                this.TableNewRow(this, new DataTableNewRowEventArgs(r));

            return r;
        }

        public static implicit operator DataTable(__DataTable x)
        {
            return (DataTable)(object)x;
        }

        public bool ContainsListCollection
        {
            // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView\DataGridView.DataSource.cs
            get { return true; }
        }

        public global::System.Collections.IList GetList()
        {
            // ?
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140409
            // http://stackoverflow.com/questions/6588210/does-datatable-implement-ilistsource


            return new __DataRowViewList { InternalContext = this };
        }


        [Script]
        public class __DataRowViewList : IList
        {
            public __DataTable InternalContext;

            public int Add(object value)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(object value)
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }

            public object this[int index]
            {
                get
                {
                    var r = new __DataRowView
                    {
                        Row = this.InternalContext.Rows[index]
                    };

                    return r;
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public void CopyTo(Array array, int index)
            {
                throw new NotImplementedException();
            }

            public int Count
            {
                get
                {
                    return this.InternalContext.Rows.Count;
                }
            }

            public bool IsSynchronized
            {
                get { throw new NotImplementedException(); }
            }

            public object SyncRoot
            {
                get { throw new NotImplementedException(); }
            }

            public IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
    }
}
