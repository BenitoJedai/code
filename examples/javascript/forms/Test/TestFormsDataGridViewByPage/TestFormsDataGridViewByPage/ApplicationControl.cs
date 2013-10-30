using TestFormsDataGridViewByPage;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestFormsDataGridViewByPage
{
    public partial class ApplicationControl : UserControl
    {
        int pagenum = 1;
        int maxnum = 0;
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var table = await this.applicationWebService1.GetAllItemsCount();
            maxnum = table;
            this.PageCountLabel.Text = ""+table;

            var page = await this.applicationWebService1.GetAllItemsFromDB(pagenum);
            this.dataGridView1.DataSource = page;
            this.PageNumberLabel.Text = ""+pagenum;
        }

        private async void PrevPageLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (pagenum > 1)
            {
                pagenum = pagenum - 1;
                this.PageNumberLabel.Text = ""+pagenum;
                var temp = await this.applicationWebService1.GetAllItemsFromDB(pagenum);
                this.dataGridView1.DataSource = temp;
            }
        }

        private async void NextPageLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (pagenum < maxnum)
            {
                pagenum = pagenum + 1;
                this.PageNumberLabel.Text = ""+pagenum;
                var temp = await this.applicationWebService1.GetAllItemsFromDB(pagenum);
                this.dataGridView1.DataSource = temp;
            }

        }

    }
}
