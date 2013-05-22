using TestDragDrop;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.Threading;
using System;

namespace TestDragDrop
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            label1.Text = "DragOver";


        }

        private void ApplicationControl_DragDrop(object sender, DragEventArgs e)
        {
            label1.Text = "DragDrop = " + e.Data.GetData("Text");

            e.Data.GetFormats().WithEach(
                f =>
                {
                    label1.Text += "\n" + f;
                }
            );
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }

        private void ApplicationControl_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseMove " + new { e.Button });

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                label1.Text = "MouseMove";
                this.DoDragDrop("hello", DragDropEffects.Copy);
                label1.Text = "DoDragDrop";

            }
        }

        private void ApplicationControl_DragLeave(object sender, System.EventArgs e)
        {
            label1.Text = "DragLeave";

        }

    }
}
