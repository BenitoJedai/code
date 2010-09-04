using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio
{
	public static class SolutionProjectLanguageExtensions
	{
		public static void WriteIndent(this SolutionFile File)
		{
			File.IndentStack.Invoke();
		}

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
				File.WriteIndent();
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

		public static void WriteIndentedComment(this SolutionProjectLanguage that, SolutionFile File, string summary)
		{
			foreach (var item in summary.ToLines())
			{
				File.WriteIndent();
				that.WriteCommentLine(File, item);
			}
		}

		public static void WriteSummary(this SolutionProjectLanguage that, SolutionFile File, string summary)
		{
            if (string.IsNullOrEmpty(summary))
                return;

			that.WriteSummary(File, summary, null);
		}

		public static void WriteSummary(this SolutionProjectLanguage that, SolutionFile File, string summary, SolutionProjectLanguageArgument[] @params)
		{
			var c = new List<XElement>();

			c.Add(new XElement("summary", "\n" + summary + "\n"));

			if (@params != null)
				foreach (var item in @params)
				{
					c.Add(
						new XElement("param",
							new XAttribute("name", item.Name),
							item.Summary
						)
					);
				}

			that.WriteComment(File, c.ToArray());
		}
	}
}
