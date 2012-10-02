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


            var ab = AppDomain.CurrentDomain.DefineDynamicAssembly(
              new AssemblyName("TestCyclicStructRewrite"), System.Reflection.Emit.AssemblyBuilderAccess.RunAndSave
          );

            var m = ab.DefineDynamicModule("TestCyclicStructRewrite.dll");

            var a = m.DefineType(

                 name: "a`1"
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
                    | TypeAttributes.NotPublic
                    | TypeAttributes.SequentialLayout
                    | TypeAttributes.BeforeFieldInit,

                parent: typeof(ValueType)
            );


            var u = m.DefineType(
              name: "u",
              attr: TypeAttributes.BeforeFieldInit
            );


      

            #region workaround

            #region shadow of u
            var abs = AppDomain.CurrentDomain.DefineDynamicAssembly(
              new AssemblyName("TestCyclicStructRewrite"), 
              
              // Additional information: A non-collectible assembly may not reference a collectible assembly.
              //System.Reflection.Emit.AssemblyBuilderAccess.RunAndCollect

              System.Reflection.Emit.AssemblyBuilderAccess.Run
          );
            var ms = abs.DefineDynamicModule("TestCyclicStructRewrite.dll");

         
            var us = ms.DefineType(
              name: "u",
              attr: TypeAttributes.Public
            );
            #endregion

            us.CreateType();

            var as_of_u = @a.MakeGenericType(us);

            c.DefineField("a", as_of_u, FieldAttributes.Private);
            #endregion

            u.DefineField("c", c, FieldAttributes.Private);


            a.CreateType();

            c.CreateType();
            u.CreateType();



            ab.Save("TestCyclicStructRewrite.dll");
        }
    }
}
