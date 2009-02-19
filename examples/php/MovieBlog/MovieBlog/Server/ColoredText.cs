using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace MovieBlog.Server
{
	[Script]
	public class ColoredText
	{
		public readonly string Source;

		public readonly string[] Background;

		public ColoredText(string Source)
		{
			this.Source = Source;
			this.Background = new string[Source.Length];
		}

		public void SetBackground(int offset, int length, string value)
		{
			for (int i = 0; i < length; i++)
			{
				if (string.IsNullOrEmpty(this.Background[i + offset]))
					this.Background[i + offset] = value;
			}
		}

		public override string ToString()
		{
			var w = new StringBuilder();

			for (int i = 0; i < Source.Length; i++)
			{
				var Color = this.Background[i];

				if (string.IsNullOrEmpty(Color))
				{
					w.Append(Source[i]);
				}
				else
				{
					w.Append("<span style='background-color: " + Color + ";'>");
					w.Append(Source[i]);
					w.Append("</span>");
				}
			}


			return w.ToString();
		}
	}
}
