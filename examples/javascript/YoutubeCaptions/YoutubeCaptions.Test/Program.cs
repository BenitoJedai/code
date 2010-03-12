using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoutubeCaptions.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			"".ToCaptions(
				x =>
				{
					foreach (var item in x.Root.Elements("text"))
					{
						Console.WriteLine(
							item.Value
						);
					}
				}
			);
		}
	}
}
