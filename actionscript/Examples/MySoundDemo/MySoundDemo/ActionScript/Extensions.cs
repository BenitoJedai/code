using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript;

namespace MySoundDemo.ActionScript
{
    [Script]
    static class Extensions
    {
        [Script(OptimizedCode = "return new c();")]
        public static object CreateType(this Class c)
        {
            return default(object);
        }

        
        public static SoundChannel PlaySoundLoop(this string e)
        {
            return new Sound(new URLRequest(e)).play(0, 999);
        }
    }
}
