using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

using ScriptCoreLib.JavaScript.Cards;
using System.Collections.Generic;
using System.ComponentModel;

namespace CardGames.source.js.Controls
{
    [Script, ScriptApplicationEntryPoint]
    public class Solitare //: SpawnControlBase
    {
        //public const string Alias = "fx.Solitare";

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
        BindingList<CardStack> GoalStacks;
        BindingList<CardStack> PlayStacks;


     
        CardGameSoundManager sounds = new CardGameSoundManager();



        static Solitare()
        {
            typeof(Solitare).SpawnTo(i => new Solitare(i));
        }

        public Solitare(IHTMLElement spawn)
            //: base(spawn)
        {

            System.Console.WriteLine("--- solitare ---");

            BackgroundImage.ToDocumentBackground();

            System.Console.WriteLine("adding card infos... ");

            MyDeck.UnusedCards.Add(CardInfo.FullDeck());

            MyDeck.Stacks.ListChanged +=
                (sender, args) =>
                {
                    if (args.ListChangedType == ListChangedType.ItemAdded)
                    {
                        var s = MyDeck.Stacks[args.NewIndex];
                        s.SetBackground(MyDeck.GfxPath + "/spider.empty.png");
                    }
                };

            System.Console.WriteLine("creating stacklists... ");

            PlayStacks = MyDeck.CreateStackList();
            //TempStacks = MyDeck.CreateStackList();
            GoalStacks = MyDeck.CreateStackList();
            
           
            //var king = new KingIcon(new Point(500, 100));
            //var status = new StatusInfo();

            //status.MoveTo(new Point(500, 20));
            //status.Update();

            #region rules
            MyDeck.ApplyCardRules += delegate(Card e)
            {
            };
            #endregion




            System.Console.WriteLine("creating goalstack... ");

            GoalStacks.Add(
                new CardStack(new Point(600, 100)),
                new CardStack(new Point(700, 100)),
                new CardStack(new Point(800, 100)),
                new CardStack(new Point(900, 100))
            );

            System.Console.WriteLine("creating playstack... ");

            PlayStacks.Add(
                new CardStack(new Point(150, 240), MyDeck.FetchCards(0)),
                new CardStack(new Point(250, 240), MyDeck.FetchCards(1)),
                new CardStack(new Point(350, 240), MyDeck.FetchCards(2)),
                new CardStack(new Point(450, 240), MyDeck.FetchCards(3)),
                new CardStack(new Point(550, 240), MyDeck.FetchCards(4)),
                new CardStack(new Point(650, 240), MyDeck.FetchCards(5)),
                new CardStack(new Point(750, 240), MyDeck.FetchCards(6))
                );

          
        }



    }


}


