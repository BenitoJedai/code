using TestComponent1;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestComponent1
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }
        int i = 0;

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.applicationWebService1.WebMethod2(
                "smth",
                y =>
                {
                    i++;

                    var r = new DataGridViewRow();

                    r.Cells.Add(
                        new DataGridViewTextBoxCell
                        {
                            Value = "#" + i
                        }
                    );

                    r.Cells.Add(
                        new DataGridViewTextBoxCell
                        {
                            Value = "foo1"
                        }
                    );

                    r.Cells.Add(
                         new DataGridViewTextBoxCell
                         {
                             Value = "foo2"
                         }
                     );

                    this.dataGridView1.Rows.Add(r);

                    //this.dataGridView1[0, 0].Value

                    button1.Text = y;
                }
            );
        }

    }
}
