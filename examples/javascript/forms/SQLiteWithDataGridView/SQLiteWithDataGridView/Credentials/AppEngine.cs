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
   

        [Conditional("AppEngine")]
        static partial void ApplyRestrictedCredentials(SQLiteConnectionStringBuilder b, bool admin = false)
        {
            b.Add("InternalUser", "user1");
            b.Add("InternalPassword", "mypass");
        }

    }
}
