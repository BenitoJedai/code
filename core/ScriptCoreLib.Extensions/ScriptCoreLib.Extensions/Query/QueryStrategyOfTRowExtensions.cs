using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using ScriptCoreLib.Shared.Data.Diagnostics;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Ultra.Library;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace System.Data
{
    // move to a nuget?
    // shall reimplement IQueriable for jsc data layer gen

    //[Obsolete]
    public static partial class QueryStrategyOfTRowExtensions
    {
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\Program.cs

        #region XQueryStrategy
        class XQueryStrategy<TRow> : IQueryStrategy<TRow>
        {

            List<Action<QueryStrategyExtensions.CommandBuilderState>> InternalCommandBuilder = new List<Action<QueryStrategyExtensions.CommandBuilderState>>();

            public List<Action<QueryStrategyExtensions.CommandBuilderState>> GetCommandBuilder()
            {
                return InternalCommandBuilder;
            }

            public Func<IQueryDescriptor> InternalGetDescriptor;

            public IQueryDescriptor GetDescriptor()
            {
                //  public static DataTable AsDataTable(IQueryStrategy Strategy)

                return InternalGetDescriptor();
            }

            public Func<Type> InternalGetElementType;
            public Type GetElementType()
            {
                if (InternalGetElementType != null)
                    return InternalGetElementType();

                // not sure. replaced by selectorExpression?
                return null;
            }
        }
        #endregion




    


   


        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Enumerable.Methods.cs

        //public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
        //    this IEnumerable<TOuter> outer,
        //    IEnumerable<TInner> inner,
        //    Func<TOuter, TKey> outerKeySelector,
        //    Func<TInner, TKey> innerKeySelector,
        //    Func<TOuter, TInner, TResult> resultSelector)


        // public static Book1.DealerContact Where(this Book1.DealerContact value, Expression<Func<Book1DealerContactRow, bool>> value);
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140429

        // Error	4	Could not find an implementation of the query pattern for source type 
        // 'TestSQLJoin.Data.Book1.DealerContact'.  'Join' not found.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	51	33	TestSQLJoin


        // http://social.msdn.microsoft.com/Forums/en-US/bf98ec7a-cb80-4901-8eb2-3aa6636a4fde/linq-join-error-the-type-of-one-of-the-expressions-in-the-join-clause-is-incorrect-type-inference?forum=linqprojectgeneral
        // http://weblogs.asp.net/rajbk/archive/2010/03/12/joins-in-linq-to-sql.aspx
        // http://msdn.microsoft.com/en-us/library/bb311040.aspx
        // http://thomashundley.com/post/2010/05/20/The-type-of-one-of-the-expressions-in-the-join-clause-is-incorrect-Type-inference-failed-in-the-call-to-Join.aspx
        // http://www.roelvanlisdonk.nl/?p=2904
        // is this it?
        // http://www.pcreview.co.uk/forums/linq-join-using-expression-tree-t3432559.html

        //[Obsolete("whats the correct signature?")]
        //public static IEnumerable<TestSQLJoin.Data.Book1TheViewRow> Join<TKey>(

        // do we need  IQueryable<> ?

        //[Obsolete("can we get rid of the return type too? how would that look like?")]






    }
}

