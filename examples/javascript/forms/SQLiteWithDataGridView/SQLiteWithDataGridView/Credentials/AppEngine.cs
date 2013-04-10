using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SQLiteWithDataGridView
{
    partial class ApplicationWebService
    {

        public static partial class Credentials
        {
#if AppEngine
        static partial void ApplyRestrictedCredentials(SQLiteConnectionStringBuilder b, bool admin = false)
        {
            // Caused by: java.sql.SQLException: Access denied for user 'user1'@'localhost' to database 'sqlitewithdatagridview6.sqlite'

            b.Add("InternalUser", "user3");
            b.Password = "mypass";
        }
#endif

        }
    }
}
