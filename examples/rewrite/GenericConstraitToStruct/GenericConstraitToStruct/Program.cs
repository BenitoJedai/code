using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericConstraitToStruct
{
    /*
     
.class private auto ansi sealed beforefieldinit PropertyNullable<TSource, valuetype ([mscorlib]System.ValueType) .ctor TValue>
    extends PlayerIOClient.Internal.Property`2<!TSource, valuetype [mscorlib]System.Nullable`1<!TValue>>

     vs * 
     
.class private auto ansi sealed beforefieldinit PropertyNullable<TSource, ([mscorlib]System.ValueType) TValue>
    extends [mscorlib]System.Object

 
 

     * 
     */
    internal sealed class PropertyNullable<TSource, TValue> /* : Property<TSource, TValue?> */ where TValue : struct
    { 

    }
 

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
