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

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140217

            Action goo = async delegate
            {

                // when can we do Task, await and then threads?

                Console.WriteLine("hi from goo");

                new TextField { text = "hi from goo" }.AttachTo(this);
            };
            goo();
        }
    }
}
