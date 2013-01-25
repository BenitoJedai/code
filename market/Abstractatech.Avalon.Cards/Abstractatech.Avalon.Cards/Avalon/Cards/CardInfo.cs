using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using Abstractatech.Avalon.Cards.Avalon.Images;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
    [Script]
    public partial class CardInfo
    {
        // http://www.goodsol.com/pgshelp/trapdoor_spider.htm
        // http://en.wikipedia.org/wiki/Playing_card
        // http://www.jfitz.com/cards/classic-cards.zip

        public CardInfo(SuitEnum suit, RankEnum rank)
        {
            Suit = suit;
            Rank = rank;
        }




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
                var w = new StringBuilder();

                //if (Visible)
                //{
                if (Rank == RankEnum.RankJoker)
                    w.Append("Joker");
                else
                {
                    if (Rank == RankEnum.RankAce) w.Append("Ace");
                    else if (Rank == RankEnum.RankKing) w.Append("King");
                    else if (Rank == RankEnum.RankQueen) w.Append("Queen");
                    else if (Rank == RankEnum.RankJack) w.Append("Jack");
                    else w.Append("" + (2 + (int)RankEnum.Rank2 - (int)Rank));


                    w.Append(" Of ");

                    if (Suit == SuitEnum.Diamond) w.Append("Diamonds");
                    if (Suit == SuitEnum.Heart) w.Append("Hearts");
                    if (Suit == SuitEnum.Spade) w.Append("Spades");
                    if (Suit == SuitEnum.Club) w.Append("Clubs");
                }
                //}
                //else
                //{
                //    w.Write("Unknown");
                //}

                return w.ToString();
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
            return rank.ToArray(z => new CardInfo(suit, z));

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



        public static CardInfo[] FullDeck()
        {
            return FullDeck(false);
        }

        public static CardInfo[] FullDeck(bool IsBlackDeck)
        {
            var a = By(
                SuitEnum.Spade,
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


        public Image GetImage()
        {
            var a = new Func<Image>[] { 
                null,
                () => new _1(),
                () => new _2(),
                () => new _3(),
                () => new _4(),
                () => new _5(),
                () => new _6(),
                () => new _7(),
                () => new _8(),
                () => new _9(),
                () => new _10(),
                () => new _11(),
                () => new _12(),
                () => new _13(),
                () => new _14(),
                () => new _15(),
                () => new _16(),
                () => new _17(),
                () => new _18(),
                () => new _19(),
                () => new _20(),
                () => new _21(),
                () => new _22(),
                () => new _23(),
                () => new _24(),
                () => new _25(),
                () => new _26(),
                () => new _27(),
                () => new _28(),
                () => new _29(),
                () => new _30(),
                () => new _31(),
                () => new _32(),
                () => new _33(),
                () => new _34(),
                () => new _35(),
                () => new _36(),
                () => new _37(),
                () => new _38(),
                () => new _39(),
                () => new _40(),
                () => new _41(),
                () => new _42(),
                () => new _43(),
                () => new _44(),
                () => new _45(),
                () => new _46(),
                () => new _47(),
                () => new _48(),
                () => new _49(),
                () => new _50(),
                () => new _51(),
                () => new _52(),
                () => new _53(),
                () => new _54(),
            };

            return a[this.ResourceIndex]();
        }

        public string GetImagePath(string path)
        {
            return GetImagePath(path, Visible, ResourceIndex, CustomBackground, IsBlackDeck);
        }


        public static string GetImagePath(string path, bool Visible, int ResourceIndex, string CustomBackground, bool IsBlackDeck)
        {
            var src = path + "/";

            if (Visible)
            {
                src += ResourceIndex;
            }
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



    }
}
