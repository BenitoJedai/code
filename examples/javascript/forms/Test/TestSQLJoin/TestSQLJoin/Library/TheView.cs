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
    }
}
