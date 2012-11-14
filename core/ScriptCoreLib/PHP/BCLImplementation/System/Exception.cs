using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    // http://www.php.net/manual/en/class.exception.php
    [Script(IsNative = true, ExternalTarget = "Exception")]
    internal class PHPSPLException
    {
        public PHPSPLException()
        {

        }

        public PHPSPLException(string message)
        {

        }

        public string getMessage()
        {
            return default(string);
        }

        // final public string getTraceAsString ( void )
        public string getTraceAsString()
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

        public string StackTrace
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return ((PHPSPLException)(object)this).getTraceAsString();
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
