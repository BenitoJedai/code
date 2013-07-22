using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbConnectionStringBuilder))]
    public class __DbConnectionStringBuilder
    {
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
