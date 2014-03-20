using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEngineUserAgentLogger.Schema
{
    class FirstTable : FirstTableQueries
    {
        //public const string DefaultDataSource = "SQLiteWithDataGridView52.sqlite";
        public const string DefaultDataSource = "file:SQLiteWithDataGridView53.sqlite";

        public readonly Action<Action<SQLiteConnection>> WithConnection;

        public FirstTable(string DataSource = DefaultDataSource)
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

            // Error	4	The type 'System.Data.SQLite.SQLiteConnectionStringBuilder' is defined in an assembly that is not referenced. You must add a reference to assembly 'System.Data.SQLite, Version=1.0.89.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139'.	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLogger\AppEngineUserAgentLogger\Schema\FirstTable.cs	35	13	AppEngineUserAgentLogger
            //Error	5	The type 'System.Data.SQLite.SQLiteConnection' is defined in an assembly that is not referenced. You must add a reference to assembly 'System.Data.SQLite, Version=1.0.89.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139'.	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLogger\AppEngineUserAgentLogger\Schema\FirstTable.cs	35	13	AppEngineUserAgentLogger

            //this.WithConnection = DataSource.AsWithConnection();
            this.WithConnection = DataSource.xAsWithConnection();

            WithConnection(
                c =>
                {

                    //Additional information: { Message = Method not found: '?'., StackTrace =   
                    // at AppEngineUserAgentLogger.Schema.FirstTableExtensions.ExecuteNonQuery(CreateMeta , IDbConnection )

                    new CreateMeta { }.ExecuteNonQuery(c);
                }
             );
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

        public Task<InsertMeta> Insert(InsertMeta value)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);
                }
            );

            return value.ToTaskResult();
        }

    }

    public static partial class XX
    {
        // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\Schema\TheGridTable.cs

        //public static int ExecuteNonQuery(this AppEngineUserAgentLogger.Schema.FirstTableQueries.CreateMeta data, IDbConnection c)
        //{
        //    return c
        //        .CreateCommand(AppEngineUserAgentLogger.Schema.FirstTableQueries.CreateMeta.CommandText)
        //        .ExecuteNonQuery();
        //}

        //public static IDataReader ExecuteReader(this AppEngineUserAgentLogger.Schema.FirstTableQueries.SelectAll data, IDbConnection c)
        //{
        //    return c
        //        .CreateCommand(AppEngineUserAgentLogger.Schema.FirstTableQueries.SelectAll.CommandText)
        //        .ExecuteReader();
        //}

        //public static int ExecuteNonQuery(this AppEngineUserAgentLogger.Schema.FirstTableQueries.InsertMeta data, IDbConnection c)
        //{
        //    return c
        //        .CreateCommand(AppEngineUserAgentLogger.Schema.FirstTableQueries.InsertMeta.CommandText)
        //        .AddParameter("@width", data.width)
        //        .AddParameter("@height", data.height)
        //        .AddParameter("@ip", data.ip)
        //        .AddParameter("@useragent", data.useragent)
        //        .ExecuteNonQuery();
        //}


        //Error	9	The type or namespace name 'DynamicDataReader' could not be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLogger\AppEngineUserAgentLogger\Schema\FirstTable.cs	108	27	AppEngineUserAgentLogger
        //Error	10	The type or namespace name 'DynamicDataReader' could not be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLogger\AppEngineUserAgentLogger\Schema\FirstTable.cs	119	27	AppEngineUserAgentLogger


        // jsc cannot handle generic that only differ in generic arguments?
        public static void WithEachReader(this SQLiteDataReader reader, Action<IDataReader> y)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    y(new ScriptCoreLib.Shared.Data.DynamicDataReader(reader));
                }
            }
        }

        public static void WithEach(this SQLiteDataReader reader, Action<dynamic> y)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    y(new ScriptCoreLib.Shared.Data.DynamicDataReader(reader));
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
                    c.Open();

                    try
                    {
                        y(c);
                    }
                    catch (Exception ex)
                    {
                        var message = new { ex.Message, ex.StackTrace }.ToString();

                        //Console.WriteLine("AsWithConnection... error: " + message);

                        throw new InvalidOperationException(message);
                    }
                }
            };
        }

        public static SQLiteConnection ToConnection(this string DataSource, int Version = 3)
        {
            //Error	1	The type 'System.Data.SQLite.SQLiteConnectionStringBuilder' is defined in an assembly that is not referenced. You must add a reference to assembly 'System.Data.SQLite, Version=1.0.89.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139'.	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLogger\AppEngineUserAgentLogger\Schema\FirstTable.cs	35	13	AppEngineUserAgentLogger
            //Error	2	The type 'System.Data.SQLite.SQLiteConnection' is defined in an assembly that is not referenced. You must add a reference to assembly 'System.Data.SQLite, Version=1.0.89.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139'.	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLogger\AppEngineUserAgentLogger\Schema\FirstTable.cs	35	13	AppEngineUserAgentLogger


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
