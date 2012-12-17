using ScriptCoreLib.PHP.Data;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteCommand))]
    internal class __SQLiteCommand : __DbCommand
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs

        __SQLiteConnection c;

        string sql;



        public __SQLiteCommand(string sql, SQLiteConnection c)
        {
            this.c = (__SQLiteConnection)(object)c;


            this.sql = SQLiteToMySQLConversion.Convert(
                sql,
                __SQLiteConnectionStringBuilder.InternalConnectionString.DataSource
            );


            this.InternalParameters = new __SQLiteParameterCollection { };
            this.Parameters = (SQLiteParameterCollection)(object)this.InternalParameters;
        }

        public mysqli_stmt InternalPreparedStatement;


        private void InternalCreateStatement()
        {
            if (this.InternalParameters.InternalParameters.Count > 0)
            {
                var sql = this.sql;

                Console.WriteLine("we have InternalParameters for " + sql);

                var parameters = this.InternalParameters.InternalParameters;

                Console.WriteLine("we have InternalParameters for " + new { parameters.Count });

                //var pi = parameters.Select(
                //    p =>
                //    {
                //        var i = this.sql.GetIndecies(p.ParameterName);

                //        return new { p, i };
                //    }
                //);

                //Console.WriteLine("we have InternalParameters for before order by");

                //var index = pi.OrderBy(k => k.i).ToArray();

                // broken:
                var index = Enumerable.ToArray(
                   from p in parameters.AsEnumerable()
                   from i in this.sql.GetIndecies(p.ParameterName)
                   orderby i
                   select new { p, i }
                );

                Console.WriteLine("we have InternalParameters for index: " + new { index.Length });


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

                this.InternalPreparedStatement.bind_param_array(types, values.ToArray());

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

                r = this.InternalPreparedStatement.get_result() as mysqli_result;
            }
            else
            {
                r = this.c.InternalConnection.query(this.sql) as mysqli_result;
            }

            if (this.c.InternalConnection.errno != 0)
            {
                var message = new { this.c.InternalConnection.errno, this.c.InternalConnection.error };

                throw new Exception(message.ToString());
            }

            return 0;
        }

        public __SQLiteParameterCollection InternalParameters;
        public SQLiteParameterCollection Parameters { get; set; }

        public __SQLiteDataReader ExecuteReader()
        {
            InternalCreateStatement();

            var r = default(mysqli_result);

            if (this.InternalPreparedStatement != null)
            {
                this.InternalPreparedStatement.execute();

                r = this.InternalPreparedStatement.get_result() as mysqli_result;
            }
            else
            {
                r = this.c.InternalConnection.query(this.sql) as mysqli_result;
            }

            if (this.c.InternalConnection.errno != 0)
            {
                var message = new { this.c.InternalConnection.errno, this.c.InternalConnection.error };

                throw new Exception(message.ToString());
            }

            return new __SQLiteDataReader
            {
                InternalResultSet = r
            };
        }
    }

}
