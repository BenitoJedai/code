using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace Test453RefByteLocal
{
    public class Class1
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150103
        static byte[] m_byteInput;

        public static void Invoke()
        {
            byte[] bMsg = new byte[m_byteInput.Length];
            bMsg[m_byteInput.Length] |= 0x80;		///making first bit of padding 1,

        }

        //    b = new Array((~~AQAABBu2WjybquZU0FQrsA.length));
        //c = b[(~~AQAABBu2WjybquZU0FQrsA.length)];
        //c[0] = ((((c[0] | 128)) >>> 0) & 0xff);

    }
}
