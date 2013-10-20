using TestDropHTML;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace TestDropHTML
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;

        }

        private void ApplicationControl_DragDrop(object sender, DragEventArgs e)
        {
            label1.Text = "DragDrop = " + e.Data.GetData("text/html");
            //label1.Text = "DragDrop = " + e.Data.GetData("HTML Format");
            e.Data.GetFormats().WithEach(
               f =>
               {
                   label1.Text += "\n" + f;
               }
           );
        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

    }
}
