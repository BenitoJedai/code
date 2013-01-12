using Abstractatech.ConsoleFormPackage;
using Abstractatech.ConsoleFormPackage.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Abstractatech.ConsoleFormPackage
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            new ConsoleForm().InitializeConsoleFormWriter().Show();

            Console.WriteLine("hey there");
        }

    }
}
