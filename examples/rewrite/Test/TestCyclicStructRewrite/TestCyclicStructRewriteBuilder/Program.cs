using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestCyclicStructRewriteBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var abs = AppDomain.CurrentDomain.DefineDynamicAssembly(
              new AssemblyName("foo"), System.Reflection.Emit.AssemblyBuilderAccess.RunAndSave
          );
            var ms = ab.DefineDynamicModule("foo.dll");


            var ab = AppDomain.CurrentDomain.DefineDynamicAssembly(
              new AssemblyName("foo"), System.Reflection.Emit.AssemblyBuilderAccess.RunAndSave
          );

            var m = ab.DefineDynamicModule("foo.dll");

            var a = m.DefineType(

                 name: "a"
                 , attr: TypeAttributes.AnsiClass
                     | TypeAttributes.Sealed
                     | TypeAttributes.Public
                     | TypeAttributes.SequentialLayout
                     | TypeAttributes.BeforeFieldInit,

                 parent: typeof(ValueType),

                 packingSize: System.Reflection.Emit.PackingSize.Unspecified,
                 typesize: 1
             );

            var agp = a.DefineGenericParameters("T");

            
            var c = m.DefineType(

                name: "c",
                attr: TypeAttributes.AnsiClass
                    | TypeAttributes.Sealed
                    | TypeAttributes.Public
                    | TypeAttributes.SequentialLayout
                    | TypeAttributes.BeforeFieldInit,

                parent: typeof(ValueType),

                packingSize: System.Reflection.Emit.PackingSize.Unspecified,
                typesize: 1
            );


            var u = m.DefineType(
              name: "u"
            );


            var a_of_u = a.MakeGenericType(u);
            
            c.DefineField("a", a_of_u, FieldAttributes.Assembly);

            u.DefineField("c", c, FieldAttributes.Assembly);


            a.CreateType();
            c.CreateType();
            u.CreateType();

            


            ab.Save("foo.dll");
        }
    }
}
