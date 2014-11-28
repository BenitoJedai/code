using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestTypeOfBool
{
    public class Class1 : ScriptCoreLibJava.IAssemblyReferenceToken
    {
        bool field1;

        public void invoke()
        {
            //            type0 = __Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(Integer.class));
            //type1 = __Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(Boolean.class));
            //var x = typeof(Class1).GetField("field1");

            // X:\jsc.svn\core\ScriptCoreLibJava\java\lang\Boolean.cs
            //  class0 = boolean.TYPE;
            var x = java.lang.Boolean.TYPE;


            var Tint = typeof(int);
            var Tbool = typeof(bool);

        }
    }
}
