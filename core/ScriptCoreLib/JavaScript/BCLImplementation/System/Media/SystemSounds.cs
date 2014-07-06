using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Media
{
    // http://referencesource.microsoft.com/#System/sys/system/Media/SystemSounds.cs
    [Script(Implements = typeof(global::System.Media.SystemSounds))]
    internal class __SystemSounds
    {

        public static SystemSound Beep
        {
            get
            {
                IHTMLAudio InternalBeep = new IHTMLAudio { src = "assets/ScriptCoreLib/Windows Ding.wav", autobuffer = true };

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
