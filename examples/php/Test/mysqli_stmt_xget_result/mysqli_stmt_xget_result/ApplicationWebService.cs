using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.PHP;
using ScriptCoreLib.PHP.Data;
using System;
using System.Linq;
using System.Xml.Linq;

namespace mysqli_stmt_xget_result
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> yield)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201301/20130101


            var m = new mysqli(
               "localhost",
               "root",
               ""
           );

            #region query
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


            {
                #region create table.sql
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
                #endregion

                query(sql);
            }

            {
                var sql = @"
insert into History1 (query, context) values (
? /* text */,
? /* context */
)
";

                (m.prepare(sql) as mysqli_stmt).With(
                    stmt =>
                    {
                        yield("in prepare " + new { sql });

                        stmt.bind_param_array("ss",
                              "a text",
                              "a context"
                          );

                        stmt.execute();
                    }
                );
            }

            {
                var sql = @"
select id, query from History1 where context = ?
";
                yield("before select");

                var stmt = (m.prepare(sql) as mysqli_stmt);

                {
                    var message = new { m.errno, m.error, sql };
                    yield(message.ToString());
                }

                if (stmt != null)
                {
                    yield("in prepare " + new { sql });

                    stmt.bind_param_array("s",
                          "a context"
                      );

                    stmt.execute();

                    stmt.store_result();

                    yield("store_result " + new { stmt.num_rows, stmt.field_count });

                   
                    for (int i = 0; i < stmt.num_rows; i++)
                    {
                        var a = stmt.__fetch_array();
                        var message = new { i, a = Native.DumpToString(a) };

                        yield(message.ToString());

                    }
                    //reader.dispose
                    stmt.free_result();


                }
            }


            yield("all done");
        }


    }

    static class X
    {
        // http://php.net/manual/en/mysqli-stmt.fetch.php
        [Script(OptimizedCode = @"

        $data = mysqli_stmt_result_metadata($stmt);
        $fields = array();
        $out = array();

        $fields[0] = &$stmt;
        $count = 1;

        while($field = mysqli_fetch_field($data)) {
            $fields[$count] = &$out[$field->name];
            $count++;
        }
        
        call_user_func_array('mysqli_stmt_bind_result', $fields);
        mysqli_stmt_fetch($stmt);
        return $out;

")]
        public static ScriptCoreLib.PHP.Runtime.IArray __fetch_array(this mysqli_stmt stmt)
        {

            return null;
        }


    }
}
