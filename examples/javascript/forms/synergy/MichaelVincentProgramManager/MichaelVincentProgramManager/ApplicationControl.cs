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
        public event Action<Button, Control> WhenClickedGoPopup;

        private void button1_Click(object sender, System.EventArgs e)
        {
    
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
      
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var f = new Form1
            {
                Width = 800,
                Height = 400
            };

            f.Shown +=
                delegate
                {
                    //if (Augment != null)
                    //    Augment(f.panel1);


                    //if (WhenClickedGoFullscreen != null)
                    //    WhenClickedGoFullscreen(f.button1, f.webBrowser1);

                    //if (WhenClickedGoPopup!= null)
                    //    WhenClickedGoPopup(f.button2, f.webBrowser1);
                };

            f.Show();


            if (NewForm != null)
                NewForm(f);
        }

        public event Action<Form1> NewForm;
    }
}
