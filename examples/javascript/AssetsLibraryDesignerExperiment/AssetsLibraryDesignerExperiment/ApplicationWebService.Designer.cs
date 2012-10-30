using AssetsLibraryDesignerExperiment.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetsLibraryDesignerExperiment
{
    [Designer(typeof(Class1Designer))]
    partial class ApplicationWebService
    {
        // jsc should
        // detect System.Drawing references for serverside?
        // this will only work with GAE
        ScriptCoreLibJava.Drawing.IAssemblyReferenceToken ref0;

        private Components.Class2 class21;

        public ApplicationWebService()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.class21 = new AssetsLibraryDesignerExperiment.Components.Class2();

        }
    }
}
