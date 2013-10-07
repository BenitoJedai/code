using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace com.abstractatech.gamification.synergy.cashflow
{
    partial class Application : UserControl
    {
        public Application()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.service = new com.abstractatech.gamification.synergy.cashflow.ApplicationWebService();
            this.SuspendLayout();
            // 
            // Application
            // 
            this.Name = "Application";
            this.Size = new System.Drawing.Size(587, 356);
            this.Load += new System.EventHandler(this.Application_Load);
            this.ResumeLayout(false);

        }

        private ApplicationWebService service;

    }
}
