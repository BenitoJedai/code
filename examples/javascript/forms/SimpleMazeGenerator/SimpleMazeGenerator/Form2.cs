﻿using ScriptCoreLib.Shared.Maze;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SimpleMazeGenerator
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            this.InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        public void CreateMaze(int _w, int _h)
        {
            var maze = new MazeGenerator(_w, _h, null);

            var w = new BlockMaze(maze);

            for (var y = 0; y < w.Height; y++)
            {
                Console.Write(new string(' ', 8));

                for (var x = 0; x < w.Width; x++)
                {
                    var v = w.Walls[x][y];

                    var p = new Panel();

                    p.SetBounds(16 + x * 16, 16 + y * 16, 15, 15);
                    if (!v)
                    {
                        p.BackColor = Color.White;
                    }
                    else
                    {
                        p.BackColor = Color.Black;
                    }

                    this.Controls.Add(p);



                }
            }

            this.ClientSize = new Size(16 * (w.Width + 2), 16 * (w.Height + 2));
        }

    }

}
