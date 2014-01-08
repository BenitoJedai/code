using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.SQLite
{
    public static class SQLiteConnectionStringBuilderExtensions
    {
        public static long Counter = 0;

        public static Action<Action<SQLiteConnection>> AsWithConnection(this SQLiteConnectionStringBuilder csb,
            Action<SQLiteConnection> Initializer = null
            )
        {
            // we a re missing :memory: as used in multimon svg draw experiment

            var cc = default(SQLiteConnection);

            return y =>
            {
                if (cc != null)
                {
                    // reenty!
                    y(cc);
                    return;
                }

                //Console.WriteLine("AsWithConnection... invoke");

                var ccc = Counter++;

                using (var c = new SQLiteConnection(csb.ConnectionString))
                {
                    Console.WriteLine("open SQLiteConnection " + new { ccc });
                    c.Open();

                    cc = c;

                    try
                    {
                        if (Initializer != null)
                            Initializer(c);

                        y(c);
                    }
                    catch (Exception ex)
                    {
                        var message = new { ex.Message, ex.StackTrace };

                        //Console.WriteLine("AsWithConnection... error: " + message);

                        //java
                        //throw new InvalidOperationException(message.ToString());

                        // php
                        throw new Exception(message.ToString());
                    }
                    cc = null;
                }
                Console.WriteLine("close SQLiteConnection or pool it?  " + new { ccc });
            };
        }


    }
}
