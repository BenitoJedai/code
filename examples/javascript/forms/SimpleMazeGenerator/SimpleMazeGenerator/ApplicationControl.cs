using SimpleMazeGenerator;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleMazeGenerator
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var f = new Form2();

            f.CreateMaze(
                int.Parse(this.textBox1.Text),
                int.Parse(this.textBox2.Text)
                );

            f.Show();

        }

    }
}
