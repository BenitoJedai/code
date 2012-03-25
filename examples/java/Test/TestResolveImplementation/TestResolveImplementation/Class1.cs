using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

[assembly: Script, ScriptTypeFilter(ScriptType.Java)]

namespace TestResolveImplementation
{
    [Script]
    public class Class1
    {
        public static event Func<Action<FileInfo>, Func<Action<DirectoryInfo>, Class1>> ComplexHandler;

        static void S1()
        {
        }

        static void Foo()
        {
            try
            {
                S1();
            }
            catch (csharp.ThrowableException e)
            {
                var x = (object)e;
                var y = (csharp.ThrowableException)x;

                S1();
            }
        }
    }


    interface IAssemblyReferenceToken : ScriptCoreLibJava.IAssemblyReferenceToken
    {

    }
}
