using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.Shared.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    partial class __DataGridView
    {
        // X:\jsc.svn\examples\javascript\forms\Test\TestDynamicBindingSourceForDataTable\TestDynamicBindingSourceForDataTable\ApplicationControl.cs
        // X:\jsc.svn\examples\javascript\forms\FormsHistoricBindingSourcePosition\FormsHistoricBindingSourcePosition\ApplicationControl.Designer.cs
        public bool AutoGenerateColumns { get; set; }

        private void InternalAutoGenerateColumns(DataTable SourceDataTable)
        {
            var SourceDataTableColumnCount = SourceDataTable.Columns.Count;
            var cstopwatch = Stopwatch.StartNew();

            // AutoGenerateColumns
            #region Columns
            while (this.Columns.Count > SourceDataTableColumnCount)
                this.Columns.RemoveAt(this.Columns.Count - 1);


            var ColumnIndex = 0;
            foreach (DataColumn item in SourceDataTable.Columns)
            {

                if (ColumnIndex < this.Columns.Count)
                {
                }
                else
                {
                    var ColumnStopwatch = Stopwatch.StartNew();

                    this.Columns.Add(
                        new DataGridViewColumn
                        {
                        }
                    );


                    // 793192ms { Name = dataGridView2, cIndex = 1 } InternalSetDataSource a Column done at { ElapsedMilliseconds = 935 } 

                    // Console.WriteLine(
                    //   new { Name, cIndex = ColumnIndex }
                    //   + " InternalSetDataSource a Column done at "
                    //   + new { ColumnStopwatch.ElapsedMilliseconds }
                    //);
                }

                // X:\jsc.internal.svn\core\com.abstractatech.my.business\com.abstractatech.my.business\Application.cs
                this.Columns[ColumnIndex].Name = item.ColumnName;
                this.Columns[ColumnIndex].HeaderText = item.ColumnName;

                this.Columns[ColumnIndex].ReadOnly = item.ReadOnly;

                ColumnIndex++;




            }

            #endregion



            cstopwatch.Stop();
            // 4141ms event: dataGridView1 set DataSource columns { SourceDataTableColumnCount = 8, ElapsedMilliseconds = 999 } 

            if (cstopwatch.ElapsedMilliseconds > 30)
                Console.WriteLine(
                        "event: "
                        + this.Name
                        + " set DataSource columns "
                        + new
                        {
                            //SourceDataTableColumnCount,
                            cstopwatch.ElapsedMilliseconds,
                        }
                );
        }


    }
}
