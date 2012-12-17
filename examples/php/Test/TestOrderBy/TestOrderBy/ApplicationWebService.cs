using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace TestOrderBy
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            string sql = "foo @x @x bar";
            int Count = 1;


            if (Count > 0)
            {

                //Console.WriteLine("we have InternalParameters for " + sql);

                var parameters = new[]
                {
                    new { ParameterName = "@x", Value = "u" },
                };

                var index =
                   from p in parameters
                   from i in sql.GetIndecies(p.ParameterName)
                   orderby i
                   select new { p, i };


                foreach (var p in parameters)
                {
                    // java seems to like indexed parameters instead
                    sql = sql.Replace(p.ParameterName, "?");
                }

                y(sql);
                // add values
            }
        }
    }
}
