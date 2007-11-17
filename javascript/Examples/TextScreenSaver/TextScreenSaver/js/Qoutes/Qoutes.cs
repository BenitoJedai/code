using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;

namespace TextScreenSaver.js.Qoutes
{
    namespace Extensions
    {
        [Script]
        public static class Qoutes
        {
            /// <summary>
            /// Returns the content line by line.
            /// </summary>
            /// <param name="e"></param>
            /// <returns></returns>
            public static string[] Lines(this Document e)
            {
                return e.Content.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();
            }

            public static Style ApplyTo(this Style e, IStyle s)
            {
                s.backgroundColor = e.BackgroundColor;
                s.color = e.Color;

                return e;
            }
        }
    }

    [Script, Serializable]
    public sealed class Style
    {
        public string BackgroundColor;
        public string Color;
        public string HoverColor;

    }

    [Script, Serializable]
    public sealed class Document
    {
        public string Source;
        public string Topic;
        public string Content;
        public Style Style;
        public string Count;

    }

    [Script, Serializable]
    public sealed class DocumentRef
    {
        public string Source;
        public Document Document;
    }

    [Script, Serializable]
    public sealed class DocumentList
    {
        public DocumentRef[] Documents;

    }

    [Script]
    public static class Settings
    {
        public static object[] KnownTypes = new object[]
        {
            new Qoutes.DocumentList(),
            new Qoutes.DocumentRef(),
            new Qoutes.Document(),
            new Qoutes.Style(),
        };

    }
}
