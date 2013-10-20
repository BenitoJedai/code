using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MichaelVincentProgramManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        //Form1_LocationChanged
        //Form1_LocationChanged FormBorderStyle <- Sizable
        //Form1_LocationChanged
        //Form1_LocationChanged FormBorderStyle <- None
        //Form1_LocationChanged WindowState <- Normal
        //Form1_LocationChanged
        //Form1_LocationChanged InternalGoingFullscreen
        //Form1_LocationChanged WindowState <- Maximized
        //Form1_LocationChanged
        //Form1_LocationChanged InternalGoingFullscreen

        //Form1_LocationChanged
        //Form1_LocationChanged FormBorderStyle <- Sizable
        //Form1_LocationChanged
        //Form1_LocationChanged FormBorderStyle <- None
        //Form1_LocationChanged WindowState <- Normal
        //Form1_LocationChanged
        //Form1_LocationChanged InternalGoingFullscreen
        //Form1_LocationChanged WindowState <- Maximized
        //Form1_LocationChanged
        //Form1_LocationChanged InternalGoingFullscreen
        //Form1_LocationChanged
        //Form1_LocationChanged FormBorderStyle <- Sizable



        bool InternalGoingFullscreen;
        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("Form1_LocationChanged");

            //if (InternalGoingFullscreen)
            //{
            //    Console.WriteLine("Form1_LocationChanged InternalGoingFullscreen");
            //    return;
            //}

            //if (this.WindowState == FormWindowState.Maximized)
            //{
            //    if (this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.None)
            //    {
            //        InternalGoingFullscreen = true;
            //        this.MaximumSize = this.DefaultMaximumSize;

            //        Console.WriteLine("Form1_LocationChanged FormBorderStyle <- None");
            //        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            //        Console.WriteLine("Form1_LocationChanged WindowState <- Normal");
            //        this.WindowState = FormWindowState.Normal;
            //        Console.WriteLine("Form1_LocationChanged WindowState <- Maximized");
            //        this.WindowState = FormWindowState.Maximized;

            //        InternalGoingFullscreen = false;
            //    }
            //}
            //else if (this.WindowState == FormWindowState.Normal)
            //{
            //    Console.WriteLine("Form1_LocationChanged FormBorderStyle <- Sizable");
            //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            //}
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Form1_Click");
            //this.WindowState = FormWindowState.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button1_Click enter");
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
            Console.WriteLine("button1_Click exit");

            ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.InternalDiagnostics.BreakAtWindowState = true;
        }






    }

}
