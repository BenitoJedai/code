using ScriptCoreLib;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared;

[assembly:
ScriptResources("assets/NatureBoy/alpha"),

ScriptResources("fx/building"),
ScriptResources("fx/vehicle"),
ScriptResources("fx/explosion"),

ScriptResources("fx/building_1"),
ScriptResources("fx/building_2"),
ScriptResources("fx/building_3"),
ScriptResources("fx/building_4"),
ScriptResources("fx/explosion_1"),
ScriptResources("fx/harvester_1"),
ScriptResources("fx/tank_1"),
ScriptResources("fx/tank_2"),
ScriptResources("fx/veh_1"),
ScriptResources("fx/tree_1"),
ScriptResources("fx/bg"),
ScriptResources("fx/css"),
ScriptResources("fx/gfx/logo"),
ScriptResources("sfx"),
//ScriptResources("swf")
]

namespace gameclient.source.js.fx
{

    [Script]
    public static class Settings
    {
        [Script]
        public class Value
        {
            public string FileName;

            public string Title;

            public int Width;

            public int Height;

            public int FirstFrame;
            public int LastFrame;

            public Point Size
            {
                get { return new Point(Width, Height); }
            }

            public void ShowFrame(IHTMLElement e, int index)
            {
                e.style.backgroundColor = Color.Transparent;
                e.style.backgroundImage = "url(" + this.FileName + ")";
                e.style.backgroundPosition =  -(this.Width * index) + "px 0px";
            }
        }

        public static Value ConstructionYard = new Value {
            FileName = "fx/vehicle/veh_cy.png",
            Title = "ConstructionYard",
            Width = 48,
            Height = 48,
            FirstFrame = 0,
            LastFrame = 31
        };
    }

}