using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FormsExample.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            var f = new System.Windows.Forms.Form();

            
            var s = typeof(FormsExample.js.UserControl1).Assembly.GetManifestResourceStream("FormsExample.web.assets.WebCalculator.sc.ico");

            f.Icon = new Icon(s);
            f.BackgroundImage = Image.FromStream(s);
            f.BackgroundImageLayout = ImageLayout.None;

            f.Controls.Add(new FormsExample.js.UserControl1());

            f.ShowDialog();
            //new FormsExample.js.Class1(null);
        }
    }
}
