using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakeWindowsLoginExperiment.Library.Designers.Samples
{
    // This sample demonstrates how to provide the root designer view, or 
    // design mode background view, by overriding IRootDesigner.GetView(). 

    // This sample component inherits from RootDesignedComponent which  
    // uses the SampleRootDesigner. 
    public class RootViewSampleComponent : RootDesignedComponent
    {
        public RootViewSampleComponent()
        {
        }
    }



 }
