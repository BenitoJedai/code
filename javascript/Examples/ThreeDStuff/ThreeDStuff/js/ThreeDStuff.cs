using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ThreeDStuff.js
{
    [Script, ScriptApplicationEntryPoint]
    class ThreeDStuff
    {
        public ThreeDStuff()
        {
            var a = typeof(ThreeDStuff).Assembly;

            foreach (var v in new[] {
                        typeof(Isometric),
                        typeof(IsometricRotating)}
                    )
            {
                new IHTMLDiv(new IHTMLAnchor(v.Name + ".htm", v.Name)).AttachToDocument();
            }


        }

        static ThreeDStuff()
        {
            typeof(ThreeDStuff).SpawnTo(i => new ThreeDStuff());
        }
    }
}
