using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ClickOnce.js
{
    [Script, ScriptApplicationEntryPoint(IsClickOnce = true)]
    class Class2
    {
        public readonly Class1 Value;

        public Class2()
        {
            this.Value = new Class1(Class1.DefaultData);
        }
    }
}
