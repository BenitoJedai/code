using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Binding))]
    public class __Binding
    {
        // X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\FormsDataBindingForEnabled\ApplicationControl.cs

        // ?
        public string DataMember { get; set; }

        public string PropertyName { get; set; }
        public object DataSource { get; set; }

        public __Binding(string propertyName, object dataSource, string dataMember)
            : this(propertyName, dataSource, dataMember, default(bool), default(DataSourceUpdateMode))
        {
        }

        public __Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled)
            : this(propertyName, dataSource, dataMember, formattingEnabled, default(DataSourceUpdateMode))
        {
        }

        public bool FormattingEnabled { get; set; }
        public DataSourceUpdateMode DataSourceUpdateMode { get; set; }

        //0200001e SharedBrowserSessionExperiment.TheBrowserTab
        //script: error JSC1000: Missing Script Attribute? Native constructor was invoked, please implement [System.Windows.Forms.Binding..ctor(System.String, System.Object, System.String, System.Boolean)]
        // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\TheBrowserTab.cs

        public __Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode)
        {
            // X:\jsc.svn\examples\javascript\forms\test\TestWebBrowserOneWayDataBinding\TestWebBrowserOneWayDataBinding\ApplicationControl.Designer.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140412

            this.DataSource = dataSource;


            this.DataMember = dataMember;
            this.PropertyName = propertyName;

            this.FormattingEnabled = formattingEnabled;
            this.DataSourceUpdateMode = dataSourceUpdateMode;
        }
    }
}
