using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Controls.NatureBoy;
using ScriptCoreLib.Shared;

[assembly:
    ScriptResources(Assets.alpha),
    ScriptResources(Assets.dude5),
    ScriptResources(Assets.dude6),
]
namespace ScriptCoreLib.JavaScript.Controls.NatureBoy
{
    [Script]
    public static class Assets
    {
        public const string alpha = "assets/Controls/NatureBoy/alpha";
        public const string dude5 = "assets/Controls/NatureBoy/dude5";
        public const string dude6 = "assets/Controls/NatureBoy/dude6";
    }
}
