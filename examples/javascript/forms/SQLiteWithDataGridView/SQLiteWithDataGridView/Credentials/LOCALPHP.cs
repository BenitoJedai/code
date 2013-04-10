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

        //Error	1	A partial method may not have multiple implementing declarations	X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\Credentials\LOCALPHP.cs	15	29	SQLiteWithDataGridView

        public static partial class Credentials
        {
#if LOCALPHP
        static partial void ApplyRestrictedCredentials(SQLiteConnectionStringBuilder b, bool admin = false)
        {
            //b.Add("InternalHost", "invalidhost");

            b.Add("InternalUser", "user3");
            b.Password = "mypass";
        }
#endif
        }

    }
}
