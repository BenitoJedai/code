//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

//using global::System.Collections.Generic;



namespace TextRotator.js
{


    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {
            

            IStyleSheet.Default.AddRule("html",
                r =>
                {
                    r.style.backgroundColor = Color.Black;
                    r.style.color = Color.Green;
                    r.style.fontFamily = IStyle.FontFamilyEnum.Consolas;
                    r.style.padding = "3em";
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                }
            );

            var c = new IHTMLElement(IHTMLElement.HTMLElementEnum.center, "");
            var cursor = Native.Document.createElement("blink");
            cursor.innerText = "_";
            var lines = Text.Split('\n');

            var index = 0;
            var index_char = 0;

            var span = new IHTMLSpan();

            var delay_delayed = false;

            var Delay = default(Action<Action, int>);

            Delay = (h, due) => new Timer(

            delegate
            {

                if (delay_delayed)
                    Delay(h, due);
                else
                    h();

            }, due, 0);

            Func<string> CurrentLineString = () => index + ". " + lines[index];

            var DeleteChar = default(Action);
            var PrintChar = default(Action);
            var ChooseLine = default(Action);

            DeleteChar =
                () =>
                {
                    index_char--;

                    span.innerText = CurrentLineString().Substring(0, index_char);

                    if (index_char == 0)
                    {
                        ChooseLine();
                    }
                    else
                    {
                        Delay(DeleteChar, 30);
                    }
                };

            PrintChar =
                () =>
                {
                    index_char++;

                    if (index_char < CurrentLineString().Length)
                    {
                        var x = 100;
                        var y = CurrentLineString()[index_char];


                        if (",. \t\n".Contains("" + y))
                            x = 200;



                        span.innerText = CurrentLineString().Substring(0, index_char);
                        Delay(PrintChar, x);
                    }
                    else
                    {
                        Delay(DeleteChar, 3000);
                    }
                };

            ChooseLine =
                () =>
                {
                    index = new System.Random().Next() % lines.Length;
                    index_char = 0;
                    span.innerText = "";

                    PrintChar();
                };

            c.onmouseover +=
                delegate
                {
                    c.style.color = Color.Yellow;
                    delay_delayed = true;
                };

            c.onmouseout +=
                delegate
                {
                    c.style.color = Color.None;
                    delay_delayed = false;
                };


            c.appendChild(span, cursor);
            c.attachToDocument();

            ChooseLine();
        }

        // see http://www.cncden.com/tsmods/tsfaq.zip
        static string Text
        {
            get
            {
                return @"
If the enemy is in range, so are you.
     	Incoming fire has the right of way.
     	Don't look conspicuous, it draws fire.
There is always a way.
The easy way is always mined.
Try to look unimportant, they may be low on ammo.
     	The enemy invariably attacks on two occasions:
       		a. When you're ready for them.
b. When you're not ready for them.
   	Teamwork is essential, it gives them someone else to shoot at.
The enemy diversion you have been ignoring will be the main attack.
If your attack is going well, you have walked into an ambush.
Never draw fire, it irritates everyone around you.
Anything you do can get you shot, including nothing.
Make it tough enough for the enemy to get in and you won't be able to get out.
Never share a foxhole with anyone braver than yourself.
If you're short of everything but the enemy, you're in a combat zone.
When you have secured an area, don't forget to tell the enemy.
Never forget that your weapon is made by the lowest bidder.
Friendly fire isn't.
If the sergeant can see you, so can the enemy.
Remember, a retreating enemy is probably just falling back and regrouping.
If at first you don't succeed call in an air-strike.
Exceptions prove the rule, and destroy the battle plan.
The enemy never watches until you make a mistake.
One enemy soldier is never enough, but two is entirely too many.
The more a weapon costs, the farther you will have to send it away to be repaired.
Field experience is something you don't get until just after you need it.
  	No matter which way you have to march, its always uphill.
The one item you need is always in short supply.
Airstrikes always overshoot the target, artillery always falls short.
When you have sufficient supplies & ammo, the enemy takes 2 weeks to attack. When you are low on supplies & ammo the enemy decides to attack that night.
Suppressive fires - won't.
If it's stupid but it works, it isn't stupid.
When in doubt empty the magazine.
No plan survives the first contact, intact.
If you are forward of your position, the artillery will fall short.
The important things are always simple.
The simple things are always hard.
Beer Math -> 2 beers time 37 men equals 49 cases.
Body count math -> 3 guerrillas plus 1 probable plus 2 pigs equals 37 enemies killed in action.
Tracers work both ways.
The only thing more accurate than incoming enemy fire is incoming friendly fire.
If you take more than your share of objectives, you will have more than your fair share to take.
When both sides are convinced they are about to lose, they're both right.
";

            }
        }

        static Class1()
        {
            //Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
