using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestFunctionDecorationForCatch
{
    [Script(HasNoPrototype = true)]
    public class Class1
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IPromise.cs

        [Script(DefineAsStatic = true)]
        public void Invoke()
        {
            //  a['catch']();
            //   a.catch();
            this.@catch();
        }

        public void @catch()
        {

        }
    }
}
