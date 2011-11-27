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
			var i = e.LastIndexOf(u);

			if (i < 0)
				return e;

			return e.Substring(i + u.Length);
		}


		public static string SkipUntilLastOrEmpty(this string e, string u)
		{
			var i = e.LastIndexOf(u);

			if (i < 0)
				return "";

			return e.Substring(i + u.Length);
		}

		public static string SkipUntilIfAny(this string e, string u)
		{
			var i = e.IndexOf(u);

			if (i < 0)
				return e;

			return e.Substring(i + u.Length);
		}


		public static string SkipUntilOrEmpty(this string e, string u)
		{
			var i = e.IndexOf(u);

			if (i < 0)
				return "";

			return e.Substring(i + u.Length);
		}

		public static string TakeUntilIfAny(this string e, string u)
		{
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

		public static string ToHexString(this byte e)
		{
			const string u = "0123456789abcdef";

			return u.Substring((e >> 4) & 0xF, 1) + u.Substring((e >> 0) & 0xF, 1);
		}

		public static void AtIndecies(this string e, string target, AtIndeciesDelegate h)
		{
			var i = e.IndexOf(target);
			var YieldIndex = -1;
			while (i >= 0)
			{
				YieldIndex++;

				h(
					new AtIndeciesArguments
					{
						e = e,
						i = i,
						target = target,
						YieldIndex = YieldIndex,
						YieldBreak = () => i = -1
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
        public static Uri GetNearestRepositryLocation(this string path)
        {
            var p = path;
            var u = default(Uri);

            while (p != null)
            {
                u = p.GetRepositryLocation();

                if (u != null)
                    break;

                p = p.TakeUntilLastOrNull(@"\");
            }

            return u;
        }

        public static Uri GetRepositryLocation(this string path)
        {
            return path.GetRepositryLocation(".git/config", k => k.SkipUntilOrEmpty("url = ")) ??
            path.GetRepositryLocation(".hg/hgrc", k => k.SkipUntilOrEmpty("default = ")) ??
            path.GetRepositryLocation(".svn/entries", k => k.SkipUntilOrEmpty("\n").SkipUntilOrEmpty("\n").SkipUntilOrEmpty("\n").SkipUntilOrEmpty("\n"));
        }

        public static Uri GetRepositryLocation(this string path, string FileNamePartialHint, Func<string, string> SkipToServerHint)
        {
            if (path.EndsWith("\\"))
                path = path.Substring(0, path.Length - 1);

            var FileNameHint = Path.Combine(path, FileNamePartialHint);

            var ParentFile = Directory.GetParent(path);

            if (!File.Exists(FileNameHint))
                return null;
            if (ParentFile != null)
            {
                var ParentFileNameHint = Path.Combine(ParentFile.FullName, FileNamePartialHint);

                if (File.Exists(ParentFileNameHint))
                    return null;
            }

            var data = File.ReadAllText(FileNameHint);
            var value = SkipToServerHint(data)
                .TakeUntilOrEmpty("\n")
                .Trim();



            if (string.IsNullOrEmpty(value))
                return null;

            // file:///Y:/opensource/%23codeplex/sfeir/GoogleSite/.git/config
            if (value.Contains("@"))
                return null;


            return new Uri(value);
        }
    }
}
