using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssetsLibraryDesignerExperiment.Components
{
    [Designer(typeof(UserControlDesigner))]
    public partial class UserControl2 : UserControl, IDropZone
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        //define a property called "DropZone"
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel DropZone
        {
            get { return userControl12.DropZone; }
        }

        private void UserControl2_Load(object sender, EventArgs e)
        {

        }

        private void userControl11_DropZone_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
