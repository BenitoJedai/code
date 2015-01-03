using ScriptCoreLib.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test435CoreDynamic
{
    public class Class1 : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        // can we use as dynamic.foo in core lib in script?

        public static void Invoke(string item)
        {
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ContinueWith.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ctor.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\TaskFactory.ContinueWhenAll.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Type.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\CanvasRenderingContext2D.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IDocument.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IStyle.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\Worker.cs

            dynamic self = Native.self;
            object value = self[item];

        }
    }
}
