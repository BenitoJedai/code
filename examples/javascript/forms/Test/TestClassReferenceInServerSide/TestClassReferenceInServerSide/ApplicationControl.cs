using TestClassReferenceInServerSide;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SearchComponent;

namespace TestClassReferenceInServerSide
{
    public partial class ApplicationControl : UserControl
    {
        SearchComponentClass search = new SearchComponentClass(); 
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

    }
}
