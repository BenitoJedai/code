using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    internal abstract partial class __Expression
    {
        public static Expression<TDelegate> Lambda<TDelegate>(Expression body, params ParameterExpression[] parameters)
        {
            // MemberExpression


            //Console.WriteLine("Lambda " + new { body });


            return
                 (Expression<TDelegate>)(object)
                 new __Expression<TDelegate>
                 {
                     NodeType = ExpressionType.Lambda,

                     Body = body,

                     // X:\jsc.svn\examples\java\test\JVMCLRIdentityExpression\JVMCLRIdentityExpression\Program.cs
                     Parameters = new global::System.Collections.ObjectModel.ReadOnlyCollection<ParameterExpression>(
                         parameters.ToList()
                     )
                 };
        }



    }
}
