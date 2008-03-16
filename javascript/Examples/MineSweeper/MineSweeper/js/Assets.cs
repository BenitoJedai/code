using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;

namespace MineSweeper.js
{
    [Script]
    public class Assets
    {
        public const string DefaultPath = "assets/MineSweeper";

        public readonly string Path = DefaultPath;

        public Assets(string p)
        {
            this.Path = p;
        }

        public Assets()
        {

        }

        static Assets _Default;
        public static Assets Default
        {
            get
            {
                if (_Default == null)
                    _Default = new Assets();

                return _Default;
            }
        }

        public string Preview { get { return Path + "/Preview.png"; } }
        public string g_6591cd { get { return Path + "/g_6591cd.gif"; } }
        public string face_cool { get { return Path + "/face_cool.PNG"; } }
        public string face_ok { get { return Path + "/face_ok.PNG"; } }
        public string face_ok_down { get { return Path + "/face_ok_down.PNG"; } }
        public string face_scared { get { return Path + "/face_scared.PNG"; } }
        public string face_dead { get { return Path + "/face_dead.PNG"; } }

        public string question { get { return Path + "/question.PNG"; } }
        public string flag { get { return Path + "/flag.PNG"; } }
        public string button { get { return Path + "/button.PNG"; } }
        public string empty { get { return Path + "/empty.PNG"; } }
        public string notmine { get { return Path + "/notmine.PNG"; } }
        public string mine { get { return Path + "/mine.PNG"; } }
        public string mine_found { get { return Path + "/mine_found.PNG"; } }

        public string _1 { get { return Path + "/1.PNG"; } }
        public string _2 { get { return Path + "/2.PNG"; } }
        public string _3 { get { return Path + "/3.PNG"; } }
        public string _4 { get { return Path + "/4.PNG"; } }
        public string _5 { get { return Path + "/5.PNG"; } }
        public string _6 { get { return Path + "/6.PNG"; } }
        public string _7 { get { return Path + "/7.PNG"; } }
        public string _8 { get { return Path + "/8.PNG"; } }

        public string[] red_numbers
        {
            get
            {
                return Enumerable.Range(0, 10).Select(i => Path + "/red_" + i + ".PNG").ToArray();
            }
        }

        public string[] numbers
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
