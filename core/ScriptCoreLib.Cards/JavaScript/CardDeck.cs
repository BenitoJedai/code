using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.Shared.Drawing;

using global::System.Collections.Generic;
using global::System.ComponentModel;

namespace ScriptCoreLib.JavaScript.Cards
{
    [Script]
    public partial class CardDeck
    {


        public string GfxPath = "fx/cards";

        public readonly List<CardInfo> UnusedCards = new List<CardInfo>();
        public readonly BindingList<CardStack> Stacks = new BindingList<CardStack>();

        /// <summary>
        /// override this if the current game does not use default card ranking
        /// </summary>
        public EventHandler<ConvertTo<CardInfo.RankEnum, int>> RankConverter;

        public CardDeck()
        {
            Stacks.ListChanged += (sender, args) =>
                {
                    if (args.ListChangedType == ListChangedType.ItemAdded)
                    {
                        Stacks[args.NewIndex].CurrentDeck = this;
                        return;
                    }
                };
        }

        public CardInfo Fetch
        {
            get
            {
                if (UnusedCards.Count == 0)
                    return null;


                var x = (int)System.Math.Floor(new System.Random().NextDouble() * UnusedCards.Count);

                var i = UnusedCards[x];

                UnusedCards.Remove(i);
                return i;
            }
        }

        public void ToConsole()
        {
            Console.Log("unused cards: " + UnusedCards.Count);
        }

        public CardStack StackBy(Card point)
        {
            var s = StackBy(point.Drag.Position);

            if (s == point.CurrentStack)
                return null;

            return s;
        }

        public CardStack StackBy(Point point)
        {
            CardStack r = null;

            foreach (CardStack v in this.Stacks)
            {
                if (v.HitTest(point))
                {
                    r = v;
                }
            }

            return r;
        }

        /// <summary>
        /// this event will be invoked at the moment a new card is created
        /// </summary>
        public event EventHandler<Card> ApplyCardRules;

        public List<Card> Cards = new List<Card>();

        public Card FetchCard
        {
            get
            {
                var i = this.Fetch;

                if (i != null)
                {
                    var c = new Card(this, i);

                    Helper.Invoke(ApplyCardRules, c);

                    Cards.Add(c);

                    return c;
                }

                return null;
            }
        }

        public Card[] FetchCards(int count)
        {
            var a = new List<Card>();

            while (count > 0)
            {
                var c = FetchCard;

                if (c != null)
                {
                    a.Add(c);
                }

                count--;
            }

            return a.ToArray();
        }

        /// <summary>
        ///  creates a new list of cardstacks, and makes sure that each card
        ///  will be added to the decks stack list as backreference
        /// </summary>
        /// <returns></returns>
        public BindingList<CardStack> CreateStackList()
        {
            var p = new BindingList<CardStack>();

            p.ListChanged +=
                (sender, args) =>
                {
                    if (args.ListChangedType == ListChangedType.ItemAdded)
                    {
                        //System.Diagnostics.Debugger.Break();
                        this.Stacks.Add(p[args.NewIndex]);
                        return;
                    }

                    if (args.ListChangedType == ListChangedType.ItemDeleted)
                    {
                        //System.Diagnostics.Debugger.Break();
                        // sync?

                        this.Stacks.RemoveAt(args.NewIndex);
                        return;
                    }
                };

            return p;
        }


        public bool TryToFitToAnyStack(Card c, IEnumerable<CardStack> s, EventHandler<Predicate<CardStack, Card>> h)
        {
            var r = false;

            foreach (CardStack v in s.AsEnumerable())
            {
                if (Predicate.Invoke(v, c, h))
                {
                    v.AttachCardsAndMove(false, c.MovableCards);


                    r = true;

                    break;
                }
            }

            return r;
        }
    }
}
