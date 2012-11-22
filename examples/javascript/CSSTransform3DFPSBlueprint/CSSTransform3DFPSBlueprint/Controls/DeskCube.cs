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
    public partial class DeskCube : UserControl
    {
        [DefaultValue(250)]
        public int CubeHeight { get; set; }

        public string LeftWallSource { get; set; }

        public DeskCube()
        {
            InitializeComponent();
        }
    }
}
