using FormsSVGTreeView;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsSVGTreeView
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //e.Node.Text += " click!";


            e.Node.Nodes.Add("click " + new { e.Node.Nodes.Count });


        }


    }
}
