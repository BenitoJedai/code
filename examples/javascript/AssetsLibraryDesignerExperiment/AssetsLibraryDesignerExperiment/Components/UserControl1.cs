using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AssetsLibraryDesignerExperiment.Components
{
    [Designer(typeof(UserControlDesigner))]
    public partial class UserControl1 : UserControl
    {
        // http://stackoverflow.com/questions/2694889/user-control-as-container-at-design-time

        public UserControl1()
        {
            InitializeComponent();
        }


        // define a property called "DropZone"
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel DropZone
        {
            get { return panel2; }
        }
    }

   

}
