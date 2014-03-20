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

        private async void timer1_Tick(object sender, System.EventArgs e)
        {
            //await this.applicationWebService1.CheckEnabled();
            //this.applicationWebService1.CheckEnabled();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // http://stackoverflow.com/questions/2820447/net-winforms-inotifypropertychanged-updates-all-bindings-when-one-is-changed-b

            //var x = new BindingToTask("Enabled", () => this.applicationWebService1.CheckEnabled());


            this.button1.DataBindings.Add("Enabled", () => this.applicationWebService1.CheckEnabled());
        }

    }


    public static class BindingToTaskExtensions
    {
        public static BindingToTask Add(this ControlBindingsCollection DataBindings, string PropertyName, Func<Task<bool>> yield)
        {
            var x = new BindingToTask(PropertyName, yield);

            DataBindings.Add(x);

            return x;
        }
    }
    public class BindingToTask : Binding
    {
        public sealed class BindingToTaskDataSource : INotifyPropertyChanged
        {
            public bool BooleanValue { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            public void RaisePropertyChanged()
            {
                if (PropertyChanged != null)
                    PropertyChanged(this,
                        new PropertyChangedEventArgs("BooleanValue")
                    );
            }
        }

        public BindingToTask(string PropertyName, Func<Task<bool>> yield)
            : base(PropertyName, new BindingToTaskDataSource(), "BooleanValue")
        {
            (this.DataSource as BindingToTaskDataSource).With(
                async x =>
                {
                    while (true)
                    {
                        x.BooleanValue = await yield();
                        x.RaisePropertyChanged();

                        await 100;
                    }
                }
            );
        }
    }
}
