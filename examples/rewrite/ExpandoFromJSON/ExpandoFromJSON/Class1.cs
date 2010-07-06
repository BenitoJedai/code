using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]
namespace ExpandoFromJSON
{
    public class Expando
    {
        public static Expando FromJSON(string e)
        {
            Expando r = null;

            if (e != null)
            {
                try
                {

                    //r = new IFunction("return (" + e + ");").CreateType();
                    r = new IFunction("return;").CreateType();
                }
                catch
                {
                    throw new global::System.Exception("Could not create object from json string : " + e);

                }
            }


            return r;
        }

    }
}
