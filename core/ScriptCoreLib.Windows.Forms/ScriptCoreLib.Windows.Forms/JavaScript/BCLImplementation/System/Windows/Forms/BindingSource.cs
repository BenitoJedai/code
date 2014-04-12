using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.BindingSource))]
    public class __BindingSource : __Component, __ISupportInitialize
    {
        // X:\jsc.svn\examples\javascript\forms\Test\TestDynamicBindingSourceForDataTable\TestDynamicBindingSourceForDataTable\ApplicationControl.Designer.cs


        // X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\FormsDataBindingForEnabled\ApplicationControl.cs

        // ?

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


        public void EndInit()
        {
            // new DataSource();

            //Console.WriteLine("__BindingSource EndInit");

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
    }
}
