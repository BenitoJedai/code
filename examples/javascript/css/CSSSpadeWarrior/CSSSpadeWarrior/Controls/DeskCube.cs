using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSSSpadeWarrior.Controls
{
    public partial class DeskCube : UserControl
    {
        //[DefaultValue(250)]
        public int CubeHeight { get; set; }

        public string LeftWallSource { get; set; }

        public event Action LeftWallSourceAutoLoadChanged;

        bool InternalLeftWallSourceAutoLoad;
        public bool LeftWallSourceAutoLoad
        {
            get
            { return this.InternalLeftWallSourceAutoLoad; }
            set
            {
                this.InternalLeftWallSourceAutoLoad = value;
                if (LeftWallSourceAutoLoadChanged != null)
                    LeftWallSourceAutoLoadChanged();
            }
        }

        public DeskCube()
        {
            InitializeComponent();
        }
    }
}
