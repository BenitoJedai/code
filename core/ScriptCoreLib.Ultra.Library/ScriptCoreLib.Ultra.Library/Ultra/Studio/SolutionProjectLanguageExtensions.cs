﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace ScriptCoreLib.Ultra.Studio
{
	public static class SolutionProjectLanguageExtensions
	{
		/// <summary>
		/// sadsad
		/// </summary>
		/// <param name="that">fsdfsdf</param>
		/// <param name="File">sdfsf</param>
		/// <param name="comment">sdg</param>
		public static void WriteComment(this SolutionProjectLanguage that, SolutionFile File, XElement comment)
		{
			var x = comment.ToString();

			var r = new StringReader(x);

			var n = r.ReadLine();

			while (n != null)
			{
				that.WriteIndent(File);
				that.WriteXMLCommentLine(File, n);

				n = r.ReadLine();
			}
		}

		public static void WriteComment(this SolutionProjectLanguage that, SolutionFile File, params XElement[] comments)
		{
			foreach (var comment in comments)
			{
				that.WriteComment(File, comment);
			}
		}

		public static void WriteComment(this SolutionProjectLanguage that, SolutionFile File, string summary)
		{
			that.WriteComment(File, summary, null);
		}

		public static void WriteComment(this SolutionProjectLanguage that, SolutionFile File, string summary, Dictionary<string, string> @params)
		{
			var c = new List<XElement>();

			c.Add(new XElement("summary", "\n" + summary + "\n"));

			if (@params != null)
				foreach (var item in @params)
				{
					c.Add(
						new XElement("param",
							new XAttribute("name", item.Key),
							item.Value
						)
					);
				}

			that.WriteComment(File, c.ToArray());
		}
	}
}
