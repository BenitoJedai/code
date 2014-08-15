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
        // X:\jsc.svn\examples\javascript\test\TestSQLiteConnection\TestSQLiteConnection\Application.cs

        public static NewExpression New(Type Type)
        {
            //var Type = default(Type);

            //if ((object)constructor != null)
            //    Type = constructor.DeclaringType;

            //Console.WriteLine("Expression.New " + new
            //{
            //    constructor
            //    //, constructor.DeclaringType 
            //});
            // method: System.Linq.Expressions.NewExpression New(System.Reflection.ConstructorInfo, System.Linq.Expressions.Expression[])
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs

            return
                (NewExpression)(object)
                new __NewExpression
                {
                    NodeType = ExpressionType.New,

                    //Constructor = constructor,
                    Type = Type,

                    Arguments = new global::System.Collections.ObjectModel.ReadOnlyCollection<Expression>(new Expression[0].ToList()),
                    Members = new global::System.Collections.ObjectModel.ReadOnlyCollection<MemberInfo>(new MemberInfo[0].ToList()),

                };
        }


        public static NewExpression New(ConstructorInfo constructor, Expression[] arguments)
        {
            var Type = default(Type);

            if ((object)constructor != null)
                Type = constructor.DeclaringType;

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\JVMCLRSyntaxOrderByThenGroupBy\Program.cs

            //Console.WriteLine("Expression.New " + new
            //{
            //    constructor,
            //    Type
            //    //, constructor.DeclaringType 
            //});

            // method: System.Linq.Expressions.NewExpression New(System.Reflection.ConstructorInfo, System.Linq.Expressions.Expression[])
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs

            return
                (NewExpression)(object)
                new __NewExpression
                {
                    NodeType = ExpressionType.New,

                    Constructor = constructor,
                    Type = Type,

                    Arguments = new global::System.Collections.ObjectModel.ReadOnlyCollection<Expression>(arguments.ToList()),
                    Members = new global::System.Collections.ObjectModel.ReadOnlyCollection<MemberInfo>(new MemberInfo[0].ToList()),

                };
        }



        public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments, params MemberInfo[] members)
        {
            // X:\jsc.svn\examples\javascript\linq\visualized\VisualizeWhere\VisualizeWhere\Application.cs

            var Type = default(Type);

            if ((object)constructor != null)
                Type = constructor.DeclaringType;

            //GetConstructor { FullName = __AnonymousTypes__TestSQLJoin_ApplicationWebService.__f__AnonymousType_694_0_2, parameters = [LScriptCoreLibJava.BCLImplementation.System.__Type;@f9651 }
            //GetConstructor { FullName = __AnonymousTypes__TestSQLJoin_ApplicationWebService.__f__AnonymousType_694_0_2, cc = .ctor() }
            //Expression.New { constructor = .ctor(), DeclaringType = , 

            // arguments = ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator_1@1e5182f, 
            // members = [LScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo;@196e0b0 }

            //GetConstructor { FullName = __AnonymousTypes__TestSQLJoin_ApplicationWebService.__f__AnonymousType_694_0_2, parameters = [LScriptCoreLibJava.BCLImplementation.System.__Type;@f9651 }
            //GetConstructor { FullName = __AnonymousTypes__TestSQLJoin_ApplicationWebService.__f__AnonymousType_694_0_2, cc = .ctor() }
            //Expression.New { constructor = .ctor(), DeclaringType = , arguments = ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator_1@1e5182f, members = [LScriptCoreLibJava.BCLImpl

            //Lambda { body = NewExpression { Constructor = .ctor(), Type =  } }

            Console.WriteLine("Expression.New " + new
            {
                constructor,
                //, constructor.DeclaringType, 
                arguments,
                members
            });
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyOfTRowExtensions.Join.cs

            return
                (NewExpression)(object)
                new __NewExpression
                {
                    NodeType = ExpressionType.New,

                    Constructor = constructor,
                    Type = Type,

                    Arguments = new global::System.Collections.ObjectModel.ReadOnlyCollection<Expression>(arguments.ToList()),
                    Members = new global::System.Collections.ObjectModel.ReadOnlyCollection<MemberInfo>(members.ToList()),

                };
        }



    }
}
