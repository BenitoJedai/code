using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Linq.Expressions;

namespace TestANDExpression
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Expression<Func<test, bool>> f = a => a.i == 4 && a.ii == 5; 

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
