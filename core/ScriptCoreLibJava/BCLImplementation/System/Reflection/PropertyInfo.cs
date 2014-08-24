using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;
using java.lang.reflect;
using java.lang;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
    // http://referencesource.microsoft.com/#mscorlib/system/reflection/propertyinfo.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Reflection/PropertyInfo.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\PropertyInfo.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Reflection\PropertyInfo.cs

    [Script(Implements = typeof(PropertyInfo))]
    internal class __PropertyInfo : __MethodBase
    {
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestAndroidOrderByThenGroupBy\ApplicationWebService.cs

        // can jsc keep property RTTI  for all platforms
        // for datalayer use.

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140514
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs




        public global::System.Type PropertyType
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public static bool operator !=(__PropertyInfo left, __PropertyInfo right)
        {
            return (object)left != (object)right;
        }

        public static bool operator ==(__PropertyInfo left, __PropertyInfo right)
        {
            return (object)left == (object)right;

        }



        public override ParameterInfo[] GetParameters()
        {
            throw new NotImplementedException();
        }

        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        public override global::System.Type DeclaringType
        {
            get { throw new NotImplementedException(); }
        }

        public virtual object GetValue(object obj, object[] index)
        {
            return null;
        }
    }
}
