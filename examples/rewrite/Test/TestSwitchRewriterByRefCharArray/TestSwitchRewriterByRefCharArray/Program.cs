using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSwitchRewriterByRefCharArray
{
    internal enum BsonType : sbyte
    {
        Number = 1,
        String = 2,
        Object = 3,
        Array = 4,
        Binary = 5,
        Undefined = 6,
        Oid = 7,
        Boolean = 8,
        Date = 9,
        Null = 10,
        Regex = 11,
        Reference = 12,
        Code = 13,
        Symbol = 14,
        CodeWScope = 15,
        Integer = 16,
        TimeStamp = 17,
        Long = 18,
        MinKey = -1,
        MaxKey = 127
    }

    struct foo
    {
        public char[] writeBuffer;
    }

    class Program
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140524

        //public static int WriteEscapedJavaScriptString(BsonType t_Type, ref  char[] writeBuffer)
        //public static int WriteEscapedJavaScriptString(BsonType t_Type, char[] writeBuffer)



        public int WriteEscapedJavaScriptString(
            BsonType t_Type,
            char[] writeBuffer,
            // why is this not made byref?
            foo foo)
        {
            foo foocopy = foo;

            // X:\jsc.svn\examples\rewrite\Test\TestSwitchForEach\TestSwitchForEach\Program.cs

            switch (t_Type)
            {
                case BsonType.Object:
                    {
                        //foreach (var p in writeBuffer)
                        foreach (var p in foo.writeBuffer)
                        {
                            Console.WriteLine(new { p });
                        }
                        return -1;
                    }

                case BsonType.Integer:
                    return 4;
                case BsonType.Long:
                    return 8;
                case BsonType.Number:
                    return 8;
                case BsonType.Boolean:
                    return 1;
                case BsonType.Null:
                case BsonType.Undefined:
                    return 0;
                case BsonType.Date:
                    return 8;
                default:
                    return 12;
            }
        }

        static void Main(string[] args)
        {
            var x = new[] { 'a', 'x' };


            //WriteEscapedJavaScriptString(BsonType.Object, ref x);
            //WriteEscapedJavaScriptString(BsonType.Object, x);
            new Program().WriteEscapedJavaScriptString(BsonType.Object, x, new foo { writeBuffer = x });

        }
    }
}
