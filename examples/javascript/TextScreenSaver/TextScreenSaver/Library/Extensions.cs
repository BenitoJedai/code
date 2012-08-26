﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.XML;
//using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;

namespace TextScreenSaver.js
{
    [Script]
    static class Extensions
    {



        public static int GetOffsetRight(this IHTMLElement e)
        {
            return e.offsetLeft + e.offsetWidth;
        }

        public static int ToInt32(this string e)
        {
            var dummy = 0;

            return int.Parse(e);
        }

        public static int ToInt32(this double e)
        {
            var dummy = 0;

            return System.Convert.ToInt32(e);
        }

        public static int Floor(this double f)
        {
            return Math.Floor(f).ToInt32();
        }

        public static Timer Delayed(this int i, Action e)
        {
            return new Timer(
                t => e(), i, 0
                );
        }

        public static Timer Delayed(this int i, Action<Timer> e)
        {
            return new Timer(
                t => e(t), i, 0
                );
        }

        public static Timer AsTimer(this int i, Action<Timer> e)
        {
            return Timer.Interval(t => e(t), i);
        }



        public static double Random(this double e)
        {
            return new System.Random().NextDouble() * e;
        }

        public static int Random(this int e)
        {
            return new System.Random().Next(e);
        }

        public static void KeepInCenter(this IHTMLElement e)
        {
            Action MoveToCenter =
                delegate
                {
                    var w = Native.Window.Width;
                    var h = Native.Window.Height;


                    e.SetCenteredLocation(Native.Document.body.scrollLeft + w / 2, Native.Document.body.scrollTop + h / 2);

                };

            Native.Window.onresize += delegate { MoveToCenter(); };

            MoveToCenter();
        }





        public static string ToConsole(this string e)
        {
            System.Console.WriteLine(e);

            return e;
        }



        public static string[] Split(this string e, string d, StringSplitOptions op)
        {
            return e.Split(new[] { d }, op);
        }

        public static string[] Split(this string e, string d)
        {
            return e.Split(new[] { d }, StringSplitOptions.None);
        }

        public static IXMLDocument SerializeToXML<T>(this T e)
                 where T : class, new()
        {
            return new IXMLSerializer<T>().Serialize(e);
        }



        public static void DownloadToXML(this string url, Action<IXMLHttpRequest> done)
        {
            new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.GET, url, i => done(i));
        }

        public static void DownloadToXML<T>(this string url, object[] KnownTypes, Action<T> done)
            where T : class, new()
        {
            url.DownloadToXML(
                r => done(r.responseXML.Deserialize<T>(KnownTypes))
            );

        }
    }
}
