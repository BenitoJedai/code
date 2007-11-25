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
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Drawing;

using ScriptCoreLib.JavaScript.Cards;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;

namespace CardGames.source.js.Controls
{
    [Script, ScriptApplicationEntryPoint(IsClickOnce=true)]
    public class FreeCell// : SpawnControlBase
    {
        //public const string Alias = "fx.FreeCell";

        static IHTMLImage BackgroundImage
        {
            get
            {
                return new IHTMLImage("fx/felt.gif");
            }
        }

        CardDeck MyDeck = new CardDeck();

        BindingList<CardStack> TempStacks;
        BindingList<CardStack> GoalStacks;
        BindingList<CardStack> PlayStacks;


        [Script]
        class KingIcon
        {
            public readonly IHTMLImage Left = new IHTMLImage("fx/kingleft.bmp");
            public readonly IHTMLImage Rigth = new IHTMLImage("fx/kingbitm.bmp");
            public readonly IHTMLImage Smile = new IHTMLImage("fx/kingsmil.bmp");

            public readonly IHTMLDiv Control = new IHTMLDiv();

            public KingIcon(Point p)
                : this()
            {
                MoveTo(p);
            }

            public bool IsRight;
            public bool IsSmile;

            public KingIcon()
            {

                Control.style.SetSize(32, 32);
                Control.style.border = "1px solid #00FF00";

                Left.ToBackground(Control);

                Native.Document.body.appendChild(Control);

                Native.Document.onmousemove +=
                    delegate(IEvent e)
                    {
                        IsRight = e.CursorX > Position.X;

                        Update();
                    };
            }

            public void Update()
            {
                if (IsSmile)
                    Smile.ToBackground(Control);
                else
                {
                    if (IsRight)
                        Rigth.ToBackground(Control);
                    else
                        Left.ToBackground(Control);
                }
            }

            public Point Position;

            public void MoveTo(Point p)
            {
                Position = p;

                Control.SetCenteredLocation(p);
            }

        }

        [Script]
        public class StatusInfo : StatusPanel
        {
            public int Moves;
            public int CardsLeft;

            public bool Loading;

            public override void Update()
            {

                Control.innerHTML = "cards left: " + CardsLeft + ", moves: " + Moves;

                Control.appendChild(SoundSettingDiv);
            }
        }

        CardGameSoundManager sounds = new CardGameSoundManager();

        Cookie Storage = new Cookie(typeof(FreeCell).Name);

        public bool UseNoValidationCheat = false;

        static FreeCell()
        {
            typeof(FreeCell).SpawnTo(i => new FreeCell(i));
        }


        public FreeCell() : this(null)
        {
        }

        public FreeCell(IHTMLElement spawn)
        //: base(spawn)
        {

            Console.Log("--- freecell ---");

            BackgroundImage.ToDocumentBackground();

            Console.Log("adding card infos... ");

            MyDeck.UnusedCards.Add(CardInfo.FullDeck());

            var images = new List<IHTMLImage>();

            foreach (CardInfo v in MyDeck.UnusedCards.ToArray())
            {
                images.Add(v.ToImage("fx/cards"));
            }

            var king = new KingIcon(new Point(500, 100));



            //Timer.While(
            //    delegate
            //    {
            //        var ready = true;

            //        foreach (IHTMLImage image in images.ToArray())
            //        {
            //            if (!image.complete)
            //            {
            //                Console.WriteLine(image.src);

            //                ready = false;
            //                break;
            //            }
            //        }

            //        return !ready;
            //    }
            //    ,
            //    delegate
            //    {
            var status = new StatusInfo();

            status.Update();

            MyDeck.Stacks.ListChanged +=
                (sender, args) =>
                {

                    if (args.ListChangedType == ListChangedType.ItemAdded)
                    {
                        var s = MyDeck.Stacks[args.NewIndex];

                        s.SetBackground(MyDeck.GfxPath + "/spider.empty.png");
                    }
                };

            Console.Log("creating stacklists... ");

            PlayStacks = MyDeck.CreateStackList();
            TempStacks = MyDeck.CreateStackList();
            GoalStacks = MyDeck.CreateStackList();




            var usesound_cookie = Storage["usesound"];

            status.MoveTo(new Point(500, 20));
            status.Update();
            status.SoundSettingChanged +=
                delegate
                {
                    sounds.Enabled = status.UseSounds;
                    usesound_cookie.BooleanValue = status.UseSounds;
                };

            status.UseSounds = usesound_cookie.BooleanValue;

            #region rules
            MyDeck.ApplyCardRules += delegate(Card e)
            {
                e.Enabled = true;
                e.Moved +=
                    delegate
                    {
                        status.Moves++;
                        status.Update();


                    };

                #region automove


                e.Drag.MiddleClick +=
                    delegate
                    {
                        TryAutoMove(e);
                    };

                e.DoubleClick +=
                    delegate
                    {
                        TryAutoMove(e);
                    };
                #endregion

                e.Drag.DragStop +=
                    delegate
                    {
                        sounds.PlaySoundDrop();
                    };

                e.Drag.DragStart +=
                    delegate
                    {
                        sounds.PlaySoundDrag();

                    };

                // rules for starting a drag
                e.Drag.DragStartValidate +=
                    delegate(Predicate p)
                    {
                        if (UseNoValidationCheat)
                            return;

                        // cannot remove cards from goal stack
                        if (GoalStacks.Contains(e.CurrentStack))
                        {
                            p.Value = false;

                            return;
                        }

                        // cannot drag a pile of cards
                        if (e.HasStackedCards)
                            p.Value = false;
                    };

                // rules for ending a drag
                e.ValidateDragStop +=
                    delegate(Predicate<CardStack> p)
                    {
                        if (UseNoValidationCheat)
                            return;

                        if (IsStackTypeAndDoesNotFit(e, PlayStacks, p, IsFitForPlayStack))
                        {
                            p.Target = null;
                        }
                        else if (IsStackTypeAndDoesNotFit(e, TempStacks, p, IsFitForTempStack))
                        {
                            p.Target = null;
                        }
                        else if (IsStackTypeAndDoesNotFit(e, GoalStacks, p, IsFitForGoalStack))
                        {
                            p.Target = null;
                        }


                    };


            };
            #endregion

            GoalStacks.ListChanged +=
                (sender0, args0) =>
                {
                    if (args0.ListChangedType == ListChangedType.ItemAdded)
                    {
                        var s = GoalStacks[args0.NewIndex];

                        s.Cards.ListChanged +=

                            (sender1, args1) =>
                            {
                                if (args1.ListChangedType == ListChangedType.ItemAdded)
                                {
                                    var u = s.Cards[args1.NewIndex];
                                    // hide the previous, as we never need it to be seen again
                                    status.CardsLeft--;
                                    status.Update();

                                    #region end suit
                                    if (u.Info.Rank == CardInfo.RankEnum.Rank2)
                                    {
                                        s.Enabled = false;

                                        if (status.CardsLeft == 0)
                                        {
                                            king.IsSmile = true;
                                            king.Update();


                                            sounds.PlaySoundWin();
                                        }

                                        // check for victory?
                                    }
                                    #endregion

                                }
                            };

                        // each card on top of eachother
                        s.CardMargin *= 0;
                    }
                };

            Console.Log("creating tempstack... ");

            TempStacks.Add(
                new CardStack(new Point(100, 100)),
                new CardStack(new Point(200, 100)),
                new CardStack(new Point(300, 100)),
                new CardStack(new Point(400, 100))
            );


            Console.Log("creating goalstack... ");

            GoalStacks.Add(
                new CardStack(new Point(600, 100)),
                new CardStack(new Point(700, 100)),
                new CardStack(new Point(800, 100)),
                new CardStack(new Point(900, 100))
            );

            Console.Log("creating playstack... ");

            PlayStacks.Add(
                new CardStack(new Point(150, 240), MyDeck.FetchCards(7)),
                new CardStack(new Point(250, 240), MyDeck.FetchCards(7)),
                new CardStack(new Point(350, 240), MyDeck.FetchCards(7)),
                new CardStack(new Point(450, 240), MyDeck.FetchCards(7)),
                new CardStack(new Point(550, 240), MyDeck.FetchCards(6)),
                new CardStack(new Point(650, 240), MyDeck.FetchCards(6)),
                new CardStack(new Point(750, 240), MyDeck.FetchCards(6)),
                new CardStack(new Point(850, 240), MyDeck.FetchCards(6))
                );

            Console.Log("updating status... ");

            status.Moves = 0;
            status.CardsLeft = MyDeck.Cards.Count;
            status.Update();
            //}, 300
            //);

        }

        private void TryAutoMove(Card c)
        {
            Console.Log("finding free move... ");

            // try to send to a location
            if (c.NextCard != null)
                return;

            // cannot play cards in goal pile
            if (GoalStacks.Contains(c))
                return;

            Console.Log("will look for goal move... ");

            if (MyDeck.TryToFitToAnyStack(c, GoalStacks, IsFitForGoalStack))
                return;

            Console.Log("will look for play move... ");

            if (MyDeck.TryToFitToAnyStack(c, PlayStacks, IsFitForPlayStack))
                return;

            // still on playground? try temp
            if (PlayStacks.Contains(c))
            {
                Console.Log("will look for temp move... ");

                if (MyDeck.TryToFitToAnyStack(c, TempStacks, IsFitForTempStack))
                    return;
            }

            sounds.PlaySoundNoMoveFound();

            return;
        }


        public static bool IsStackTypeAndDoesNotFit(Card c, IEnumerable<CardStack> s, CardStack p, EventHandler<Predicate<CardStack, Card>> h)
        {
            if (s.Count(i => i == p) > 0)
            {
                if (!Predicate.Invoke(p, c, h))
                    return true;
            }

            return false;
        }






        public void IsFitForPlayStack(Predicate<CardStack, Card> p)
        {
            // temp stack cannot handle more than one card

            var s = p.TargetIn;
            var c = p.TargetOut;

            if (s.Cards.Count > 0)
            {
                if (s.Cards.Last().Info.Rank + 1 != c.Info.Rank)
                {
                    p.Value = false;

                    return;
                }
            }

            p.Value = true;

            return;
        }


        public void IsFitForTempStack(Predicate<CardStack, Card> p)
        {
            // temp stack cannot handle more than one card

            var s = p.TargetIn;
            var c = p.TargetOut;

            if (s.Cards.Count > 0)
            {
                p.Value = false;

                return;
            }

            p.Value = true;

            return;
        }



        public void IsFitForGoalStack(Predicate<CardStack, Card> p)
        {
            var s = p.TargetIn;
            var c = p.TargetOut;

            // first card could be any suit
            if (s.Cards.Count > 0)
            {
                // must be of the same suit
                if (s.Cards.Last().Info.Suit != c.Info.Suit)
                {
                    p.Value = false;

                    return;
                }
                else
                {
                    // one lesser then the previous

                    if (s.Cards.Last().Info.Rank + 1 != c.Info.Rank)
                    {
                        p.Value = false;

                        return;
                    }


                }
            }
            else
            {
                // but only ace

                if (c.Info.Rank != CardInfo.RankEnum.RankAce)
                {
                    p.Value = false;

                    return;
                }
            }

            p.Value = true;

        }


    }


}


