using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void EndInit()
        {
            // new DataSource();

            Console.WriteLine("__BindingSource EndInit");

        }
    }
}
