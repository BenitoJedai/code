using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: ObfuscationAttribute(Feature = "script")]

namespace TestJavaNativesWithReferences
{
    public interface Class1
    {
        Class2 method2();

        string GetDescription();
    }



    public class Class2 : Class1
    {
        #region fields
        public Class1 field1;

#if WithMissingReference
        /// <summary>
        /// This will cause all fields be missing
        /// </summary>
        public MissingClass1 MissingField;
#endif
        #endregion

        #region methods
        public Class2 method2()
        {
            // note we cannot use CLR as ScriptCoreLibJava isnt referenced to provide the implementation
            return null;
        }

#if WithMissingReference
        /// <summary>
        /// This will cause all methods to be missing
        /// </summary>
        /// <param name="e"></param>
        public void MissingMethod(MissingClass1 e)
        {
        }
#endif
        #endregion


        public string GetDescription()
        {
            return "hello world";
        }
    }



    public class MissingClass1
    {
    }
}
