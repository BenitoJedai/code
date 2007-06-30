using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;

using System.Linq;

using ScriptCoreLib.Shared.Drawing;

using global::System.Collections.Generic;
using System.ComponentModel;

namespace ScriptCoreLib.JavaScript.Cards
{
    [Script]
    public class CardStack
    {
        public event EventHandler<Card> Click;

        public readonly BindingList<Card> Cards = new BindingList<Card>();

        public readonly IHTMLDiv Control = new IHTMLDiv();

        private bool _Enabled = true;

        public bool Enabled
        {
            get { return _Enabled; }
            set 
            { 
                _Enabled = value;

                foreach (Card v in Cards.ToArray())
                {
                    v.Enabled = value;
                }
            }
        }


        public double Opacity
        {
            set
            {
                foreach (Card c in Cards.ToArray())
                {
                    c.Opacity = value;
                }
            }
        }

        public CardStack(Point p, params Card[] c)
            : this(p)
        {
            this.AttachCardsAndMove(true, c);

            if (Cards.Count > 0)
            {
                Native.Document.body.insertBefore(Control, Cards.First().Control);
            }
        }

        public CardStack(Point p)
            : this()
        {
            MoveTo(p);
        }

        public CardStack()
        {
            Control.style.SetSize(CardInfo.Width, CardInfo.Height);
            Control.style.backgroundColor = Color.White;
            Control.style.Opacity = 0.5;



            Native.Document.body.appendChild(Control);

            MoveTo(new Point(0, 0));
        }

        public void SetBackground(string e)
        {
            this.Control.style.backgroundColor = Color.None;
            this.Control.style.Opacity = 1;

            this.Control.style.SetBackground(e, false);
        }

        public Point Position;

        public void MoveTo(Point p)
        {
            Position = p;

            Control.SetCenteredLocation(p);
            Update();

        }


        public int HitRange = 32;

        public Point CardMargin = new Point(0, 28);

        public bool HitTest(Point point)
        {
            return point.CompareRange(NextPosition, HitRange) == -1;
        }


        public void AttachCards(params Card[] c)
        {
            AttachCards(null, c);
        }

        public event EventHandler CardsMovedToStack;

        public void AttachCards(EventHandler<Card> h, params Card[] e)
        {
            Helper.ForEach(e,
                delegate(Card c)
                {
                    c.AttachToStack(this);

                    Helper.Invoke(h, c);
                }
            );

            Helper.Invoke(CardsMovedToStack);
        }

        public static void CardByCard(EventHandler<Pair<Card, EventHandler>> h, params Card[] e)
        {
            var a = new List<Card>();


            a.AddRange(e);

            var z = new Pair<Card, EventHandler>(null, null);

            z.B =
                delegate
                {
                    z.A = a.Pop();

                    Helper.Invoke(h, z);
                };


            Helper.Invoke(z.B);
        }

        
        public void AttachCardsAndMove(bool fast, params Card[] x)
        {
            AttachCards(
                delegate(Card c)
                {
                    c.BringToFront();

                    if (fast)
                        c.MoveToFast(this.LastPosition);
                    else
                        this.MoveSlow(c, null);
                }
                , x
            );

        }


        public Point LastPosition
        {
            get
            {
                return new Point(
                    Position.X + CardMargin.X * (Cards.Count - 1), 
                    Position.Y + CardMargin.Y * (Cards.Count - 1));
            }
        }

        public Point NextPosition
        {
            get
            {
                return new Point(
                    Position.X + CardMargin.X * Cards.Count, 
                    Position.Y + CardMargin.Y * Cards.Count);
            }
        }



    

        public void Update()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                this[i].MoveToFast(new Point(
                    Position.X + CardMargin.X * i, 
                    Position.Y + CardMargin.Y * i));
            }
        }

        public Card this[int i]
        {
            get
            {
                return Cards[i];
            }
        }

        public void RaiseClick(Card card)
        {
            Helper.Invoke(Click, card);
        }

        public void Hide()
        {
            Control.Hide();

            foreach (Card v in Cards.ToArray())
            {
                v.Hide();
            }
        }

        public CardDeck CurrentDeck;

        /// <summary>
        /// animates movement
        /// </summary>
        /// <param name="c"></param>
        public void MoveSlow(Card c, EventHandler done)
        {


            TweenDataPoint u = null;


            u = new TweenDataPoint(
                delegate
                {
                    c.MoveTo(u.Value);
                }
            );

            u.Speed = 20;
            
            u.Done += done;

            u.Value = c.Drag.Position;
            u.Value = this.LastPosition;


        }

        public void Remove(params Card[] e)
        {
            foreach (Card v in e)
            {
                v.Drag.Enabled = false;
                v.Opacity = 0;
            }

            foreach (var v in e) Cards.Remove(v);


            if (CurrentDeck != null)
            {
                foreach (var v in e) CurrentDeck.Cards.Remove(v);
            }
        }

        public void Clear()
        {
            Remove(Cards.ToArray());

        }

        public void ToConsole()
        {
            Console.Log("stack has these cards:");

            foreach (Card v in this.Cards.ToArray())
            {
                Console.Log(v.Info.Description);
            }
        }

        public void RevealLastCard()
        {
            if (this.Cards.Count == 0)
                return;

            if (!this.Cards.Last().Enabled)
                this.Cards.Last().Enabled = true;
        }
    }
}
