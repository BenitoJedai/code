using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(IsNative = true, ExternalTarget = "Exception")]
    internal class PHPSPLException
    {
        public string getMessage()
        {
            return default(string);
        }
    }

    [Script(ImplementationType = typeof(PHPSPLException), Implements = typeof(global::System.Exception))]
    internal class __Exception
    {
        // http://www.php.net/manual/en/class.exception.php

        public string Message
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return ((PHPSPLException)(object)this).getMessage();
            }
        }


        public __Exception()
        {

        }

        public __Exception(string message)
        {

        }
    }

}
