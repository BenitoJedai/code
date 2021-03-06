﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    // http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Ast/NewExpression.cs
    [Script(Implements = typeof(global::System.Linq.Expressions.NewExpression))]
    internal class __NewExpression : __Expression
    {
        //script: error JSC1000: No implementation found for this native method, please implement [System.Linq.Expressions.ElementInit.get_Arguments()]

        //no implementation for System.Linq.Expressions.ListInitExpression 5d5caf46-34f9-3338-97c5-3fe54aa65cd0
        //script: error JSC1000: No implementation found for this native method, please implement [System.Linq.Expressions.ListInitExpression.get_NewExpression()]

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression
        // script: error JSC1000: No implementation found for this native method, please implement [System.Linq.Expressions.NewArrayExpression.get_Expressions()]
        //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?

        public ReadOnlyCollection<Expression> Arguments { get; set; }
        public ConstructorInfo Constructor { get; set; }
        public ReadOnlyCollection<MemberInfo> Members { get; set; }


        public override string ToString()
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
            //Console.WriteLine("Expression.New " + new { constructor, constructor.DeclaringType });

            return "NewExpression " + new { Constructor, Type }.ToString();
        }

    }

}
