using ScriptCoreLib;
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

            Control.appendChild( "This Control was created at " + IDate.Now.toLocaleString() );

        }
    }
}
