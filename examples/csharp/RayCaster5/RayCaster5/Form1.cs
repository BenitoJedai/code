using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib;

namespace RayCaster5
{
    [Script]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        RayCaster4.ActionScript.RayCaster4base Virtual;

        private void Form1_Load(object sender, EventArgs e)
        {
            Virtual = new RayCaster4.ActionScript.RayCaster4base();
            Virtual.txtMain = label1;
            Virtual.prepare();
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(
                Virtual.screen,
                0,
                0,

                Virtual.screen.Width * 2,
                Virtual.screen.Height * 2);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Virtual.fKeyUp.ProcessKeyDown((int)e.KeyCode);
            Virtual.fKeyDown.ProcessKeyDown((int)e.KeyCode);
            Virtual.fKeyLeft.ProcessKeyDown((int)e.KeyCode);
            Virtual.fKeyRight.ProcessKeyDown((int)e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Virtual.fKeyUp.ProcessKeyUp((int)e.KeyCode);
            Virtual.fKeyDown.ProcessKeyUp((int)e.KeyCode);
            Virtual.fKeyLeft.ProcessKeyUp((int)e.KeyCode);
            Virtual.fKeyRight.ProcessKeyUp((int)e.KeyCode);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Virtual.render(null);
            Refresh();
        }
    }
}
