using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Media
{
    [Script(Implements = typeof(global::System.Media.SystemSounds))]
    internal class __SystemSounds
    {
        static IHTMLAudio InternalBeep = new IHTMLAudio { src = "assets/ScriptCoreLib/Windows Ding.wav", autobuffer = true };

        public static SystemSound Beep
        {
            get
            {
                return (SystemSound)(object)new __SystemSound
                {
                    InternalPlay =
                        delegate
                        {
                            var x = (IHTMLAudio)InternalBeep.cloneNode(true);

                            InternalBeep.play();

                            InternalBeep = x;
                        }
                };
            }
        }
    }
}
