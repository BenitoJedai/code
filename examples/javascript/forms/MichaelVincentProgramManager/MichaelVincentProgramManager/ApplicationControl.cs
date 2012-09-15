using MichaelVincentProgramManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MichaelVincentProgramManager
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        public event Action<Button, Control> WhenClickedGoFullscreen;

        private void button1_Click(object sender, System.EventArgs e)
        {
    
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new Form1
            {
                Width = 800,
                Height = 600
            };

            f.Shown +=
                delegate
                {
                    //if (Augment != null)
                    //    Augment(f.panel1);


                    if (WhenClickedGoFullscreen != null)
                        WhenClickedGoFullscreen(f.button1, f.webBrowser1);
                };

            f.Show();
        }

    }
}
