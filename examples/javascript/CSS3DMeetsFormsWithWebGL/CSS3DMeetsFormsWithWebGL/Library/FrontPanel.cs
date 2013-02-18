using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSS3DMeetsFormsWithWebGL.Library
{
    public partial class FrontPanel : UserControl
    {
        public FrontPanel()
        {
            InitializeComponent();
        }

        private void FrontPanel_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var url = this.comboBox1.Text;

            CreateWindowAndNavigate(url);
        }

        public Func<string, string> RelativeToAbsolute = e => e;

        public void CreateWindowAndNavigate(string url)
        {
            var f = new Form
            {

                Text = url
            };
            var c = new WebBrowser();
            c.Dock = DockStyle.Fill;
            //c.Size = new System.Drawing.Size(400, 300);
            f.Controls.Add(c);
            f.ClientSize = new System.Drawing.Size(400, 300);
            c.Navigate(
                RelativeToAbsolute(url)
                );
            //c.Url = new Uri(this.comboBox1.Text);
            f.Show();


            if (NewForm != null)
                NewForm(f);
        }

        public event Action<Form> NewForm;
    }
}
