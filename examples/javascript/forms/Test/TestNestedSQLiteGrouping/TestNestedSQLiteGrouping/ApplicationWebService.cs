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
using TestNestedSQLiteGrouping.Data;
using ScriptCoreLib.Extensions;

namespace TestNestedSQLiteGrouping
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2()
        {

            var bin = new Schema.Binary();

            Schema.GetDataSet().Tables[0].Rows.AsEnumerable().Select(x => (SchemaBinaryRow)x).WithEach(x => bin.Insert(x));

            var all =
                from z in new Schema.Binary()
                select z;

            var all0 =
                all.AsDataTable();


            var where =
                from z in new Schema.Binary()
                where z.B == "b"
                select z;

            var where0 = where.AsDataTable();


            var group =
                from z in new Schema.Binary()
                group z by z.B into g
                select new SchemaBinaryRow
                {
                    A = g.Last().A,
                    B = g.Key,
                    C = g.Count()
                };

            var group0 = group.AsDataTable();


            var ggroup =
                from z in new Schema.Binary()
                group z by z.B into g
                select new SchemaBinaryRow { A = g.Last().A, B = g.Key, C = g.Count() } into zz
                group zz by zz.A into gg
                select new SchemaBinaryRow { A = gg.Last().A, B = gg.Key, C = gg.Sum(r => r.C) };


            var ggroup0 = ggroup.AsDataTable();

        }

    }
}
