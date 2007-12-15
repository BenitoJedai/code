using ScriptCoreLib;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System;

namespace NumberGuessingGame.source.js
{

    [Script]
    class GuessingGame
    {
        IHTMLElement Control;


        public IStyle Style
        {
            get
            {
                return Control.style;
            }
        }


        [Script]
        class Game
        {
            public int Value = GetRandomInteger;

            public bool Contains(int v)
            {
                Console.WriteLine("will search for " + v);

                for (int i = 0; i < GuessedValues.length; i++)
                {
                    int x = GuessedValues[i];

                    Console.WriteLine("compare " + x + " to " + v);

                    if (GuessedValues[i] == v)
                        return true;
                }

                return false;
            }

            public bool Done
            {
                get
                {
                    if (GuessedValues.length == 10)
                        return true;

                    string y = Value + "";

                    for (int i = 0; i < y.Length; i++)
                    {
                        string c = y.Substring(i, 1);

                        if (!Contains(int.Parse(c)))
                            return false;
                    }

                    return true;
                }
            }
            public string MaskedValue
            {
                get
                {
                    string x = "";

                    string y = Value + "";

                    for (int i = 0; i < y.Length; i++)
                    {
                        string c = y.Substring(i, 1);

                        if (Contains(int.Parse(c)))
                            x += c;
                        else
                            x += "#";
                    }

                    return x;
                }
            }

            public IArray<int> GuessedValues = new IArray<int>();

            static int GetRandomInteger
            {
                get
                {
                    return System.Convert.ToInt32( new System.Random().NextDouble()  * 0xffff );
                }
            }

            public event ScriptCoreLib.Shared.EventHandler Changed;

            public void Guessed(int i)
            {
                if (Contains(i))
                    return;

                GuessedValues.push(i);

                RaiseChanged();
            }

            public void RaiseChanged()
            {
                Helper.Invoke(Changed);
            }
        }


        Game MyGame = new Game();

        IHTMLDiv HintControl = new IHTMLDiv();
        IHTMLDiv StatusControl = new IHTMLDiv();
        IHTMLDiv NumberBar = new IHTMLDiv();
        IHTMLButton ResetButton = new IHTMLButton("new game");

        public GuessingGame(IHTMLElement e)
        {
            // 
            e.innerHTML = @"<h1>
                Try to guess a 5-figure integer 
                </h1>
                ";

            e.appendChild(HintControl, StatusControl, NumberBar);

            
            StatusControl.style.color = Color.Blue;
            
            Control = e;

            ResetButton.onclick +=
                delegate
                {
                    new GuessingGame(Control);
                };

            MyGame.Changed +=
                delegate
                {
                    HintControl.innerHTML = "<h2>You have guessed " + MyGame.GuessedValues.length + " times";



                    StatusControl.innerHTML = MyGame.MaskedValue;

                    if (MyGame.Done)
                    {
                        NumberBar.FadeOut();

                        StatusControl.innerHTML += "<hr /> game over";
              
                        Control.appendChild(ResetButton);
                    }
                };

            MyGame.RaiseChanged();

            for (int i = 0; i < 10; i++)
            {
                IHTMLButton btn = new IHTMLButton(i + "");

                int ux = i;

                btn.onclick +=
                    delegate
                    {
                        MyGame.Guessed(ux);

                        btn.FadeOut();
                    };

                NumberBar.appendChild(btn);
            }


            Style.textAlign = IStyle.TextAlignEnum.center;
            Style.backgroundColor = Color.System.ButtonFace;
            Style.borderColor = Color.Gray;
            Style.borderWidth = "1px";
            Style.borderStyle = "dotted";
            Style.padding = "8px";
        }

        #region spawn
        static GuessingGame()
        {

            Native.Spawn("GuessingGame",
                delegate(IHTMLElement e)
                {
                    new GuessingGame(e);
                }
            );


        }
        #endregion
    }
}