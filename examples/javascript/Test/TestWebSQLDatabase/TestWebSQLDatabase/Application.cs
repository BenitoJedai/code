using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestWebSQLDatabase;
using TestWebSQLDatabase.Design;
using TestWebSQLDatabase.HTML.Pages;
using System.Diagnostics;

namespace TestWebSQLDatabase
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\test\TestSQLiteConnection\TestSQLiteConnection\Application.cs

            // http://cheggeng.github.io/Bucket/docs/files/src_drivers_WebSQL.js.html

            // as per http://www.c-sharpcorner.com/UploadFile/75a48f/html-5-web-sql-database/
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.cs
            // A Web SQL database only works in the latest versions of Safari, Google Chrome and Opera browsers.
            // no ff, ie?
            // what if the IE logo on rebuild would indicate what browser can run the current app based on analysis?
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs

            // http://programmers.stackexchange.com/questions/220254/why-is-web-sql-database-deprecated
            // Should you go with Web SQL now? I don't expect the vendors that currently support it (like Google and Apple) to drop it any time soon, but IE and Firefox won't be adding it, and since it's deprecated, why invest in it? 
            // We (developers) can still use this technology. No browser vendor requested removal of this technology, nor plan to remove it. Developers are the voice of the web. We can just still using it, maybe Mozilla will change mind ;-)
            // Forget Mozilla on this issue. If you want persistent, reliable and query enabled storage for your hybrid mobile apps, especially for Phonegap, this is the storage method to use.

            // X:\jsc.svn\javascript\Examples\GoogleGears\GGearAlpha\js\GoogleGearsAdvanced.cs

            //new { }.With(
            new IHTMLButton { "go" }.AttachToDocument().onclick +=
                 delegate
            {
                new IHTMLPre { "about to connnect..." }.AttachToDocument();


                var db = Native.window.openDatabase();

                // about to connnect... done {{ db = [object Database], version =  }}
                new IHTMLPre { "about to connnect... done " }.AttachToDocument();

                new IHTMLButton { "do transaction" }.AttachToDocument().onclick +=
                    delegate
                {
                    // https://code.google.com/p/chromium/issues/detail?id=324593


                    //Debugger.Break();
                    // jsc async using, finally not yet called?
                    db.transaction(
                    callback:
                        tx =>
                        {
                            new IHTMLPre { "enter transaction " }.AttachToDocument();

                            tx.executeSql(
                                sqlStatement: "CREATE TABLE IF NOT EXISTS Employee_Table (xid, Name, Location)"
                            );

                            tx.executeSql("insert into Employee_Table(xid, Name, Location) values(0, 'foo', 'bar')",
                                callback:
                                (SQLTransaction xtx, SQLResultSet r) =>
                                {
                                    // after insert {{ insertId = 1, rowsAffected = 1 }}
                                    // Uncaught InvalidAccessError: Failed to read the 'insertId' property from 'SQLResultSet': The query didn't result in any rows being added. 

                                    new IHTMLPre { "after insert " + new { r.insertId, r.rowsAffected } }.AttachToDocument();
                                }
                            );

                            new IHTMLPre { "before SELECT" }.AttachToDocument();
                            tx.executeSql("SELECT xid, Name, Location FROM Employee_Table",
                                 callback:
                                (SQLTransaction xtx, SQLResultSet r) =>
                                {
                                    new IHTMLPre { "enter SELECT " + new { r.rowsAffected, r.rows.length } }.AttachToDocument();


                                    for (uint i = 0; i < r.rows.length; i++)
                                    {
                                        var o = r.rows.item(i);


                                        var z = new { o.xid, Name = o["Name"], o.Location };

                                        new IHTMLPre { "row " + new { i, z } }.AttachToDocument();
                                    }
                                    new IHTMLPre { "exit SELECT " }.AttachToDocument();
                                }
                            );

                            new IHTMLPre { "exit transaction " }.AttachToDocument();

                        }
                );

                    new IHTMLPre { "after transaction " }.AttachToDocument();
                };



            };



            // http://www.w3.org/TR/webdatabase/


        }

    }

}
