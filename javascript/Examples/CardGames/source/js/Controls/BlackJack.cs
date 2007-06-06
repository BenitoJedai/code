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
    public class BlackJack : SpawnControlBase
    {
        public const string Alias = "fx.BlackJack";

        static IHTMLImage BackgroundImage
        {
            get
            {
                return new IHTMLImage("fx/felt.gif");
            }
        }

        CardDeck MyDeck = new CardDeck();

        //List<CardStack> TempStacks;
        List<CardStack> GoalStacks;
        List<CardStack> PlayStacks;


     
        CardGameSoundManager sounds = new CardGameSoundManager();


        public BlackJack(IHTMLElement spawn)
            : base(spawn)
        {

            Console.Log("--- BlackJack ---");

            BackgroundImage.ToDocumentBackground();

            Console.Log("adding card infos... ");

            MyDeck.UnusedCards.Add(CardInfo.FullDeck());

           
          
        }



    }


}


