using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;

namespace MineSweeper.js
{
    [Script]
    class Assets
    {
        const string Path = "assets/MineSweeper";

        public static readonly string face_ok = Path + "/face_ok.PNG";
        public static readonly string face_ok_down = Path + "/face_ok_down.PNG";
        public static readonly string face_scared = Path + "/face_scared.PNG";
        public static readonly string face_dead = Path + "/face_dead.PNG";

        public static readonly string question = Path + "/question.PNG";
        public static readonly string flag = Path + "/flag.PNG";
        public static readonly string button = Path + "/button.PNG";
        public static readonly string empty = Path + "/empty.PNG";
        public static readonly string notmine = Path + "/notmine.PNG";
        public static readonly string mine = Path + "/mine.PNG";
        public static readonly string mine_found = Path + "/mine_found.PNG";

        public static readonly string _1 = Path + "/1.PNG";
        public static readonly string _2 = Path + "/2.PNG";
        public static readonly string _3 = Path + "/3.PNG";
        public static readonly string _4 = Path + "/4.PNG";
        public static readonly string _5 = Path + "/5.PNG";
        public static readonly string _6 = Path + "/6.PNG";
        public static readonly string _7 = Path + "/7.PNG";
        public static readonly string _8 = Path + "/8.PNG";

        public static string[] red_numbers
        {
            get
            {
                return Enumerable.Range(0, 10).Select(i => Path + "/red_" + i + ".PNG").ToArray();
            }
        }

        public static string[] numbers
        {
            get
            {
                return new[]
                    {
                        empty,
                        _1,
                        _2,
                        _3,
                        _4,
                        _5,
                        _6,
                        _7,
                        _8
                    };
            }
        }
    }

}
