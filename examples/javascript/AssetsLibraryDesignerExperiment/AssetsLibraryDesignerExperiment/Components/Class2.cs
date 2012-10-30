using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AssetsLibraryDesignerExperiment.Components
{
    [Designer(typeof(Class1Designer))]
    public class Class2 : Component
    {
        private Class1 foo_sdf_sdf;
    
        public Class2()
        {

        }

        private void InitializeComponent()
        {
            this.foo_sdf_sdf = new AssetsLibraryDesignerExperiment.Components.Class1();
            // 
            // foo_sdf_sdf
            // 
            this.foo_sdf_sdf.BackColor = System.Drawing.Color.Silver;
            this.foo_sdf_sdf.Foo = null;
            this.foo_sdf_sdf.ForeColor = System.Drawing.Color.Red;

        }
    }
}
