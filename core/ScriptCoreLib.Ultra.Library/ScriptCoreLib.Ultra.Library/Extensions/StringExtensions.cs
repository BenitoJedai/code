using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Extensions
{
    public static class StringExtensions
    {
        public static string ToCharacterEllipsis(this string e, int length = 48)
        {
            if (e.Length < length)
                return e;

            return e.Substring(0, length - 1) + "…";
        }

        public static string[] ToLines(this string e)
        {
            return e.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public static string SkipUntilLastIfAny(this string e, string u)
        {
            if (e == null)
                return e;

            var i = e.LastIndexOf(u);

            if (i < 0)
                return e;

            return e.Substring(i + u.Length);
        }


        public static string SkipUntilLastOrEmpty(this string e, string u)
        {
            if (e == null)
                return "";

            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.Select.cs

            var i = e.LastIndexOf(u);

            if (i < 0)
                return "";

            return e.Substring(i + u.Length);
        }

        public static string SkipUntilIfAny(this string e, string u)
        {
            if (u == null)
                return e;

            var i = e.IndexOf(u);

            if (i < 0)
                return e;

            return e.Substring(i + u.Length);
        }

        public static string SkipUntilBeforeOrEmpty(this string e, string u)
        {
            if (null == e)
                return "";

            if (u == null)
                return "";


            var i = e.IndexOf(u);

            if (i < 0)
                return "";

            return e.Substring(i);
        }

        public static string SkipUntilOrEmpty(this string e, string u)
        {
            if (null == e)
                return "";

            if (u == null)
                return "";


            var i = e.IndexOf(u);

            if (i < 0)
                return "";

            return e.Substring(i + u.Length);
        }

        public static string TakeUntilIfAny(this string e, string u)
        {
            if (e == null)
                return null;

            var i = e.IndexOf(u);

            if (i < 0)
                return e;

            return e.Substring(0, i);
        }

        public static string TakeUntilOrEmpty(this string e, string u)
        {
            var i = e.IndexOf(u);

            if (i < 0)
                return "";

            return e.Substring(0, i);
        }

        public static string TakeUntilOrNull(this string e, string u)
        {
            var i = e.IndexOf(u);

            if (i < 0)
                return null;

            return e.Substring(0, i);
        }


        public static string TakeUntilLastIfAny(this string e, string u)
        {
            var i = e.LastIndexOf(u);

            if (i < 0)
                return e;

            return e.Substring(0, i);
        }


        public static string TakeUntilLastOrEmpty(this string e, string u)
        {
            var i = e.LastIndexOf(u);

            if (i < 0)
                return "";

            return e.Substring(0, i);
        }

        public static string TakeUntilLastOrNull(this string e, string u)
        {
            var i = e.LastIndexOf(u);

            if (i < 0)
                return null;

            return e.Substring(0, i);
        }

        public static string ToHexString(this byte[] e)
        {
            var w = new StringBuilder();

            foreach (var v in e)
            {
                w.Append(v.ToHexString());
            }

            return w.ToString();
        }

        [Obsolete(".tostring x2")]

        public static string ToHexString(this byte e)
        {
            const string u = "0123456789abcdef";

            return u.Substring((e >> 4) & 0xF, 1) + u.Substring((e >> 0) & 0xF, 1);
        }

        public static void AtIndecies(this string e, string target, AtIndeciesDelegate h)
        {
            // X:\jsc.svn\examples\javascript\Test\Test453AtIndeciesWhile\Test453AtIndeciesWhile\Class1.cs
            // X:\jsc.svn\examples\javascript\css\Test\TestLongWebMethod\TestLongWebMethod\ApplicationWebService.cs

            var i = e.IndexOf(target);
            var YieldIndex = -1;
            while (i >= 0)
            {
                YieldIndex++;

                Action YieldBreak = () => i = -1;

                h(
                    new AtIndeciesArguments
                    {
                        e = e,
                        i = i,
                        target = target,
                        YieldIndex = YieldIndex,
                        YieldBreak = YieldBreak
                    }
                );


                if (i >= 0)
                    i = e.IndexOf(target, i + target.Length);
            }
        }


    }

    public class AtIndeciesArguments
    {
        public string e;
        public string target;
        public int i;

        public int YieldIndex;

        public Action YieldBreak;
    }

    public delegate void AtIndeciesDelegate(AtIndeciesArguments a);



    public static class StringAsFilePathExtensions
    {
    }
}
