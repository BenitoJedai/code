using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

using global::System.Collections.Generic;

namespace ScriptCoreLib.JavaScript.Cards
{
    [Script]
    public class CardInfo
    {
        // http://www.goodsol.com/pgshelp/trapdoor_spider.htm
        // http://en.wikipedia.org/wiki/Playing_card
        // http://www.jfitz.com/cards/classic-cards.zip

        public CardInfo(SuitEnum suit, RankEnum rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public enum SuitEnum
        {
            Unknown,

            Spade,
            Club,
            Heart,
            Diamond,

        }

        public SuitEnum Suit;

        public enum RankEnum
        {
            Unknown,

            RankAce,
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
            RankJoker,
        }

        public RankEnum Rank;

        public bool Visible;

        public int ResourceIndex
        {
            get
            {
                var i = (((int)Rank - 1) * 4) + ((int)Suit + 0);

                return i;
            }
        }

        public string Description
        {
            get
            {
                var w = new TextWriter();

                //if (Visible)
                //{
                    if (Rank == RankEnum.RankJoker)
                        w.Write("Joker");
                    else
                    {
                        if (Rank == RankEnum.RankAce) w.Write("Ace");
                        else if (Rank == RankEnum.RankKing) w.Write("King");
                        else if (Rank == RankEnum.RankQueen) w.Write("Queen");
                        else if (Rank == RankEnum.RankJack) w.Write("Jack");
                        else w.Write("" + (2 + (int)RankEnum.Rank2 - (int)Rank));


                        w.Write(" Of ");

                        if (Suit == SuitEnum.Diamond) w.Write("Diamonds");
                        if (Suit == SuitEnum.Heart) w.Write("Hearts");
                        if (Suit == SuitEnum.Spade) w.Write("Spades");
                        if (Suit == SuitEnum.Club) w.Write("Clubs");
                    }
                //}
                //else
                //{
                //    w.Write("Unknown");
                //}

                return w.Text;
            }
        }

        public static int Width
        {
            get
            {
                return 72;
            }
        }

        public static int Height
        {
            get
            {
                return 96;
            }
        }



        public static CardInfo[] By(SuitEnum suit, params CardInfo.RankEnum[] rank)
        {
            var a = new List<CardInfo>();

            foreach (var z in rank)
            {
                var c = new CardInfo(suit, z);



                a.Add(c);
            }

            return a.ToArray();
        }



        public static CardInfo[] By(int multiple, params SuitEnum[] suit)
        {
            var a = new List<CardInfo>();

            while (multiple > 0)
            {
                a.AddRange(By(suit));

                multiple--;
            }

            return a.ToArray();
        }

        public static CardInfo[] By(params SuitEnum[] suit)
        {
            var a = new List<CardInfo>();

            foreach (SuitEnum s in suit)
            {
                a.AddRange(By(s,
               RankEnum.RankAce,
               RankEnum.RankKing,
               RankEnum.RankQueen,
               RankEnum.RankJack,
               RankEnum.Rank10,
               RankEnum.Rank9,
               RankEnum.Rank8,
               RankEnum.Rank7,
               RankEnum.Rank6,
               RankEnum.Rank5,
               RankEnum.Rank4,
               RankEnum.Rank3,
               RankEnum.Rank2
                    //RankEnum.RankJoker
               )
           );
            }

            return a.ToArray();
        }

        public static CardInfo[] DualDeck()
        {
            var a = new List<CardInfo>();

            a.AddRange(FullDeck(false));
            a.AddRange(FullDeck(true));

            return a.ToArray();
        }



        public static CardInfo[] FullDeck(bool IsBlackDeck)
        {
            var a = By(SuitEnum.Spade,
                SuitEnum.Club,
                SuitEnum.Heart,
                SuitEnum.Diamond);

            foreach (var v in a)
            {
                v.IsBlackDeck = IsBlackDeck;
            }

            return a;
        }

        public bool IsBlackDeck;

        public string CustomBackground;


        public string GetImagePath(string path)
        {
            var src = path + "/";

            if (Visible)
                src += ResourceIndex;
            else
            {
                if (CustomBackground != null)
                {
                    src += CustomBackground;
                }
                else
                {
                    if (IsBlackDeck)
                        src += "b1fv";
                    else
                        src += "b2fv";
                }

            }

            src += ".png";

            return src;
        }

        public IHTMLImage ToImage(string path)
        {
            var i = new IHTMLImage(GetImagePath(path));

            i.alt = Description;

            return i;
        }

        public static CardInfo[] FullDeck()
        {
            return FullDeck(false);
        }
    }
}
