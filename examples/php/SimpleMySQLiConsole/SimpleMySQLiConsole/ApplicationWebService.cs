using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.PHP;
using System;
using System.Linq;
using System.Xml.Linq;

namespace SimpleMySQLiConsole
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/2012/20121217


        public void WebMethod2(string e, Action<string> y)
        {
            var m = new mysqli(
                "localhost",
                "root",
                ""
            );

            // <document><y><obj>{ server_info = 5.5.25a, server_version = 50525 }</obj></y></document>
            //<b>Warning</b>:  mysqli::mysqli(): (HY000/1049): Unknown database 'datasource1001' in <b>B:\inc\SimpleMySQLiConsole.ApplicationWebService.exe\class.SimpleMySQLiConsole.ApplicationWebService.php</b> on line <b>17</b><br />

            // http://svn2.assembla.com/svn/nooku-framework/trunk/code/libraries/koowa/database/adapter/mysqli.php
            // http://stackoverflow.com/questions/2203110/check-if-a-variable-is-of-type-mysqli-object
            var __is_mysqli = m is mysqli;

            var message = new { m.server_info, m.server_version, __is_mysqli };

            // Send it back to the caller.
            y(message.ToString());
        }

        public void __mysqli_query(string sql, Action<string> y)
        {
            var m = new mysqli(
                "localhost",
                "root",
                ""
            );

            var r = m.query(sql);

            (r as mysqli_result).With(
                result =>
                {
                    var f0 = result.fetch_field_direct(0);
                    var d0 = Native.DumpToString(f0);

                    var message = new { result.field_count, result.num_rows, d0 };

                    // Send it back to the caller.
                    y(message.ToString());



                    result.close();
                }
            );
        }
    }

    [Script(IsNative = true)]
    class mysqli_result
    {
        public int field_count;
        public int num_rows;


        // http://php.net/manual/en/mysqli-result.fetch-field-direct.php

        /// <summary>
        /// Fetch meta-data for a single field
        /// </summary>
        /// <param name="fieldnr">The field number. This value must be in the range from 0 to number of fields - 1.</param>
        /// <returns>Returns an object which contains field definition information from the specified result set.</returns>
        public object fetch_field_direct(int fieldnr)
        {
            return default(object);
        }

        public void close()
        {

        }
    }

    // http://php.net/manual/en/class.mysqli-result.php
    [Script(IsNative = true)]
    class mysqli
    {
        // http://php.net/manual/en/mysqli.quickstart.prepared-statements.php
        // http://php.net/manual/en/mysqlinfo.concepts.buffering.php

        public mysqli(
            string host,
            string user,
            string password
            //, string datasource
            )
        {

        }

        // http://php.net/manual/en/mysqli.get-server-version.php
        public int server_version;

        // http://php.net/manual/en/mysqli.get-server-info.php
        public string server_info;

        // http://php.net/manual/en/mysqli.query.php
        public object query(string sql)
        {
            return default(mysqli_result);
        }

    }
}
