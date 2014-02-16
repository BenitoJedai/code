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

            Action goo = async delegate
            {



                Console.WriteLine("hi from goo");

                new TextField { text = "hi from goo" }.AttachTo(this);
            };
            goo();
        }
    }
}
