using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataTable))]
    public class __DataTable : __MarshalByValueComponent
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


            foreach (DataColumn item in table.Columns)
            {
                if (!this.Columns.Contains(item.ColumnName))
                    this.Columns.Add(item.ColumnName);

            }

            foreach (DataRow item in table.Rows)
            {
                var r = this.NewRow();
                this.Rows.Add(r);

                foreach (DataColumn c in table.Columns)
                {
                    r[c.ColumnName] = item[c];
                }
            }
        }


        public void raise_ColumnChanged(DataColumnChangeEventArgs e)
        {
            if (this.ColumnChanged != null)
                this.ColumnChanged(this, e);
        }


        public event DataColumnChangeEventHandler ColumnChanged;
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

            if (this.TableNewRow != null)
                this.TableNewRow(this, new DataTableNewRowEventArgs(r));

            return r;
        }

        public static implicit operator DataTable(__DataTable x)
        {
            return (DataTable)(object)x;
        }
    }
}
