using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Runtime;

namespace MonthSchedule.js
{
    [Script]
    public static class Storage
    {
        static Cookie _About = new Cookie("MonthSchedule.About");
        public static bool About { get { return _About.BooleanValue; } set { _About.BooleanValue = value; } }


        static Cookie<string[]> _Workers = new Cookie<string[]>("MonthSchedule.Workers");
        public static string[] Workers { get { return  _Workers.Value; } set { _Workers.Value = value; } }


    }
}
