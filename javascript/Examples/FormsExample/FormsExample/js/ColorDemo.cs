using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib;

namespace FormsExample.js
{
    [Script]
    public partial class ColorDemo : UserControl
    {
        public ColorDemo()
        {
            InitializeComponent();
        }

        public Color ColorPanel { get { return ColorPanelControl.BackColor; } set { ColorPanelControl.BackColor = value; } }
        public string ColorName { get { return ColorNameControl.Text; } set { ColorNameControl.Text = value; } }
    }
}
