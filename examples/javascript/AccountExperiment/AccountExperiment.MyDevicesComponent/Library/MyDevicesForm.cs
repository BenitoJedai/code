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

        public int __account { get; set; }
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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

            #region pending
            if (string.IsNullOrEmpty((string)cells_id.Value))
            {
                // need to add it!

                // until the service tells us, mark it pending
                cells_id.Value = "?";

                // we are limited by sinle account.
                // client can try to hack. server must enforce.
                cells_account.Value = "" + this.__account;

                // prevent nulls
                if (cells_value.Value == null)
                    cells_value.Value = "";

                if (cells_name.Value == null)
                    cells_name.Value = "";


                this.Cursor = Cursors.AppStarting;
                service.MyDevices_Insert(
                    "" + this.__account,
                    (string)cells_name.Value,
                    (string)cells_value.Value,

                    __id =>
                    {
                        cells_id.Value = __id;
                        this.Cursor = Cursors.Default;
                    }
                );
                return;
            }
            #endregion

            if ((string)cells_id.Value == "?")
                return;

            this.Cursor = Cursors.AppStarting;
            service.MyDevices_Update(
                "" + this.__account,
                (string)cells_id.Value,
                (string)cells_name.Value,
                (string)cells_value.Value,

                done: delegate
                {
                    this.Cursor = Cursors.Default;
                }
            );

        }

        bool ScriptCoreLib_bug_disable_dataGridView1_CellValueChanged = true;

        private void MyDevicesForm_Load(object sender, EventArgs e)
        {
            // 
            this.Cursor = Cursors.AppStarting;
            service.MyDevices_SelectByAccount(
                "" + this.__account,
                yield: (id, name, value) =>
                {
                    var row = new DataGridViewRow();

                    row.Cells.AddTextRange(
                        id,
                        "" + __account,
                        name,
                        value
                    );

                    this.dataGridView1.Rows.Add(row);
                },
                done: delegate
                {
                    this.Cursor = Cursors.Default;

                    ScriptCoreLib_bug_disable_dataGridView1_CellValueChanged = false;
                }

            );
        }
    }
}
