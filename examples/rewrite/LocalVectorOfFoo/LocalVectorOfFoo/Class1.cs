using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly:
    Script(IsScriptLibrary = true),
    ScriptTypeFilter(ScriptCoreLib.ScriptType.ActionScript),
    ScriptNamespaceRename(NativeNamespaceName = "ScriptCoreLib.ActionScript", VirtualNamespaceName = "", FilterToIsNative = true)
]

namespace LocalVectorOfFoo
{
    class Foo
    {
    }

    class Bar
    {
    }

    class Class2
    {
        Vector<Vector<Bar>> bar;
    }


    public class Class1
    {
        public string Error
        {
            get
            {
                var foo = new Vector<Foo>();
                var bar = new Vector<Vector<Bar>>();

                return "";
            }
        }

    }
}
