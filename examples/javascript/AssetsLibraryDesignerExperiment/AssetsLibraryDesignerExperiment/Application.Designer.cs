using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetsLibraryDesignerExperiment
{
    partial class Application
    {
        private Components.Class1 class11;

        public Application()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.class11 = new AssetsLibraryDesignerExperiment.Components.Class1();
            this.class21 = new AssetsLibraryDesignerExperiment.Components.Class2();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.applicationWebService1 = new AssetsLibraryDesignerExperiment.ApplicationWebService();
            this.button1 = new System.Windows.Forms.Button();
            // 
            // class11
            // 
            this.class11.BackColor = System.Drawing.Color.Empty;
            this.class11.Foo = null;
            this.class11.ForeColor = System.Drawing.Color.Empty;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);

        }

        private Components.Class2 class21;

        private System.Windows.Forms.Timer timer1;
        private IContainer components;
        private ApplicationWebService applicationWebService1;
        private System.Windows.Forms.Button button1;
    }
}
