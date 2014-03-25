using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropNuGetToServer.Schema
{
    public class Table1 : Table1Queries
    {
        // System.InvalidOperationException: Invalid connection string: invalid URI
        // http://stackoverflow.com/questions/10329356/data-truncation-error-data-too-long-for-column
        // http://stackoverflow.com/questions/3503841/jpa-mysql-blob-returns-data-too-long/3507664#3507664
        //BLOB: maximum length of 65,535 bytes
        //MEDIUMBLOB: maximum length of 16,777,215 bytes
        //LONGBLOB: maximum length of 4,294,967,295 bytes

        public const string DefaultDataSource = "file:SQLiteWithDataGridView500.sqlite";
        //public const string DefaultDataSource = @"G:\SQLiteWithDataGridView54.sqlite";

        public readonly Action<Action<SQLiteConnection>> WithConnection;



        public Table1(string DataSource = DefaultDataSource)
        {

            #region abort if in design mode
            if (new StackTrace().ToString().Contains("System.ComponentModel.Design.DesignerHost.System.ComponentModel.Design.IDesignerHost"))
            {
                // Y:\DeltaExperiment.ApplicationWebService\staging.java\web\java\DeltaExperiment\Delta.java:35: variable WithConnection might already have been assigned

                // make javac happy
                this.WithConnection = null;

                return;
            }
            #endregion

            this.WithConnection = DataSource.xAsWithConnection();

            WithConnection(
                c =>
                {
                    new Create { }.ExecuteNonQuery(c);
                    new Table1MetaQueries.CreateMeta { }.ExecuteNonQuery(c);
                }
             );
        }

        // reference to child table?


        public void Delete(Delete value)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);
                }
            );
        }

        public void Insert(Insert value, Action<long> yield)
        {
            //enter upload
            //enter upload
            //{ ContentType = image/jpeg, FileName = emspectrum.jpg, ContentLength = 82548, Length = 82548 }
            //{ ContentType = image/jpeg, FileName = emspectrum.jpg, ContentLength = 82548, Length = 82548 }
            //before insert { ManagedThreadId = 34 }
            //before insert { ManagedThreadId = 13 }
            //AsWithConnection... error: { ex = System.Data.SQLite.SQLiteBusyException (0x80004005)
            //   at System.Data.SQLite.SQLiteCommand.ExecuteStatement(Vdbe pStmt, Int32& cols, IntPtr& pazValue, IntPtr& pazColName)
            //   at System.Data.SQLite.SQLiteCommand.ExecuteStatement(Vdbe pStmt)
            //   at System.Data.SQLite.SQLiteCommand.ExecuteReader(CommandBehavior behavior, Boolean want_results, Int32& rows_affected)
            //   at System.Data.SQLite.SQLiteCommand.ExecuteNonQuery()
            //   at DropFileIntoSQLite.Schema.Table1Extensions.ExecuteNonQuery(Insert , IDbConnection )

            WithConnection(
             c =>
             {
                 value.ExecuteNonQuery(c);

                 yield(c.LastInsertRowId);
             }
           );
        }

        public Task<Table1MetaQueries.InsertMeta> InsertMeta(Table1MetaQueries.InsertMeta value)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);
                }
            );

            return value.ToTaskResult();
        }


        public void SelectAll(Action<dynamic> yield)
        {
            WithConnection(
              c =>
              {
                  new SelectAll().ExecuteReader(c).WithEach(yield);
              }
            );
        }

        public void SelectBytes(SelectBytes value, Action<IDataReader> yield)
        {
            WithConnection(
               c =>
               {
                   //Error	7	'System.Data.IDataReader' does not contain a definition for 'WithEachReader' and the best extension method overload 'DropFileIntoSQLite.Schema.XX.WithEachReader(System.Data.SQLite.SQLiteDataReader, System.Action<System.Data.IDataReader>)' has some invalid arguments	X:\jsc.svn\examples\javascript\io\DropFileIntoSQLite\DropFileIntoSQLite\Schema\Table1.cs	99	20	DropFileIntoSQLite


                   value.ExecuteReader(c).WithEachReader(yield);
               }
             );
        }


        public void SelectBytesByValue(SelectBytesByValue value, Action<IDataReader> yield)
        {
            WithConnection(
               c =>
               {
                   //Error	7	'System.Data.IDataReader' does not contain a definition for 'WithEachReader' and the best extension method overload 'DropFileIntoSQLite.Schema.XX.WithEachReader(System.Data.SQLite.SQLiteDataReader, System.Action<System.Data.IDataReader>)' has some invalid arguments	X:\jsc.svn\examples\javascript\io\DropFileIntoSQLite\DropFileIntoSQLite\Schema\Table1.cs	99	20	DropFileIntoSQLite


                   value.ExecuteReader(c).WithEachReader(yield);
               }
             );
        }

    }




    public static partial class XX
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140320

        //public static int ExecuteNonQuery(this DropFileIntoSQLite.Schema.Table1Queries.Create data, IDbConnection c)
        //{
        //    // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\Schema\TheGridTable.cs

        //    return c
        //        .CreateCommand(DropFileIntoSQLite.Schema.Table1Queries.Create.CommandText)
        //        .ExecuteNonQuery();
        //}

        //public static int ExecuteNonQuery(this DropFileIntoSQLite.Schema.Table1MetaQueries.CreateMeta data, IDbConnection c)
        //{
        //    // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\Schema\TheGridTable.cs

        //    return c
        //        .CreateCommand(DropFileIntoSQLite.Schema.Table1MetaQueries.CreateMeta.CommandText)
        //        .ExecuteNonQuery();
        //}

        //public static int ExecuteNonQuery(this DropFileIntoSQLite.Schema.Table1Queries.Delete data, IDbConnection c)
        //{
        //    // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\Schema\TheGridTable.cs

        //    return c
        //        .CreateCommand(DropFileIntoSQLite.Schema.Table1Queries.Delete.CommandText)
        //        .AddParameter("@ContentKey", data.ContentKey)
        //        .ExecuteNonQuery();
        //}

        //public static int ExecuteNonQuery(this DropFileIntoSQLite.Schema.Table1Queries.Insert data, IDbConnection c)
        //{
        //    // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\Schema\TheGridTable.cs

        //    return c
        //        .CreateCommand(DropFileIntoSQLite.Schema.Table1Queries.Insert.CommandText)
        //        .AddParameter("@ContentValue", data.ContentValue)
        //        .AddParameter("@ContentBytes", data.ContentBytes)
        //        .ExecuteNonQuery();
        //}

        //public static int ExecuteNonQuery(this DropFileIntoSQLite.Schema.Table1MetaQueries.InsertMeta data, IDbConnection c)
        //{
        //    // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\Schema\TheGridTable.cs

        //    return c
        //        .CreateCommand(DropFileIntoSQLite.Schema.Table1MetaQueries.InsertMeta.CommandText)
        //        .AddParameter("@MemberName", data.MemberName)
        //        .AddParameter("@MemberValue", data.MemberValue)
        //        .AddParameter("@DeclaringType", data.DeclaringType)
        //        .ExecuteNonQuery();
        //}



        //public static IDataReader ExecuteReader(this DropFileIntoSQLite.Schema.Table1Queries.SelectAll data, IDbConnection c)
        //{
        //    return c
        //        .CreateCommand(DropFileIntoSQLite.Schema.Table1Queries.SelectAll.CommandText)
        //       .ExecuteReader();
        //}

        //public static IDataReader ExecuteReader(this DropFileIntoSQLite.Schema.Table1Queries.SelectBytes data, IDbConnection c)
        //{
        //    return c
        //        .CreateCommand(DropFileIntoSQLite.Schema.Table1Queries.SelectBytes.CommandText)
        //        .AddParameter("@ContentKey", data.ContentKey)
        //       .ExecuteReader();
        //}


        // jsc cannot handle generic that only differ in generic arguments?
        public static void WithEachReader(this IDataReader reader, Action<IDataReader> y)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    y(new DynamicDataReader(reader));
                }
            }
        }

        public static void WithEach(this IDataReader reader, Action<dynamic> y)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    y(new DynamicDataReader(reader));
                }
            }
        }




        public static Action<Action<SQLiteConnection>> xAsWithConnection(this string DataSource, int Version = 3)
        {
            //Console.WriteLine("AsWithConnection...");

            return y =>
            {
                //Console.WriteLine("AsWithConnection... invoke");

                using (var c = DataSource.ToConnection(Version))
                {
                    c.BusyTimeout = 5000;
                    c.Open();

                    try
                    {
                        y(c);
                    }
                    catch (Exception ex)
                    {
                        var message = new { ex.Message, ex.StackTrace }.ToString();

                        Console.WriteLine("AsWithConnection... error: " + new { ex });

                        throw new InvalidOperationException(message);
                    }
                }
            };
        }

        public static SQLiteConnection ToConnection(this string DataSource, int Version = 3)
        {
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = Version
            };

            var c = new SQLiteConnection(csb.ConnectionString);

            return c;
        }
    }
}
