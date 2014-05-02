using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestSQLJoin.Library
{
    [DefaultEvent("AtRefresh")]
    public partial class TheView : UserControl, IComponent
    {
        public TheView()
        {
            InitializeComponent();
        }

        public event Action AtRefresh;

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (AtRefresh != null)
                AtRefresh();

        }

        private void book1TheViewBindingSourceBindingSource_DataSourceChanged(object sender, EventArgs e)
        {
            Console.WriteLine("book1TheViewBindingSourceBindingSource_DataSourceChanged");

            //this.book1TheViewBindingSourceDataGridView.DataSource = null;
            //this.book1TheViewBindingSourceDataGridView.DataSource = book1TheViewBindingSourceBindingSource.DataSource;

            // ActivaDataSource gets in the way?
            //this.book1TheViewBindingSourceDataGridView.DataSource = book1TheViewBindingSourceBindingSource;
        }

        private void book1TheViewBindingSourceBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
