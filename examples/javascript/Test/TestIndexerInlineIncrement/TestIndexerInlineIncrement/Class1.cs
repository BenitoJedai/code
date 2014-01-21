using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestIndexerInlineIncrement
{
    public class Class1
    {
        public Class1(int length, char[] input)
        {
            // X:\jsc.internal.svn\compiler\jsc\CodeModel\ILFlow.cs
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Convert.ToBase64String.cs

            char chr1 = default(char);

            for (int i = 0; i < length; i++)
            {
                chr1 = input[i++];
            }
        }
    }
}
