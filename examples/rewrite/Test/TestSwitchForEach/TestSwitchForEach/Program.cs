using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSwitchForEach
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new BsonBinaryWriter().CalculateSize(
                BsonType.Object, new List<object> { new { x = 1 }, new { x = 2 } }
            );

        }
    }

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



    class BsonBinaryWriter
    {
        public int CalculateSize(BsonType t_Type, List<object> value)
        {
            //TestSwitchForEach.BsonBinaryWriter <0000> nop
            //TestSwitchForEach.BsonBinaryWriter <004a> nop
            //TestSwitchForEach.BsonBinaryWriter <0053> br.s, to be optimized away
            //enter finally { mname = <0053> br.s, to be optimized away.try }
            //TestSwitchForEach.BsonBinaryWriter <006b> ldloca.s
            //enter finally { mname = <006b> ldloca.s.try }
            //TestSwitchForEach.BsonBinaryWriter <0055> ldloca.s
            //{ p = { x = 1 } }
            //enter finally { mname = <0055> ldloca.s.try }
            //TestSwitchForEach.BsonBinaryWriter <006b> ldloca.s
            //enter finally { mname = <006b> ldloca.s.try }
            //TestSwitchForEach.BsonBinaryWriter <0055> ldloca.s
            //{ p = { x = 2 } }
            //enter finally { mname = <0055> ldloca.s.try }
            //TestSwitchForEach.BsonBinaryWriter <006b> ldloca.s
            //enter finally { mname = <006b> ldloca.s.try }
            //TestSwitchForEach.BsonBinaryWriter <0078> leave.s, to be optimized away
            //enter finally { mname = <0078> leave.s, to be optimized away.try }
            //TestSwitchForEach.BsonBinaryWriter <0089> nop
            //TestSwitchForEach.BsonBinaryWriter <00ab> ldloc.1

            switch (t_Type)
            {
                case BsonType.Object:
                    {
                        foreach (var p in value)
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
    }
}
