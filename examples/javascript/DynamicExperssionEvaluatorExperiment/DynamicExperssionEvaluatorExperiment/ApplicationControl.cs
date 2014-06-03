using DynamicExperssionEvaluatorExperiment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DynamicExperssionEvaluatorExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            dynamic global = new DynamicApplicationWebServiceX();


            // not yet. jsc cannot handle complex il yet.
            //            script: error JSC1000:
            //error:
            //  statement cannot be a load instruction (or is it a bug?)
            //  [0x005f] ldsfld     +1 -0

            // type: DynamicExperssionEvaluatorExperiment.ApplicationControl
            // offset: 0x005f
            //  method:Void button1_Click(System.Object, System.EventArgs)


            // how does this look in C# IL, then after how does it look in JS

            var System = global.System;

            var Console = System.Console;

            // Uncaught Error: NotImplementedException: __CallSite.Create 
            Console.WriteLine("Hello world");
        }

    }
}
