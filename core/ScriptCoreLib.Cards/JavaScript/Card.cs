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


namespace ScriptCoreLib.JavaScript.Cards
{
    [Script]
    public partial class Card 
    {
        public readonly CardInfo Info;

        public readonly IHTMLDiv Control = new IHTMLDiv();


        public readonly DragHelper Drag;

        public readonly TweenDataDouble OpacityHelper;

        public double Opacity
        {
            get { return OpacityHelper.Value; }
            set { OpacityHelper.Value = value; }
        }

        public readonly CardDeck CurrentDeck;

        public CardStack CurrentStack;

        public static implicit operator CardStack(Card e)
        {
            return e.CurrentStack;
        }

        public event EventHandler Click;
        public event EventHandler DoubleClick;
        public event EventHandler Moved;

        public event EventHandler<Predicate<CardStack>> ValidateDragStop;

        CardStack FindStack()
        {
            var p = new Predicate<CardStack>();

            p.Target = CurrentDeck.StackBy(this);

            if (p.Target == null)
                return null;

            p.Invoke(ValidateDragStop);

            return p.Target;
        }



        public Card(CardDeck d, CardInfo i)
        {
            CurrentDeck = d;

            Drag = new DragHelper(Control);
            OpacityHelper = new TweenDataDouble(
                delegate
                {
                    foreach (Card v in this.MovableCards)
                    {
                        v.Control.style.Opacity = Opacity;
                    }
                }
                );

            Info = i;

            Control.style.cursor = IStyle.CursorEnum.@default;
            Control.style.SetSize(CardInfo.Width, CardInfo.Height);

            

            Native.Document.body.appendChild(Control);

            MoveTo(Drag.Position);

            Drag.History = new List<Point>();

            Drag.DragStop +=
                delegate
                {
                    Opacity = 1;

                    var s = FindStack();

                    if (s == null)
                        MoveTo(Drag.History.First);
                    else
                    {
                        s.AttachCards(this.MovableCards);
                    }

                    Drag.History.Clear();
                };

            Drag.DragStart +=
                delegate
                {
                    Opacity = 0.7;

                    foreach (Card v in MovableCards)
                    {
                        v.BringToFront();
                    }
                };

            Drag.DragMove +=
                delegate
                {
                   
                          
                 
                    var s = FindStack();

                    if (s != null)
                    {
                        Opacity = 1;
                        MoveTo(s.NextPosition);
                    }
                    else
                    {
                        Opacity = 0.7;
                        MoveTo(Drag.Position);
                    }
                

                    
               

                };



            Control.ondblclick += ( e) => Helper.Invoke(DoubleClick);
            Control.onclick += delegate (IEvent e)
            {

                Helper.Invoke(Click);

                if (CurrentStack != null)
                    CurrentStack.RaiseClick(this);
            };
    

    
            Opacity = 1;

            Enabled = false;
        }

        public void BringToFront()
        {
            Native.Document.body.appendChild(Control);
        }

        public bool Enabled
        {
            get
            {
                return Drag.Enabled;
            }

            set
            {
                Drag.Enabled = value;

                Info.Visible = value;

                Update();
            }
        }

        public void Update()
        {
            Control.style.SetBackground(ImagePath, false);

        }

        public string ImagePath
        {
            get
            {
                var s = "";

                if (CurrentDeck != null)
                    s = CurrentDeck.GfxPath;

                return Info.GetImagePath(s);
            }
        }

        public int ModifiedRank
        {
            get
            {
                var r = Info.Rank;
                var i =(int)r;

                if (CurrentDeck != null)
                    return Convert.To(r, i, CurrentDeck.RankConverter);


                return i;
            }
        }

        public Card[] MovableCards
        {
            get
            {
                var a = new List<Card>();

                a.Add(this);
                a.Add(StackedCards);

                return a.ToArray();
            }
        }

        public bool HasStackedCards
        {
            get
            {
                if (CurrentStack != null)
                {
                    var i = CurrentStack.Cards.IndexOf(this);

                    return i < CurrentStack.Cards.Count - 1;
                }

                return false;
            }
        }

        public Card[] StackedCards
        {
            get
            {
                var a = new List<Card>();

                if (CurrentStack != null)
                {
                    var i = CurrentStack.Cards.IndexOf(this);

                    if (i > -1)
                    {
                        for (int j = i +1; j < CurrentStack.Cards.Count; j++)
                        {
                            a.Add(CurrentStack.Cards[j]);
                        }
                    }
                }

                return a.ToArray();
            }
        }

        public void MoveToFast(Point p)
        {
            Drag.Position = p;

            Control.SetCenteredLocation(p);
        }

        public void MoveTo(Point p)
        {

            MoveToFast(p);

            int i = 0;

            foreach (Card v in StackedCards)
            {
                i++;

                v.MoveToFast(p + new Point(
                    i * CurrentStack.CardMargin.X, 
                    i * CurrentStack.CardMargin.Y));
            }
        }

        public int Index
        {
            get
            {
                if (CurrentStack == null)
                    return -1;

                return CurrentStack.Cards.IndexOf(this);
            }
        }

        public Card PreviousCard
        {
            get
            {

                var i = Index;

                if (i > 0)
                    return CurrentStack.Cards[i - 1];

                return null;

            }
        }

        public Card NextCard
        {
            get
            {

                var i = Index;

                if (i == -1)
                    return null;

                if (i < CurrentStack.Cards.Count - 1)
                    return CurrentStack.Cards[i + 1];

                return null;

            }
        }

        public void Hide()
        {

            Control.Hide();
        }

        public void AttachToStack(CardStack s)
        {
            var c = this;

            if (c.CurrentStack != null)
            {
                c.CurrentStack.Cards.Remove(c);
            }

            c.CurrentStack = s;

            s.Cards.Add(c);

            Helper.Invoke(this.Moved);
        }

        //public void MoveToStackSlow(CardStack s/*, EventHandler done*/)
        //{
        //    s.AttachCards(
        //        delegate(Card c)
        //        {
        //            c.BringToFront();

        //            s.MoveSlow(c, null);
        //        },
        //        this.MovableCards
        //    );
        //}

        public void MoveToStackSlow(EventHandler h)
        {
            if (this.CurrentStack == null)
                return;

            if (this.HasStackedCards)
                throw new System.Exception("single card expected");

            CurrentStack.AttachCards(
                delegate(Card c)
                {
                    c.BringToFront();

                    CurrentStack.MoveSlow(c, h);
                },
                this
            );
        }

        public void ToConsole()
        {
            Console.Log(": " + Info.Description);

        }



        public bool IsParentRankOf(Card c)
        {
            return c.ModifiedRank + 1 == ModifiedRank;
        }
    }
}
