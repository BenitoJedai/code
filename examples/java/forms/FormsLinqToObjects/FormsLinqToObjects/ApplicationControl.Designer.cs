using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FormsLinqToObjects
{
    public partial class ApplicationControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //            symbol  : method Dispose_06000a29(boolean)
            //location: class ScriptCoreLibJava.BCLImplementation.System.Windows.Forms.__UserControl
            //        super.Dispose_06000a29(disposing);
            //             ^


            // Note: This jsc project does not support unmanaged resources.
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Name = @"ApplicationControl";
            this.Size = new Size(400, 300);
        }

    }
}
