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
            var res = (from c in new NfcDB.UserAuth()
                       where c.User == username
                       orderby c.Key descending
                       select c).FirstOrDefault();

            Func<DateTime, bool> checkTime = d =>
            {
                var now = DateTime.Now;

                Console.WriteLine(now.ToString());
                Console.WriteLine(d.ToString());
               
                //Console.WriteLine((TimeSpan.FromTicks(now.Ticks) - TimeSpan.FromTicks(d.Ticks)).TotalSeconds.ToString());

                if (TimeSpan.FromTicks(now.Ticks - d.Ticks).TotalSeconds > 10)
                {
                    Console.WriteLine("false");

                    return false;
                }
                else
                {
                    Console.WriteLine("true");

                    return true;
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
                    return checkTime(res2.Timestamp);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var ret = (checkTime(res.Timestamp));
                Console.WriteLine(ret.ToString());
                return ret;
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
