using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsExample.js
{
    [ScriptCoreLib.Script]
    public partial class WebCalculator : UserControl
    {

        public WebCalculator()
        {
            InitializeComponent();


            var n = NumberButtons;

            for (int i = 0; i < n.Length; i++)
            {
                var v = n[i];

                v.Click +=
                    delegate
                    {
                        this.textBox1.Text += v.Text;
                    };
            }

        }

        public Button[] NumberButtons
        {
            get
            {
                return new[] { num0, num1, num2, num3, num4, num5, num6, num7, num8, num9 };
            }
        }

        public Button[] OperatorButtons
        {
            get
            {
                return new[] { div, sub, mul, add, eq };
            }
        }

        #region OperatorColor
        private Color _OperatorColor = Color.Red;

        public Color OperatorColor
        {
            get { return _OperatorColor; }
            set { _OperatorColor = value; UpdateOperatorColor(); }
        }



        void UpdateOperatorColor()
        {
            foreach (var v in OperatorButtons)
                v.ForeColor = _OperatorColor;
        }
        #endregion

        #region NumberColor
        private Color _NumberColor = Color.Blue;


        public Color NumberColor
        {
            get { return _NumberColor; }
            set { _NumberColor = value; UpdateNumberColor(); }
        }



        void UpdateNumberColor()
        {
            foreach (var v in NumberButtons)
                v.ForeColor = _NumberColor;
        }
        #endregion

        private void WebCalculator_Resize(object sender, EventArgs e)
        {
            gradient1.SetBounds(0, 0, Width, Height);
        }

        private void WebCalculator_MouseEnter(object sender, EventArgs e)
        {
            gradient1.GradientStartColor = Color.FromArgb(0xff, 0x00, 0x00);
        }

        private void WebCalculator_MouseLeave(object sender, EventArgs e)
        {
            gradient1.GradientStartColor = Color.FromArgb(0xff, 0xFF, 0xFF);
        }


        private void num9_Click(object sender, EventArgs e)
        {
            Console.WriteLine("9");

        }


        Point DragLocation;
        Point DragStart;
        int Drag;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mousedown");

            if (Drag == 0)
            {
                panel1.GradientStartColor = Color.FromArgb(0, 0, 0xff);

                DragLocation = this.Location;
                DragStart = e.Location;
            }

            Drag++;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Drag > 0)
            {
                Drag--;

                if (Drag == 0)
                {
                    panel1.GradientStartColor = Color.FromArgb(0, 0, 80);
                }
            }
        }


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mousemove" + new { e.X, e.Y, Drag }  );

            if (Drag > 0)
            {
                this.Location = new Point(

                            DragLocation.X + (e.X - DragStart.X),
                            DragLocation.Y + (e.Y - DragStart.Y)

                        );

                //Console.WriteLine("offset: " + p);

            }
        }


    }
}
