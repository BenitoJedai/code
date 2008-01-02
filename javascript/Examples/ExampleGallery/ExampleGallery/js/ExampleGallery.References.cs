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
                    typeof(HulaGirl.source.js.Controls.HulaGirl),
                    typeof(NumberGuessingGame.source.js.Controls.NumberGuessingGame),
                    typeof(SimpleBankPage.js.SimpleBankPage),
                    typeof(GMapsClone.source.js.Controls.GoogleMapsControl),
                    typeof(OrcasVisualBasicScriptApplication.JavaScript.OrcasVisualBasicScriptApplication),
                    typeof(OrcasScriptApplication.js.OrcasScriptApplication),
                    typeof(SubSquare.source.js.Controls.SubSquareControl),
                    typeof(SimpleRollover.js.SimpleRollover),
                    typeof(SimpleFilmstrip.js.SimpleFilmstrip),
                    typeof(HotPolygon.js.HotPolygon),
                    typeof(DockMaster.source.js.DockMaster),
                    typeof(GGearAlpha.js.GoogleGearsAdvanced),
                    typeof(MonthSchedule.js.MonthSchedule),
                    typeof(GameOfLife.js.GameOfLife),
                    typeof(VectorExample.js.VectorExample),
                    typeof(FormsExample.js.FormsExampleApplication),
                    typeof(FormsExample.VisualBasic.JavaScript.FormsExampleVBApplication),
                    typeof(Mahjong.js.MahjongGame),
                    typeof(RetroCanvas.js.AmiNET110.AmiNET110),
                    typeof(NatureBoyTestPad.js.NatureBoyTestPad)
                };
            }
        }


    }

}
