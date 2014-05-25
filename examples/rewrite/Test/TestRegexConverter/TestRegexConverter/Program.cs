using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestRegexConverter
{


    public class RegexConverter : IEnumerator
    {
        private Regex MoveNext()
        {
            //string pattern = null;
            RegexOptions? options = null;

            //PEVerify [IL]: Error: [X:\jsc.svn\examples\rewrite\test\TestRegexConverter\TestRegexConverter\bin\Debug\xTestRegexConverter.exe : TestRegexConverter.RegexConverter+<MoveNext>06000001::<0000> nop][offset 0x0000003C] Stack must contain only the return value.
            //PEVerify [IL]: Error: [X:\jsc.svn\examples\rewrite\test\TestRegexConverter\TestRegexConverter\bin\Debug\xTestRegexConverter.exe : TestRegexConverter.RegexConverter+<MoveNext>06000001::<0023> nop][offset 0x00000011] Stack underflow.

            return new Regex("", options ?? RegexOptions.None);

        }



        object IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }

        bool IEnumerator.MoveNext()
        {
            throw new NotImplementedException();
        }

        void IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }
    }


    class Program
    {
        // PEVerify [IL]: Error: [X:\jsc.svn\examples\rewrite\test\TestRegexConverter\TestRegexConverter\bin\Debug\xTestRegexConverter.exe : TestRegexConverter.RegexConverter+<ReadRegexObject>06000006::<00b8> ldloca.s][offset 0x00000017] Stack must contain only the return value.

        //error at CopyType:
        //*Illegal one - byte branch at position: 43.Requested branch was: 130.
        //* Newtonsoft.Json.Converters.RegexConverter 020000c9

        //* IllegalBranchAt 0000002b

        //* RequestedBranch 130

        static void Main(string[] args)
        {
        }
    }
}
