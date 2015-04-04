using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    //[Script]
    //class __Stream
    //{
    //    public virtual void WriteByte(byte value)
    //    {

    //    }
    //}

    //[Script]
    //class __MemoryStream : __Stream
    //{
    //    public byte[] m_inline_array;
    //    public int m_inline_index = 0;

    //    public virtual int Capacity
    //    {
    //        get
    //        {
    //            if (m_inline_array == null)
    //                return 0x1000;

    //            return m_inline_array.Length;

    //        }
    //        set
    //        {
    //        }
    //    }

    //    void InternalEnsureCapacity(long TargetCapacity)
    //    {
    //        if (Capacity < TargetCapacity)
    //            Capacity = (int)(TargetCapacity + 8);
    //    }

    //    public override void WriteByte(byte value)
    //    {
    //        m_inline_array[m_inline_index++] = value;
    //    }
    //}

    partial class __Convert
    {
        public static byte[] FromBase64String(string input)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402/frombase64string
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402

            // X:\jsc.svn\examples\javascript\test\TestIntPostfixIncrement\TestIntPostfixIncrement\Class1.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201502/20150226

            // X:\jsc.svn\examples\javascript\synergy\WebServicePDFGenerator\WebServicePDFGenerator\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\Test\TestMemoryStreamPerformance\TestMemoryStreamPerformance\Application.cs
            // X:\jsc.svn\examples\java\JVMCLRBase64\JVMCLRBase64\Program.cs
            // X:\jsc.svn\examples\java\hybrid\JVMCLRBase64\

            //Console.WriteLine("enter __Convert.FromBase64String " + new { input.Length });
            // preroslyn broken?

            var FromBase64String_while_timeout = Stopwatch.StartNew();


            if (string.IsNullOrEmpty(input))
                return new byte[0];

            // X:\jsc.svn\examples\javascript\Test\TestFromBase64String\TestFromBase64String\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestInt32Div\TestInt32Div\Class1.cs
            var capacity = 4 * input.Length / 3;

            // how slow is it?
            var m = new MemoryStream { Capacity = capacity };
            // why the fk does using MemoryStream WriteByte take so long here?
            // { w0ms = 11777, w1ms = 11619, bytes = 65536, bytes999 = 65536, string0 = 87384 }
            // { w0ms = 208, w1ms = 74, bytes = 116512, bytes999 = 65536, string0 = 87384 }
            //var m_inline_array = new byte[capacity];
            //var m_inline_index = 0;

            //var mm = new __MemoryStream { m_inline_array = new byte[capacity] };


            var length = input.Length;

            int chr1, chr2, chr3;
            int enc1, enc2, enc3, enc4;

            int i = 0;

            bool b = true;

            if (i < length)
                while (b)
                {
                    enc1 = 64;
                    enc2 = 64;
                    enc3 = 64;
                    enc4 = 64;

                    if (i < length)
                        enc1 = Base64Key.IndexOf(input[i++]);
                    if (i < length)
                        enc2 = Base64Key.IndexOf(input[i++]);
                    if (i < length)
                        enc3 = Base64Key.IndexOf(input[i++]);
                    if (i < length)
                        enc4 = Base64Key.IndexOf(input[i++]);

                    chr1 = (enc1 << 2) | (enc2 >> 4);
                    chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                    chr3 = ((enc3 & 3) << 6) | enc4;

                    m.WriteByte((byte)chr1);
                    //mm.WriteByte((byte)chr1);
                    //m_inline_array[m_inline_index++] = ((byte)chr1);

                    if (enc3 != 64)
                    {
                        m.WriteByte((byte)chr2);
                        //mm.WriteByte((byte)chr2);
                        //m_inline_array[m_inline_index++] = ((byte)chr2);

                    }
                    if (enc4 != 64)
                    {
                        m.WriteByte((byte)chr3);
                        //mm.WriteByte((byte)chr3);
                        //m_inline_array[m_inline_index++] = ((byte)chr3);
                    }

                    // @progress this is how this function would report progress
                    //if (FromBase64String_while_timeout.ElapsedMilliseconds > 500)
                    //{
                    //    Console.WriteLine("FromBase64String loop!! " + new
                    //    {
                    //        FromBase64String_while_timeout = FromBase64String_while_timeout.ElapsedMilliseconds,
                    //        i,
                    //        // m_inline_index,
                    //        //mm.m_inline_index,
                    //        input.Length,
                    //        enc1,
                    //        enc2,
                    //        enc3,
                    //        enc4
                    //    });
                    //    FromBase64String_while_timeout.Restart();
                    //}

                    b = i < length;
                }

            var value = m.ToArray();

            //Console.WriteLine("exit __Convert.FromBase64String " + new { value.Length });

            // value 61 is also valid?
#if BUGCHECK
            if (input.Length == 84)
                if (value.Length != 63)
                    throw new Exception(
                        "bugcheck  __Convert.FromBase64String " + new { inputLength = input.Length, value = value.Length, input });
#endif


            return value;
            //return m_inline_array;
            //return mm.m_inline_array;
        }



    }

}
