using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WithClickOnceLANLauncherTabClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            var f = new WithClickOnceLANLauncherClient.FindServiceProviderOverMulticastForm();


            f.MissionAcomplished +=
                u =>
                {
                    f.progressBar1.Style = ProgressBarStyle.Continuous;

                    this.webBrowser1.DocumentTitleChanged +=
                        delegate
                        {
                            //this.Text = this.webBrowser1.DocumentText;
                            this.Text = this.webBrowser1.DocumentTitle;
                        };

                    this.webBrowser1.ProgressChanged +=
                        (ss, ee) =>
                        {
                            f.progressBar1.Maximum = (int)ee.MaximumProgress;
                            f.progressBar1.Value = (int)ee.CurrentProgress;
                        };

                    this.webBrowser1.DocumentCompleted +=
                        delegate
                        {
                            f.Close();
                        };

                    this.webBrowser1.Navigate(u);
                };

            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);


        }
    }
}
