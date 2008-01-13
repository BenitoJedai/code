using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.net;

namespace MySoundDemo.ActionScript
{
    [Script]
    static class Extensions
    {
        public static SoundChannel PlaySoundLoop(this string e)
        {
            return new Sound(new URLRequest(e)).play(0, 999);
        }
    }
}
