using FormsTreeViewDrag;
using System;
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
            // DragDrop.DoDragDrop returns only after the complete drag-drop process is finished,
            // http://w3facility.org/question/dodragdrop-freezes-winforms-app-sometimes/
            // https://msdn.microsoft.com/en-us/library/ms649011(VS.85).aspx

            // http://www.codeproject.com/Articles/17266/Drag-and-Drop-Items-in-a-WPF-ListView

            Console.WriteLine("treeView1_ItemDrag"); ;

            //this.DoDragDrop("treeView1_ItemDrag " + new { e.Item }, DragDropEffects.Copy);
            treeView1.DoDragDrop("treeView1_ItemDrag " + new { e.Item }, DragDropEffects.Copy);

        }

        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.Control.add_DragEnter(System.Windows.Forms.DragEventHandler)]
        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("treeView1_DragDrop " + new { data = e.Data.GetData(typeof(string)) });


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

        private void treeView1_DragOver(object sender, DragEventArgs e)
        {
            Console.WriteLine("treeView1_DragOver " + new { data = e.Data.GetData(typeof(string)) });

            e.Effect = DragDropEffects.Copy;
        }
    }
}
