using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.PHP;
using SimpleMySQLiConsole.Schema;
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

        public void __connect_badhost(string e, Action<string> y)
        {
            //            <br />
            //<b>Warning</b>:  mysqli::mysqli(): php_network_getaddresses: getaddrinfo failed: No such host is known.  in <b>B:\inc\SimpleMySQLiConsole.ApplicationWebService.exe\class.SimpleMySQLiConsole.ApplicationWebService.php</b> on line <b>17</b><br />
            //<br />
            //<b>Warning</b>:  mysqli::mysqli(): (HY000/2002): php_network_getaddresses: getaddrinfo failed: No such host is known.  in <b>B:\inc\SimpleMySQLiConsole.ApplicationWebService.exe\class.SimpleMySQLiConsole.ApplicationWebService.php</b> on line <b>17</b><br />

            // http://php.net/manual/en/function.error-reporting.php
            Native.API.error_reporting(64);

            var m = new mysqli(
                "nolocalhost",
                "noroot",
                ""
            );

            var message = new { m.connect_errno };
            y(message.ToString());
        }

        public void __connect_baduser(string e, Action<string> y)
        {
            Native.API.error_reporting(64);

            var m = new mysqli(
                "localhost",
                "noroot",
                ""
            );

            var message = new { m.connect_errno, m.errno };
            y(message.ToString());
        }

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
            //var __is_mysqli = m is mysqli;

            var message = new { m.server_info, m.server_version, m.stat };

            // Send it back to the caller.
            y(message.ToString());
        }

        public void __mysqli_query(
            string sql,
            Action<string> y,
            Action<string, string> yield_field,
            Action<XElement> yield_resultset
            )
        {
            var h = new History();


            var m = new mysqli(
                "localhost",
                "root",
                ""
            );

            var flag = m.multi_query(sql);

            var sqlindex = 0;

            {
                var message = new
                {
                    sqlindex,
                    m.errno,
                    m.error,
                    m.insert_id,
                };
                y(message.ToString());
            }

            while (flag)
            {

                var rr = m.store_result();

                //Native.Dump(rr);

                (rr as mysqli_result).With(
                    result =>
                    {
                        #region mysqli_result
                        //var f0 = result.fetch_field_direct(0);
                        //var d0 = Native.DumpToString(f0);

                        var message = new
                        {
                            sqlindex,

                            m.errno,
                            m.error,

                            result.field_count,
                            result.num_rows,

                            m.insert_id,

                            //, d0 
                        };
                        y(message.ToString());

                        //Console.WriteLine("before fields");

                        var resultset = new XElement("table");

                        var resultset_th = new XElement("th");

                        resultset.Add(resultset_th);


                        var fields = Enumerable.Range(0, result.field_count).Select(
                            // jsc can we have implicit delegates to natives?
                            x => result.fetch_field_direct(x)
                        ).Select(
                            (f, i) =>
                            {
                                var n = new { f.name, i, f.type };

                                //Console.WriteLine("field: " + n);
                                resultset_th.Add(
                                    new XElement("td",
                                        new XAttribute("title", "type " + f.type),
                                         f.name
                                    )
                                );

                                yield_field(f.name, "" + f.type);

                                return n;
                            }
                        ).ToArray();

                        //Console.WriteLine("after fields");


                        for (int row = 0; row < result.num_rows; row++)
                        {
                            result.data_seek(row);

                            var resultset_row = new XElement("tr");

                            resultset_row.Add(
                                new XAttribute("title", "row " + row)
                            );

                            var row_data = result.fetch_row();

                            //Console.WriteLine("row: " + row);

                            // broken?
                            //fields.WithEach(

                            for (int i = 0; i < fields.Length; i++)
                            {
                                fields[i].With(
                                               f =>
                                               {
                                                   //Console.WriteLine("field: " + f.name);

                                                   var data = row_data[f.i];

                                                   resultset_row.Add(
                                                       new XElement("td",
                                                           "" + data
                                                       )
                                                   );

                                               }

                                           );
                            }

                            //resultset_row.Add(
                            //    new XElement("dump", Native.DumpToString(row_data))
                            //);


                            resultset.Add(resultset_row);
                        }


                        yield_resultset(resultset);

                        result.close();
                        #endregion


                    }
                );

                //  mysqli::next_result(): There is no next result set. Please, call mysqli_more_results()/mysqli::more_results() to check whether to call this function/method

                flag = m.more_results();

                if (flag)
                {
                    flag = m.next_result();

                    {
                        var message = new
                        {
                            sqlindex,
                            m.errno,
                            m.error,
                            m.insert_id,
                        };
                        y(message.ToString());
                    }
                }

                sqlindex++;
            }

            m.close();
        }
    }

    //d0 = object(stdClass)#30 (13) { ["name"]=> string(13) "TABLE_CATALOG" ["orgname"]=> string(13) "TABLE_CATALOG" ["table"]=> string(6) "TABLES" ["orgtable"]=> string(6) "TABLES" ["def"]=> string(0) "" ["db"]=> string(18) "information_schema" ["catalog"]=> string(3) "def" ["max_length"]=> int(3) ["length"]=> int(512) ["charsetnr"]=> int(8) ["flags"]=> int(1) ["type"]=> int(253) ["decimals"]=> int(0) } }

    [Script(IsNative = true)]
    abstract class mysqli_result_field
    {
        public string name;
        public int type;
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
        public mysqli_result_field fetch_field_direct(int fieldnr)
        {
            return default(mysqli_result_field);
        }

        public bool data_seek(int offset)
        {
            return default(bool);
        }

        public object[] fetch_row()
        {
            return default(object[]);
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

        public string stat;

        public object insert_id;

        public string connect_errno;

        // http://php.net/manual/en/mysqli.error.php
        public string errno;
        public string error;

        // http://php.net/manual/en/mysqli.get-server-version.php
        public int server_version;

        // http://php.net/manual/en/mysqli.get-server-info.php
        public string server_info;

        // http://php.net/manual/en/mysqli.query.php
        public object query(string sql)
        {
            return default(mysqli_result);
        }

        // http://php.net/manual/en/mysqli.next-result.php
        public bool next_result()
        {
            return default(bool);
        }

        public bool more_results()
        {
            return default(bool);
        }


        public bool multi_query(string query)
        {
            return default(bool);
        }

        // http://php.net/manual/en/mysqli.use-result.php
        public object use_result()
        {
            return default(mysqli_result);
        }

        // http://php.net/manual/en/mysqli.store-result.php
        public object store_result()
        {
            return default(mysqli_result);
        }

        public void close()
        {
        }
    }
}
