using ScriptCoreLib.PHP.Data;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
//using System.Data.SQLite;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.SQLite;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteCommand))]
    internal class __SQLiteCommand : __DbCommand
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs

        __SQLiteConnection c;


        public override string CommandText { get; set; }

        public __SQLiteCommand(string sql, __SQLiteConnection c)
        {
            this.c = (__SQLiteConnection)(object)c;


            this.CommandText = SQLiteToMySQLConversion.Convert(
                sql,
                __SQLiteConnectionStringBuilder.InternalConnectionString.DataSource
            );


            this.InternalParameters = new __SQLiteParameterCollection { };
            this.Parameters = (__SQLiteParameterCollection)(object)this.InternalParameters;
        }

        public mysqli_stmt InternalPreparedStatement;


        private void InternalCreateStatement()
        {
            if (this.InternalParameters.InternalParameters.Count > 0)
            {
                var sql = this.CommandText;


                var parameters = this.InternalParameters.InternalParameters;

                //Console.WriteLine("we have InternalParameters for " + new { parameters.Count });


                var index = Enumerable.ToArray(
                   from p in parameters.AsEnumerable()
                   from i in this.CommandText.GetIndecies(p.ParameterName)
                   orderby i
                   select new { p, i }
                );

                //Console.WriteLine("we have InternalParameters for index: " + new { index.Length });


                foreach (var p in parameters)
                {
                    // java seems to like indexed parameters instead
                    sql = sql.Replace(p.ParameterName, "?");
                }

                this.InternalPreparedStatement = this.c.InternalConnection.prepare(sql) as mysqli_stmt;

                var types = "";
                var values = new List<object>();

                var c = 0;
                foreach (var item in index)
                {
                    c++;

                    // what about blobs?

                    if (item.p.Value is int)
                    {
                        types += "i";
                        values.Add(item.p.Value);
                    }
                    else if (item.p.Value is double)
                    {
                        types += "d";
                        values.Add(item.p.Value);
                    }
                    else
                    {
                        types += "s";
                        values.Add(item.p.Value);
                    }


                }

                var args = values.ToArray();

                //Console.WriteLine("we have InternalParameters for " + new { sql, types, args = Native.DumpToString(args) });


                this.InternalPreparedStatement.bind_param_array(types, args);

                // add values
            }
        }

        public override int ExecuteNonQuery()
        {
            InternalCreateStatement();

            var r = default(mysqli_result);

            if (this.InternalPreparedStatement != null)
            {
                this.InternalPreparedStatement.execute();

                // do we need this?
                //r = this.InternalPreparedStatement.get_result() as mysqli_result;
            }
            else
            {
                r = this.c.InternalConnection.query(this.CommandText) as mysqli_result;
            }

            if (this.c.InternalConnection.errno != 0)
            {
                var message = new { this.c.InternalConnection.errno, this.c.InternalConnection.error };

                throw new Exception(message.ToString());
            }

            return 0;
        }

        public __SQLiteParameterCollection InternalParameters;
        public __SQLiteParameterCollection Parameters { get; set; }

        public override global::System.Data.Common.DbParameterCollection DbParameterCollection
        {
            get { return (global::System.Data.Common.DbParameterCollection)(object)Parameters; }
        }

        public __SQLiteDataReader ExecuteReader()
        {
            InternalCreateStatement();

            var r = default(mysqli_result);
            var s = default(mysqli_stmt);

            if (this.InternalPreparedStatement != null)
            {
                this.InternalPreparedStatement.execute();



                // http://stackoverflow.com/questions/13659856/fatal-error-call-to-undefined-method-mysqli-stmtget-result
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201301/20130101

                //r = this.InternalPreparedStatement.get_result() as mysqli_result;
                s = this.InternalPreparedStatement;

                if (s.errno != 0)
                {
                    var message = new { s.errno, s.error };

                    throw new Exception(message.ToString());
                }

                s.store_result();

                //Console.WriteLine("ExecuteReader: " + new { this.InternalPreparedStatement.num_rows, this.InternalPreparedStatement.field_count });
            }
            else
            {
                r = this.c.InternalConnection.query(this.CommandText) as mysqli_result;

                if (this.c.InternalConnection.errno != 0)
                {
                    var message = new { this.c.InternalConnection.errno, this.c.InternalConnection.error };

                    throw new Exception(message.ToString());
                }
            }



            return new __SQLiteDataReader
            {
                InternalResultSet = r,
                InternalStatement = s
            };
        }
    }

}
