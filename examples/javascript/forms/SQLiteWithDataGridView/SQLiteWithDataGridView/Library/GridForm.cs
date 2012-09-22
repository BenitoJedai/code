using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace SQLiteWithDataGridView.Library
{
    public partial class GridForm : Form
    {
        public GridForm()
        {
            this.InitializeComponent();

        }

        public string TableName = "SQLiteWithDataGridView_0_Table001";

        ApplicationWebService service = new ApplicationWebService();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = TableName;

            dataGridView1.Enabled = false;
            service.EnumerateItems("",
                (ContentKey, ContentValue, ContentComment) =>
                {
                    var r = new DataGridViewRow();

                    r.Cells.AddRange(

                        new DataGridViewTextBoxCell
                        {
                            Value = ContentKey
                        },
                        new DataGridViewTextBoxCell
                        {
                            Value = ContentValue
                        },
                        new DataGridViewTextBoxCell
                        {
                            Value = ContentComment
                        }
                    );

                    dataGridView1.Rows.Add(r);

                },
                TableName: TableName,

                AtTransactionKey: value =>
                {
                    LocalTransactionKey = value;
                    label2.Text = LocalTransactionKey;
                    timer1.Start();
                    dataGridView1.Enabled = true;

                }
            );
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            // this is the ISNewRow
            //e.Row.Cells[0].Value = "" + dataGridView1.Rows.Count;
        }

        bool InternalCellValueChanged;

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (InternalCellValueChanged)
                return;

            if (e.RowIndex < 0)
                return;
            var c0 = dataGridView1[0, e.RowIndex];

            if ((string)dataGridView1[0, e.RowIndex].Value == "?")
                return;

            if (string.IsNullOrEmpty((string)c0.Value))
            {
                InternalCellValueChanged = true;
                dataGridView1[0, e.RowIndex].Value = "?";
                dataGridView1[0, e.RowIndex].Style.ForeColor = Color.Red;
                InternalCellValueChanged = false;

                var ContentValue = (string)dataGridView1[1, e.RowIndex].Value;
                if (ContentValue == null)
                    ContentValue = "";

                var ContentComment = (string)dataGridView1[2, e.RowIndex].Value;
                if (ContentComment == null)
                    ContentComment = "";

                service.AddItem(
                    ContentValue,
                    ContentComment,
                    LastInsertRowId =>
                    {
                        dataGridView1[0, e.RowIndex].Value = LastInsertRowId;
                        dataGridView1[0, e.RowIndex].Style.ForeColor = Color.Blue;

                        var i = int.Parse(LocalTransactionKey);
                        i++;

                        LocalTransactionKey = i.ToString();
                    },
                    TableName: TableName
                );
            }
            else
            {
                var ContentKey = (string)dataGridView1[0, e.RowIndex].Value;
                dataGridView1[0, e.RowIndex].Style.ForeColor = Color.Blue;

                var ContentValue = (string)dataGridView1[1, e.RowIndex].Value;
                if (ContentValue == null)
                    ContentValue = "";

                var ContentComment = (string)dataGridView1[2, e.RowIndex].Value;
                if (ContentComment == null)
                    ContentComment = "";

                service.UpdateItem(
                    TableName,
                    ContentKey,
                    ContentValue,
                    ContentComment,
                    RemoteTransactionKey =>
                    {
                        var i = int.Parse(LocalTransactionKey);
                        i++;

                        LocalTransactionKey = i.ToString();
                    }
                );

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        string LocalTransactionKey;

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            Action<string> AtServerTransactionKey =
                ServerTransactionKey =>
                {
                    label2.Text = ServerTransactionKey;

                    if (LocalTransactionKey != ServerTransactionKey)
                    {
                        label2.ForeColor = Color.Red;

                        Action<string, string, string> AtContentKey =
                            (ContentKey, ContentValue, ContentComment) =>
                            {
                                DataGridViewRow r = this.dataGridView1.Rows.AsEnumerable().FirstOrDefault(
                                    item => (string)item.Cells[0].Value == ContentKey
                                );


                                if (r == null)
                                {
                                    r = new DataGridViewRow();

                                    r.Cells.AddRange(
                                        new DataGridViewTextBoxCell
                                        {
                                            Value = ContentKey
                                        },
                                        new DataGridViewTextBoxCell
                                        {
                                            Value = ContentValue
                                        },
                                        new DataGridViewTextBoxCell
                                        {
                                            Value = ContentComment
                                        }

                                    );

                                    dataGridView1.Rows.Add(r);

                                }
                                else
                                {
                                    InternalCellValueChanged = true;

                                    r.Cells[1].Value = ContentValue;
                                    r.Cells[2].Value = ContentComment;

                                    InternalCellValueChanged = false;

                                }

                                r.Cells[0].Style.ForeColor = Color.Red;
                            };

                        Action<string> done = delegate
                                {
                                    LocalTransactionKey = ServerTransactionKey;
                                    label2.Text = ServerTransactionKey;
                                    label2.ForeColor = Color.Black;
                                    timer1.Start();
                                };

                        service.EnumerateItemsChangedBetweenTransactions(
                            TableName,
                            LocalTransactionKey,
                            ServerTransactionKey,
                            AtContent: AtContentKey,
                            done: done
                        );

                        // we need updates!
                        return;
                    }


                    timer1.Start();
                };

            service.GetTransactionKeyFor(
                TableName: TableName,
                y: AtServerTransactionKey
            );
        }



    }

}
