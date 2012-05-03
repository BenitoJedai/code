using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: Script]
[assembly: ScriptTypeFilter(ScriptType.Java)]

namespace TestFloatVariableInitializer
{
    [Script]
    public class Class1
    {
        public float value = 0.9f;
    }
}
