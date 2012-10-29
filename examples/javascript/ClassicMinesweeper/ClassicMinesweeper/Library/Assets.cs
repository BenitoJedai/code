using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;

namespace MineSweeper.js
{
    public class Assets
    {
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

        public string Preview { get { return new ClassicMinesweeper.HTML.Images.FromAssets.Preview().src; } }
        public string g_6591cd { get { return new ClassicMinesweeper.HTML.Images.FromAssets.g_6591cd().src; } }
        public string face_cool { get { return new ClassicMinesweeper.HTML.Images.FromAssets.face_cool().src; } }
        public string face_ok { get { return new ClassicMinesweeper.HTML.Images.FromAssets.face_ok().src; } }
        public string face_ok_down { get { return new ClassicMinesweeper.HTML.Images.FromAssets.face_ok_down().src; } }
        public string face_scared { get { return new ClassicMinesweeper.HTML.Images.FromAssets.face_scared().src; } }
        public string face_dead { get { return new ClassicMinesweeper.HTML.Images.FromAssets.face_dead().src; } }

        public string question { get { return new ClassicMinesweeper.HTML.Images.FromAssets.question().src; } }
        public string flag { get { return new ClassicMinesweeper.HTML.Images.FromAssets.flag().src; } }
        public string button { get { return new ClassicMinesweeper.HTML.Images.FromAssets.button().src; } }
        public string empty { get { return new ClassicMinesweeper.HTML.Images.FromAssets.empty().src; } }
        public string notmine { get { return new ClassicMinesweeper.HTML.Images.FromAssets.notmine().src; } }
        public string mine { get { return new ClassicMinesweeper.HTML.Images.FromAssets.mine().src; } }
        public string mine_found { get { return new ClassicMinesweeper.HTML.Images.FromAssets.mine_found().src; } }

        public string _1 { get { return new ClassicMinesweeper.HTML.Images.FromAssets._1().src; } }
        public string _2 { get { return new ClassicMinesweeper.HTML.Images.FromAssets._2().src; } }
        public string _3 { get { return new ClassicMinesweeper.HTML.Images.FromAssets._3().src; } }
        public string _4 { get { return new ClassicMinesweeper.HTML.Images.FromAssets._4().src; } }
        public string _5 { get { return new ClassicMinesweeper.HTML.Images.FromAssets._5().src; } }
        public string _6 { get { return new ClassicMinesweeper.HTML.Images.FromAssets._6().src; } }
        public string _7 { get { return new ClassicMinesweeper.HTML.Images.FromAssets._7().src; } }
        public string _8 { get { return new ClassicMinesweeper.HTML.Images.FromAssets._8().src; } }

        public string[] red_numbers
        {
            get
            {
                return new[]
                    {
                        new ClassicMinesweeper.HTML.Images.FromAssets.red_0().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets.red_1().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets.red_2().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets.red_3().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets.red_4().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets.red_5().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets.red_6().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets.red_7().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets.red_8().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets.red_9().src
                    };
            }
        }

        public string[] numbers
        {
            get
            {
                return new[]
                    {
                        empty,
                        new ClassicMinesweeper.HTML.Images.FromAssets._1().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets._2().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets._3().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets._4().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets._5().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets._6().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets._7().src,
                        new ClassicMinesweeper.HTML.Images.FromAssets._8().src
                    };
            }
        }
    }

}
