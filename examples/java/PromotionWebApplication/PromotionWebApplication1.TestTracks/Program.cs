using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PromotionWebApplication1.Services;

namespace PromotionWebApplication1.TestTracks
{
	class Program
	{
		static void Main(string[] args)
		{
			var w = new UltraWebService();
			
			w.SoundCloudTracksDownload("4",
				e =>
				{
					Console.WriteLine(e.trackName);
				}
			);
		}
	}
}
