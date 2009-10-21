using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SimpleChat
{

	public class IncomingDataArguments
	{
		public string PathAndQuery;

		public string Text;

		public StringAction SetLogText;

		public WebServer Server;

		public string Path
		{
			get
			{
				var q = this.PathAndQuery.IndexOf("?");
				if (q < 0)
					return this.PathAndQuery;

				return this.PathAndQuery.Substring(0, q);
			}
		}

		public string Query
		{
			get
			{
				var q = this.PathAndQuery.IndexOf("?");
				if (q < 0)
					return "";

				return this.PathAndQuery.Substring(q + 1);
			}
		}

		public KeyValuePair[] Parameters
		{
			get
			{
				var a = new ArrayList();

				var q = Query.Split('&');

				foreach (var item in q)
				{
					var e = item.IndexOf("=");

					if (e > 0)
					{
						a.Add(
							new KeyValuePair
							{
								Key = item.Substring(0, e),
								// decode?
								Value = item.Substring(e + 1).Replace("%20", " ")
							}
						);
					}
				}

				return (KeyValuePair[])a.ToArray(typeof(KeyValuePair));
			}
		}

		public class KeyValuePair
		{
			public string Key;
			public string Value;
		}

		public string[] GetArguments()
		{
			var Parameters = this.Parameters;

			var s = new string[Parameters.Length + 1];

			s[0] = this.Path.Substring(1);

			for (int i = 0; i < Parameters.Length; i++)
			{
				var item = Parameters[i];

				s[i + 1] = "/" + item.Key + ":" + item.Value;
			}
				

			return s;
		}
	}
}
