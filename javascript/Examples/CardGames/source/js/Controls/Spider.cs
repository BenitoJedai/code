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

namespace CardGames.source.js.Controls
{
    [Script]
    public class Spider : SpawnControlBase
    {
        public const string Alias = "fx.Spider";

        #region status
        [Script]
        public class StatusInfo : StatusPanel
        {
            public int Moves;
            public int Score;

            public override void Update()
            {
                Control.innerHTML = "score: " + Score + ", moves: " + Moves;

                Control.appendChild(SoundSettingDiv);
            }

          
        }

        #endregion

        static IHTMLImage BackgroundImage
        {
            get
            {
                return new IHTMLImage("fx/felt.gif");
            }
        }

        CardDeck MyDeck = new CardDeck();
        CardGameSoundManager MySounds = new CardGameSoundManager();

        //List<CardStack> TempStacks = new List<CardStack>();
        List<CardStack> DealStacks;
        List<CardStack> PlayStacks;

        List<CardStack> DeadStacks;

        #region levels
        static CardInfo.SuitEnum[] LevelEasy = new CardInfo.SuitEnum[]
            {
                CardInfo.SuitEnum.Club,
                CardInfo.SuitEnum.Club,
                CardInfo.SuitEnum.Club,
                CardInfo.SuitEnum.Club,
            };

        static CardInfo.SuitEnum[] LevelMedium = new CardInfo.SuitEnum[]
            {
                CardInfo.SuitEnum.Club,
                CardInfo.SuitEnum.Heart,
                CardInfo.SuitEnum.Club,
                CardInfo.SuitEnum.Heart,
            };

        static CardInfo.SuitEnum[] LevelHard = new CardInfo.SuitEnum[]
            {
                CardInfo.SuitEnum.Club,
                CardInfo.SuitEnum.Heart,
                CardInfo.SuitEnum.Spade,
                CardInfo.SuitEnum.Diamond,
            };
        #endregion

        public enum SpiderRankEnum
        {
            Unknown,

            RankKing,
            RankQueen,
            RankJack,
            Rank10,
            Rank9,
            Rank8,
            Rank7,
            Rank6,
            Rank5,
            Rank4,
            Rank3,
            Rank2,
            RankAce,

        }

        static void RankComparer(Predicate<CardInfo.RankEnum, CardInfo.RankEnum> p)
        {
            p.Value = p.TargetIn == p.TargetOut;
        }

        static void RankConverter(ConvertTo<CardInfo.RankEnum, int> c)
        {
            c.TargetInComparer = RankComparer;

            c[CardInfo.RankEnum.RankKing] = (int)SpiderRankEnum.RankKing;
            c[CardInfo.RankEnum.RankQueen] = (int)SpiderRankEnum.RankQueen;
            c[CardInfo.RankEnum.RankJack] = (int)SpiderRankEnum.RankJack;
            c[CardInfo.RankEnum.Rank10] = (int)SpiderRankEnum.Rank10;
            c[CardInfo.RankEnum.Rank9] = (int)SpiderRankEnum.Rank9;
            c[CardInfo.RankEnum.Rank8] = (int)SpiderRankEnum.Rank8;
            c[CardInfo.RankEnum.Rank7] = (int)SpiderRankEnum.Rank7;
            c[CardInfo.RankEnum.Rank6] = (int)SpiderRankEnum.Rank6;
            c[CardInfo.RankEnum.Rank5] = (int)SpiderRankEnum.Rank5;
            c[CardInfo.RankEnum.Rank4] = (int)SpiderRankEnum.Rank4;
            c[CardInfo.RankEnum.Rank3] = (int)SpiderRankEnum.Rank3;
            c[CardInfo.RankEnum.Rank2] = (int)SpiderRankEnum.Rank2;
            c[CardInfo.RankEnum.RankAce] = (int)SpiderRankEnum.RankAce;
        }

        StatusInfo MyStatus = new StatusInfo();

        public bool CheatNoValidation = false;
        public bool CheatReveal = false;

        Cookie Storage = new Cookie(Alias);

        int BottomY
        {
            get
            {
                return Native.Window.Height - CardInfo.Height / 2 - 4;
            }
        }

        public Spider(IHTMLElement spawn) : base(spawn)
        {
            MyStatus.Visible = false;

            Console.Log("--- spider ---");

            BackgroundImage.ToDocumentBackground();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            var level = LevelEasy;

            if (Native.Document.location.search == "?hard")
                level = LevelHard;

            if (Native.Document.location.search == "?medium")
                level = LevelMedium;

            var top = CardInfo.Height / 2 + 12;
            var bottom = 500;

            

            MyStatus.MoveTo(new Point(500, bottom));
            var usesound_cookie = Storage["usesound"];

            MyStatus.Update();
            MyStatus.SoundSettingChanged +=
                delegate
                {
                    MySounds.Enabled = MyStatus.UseSounds;
                    usesound_cookie.BooleanValue = MyStatus.UseSounds;
                };

            MyStatus.UseSounds = usesound_cookie.BooleanValue;

            Native.Window.onresize +=
                delegate
                {
                    MyStatus.MoveTo(new Point(500, BottomY));

                    foreach (CardStack v in DealStacks.ToArray())
                    {
                        v.MoveTo(new Point(v.Position.X, BottomY));
                    }

                };

            Console.Log("adding card infos... ");

            MyDeck.UnusedCards.Add(CardInfo.By(2, level));

            MyDeck.RankConverter = RankConverter;

            Console.Log("creating stacklists... ");

            DealStacks = MyDeck.CreateStackList();
            PlayStacks = MyDeck.CreateStackList();
            DeadStacks = MyDeck.CreateStackList();

            PlayStacks.ItemAdded +=
                delegate(CardStack s)
                {
                    s.SetBackground(MyDeck.GfxPath + "/spider.empty.png");
                };

            DeadStacks.ItemAdded +=
                 delegate(CardStack s)
                 {
                     s.Control.Hide();

                     s.CardMargin = new Point(2, 0);

                     s.Cards.ItemAdded +=
                         delegate(Card c)
                         {
                             c.Enabled = false;
                         };
                 };

            DealStacks.ItemAdded +=
                delegate (CardStack s)
                {
                    s.Control.Hide();
                };

      

            #region drag rules
            MyDeck.ApplyCardRules += delegate(Card c)
            {
                c.Info.CustomBackground = "spider";
                c.Update();

                c.Moved +=
                    delegate
                    {
                        if (!this.MyStatus.Ready)
                            return;

                        //AddScore(1);

                        // check for good suit

                        var s = c.CurrentStack;

                        CheckForGoodSuit(s);

                    };

                #region sounds
                c.Drag.DragStop +=
                    delegate
                    {
                        MySounds.PlaySoundDrop();
                    };

                c.Drag.DragStart +=
                    delegate
                    {
                        MySounds.PlaySoundDrag();

                    };
                #endregion

                #region automove
                c.DoubleClick += delegate
                {
                    TryAutoMove(c);
                };

                c.Drag.MiddleClick += delegate
                {
                    TryAutoMove(c);
                };

               
                #endregion

                // rules for starting a drag
                c.Drag.DragStartValidate +=
                    delegate(Predicate p)
                    {
                        if (!MyStatus.Ready)
                        {
                            p.Value = false;

                            return;
                        }

                        if (CheatNoValidation)
                            return;

                        p.Value = Predicate.Invoke(c, IsDraggableFormPlayStack);


                    };

                // rules for ending a drag
                c.ValidateDragStop +=
                    delegate(Predicate<CardStack> p)
                    {
                        if (CheatNoValidation)
                            return;


                        if (Predicate.Invoke(p.Target, c, IsFitForPlayStack))
                        {
                            return;
                        }

                        p.Target = null;
                    };
            };
            #endregion

         

            Console.Log("creating playstack... ");

            PlayStacks.Add(
                new CardStack(new Point(50, top), MyDeck.FetchCards(5)),
                new CardStack(new Point(150, top), MyDeck.FetchCards(5)),
                new CardStack(new Point(250, top), MyDeck.FetchCards(5)),
                new CardStack(new Point(350, top), MyDeck.FetchCards(5)),
                new CardStack(new Point(450, top), MyDeck.FetchCards(4)),
                new CardStack(new Point(550, top), MyDeck.FetchCards(4)),
                new CardStack(new Point(650, top), MyDeck.FetchCards(4)),
                new CardStack(new Point(750, top), MyDeck.FetchCards(4)),
                new CardStack(new Point(850, top), MyDeck.FetchCards(4)),
                new CardStack(new Point(950, top), MyDeck.FetchCards(4))
                );

            PlayStacks.ForEach(
                delegate(CardStack s)
                {
                    s.Cards.ItemRemoved +=
                        delegate
                        {
                            if (MyStatus.Ready)
                                s.RevealLastCard();
                        };


                    s.CardsMovedToStack +=
                        delegate
                        {
                            if (MyStatus.Ready)
                            {
                                MyStatus.Score--;
                                MyStatus.Moves++;

                                MyStatus.Update();
                            }
                        };
                }
            );

            

            DealStacks.ItemAdded +=
                delegate(CardStack s)
                {
                    
                    s.CardMargin *= 0;
                    s.Update();

                    s.Click +=
                        delegate
                        {
                            if (DealStacks.Contains(s))
                            {
                                if (MyStatus.Ready)
                                    DealRow(null);
                            }
                            else
                            {
                                Console.LogError("whoops wrong stack click ");

                            }
                        };
                };

            Console.Log("creating dealstack... ");

            var dealpoint = new Point(860, bottom);

            while (MyDeck.UnusedCards.Count > 0)
            {
                DealStacks.Add(
                    new CardStack(dealpoint, MyDeck.FetchCards(10))
                );

                dealpoint -= new Point(10, 0);
            }

            DeadStacks.Add(new CardStack(new Point(150, bottom)));

            if (CheatReveal)
                DoCheatRevealAllCards();


            MyDeck.ToConsole();




            DealRow(
                delegate
                {

                    MyStatus.Ready = true;
                    MyStatus.Score = 500;
                    MyStatus.Moves = 0;
                    MyStatus.Visible = true;
                    MyStatus.Update();
                }
            );
        }

        private void CheckForGoodSuit(CardStack s)
        {
            var gs = GetGoodSuit(s);

            if (gs != null)
            {
                Console.Log("got good suit...");

                MyStatus.Score += 100;
                MyStatus.Update();

                var sxx = DeadStacks.Last;

                MyStatus.Ready = false;

                CardStack.CardByCard(
                    delegate(Pair<Card, EventHandler> p)
                    {
                        if (p.A == null)
                        {
                            MyStatus.Ready = true;


                            DeadStacks.Add(
                                new CardStack(sxx.Position + new Point(0, 4))
                            );

                            CheckForWin();

                            s.RevealLastCard();
                        }
                        else
                        {
                            MySounds.PlaySoundDeal();

                            p.A.AttachToStack(sxx);

                            p.A.BringToFront();

                            sxx.MoveSlow(p.A, p.B);
                        }
                    }
                    , gs);
            }
        }

        private void CheckForWin()
        {
            if (DeadStacks.Count > 8)
            {
                MyStatus.Control.style.color = Color.Red;

                MySounds.PlaySoundWin();
            }
        }



        private void DoCheatRevealAllCards()
        {
            MyDeck.Cards.ForEach(
                delegate(Card c)
                {
                    if (PlayStacks.Contains(c))
                    {
                        c.Enabled = true;
                    }
                }
            );
        }

        private Card[] GetGoodSuit(CardStack s)
        {
            if (!PlayStacks.Contains(s))
                return null;

            List<Card> xx = s.Cards[s.Cards.Count - 13, 13];

            if (xx.Count != 13)
            {
                return null;
            }

            var p = xx.First;
            var z = xx.Last;


            if (p.ModifiedRank != (int)SpiderRankEnum.RankKing)
            {
                Console.Log("first isnt king...");

                return null;

            }
            if (z.ModifiedRank != (int)SpiderRankEnum.RankAce)
            {
                Console.Log("last isnt ace...");

                return null;
                
            }

       

            Console.Log("checking for good suit...");

            var r = true;

            var arr = xx.ToArray();

            Card u = null;

            foreach (Card c in arr)
            {
                if (u != null)
                {

                    if (u.Info.Suit != c.Info.Suit)
                    {
                        Console.Log("bad suit...");


                        arr = null;
                        break;
                    }

                    if (u.IsParentRankOf(c))
                    {
                        Console.Log("rank mismatch");

                        arr = null;
                        break;
                    }
                }

                u = c;
            }

            
            return arr;
        }


        private static void IsDraggableFormPlayStack(Predicate<Card> p)
        {
            var c = p.Target;

            p.Value = true;


            foreach (Card v in c.StackedCards)
            {
                var above = v.PreviousCard;

                if (v.Info.Suit != above.Info.Suit)
                {
                    p.Value = false;

                    break;
                }

                if (v.ModifiedRank != above.ModifiedRank + 1)
                {
                    p.Value = false;

                    break;
                }
            }
        }


        public void DealRow(EventHandler done)
        {

            var DealingStack = DealStacks.Pop();

            if (DealingStack == null)
            {
                Console.Log("whoops, no more stacks left, but a click was made?");

                return;
            }

            if (PlayStacks.Find(( p) => p.Value = p.Target.Cards.Count == 0) != null)
            {
                this.MySounds.PlaySoundNoMoveFound();

                return;
            }

            MyStatus.Ready = false;
            
            Console.Log("dealing new row of cards...");

            //Console.Log("deal last stack of " + DealingStack.Cards.Count + " to " + PlayStacks.Count + " stacks");

            if (DealingStack.Cards.Count == PlayStacks.Count)
            {
                // AddScore(- PlayStacks.Count);

                var ToBeAnimated = new List<Card>();

                foreach (CardStack v in PlayStacks.ToArray())
                {
                    var c = DealingStack.Cards.Last;

                   
                    c.Enabled = true;
                    c.Drag.Enabled = false;

                    // could  be rewritten?

                    c.AttachToStack(v);


                    ToBeAnimated.Add(c);

                }

                Console.Log("reordering cards, and animating...");

                ToBeAnimated.ForEachReversed( (c) => c.BringToFront() );

                //Console.Log("cards to be animated: " + ToBeAnimated.Count);

                EventHandler NextCard = null;

                NextCard =
                    delegate
                    {
                        if (ToBeAnimated.Count > 0)
                        {
                            var c = ToBeAnimated.Shift();

                            MySounds.PlaySoundDeal();

                            c.MoveToStackSlow(
                                delegate
                                {
                                    c.Drag.Enabled = true;
                                    CheckForGoodSuit(c.CurrentStack);

                                    if (ToBeAnimated.Count == 0)
                                    {
                                        DealingStack.Hide();

                                        MyStatus.Ready = true;

                                        Console.Log("done...");

                                        Helper.Invoke(done);
                                    }
                                    else
                                        NextCard();
                                });
                        }

                       
                    };

                NextCard();

            }
        }

     

        private void TryAutoMove(Card c)
        {
            Console.Log("finding free move... ");

            // try to send to a location / multiple cards?
  

            if (!this.PlayStacks.Contains(c))
                return;

            if (!Predicate.Invoke(c, IsDraggableFormPlayStack))
                return;

            Console.Log("will look for play move... ");

            if (MyDeck.TryToFitToAnyStack(c, PlayStacks, IsFitForPlayStack))
                return;

            Console.Log("didnt find any...");

            MySounds.PlaySoundNoMoveFound();
        }

        public void IsFitForPlayStack(Predicate<CardStack, Card> p)
        {
            // temp stack cannot handle more than one card

            var s = p.TargetIn;
            var c = p.TargetOut;

            p.Value = true;

            if (!PlayStacks.Contains(s))
            {
                p.Value = false;
                return;
            }


            if (s.Cards.Count == 0)
                return;

            var last = s.Cards.Last;

            if (!last.Drag.Enabled)
            {
                p.Value = false;
                return;
            }

            if (last.ModifiedRank + 1 != c.ModifiedRank)
            {
                p.Value = false;
                return;
            }
        }

    }


}


