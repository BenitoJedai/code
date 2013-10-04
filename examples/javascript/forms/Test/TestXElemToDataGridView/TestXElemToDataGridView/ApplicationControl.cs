using TestXElemToDataGridView;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using ScriptCoreLib.Extensions;

namespace TestXElemToDataGridView
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
 
            var xElem = await this.applicationWebService1.GetQueryResultAsXElement();
            //xElem.ContinueWith(Task => {

                bool first = true;

                // JSC please support Descendants foreach (var i in Task.Result.Descendants("Query"))
                // Elements http://msdn.microsoft.com/en-us/library/bb342765.aspx
                // Descendants http://msdn.microsoft.com/en-us/library/system.xml.linq.xdocument.descendants.aspx


                //foreach (var i in Task.Result.Elements("Query"))
                //{
                //    if (first)
                //    {
                //        i.Elements().ToList().ForEach(element => dataGridView1.Columns.Add(element.Name.ToString(), element.Name.ToString()));
                //        first = false;
                //    }
                //    DataGridViewRow row = new DataGridViewRow();
                //    foreach (var elem in i.Elements())
                //    {
                //        row.Cells.Add(new DataGridViewTextBoxCell { Value = elem.Value });
                //    }
                //    dataGridView1.Rows.Add(row);
                //    Console.WriteLine(i);
                //}
                xElem.Elements("Query").WithEach(i =>
                {
                    if (first)
                    {
                        //i.Elements().ToList().ForEach(element => dataGridView1.Columns.Add(element.Name.ToString(), element.Name.ToString()));
                        i.Elements().ToArray().WithEach(element => dataGridView1.Columns.Add(element.Name.ToString(), element.Name.ToString()));
                        first = false;
                    }
                    var row = new DataGridViewRow();
                    foreach (var elem in i.Elements())
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell { Value = elem.Value });
                    }
                    dataGridView1.Rows.Add(row);
                    Console.WriteLine(i);
                });
           // });
            
        }

    }
}
