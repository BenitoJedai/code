using BindingXElementToDataGridView;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BindingXElementToDataGridView
{
    public partial class ApplicationControl : UserControl
    {
        // http://www.java2s.com/Tutorial/CSharp/0541__XML-LINQ/BindingxmltoDataGridView.htm

        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var employees = new[] {
                new
                {
                    EmployeeID = "a",
                    FirstName = "b",
                    LastName = "c",
                    HomePhone = "d",
                    Notes = "x"
                }
            };

            dataGridView1.DataSource = employees.ToArray();
        }

    }
}
