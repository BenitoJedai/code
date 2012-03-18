using System;
using ScriptCoreLib;
using System.Reflection;
using System.IO;

[assembly: Script, ScriptTypeFilter(ScriptType.JavaScript, typeof(TestGetEnumerator.JavaScript.__List<>))]

namespace TestGetEnumerator
{
    namespace JavaScript
    {
        [Script]
        class __List<T> : System.Collections.Generic.IEnumerable<T>
        {
            System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
            {
                throw null;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                throw null;
            }
        }
    }

    internal static class Program
    {
        public static void Main(string[] args)
        {
            System.Collections.Generic.IEnumerable<object> o = new TestGetEnumerator.JavaScript.__List<object>();
            var t = typeof(System.Collections.Generic.IEnumerable<object>);

            var g = t.GetGenericTypeDefinition();

            Func<System.Collections.Generic.IEnumerator<object>> m = o.GetEnumerator;



            {
                var bytes = WriteGUIDAndToken64(m.Method);

                Console.WriteLine("TestGetEnumerator.JavaScript.__List.GetEnumerator");
                WriteSpecialBase64(bytes);

                var GUID = g.GUID;
                var MetadataToken = m.Method.MetadataToken;
            }

            {
                var Methods = t.GetMethods();
                var bytes = WriteGUIDAndToken64(Methods[0]);

                Console.WriteLine("System.Collections.Generic.IEnumerable.GetEnumerator");
                WriteSpecialBase64(bytes);

            }

            Console.ReadKey(true);
        }

        private static void WriteSpecialBase64(byte[] e)
        {
            string name64 = Convert.ToBase64String(e).
                Replace("+", "_a").
                Replace("/", "_b").
                Replace("=", "");

            if (!char.IsLetter(name64[0]))
                Console.Write("_");

            Console.WriteLine(name64);
        }

        public static Type ToGenericDefinition(Type x)
        {
            return x.IsGenericType ?
                    x.GetGenericTypeDefinition() :
                    x;
        }

        private static byte[] WriteGUIDAndToken64(MemberInfo x)
        {
            var Generic = ToGenericDefinition(x.DeclaringType);
            var GUID = Generic.GUID;
            var GUID_bytes = GUID.ToByteArray();

            var m = new MemoryStream(GUID_bytes);

            Console.WriteLine("GUID:");
            foreach (var item in GUID_bytes)
            {
                Console.Write(item.ToString("x2"));
            }
            Console.WriteLine();
           
            int m_token = x.MetadataToken;
            Console.WriteLine("MetadataToken:");
            Console.WriteLine(m_token.ToString("x2"));

            do
            {
                m.WriteByte((byte)(m_token & 0xff));
            }
            while ((m_token >>= 8) > 0);

            return m.ToArray();
        }
    }
}
