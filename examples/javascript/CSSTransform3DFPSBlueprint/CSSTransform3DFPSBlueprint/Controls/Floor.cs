using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSSTransform3DFPSBlueprint.Controls
{
    public partial class Floor : UserControl
    {
        public double Z { get; set; }

        public string LeftWallSource { get; set; }

        public Floor()
        {
            InitializeComponent();
        }
    }
}
