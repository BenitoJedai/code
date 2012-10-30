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
        private Class1 class11;
    
        public Class2()
        {

        }

        private void InitializeComponent()
        {
            this.class11 = new AssetsLibraryDesignerExperiment.Components.Class1();
            // 
            // class11
            // 
            this.class11.BackColor = System.Drawing.Color.Silver;
            this.class11.Foo = null;
            this.class11.ForeColor = System.Drawing.Color.Red;

        }
    }
}
