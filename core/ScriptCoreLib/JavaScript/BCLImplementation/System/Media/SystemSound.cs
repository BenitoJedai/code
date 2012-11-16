using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Media
{
    [Script(Implements = typeof(global::System.Media.SystemSound))]
    internal class __SystemSound
    {
        public Action InternalPlay;

        public void Play()
        {
            if (InternalPlay != null)
                InternalPlay();
        }
    }
}
