using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using System;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.DOM
{



    partial class IWindow
    {
        // http://www.w3.org/TR/webdatabase/

      
        // see: http://creativepark.net/blog/entry/id/1191
        // X:\jsc.svn\examples\javascript\test\TestWebSQLDatabase\TestWebSQLDatabase\Application.cs
        //   Database openDatabase(in DOMString name, in DOMString version, in DOMString displayName, in unsigned long estimatedSize, in optional DatabaseCallback creationCallback);

        // web workers can do it sync
        [Script(DefineAsStatic = true)]
        public Task<Database> openDatabase(
            string name = "database.sqlite",
            string version = "1.0",
            string displayName = "Web SQL",

            // AppCache allows 5MB, how much for db?
            ulong estimatedSize = 2 * 1024 * 1024
            )
        {
            // tested by
            // x:\jsc.svn\examples\javascript\test\testwebsqldatabase\testwebsqldatabase\application.cs

            var t = new TaskCompletionSource<Database>();

            dynamic window = Native.window;
            IFunction openDatabase = window.openDatabase;

            IFunction creationCallback = new Action<Database>(
               (Database db) =>
               {
                   t.SetResult(db);
               }
           );

            // http://www.w3.org/TR/webdatabase/

            openDatabase.apply(Native.window,
                name, version, displayName, estimatedSize, creationCallback
            );

            return t.Task;
        }
    }


    #region SQLResultSet

    // http://www.w3.org/TR/webdatabase/#sqlresultset

    //    interface SQLResultSet {
    //  readonly attribute long insertId;
    //  readonly attribute long rowsAffected;
    //  readonly attribute SQLResultSetRowList rows;
    //};

    [Script(InternalConstructor = true)]
    public class SQLResultSet
    {

    }
    #endregion


    // http://www.w3.org/TR/webdatabase/#sqlerror
    [Script(InternalConstructor = true)]
    public class SQLError
    {
        public readonly short code;
        public string message;
    }



    #region SQLStatementCallback
    [Script]
    public delegate void SQLStatementCallback(SQLTransaction transaction, SQLResultSet resultSet);


    //[Callback=FunctionOnly, NoInterfaceObject]
    //interface SQLStatementCallback {
    //  void handleEvent(in SQLTransaction transaction, in SQLResultSet resultSet);
    //};
    #endregion


    #region SQLStatementErrorCallback
    [Script]
    public delegate void SQLStatementErrorCallback(SQLTransaction transaction, SQLError error);

    //[Callback=FunctionOnly, NoInterfaceObject]
    //interface SQLStatementErrorCallback {
    //  boolean handleEvent(in SQLTransaction transaction, in SQLError error);
    //};
    #endregion


    #region SQLTransactionCallback
    [Script]
    public delegate void SQLTransactionCallback(SQLTransaction transaction);

    //    [Callback=FunctionOnly, NoInterfaceObject]
    //interface SQLTransactionCallback {
    //  void handleEvent(in SQLTransaction transaction);
    //};
    #endregion


    #region SQLTransactionErrorCallback
    [Script]
    public delegate void SQLTransactionErrorCallback(SQLError error);

    //    [Callback=FunctionOnly, NoInterfaceObject]
    //interface SQLTransactionErrorCallback {
    //  void handleEvent(in SQLError error);
    //};
    #endregion

    [Script(InternalConstructor = true)]
    public class SQLTransaction
    {
        //void executeSql(in DOMString sqlStatement, in optional ObjectArray arguments, in optional SQLStatementCallback callback, in optional SQLStatementErrorCallback errorCallback);
        public void executeSql(string sqlStatement, object[] arguments = null, SQLStatementCallback callback = null, SQLStatementErrorCallback errorCallback = null)
        {

        }

    };

    [Script(InternalConstructor = true)]
    public class Database
    {
        // "Failed to execute 'transaction' on 'Database': The callback provided as parameter 1 is not a function."
        // is jsc generating delegates from IDL yet?
        // if we talk to DOM are we sending a function innstead of delegate?

        //public void transaction(in SQLTransactionCallback callback, in optional SQLTransactionErrorCallback errorCallback, in optional SQLVoidCallback successCallback);
        public void transaction(SQLTransactionCallback callback, SQLTransactionErrorCallback errorCallback = null, Action successCallback = null)
        {

        }

        //  readonly attribute DOMString version;
        //public readonly string version;
    }
}
