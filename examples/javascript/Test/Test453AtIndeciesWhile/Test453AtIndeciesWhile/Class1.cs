using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: System.Reflection.Obfuscation(Feature = "script")]

namespace Test453AtIndeciesWhile
{
    public class AtIndeciesArguments
        : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        public string e;
        public string target;
        public int i;

        public int YieldIndex;

        public Action YieldBreak;
    }

    public delegate void AtIndeciesDelegate(AtIndeciesArguments a);

    public static class Class1
    {

        public static void AtIndecies(this string e, string target, AtIndeciesDelegate h)
        {
            // X:\jsc.svn\examples\javascript\Test\Test453AtIndeciesWhile\Test453AtIndeciesWhile\Class1.cs
            // X:\jsc.svn\examples\javascript\css\Test\TestLongWebMethod\TestLongWebMethod\ApplicationWebService.cs

            var i = e.IndexOf(target);
            var YieldIndex = -1;
            while (i >= 0)
            {
                YieldIndex++;

                Action YieldBreak = () => i = -1;

                h(
                    new AtIndeciesArguments
                    {
                        e = e,
                        i = i,
                        target = target,
                        YieldIndex = YieldIndex,
                        YieldBreak = YieldBreak
                    }
                );


                if (i >= 0)
                    i = e.IndexOf(target, i + target.Length);
            }
        }
    }
}
