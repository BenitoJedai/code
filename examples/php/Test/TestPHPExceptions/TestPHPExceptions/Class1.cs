using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib;
[assembly: Obfuscation(Feature = "script")]


namespace TestPHPExceptions
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

    [Script(Implements = typeof(global::System.NotImplementedException))]
    internal class __NotImplementedException : __Exception
    {
        //public __NotImplementedException()
        //{

        //}

        public __NotImplementedException(string m) : base(m)
        {


        }
    }

    public class Class1
    {
        void Method1NotImplementedException()
        {
            throw new NotImplementedException("");
        }

        void Method2NotImplementedException()
        {
            throw new NotImplementedException("message");
        }

        void Method3NotImplementedException()
        {
            try
            {
                Method2NotImplementedException();
            }
            catch (NotImplementedException e)
            {
                throw new NotImplementedException(e.Message);
            }

        }

        public Class1()
        {

        }

        void Method1()
        {
            throw new Exception();
        }

        void Method2()
        {
            throw new Exception("message");
        }

        void Method3()
        {
            try
            {
                Method2();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
