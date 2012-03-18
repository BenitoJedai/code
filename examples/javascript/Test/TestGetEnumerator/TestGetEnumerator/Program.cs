using System;
using ScriptCoreLib;
using System.Reflection;
using System.IO;

[assembly: Script, ScriptTypeFilter(ScriptType.JavaScript, typeof(TestGetEnumerator.JavaScript.__List<>))]

namespace TestGetEnumerator
{
    namespace JavaScript
    {
        //  error CS0695: 'TestGetEnumerator.JavaScript.__List<T,Y>' cannot implement both 
        // 'System.Collections.Generic.IEnumerable<T>' and 
        // 'System.Collections.Generic.IEnumerable<Y>' because they may unify for some type parameter substitutions

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
            var t = typeof(System.Collections.Generic.IEnumerable<object>);
            var t_int = typeof(System.Collections.Generic.IEnumerable<int>);


            var builde_server = 0x60002b0;
            var pc = 0x60002c2;



            WriteSpecialBase64(WriteGUIDAndToken64(t.GetMethods()[0]));
            WriteSpecialBase64(WriteGUIDAndToken64(t_int.GetMethods()[0]));


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
            Console.WriteLine();
            Console.WriteLine(x.DeclaringType.AssemblyQualifiedName);


            var Generic = ToGenericDefinition(x.DeclaringType);

            Console.WriteLine(Generic.FullName + "." + x.Name);

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
