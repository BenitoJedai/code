using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]
namespace TestDelegateInitializer
{
    public class Entry
    {
        public string TypeFullName;
        public Func<Type> InternalGetType;
    }

    public class Class1
    {
        public void LoadFile(string path)
        {
            var n = new Entry { };
            if (n != null)
            {
                Console.WriteLine("JavaArchiveReflector.Loadfile.InternalGetType almost set for " + n.TypeFullName);

                n.InternalGetType = delegate
                {
                    Console.WriteLine("JavaArchiveReflector.Loadfile.InternalGetType - " + n.TypeFullName);

                    return default(Type);
                };
            }

        }


    }

    interface i : ScriptCoreLibJava.IAssemblyReferenceToken
    {

    }
}
