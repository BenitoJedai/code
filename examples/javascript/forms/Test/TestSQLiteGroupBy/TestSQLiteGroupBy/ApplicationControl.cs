using TestSQLiteGroupBy;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using TestSQLiteGroupBy.Data;

namespace TestSQLiteGroupBy
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {


        }

        private async void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            var w = await this.applicationWebService1.WebMethod2();

            //this.ParentForm.Text = new { Groups = w.Count() }.ToString();

            //Book1
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingSource.DataSource = w.AsDataTable();


            //w.FirstOrDefault().With(
            //    f =>
            //    {
            //        this.ParentForm.Text = new { 
            //            f.SumOfx,

            //            f.FirstKey
            //            f.Firstx,
            //            f.FirstTitle, 

            //            f.LastTitle,
            //            f.las
            //        }.ToString();
            //    }
            //);
        }

    }
}
