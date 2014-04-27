using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartExperiment.Experimental
{
    // http://msdn.microsoft.com/en-us/library/system.componentmodel.providepropertyattribute(v=vs.110).aspx

    [Obsolete(@"
can we provide additional properties for designer?
Would we clash with the Microsoft Web UI namespace?
should jsc extract, generate and autolink such an extender control for us?
")]
    [ProvideProperty(BindingSourcePositionAlpha, typeof(Chart))]
    //[ProvideEvent]
    public class ApplicationControlExtender
        :
        Component,
        System.ComponentModel.IExtenderProvider
    {
        // http://www.codeproject.com/Articles/33185/Building-multi-control-components-using-IExtenderP


        // http://msdn.microsoft.com/en-us/library/system.componentmodel.iextenderprovider(v=vs.110).aspx

        //The designer must create an instance of type 'ChartExperiment.Experimental.WebUserControl' but it cannot because the type is declared as abstract. 

        const string BindingSourcePositionAlpha = "BindingSourcePositionAlpha";

        // "X:\jsc.svn\examples\javascript\forms\FormsProvidePropertyExperiment\FormsProvidePropertyExperiment.sln"
        // X:\jsc.svn\examples\javascript\forms\FormsProvidePropertyExperiment\FormsProvidePropertyExperiment\SpecialApplicationControl.cs





        // http://www.codeproject.com/Articles/4683/Getting-to-know-IExtenderProvider



        public static Action<ApplicationControlExtender, Chart, double> InternalSetBindingSourcePositionAlpha;
        public Dictionary<Chart, double> InternalBindingSourcePositionAlpha = new Dictionary<Chart, double>();

        [DefaultValue(0.07)]
        [Category("Something")]
        [Description("This is used by some code somewhere to do something")]



        [DisplayName(BindingSourcePositionAlpha)]
        public double GetBindingSourcePositionAlpha(Chart control)
        {
            if (!InternalBindingSourcePositionAlpha.ContainsKey(control))
                return default(double);

            return InternalBindingSourcePositionAlpha[control];
        }

        [DisplayName(BindingSourcePositionAlpha)]
        public void SetBindingSourcePositionAlpha(Chart control, double value)
        {
            InternalBindingSourcePositionAlpha[control] = value;

            if (InternalSetBindingSourcePositionAlpha != null)
                InternalSetBindingSourcePositionAlpha(this, control, value);
        }

        public bool CanExtend(object extendee)
        {
            return extendee is Chart;
        }

    }



}
