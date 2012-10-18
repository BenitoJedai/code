using FastTextureExtraction.ActionScript.Images;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared;

namespace FastTextureExtraction.Components
{
    internal sealed class MySprite1 : HTest
    {
        public const int DefaultWidth = 600;
        public const int DefaultHeight = 600;

        // http://wonderfl.net/c/vbla

        public MySprite1()
        {

        }

    }

    public class HTest : Sprite
    {

        public BitmapData image;
        Vector<Anchor> anchors;
        Homography homography;

        public void onMouseMove(MouseEvent e)
        {
            
            if ((e == null)||( e.buttonDown))
            {
                homography.setTransform(image,
                    new Point(anchors[0].x, anchors[0].y),
                    new Point(anchors[1].x, anchors[1].y),
                    new Point(anchors[2].x, anchors[2].y),
                    new Point(anchors[3].x, anchors[3].y)
                );
            }
        }

        public HTest()
        {

            var roofs = new roofs().AttachTo(this);

            image = roofs.bitmapData;
            anchors = new Vector<Anchor>(4, true);
            anchors[0] = new Anchor(100, 257).AttachTo(this);
            anchors[1] = new Anchor(122, 218).AttachTo(this);
            anchors[2] = new Anchor(131, 251).AttachTo(this);
            anchors[3] = new Anchor(111, 296).AttachTo(this);
            homography = new Homography().AttachTo(this);
            homography.x = homography.y = 350;
            homography.filters = new[] { new GlowFilter(0xFF7F00, 1, 2, 2, 200, 2) };


            stage.mouseMove += onMouseMove;

            onMouseMove(null);

        }



    }

    class Anchor : Sprite
    {
        public Anchor(int x0, int y0)
        {
            x = x0; y = y0;
            graphics.beginFill(0xFF7F00, 1);
            graphics.drawRect(-4, -4, 8, 8);
            graphics.drawRect(-2, -2, 4, 4);
            graphics.beginFill(0xFF7F00, 0);
            graphics.drawRect(-2, -2, 4, 4);
            useHandCursor = true;
            buttonMode = true;

            this.mouseDown += delegate { startDrag(); };
            this.mouseUp += delegate { stopDrag(); };
        }

    }
 
    class Homography : Shape
    {
        private Vector<int> v6 = new[] { 0, 1, 2, 0, 2, 3 };
        private Vector<double> v8 = new Vector<double>(8, true);
        private Vector<double> v12 = new Vector<double>(12, true);

        public void setTransform(BitmapData src,
            Point p0, Point p1, Point p2, Point p3,
            int destWidth = 100, int destHeight = 100)
        {

            // Find diagonals intersection point
            var pc = new Point();

            var a1 = p2.y - p0.y;
            var b1 = p0.x - p2.x;
            var a2 = p3.y - p1.y;
            var b2 = p1.x - p3.x;

            var denom = a1 * b2 - a2 * b1;
            if (denom == 0)
            {
                // something is better than nothing
                pc.x = 0.25 * (p0.x + p1.x + p2.x + p3.x);
                pc.y = 0.25 * (p0.y + p1.y + p2.y + p3.y);
            }
            else
            {
                var c1 = p2.x * p0.y - p0.x * p2.y;
                var c2 = p3.x * p1.y - p1.x * p3.y;
                pc.x = (b1 * c2 - b2 * c1) / denom;
                pc.y = (a2 * c1 - a1 * c2) / denom;
            }

            // Lengths of first diagonal
            var ll1 = Point.distance(p0, pc);
            var ll2 = Point.distance(pc, p2);

            // Lengths of second diagonal
            var lr1 = Point.distance(p1, pc);
            var lr2 = Point.distance(pc, p3);

            // Ratio between diagonals
            var f = (ll1 + ll2) / (lr1 + lr2);

            var sw = src.width;
            var sh = src.height;
            var dw = destWidth;
            var dh = destHeight;

            v8[2] = dw; v8[4] = dw; v8[5] = dh; v8[7] = dh;

            v12[0] = p0.x / sw; v12[1] = p0.y / sh; v12[2] = ll2 / f;
            v12[3] = p1.x / sw; v12[4] = p1.y / sh; v12[5] = lr2;
            v12[6] = p2.x / sw; v12[7] = p2.y / sh; v12[8] = ll1 / f;
            v12[9] = p3.x / sw; v12[10] = p3.y / sh; v12[11] = lr1;

            graphics.clear();
            graphics.beginBitmapFill(src, null, false, true);
            graphics.drawTriangles(v8, v6, v12);
        }


  
    }
}
