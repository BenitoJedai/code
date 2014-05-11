using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using SQLiteWithDataGridViewX.Data;
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

namespace SQLiteWithDataGridViewX
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {

        public void SelectContent()
        {
            // Error	3	An expression tree may not contain a call or invocation that uses optional arguments	X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridViewX\SQLiteWithDataGridViewX\ApplicationWebService.cs	28	24	SQLiteWithDataGridViewX

            #region data
            if (new Schema.TheGridTable().Count() == 0)
            {
                new SchemaTheGridTableUpdatesRow { ContentValue = "v1", ContentComment = "c1" }.With(
                    root =>
                    {

                        root.ContentReferenceKey = new Schema.TheGridTable().Insert(
                            new SchemaTheGridTableRow
                            {
                                ContentComment = root.ContentComment,
                                ContentValue = root.ContentValue
                            }
                        );

                        new Schema.TheGridTableUpdates().Insert(root);



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
                    }
                );
            }
            #endregion

            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
            // Error	233	A query body must end with a select clause or a group clause	X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridViewX\SQLiteWithDataGridViewX\ApplicationWebService.cs	93	54	SQLiteWithDataGridViewX


            // http://msdn.microsoft.com/en-us/library/system.linq.queryable.groupjoin.aspX

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
                    ContentComment = u.ContentComment

                };


            var AllUpdatesByParentContentKey =
                from g in new Schema.TheGridTable()
                //from g in AllUpdates
                group g by g.ParentContentKey into gg
                select new SchemaTheGridTableViewRow
                {
                    // who are we?
                    // a parent to a group. lets get details later ?
                    ContentKey = gg.Key,

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


            var z = AllUpdatesByParentContentKey.AsDataTable();

            // http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/5711364-make-the-debugging-visualizers-non-modal-windows
            // i wish i could visualize and drop it here in comments.

            var AllUpdatesCount = AllUpdates.Count();
            var AllUpdatesAsDataTable = AllUpdates.AsDataTable();

            Debugger.Break();
        }

    }
}
