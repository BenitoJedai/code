using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleByCookie.Schema
{
    class SystemConsoleOut : SystemConsoleOutQueries
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;
        public readonly Action<Action<SQLiteConnection>> WithWriteConnection;

        public SystemConsoleOut(string DataSource = "ConsoleByCookie.sqlite")
        {
            this.WithConnection = DataSource.AsWithConnection();
            this.WithWriteConnection = DataSource.AsWithConnection(ReadOnly: false);

            WithWriteConnection(
               c =>
               {
                   new Create { }.ExecuteNonQuery(c);
               }
            );
        }

        public void InsertContent(InsertContent value, Action<long> y)
        {
            WithWriteConnection(
                c =>
                {
                    //__ConsoleToDatabaseWriter.InternalWriteLine("WithWriteConnection ExecuteNonQuery");
                    value.ExecuteNonQuery(c);

                    y(c.LastInsertRowId);
                    //__ConsoleToDatabaseWriter.InternalWriteLine("WithWriteConnection ExecuteNonQuery done");
                }
             );
        }

        public void SelectTransactionKey(SelectTransaction e, Action<long> yield)
        {
            WithConnection(
                c =>
                {
                    e.ExecuteReader(c).WithEach(
                        reader =>
                        {
                            long id = reader.id;

                            yield(id);
                        }
                    );
                }
             );
        }

        public void SelectContentUpdates(SelectContentUpdates value, Action<dynamic> yield)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteReader(c).WithEach(yield);
                }
             );
        }
    }



    public static partial class XX
    {
        public static void WithEach(this SQLiteDataReader reader, Action<dynamic> y)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    y(new DynamicDataReader(reader));
                }
            }
        }





        public static Action<Action<SQLiteConnection>> AsWithConnection(this string DataSource, int Version = 3, bool ReadOnly = true)
        {
            //Console.WriteLine("AsWithConnection...");

            return y =>
            {
                //Console.WriteLine("AsWithConnection... invoke");

                using (var c = DataSource.ToConnection(Version, ReadOnly))
                {
                    try
                    {
                        c.Open();

                        //__ConsoleToDatabaseWriter.InternalWriteLine("AsWithConnection open");
                        y(c);
                        //__ConsoleToDatabaseWriter.InternalWriteLine("AsWithConnection close");
                        c.Close();
                    }
                    catch (Exception ex)
                    {
                        __ConsoleToDatabaseWriter.InternalWriteLine("AsWithConnection... error: ");
                        Thread.Sleep(5000);


                        var message = new { ex.Message, ex.StackTrace };

                        //Console.WriteLine("AsWithConnection... error: " + message);
                        __ConsoleToDatabaseWriter.InternalWriteLine(" " + message);

                        //                Caused by: java.lang.StackOverflowError
                        //at java.io.Win32FileSystem.canonicalize(Unknown Source)
                        //at java.io.File.getCanonicalPath(Unknown Source)
                        //at sun.security.provider.PolicyFile.canonPath(Unknown Source)
                        //at java.io.FilePermission$1.run(Unknown Source)
                        //at java.io.FilePermission$1.run(Unknown Source)
                        //at java.security.AccessController.doPrivileged(Native Method)
                        //at java.io.FilePermission.init(Unknown Source)
                        //at java.io.FilePermission.<init>(Unknown Source)
                        //at java.lang.SecurityManager.checkRead(Unknown Source)
                        //at java.io.File.exists(Unknown Source)
                        //at java.io.Win32FileSystem.canonicalize(Unknown Source)
                        //at java.io.File.getCanonicalPath(Unknown Source)
                        //at sun.security.provider.PolicyFile.canonPath(Unknown Source)
                        //at java.io.FilePermission$1.run(Unknown Source)
                        //at java.io.FilePermission$1.run(Unknown Source)
                        //at java.security.AccessController.doPrivileged(Native Method)
                        //at java.io.FilePermission.init(Unknown Source)
                        //at java.io.FilePermission.<init>(Unknown Source)
                        //at java.lang.SecurityManager.checkRead(Unknown Source)
                        //at java.io.File.exists(Unknown Source)
                        //at sun.misc.URLClassPath$FileLoader.getResource(Unknown Source)
                        //at sun.misc.URLClassPath.getResource(Unknown Source)
                        //at java.net.URLClassLoader$1.run(Unknown Source)
                        //at java.security.AccessController.doPrivileged(Native Method)
                        //at java.net.URLClassLoader.findClass(Unknown Source)
                        //at java.lang.ClassLoader.loadClass(Unknown Source)
                        //at com.google.appengine.tools.development.IsolatedAppClassLoader.loadClass(IsolatedAppClassLoader.java:213)
                        //at java.lang.ClassLoader.loadClass(Unknown Source)
                        //at ConsoleByCookie.Schema.XX___c__DisplayClass1._AsWithConnection_b__0(XX___c__DisplayClass1.java:43)


                        Thread.Sleep(10000);

                        //java
                        //throw new InvalidOperationException(message.ToString());

                        // php
                        throw new Exception(message.ToString());
                    }
                }
            };
        }

        public static SQLiteConnection ToConnection(this string DataSource, int Version = 3, bool ReadOnly = true)
        {
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = Version,
                ReadOnly = ReadOnly
            };

            var c = new SQLiteConnection(csb.ConnectionString);

            return c;
        }
    }

}
