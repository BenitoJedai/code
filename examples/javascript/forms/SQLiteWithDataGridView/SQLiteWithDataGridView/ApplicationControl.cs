using Abstractatech.ConsoleFormPackage.Library;
using SQLiteWithDataGridView;
using SQLiteWithDataGridView.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLiteWithDataGridView
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }


        public ConsoleForm con = new ConsoleForm();

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

            con.InitializeConsoleFormWriter();
            con.Show();

            Console.WriteLine("Console has been redirected!");
        }


        private void Table001_Click(object sender, System.EventArgs e)
        {
            var f = new GridForm { service = this.applicationWebService1 };


            f.Show();


            if (NewForm != null)
                NewForm(f);
        }

        public event Action<GridForm> NewForm;

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

    }
}
