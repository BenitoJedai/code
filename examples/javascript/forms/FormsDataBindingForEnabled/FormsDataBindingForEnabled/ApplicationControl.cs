using FormsDataBindingForEnabled;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Lambda;

namespace FormsDataBindingForEnabled
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }



        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // http://stackoverflow.com/questions/2820447/net-winforms-inotifypropertychanged-updates-all-bindings-when-one-is-changed-b

            //var x = new BindingToTask("Enabled", () => this.applicationWebService1.CheckEnabled());

            //looking at { Name = packages.config }
            //{ FixupHintPath = X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\packages\ScriptCoreLib.Async.1.0.0.0 }
            //will need to find package  { id = ScriptCoreLib.Async }
            //will find package  { id = ScriptCoreLib.Async }
            //cleaned { id = ScriptCoreLib.Async }
            //updating { id = ScriptCoreLib.Async }
            //updating { RestorePackagesFromFile = c:/util/jsc/nuget/ScriptCoreLib.Async.1.0.0.0.nupkg }
            //updated { id = ScriptCoreLib.Async }

            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.cs

            this.button1.DataBindings.Add(
                "Enabled",
                () => this.applicationWebService1.CheckEnabled()
            );
        }

    }

}
