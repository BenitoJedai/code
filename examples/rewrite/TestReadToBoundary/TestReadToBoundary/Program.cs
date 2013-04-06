using ScriptCoreLib.Shared.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReadToBoundary
{
    class Program
    {
        static void Main(string[] args)
        {
            SmartStreamReader.InternalBufferCapacity = 9;

            var input = new MemoryStream(Encoding.ASCII.GetBytes("xa\rbbb\n ?ccc\r\n                      yuuuux\nz"));
            var r = new SmartStreamReader(input);
            var a = r.ReadLine();

            var x = r.ReadToBoundary("uuuu");

            var y = Encoding.UTF8.GetString(x.ToArray());

            var z = r.ReadLine();
        }
    }
}
