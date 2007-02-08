using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace cnc.source.js.Controls
{
    [Script]
    public class DemoControl : SpawnControlBase
    {
        public const string Alias = "fx.DemoControl";



        public DemoControl(IHTMLElement e)
            : base(e)
        {
            new IHTMLImage("fx/bg/3877.jpg").ToDocumentBackground();

            var t = new Timer();



            CreateRotatingTank(96 + 48 * 7, 96 + 48 * 1, t, "tree_1", 383, 392);
            CreateRotatingTank(96 + 48 * 1, 96 + 48 * 1, t, "tank_1", 1562, 1593);
            CreateRotatingTank(96 + 48 * 1, 96 + 48 * 2, t, "tank_2", 308, 339);
            CreateRotatingTank(96 + 48 * 4, 96 + 48 * 1, t, "harvester_1", 71, 71 + 31);
            CreateRotatingTank(96 + 48 * 4, 96 + 48 * 3, t, "explosion_1", 990, 1015);
            CreateRotatingTank(96 + 48 * 5, 96 + 48 * 5, t, "building_1", 365, 382);
            CreateRotatingTank(96 + 48 * 7, 96 + 48 * 5, t, "building_2", 1252, 1267);
            CreateRotatingTank(96 + 48 * 3, 96 + 48 * 5, t, "building_3", 393, 404);
            CreateRotatingTank(96 + 48 * 1, 96 + 48 * 5, t, "building_4", 405, 422);

        

            t.StartInterval(100);
        }

        private static void CreateRotatingTank(int x, int y, Timer t, string fxtype, int min, int max)
        {
    

            int cur = min;

            var cache = new string[max - min + 1];

            for (int i = min; i <= max; i++)
            {
                cache[i - min] = "fx/" + fxtype + "/" +  i + ".png";
            }

            var img = new IHTMLImage();

            img.attachToDocument();

            img.SetCenteredLocation(x, y);

            bool ok = true;

            img.onmouseover += delegate
            {
                ok = false;
            };

            img.onmouseout +=
                delegate
                {
                    ok = true;

                };

            int dir = 0;

            img.onclick += delegate
            {
                dir++;
            };


            t.Tick +=
                delegate
                {
                    if (ok)
                    {
                        UpdateFrame(min, cur, cache, img);

                        cur = NextFrame(min, max, cur, dir);
                    }
                };
        }

        private static void UpdateFrame(int min, int cur, string[] cache, IHTMLImage img)
        {
            // _1483b5833155c53585239c5e871e940c_600000c	2496	93.55%	871.254ms	1041.498ms	0.417ms
            img.src = cache[cur - min];
        }

        private static int NextFrame(int min, int max, int cur, int dir)
        {
            if (dir % 2 == 0)
            {
                cur++;

                if (cur > max)
                    cur = min;
            }
            else
            {
                cur--;

                if (cur < min)
                    cur = max;
            }
            return cur;
        }



    }


}
