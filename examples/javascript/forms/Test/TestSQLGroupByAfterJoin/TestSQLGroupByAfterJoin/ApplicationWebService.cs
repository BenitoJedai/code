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
using TestSQLGroupByAfterJoin.Data;

namespace TestSQLGroupByAfterJoin
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
        public async Task<IEnumerable<DatabaseJoinViewRow>> WebMethod2()
        {

            var SourceDataSet = Database.GetDataSet();

            var leftTable = new Database.LeftTable();
            SourceDataSet.Tables["LeftTable"].Rows.AsEnumerable().WithEach(r => leftTable.Insert(r));

            var RightTable = new Database.RightTable();
            SourceDataSet.Tables["RightTable"].Rows.AsEnumerable().WithEach(r => RightTable.Insert(r));

            return (from l in new Database.LeftTable()
                      join rJoin in new Database.RightTable() on l.Key equals rJoin.ClientName // into test
                      group test by test.ClientName into result
                      select new DatabaseJoinViewRow
                      {
                          ClientName = result.Last().FirstName,
                          Payment = result.Last().Payment,
                          Tag = result.Last().Tag,
                          Timestamp = result.Last().Timestamp
                      }).AsEnumerable();
        }
    }
}
