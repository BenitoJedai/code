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
    }
}
