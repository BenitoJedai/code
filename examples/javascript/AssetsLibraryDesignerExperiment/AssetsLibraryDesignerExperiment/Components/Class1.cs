using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AssetsLibraryDesignerExperiment.Components
{
    //[Designer(typeof(Class1Designer))]
    [Designer(typeof(Class1Designer))]
    public class Class1 : Component
    {
        // http://msdn.microsoft.com/en-us/library/system.componentmodel.design.componentdesigner.aspx#Y0
        // http://www.codeproject.com/Articles/37103/Customizing-User-Controls-with-Smart-Tag-Feature
        // http://msdn.microsoft.com/en-us/library/system.windows.forms.design.controldesigner.aspx

        public string Foo { get; set; }

        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }

        private void InitializeComponent()
        {

        }
    }

}
