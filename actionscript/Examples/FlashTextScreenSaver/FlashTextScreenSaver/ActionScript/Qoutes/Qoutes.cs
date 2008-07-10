using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
//using ScriptCoreLib.JavaScript.DOM;

namespace FlashTextScreenSaver.ActionScript.Qoutes
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
                return e.Content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).Where(t => t != "").ToArray();
            }

            public static Style ApplyTo(this Style e, object s)
            {
                //s.backgroundColor = e.BackgroundColor;
                //s.color = e.Color;

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

        static object[] KnownTypesCache;

        public static object[] KnownTypes
        {
            get
            {
                // cannot use static auto init fields because
                // the static constructors might be executed in the wrong order for us
                // jsc could try to order cctor calls by dependency in the future

                if (KnownTypesCache == null)
                    KnownTypesCache = new object[]
                {
                    new Qoutes.DocumentList(),
                    new Qoutes.DocumentRef(),
                    new Qoutes.Document(),
                    new Qoutes.Style(),
                };

                return KnownTypesCache;

            }
        }



    }
}
