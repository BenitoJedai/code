using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib
{
    [global::System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, Inherited = false, AllowMultiple = true)]
    public sealed class ScriptMethodThrows : Attribute
    {
        // X:\jsc.svn\examples\javascript\forms\test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/201405
        // X:\jsc.internal.svn\compiler\jsc.internal\jsc.internal\meta\Commands\Reference\ReferenceAssetsLibrary.cs
        
        public Type ThrowType { get; set; }



        public ScriptMethodThrows(Type e)
        {
            this.ThrowType = e;
        }

        public static ScriptMethodThrows[] ArrayOfProvider(ICustomAttributeProvider m)
        {
            try
            {
                ScriptMethodThrows[] s = m.GetCustomAttributes(typeof(ScriptMethodThrows), false) as ScriptMethodThrows[];

                return s;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }

}
