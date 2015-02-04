using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestCopyStringToBytePointer
{
    unsafe public class Class1
    {
        public static int GetLibVersion(byte* pszVersion)
        {
            // https://msdn.microsoft.com/en-us/library/ms717795.aspx

            const string v = "C2015";

            for (int i = 0; i < v.Length; i++)
            {
                pszVersion[i] = (byte)v[i];
            }

            return default(int);
        }
    }
}
