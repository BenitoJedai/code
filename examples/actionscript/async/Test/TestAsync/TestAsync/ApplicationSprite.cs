using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System;

namespace TestAsync
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // X:\jsc.svn\examples\java\async\Test\TestByRefArgumentLdFld\TestByRefArgumentLdFld\Class1.cs

            //TypeError: Error #1034: Type Coercion failed: cannot convert []@4fc1ba1 to ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__IAsyncStateMachine.
            //    at ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices::__AsyncVoidMethodBuilder/Start_4ebbe596_060009db()[V:\web\ScriptCoreLib\Shared\BCLImplementation\System\Runtime\CompilerServices\__AsyncVoidMethodBuilder.as:44]
            //    at TestAsync::ApplicationSprite/__ctor_b__1_9ca8be72_06000002()[V:\web\TestAsync\ApplicationSprite.as:59]
            //    at Function/http://adobe.com/AS3/2006/builtin::apply()
            //    at ScriptCoreLib.Shared.BCLImplementation.System::__Action/Invoke_4ebbe596_06001d97()[V:\web\ScriptCoreLib\Shared\BCLImplementation\System\__Action.as:30]
            //    at TestAsync::ApplicationSprite()[V:\web\TestAsync\ApplicationSprite.as:39]

            Action goo = async delegate
            {



                Console.WriteLine("hi from goo");

                new TextField { text = "hi from goo" }.AttachTo(this);
            };
            goo();
        }
    }
}
