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
        {
            this.DataSource = dataSource;


            this.DataMember = dataMember;
            this.PropertyName = propertyName;


            // now what?
        }
    }
}
