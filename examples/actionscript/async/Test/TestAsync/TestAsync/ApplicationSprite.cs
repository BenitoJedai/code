using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System;

namespace TestAsync
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            //V:\web\TestAsync\ApplicationSprite_ctor_b__0_d__2.as(22): col: 39 Error: Implicit coercion of a value of type TestAsync:ApplicationSprite_ctor_b__0_d__2 to an unrelated type Array.

            //            ref_ctor_b__0_d__20 = new ApplicationSprite_ctor_b__0_d__2()
            //            var ref_ctor_b__0_d__20:Array = [];
            //            ref_ctor_b__0_d__20 = new ApplicationSprite_ctor_b__0_d__2()
            //;

            //            ref_ctor_b__0_d__20[0] = this;
            //            ApplicationSprite_ctor_b__0_d__2__MoveNext_06000008.__forwardref_9ca8be72_0600000c(ref_ctor_b__0_d__20);


            Action goo = async delegate
            {



                Console.WriteLine("hi from goo");
            };
            goo();
        }
    }
}
