using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace MovieBlog.Server
{
	[Script]
	public class BasicPirateBaySearch
	{
		public readonly BasicWebCrawler Crawler;

		public BasicPirateBaySearch(BasicWebCrawler Crawler)
		{
			this.Crawler = Crawler;

			this.Crawler.DataReceived +=
				document =>
				{
					var results = document.IndexOf("<table id=\"searchResult\">");
					var headend = document.IndexOf("</thead>", results);
					var results_end = document.IndexOf("</table>", headend);

					int entryindex = -1;

					Action<Action<Entry, int>> ForEachEntry =
						AddEntry =>
						{
							#region ScanSingleResultOrReturn
							Func<int, int> ScanSingleResultOrReturn =
								offset =>
								{

									var itemstart = document.IndexOf("<tr>", offset);

									if (itemstart < 0)
										return offset;

									if (itemstart > results_end)
										return offset;

									var itemend = document.IndexOf("</tr>", itemstart);

									if (itemend < 0)
										return offset;

									if (itemend > results_end)
										return offset;

									var itemdata = document.Substring(itemstart, itemend - itemstart);



									//<tr>
									//<td class="vertTh"><a href="/browse/205" title="More from this category">Video &gt; TV shows</a></td>
									//<td><a href="/torrent/4727946/Heroes.S03E16.HDTV.XviD-XOR.avi" class="detLink" title="Details for Heroes.S03E16.HDTV.XviD-XOR.avi">Heroes.S03E16.HDTV.XviD-XOR.avi</a></td>
									//<td>Today&nbsp;04:55</td>
									//<td><a href="http://torrents.thepiratebay.org/4727946/Heroes.S03E16.HDTV.XviD-XOR.avi.4727946.TPB.torrent" title="Download this torrent"><img src="http://static.thepiratebay.org/img/dl.gif" class="dl" alt="Download" /></a><img src="http://static.thepiratebay.org/img/icon_comment.gif" alt="This torrent has 22 comments." title="This torrent has 22 comments." /><img src="http://static.thepiratebay.org/img/vip.gif" alt="VIP" title="VIP" style="width:11px;" /></td>
									//<td align="right">348.97&nbsp;MiB</td>
									//<td align="right">47773</td>
									//<td align="right">60267</td>

									//Console.WriteLine("<h1>Most Popular video</h1>");
									//Console.WriteLine("<table>");

									// type, name, uploaded, links, size, se, le

									var Fields = new BasicPirateBaySearch.Entry();

									Action<string> SetField = null;

									SetField = Type =>
									SetField = Name =>
									SetField = Time =>
									SetField = Links =>
									SetField = Size =>
									SetField = Seeders =>
									SetField = Leechers =>
									{

										Fields = new BasicPirateBaySearch.Entry
										{
											Type = Type,
											Name = Name,
											Time = Time,
											Links = Links,
											Size = Size,
											Seeders = Seeders,
											Leechers = Leechers
										};

										SetField = delegate { };
									};


									var ep = new BasicElementParser();

									ep.AddContent +=
										(value, index) =>
										{
											//Console.WriteLine("AddContent start #" + index);
											SetField(value);
											//Console.WriteLine("AddContent stop #" + index);
										};

									ep.Parse(itemdata, "td");

									entryindex++;

									if (AddEntry != null)
										AddEntry(Fields, entryindex);



									return itemend + 5;
								};
							#endregion


							ScanSingleResultOrReturn.ToChainedFunc((x, y) => y > x)(headend);
						};

					if (this.Loaded != null)
						this.Loaded(ForEachEntry);

				};
		}

		[Script]
		public class Entry
		{
			public string Type;
			public string Name;
			public string Time;
			public string Links;
			public string Size;
			public string Seeders;
			public string Leechers;
		}


		public event ForEachCallback<Entry> Loaded;

		
	}


	[Script]
	public delegate void ForEachCallback<T>(Action<Action<T, int>> ForEach);
}
