using FormsTreeViewDrag;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsTreeViewDrag
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // gx2. jsx, jsc reflector
            // ?

            // http://support.microsoft.com/kb/307968

            // we can drag it into scite
            this.DoDragDrop("treeView1_ItemDrag " + new { e.Item }, DragDropEffects.Copy);

        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            //Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            //TreeNode DestinationNode = ((TreeView)sender).GetNodeAt(pt);
            //NewNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            //if (DestinationNode.TreeView != NewNode.TreeView)
            //{
            //    DestinationNode.Nodes.Add((TreeNode)NewNode.Clone());
            //    DestinationNode.Expand();
            //    //Remove Original Node
            //    NewNode.Remove();
            //}

            var text = e.Data.GetData(typeof(string)) as string;

            treeView1.Nodes.Add(text);
        }
    }
}
