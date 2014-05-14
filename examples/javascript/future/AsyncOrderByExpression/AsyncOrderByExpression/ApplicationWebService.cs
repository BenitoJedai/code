using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq.Expressions;

public class FooRow(public string goo, public string zoo) { }

static class SchemaContext
{
    // jsc data layer uses
    // IQueryStrategy<> for now instead of IQueryable

    public static IQueryable<FooRow> foo
    { get; }
    = new[] {
                new FooRow("foo8", "bar9"),
                new FooRow("foo9", "bar2"),
                new FooRow("foo7", "bar3")
    }.AsQueryable();
}

namespace AsyncOrderByExpression
{
    using SchemaContext;

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        // X:\jsc.svn\examples\javascript\test\TestPrimaryConstructorData\TestPrimaryConstructorData\ApplicationWebService.cs



        //     at System.Signature.GetSignature(Void* pCorSig, Int32 cCorSig, RuntimeFieldHandleInternal fieldHandle, IRuntimeMethodInfo methodHandle, RuntimeType declaringType)
        //at System.Reflection.RuntimeMethodInfo.FetchNonReturnParameters()
        //at System.Reflection.RuntimeMethodInfo.GetParameters()
        //at jsc.IL2Script.DeclareMethods(IdentWriter w, MethodBase[] mi) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2Script.cs:line 1206
        //at jsc.IL2Script.DeclareTypes(IdentWriter w, Type[] arg_types, Boolean debug, ScriptAttribute attribute, Assembly assembly, CompileSessionInfo sinfo) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2Script.DeclareTypes.cs:line 460

        //public async Task<IEnumerable<FooRow>> WebMethod2<TKey>(Expression<Func<FooRow, TKey>> f)
        //{
        //    // http://stackoverflow.com/questions/5061761/is-it-possible-to-await-yield-return-dosomethingasync
        //    // http://www.interact-sw.co.uk/iangblog/2013/11/29/async-yield-return

        //    //yield return new foo("hello", "world");


        //    return foo.OrderBy(f);
        //}




        public async Task<IEnumerable<FooRow>> WithSelector(Expression<Func<IQueryable<FooRow>, IQueryable<FooRow>>> f)
        {
            // Error	4	Argument 1: cannot convert from 'System.Linq.IOrderedEnumerable<FooRow>' to
            // 'System.Linq.Expressions.Expression<System.Linq.IOrderedEnumerable<FooRow>>'	X:\jsc.svn\examples\javascript\future\AsyncOrderByExpression\AsyncOrderByExpression\ApplicationControl.cs	33	17	AsyncOrderByExpression


            // http://stackoverflow.com/questions/5061761/is-it-possible-to-await-yield-return-dosomethingasync
            // http://www.interact-sw.co.uk/iangblog/2013/11/29/async-yield-return

            //yield return new foo("hello", "world");

            var ff = f.Compile();

            return ff(foo);

            //foo.Select()
            //return foo.Select(f);
        }

    }
}
