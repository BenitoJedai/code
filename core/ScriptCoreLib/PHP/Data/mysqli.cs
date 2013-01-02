using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.Data
{
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

        // http://php.net/manual/en/mysqli.prepare.php
        public object prepare(string query)
        {
            return default(object);
        }


        public string stat;

        public int insert_id;

        // http://php.net/manual/en/mysqli.connect-errno.php
        public int connect_errno;
        public string connect_error;

        // http://php.net/manual/en/mysqli.error.php
        public int errno;

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
