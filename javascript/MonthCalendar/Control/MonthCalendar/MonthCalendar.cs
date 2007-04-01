using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.Controls
{

    [Script]
    public class MonthCalendar
    {
        public IHTMLElement Control = new IHTMLDiv();


        public MonthCalendar()
        {
            Control.style.border = "1px solid blue";

            var d = IDate.Now;

            Console.WriteLine("Current year: " +  d.getFullYear());
            Console.WriteLine("Current month: " +  d.getMonth());
            Console.WriteLine("Current date: " +  d.getDate());

            Control.appendChild( "This Control was created at " + IDate.Now.toLocaleString() );

        }
    }
}
