using FormsTreeViewDrag;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

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

            // http://stackoverflow.com/questions/1772102/c-sharp-drag-and-drop-from-my-custom-app-to-notepad
            var x = new DataObject(
                "treeView1_ItemDrag " + new { e.Item }
            );


            x.SetData("text/nodes/0", "hello");

            // https://msdn.microsoft.com/en-us/library/system.windows.forms.control.dodragdrop(v=vs.110).aspx
            //this.DoDragDrop("treeView1_ItemDrag " + new { e.Item }, DragDropEffects.Copy);
            treeView1.DoDragDrop(x, DragDropEffects.Copy);

        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            foreach (var item in e.Data.GetFormats())
            {
                Console.WriteLine(new { item });

            }

            Console.WriteLine("treeView1_DragOver " + new { data = e.Data.GetData(typeof(string)) });

            e.Effect = DragDropEffects.Copy;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            // http://www.codemag.com/Article/0407031
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

            //var text = e.Data.GetData(typeof(string)) as string;
            var xDataObject = e.Data as DataObject;

            var n = treeView1.Nodes.Add(xDataObject.GetText());

            xDataObject.GetData("text/nodes/0").With(x =>
                n.Nodes.Add("" + x)
            );
        }

        private void treeView1_DragOver(object sender, DragEventArgs e)
        {
            foreach (var item in e.Data.GetFormats())
            {
                Console.WriteLine(new { item });

            }

            Console.WriteLine("treeView1_DragOver " + new { data = e.Data.GetData(typeof(string)) });



            e.Effect = DragDropEffects.Copy;
        }
    }
}
