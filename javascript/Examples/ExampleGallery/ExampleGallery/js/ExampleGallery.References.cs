using System;
using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;


namespace ExampleGallery.js
{
    public partial class ExampleGallery
    {

        static public Type[] Applications
        {
            get
            {
                return new[]
                {
                    typeof(ThreeDStuff.js.ThreeDStuff),
                    typeof(ConsoleWorm.js.ConsoleWorm),
                    typeof(ButterFly.source.js.Butterfly),
                    typeof(SpaceInvaders.source.js.Controls.SpaceInvadersGame),
                    typeof(LightsOut.js.LightsOut2FullScreen),
                    typeof(TextEditorDemo.source.js.Controls.TextEditorDemo),
                    typeof(CardGames.source.js.Controls.FreeCell),
                    typeof(MouseWheel.js.MouseWheel),
                    typeof(ImageZoomer.js.ImageZoomer),
                    typeof(TextRotator.js.TextRotator2),
                    typeof(TextScreenSaver.js.TextScreenSaver),
                    typeof(LinqToObjects.source.js.Controls.LinqToObjects),
                };
            }
        }


    }

}
