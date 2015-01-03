using ScriptCoreLib.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test435CoreDynamic
{
    public class Class1 : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        // can we use as dynamic.foo in core lib in script?

        // X:\jsc.svn\examples\rewrite\Test\Test453If\Test453If\Program.cs

        public static void Invoke(string item)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150103/dynamic

            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ContinueWith.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ctor.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\TaskFactory.ContinueWhenAll.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Type.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\CanvasRenderingContext2D.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IDocument.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IStyle.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\Worker.cs

            // roslyn seems to emit dynamic sites a bit differently.
            // while stack rewriter can help in merge mode
            // scriptcorelib would also lie to make use of it, with out the merge mode, only with script mode.

            //dynamic self = Native.self;
            //object value = self[item];


            // we should test all use cases for dynamic for roslyn js.


            //if (AQAABMBB3zSFYBzhRvbJgA)
            //{
            //}
            //else
            //{
            //    new Array(2)[0] = twgABr0mMjmREJxf62g10A(0, null);
            //    new Array(2)[1] = twgABr0mMjmREJxf62g10A(3, null);
            //    AQAABMBB3zSFYBzhRvbJgA = hQkABkmHWjqHBHzPjs4Qsg(OwkABiO2_aTySKN41_aKL3ew(0, 'bar', _4R8ABtC6ljmbrk8x5kK6iA(new ctor$bR8ABhfpfj6IFLf_a4gLSZg(type$wUOfiFalnze9oE9IyMu9MA)), new Array(2)));
            //}

            //AQAABMBB3zSFYBzhRvbJgA.Target.GREABtXRBzSbUYeYQmTv4A(AQAABMBB3zSFYBzhRvbJgA, c, 'hello');



            dynamic foo = new object();


            foo.bar = "hello";

        }
    }
}
