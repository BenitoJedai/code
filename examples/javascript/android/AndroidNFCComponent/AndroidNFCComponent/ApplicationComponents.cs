using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AndroidNFCComponent
{
    public partial class ApplicationComponents : UserControl
    {
        public ApplicationComponents()
        {
            Console.WriteLine("ApplicationComponents InitializeComponent");

            InitializeComponent();
        }

        private void xnfcComponent1_AtTagDiscovered(string obj)
        {
            Console.WriteLine("xnfcComponent1_AtTagDiscovered");
        }
    }
}
