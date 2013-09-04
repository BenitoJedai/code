using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TestStackRewrite
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        public static void Invoke(TypeBuilder t, string ExternalTarget)
        {
            t.SetCustomAttribute(
                 new CustomAttributeBuilder(
                    typeof(ScriptAttribute).GetConstructor(new Type[0]),
                    new object[0],



                    namedProperties: new[] { typeof(ScriptAttribute).GetProperty("HasNoPrototype") },
                    propertyValues: new object[] { true },

                     namedFields: new[] { typeof(ScriptAttribute).GetField("ExternalTarget") },
                    fieldValues: new object[] { ExternalTarget }
                )
                );
        }
    }
}
