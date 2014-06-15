using System;
using System.Diagnostics;
using System.Linq.Expressions;
namespace ComplexQueryExperiment
{
    public partial class ApplicationWebService
    {
        public void WebMethod2()
        {
            // running out of brain power are we?
            // we need a high level overview.
            // all examples at the same time.
            // all tests shall work for sqlite and mysql



            // Error	1	Could not find an implementation of the query pattern for source type 'ComplexQueryExperiment.xTable'.  'Select' not found.	X:\jsc.svn\examples\javascript\LINQ\ComplexQueryExperiment\ComplexQueryExperiment\ApplicationWebService.WebMethod2.cs	13	31	ComplexQueryExperiment
            var x = from z in new xTable()
                    let field3 = z.field1
                    let field4 = z.field1 + z.field2
                    let field5 = z.field1 + 33

                    where field5 > 44
                    where new { field3 }.field3 > field5

                    let field6 = field3 + field4 + field5
                    let field7 = new { z.field1, field6 }
                    let field8 = "???"
                    let field9 = new { field3, field7, x = new { field4 }, y = new[] { field5, field6 } }
                    select z;



            var f = x.FirstOrDefault();


            Debugger.Break();
        }

        // we need to extract alll above into a hige frikking expression tree
        // we need to be able to encrypt the private state of the query
        // so we could send it to the client for additions?

    }

    static class FrikkingExpressionBuilder
    {
        public static TElement FirstOrDefault<TElement>(this IQueryStrategy<TElement> source)
        {
            // selector = {<>h__TransparentIdentifier6 => <>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.z}

            // convert to SQL!

            Console.WriteLine("select ?");
            Console.WriteLine("from ?");

            Debugger.Break();
            return default(TElement);
        }

        class xWhere<TElement> : IQueryStrategy<TElement>
        {
            public IQueryStrategy<TElement> source;
            public Expression<Func<TElement, bool>> filter;

        }

        // called by LINQ
        public static IQueryStrategy<TElement> Where<TElement>(this IQueryStrategy<TElement> source, Expression<Func<TElement, bool>> filter)
        {
            return new xWhere<TElement>
            {
                source = source,
                filter = filter
            };
        }


        class xSelect<TSource, TResult> : IQueryStrategy<TResult>
        {
            public IQueryStrategy<TSource> source;
            public Expression<Func<TSource, TResult>> selector;

        }

        // called by LINQ
        public static IQueryStrategy<TResult> Select<TSource, TResult>(this IQueryStrategy<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            return new xSelect<TSource, TResult>
            {
                source = source,
                selector = selector
            };
        }
    }
}
