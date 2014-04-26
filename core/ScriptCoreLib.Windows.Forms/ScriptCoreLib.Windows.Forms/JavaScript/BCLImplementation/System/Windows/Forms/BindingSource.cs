using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.BindingSource))]
    public class __BindingSource : __Component, ISupportInitialize
    {
        // X:\jsc.svn\examples\javascript\forms\FormsNICWithDataSource\FormsNICWithDataSource\ApplicationControl.cs
        // X:\jsc.svn\examples\javascript\forms\Test\TestDynamicBindingSourceForDataTable\TestDynamicBindingSourceForDataTable\ApplicationControl.Designer.cs
        // X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\FormsDataBindingForEnabled\ApplicationControl.cs

        // ?
        public event BindingCompleteEventHandler BindingComplete;
        public event EventHandler DataMemberChanged;

        public virtual bool AllowNew { get; set; }

        public __BindingSource()
        {

        }

        public __BindingSource(IContainer container)
        //: base(container)
        {
        }

        #region Position
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView\DataGridView.DataSource.cs

        public int InternalPosition;
        public int Position
        {
            get
            {
                return InternalPosition;
            }
            set
            {
                InternalPosition = value;
                if (PositionChanged != null)
                    PositionChanged(this, new EventArgs());

                if (CurrentChanged != null)
                    CurrentChanged(this, new EventArgs());

            }
        }

        // X:\jsc.svn\examples\javascript\forms\FormsDualDataSource\FormsDualDataSource\ApplicationControl.cs
        public event EventHandler PositionChanged;
        #endregion



        #region DataSource
        public object InternalDataSource;
        public object DataSource
        {
            get { return InternalDataSource; }
            set
            {
                this.InternalDataSource = value;

                if (DataSourceChanged != null)
                    DataSourceChanged(this, new EventArgs());
            }
        }
        public event EventHandler DataSourceChanged;
        #endregion

        //public string DataPropertyName { get; set; }


        public void BeginInit()
        {
        }

        public object _innerList;
        // -		_innerList	{SharedBrowserSessionExperiment.DataLayer.Data.NavigationOrdersNavigateBindingSource}	System.Collections.IList {SharedBrowserSessionExperiment.DataLayer.Data.NavigationOrdersNavigateBindingSource}

        public object ActivatedDataSource
        {
            get
            {
                if (_innerList == null)
                {
                    var asType = InternalDataSource as Type;
                    if (asType != null)
                    {
                        // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\TheBrowserTab.Designer.cs
                        _innerList = Activator.CreateInstance(asType);
                    }
                    else
                    {
                        _innerList = InternalDataSource;
                    }
                }


                return _innerList;
            }
        }

        public TaskCompletionSource<object> InternalAfterEndInit = new TaskCompletionSource<object>();

        public void EndInit()
        {
            Console.WriteLine("__BindingSource.EndInit ");

            // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\Control\ControlBindingsCollection.cs
            // two way binding to be activated now?

            // 72:494ms await newBindingSource.InternalAfterEndInit

            InternalAfterEndInit.SetResult(null);
        }



        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.BindingSource.RemoveAt(System.Int32)]

        public virtual void RemoveAt(int index)
        {
            var x = this.ActivatedDataSource;

            var asBindingSource = x as BindingSource;
            if (asBindingSource != null)
            {
                asBindingSource.RemoveAt(index);
                return;
            }

            var asDataTable = x as DataTable;
            if (asDataTable != null)
            {
                asDataTable.Rows.RemoveAt(index);
                return;
            }

            return;
        }

        public virtual object AddNew()
        {
            var x = this.ActivatedDataSource;

            var asBindingSource = x as BindingSource;
            if (asBindingSource != null)
            {
                return asBindingSource.AddNew();
            }

            var asDataTable = x as DataTable;
            if (asDataTable != null)
            {
                //new DataRowView(;
                // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\TheBrowserTab.cs
                // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Data\DataRowView.cs

                // is new row also supposed to add the row?
                var rr = asDataTable.NewRow();
                asDataTable.Rows.Add(rr);

                // whats the correct way to do this?
                return new __DataRowView { Row = rr };
            }

            return null;
        }



        public virtual object this[int index]
        {
            get
            {
                var x = this.ActivatedDataSource;

                var asBindingSource = x as BindingSource;
                if (asBindingSource != null)
                {
                    return asBindingSource[index];
                }

                var asDataTable = x as DataTable;
                if (asDataTable != null)
                {
                    // X:\jsc.svn\examples\javascript\forms\HashForBindingSource\HashForBindingSource\ApplicationControl.cs

                    //asDataTable.DefaultView[
                    var rr = asDataTable.Rows[index];
                    return new __DataRowView { Row = rr };
                }

                return null;
            }
            set
            {
                // ?
            }
        }

        public virtual int Count
        {
            get
            {
                var x = this.ActivatedDataSource;

                var asBindingSource = x as BindingSource;
                if (asBindingSource != null)
                {
                    return asBindingSource.Count;
                }

                var asDataTable = x as DataTable;
                if (asDataTable != null)
                {
                    return asDataTable.Rows.Count;
                }

                return 0;
            }
        }




        public event EventHandler CurrentChanged;

        // pt: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.BindingSource.add_CurrentItemChanged(System.EventHandler)]
        // ?
        public event EventHandler CurrentItemChanged;

        public object Current
        {
            get
            {
                // X:\jsc.svn\examples\javascript\forms\HashForBindingSource\HashForBindingSource\Application.cs

                return this[this.Position];
            }
        }


        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.BindingSource.set_Filter(System.String)]

        public string Filter { get; set; }
    }
}
