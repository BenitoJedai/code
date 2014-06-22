using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestIsInt32
{
    public class Class1
    {
        public int CompareTo(object value)
        {
            // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Single.cs
            // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Int32.cs
            // c:/util/jsc/bin/jsc.meta.exe $(TargetPath) -as

            if (value == null)
            {
                return 1;
            }

            //  if ((value as  int) == null)
            if (!(value is int))
                return 2;

            if (value is int)
                return 3;

            return 0;
        }
    }
}
