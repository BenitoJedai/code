using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Int16),
         ImplementationType = typeof(java.lang.Short))]
    internal class __Int16
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Int16.cs



        //Implementation not found for type import :
        //type: System.Int16
        //method: Int16 Parse(System.String)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!


        [Script(ExternalTarget = "parseShort")]
        public static short Parse(string e)
        {
            return default(short);
        }
    }
}
