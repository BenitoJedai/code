using FormsLinqToObjects;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsLinqToObjects
{
    //public partial class ApplicationControl : UserControl
    public partial class ApplicationControl : LinqToObjects.ApplicationControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

    }
}
