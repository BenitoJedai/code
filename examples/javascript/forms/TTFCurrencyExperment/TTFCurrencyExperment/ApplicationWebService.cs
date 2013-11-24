using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TTFCurrencyExperment
{
    public class XSheet1
    {
        public enum XKey : long { }
    }


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

            // Additional information: Common Language Runtime detected an invalid program.
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
            // 		Design.Treasury.Sheet1.Key	TTFCurrencyExperment.Design.Treasury.Sheet1.Key	long
            var row1 = new Design.Treasury.Sheet1.Row
            {
                Currency = "EUR",
                Value = "456"

                // data
            };

            var insert0 = (Task<Design.Treasury.Sheet1.Key>)table1q.WithConnection(c => Design.Treasury.Sheet1.Queries.Insert(c, row1));
            Console.WriteLine(new { insert0.Result });

            //table1.Insert(

            //var select0 = (Task<DataTable>)table1q.WithConnection(c => Design.Treasury.Sheet1.Queries.SelectAllAsDataTable(c));
            var select0 = (Task<DataTable>)table1q.WithConnection(Design.Treasury.Sheet1.Queries.SelectAllAsDataTable);

            // Additional information: Bad method token.
            var k = table1.Insert(
                 new Design.Treasury.Sheet1.Row
                 {
                     Currency = "GBP",
                     Value = "777"

                     // data
                 }
            );

            var a = table1.SelectAllAsDataTable();

            var zz = table1.SelectAllAsEnumerable();
            var zza = zz.ToArray();

            var z =
                from x in table1.XSelectAllAsEnumerable()
                group x by x.Currency;



            // will this datatable allow to bind back?
            // if it wont allow update or delete, will it allow inserts?
            // would it be secure?


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
        public static IEnumerable<Design.Treasury.Sheet1.Row> XSelectAllAsEnumerable(this Design.Treasury.Sheet1 data)
        {
            var x = data.SelectAllAsDataTable();

            return x.Rows.AsEnumerable().Select(
                r =>
                    new Design.Treasury.Sheet1.Row
                    {
                        Key = (Design.Treasury.Sheet1.Key)r["Key"],
                        Currency = (string)r["Currency"],
                        Value = (string)r["Value"]
                    }
            );
        }

        class _Insert_closure
        {
            public Design.Treasury.Sheet1.Row value;

            public Task<Design.Treasury.Sheet1.Key> yield(SQLiteConnection c)
            {
                return Design.Treasury.Sheet1.Queries.Insert(
                    c, value
                );

            }
        }

        //public static Design.Treasury.Sheet1.Key Insert(this Design.Treasury.Sheet1.Queries data, Design.Treasury.Sheet1.Row value)
        //{
        //    var loc = new _Insert_closure();
        //    loc.value = value;

        //    var x = data.WithConnection(loc.yield);
        //    var y = (Task<Design.Treasury.Sheet1.Key>)x;
        //    return y.Result;
        //}

        public static IEnumerable<Design.Treasury.Sheet1.Row> Select<T>(this _Treasury_Sheet1_Where data, Expression<Func<Design.Treasury.Sheet1.Row, T>> f)
        {
            // which fields do we need? all or specific?
            // or are we doing a join?

            return null;
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
