using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Lambda;
using ScriptCoreLib.Extensions;

namespace System.Windows.Forms
{
    public static class ControlBindingsCollectionExtensions
    {
        // X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\FormsDataBindingForEnabled\ApplicationControl.cs

        public static BindingToTask Add(this ControlBindingsCollection DataBindings, string PropertyName, Func<Task<bool>> yield)
        {
            var x = new BindingToTask(PropertyName, yield);

            DataBindings.Add(x);

            return x;
        }

        public class BindingToTask : Binding
        {
            //no implementation for System.ComponentModel.PropertyChangedEventHandler b5aa0254-4b45-32a5-97e6-c9a514e7e549
            //script: error JSC1000: No implementation found for this native method, please implement [System.ComponentModel.PropertyChangedEventHandler.Invoke(System.Object, System.ComponentModel.PropertyChangedEventArgs)]
            //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?

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

  //// FormsDataBindingForEnabled.BindingToTask..ctor
                //type$_9I2nYPDGKDaQBkpAm20sDg.dAAABvDGKDaQBkpAm20sDg = function (b, c)
                //{
                //  var a = [this], d, e;

  //  d = null;
                //  e = new ctor$fAAABgUfwDywsYmqSNbRgQ();
                //  e.yield = c;
                //  a[0].WwEABuWzsDyCcW0H2L852Q(b, new ctor$egAABkf_aSzmxyAettfDeww(), 'BooleanValue');

            : base(PropertyName, new BindingToTaskDataSource(), "BooleanValue")
            {
                (this.DataSource as BindingToTaskDataSource).With(
                    async x =>
                    {
                        while (true)
                        {
                            x.BooleanValue = await yield();
                            x.RaisePropertyChanged();

                            await 1000;
                        }
                    }
                );
            }
        }
    }

}
