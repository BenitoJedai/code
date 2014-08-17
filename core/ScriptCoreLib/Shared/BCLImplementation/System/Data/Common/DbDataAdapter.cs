using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    // http://referencesource.microsoft.com/#System.Data/data/System/Data/Common/DataAdapter.cs
    // http://referencesource.microsoft.com/#System.Data/data/System/Data/Common/DbDataAdapter.cs
    // https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/System.Data/System.Data.Common/DbDataAdapter.cs

    [Script(Implements = typeof(global::System.Data.Common.DbDataAdapter))]
    public
        //abstract 
        class __DbDataAdapter
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs


        #region SelectCommand
        public DbCommand InternalSelectCommand;
        public DbCommand SelectCommand
        {
            get
            {
                return InternalSelectCommand;
            }
            set
            {
                InternalSelectCommand = value;
            }
        }
        #endregion



        public int Fill(DataTable dataTable)
        {
            // X:\jsc.svn\examples\javascript\LINQ\MashableVelocityGraph\MashableVelocityGraph\ApplicationWebService.cs

            //  The number of rows successfully added 

            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs
            // x:\jsc.svn\examples\javascript\appengine\webnotificationsviadataadapter\webnotificationsviadataadapter\applicationwebservice.cs
            // x:\jsc.svn\examples\javascript\dropfileintosqlite\dropfileintosqlite\schema\table1.cs
            // X:\jsc.svn\examples\javascript\forms\SQLiteConsoleExperiment\SQLiteConsoleExperiment\ApplicationWebService.cs

            //Caused by: java.lang.NullPointerException
            //        at ScriptCoreLib.Shared.BCLImplementation.System.Data.Common.__DbDataAdapter.Fill(__DbDataAdapter.java:53)

            var data = default(DataTable);

            //Console.WriteLine("Fill enter " + new { this.InternalSelectCommand });

            var reader = this.InternalSelectCommand.ExecuteReader();
            //Console.WriteLine("Fill reader " + new { reader });



            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131101
            if (reader != null)
                using (reader)
                {

                    while (reader.Read())
                    {
                        //Console.WriteLine("Fill reader Read");
                        if (data == null)
                        {
                            data = FillColumns(reader);
                        }


                        //Console.WriteLine("Fill NewRow ");
                        var row = data.NewRow();
                        data.Rows.Add(row);

                        //Console.WriteLine("Fill ");

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var n = reader.GetName(i);
                            var ft = reader.GetFieldType(i);

                            // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteDataReader.cs
                            var value = reader[n];


                            //                    Caused by: java.lang.NullPointerException
                            //at ScriptCoreLibJava.BCLImplementation.System.__Type.GetTypeFromValue(__Type.java:505)
                            //at ScriptCoreLibJava.BCLImplementation.System.__Object.System_Object_GetType_06000007(__Object.java:19)
                            //at ScriptCoreLib.Shared.BCLImplementation.System.Data.Common.__DbDataAdapter.Fill(__DbDataAdapter.java:71)

                            var valueType = default(string);

                            if (value != null)
                                valueType = value.GetType().FullName;

                            // tested by
                            // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs

                            //Console.WriteLine("Fill " + new { n, ft, value, valueType });

                            row[n] = value;
                        }
                    }
                }


            if (data == null)
                return 0;


            // Fill { n = Key, ft = java.lang.Integer, value = 48 }
            //Fill Merge
            //Book1Sheet2Key:
            //{ KeyObject = 48, FullName = java.lang.String }

            //Console.WriteLine("Fill Merge ");
            dataTable.Merge(data);

            return data.Rows.Count;
        }

        private static DataTable FillColumns(DbDataReader reader)
        {
            var xdata = new DataTable();

            Console.WriteLine("FillColumns " + new { reader.FieldCount });

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var columName = reader.GetName(i);

                Console.WriteLine("FillColumns " + new { i, columName });

                xdata.Columns.Add(columName);
            }
            return xdata;
        }
    }
}
