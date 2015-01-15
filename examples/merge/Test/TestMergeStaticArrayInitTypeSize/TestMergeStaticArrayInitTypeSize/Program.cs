using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMergeStaticArrayInitTypeSize
{
    class Program
    {
        // Show Details	Severity	Code	Description	Project	File	Line
        //Error CS0029  Cannot implicitly convert type 'int[]' to 'byte[]'	TestMergeStaticArrayInitTypeSize Program.cs  11

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150115/staticarrayinittypesize
        static int[] data = new[] { 1, 2, 3, 4 };
        static int[] lib = TestMergeStaticArrayInitTypeSizeLib.Class1.data;


        static void Main(string[] args)
        {
        }
    }
}
