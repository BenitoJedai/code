using ScriptCoreLib.PHP.Data;
using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnection))]
    internal class __SQLiteConnection : __DbConnection
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322
        // tested by X:\jsc.svn\examples\php\SimpleMySQLiConsole\SimpleMySQLiConsole\ApplicationWebService.cs

        public mysqli InternalConnection;

        public override string ConnectionString { get; set; }

        public __SQLiteConnection(string connectionstring)
        {
        }

        public override void Open()
        {
            this.InternalConnection = new mysqli(
                host: __SQLiteConnectionStringBuilder.InternalConnectionString.InternalHost,
                user: __SQLiteConnectionStringBuilder.InternalConnectionString.InternalUser,
                password: __SQLiteConnectionStringBuilder.InternalConnectionString.Password
            );

            //http://php.net/manual/en/mysqli.errno.php

            if (this.InternalConnection.connect_errno != 0)
            {
                var message = new
                {
                    this.InternalConnection.connect_errno,

                    //hint = "Check your credentials!",


                    this.InternalConnection.connect_error,

                    ConnectionString = new
                    {
                        __SQLiteConnectionStringBuilder.InternalConnectionString.InternalHost,
                        __SQLiteConnectionStringBuilder.InternalConnectionString.InternalUser
                    }
                };

                throw new Exception(
                    message.ToString()
                );
            }

            this.InternalConnection.query(
                "CREATE DATABASE IF NOT EXISTS `" + __SQLiteConnectionStringBuilder.InternalConnectionString.DataSource + "`"
            );

            if (this.InternalConnection.errno != 0)
            {
                var message = new
                {
                    this.InternalConnection.errno,

                    hint = "Check your credentials!",


                    this.InternalConnection.error,
                };

                throw new Exception(
                    message.ToString()
                );
            }

            this.InternalConnection.query(
                "use `" + __SQLiteConnectionStringBuilder.InternalConnectionString.DataSource + "`"
            );
        }



        public override void Close()
        {
            if (this.InternalConnection == null)
                return;

            this.InternalConnection.close();
            this.InternalConnection = null;
        }

        //public override void Dispose()
        //{
        //    this.Close();
        //}


        public long LastInsertRowId
        {
            get
            {
                return this.InternalConnection.insert_id;
            }
        }
    }

}
