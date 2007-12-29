using ScriptCoreLib;



[assembly: 
    ScriptResources(NatureBoy.js.Zak.Assets.Images),
    ScriptResources("assets/NatureBoy/alpha"),
    ScriptResources("assets/NatureBoy/back"),
    ScriptResources("assets/NatureBoy/data/LostInTime"),
]

namespace NatureBoy.js
{
    namespace Zak
    {
        [Script]
        public static class Assets
        {
            public const string Images = "assets/NatureBoy/zak/images";
        }

    }
}