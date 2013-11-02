using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace WebNotificationsViaDataAdapter.Schema
{
    public partial class FooTableDesigner : UserControl
    {
        public FooTableDesigner()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                var i = 0;
                var value =
                    (string)e.FormattedValue;

                var ok = int.TryParse(value, out i);

                if (ok)
                {
                    this.dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
                    return;
                }

                this.dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Yellow;
                e.Cancel = true;
            }
        }

        private async void FooTableDesigner_Load(object sender, EventArgs e)
        {
            //at System.Data.SQLite.SQLite3.Open(String strFilename, SQLiteConnectionFlags connectionFlags, SQLiteOpenFlagsEnum openFlags, Int32 maxPoolSize, Boolean usePool)
            //at System.Data.SQLite.SQLiteConnection.Open()
            //at WebNotificationsViaDataAdapter.Schema.FooTable.<>c__DisplayClassb.<.ctor>b__1(Action`1 y) in x:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\Schema\FooTable.cs:line 34
            //at WebNotificationsViaDataAdapter.Schema.FooTable..ctor(String DataSource) in x:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\Schema\FooTable.cs:line 50
            //at WebNotificationsViaDataAdapter.ApplicationWebService..ctor() in x:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\ApplicationWebService.cs:line 22 

            var xdata = await this.applicationWebService1.__FooTable_Select();
            var data = xdata;

            this.dataGridView1.DataSource = data;



            this.save.Enabled = true;
            this.save.Click +=
                async delegate
                {
                    this.save.Enabled = false;

                    var zdata = await this.applicationWebService1.__FooTable_Insert(
                        Enumerable.ToArray(
                            from row in data.Rows.AsEnumerable()
                            let delay = (string)row["delay"]
                            let text = (string)row["text"]
                            where !string.IsNullOrEmpty(delay)
                            where !string.IsNullOrEmpty(text)
                            select new FooTable.InsertFoo
                            {
                                delay = int.Parse(delay),
                                text = text
                            }
                        )
                    );
                    data = zdata;


                    this.dataGridView1.DataSource = data;
                    this.save.Enabled = true;
                };

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
