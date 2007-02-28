using ScriptCoreLib;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.System;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace SubSquare.source.js
{

    [Script]
    class SubSquare
    {

        IHTMLDiv Base = new IHTMLDiv();

        Timer timer = new Timer();

        int value = 0xFF;

        Rectangle Location;

        string _bg;
        


        public SubSquare(Rectangle size, string bg)
        {
            this._bg = bg;

            Location = size;

            Base.style.SetLocation(size);
            Base.style.backgroundImage = "url(" + bg + ")";

            Base.style.backgroundPosition = (-size.Left) + "px " + (-size.Top) + "px";

            timer.Tick += new EventHandler<Timer>(timer_Tick);

            Base.onmouseover += delegate { if (value > step) timer.StartInterval(40); };

            Base.onmouseout += delegate { timer.Stop();
                //Base.style.backgroundColor = Color.FromGray(value); 
            };

            Base.attachToDocument();
        }

        const int step = 5;

        void timer_Tick(Timer e)
        {
            if (value > step)
            {
                Console.WriteLine("tick");

                value -= step;

                //Base.style.backgroundColor = Color.Red;

                Base.style.Opacity = 1 - (value / 0xFF);

                if (value <= step)
                {
                    Console.WriteLine("// we are done");


                    
                    timer.Stop();

                    //this.Base.style.backgroundColor = Color.Red;

                    WorkPool p = Fader.FlashAndFadeOut(this.Base, 200);


                    p += delegate
                    {
                        new SubSquare(
                        Rectangle.Of(Location.Left, Location.Top, Location.Width / 2, Location.Height / 2)
                        , _bg
                          );

                        new SubSquare(
                            Rectangle.Of(Location.Left + Location.Width / 2, Location.Top, Location.Width / 2, Location.Height / 2)
                            , _bg
                        );

                        new SubSquare(
                            Rectangle.Of(Location.Left + Location.Width / 2, Location.Top + Location.Height / 2, Location.Width / 2, Location.Height / 2)
                            , _bg
                        );

                        new SubSquare(
                            Rectangle.Of(Location.Left, Location.Top + Location.Height / 2, Location.Width / 2, Location.Height / 2)
                            , _bg
                        );

                        this.Base.Dispose();
                    };
                }
            }
            else
                timer.Stop();
        }

        static SubSquare()
        {

            //Native.Spawn("Web.ss",
            //    delegate(IHTMLElement e)
            //    {
            //        new SubSquare(Rectangle.Of(8,8, Native.Document.body.clientWidth - 16, Native.Document.body.clientHeight - 16));


            //        e.Dispose();
            //    }
            //);

        }
    }
}
