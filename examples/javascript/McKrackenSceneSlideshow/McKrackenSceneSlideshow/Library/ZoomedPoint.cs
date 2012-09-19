using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NatureBoy.js;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace McKrackenSceneSlideshow.Library
{
    class ZoomedPoint
    {

        public Func<double> GetX;
        public Func<double> GetY;
        public Func<double> GetZ;

        public double Z { get { return GetZ(); } set { GetZ = () => value; } }
        public double X { get { return GetX(); } set { GetX = () => value; } }
        public double Y { get { return GetY(); } set { GetY = () => value; } }

        public double ZoomedX { get { return GetX() * GetZ(); } }
        public double ZoomedY { get { return GetY() * GetZ(); } }

        public int ZoomedXint { get { return ZoomedX.ToInt32(); } }
        public int ZoomedYint { get { return ZoomedY.ToInt32(); } }

        public string ZoomedXpx { get { return ZoomedXint + "px"; } }
        public string ZoomedYpx { get { return ZoomedYint + "px"; } }


        public ZoomedPoint ApplyZoomedLocation(IHTMLElement e)
        {
            e.style.SetLocation(ZoomedX.ToInt32(), ZoomedY.ToInt32());

            return this;
        }

        public ZoomedPoint ApplyZoomedSize(IHTMLElement e)
        {
            e.style.width = ZoomedXpx;
            e.style.height = ZoomedYpx;

            return this;
        }


    }

}
