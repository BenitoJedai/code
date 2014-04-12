using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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


        public __Binding Add(string propertyName, object dataSource, string dataMember)
        {
            var x = new __Binding(propertyName, dataSource, dataMember);

            Add(x);

            return x;
        }
        public void Add(__Binding binding)
        {
            Console.WriteLine("__ControlBindingsCollection.Add " + new
            {
                binding.PropertyName,
                binding.DataSource,
                binding.DataMember
            });

            if (binding.DataSource == null)
                return;


            // X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\FormsDataBindingForEnabled\ApplicationControl.cs
            //4:158ms __ControlBindingsCollection.Add  


            var asControl = (this.BindableComponent as Control);
            if (asControl == null)
                return;


            // X:\jsc.svn\examples\javascript\forms\Test\TestWebBrowserOneWayDataBinding\TestWebBrowserOneWayDataBinding\ApplicationControl.Designer.cs

            #region asWebBrowser
            var asWebBrowser = this.BindableComponent as WebBrowser;
            if (asWebBrowser != null)
            {
                var asBindingSource = binding.DataSource as BindingSource;
                if (asBindingSource != null)
                {
                    //this.navigationOrdersNavigateBindingSourceBindingSource.DataSource = typeof(SharedBrowserSessionExperiment.DataLayer.Data.NavigationOrdersNavigateBindingSource);
                    //this.navigationOrdersNavigateBindingSourceBindingSource.Position = 0;

                    // this.webBrowser1.DataBindings.Add(new System.Windows.Forms.Binding("Url", this.navigationOrdersNavigateBindingSourceBindingSource, "urlString", true, System.Windows.Forms.DataSourceUpdateMode.Never));


                    Action AtDataSource = delegate
                    {
                        var asType = asBindingSource.DataSource as Type;

                        // 3:155ms { asWebBrowser = <Namespace>.WebBrowser, PropertyName = Url, asType =  }
                        //Console.WriteLine(
                        //    new
                        //    {
                        //        asWebBrowser,
                        //        binding.PropertyName,
                        //        asType
                        //    }
                        //);

                        //27:182ms { asWebBrowser = <Namespace>.WebBrowser, PropertyName = Url, asType = <Namespace>.NavigationOrdersNavigateBindingSource }

                        if (asType != null)
                        {
                            // onetime here or shall be done by host BindingSource ?
                            var newBindingSource = Activator.CreateInstance(asType) as BindingSource;
                            if (newBindingSource != null)
                            {
                                var asDataTable = newBindingSource.DataSource as DataTable;

                                // 27:162ms { asWebBrowser = <Namespace>.WebBrowser, PropertyName = Url, DataMember = urlString, asDataTable = [object Object] }


                                //Console.WriteLine(
                                //      new
                                //      {
                                //          asWebBrowser,
                                //          binding.PropertyName,
                                //          binding.DataMember,
                                //          //asDataTable
                                //          asBindingSource.Position,
                                //      }
                                //  );

                                Action AtPosition = delegate
                                {
                                    if (asBindingSource.Position >= 0)
                                        if (asBindingSource.Position < asDataTable.Rows.Count)
                                        {
                                            var asRow = asDataTable.Rows[asBindingSource.Position];

                                            // X:\jsc.svn\examples\javascript\forms\Test\TestWebBrowserOneWayDataBinding\TestWebBrowserOneWayDataBinding\ApplicationControl.cs
                                            var value = asRow[binding.DataMember];

                                            // 31:9446ms { asWebBrowser = <Namespace>.WebBrowser, PropertyName = Url, value = http://example.com/ } 

                                            Console.WriteLine(
                                                new
                                                {
                                                    asWebBrowser,
                                                    binding.PropertyName,
                                                    value
                                                }
                                            );

                                            if (binding.PropertyName == "Url")
                                            {
                                                asWebBrowser.Navigate((string)value);
                                            }
                                        }
                                };


                                // now or later?
                                AtPosition();

                                asBindingSource.PositionChanged +=
                                    delegate
                                    {
                                        AtPosition();
                                    };
                            }
                        }
                    };

                    // await
                    if (asBindingSource.DataSource == null)
                        asBindingSource.DataSourceChanged += delegate { AtDataSource(); };
                    else
                        AtDataSource();
                }


                return;
            }
            #endregion



            // 4:105ms __ControlBindingsCollection.Add { asControl = <Namespace>.Button, DataSource =  } 
            //Console.WriteLine("__ControlBindingsCollection.Add " + new { asControl });

            //4:117ms __ControlBindingsCollection.Add { asControl = <Namespace>.Button } view-source:37151
            //Uncaught TypeError: Cannot read property 'constructor' of undefined 

            var isINotifyPropertyChanged = binding.DataSource is INotifyPropertyChanged;
            //Console.WriteLine("__ControlBindingsCollection.Add " + new { isINotifyPropertyChanged });

            if (!isINotifyPropertyChanged)
                return;

            var f = "_" + binding.DataMember + "_k__BackingField";

            Action update = delegate
            {
                // would this work with properties?
                // script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, System.Type, System.Collections.Generic.IEnumerable`1[[Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]])]


                //var value = (binding.DataSource as dynamic)[binding.DataMember];

                //   type$aw89N0f_aSzmxyAettfDeww._BooleanValue_k__BackingField = false;


                var value = Expando.InternalGetMember(binding.DataSource, f);

                //Console.WriteLine(new { binding.PropertyName, value });

                // this is the only example for now.
                if (binding.PropertyName == "Enabled")
                {
                    asControl.Enabled = (bool)value;
                }

                if (binding.PropertyName == "Text")
                {
                    asControl.Text = (string)value;
                }
            };

            // d = ( function () { var c$189 = f.binding.WQEABuWzsDyCcW0H2L852Q(); return (( function () { var c$189 = c$189.constructor; return 'Interfaces' in c$189 ? ('idtkRlDX9zioWpwjiQ7IgA' in c$189.Interfaces) : false; } )() ? c$189 : null); } )();
            var asINotifyPropertyChanged = (INotifyPropertyChanged)binding.DataSource;
            //var asINotifyPropertyChanged = (__INotifyPropertyChanged)binding.DataSource;
            asINotifyPropertyChanged.PropertyChanged +=
                (sender, e) =>
                {
                    //Console.WriteLine("__ControlBindingsCollection asINotifyPropertyChanged.PropertyChanged " + new { e.PropertyName, f });


                    //if (e.PropertyName != binding.PropertyName)
                    if (e.PropertyName != binding.DataMember)
                        return;

                    update();
                };

            update();
        }

        public static implicit operator ControlBindingsCollection(__ControlBindingsCollection x)
        {
            return (ControlBindingsCollection)(object)x;
        }
    }
}
