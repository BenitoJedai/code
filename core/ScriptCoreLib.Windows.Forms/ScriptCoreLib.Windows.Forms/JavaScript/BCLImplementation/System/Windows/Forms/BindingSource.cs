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

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.BindingSource.set_Position(System.Int32)]
        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridViewColumn.set_DataPropertyName(System.String)]
        public int Position { get; set; }

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
