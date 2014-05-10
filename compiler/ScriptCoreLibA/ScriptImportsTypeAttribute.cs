using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib
{
    /// <summary>
    /// Defines an external type to be imported with current class definition. To be used with native source code injection.
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ScriptImportsTypeAttribute : Attribute
    {
        // hows this of use?
        // what tests this?
        // X:\jsc.svn\examples\javascript\forms\test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs


        public string Name;

        public ScriptImportsTypeAttribute(string Name)
        {
            this.Name = Name;
        }
    }
}
