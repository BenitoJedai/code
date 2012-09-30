using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestNestedStructWithStaticFieldBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://connect.microsoft.com/VisualStudio/feedback/details/765430/testnestedstructwithstaticfieldbuilder#details

#if error
            var a = AppDomain.CurrentDomain.DefineDynamicAssembly(
new AssemblyName("foo"), System.Reflection.Emit.AssemblyBuilderAccess.RunAndSave
);

var m = a.DefineDynamicModule("foo.dll");

var p = m.DefineType("p", TypeAttributes.BeforeFieldInit);
var s = p.DefineNestedType(

name: "s",
attr: TypeAttributes.AnsiClass
| TypeAttributes.Sealed
| TypeAttributes.NestedPublic
| TypeAttributes.SequentialLayout
| TypeAttributes.BeforeFieldInit,

parent: null,

packSize: System.Reflection.Emit.PackingSize.Unspecified,
typeSize: 1
);

s.SetParent(typeof(ValueType));


s.DefineField("e", s, FieldAttributes.Static);
p.DefineField("a", s, FieldAttributes.Private);

p.CreateType();
s.CreateType();


a.Save("foo.dll");
#endif

            var a = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName("foo"), System.Reflection.Emit.AssemblyBuilderAccess.RunAndSave
            );

            var m = a.DefineDynamicModule("foo.dll");

            var p = m.DefineType("p", TypeAttributes.BeforeFieldInit);
            //var s = p.DefineNestedType(
            var s = m.DefineType(

                name: "p+s",
                attr: TypeAttributes.AnsiClass
                    | TypeAttributes.Sealed
                    | TypeAttributes.Public
                    | TypeAttributes.SequentialLayout
                    | TypeAttributes.BeforeFieldInit,

                parent: typeof(ValueType),

                packingSize: System.Reflection.Emit.PackingSize.Unspecified,
                typesize: 1
            );



            s.DefineField("e", s, FieldAttributes.Static);
            p.DefineField("a", s, FieldAttributes.Private);

            s.CreateType();
            p.CreateType();


            a.Save("foo.dll");

            // Error	2	Struct member 'p.s.e' of type 'p.s' causes a cycle in the struct layout	X:\jsc.svn\examples\rewrite\Test\TestNestedStructWithStaticField\TestNestedStructWithStaticField\Class1.cs	10	11	TestNestedStructWithStaticField
            //s.DefineField("__value", typeof(int), FieldAttributes.Public);

        }
    }
}
