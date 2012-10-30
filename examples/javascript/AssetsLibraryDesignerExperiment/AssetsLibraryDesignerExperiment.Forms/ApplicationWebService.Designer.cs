using AssetsLibraryDesignerExperiment.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AssetsLibraryDesignerExperiment.Forms
{
    [Designer(typeof(Class1Designer))]
    partial class ApplicationWebService : Component
    {


    
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
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
        private IContainer components;
        public System.Windows.Forms.Button button1;
    }
}
