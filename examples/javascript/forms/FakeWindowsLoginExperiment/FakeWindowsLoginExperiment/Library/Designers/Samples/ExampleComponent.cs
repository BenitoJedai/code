using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeWindowsLoginExperiment.Library.Designers.Samples
{
    // Provides an example component associated with the example component designer.
    [DesignerAttribute(typeof(ExampleComponentDesigner), typeof(IDesigner))]
    public class ExampleComponent : System.ComponentModel.Component
    {
        public ExampleComponent()
        {
        }
    }
}
