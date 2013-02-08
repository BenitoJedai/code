using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.IO;
using ScriptCoreLib.Extensions;

namespace ReadFloat32
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            {
                var m = new MemoryStream();

                m.WriteByte((byte)' ');


                var w = new BinaryWriter(m);

                w.Write((float)0.79);

                var bytes = m.ToBytes();

                Console.WriteLine(bytes.ToHexString());

            }

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
