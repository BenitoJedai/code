using AndroidBroadcastLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace AndroidBroadcastLogger
{
    public partial class ApplicationControl : UserControl
    {
        // jsc, this looks too complex?

        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void bindingSource1_CurrentChanged(object sender, System.EventArgs e)
        {

        }

        public List<DataTable> History = new List<DataTable>();

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var DataSource = new MyDataSource();


            while (true)
            {
                await Task.Delay(500);



                // leech only
                if (this.checkBox1.Checked)
                {
                    DataSource = await applicationWebService1.DataSource_poll(DataSource);

                    button1.Text =
                        new { DataSource.last_id }.ToString();

                    // Set-Cookie:InternalFields=field_DataSource=<_02000006>%0d%0a  <_0400000a>1</_0400000a>%0d%0a  <_0400000b>&lt;DataTable TableName=""&gt;%0d%0a  &lt;Columns&gt;%0d%0a    &lt;DataColumn ReadOnly="true"&gt;xml&lt;/DataColumn&gt;%0d%0a  &lt;/Columns&gt;%0d%0a  &lt;DataRow&gt;%0d%0a    &lt;DataColumn&gt;&amp;lt;fake&amp;gt;data { last_id = 0, Count = 0 }&amp;lt;/fake&amp;gt;&lt;/DataColumn&gt;%0d%0a  &lt;/DataRow&gt;%0d%0a&lt;/DataTable&gt;</_0400000b>%0d%0a  <_0400000c>1000</_0400000c>%0d%0a  <_0400000d>10</_0400000d>%0d%0a  <_0400000e>30</_0400000e>%0d%0a</_02000006>;

                    if (DataSource.data != null)
                    {
                        Console.WriteLine("got DataSource.data");

                        var value = DataSource.data;
                        this.History.Add(value);
                        DataSource.data = null;

                        SetBindingSource();

                    }
                }

                await Task.Delay(1500);
            }
        }

        private void SetBindingSource()
        {
            var mydata = new DataTable();

            var SourceRows = this.History.SelectMany(x => x.Rows.AsEnumerable()).Reverse();

            foreach (var SourceRow in SourceRows)
            {
                var myrow = mydata.NewRow();

                foreach (var SourceColumn in SourceRow.Table.Columns.AsEnumerable())
                {
                    var mycolumn = mydata.Columns.AsEnumerable().FirstOrDefault(x => x.ColumnName == SourceColumn.ColumnName);


                    if (mycolumn == null)
                    {
                        mycolumn = new DataColumn
                        {
                            ColumnName = SourceColumn.ColumnName,
                            ReadOnly = SourceColumn.ReadOnly
                        };

                        mydata.Columns.Add(mycolumn);
                    }


                    myrow[mycolumn] = SourceRow[SourceColumn];
                }

                mydata.Rows.Add(myrow);
            }

            this.dataGridView1.DataSource = mydata;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await applicationWebService1.DataSource_addfake();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new Form();

            var c = new ApplicationControl();
            c.Dock = DockStyle.Fill;

            f.Controls.Add(c);

            f.Show();


        }

    }
}
