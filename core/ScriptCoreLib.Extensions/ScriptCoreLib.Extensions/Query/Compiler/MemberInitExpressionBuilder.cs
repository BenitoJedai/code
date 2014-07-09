using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ScriptCoreLib.Query.Compiler
{
    [Obsolete("this is the first type exposes from scriptcorelib yet used by jsc assetslibrary")]
    //[AssetCompiler]
    public class MemberInitExpressionBuilder
    {

        // X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140707/xlsx

        public static LocalBuilder EmitLambdaExpression(ILGenerator il, Type SourceType, string ParameterName, FieldInfo[] SourceFields)
        {
            // we are about to gen expression code like roslyn does.

            #region ref
            Func<RuntimeFieldHandle, FieldInfo> refGetFieldFromHandle = FieldInfo.GetFieldFromHandle;
            Func<RuntimeTypeHandle, Type> refGetTypeFromHandle = Type.GetTypeFromHandle;
            Func<Type, string, ParameterExpression> refParameter = Expression.Parameter;
            Func<Type, NewExpression> refNew = Expression.New;
            Func<NewExpression, MemberBinding[], MemberInitExpression> refMemberInit = Expression.MemberInit;



   //at System.Linq.Expressions.Expression.ValidateLambdaArgs(Type delegateType, Expression& body, ReadOnlyCollection`1 parameters)
   //at System.Linq.Expressions.Expression.Lambda[TDelegate](Expression body, String name, Boolean tailCall, IEnumerable`1 parameters)
   //at System.Linq.Expressions.Expression.Lambda[TDelegate](Expression body, Boolean tailCall, IEnumerable`1 parameters)
   //at System.Linq.Expressions.Expression.Lambda[TDelegate](Expression body, ParameterExpression[] parameters)
   //at TestXMySQL.PerformanceResourceTimingData2ApplicationPerformance..ctor()
            
            //    at System.Linq.Expressions.Expression.ValidateLambdaArgs(Type delegateType, Expression& body, ReadOnlyCollection`1 parameters)

            // <(xAvatarRow) -> xAvatarRow
            //Func<Expression, ParameterExpression[], Expression<Func<object, object>>> refLambda = Expression.Lambda<Func<object, object>>;
            Func<Expression, ParameterExpression[], Expression> refLambda = Expression.Lambda;
            Func<Expression, FieldInfo, MemberExpression> refField = Expression.Field;
            Func<MemberInfo, Expression, MemberAssignment> refBind = Expression.Bind;
            #endregion

            var xLambda = il.DeclareLocal(typeof(Expression));

            #region xParameterExpression
            var xParameterExpression = il.DeclareLocal(typeof(ParameterExpression));

            // http://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.ldtoken(v=vs.110).aspx
            il.Emit(OpCodes.Ldtoken, SourceType);
            il.Emit(OpCodes.Call, refGetTypeFromHandle.Method);
            il.Emit(OpCodes.Ldstr, ParameterName);
            il.Emit(OpCodes.Call, refParameter.Method);
            il.Emit(OpCodes.Stloc_S, (byte)xParameterExpression.LocalIndex);
            #endregion

            il.Emit(OpCodes.Ldtoken, SourceType);
            il.Emit(OpCodes.Call, refGetTypeFromHandle.Method);
            il.Emit(OpCodes.Call, refNew.Method);
            il.Emit(OpCodes.Ldc_I4, SourceFields.Length);
            il.Emit(OpCodes.Newarr, typeof(MemberBinding));

            // ...

            for (int i = 0; i < SourceFields.Length; i++)
            {
                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Ldc_I4, i);

                var SourceField = SourceFields[i];
                il.Emit(OpCodes.Ldtoken, SourceField);
                il.Emit(OpCodes.Call, refGetFieldFromHandle.Method);
                il.Emit(OpCodes.Ldloc_S, (byte)xParameterExpression.LocalIndex);
                il.Emit(OpCodes.Ldtoken, SourceField);
                il.Emit(OpCodes.Call, refGetFieldFromHandle.Method);
                il.Emit(OpCodes.Call, refField.Method);
                il.Emit(OpCodes.Call, refBind.Method);

                il.Emit(OpCodes.Stelem_Ref);
            }


            il.Emit(OpCodes.Call, refMemberInit.Method);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Newarr, typeof(ParameterExpression));
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ldloc_S, (byte)xParameterExpression.LocalIndex);
            il.Emit(OpCodes.Stelem_Ref);
            il.Emit(OpCodes.Call, refLambda.Method);
            il.Emit(OpCodes.Stloc_S, (byte)xLambda.LocalIndex);

            return xLambda;
        }
    }
}
