using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    // http://referencesource.microsoft.com/#System.Data/data/System/Data/DataView.cs

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
            // roslyn doesnt like smth?

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
                        SequenceEqual = InternalIsSequenceEqualToDataRow(columnNames, SourceRow, SequenceEqual, NewRow);
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

        private static bool InternalIsSequenceEqualToDataRow(string[] columnNames, DataRow SourceRow, bool SequenceEqual, DataRow NewRow)
        {
#if FIXED
            var SourceCells = columnNames.Select(z => SourceRow[z]).ToArray();
            var NewCells = columnNames.Select(z => NewRow[z]).ToArray();

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150109
            // error in java?
            if (SourceCells.SequenceEqual(NewCells))
                SequenceEqual = true;

#endif

            return SequenceEqual;
        }
    }
}
