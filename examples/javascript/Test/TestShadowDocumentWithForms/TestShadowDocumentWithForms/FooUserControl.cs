using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestShadowDocumentWithForms
{
    public class FooUserControl : UserControl
    {
        private Button button1;


        public FooUserControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "dock bar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FooUserControl
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.button1);
            this.Name = "FooUserControl";
            this.Size = new System.Drawing.Size(739, 57);
            this.ResumeLayout(false);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // where are we going to appear? inside shadow or body?

            new FooForm().Show();
        }
    }
}
