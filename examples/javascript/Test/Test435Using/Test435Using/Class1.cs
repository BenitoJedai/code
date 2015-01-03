using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace Test435Using
{
    public static class Class1
    {
        static void content() { }

        static void Fill(IDisposable reader)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150103/using
            // X:\jsc.svn\examples\rewrite\Test\Test453If\Test453If\Program.cs

            //0x0001.ldarg.0  arg.0 reader: IDisposable()-> void
            //0x0002. .ldnull  null
            //0x0003.cgt.un
            //0x0005 stloc.0  loc.0 : bool
            //0x0006.ldloc.0  loc.0 : bool
            //0x0007 brfalse.s

            if (reader != null)
            //{ 
                // will adding a scope block help?
                using (reader)
                    content();


            //}

        }

    }
}
