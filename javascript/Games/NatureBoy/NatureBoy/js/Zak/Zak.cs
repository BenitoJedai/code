using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace NatureBoy.js.Zak
{
    [Script]
    public static class Settings
    {


        public static Type[] KnownTypes
        {
            get
            {
                return new [] 
                    {
                        typeof(ImageRef), 
                        typeof(ItemRef),
                        typeof(Point),
                        typeof(Document),
                        typeof(Rect),
                        typeof(SpriteInfo),
                        typeof(WorldInfo)
                    };
            }
        }
    }


    [Script, Serializable]
    public sealed class ItemRef
    {
        public string Name;
        public string Description;
        public string ImageName;

        public Point Position;


    }

    [Script, Serializable]
    public sealed class ImageRef
    {
        public string Name;
        public string Source;

        public Point Size;
    }

    [Script, Serializable]
    public sealed class Point
    {
        public string X;
        public string Y;

        public Point()
        {

        }

        public int Xint { get { return X.ToInt32(); } }
        public int Yint { get { return Y.ToInt32(); } }

        public Point(int x, int y)
        {
            this.X = "" + x;
            this.Y = "" + y;
        }
    }

    [Script, Serializable]
    public sealed class Rect
    {
        public Point From;
        public Point To;

        public Rect()
        {

        }

        public Rect(int x, int y, int cx, int cy)
        {
            From = new Point { X = "" + x, Y = "" + y };
            To = new Point { X = "" + cx, Y = "" + cy };
        }

        public Point Size
        {
            get
            {
                return new Point(To.Xint - From.Xint, To.Yint - From.Yint);
            }
        }
    }

    [Script, Serializable]
    public sealed class Document
    {
        public ImageRef[] Images;

        public Point ViewPortSize;
        public string ViewPortZoom;

        public ItemRef[] Items;

        public string BackgroundImageName;

        public Point EntryPoint;

        public Rect[] WalkArea;
    }



    [Script, Serializable]
    public sealed class SpriteInfo
    {
        public int Room { get { return -1; } }
        public int Object { get { return -1; } }
        public int Costume { get { return -1; } }
        public int Frame { get { return -1; } }

        public string Value;

        public static implicit operator SpriteInfo(string e)
        {
            return new SpriteInfo { Value = e };
        }
    }

    [Script, Serializable]
    public sealed class WorldInfo
    {
        public string AssetsLocation;

        public SpriteInfo[] Sprites;

        public string BackgroundColor;
        public string TextColor;

        public Point ControlSize;

        public string Zoom;

        public Rect ClientRect;

        public string ClientRectColor;
    }
}
