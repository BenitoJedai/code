using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ControlBindingsCollection))]
    public class __ControlBindingsCollection : __BindingsCollection
    {
        public __IBindableComponent BindableComponent { get; set; }

        public __ControlBindingsCollection(__IBindableComponent control)
        {
            this.BindableComponent = control;


            //Console.WriteLine("__ControlBindingsCollection.ctor ");
        }

        public void Add(__Binding binding)
        {
            //Console.WriteLine("__ControlBindingsCollection.Add " + new { binding.DataSource });

            if (binding.DataSource == null)
                return;


            // X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\FormsDataBindingForEnabled\ApplicationControl.cs
            //4:158ms __ControlBindingsCollection.Add  


            var asControl = (this.BindableComponent as Control);
            if (asControl == null)
                return;

            // 4:105ms __ControlBindingsCollection.Add { asControl = <Namespace>.Button, DataSource =  } 
            //Console.WriteLine("__ControlBindingsCollection.Add " + new { asControl });

            //4:117ms __ControlBindingsCollection.Add { asControl = <Namespace>.Button } view-source:37151
            //Uncaught TypeError: Cannot read property 'constructor' of undefined 

            var isINotifyPropertyChanged = binding.DataSource is INotifyPropertyChanged;
            //Console.WriteLine("__ControlBindingsCollection.Add " + new { isINotifyPropertyChanged });

            if (!isINotifyPropertyChanged)
                return;

            // d = ( function () { var c$189 = f.binding.WQEABuWzsDyCcW0H2L852Q(); return (( function () { var c$189 = c$189.constructor; return 'Interfaces' in c$189 ? ('idtkRlDX9zioWpwjiQ7IgA' in c$189.Interfaces) : false; } )() ? c$189 : null); } )();
            var asINotifyPropertyChanged = (INotifyPropertyChanged)binding.DataSource;
            //var asINotifyPropertyChanged = (__INotifyPropertyChanged)binding.DataSource;
            asINotifyPropertyChanged.PropertyChanged +=
                (sender, e) =>
                {
                    var f = "_" + binding.DataMember + "_k__BackingField";
                    Console.WriteLine("__ControlBindingsCollection asINotifyPropertyChanged.PropertyChanged " + new { e.PropertyName, f });


                    //if (e.PropertyName != binding.PropertyName)
                    if (e.PropertyName != binding.DataMember)
                        return;

                    // would this work with properties?
                    // script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, System.Type, System.Collections.Generic.IEnumerable`1[[Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]])]


                    //var value = (binding.DataSource as dynamic)[binding.DataMember];

                    //   type$aw89N0f_aSzmxyAettfDeww._BooleanValue_k__BackingField = false;


                    var value = Expando.InternalGetMember(binding.DataSource, f);

                    Console.WriteLine(new { binding.PropertyName, value });

                    if (binding.PropertyName == "Enabled")
                    {

                        asControl.Enabled = (bool)value;
                    }
                };

            //binding.
        }

        public static implicit operator ControlBindingsCollection(__ControlBindingsCollection x)
        {
            return (ControlBindingsCollection)(object)x;
        }
    }
}
