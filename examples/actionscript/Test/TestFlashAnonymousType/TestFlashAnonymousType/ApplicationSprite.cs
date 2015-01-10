using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace TestFlashAnonymousType
{
    public sealed class ApplicationSprite : Sprite
    {
        // import TestFlashAnonymousType.ApplicationSprite.__f__AnonymousType_44_0_1;



        // X:\jsc.svn\examples\actionscript\Test\TestAnonymousType\TestAnonymousType\Class1.cs
        //        V:\web\TestFlashAnonymousType\ApplicationSprite.as(38): col: 31 Error: Call to a possibly undefined method __f__AnonymousType_44_0_1.
        //                    field1.text = new __f__AnonymousType_44_0_1("hello there").toString();
        //                              ^
        //V:\web\TestFlashAnonymousType\ApplicationSprite.as(15): col: 52 Error: Definition TestFlashAnonymousType.ApplicationSprite:__f__AnonymousType_44_0_1 could not be found.
        //    import TestFlashAnonymousType.ApplicationSprite.__f__AnonymousType_44_0_1;
        //                                                   ^

        public ApplicationSprite()
        {
            var x = new TextField
            {

                // if i change one line,
                // could we let the running apps know of the change?
                text = new { foo = "hello there" }.ToString()
            };

            // would the new know which sprite is creating it?
            x.AttachToSprite();

        }

    }
}
