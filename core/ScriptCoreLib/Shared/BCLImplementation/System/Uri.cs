using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Uri))]
	internal class __Uri
	{
		//public static readonly string SchemeDelimiter = "://";

		public string OriginalString { get; set; }

		public string Scheme { get; set; }

		public string PathAndQuery { get; set; }

		public string Host { get; set; }

		public string Query { get; set; }
		public string AbsolutePath { get; set; }

		public string[] Segments { get; set; }

		public __Uri(string uriString)
		{
			this.OriginalString = uriString;

			// http://localhost/jsc/zmovies

			const string SchemeDelimiter = "://";

			var scheme_i = uriString.IndexOf(SchemeDelimiter);

			this.Scheme = uriString.Substring(0, scheme_i);

			var path_i = uriString.IndexOf("/", scheme_i + SchemeDelimiter.Length);

			this.Host = uriString.Substring(scheme_i + SchemeDelimiter.Length, path_i - (scheme_i + SchemeDelimiter.Length));

			this.PathAndQuery = uriString.Substring(path_i);

			var query_i = this.PathAndQuery.IndexOf("?");

			if (query_i < 0)
			{
				this.Query = "";
				this.AbsolutePath = this.PathAndQuery;
			}
			else
			{
				this.Query = this.PathAndQuery.Substring(query_i + 1);
				this.AbsolutePath = this.PathAndQuery.Substring(0, query_i);
			}

			var a = new List<string>();

			var j = 0;
			var i = this.AbsolutePath.IndexOf("/");

			while (j >= 0)
			{
				i = this.AbsolutePath.IndexOf("/", j);

				if (i >= 0)
				{
					a.Add(this.AbsolutePath.Substring(j, i - j + 1));
					j = i + 1;
				}
				else
				{
					if (j < this.AbsolutePath.Length - 1)
						a.Add(this.AbsolutePath.Substring(j));

					j = -1;
				}
			}

			this.Segments = a.ToArray();
		}

		public static bool operator !=(__Uri uri1, __Uri uri2)
		{
			object o1 = uri1;
			object o2 = uri2;

			if (o1 == null)
				return o1 != o2;

			if (o2 == null)
				return o1 != o2;

			return uri1.OriginalString == uri2.OriginalString;
		}

		public static bool operator ==(__Uri uri1, __Uri uri2)
		{
			object o1 = uri1;
			object o2 = uri2;

			if (o1 == null)
				return o1 == o2;

			if (o2 == null)
				return o1 == o2;

			return uri1.OriginalString == uri2.OriginalString;
		}

		public override string ToString()
		{
			return this.OriginalString;
		}
	}
}
