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


            //var result0 = 
            //    from l in new Database.LeftTable().AsEnumerable()
            //     join rJoin in new Database.RightTable().AsEnumerable() on l.Key equals rJoin.ClientName
            //    group rJoin by rJoin.ClientName 
            //       ;

            //var result1 = result0.asd


            // Error	1	Could not find an implementation of the query pattern for source type 'ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy<AnonymousType#1>'.  'GroupBy' not found.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLGroupByAfterJoin\TestSQLGroupByAfterJoin\ApplicationWebService.cs	47	31	TestSQLGroupByAfterJoin
            // the fk?
            var a = (from l in new Database.LeftTable()
                     //.AsEnumerable()
                     //join rJoin in new Database.RightTable() on l.Key equals rJoin.ClientName // into test
                     join rJoin in new Database.RightTable()
                         //.AsEnumerable()
                     on l.Key equals rJoin.ClientName
                     // into test

                     // can we do away this select?
                     // our group by likes explicit views!
                     select new DatabaseJoinViewRow
                     {
                         ClientName = l.FirstName,

                         //ClientName = result.Last().FirstName,
                         //ClientName = ((DatabaseLeftTableRow)l).FirstName,
                         Payment = rJoin.Payment,
                         Tag = rJoin.Tag,
                         Timestamp = rJoin.Timestamp
                     } into rJoin

                     //from z in test

                     //group test by test.ClientName into result

                     group rJoin by rJoin.ClientName into result
                     select result.Last()

                    //select new DatabaseJoinViewRow
                //{
                //    //ClientName = result.Last().FirstName,
                //    //ClientName = ((DatabaseLeftTableRow)l).FirstName,
                //    Payment = result.Last().Payment,
                //    Tag = result.Last().Tag,
                //    Timestamp = result.Last().Timestamp
                //}
            );


            var a0 = a.AsDataTable();

            var a1 = a.AsEnumerable();

            return a1;
        }
    }
}
