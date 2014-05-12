using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;

// ?
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SQLiteWithDataGridViewX.Data;
using ScriptCoreLib.Shared.Data.Diagnostics;

namespace SQLiteWithDataGridViewX
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        public SchemaTheGridTableKey ParentContentKey;

        public async Task<IEnumerable<SchemaTheGridTableViewRow>> SelectContent()
        {
            // Error	3	An expression tree may not contain a call or invocation that uses optional arguments	X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridViewX\SQLiteWithDataGridViewX\ApplicationWebService.cs	28	24	SQLiteWithDataGridViewX

            #region data

            //SQLiteWithDataGridViewX.Data.SchemaExtensions.Count()
            // does not work with rolsyn?
            if (new Schema.TheGridTable().Count() == 0)
            {
                // where is our treeview? databound?

                new SchemaTheGridTableUpdatesRow { ContentValue = "root v1", ContentComment = "c1" }.With(
                    root =>
                    {
                        #region TheGridTableUpdates
                        root.ContentReferenceKey = new Schema.TheGridTable().Insert(
                            new SchemaTheGridTableRow
                            {
                                ContentComment = root.ContentComment,
                                ContentValue = root.ContentValue
                            }
                        );

                        new Schema.TheGridTableUpdates().Insert(root);
                        #endregion



                        #region child v2
                        new SchemaTheGridTableUpdatesRow { ContentValue = "child v2", ContentComment = "child c2" }.With(
                             child =>
                             {

                                 child.ContentReferenceKey = new Schema.TheGridTable().Insert(
                                     new SchemaTheGridTableRow
                                     {
                                         ContentComment = child.ContentComment,
                                         ContentValue = child.ContentValue,

                                         ParentContentKey = root.ContentReferenceKey
                                     }
                                 );

                                 new Schema.TheGridTableUpdates().Insert(child);

                                 // lets simulate  updates
                                 child.ContentValue = "child v2 update1";
                                 new Schema.TheGridTableUpdates().Insert(child);

                                 child.ContentValue = "child v2 update2";
                                 new Schema.TheGridTableUpdates().Insert(child);

                             }
                         );
                        #endregion


                        #region child v3
                        new SchemaTheGridTableUpdatesRow { ContentValue = "child v3", ContentComment = "child c3" }.With(
                             child =>
                             {

                                 child.ContentReferenceKey = new Schema.TheGridTable().Insert(
                                     new SchemaTheGridTableRow
                                     {
                                         ContentComment = child.ContentComment,
                                         ContentValue = child.ContentValue,

                                         ParentContentKey = root.ContentReferenceKey
                                     }
                                 );

                                 new Schema.TheGridTableUpdates().Insert(child);
                             }
                         );
                        #endregion

                    }
                );


                new SchemaTheGridTableUpdatesRow { ContentValue = "root2 v1", ContentComment = "c1" }.With(
                 root =>
                 {
                     #region TheGridTableUpdates
                     root.ContentReferenceKey = new Schema.TheGridTable().Insert(
                         new SchemaTheGridTableRow
                         {
                             ContentComment = root.ContentComment,
                             ContentValue = root.ContentValue
                         }
                     );

                     new Schema.TheGridTableUpdates().Insert(root);
                     #endregion



                     #region child v2
                     new SchemaTheGridTableUpdatesRow { ContentValue = "child2 v2", ContentComment = "child c2" }.With(
                          child =>
                          {

                              child.ContentReferenceKey = new Schema.TheGridTable().Insert(
                                  new SchemaTheGridTableRow
                                  {
                                      ContentComment = child.ContentComment,
                                      ContentValue = child.ContentValue,

                                      ParentContentKey = root.ContentReferenceKey
                                  }
                              );

                              new Schema.TheGridTableUpdates().Insert(child);

                              // lets simulate  updates
                              child.ContentValue = "child2 v2 update1";
                              new Schema.TheGridTableUpdates().Insert(child);

                              child.ContentValue = "child2 v2 update2";
                              new Schema.TheGridTableUpdates().Insert(child);

                          }
                      );
                     #endregion


                     #region child v3
                     new SchemaTheGridTableUpdatesRow { ContentValue = "child2 v3", ContentComment = "child c3" }.With(
                          child =>
                          {

                              child.ContentReferenceKey = new Schema.TheGridTable().Insert(
                                  new SchemaTheGridTableRow
                                  {
                                      ContentComment = child.ContentComment,
                                      ContentValue = child.ContentValue,

                                      ParentContentKey = root.ContentReferenceKey
                                  }
                              );

                              new Schema.TheGridTableUpdates().Insert(child);
                          }
                      );
                     #endregion

                 }
             );
            }
            #endregion

            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
            // Error	233	A query body must end with a select clause or a group clause	X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridViewX\SQLiteWithDataGridViewX\ApplicationWebService.cs	93	54	SQLiteWithDataGridViewX


            // http://msdn.microsoft.com/en-us/library/system.linq.queryable.groupjoin.aspX



            var AllWithChildrenCount =
                from g in new Schema.TheGridTable()
                group g by g.ParentContentKey into gg
                select new SchemaTheGridTableViewRow
                {
                    // who are we?
                    // a parent to a group. lets get details later ?
                    //ContentKey = gg.Key,
                    ParentContentKey = gg.Key,

                    // how many children are we having?
                    ContentChildren = gg.Count(),


                    // whats the original data on it?
                    // ??

                    // last child in the group:
                    //ContentValue = gg.Last().ContentValue,
                    //ContentComment = gg.Last().ContentComment,

                    // how many updates are there?
                    // can we selet the latest update?
                };


            var z = AllWithChildrenCount.AsDataTable();


            var AllUpdates =
                from g in new Schema.TheGridTable()
                join u in new Schema.TheGridTableUpdates()
                on g.Key equals u.ContentReferenceKey

                select new SchemaTheGridTableViewRow
                {
                    // jsc should not generate Key, Tag, Timestamp for views?
                    // or can we also just use anonymous types?

                    //Key = g.Key

                    ContentKey = g.Key,

                    // for grouping
                    ParentContentKey = g.ParentContentKey,

                    ContentValue = u.ContentValue,
                    ContentComment = u.ContentComment,


                    Tag = u.Tag,
                    Timestamp = u.Timestamp
                };


            // http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/5711364-make-the-debugging-visualizers-non-modal-windows
            // i wish i could visualize and drop it here in comments.

            var AllUpdatesCount = AllUpdates.Count();
            var AllUpdatesAsDataTable = AllUpdates.AsDataTable();


            //            enter xslx
            //0b48:02:01 RewriteToAssembly error: System.IO.IOException: The process cannot access the file 'X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridViewX\SQLiteWithDataGridViewX\Data\Schema.xlsx' because it is being used by another process.


            var LatestUpdate =
                from g in AllUpdates
                group g by g.ContentKey into ug
                select new SchemaTheGridTableViewRow
                {
                    // who are we?
                    // a parent to a group. lets get details later ?
                    //ContentKey = gg.Key,
                    //ParentContentKey = gg.Key,

                    // how many children are we having?
                    UpdateCount = ug.Count(),

                    //ContentKey = g.Key,

                    // for grouping
                    //ParentContentKey = g.ParentContentKey,

                    ContentKey = ug.Last().ContentKey,
                    ParentContentKey = ug.Last().ParentContentKey,

                    ContentValue = ug.Last().ContentValue,
                    ContentComment = ug.Last().ContentComment,


                    Tag = ug.Last().Tag,
                    Timestamp = ug.Last().Timestamp
                };

            //at System.Data.DataRow.GetDataColumn(String columnName)
            //at System.Data.DataRow.get_Item(String columnName)
            //at SQLiteWithDataGridViewX.Data.SchemaTheGridTableViewRow.op_Implicit(DataRow )


            var LatestUpdate0 = LatestUpdate.AsDataTable();
            var LatestUpdate1 = LatestUpdate.AsEnumerable();

            var WhereParentContentKey =
                from x in LatestUpdate
                where x.ParentContentKey == ParentContentKey
                select x;

            var WhereParentContentKey0 = WhereParentContentKey.AsDataTable();
            var WhereParentContentKey1 = WhereParentContentKey.AsEnumerable();


            //var ffContentChildren = new Schema.TheGridTable().Count(x => x.ParentContentKey == ParentContentKey);

            var WhereParentContentKeyAndChildren = 
                from u in WhereParentContentKey1
                let ContentChildren = new Schema.TheGridTable().Count(x => x.ParentContentKey == u.ContentKey)
                select new SchemaTheGridTableViewRow
                {
                    // jsc should not generate Key, Tag, Timestamp for views?
                    // or can we also just use anonymous types?

                    //Key = g.Key

                    ContentKey = u.ContentKey,
                    UpdateCount = u.UpdateCount,

                    ContentChildren = ContentChildren,

                    // for grouping
                    ParentContentKey = u.ParentContentKey,

                    ContentValue = u.ContentValue,
                    ContentComment = u.ContentComment,


                    Tag = u.Tag,
                    Timestamp = u.Timestamp
                };

            //Debugger.Break();
            return WhereParentContentKeyAndChildren;
        }

    }
}
