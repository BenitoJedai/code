﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakeWindowsLoginExperiment.Library.Designers
{
    // Provides an example component designer.
    public class ExampleComponentDesigner : System.ComponentModel.Design.ComponentDesigner
    {
        public ExampleComponentDesigner()
        {
        }

        // This method provides an opportunity to perform processing when a designer is initialized.
        // The component parameter is the component that the designer is associated with.
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            // Always call the base Initialize method in an override of this method.
            base.Initialize(component);
        }

        // This method is invoked when the associated component is double-clicked.
        public override void DoDefaultAction()
        {
            MessageBox.Show("The event handler for the default action was invoked.");
        }

        // This method provides designer verbs.
        public override System.ComponentModel.Design.DesignerVerbCollection Verbs
        {
            get
            {
                return new DesignerVerbCollection(new DesignerVerb[] { new DesignerVerb("Example Designer Verb Command", new EventHandler(this.onVerb)) });
            }
        }

        // Event handling method for the example designer verb
        private void onVerb(object sender, EventArgs e)
        {
            MessageBox.Show("The event handler for the Example Designer Verb Command was invoked.");
        }
    }
}
