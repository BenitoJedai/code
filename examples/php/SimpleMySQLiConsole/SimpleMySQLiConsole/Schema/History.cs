using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace SimpleMySQLiConsole.Schema
{
    class History
    {
        public History()
        {
            var sql = @"
create table 
	if not exists 
		History(

			/* http://stackoverflow.com/questions/7337882/sqlite-and-integer-types-int-integer-bigint */
			id INTEGER PRIMARY KEY AUTOINCREMENT, 
			-- sqlite vs mysql
			-- http://www.sqlite.org/datatype3.html
			query text not null,

		)
";
            sql = ScriptCoreLib.PHP.Data.SQLiteToMySQLConversion.Convert(sql);

            var m = new mysqli(
                "localhost",
                "root",
                ""
            );

            (m.query("CREATE DATABASE IF NOT EXISTS `datasource1001`") as mysqli_result).With(
                r =>
                {
                    r.close();
                }
            );

            (m.query("use `datasource1001`") as mysqli_result).With(
                r =>
                {
                    r.close();
                }
            );

            (m.query(sql) as mysqli_result).With(
                r =>
                {
                    r.close();
                }
            );

            m.close();
        }
    }


}
