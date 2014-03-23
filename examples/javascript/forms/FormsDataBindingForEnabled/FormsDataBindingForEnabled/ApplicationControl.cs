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
            // https://www.youtube.com/watch?v=AVZPTzkQOcY

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

            //var z = new zText { value = "zText" };

            //this.button1.DataBindings.Add("Text", z, "value");


            // http://msdn.microsoft.com/en-us/library/ms404299(v=vs.85).ASPX
            var a = this.button1.DataBindings.Add(
                "Enabled",
                () => this.applicationWebService1.CheckEnabled()
            );

            //Additional information: Type '<MoveNext>06000047' from assembly 'FormsDataBindingForEnabled.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' has a field of an illegal type.

            this.button2.DataBindings.Add(
                "Text",
                () => this.applicationWebService1.CheckText()
            );


            // this wont work for CLR
            //this.button2.DataBindings.Add(
            //    a
            //);

        }

        public class zText : INotifyPropertyChanged
        {
            // http://stackoverflow.com/questions/3367724/asp-net-can-i-databind-to-fields-instead-of-properties
            // CLR databinding does not work with fields
            public string value { get; set; }



            public void RaisePropertyChanged()
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("value"));

            }

            public event PropertyChangedEventHandler PropertyChanged;
        }
    }

}
