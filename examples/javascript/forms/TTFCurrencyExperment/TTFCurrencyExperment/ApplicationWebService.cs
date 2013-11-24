using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TTFCurrencyExperment
{

    [DesignerCategory("code")]
    public partial class ApplicationWebService : Component
    {
        // is bitcoin the internal banking database format?

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // if we send a type to the client
            // if the client was avare of the type
            // we send the reference id
            // otherwise we have to send the members of this unknown type tooo

            // what about generic type overrides?
            var table1type = typeof(Design.Treasury.Sheet1);


            //var table1type = typeof(Design.Treasury.Sheet1<TCurrency, TValue>);

            // X:\jsc.internal.svn\compiler\jsc.internal\jsc.internal\meta\Commands\Reference\ReferenceAssetsLibrary.cs
            var table1 = new Design.Treasury.Sheet1();

            var table1q = new Design.Treasury.Sheet1.Queries();

            // did you add package?
            // "X:\jsc.svn\examples\javascript\forms\TTFCurrencyExperment\TTFCurrencyExperment\bin\Debug\Treasury.xlsx.sqlite"
            var create0 = table1q.WithConnection(c => Design.Treasury.Sheet1.Queries.Create(c));


            // should the keys going to the client side be encrypted?
            // internally in db it shall be 64bit long, yet the client should see something else?
            // or would one time pad be good enough to scramle
            // http://encyclopedia2.thefreedictionary.com/One+time+pad
            // do we need an explicit attribute to tell compiler to encrypt this value for the client?
            // clientside could use obfucation of whitespaces?
            // or show the id, yet sign it, so user cannot change it?
            // signed enums
            var keytype = default(Design.Treasury.Sheet1.Key);


            // should this be enought for this obect to be added?
            var row1 = new Design.Treasury.Sheet1.Row
            {
                Currency = "EUR",
                Value = "456"

                // data
            };

            var insert0 = table1q.WithConnection(c => Design.Treasury.Sheet1.Queries.Insert(c, row1));
            var select0 = (Task<DataTable>)table1q.WithConnection(c => Design.Treasury.Sheet1.Queries.SelectAll(c));


            var k = table1.Insert(
                row1
            );



            // will this datatable allow to bind back?
            // if it wont allow update or delete, will it allow inserts?
            // would it be secure?
            var all = table1.GetDataTable();


            TryExpressions(table1);


            // insert row to get new key
            // what about data relationships?


            // we can not send the table to the user space
            // we could send a special secured perspective of it?

            // Send it back to the caller.
            y(e);
        }

        private static void TryExpressions(Design.Treasury.Sheet1 table1)
        {
            var exp1 = table1.Where(x => x.Currency == "EUR");
            var exp2 = exp1.Select(x => x);

            var ep3 = from x in table1
                      where x.Currency == "GBP"
                      select x;
        }

    }

    public class _Treasury_Sheet1_Where
    {
    }
    public static class XXX
    {
        // what about skip take and order by?

        // select all data. expensive
        public static DataTable GetDataTable(this Design.Treasury.Sheet1 data)
        {
            return null;
        }

        public static Design.Treasury.Sheet1.Key Insert(this Design.Treasury.Sheet1 data, Design.Treasury.Sheet1.Row r)
        {
            // { InsertCommandText = insert into Sheet1 (Currency, Value)  values (@Currency, @Value) }

            Console.WriteLine(
                new
                {
                    Design.Treasury.Sheet1.Queries.CreateCommandText,

                }
            );


            Console.WriteLine(
                new
                {
                    Design.Treasury.Sheet1.Queries.InsertCommandText,
                }
            );


            Console.WriteLine(
                new
                {
                    Design.Treasury.Sheet1.Queries.SelectAllCommandText,

                }
            );

            return default(Design.Treasury.Sheet1.Key);
        }

        public static object Select<T>(this _Treasury_Sheet1_Where data, Expression<Func<Design.Treasury.Sheet1.Row, T>> f)
        {
            // which fields do we need? all or specific?
            // or are we doing a join?

            return new object();
        }

        public static _Treasury_Sheet1_Where Where(this Design.Treasury.Sheet1 data, Expression<Func<Design.Treasury.Sheet1.Row, bool>> f)
        {
            // X:\jsc.svn\examples\javascript\forms\QueryableWebServiceExperiment\QueryableWebServiceExperiment\System\Linq\LambdaExpressionExtensions.cs

            // jsc compiler shall precompile all variations
            // on which fields we allow to select.
            // n x n ?
            // and then in this method
            // select the one needed?
            // how much code bloat will this be?
            // or 1n?
            // we can reorder the where selectors
            // what about expressions to be sent over to sql?

            // http://csharpeval.codeplex.com/
            // https://code.google.com/p/expressiontocode/
            // http://blogs.msdn.com/b/alexj/archive/2010/03/02/creating-a-data-service-provider-part-9-un-typed.aspx
            // http://blogs.msdn.com/b/mattwar/archive/2007/07/31/linq-building-an-iqueryable-provider-part-ii.aspx

            // first iteration
            // shall enable to do a where by 1 data column only?
            // can we have the client send in the expression?
            // would we trust the expression from client?
            // or would we only ask for a specific parameter for the expression?

            return new _Treasury_Sheet1_Where { };

        }
    }
}
