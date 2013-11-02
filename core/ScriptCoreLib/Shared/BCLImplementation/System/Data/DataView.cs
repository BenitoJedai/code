using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataView))]
    internal class __DataView : __MarshalByValueComponent
    {
        public DataTable InternalDataTable;

        public __DataView(DataTable t)
        {
            this.InternalDataTable = t;
        }

        public DataTable ToTable(bool distinct, params string[] columnNames)
        {
            var x = new DataTable();

            foreach (var c in columnNames)
            {
                x.Columns.Add(c);
            }

            foreach (DataRow SourceRow in this.InternalDataTable.Rows)
            {
                var SequenceEqual = false;

                if (distinct)
                {
                    foreach (DataRow NewRow in x.Rows)
                    {
                        var SourceCells = columnNames.Select(z => SourceRow[z]).ToArray();
                        var NewCells = columnNames.Select(z => NewRow[z]).ToArray();

                        if (SourceCells.SequenceEqual(NewCells))
                            SequenceEqual = true;
                    }
                }

                if (!SequenceEqual)
                {
                    var NewRow = x.NewRow();
                    x.Rows.Add(NewRow);

                    foreach (var c in columnNames)
                    {
                        var value = SourceRow[c];

                        //Console.WriteLine(
                        //    "ToTable: " +
                        //    new { c, value }
                        //    );


                        NewRow[c] = value;


                        //var xvalue = NewRow[c];

                        //Console.WriteLine(
                        //    "ToTable: " +
                        //    new { c, xvalue }
                        //    );

                    }
                }
            }

            return x;
        }
    }
}
