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
			"".ToCaptionTuples(
				x =>
				{
					Console.WriteLine(
						x
					);
				}
			);
		}
	}
}
