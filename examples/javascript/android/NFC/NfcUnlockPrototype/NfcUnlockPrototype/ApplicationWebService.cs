using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NfcUnlockPrototype.Design;
using System.Data.SQLite;


namespace NfcUnlockPrototype
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public void fake()
        {
            Type sqlLitec = typeof(SQLiteConnection);
            Type ext = typeof(System.Data.SQLite.SQLiteConnectionStringBuilderExtensions);
        }
       

        public async Task<NfcDBUserAuthRow> IsNfcApproved(string username)
        {
            var res = (from c in new NfcDB.UserAuth()
                       where c.User == username
                       orderby c.Key descending
                       select c).FirstOrDefault();

            return res;
        }

        public async Task InsertUserAuth(string username, bool isCard)
        {
            Console.WriteLine(username + " " + isCard.ToString());

            var res = new NfcDBUserAuthRow {User = username, IsCard = isCard };

            var nfc = new NfcDB.UserAuth();
            nfc.Insert(res);

        }
    }
}
