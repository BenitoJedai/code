using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsProvidePropertyExperiment
{
    [ProvideProperty("ServiceHost", typeof(ApplicationWebService))]
    public partial class SpecialApplicationControl : UserControl, IExtenderProvider
    {
        public SpecialApplicationControl()
        {
            InitializeComponent();
        }


        //Dictionary<ApplicationWebService, string> InternalServiceHost = new Dictionary<ApplicationWebService, string>();

        [DisplayName("ServiceHost")]
        public string GetServiceHost(ApplicationWebService control)
        {
            //if (!InternalServiceHost.ContainsKey(control))
            //    return default(string);

            //return InternalServiceHost[control];
            return "hi";
        }

        [DisplayName("ServiceHost")]
        public void SetServiceHost(ApplicationWebService control, string value)
        {
            //InternalServiceHost[control] = value;
        }

        public bool CanExtend(object extendee)
        {
            return extendee is ApplicationWebService;
        }
    }
}
