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

        public static __BindingToTask<T> Add<T>(this ControlBindingsCollection DataBindings, string PropertyName, Func<Task<T>> yield)
        {
            var x = new __BindingToTask<T>(PropertyName, yield);

            DataBindings.Add(x);

            return x;
        }

    }



    public sealed class __BindingToTaskDataSource<T> : INotifyPropertyChanged
    {
        public T TValue { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged()
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new PropertyChangedEventArgs("TValue")
                );
        }
    }

    public class __BindingToTask<T> : Binding
    {
        //no implementation for System.ComponentModel.PropertyChangedEventHandler b5aa0254-4b45-32a5-97e6-c9a514e7e549
        //script: error JSC1000: No implementation found for this native method, please implement [System.ComponentModel.PropertyChangedEventHandler.Invoke(System.Object, System.ComponentModel.PropertyChangedEventArgs)]
        //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?



        public __BindingToTask(string PropertyName, Func<Task<T>> yield)
            : base(PropertyName, new __BindingToTaskDataSource<T>(), "TValue")
        {

            (this.DataSource as __BindingToTaskDataSource<T>).With(
                x =>
                {
                    Action loop = null;

                    loop = delegate
                    {
                        Task.Delay(1000).ContinueWith(
                            delegate
                            {
                                yield().ContinueWithResult(
                                    TValue =>
                                    {
                                        x.TValue = TValue;
                                        x.RaisePropertyChanged();

                                        loop();
                                    }
                                );
                            }
                        );

                    };

                    loop();

                }
            );
        }
    }
}
