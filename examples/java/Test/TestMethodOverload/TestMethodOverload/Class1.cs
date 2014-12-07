using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestMethodOverload
{
    public class LambdaExpression
    {


    }

    public class Expression<TDelegate> : LambdaExpression
    {
    }

    public class Class1
    {
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.Where.cs

        public static void Where<TDelegate>(TDelegate s, Expression<TDelegate> y)
        {
            Where(s, (LambdaExpression)y);
        }

        public static void Where<TDelegate>(TDelegate s, LambdaExpression y)
        {

        }
    }
}
