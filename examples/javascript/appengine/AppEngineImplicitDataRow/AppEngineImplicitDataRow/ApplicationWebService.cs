using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppEngineImplicitDataRow
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            var r = new Data.Book1Sheet1Row
            {
                Title = "foo",
                Content = "bar"
            };

            new AppEngineImplicitDataRow.Data.Book1.Sheet1().Insert(r);
            new AppEngineImplicitDataRow.Data.Book1.Sheet1().Insert(
                    new Data.Book1Sheet1Row
                    {
                        Title = "foo",

                        // can we handle null yet?
                        Content = null
                    }
            );

            var z = new AppEngineImplicitDataRow.Data.Book1.Sheet1().SelectAllAsDataTable();



            //DataRow rr = r;

            // Unable to cast object of type 'System.DBNull' to type 'System.String'.

            var x = new AppEngineImplicitDataRow.Data.Book1.Sheet1().SelectAllAsEnumerable().ToArray();


            var rr = z.Rows[0];
            Data.Book1Sheet1Row rrr = rr;

            var xx =
                from k in new AppEngineImplicitDataRow.Data.Book1.Sheet1()
                //where k.Title == "foo"
                orderby k.Title
                select k;

            var xxx = xx.ToArray();

        }

    }

    public static class X
    {
        // Error	3	Could not find an implementation of the query pattern for source type 
        // 'AppEngineImplicitDataRow.Data.Book1.Sheet1'.  'OrderBy' not found.
        // X:\jsc.svn\examples\javascript\appengine\AppEngineImplicitDataRow\AppEngineImplicitDataRow\ApplicationWebService.cs	60	27	AppEngineImplicitDataRow


        public static IEnumerable<Data.Book1Sheet1Row> OrderBy(this AppEngineImplicitDataRow.Data.Book1.Sheet1 x, Expression<Func<Data.Book1Sheet1Row, string>> filter)
        {
            return Enumerable.OrderBy(x.SelectAllAsEnumerable(), k => k.Title);
        }

        public static IEnumerable<Data.Book1Sheet1Row> Where(this AppEngineImplicitDataRow.Data.Book1.Sheet1 x, Expression<Func<Data.Book1Sheet1Row, bool>> filter)
        {

            return x.SelectAllAsEnumerable().Where(k => k.Title == "foo");
        }
    }
}
