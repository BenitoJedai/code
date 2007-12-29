using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

using ScriptCoreLib.JavaScript.Cards;
using System.Collections.Generic;

namespace CardGames.source.js.Controls
{
    [Script]
    public class BlackJack// : SpawnControlBase
    {
        public const string Alias = "fx.BlackJack";

        static IHTMLImage BackgroundImage
        {
            get
            {
                return new IHTMLImage("assets/CardGames/images/felt.gif");
            }
        }

        CardDeck MyDeck = new CardDeck
        {
            GfxPath = "assets/CardGames/cards"
        };


        //List<CardStack> TempStacks;
        List<CardStack> GoalStacks;
        List<CardStack> PlayStacks;


     
        CardGameSoundManager sounds = new CardGameSoundManager();


        public BlackJack(IHTMLElement spawn)
           // : base(spawn)
        {

            System.Console.WriteLine("--- BlackJack ---");

            BackgroundImage.ToDocumentBackground();

            System.Console.WriteLine("adding card infos... ");

            MyDeck.UnusedCards.Add(CardInfo.FullDeck());

           
          
        }



    }


}


