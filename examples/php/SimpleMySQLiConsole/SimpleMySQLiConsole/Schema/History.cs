using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.PHP;

namespace SimpleMySQLiConsole.Schema
{
    class History
    {
        public History()
        {
            Action<string> yield = x => Console.WriteLine("History.ctor " + x);

            var sql = @"
create table 
	if not exists 
		History1(

			/* http://stackoverflow.com/questions/7337882/sqlite-and-integer-types-int-integer-bigint */
			id INTEGER PRIMARY KEY AUTOINCREMENT, 
			-- sqlite vs mysql
			-- http://www.sqlite.org/datatype3.html
			query text not null,
			context text not null

		)
";
            sql = ScriptCoreLib.PHP.Data.SQLiteToMySQLConversion.Convert(sql);

            #region query
            var m = new mysqli(
                "localhost",
                "root",
                ""
            );

            Action<string> query =
                _sql =>
                {

                    (m.query(_sql) as mysqli_result).With(
                        r =>
                        {
                            r.close();
                        }
                    );

                    {
                        var message = new { m.errno, m.error, _sql };
                        yield(message.ToString());
                    }

                };
            #endregion

            query("CREATE DATABASE IF NOT EXISTS `datasource1001`");
            query("use `datasource1001`");
            query(sql);




            m.close();
        }

        public void Insert(HistoryQueries.Insert value)
        {
            Action<string> yield = x => Console.WriteLine("History.Insert " + x);

            var sql = @"
insert into History1 (query, context) values (
? /* text */,
? /* context */
)
";
            var m = new mysqli(
                "localhost",
                "root",
                ""
            );

            m.query("use `datasource1001`");

            yield("before prepare");

            (m.prepare(sql) as mysqli_stmt).With(
                stmt =>
                {
                    yield("in prepare");

                    {
                        var message = new { stmt.errno, stmt.error };

                        yield(message.ToString());
                    }

                    // <b>Strict Standards</b>:  Only variables should be passed by reference

                    // errno = 1136, error = Column count doesn't match value count at row 1
                    //var arg1 = value.query;
                    //stmt.bind_param("s", arg1);

                    //var arg2 = value.context;
                    //stmt.bind_param("s", arg2);

                    yield("bind_param_array");

                    stmt.bind_param_array("ss",
                        value.query,
                        value.context
                    );

                    stmt.execute();

                    {
                        var message = new { stmt = new { stmt.errno, stmt.error, stmt.insert_id }, m.insert_id, sql };
                        yield(message.ToString());
                    }

                    stmt.close();
                }
            );

            yield("after prepare");

            {
                var message = new { m.errno, m.error, sql };
                yield(message.ToString());
            }

        }
    }


}
