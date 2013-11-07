using FormsDataSet;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsDataSet
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var x = await this.applicationWebService1.Book1();

            this.dataGridView1.DataSource = x;

            this.comboBox1.SelectedValueChanged +=
                delegate
                {
                    this.dataGridView1.DataMember = this.comboBox1.Text;
                };

            foreach (DataTable item in x.Tables)
            {
                this.comboBox1.Items.Add(item.TableName);
            }

            //this.dataGridView1.DataMember = x.Tables[0].TableName;

        }

    }
}
