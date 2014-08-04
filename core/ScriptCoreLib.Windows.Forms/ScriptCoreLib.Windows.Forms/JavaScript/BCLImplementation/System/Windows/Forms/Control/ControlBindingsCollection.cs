using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
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

            #region asBindingSource
            var asBindingSource = binding.DataSource as __BindingSource;
            if (asBindingSource != null)
            {
                //this.navigationOrdersNavigateBindingSourceBindingSource.DataSource = typeof(SharedBrowserSessionExperiment.DataLayer.Data.NavigationOrdersNavigateBindingSource);
                //this.navigationOrdersNavigateBindingSourceBindingSource.Position = 0;

                // this.webBrowser1.DataBindings.Add(new System.Windows.Forms.Binding("Url", this.navigationOrdersNavigateBindingSourceBindingSource, "urlString", true, System.Windows.Forms.DataSourceUpdateMode.Never));


                Action AtDataSource = delegate
                {

                    // onetime here or shall be done by host BindingSource ?
                    var newBindingSource = asBindingSource.ActivatedDataSource as __BindingSource;
                    if (newBindingSource != null)
                    {
                        var asDataTable = newBindingSource.ActivatedDataSource as DataTable;

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

                        Console.WriteLine("await newBindingSource.InternalAfterEndInit");
                        // is this called?
                        newBindingSource.InternalInvokeAfterEndInit(
                            delegate
                            {
                                Console.WriteLine("await newBindingSource.InternalAfterEndInit done");


                                #region AtPosition
                                Action AtPosition = delegate
                                {
                                    if (asBindingSource.Position < 0)
                                        return;

                                    if (asBindingSource.Position >= asDataTable.Rows.Count)
                                        return;

                                    var asRow = asDataTable.Rows[asBindingSource.Position];

                                    // X:\jsc.svn\examples\javascript\forms\Test\TestWebBrowserOneWayDataBinding\TestWebBrowserOneWayDataBinding\ApplicationControl.cs
                                    var value = asRow[binding.DataMember];

                                    // 31:9446ms { asWebBrowser = <Namespace>.WebBrowser, PropertyName = Url, value = http://example.com/ } 

                                    //Console.WriteLine(
                                    //    new
                                    //    {
                                    //        asWebBrowser,
                                    //        binding.PropertyName,
                                    //        value
                                    //    }
                                    //);

                                    // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\TheBrowserTab.cs

                                    #region Text
                                    if (binding.PropertyName == "Text")
                                    {

                                        Console.WriteLine("data source to Text " + new
                                        {
                                            asBindingSource.Position,
                                            binding.DataMember,
                                            value
                                        });

                                        asControl.Text = (string)value;
                                        return;
                                    }
                                    #endregion


                                    #region asWebBrowser
                                    var asWebBrowser = this.BindableComponent as WebBrowser;
                                    if (asWebBrowser != null)
                                    {
                                        //X:\jsc.svn\examples\javascript\forms\HashForBindingSource\HashForBindingSource\ApplicationControl.cs


                                        if (binding.PropertyName == "DocumentText")
                                        {
                                            asWebBrowser.DocumentText = ((string)value);
                                        }

                                        if (binding.PropertyName == "Url")
                                        {
                                            asWebBrowser.Navigate((string)value);
                                        }

                                        return;
                                    }
                                    #endregion
                                };
                                #endregion


                                // now or later?
                                AtPosition();

                                asBindingSource.PositionChanged +=
                                    delegate
                                    {
                                        AtPosition();
                                    };

                                #region TextChanged
                                if (binding.PropertyName == "Text")
                                {
                                    asControl.TextChanged +=
                                        delegate
                                        {
                                            if (asBindingSource.Position < 0)
                                                return;

                                            if (asBindingSource.Position >= asDataTable.Rows.Count)
                                                return;

                                            var asRow = asDataTable.Rows[asBindingSource.Position];

                                            Console.WriteLine("Text to data source " + new
                                            {
                                                asBindingSource.Position,
                                                binding.DataMember,
                                                asControl.Text
                                            });

                                            // X:\jsc.svn\examples\javascript\forms\Test\TestWebBrowserOneWayDataBinding\TestWebBrowserOneWayDataBinding\ApplicationControl.cs
                                            asRow[binding.DataMember] = asControl.Text;
                                        };
                                }
                                #endregion
                            }
                        );


                    }
                };

                // await
                if (asBindingSource.ActivatedDataSource == null)
                    asBindingSource.DataSourceChanged += delegate { AtDataSource(); };
                else
                    AtDataSource();
            }
            #endregion






            // 4:105ms __ControlBindingsCollection.Add { asControl = <Namespace>.Button, DataSource =  } 
            //Console.WriteLine("__ControlBindingsCollection.Add " + new { asControl });

            //4:117ms __ControlBindingsCollection.Add { asControl = <Namespace>.Button } view-source:37151
            //Uncaught TypeError: Cannot read property 'constructor' of undefined 

            #region asINotifyPropertyChanged
            var isINotifyPropertyChanged = binding.DataSource is INotifyPropertyChanged;
            //Console.WriteLine("__ControlBindingsCollection.Add " + new { isINotifyPropertyChanged });

            if (!isINotifyPropertyChanged)
                return;

            var f = "_" + binding.DataMember + "_k__BackingField";

            #region update
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
            #endregion


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
            #endregion

        }

        public static implicit operator ControlBindingsCollection(__ControlBindingsCollection x)
        {
            return (ControlBindingsCollection)(object)x;
        }
    }
}
