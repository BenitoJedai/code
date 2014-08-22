using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.PHP.Data;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
//using System.Data.SQLite;
using System.Linq;
using System.Text;
using ScriptCoreLibJava.Extensions;
using System.Threading;
using System.Data.Common;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.SQLite;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteCommand))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.MySQL.MySQLCommand")]
    internal class __MySQLCommand : __DbCommand
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121014-gae-data
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs
        // X:\jsc.svn\core\ScriptCoreLib\PHP\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs

        __MySQLConnection c;

        #region CommandText
        public override string CommandText { get; set; }

        public string InternalCommandText
        {
            get
            {
                return SQLiteToMySQLConversion.Convert(
                    this.CommandText,
                    this.c.InternalConnectionString.DataSource
                );
            }
        }
        #endregion


        public override global::System.Data.Common.DbParameter CreateDbParameter()
        {
            return (global::System.Data.Common.DbParameter)(object)new __SQLiteParameter();
        }

        public java.sql.Statement InternalStatement;
        public java.sql.PreparedStatement InternalPreparedStatement;

        public __MySQLCommand(string sql, __MySQLConnection c)
        {
            this.c = (__MySQLConnection)(object)c;

            // http://dev.mysql.com/doc/refman/5.0/en/example-auto-increment.html
            // http://www.sqlite.org/autoinc.html


            this.CommandText = sql;


            //this.InternalParameters = new __SQLiteParameterCollection { };

            // 625ms AddParameter { Parameters =  }


            this.Parameters = new __SQLiteParameterCollection { };

            //((__DbCommand)this).Parameters = (DbParameterCollection)(object)this.Parameters;

        }

        private void InternalCreateStatement()
        {
            if (this.InternalStatement != null)
                return;

            var sql = this.InternalCommandText;

            //Console.WriteLine("InternalCreateStatement" + new { sql });
            try
            {
                // http://www.javaworld.com/javaworld/jw-04-2007/jw-04-jdbc.html
                if (this.Parameters.InternalParameters.Count > 0)
                {

                    //Console.WriteLine("we have InternalParameters for " + sql);

                    var parameters = this.Parameters.InternalParameters;

                    var index =
                       from p in parameters
                       from i in this.CommandText.GetIndecies(p.ParameterName)
                       orderby i
                       select new { p, i };


                    // did we break it?
                    foreach (var p in parameters.ToArray())
                    {
                        // java seems to like indexed parameters instead
                        sql = sql.Replace(p.ParameterName, "?");
                    }

                    this.InternalPreparedStatement = this.c.InternalConnection.prepareStatement(sql);

                    var c = 0;
                    foreach (var item in index)
                    {
                        c++;

                        if (item.p.Value == null)
                        {
                            this.InternalPreparedStatement.setObject(c, null);
                        }
                        else if (item.p.Value is int)
                            this.InternalPreparedStatement.setInt(c, (int)item.p.Value);
                        else if (item.p.Value is long)
                            this.InternalPreparedStatement.setLong(c, (long)item.p.Value);
                        else if (item.p.Value is double)
                            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs
                            this.InternalPreparedStatement.setDouble(c, (double)item.p.Value);
                        else if (item.p.Value is string)
                            this.InternalPreparedStatement.setString(c, (string)item.p.Value);
                        else if (item.p.Value is byte[])
                            this.InternalPreparedStatement.setBytes(c, (sbyte[])item.p.Value);
                        else
                        {
                            //Caused by: java.lang.RuntimeException: InternalCreateStatement, what to do with this? { CommandText = insert into Table1 (ContentValue, ContentBytes) values (@ContentValue /* text */, @ContentBytes), type = ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteParameter, isBytes = true, bytes = [B, eqBytes = true, item = { p = ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteParameter@1908330, i = 82 } }

                            // http://docs.oracle.com/javase/6/docs/api/java/sql/PreparedStatement.html#setBytes(int, byte[])

                            //Caused by: java.lang.RuntimeException: InternalCreateStatement, what to do with this? 
                            // { CommandText = insert into Table1 (ContentValue, ContentBytes) values (@ContentValue /* text */, @ContentBytes), 
                            // item = { p = ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteParameter@47f703, i = 82 }, 
                            // type = __AnonymousTypes__ScriptCoreLibJava.__f__AnonymousType1_2 }

                            //var isBytes = item.p.Value is byte[];
                            //var bytes = typeof(byte[]);
                            //var eqBytes = item.p.Value.GetType() == bytes;


                            // double?
                            //Caused by: java.lang.RuntimeException: InternalCreateStatement, what to do with this? { CommandText = insert into `Book1.Middle` (`Title`, `Ratio`, `FooStateEnum`, `GooStateEnum`, `x`, `Tag`, `Timestamp`)  values (@Title, @Ratio, @FooStateEnum, @GooStateEnum, @x, @Tag, @Timestamp), type = java.lang.Double, item = { p = ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteParameter@1dc5180, i = 120 } }
                            //        at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteCommand.InternalCreateStatement(__SQLiteCommand.java:225)
                            //        ... 89 more


                            var message = "InternalCreateStatement, what to do with this? " + new
                            {
                                this.CommandText,
                                type = item.p.Value.GetType(),

                                //isBytes,
                                //bytes,
                                //eqBytes,

                                item,
                            };

                            throw new InvalidOperationException(message);
                        }
                    }

                    // add values

                    this.InternalStatement = this.InternalPreparedStatement;
                }

                if (this.InternalStatement == null)
                    this.InternalStatement = this.c.InternalConnection.createStatement();
            }
            catch
            {
                throw;
            }

            try
            {
                // Caused by: java.lang.RuntimeException: GetFieldType fault: { ColumnType = 3, ordinal = 0, CommandText = select sum(`Deposit`)
                //        at ScriptCoreLib.Shared.Data.Diagnostics.QueryStrategyExtensions.Sum(QueryStrategyExtensions.java:124)
                //from `Book18.Sheet2`
                Console.WriteLine("\n__SQLiteCommand.InternalCreateStatement " + new
                {

                    //CurrentThreadID = Thread.CurrentThread.ManagedThreadId,
                    //CurrentThreadHashCode = Thread.CurrentThread.GetHashCode(),
                    sql,
                    //isReadOnly = this.c.InternalConnection.isReadOnly(),

                    //  If the timeout period expires before the operation completes, this method returns false. A value of 0 indicates a timeout is not applied to the database operation.
                    //isValid = this.c.InternalConnection.isValid(0)

                });
            }
            catch
            {

            }
        }

        public override int ExecuteNonQuery()
        {
            var value = default(int);

            InternalCreateStatement();

            try
            {
                if (this.InternalPreparedStatement != null)
                    value = this.InternalPreparedStatement.executeUpdate();
                else
                    value = this.InternalStatement.executeUpdate(this.InternalCommandText);

                this.c.InternalLastInsertRowIdCommand = this;
            }
            catch
            {
                throw;
            }

            return value;
        }


        //Y:\SVGNavigationTiming.XApplicationPerformanceWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\__SQLiteCommand.java:297: error: get_DbParameterCollection() in __SQLiteCommand cannot override get_DbParameterCollection() in __DbCommand
        //    protected  __DbParameterCollection get_DbParameterCollection()
        //                                       ^
        //  attempting to assign weaker access privileges; was public

        public new __SQLiteParameterCollection Parameters { get; set; }

        public override DbParameterCollection DbParameterCollection
        {
            get { return this.Parameters; }
        }

        public override global::System.Data.Common.DbDataReader __DbCommand_ExecuteReader()
        {
            // tested by
            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\Schema\FooTable.cs

            return (global::System.Data.Common.DbDataReader)(object)this.ExecuteReader();
        }

        public override object ExecuteScalar()
        {
            var r = ExecuteReader();
            var value = default(object);
            if (r.Read())
            {
                value = r[0];
            }
            r.Dispose();
            return value;

        }

        public new __MySQLDataReader ExecuteReader()
        {
            var value = default(__MySQLDataReader);

            InternalCreateStatement();

            try
            {
                var r = default(java.sql.ResultSet);

                if (this.InternalPreparedStatement != null)
                    r = this.InternalPreparedStatement.executeQuery();
                else
                    r = this.InternalStatement.executeQuery(this.CommandText);

                value = (__MySQLDataReader)(object)new __MySQLDataReader { InternalResultSet = r, InternalCommand = this };
                this.c.InternalLastInsertRowIdCommand = this;
            }
            catch (Exception ex)
            {
                // The ResultSet can not be null for Statement.executeQuery.
                //                Caused by: java.sql.SQLException: The ResultSet can not be null for Statement.executeQuery.
                //at com.google.cloud.sql.jdbc.internal.Exceptions.newStatementExecuteQueryNullResultSetException(Exceptions.java:74)
                //at com.google.cloud.sql.jdbc.Statement.executeQuery(Statement.java:328)
                //at com.google.cloud.sql.jdbc.Statement.executeQuery(Statement.java:41)

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121101/20121114-mysql
                // http://www.javaworld.com.tw/jute/post/view?bid=9&id=305281
                // crude workaround
                if (ex.StackTrace.Contains("com.google.cloud.sql.jdbc.internal.Exceptions.newStatementExecuteQueryNullResultSetException"))
                {
                    // no resultset.
                    value = (__MySQLDataReader)(object)new __MySQLDataReader { InternalResultSet = null };
                }


                if (value == null)
                {
                    // implicit rethrow wont work. jsc forgot the name of the variable. 
                    //throw;
                    //                    [javac] V:\java\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\__SQLiteCommand.java:87: unreported exception java.lang.Throwable; must be caught or declared to be thrown
                    //[javac]                 throw exception3;
                    //[javac]                 ^
                    throw new InvalidOperationException(new { ex.Message, ex.StackTrace }.ToString());
                }
            }

            return value;
        }


    }
}
