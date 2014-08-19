using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    // http://referencesource.microsoft.com/#System.Data/data/System/Data/Common/DbConnectionStringBuilder.cs
    // https://github.com/mono/mono/blob/effa4c07ba850bedbe1ff54b2a5df281c058ebcb/mcs/class/System.Data/System.Data.Common/DbConnectionStringBuilder.cs

    [Script(Implements = typeof(global::System.Data.Common.DbConnectionStringBuilder))]
    public class __DbConnectionStringBuilder
    {
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestOrderByThenGroupBy\Program.cs
        public override string ToString()
        {
            return this.InternalGetConnectionString();
        }

        public string ConnectionString
        {
            get
            {
                return InternalGetConnectionString();
            }
        }

        // should use public instead of protected..
        public virtual string InternalGetConnectionString()
        {
            return null;
        }

        public virtual void InternalAdd(string keyword, object value)
        {

        }

        public void Add(string keyword, object value)
        {
            InternalAdd(keyword, value);
        }
    }
}
