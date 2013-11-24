using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace JVMCLRTypeOfInt32X
{
    public class Class1 : ScriptCoreLibJava.IAssemblyReferenceToken
    {
        public Class1()
        {
            int int32 = 0;
            object int32box = int32;

            // why are we using the boxed version of the type?
            // jsc is giving us the primitive? 
            Console.WriteLine(
                int32.GetType().FullName
            );

            Console.WriteLine(
                  int32box.GetType().FullName
              );


            //java.lang.Object, rt
            //java.lang.Integer
            //java.lang.Integer
            //int

            //__Console.WriteLine(__Object.System_Object_GetType_06000007(new Integer(num0)).get_FullName());
            //  __Console.WriteLine(__Object.System_Object_GetType_06000007(object1).get_FullName());
            //  __Console.WriteLine(__Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(int.class)).get_FullName());

            Console.WriteLine(
               typeof(int).FullName
           );

            //  ((Long)object1).longValue();

            var int32x = int32box is int;
            var int64x = int32box is long;
            Console.WriteLine(new { int32x, int64x });

            var int64 = (long)int32box;

        }
    }
}
