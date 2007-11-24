using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;

namespace cncserver
{
    public static partial class Extensions
    {
        public static T ToInterruptableCall<T>(out Action interrupt, Func<T> f)
        {
            T ReturnValue = default(T);
            var r = new ManualResetEvent(false);

            Thread t = new Thread
            (
                delegate()
                {
                    try
                    {
                        ReturnValue = f();
                    }
                    finally
                    {
                        r.Set();
                    }
                }
            );

            interrupt = delegate
            {
                r.Set();
            };

            t.Start();

            r.WaitOne();

            if (t.IsAlive)
                t.Abort();

            return ReturnValue;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Use(this ConsoleColor c, Action e)
        {
            var x = Console.ForegroundColor;

            Console.ForegroundColor = c;

            e();

            Console.ForegroundColor = x;
        }

        public static string ToMD5String(this FileInfo f)
        {
            StringBuilder sb =new StringBuilder();
            using (FileStream fs = f.OpenRead())
            {
                MD5 md5 =new MD5CryptoServiceProvider();
                byte[] hash = md5.ComputeHash(fs);
                fs.Close();
                foreach (byte hex in hash)
                    sb.Append(hex.ToString("x2"));

            }
            return sb.ToString();
        }
    }

}
