using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormsExample.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = new System.Windows.Forms.Form();

            f.Controls.Add(new FormsExample.js.UserControl1());

            f.ShowDialog();
            //new FormsExample.js.Class1(null);
        }
    }
}
