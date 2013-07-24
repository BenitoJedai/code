using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]
namespace TestObjectGetType
{
    [Script(Implements = typeof(global::System.Type))]
    internal class __Type
    { }


    [Script(

            Implements = typeof(global::System.Object)
        //ImplementationType = typeof(global::java.lang.Object)

            //Implements = typeof(global::System.Object),
        //ImplementationType = typeof(object)

            )]
    internal class __Object
    {


        //    public static  __Object GetType_06000001(__Object that)
        //     public static  __Type System_Object_GetType_06000007(__Object that)
        [Script(DefineAsStatic = true)]
        new public Type GetType()
        {

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/20130723


            return null;
        }


    }
}
