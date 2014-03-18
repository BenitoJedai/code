using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetConnectionString
{
    public class SQLiteConnection
    {
        private string conn_str;
        private int db_mode;
        private string db_file;
        private int db_version;
        private Encoding encoding;
        private int busy_timeout;
        private string db_password;

        public static void Main(string[] args)
        {
            // X:\jsc.svn\examples\rewrite\Test\TestSQLiteMSIL\TestSQLiteMSIL\Class1.cs
            // X:\opensource\googlecode\csharp-sqlite\Community.CsharpSqlite.SQLiteClient\src\SqliteConnection.cs

            // Invalid connection string: invalid URI
            var c = new SQLiteConnection();
            //c.SetConnectionString("Data Source=:memory:");
            //c.SetConnectionString("Data Source=/:memory:");
            c.SetConnectionString("Data Source=/goo.sqlite");

            Console.WriteLine(
                new { c.db_file }
                );

            ;
        }

        private void SetConnectionString(string connstring)
        {
            if (connstring == null)
            {
                Close();
                conn_str = null;
                return;
            }

            if (connstring != conn_str)
            {
                Close();
                conn_str = connstring;

                db_file = null;
                db_mode = 0644;

                string[] conn_pieces = connstring.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < conn_pieces.Length; i++)
                {
                    string piece = conn_pieces[i].Trim();
                    int firstEqual = piece.IndexOf('=');
                    if (firstEqual == -1)
                    {
                        throw new InvalidOperationException("Invalid connection string");
                    }
                    string token = piece.Substring(0, firstEqual);
                    string tvalue = piece.Remove(0, firstEqual + 1).Trim();
                    string tvalue_lc = tvalue.ToLower(
#if !SQLITE_WINRT
System.Globalization.CultureInfo.InvariantCulture
#endif
).Trim();
                    switch (token.ToLower(
#if !SQLITE_WINRT
System.Globalization.CultureInfo.InvariantCulture
#endif
).Trim())
                    {
                        case "data source":
                        case "uri":
                            if (tvalue_lc.StartsWith("file://"))
                            {
                                db_file = tvalue.Substring(7);
                            }
                            else if (tvalue_lc.StartsWith("file:"))
                            {
                                db_file = tvalue.Substring(5);
                            }
                            else if (tvalue_lc.StartsWith("/"))
                            {
                                db_file = tvalue;
#if !(SQLITE_SILVERLIGHT || WINDOWS_MOBILE || SQLITE_WINRT)
                            }
                            else if (tvalue_lc.StartsWith("|DataDirectory|",
                                           StringComparison.InvariantCultureIgnoreCase))
                            {
                                AppDomainSetup ads = AppDomain.CurrentDomain.SetupInformation;
                                string filePath = String.Format("App_Data{0}{1}",
                                                 Path.DirectorySeparatorChar,
                                                 tvalue_lc.Substring(15));

                                db_file = Path.Combine(ads.ApplicationBase, filePath);
#endif
                            }
                            else
                            {
#if !WINDOWS_PHONE
                                throw new InvalidOperationException("Invalid connection string: invalid URI");
#else
                                db_file = tvalue;		
#endif
                            }
                            break;

                        case "mode":
                            db_mode = Convert.ToInt32(tvalue);
                            break;

                        case "version":
                            db_version = Convert.ToInt32(tvalue);
                            if (db_version < 3) throw new InvalidOperationException("Minimum database version is 3");
                            break;

                        case "encoding": // only for sqlite2
                            encoding = Encoding.GetEncoding(tvalue);
                            break;

                        case "busy_timeout":
                            busy_timeout = Convert.ToInt32(tvalue);
                            break;

                        case "password":
                            if (!String.IsNullOrEmpty(db_password) && (db_password.Length != 34 || !db_password.StartsWith("0x")))
                                throw new InvalidOperationException("Invalid password string: must be 34 hex digits starting with 0x");
                            db_password = tvalue;
                            break;
                    }
                }

                if (db_file == null)
                {
                    throw new InvalidOperationException("Invalid connection string: no URI");
                }
            }
        }

        private void Close()
        {
        }
    }
}
