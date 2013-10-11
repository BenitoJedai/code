using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccountExperiment.MyDevicesComponent.Library
{
    public partial class MyDevicesForm : Form
    {
        // if the client forgets
        // the server needs to remind 
        // what data is available for the client.
        // eg. hacking shall result in logout

        public IMyDevicesComponent_MyDevices service { get; set; }

        public MyDevicesForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private async void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ScriptCoreLib_bug_disable_dataGridView1_CellValueChanged)
                return;

            //X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\Library\GridForm.cs

            // headers?
            if (e.RowIndex < 0)
                return;

            var cells_id = this.dataGridView1[
                //this.id.Index
                   0
                   , e.RowIndex
            ];

            Console.WriteLine(
                "dataGridView1_CellValueChanged: " + new { e.ColumnIndex, e.RowIndex }
            );


            var cells_account = this.dataGridView1[
                //this.account.Index
                1
                , e.RowIndex
            ];
            //cells_account.Style.BackColor = Color.Gray;

            //this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Cyan;

            var cells_name = this.dataGridView1[
                //this.name.Index
                2
                , e.RowIndex
            ];

            var cells_value = this.dataGridView1[
                //this.value.Index
                3
                , e.RowIndex
            ];

            //                    at System.Runtime.CompilerServices.AsyncMethodBuilderCore.&amp;lt;ThrowAsync&amp;gt;b__0(Object state)</StackTrace><ExceptionString>System.InvalidCastException: Unable to cast object of type 'System.Int64' to type 'System.String'.
            //at AccountExperiment.MyDevicesComponent.Library.MyDevicesForm.&amp;lt;dataGridView1_CellValueChanged&amp;gt;d__0.MoveNext() in x:\jsc.svn\examples\javascript\AccountExperiment\AccountExperiment.MyDevicesComponent\Library\MyDevicesForm.cs:line 81

            #region pending

            // Additional information: Unable to cast object of type 'System.Int64' to type 'System.String'.
            if (string.IsNullOrEmpty((string)cells_id.Value))
            {
                // need to add it!

                // until the service tells us, mark it pending
                cells_id.Value = "?";

                // we are limited by sinle account.
                // client can try to hack. server must enforce.
                //cells_account.Value = "" + this.__account;
                cells_account.Value = "?";

                // prevent nulls
                if (cells_value.Value == null)
                    cells_value.Value = "";

                if (cells_name.Value == null)
                    cells_name.Value = "";


                this.Cursor = Cursors.AppStarting;
                var __id = await service.MyDevices_Insert(
                    (string)cells_name.Value,
                    (string)cells_value.Value
                );

                cells_id.Value = "" + __id;
                this.Cursor = Cursors.Default;

                return;
            }
            #endregion

            if ((string)cells_id.Value == "?")
                return;

            this.Cursor = Cursors.AppStarting;

            await service.MyDevices_Update(
                 Convert.ToInt64((string)cells_id.Value),
                (string)cells_name.Value,
                (string)cells_value.Value
            );


            this.Cursor = Cursors.Default;

        }

        bool ScriptCoreLib_bug_disable_dataGridView1_CellValueChanged = true;

        private async void MyDevicesForm_Load(object sender, EventArgs e)
        {
            // 
            this.Cursor = Cursors.AppStarting;

            Action<long, string, string> yield =
                 (id, name, value) =>
                 {
                     var row = new DataGridViewRow();

                     row.Cells.AddTextRange(
                         "" + id,
                          "?",
                         name,
                         value
                     );

                     this.dataGridView1.Rows.Add(row);
                 };

            await service.MyDevices_SelectByAccount(
               yield
            );


            this.Cursor = Cursors.Default;

            ScriptCoreLib_bug_disable_dataGridView1_CellValueChanged = false;
        }
    }
}
