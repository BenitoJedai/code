using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]
namespace TestTheOrOperator
{
    public class Class1
    {
        public static bool Or(bool a, bool b)
        {
            /*
Y:\jsc.svn\examples\javascript\Test\TestTheOrOperator\TestTheOrOperator\bin\Debug>c:\util\jsc\bin\jsc.exe TestTheOrOperator.dll -js
The .NET cross compiler for web platforms
Abstcractatech OÜ
Copyright c jsc-solutions.net 2012

CLR: 4.0.30319.17626
Is64BitProcess: False

Current Path: Y:\jsc.svn\examples\javascript\Test\TestTheOrOperator\TestTheOrOperator\bin\Debug
Y:\jsc.svn\examples\javascript\Test\TestTheOrOperator\TestTheOrOperator\bin\Debug\TestTheOrOperator.dll
TestTheOrOperator.Class1
script: error JSC1000: logic failure
script: error JSC1000: error at TestTheOrOperator.Class1.Or, type: TestTheOrOperator.Class1 offset: 0x0005  method:Boolean Or(Boolean, Boolean)
             */

            // C# 5 behaves differently!
            // Can we support both scenarios?
            return a || b;
        }
    }
}
