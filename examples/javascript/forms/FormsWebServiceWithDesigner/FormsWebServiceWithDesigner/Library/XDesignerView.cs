using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsWebServiceWithDesigner.Library
{
    public partial class XDesignerView : UserControl
    {

        public XComponentDesigner Designer { get; set; }


        public XDesignerView()
        {
            InitializeComponent();
        }

        private void XDesignerView_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
