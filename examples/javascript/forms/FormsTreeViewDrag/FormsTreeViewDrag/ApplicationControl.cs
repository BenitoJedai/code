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
    }
}
