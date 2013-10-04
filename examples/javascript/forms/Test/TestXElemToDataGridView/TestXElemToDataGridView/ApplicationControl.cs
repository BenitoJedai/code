using TestXElemToDataGridView;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

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
            bool first = true;

                foreach (var i in xElem.Descendants("Query")) 
                {
                    if (first)
                    {
                        i.Elements().ToList().ForEach(element => dataGridView1.Columns.Add(element.Name.ToString(), element.Name.ToString()));
                        first = false;
                    }
                    DataGridViewRow row = new DataGridViewRow();
                    foreach (var elem in i.Elements())
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell { Value = elem.Value });
                    }
                    dataGridView1.Rows.Add(row);
                    Console.WriteLine(i);
                }
        }

    }
}
