using FormsNfcAuth.Design;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FormsNfcAuth
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        //script: error JSC1000: No implementation found for this native method, please implement [System.Data.SQLite.SQLiteConnection.get_LastInsertRowId()]
        //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
        //script: error JSC1000: error at FormsNfcAuth.Design.NfcDB+UserAuth+Queries.Insert,

        public Task<bool> IsNfcApproved(string user)
        {
            return new XNfcAppService().IsNfcApproved(user);
        }

        public Task InsertUserAuth(string user)
        {
            return new XNfcAppService().InsertUserAuth(user);
        }
    }

    public interface INfcInterface
    {
        Task<bool> IsNfcApproved(string user);
        Task InsertUserAuth(string username);
    }

    public class XNfcAppService : INfcInterface
    {

        public void fake()
        {
            Type sqlLitec = typeof(SQLiteConnection);
            Type ext = typeof(System.Data.SQLite.SQLiteConnectionStringBuilderExtensions);
        }


        public async Task<bool> IsNfcApproved(string username)
        {

            var HsmConn = "38-12-A4-19-C6-3B-90-00";


            var res = (from c in new NfcDB.UserAuth()
                       where c.User == username
                       orderby c.Key descending
                       select c).FirstOrDefault();

            var res3 = (from c in new NfcDB.UserAuth()
                        where c.User == HsmConn
                        orderby c.Key descending
                        select c).FirstOrDefault();

            Func<DateTime, DateTime, bool> checkTime = (d1, d2) =>
            {
                var now = DateTime.Now.ToUniversalTime();

                Console.WriteLine(now.ToString());
                Console.WriteLine(d1.ToString());

                //Console.WriteLine((TimeSpan.FromTicks(now.Ticks) - TimeSpan.FromTicks(d.Ticks)).TotalSeconds.ToString());

                if (TimeSpan.FromTicks(now.Ticks - d1.Ticks).TotalSeconds <= 10)
                {
                    if (TimeSpan.FromTicks(now.Ticks - d2.Ticks).TotalSeconds <= 10)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            };

            if (res == null)
            {
                Thread.Sleep(2000);
                var res2 = (from c in new NfcDB.UserAuth()
                            where c.User == username
                            orderby c.Key descending
                            select c).FirstOrDefault();

                if (res2 != null)
                {
                    if (res3 != null)
                    {
                        return checkTime(res2.Timestamp, res3.Timestamp);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return checkTime(res.Timestamp, res3.Timestamp);
            }
        }

        public async Task InsertUserAuth(string username)
        {
            Console.WriteLine(username);

            var res = new NfcDBUserAuthRow { User = username };

            var nfc = new NfcDB.UserAuth();
            nfc.Insert(res);

        }

    }
}
