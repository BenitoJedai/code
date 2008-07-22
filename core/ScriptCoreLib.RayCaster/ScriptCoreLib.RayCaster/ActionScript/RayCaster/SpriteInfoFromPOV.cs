using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.RayCaster
{
    [Script]
    public class SpriteInfoFromPOV
    {
        public Point RelativePosition;

        public SpriteInfo Sprite;

        public double Direction;

        public readonly ViewInfo ViewInfo = new ViewInfo();

        public double Distance;

        public SpriteInfoFromPOV(SpriteInfo s)
        {
            Sprite = s;




        }

        public void Update(double x, double y, double left, double right)
        {
            RelativePosition = new Point
            {
                x = Sprite.Position.x - x,
                y = Sprite.Position.y - y
            };

           

            Direction = RelativePosition.GetRotation();

            Distance = RelativePosition.length;

            ViewInfo.Left = left;
            ViewInfo.Right = right;
            ViewInfo.Target = Direction;

            ViewInfo.Update();

        }
    }

}
