using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly:
    ScriptResources(ThreeDStuff.js.Assets.ThreeDStuff),
    ScriptResources(ThreeDStuff.js.Assets.Tycoon_Bus),
]

namespace ThreeDStuff.js
{
    [Script]
    public static class Assets
    {
        public const string Tycoon_Bus = "assets/Tycoon_Bus";
        public const string ThreeDStuff = "assets/ThreeDStuff";
    }
}
