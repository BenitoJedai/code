using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.Media;
using System.Diagnostics;

namespace SQLiteWithDataGridView.Library
{
    public partial class GridForm : Form
    {
        public GridForm()
        {
            this.InitializeComponent();

        }

        //public string ParentContentKey = "";

        //public ApplicationWebService service;

        //public IApplicationWebService service;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Text = this.Owner.Text + "/" + xservice.ParentContentKey;
            else
                this.Text = "/";

            this.label4.Text = xservice.ParentContentKey;

            dataGridView1.Enabled = false;


            this.xservice.AtConsole = Console.Write;

            Console.WriteLine("service.__grid_SelectContent");
            InternalCellValueChanged = true;
            xservice.__grid_SelectContent("",
                (ContentKey, ContentValue, ContentComment, ContentChildren) =>
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
                        },
                          new DataGridViewButtonCell
                        {
                            Value = ContentChildren + " Children"
                        }
                    );

                    dataGridView1.Rows.Add(r);

                },
                //ParentContentKey: ParentContentKey,
                AtTransactionKey: value =>
                {
                    LocalTransactionKey = value;
                    label2.Text = LocalTransactionKey;
                    dataGridView1.Enabled = true;

                    InternalCellValueChanged = false;

                    if (checkBox1.Checked)
                        timer1.Start();

                },
                AtError: message =>
                {
                    var f = new ErrorNotificationForm();

                    f.textBox1.Text = message;
                    f.Owner = this;

                    Console.WriteLine(new { message });

                    //                    script: error JSC1000: No implementation found for this native method, please implement [static System.Media.SystemSounds.get_Exclamation()]
                    //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
                    //script: error JSC1000: error at SQLiteWithDataGridView.Library.GridForm.<Form1_Load>b__6, type: SQLiteWithDataGridView.Library.GridForm offset: 0x0020  method:Void <Form1_Load>b__6(System.String)
                    //*** Compler cannot continue... press enter to quit.

                    SystemSounds.Beep.Play();
                    f.Show();
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
            label5.Text = new { e.RowIndex, e.ColumnIndex }.ToString();

            // default cell style changed
            if (e.RowIndex < 0)
                return;

            if (InternalCellValueChanged)
                return;

            var c0 = dataGridView1[0, e.RowIndex];

            if ((string)dataGridView1[0, e.RowIndex].Value == "?" + dataGridView1.Rows.Count)
                return;

            if (string.IsNullOrEmpty((string)c0.Value))
            {
                #region GridExample_AddItem
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

                var st = new Stopwatch();
                st.Start();

                xservice.GridExample_AddItem(
                    ContentValue,
                    ContentComment,
                    //ParentContentKey,
                    AtContentReferenceKey: LastInsertRowId =>
                    {
                        st.Stop();
                        Console.WriteLine("service.GridExample_AddItem done in " + st.Elapsed);


                        dataGridView1[0, e.RowIndex].Value = LastInsertRowId;
                        dataGridView1[0, e.RowIndex].Style.ForeColor = Color.Green;

                        //var i = int.Parse(LocalTransactionKey);
                        //i++;

                        //LocalTransactionKey = i.ToString();
                    }
                );
                Console.WriteLine("service.GridExample_AddItem...");
                #endregion
            }
            else
            {
                var ContentKey = (string)dataGridView1[0, e.RowIndex].Value;

                // Primary Key is still pending. We cannot ad nor update. Wait? Ignore?
                if (ContentKey.StartsWith("?"))
                    return;

                dataGridView1[0, e.RowIndex].Style.ForeColor = Color.Red;

                var ContentValue = (string)dataGridView1[1, e.RowIndex].Value;
                if (ContentValue == null)
                    ContentValue = "";

                var ContentComment = (string)dataGridView1[2, e.RowIndex].Value;
                if (ContentComment == null)
                    ContentComment = "";

                var st = new Stopwatch();
                st.Start();

                Console.WriteLine("service.GridExample_UpdateItem...");
                xservice.GridExample_UpdateItem(
                    ContentKey,
                    ContentValue,
                    ContentComment,

                    AtTransactionKey: RemoteTransactionKey =>
                    {
                        st.Stop();
                        Console.WriteLine("service.GridExample_UpdateItem done in " + st.ElapsedMilliseconds + "ms");
                        dataGridView1[0, e.RowIndex].Style.ForeColor = Color.Green;

                        //var i = int.Parse(LocalTransactionKey);
                        //i++;

                        //LocalTransactionKey = i.ToString();
                    }
                );

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].IsNewRow)
                return;

            var ContentKey = (string)dataGridView1[0, e.RowIndex].Value;

            var f = new GridForm
            {
                Owner = this,
                //service = service,
                //ParentContentKey = ContentKey,
                StartPosition = FormStartPosition.Manual
            };

            f.xservice.ParentContentKey = ContentKey;

            f.Location = new Point(this.Left, this.Top + 32);

            f.Show();

        }

        string LocalTransactionKey;

        int TimerCounter = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimerCounter++;
            timer1.Stop();

            var st = new Stopwatch();
            st.Start();

            Action<string> AtServerTransactionKey =
                async ServerTransactionKey =>
                {
                    label2.Text = ServerTransactionKey;

                    st.Stop();
                    Console.WriteLine("#" + TimerCounter + " AtServerTransactionKey done in " + st.ElapsedMilliseconds + "ms");

                    if (LocalTransactionKey != ServerTransactionKey)
                    {
                        label2.ForeColor = Color.Green;

                        #region AtContentKey
                        Action<string, string, string, string> AtContentKey =
                            (ContentKey, ContentValue, ContentComment, ContentChildren) =>
                            {
                                DataGridViewRow r = InternalAddOrUpdateToLocalDataGrid(ContentKey, ContentValue, ContentComment, ContentChildren);

                                r.Cells[0].Style.ForeColor = Color.Green;
                            };
                        #endregion


                
                        await xservice.GridExample_EnumerateItemsChangedBetweenTransactions(
                            LocalTransactionKey,
                            ServerTransactionKey,
                            AtContent: AtContentKey
                        );

                        LocalTransactionKey = ServerTransactionKey;
                        label2.Text = ServerTransactionKey;
                        label2.ForeColor = Color.Black;

                        if (checkBox1.Checked)
                            timer1.Start();

                        // we need updates!
                        return;
                    }


                    if (checkBox1.Checked)
                        timer1.Start();
                };

            //Console.WriteLine("#" + TimerCounter + " service.GridExample_GetTransactionKeyFor");

            xservice.GridExample_GetTransactionKeyFor(
                e: "",
                y: AtServerTransactionKey
            );
        }

        private DataGridViewRow InternalAddOrUpdateToLocalDataGrid(string ContentKey, string ContentValue = "", string ContentComment = "", string ContentChildren = "0")
        {
            DataGridViewRow r = this.dataGridView1.Rows.AsEnumerable().FirstOrDefault(
                item => (string)item.Cells[0].Value == ContentKey
            );

            if (r != null)
                if (string.IsNullOrEmpty((string)r.Cells[0].Value))
                    r = null;

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
                    },
                       new DataGridViewButtonCell
                       {
                           Value = ContentChildren + " Children"
                       }
                );

                //No row can be added to a DataGridView control that does not have columns. Columns must be added first.
                dataGridView1.Rows.Add(r);

            }
            else
            {
                InternalCellValueChanged = true;

                r.Cells[1].Value = ContentValue;
                r.Cells[2].Value = ContentComment;
                r.Cells[3].Value = ContentChildren + " Children";

                InternalCellValueChanged = false;

            }

            if (this.checkBox3.Checked)
                if (ContentComment == ContentComment_Form_Location)
                {
                    if (ContentValue.Contains(";"))
                    {
                        var x = int.Parse(ContentValue.TakeUntilIfAny(";"));
                        var y = int.Parse(ContentValue.SkipUntilLastIfAny(";"));

                        if (this.Owner != null)
                        {
                            x += this.Owner.Location.X;
                            y += this.Owner.Location.Y;
                        }

                        this.Location = new Point(x, y);
                    }
                }

            return r;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = this.checkBox1.Checked;
        }

        private void GridForm_LocationChanged(object sender, EventArgs e)
        {
            InternalAtLocationChanged();
        }

        public const string ContentComment_Form_Location = "Form.Location";

        int __counter;
        int __x;
        int __y;

        private void InternalAtLocationChanged()
        {
            if (__counter > 0)
            {
                var dx = this.Location.X - __x;
                var dy = this.Location.Y - __y;

                foreach (var item in this.OwnedForms)
                {
                    item.Location += new Size(dx, dy);
                }
            }
            __counter++;
            __x = this.Location.X;
            __y = this.Location.Y;

            if (checkBox2.Checked)
            {
                var ContentKey = "";

                DataGridViewRow r = this.dataGridView1.Rows.AsEnumerable().FirstOrDefault(
                  item => (string)item.Cells[2].Value == ContentComment_Form_Location
              );

                var x = this.Location.X;
                var y = this.Location.Y;

                if (this.Owner != null)
                {
                    x -= this.Owner.Location.X;
                    y -= this.Owner.Location.Y;
                }

                var ContentValue = x + ";" + y;
                if (r != null)
                    ContentKey = (string)r.Cells[0].Value;

                r = InternalAddOrUpdateToLocalDataGrid(
                    ContentKey: ContentKey,
                    ContentComment: ContentComment_Form_Location
                );

                r.Cells[1].Value = ContentValue;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            InternalAtLocationChanged();

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox4.Checked)
                this.timer1.Interval = 100;
            else
                this.timer1.Interval = 1000;

        }

        private void GridForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.timer1.Stop();
        }



    }

}
