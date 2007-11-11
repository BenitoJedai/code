using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace NatureBoy.js.Scene
{
    [Script, Serializable]
    public sealed class Image
    {
        public string Source;

    }

    [Script, Serializable]
    public sealed class Item
    {
        public string Text;

        public string X;
        public string Y;
        public string R;

    }

    [Script, Serializable]
    public sealed class Frame
    {
        public Item[] Items;

        public Image Image;

        public string Name;

        public string Left;
        public string Right;
        public string Top;
        public string Bottom;
    }

    [Script, Serializable]
    public sealed class Document
    {
        public string IntroText;
        public string TextDelimiter;
        public string SlowText;

        public Frame[] Frames;
    }

    [Script]
    public static class Settings
    {
        public static object[] KnownTypes = new object[]
                        {
                            new Scene.Document(),
                            new Scene.Frame(),
                            new Scene.Image(),
                            new Scene.Item()
                        };
    }
}
