using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.abstractatech.cloud.gallery.Controls
{
    public partial class DeskCube : UserControl
    {
        //[DefaultValue(250)]
        public int CubeHeight { get; set; }

        [Category("WallSource")]
        public string WallSourceRight { get; set; }
        [Category("WallSource")]
        public string WallSourceTop { get; set; }
        [Category("WallSource")]
        public string WallSourceBottom { get; set; }
        [Category("WallSource")]
        public string LeftWallSource { get; set; }

        [Category("WallSource")]
        public event Action LeftWallSourceAutoLoadChanged;

        bool InternalLeftWallSourceAutoLoad;
        [Category("WallSource")]
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
