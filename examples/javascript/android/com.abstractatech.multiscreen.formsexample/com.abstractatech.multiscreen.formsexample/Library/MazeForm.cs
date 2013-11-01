using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace com.abstractatech.multiscreen.formsexample.Library
{
    public partial class MazeForm : Form
    {
        public MazeForm()
        {
            this.InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.button1.ForeColor = Color.Blue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new SimpleMazeGenerator.Form2();

            f.CreateMaze(
                int.Parse(this.textBox1.Text),
                int.Parse(this.textBox2.Text)
                );

            f.Show();

        }



    }

}
