using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: Script]
[assembly: ScriptTypeFilter(ScriptType.Java)]
namespace TestImportsOfInterfaces
{

    [Script(IsNative = true)]
    public interface Interface7
    {
    }

    [Script(IsNative = true)]
    public interface InterfaceFoo
    {
    }

    [Script(IsNative = true)]
    public class Foo : InterfaceFoo
    {
    }


    [Script]
    public class Class1 : Foo, Interface7
    {

    }
}
