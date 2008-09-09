using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using ScriptCoreLib.Shared.Lambda;
using System.Windows.Media;

namespace ScriptCoreLib.Shared.Avalon.TextSuggestions
{
	partial class TextSuggestionsControl
	{

		Dictionary<string, Match[]> GetDataSelected_Cache = new Dictionary<string, Match[]>();


		[Script]
		public class Match
		{
			public string Data;

			public int Weight;
		}
		private Match[] GetDataSelected(string Filter)
		{
			if (GetDataSelected_Cache.ContainsKey(Filter))
				return GetDataSelected_Cache[Filter];

			var Filters = Filter.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

			var DataSelectedSource = from k in InternalSuggestions
									 let Subject = k.ToLower()
									 where Filter != Subject
									 let match =
										Filters.Aggregate(0,
											(seed, entry) =>
											{
												if (Subject.Contains(entry))
													return seed + 1;

												return seed;
											}
										 )
									 orderby match descending, k
									 select new Match { Data = k, Weight = match };
			var DataSelectedArray = DataSelectedSource.ToArray();
			var DataSelected = DataSelectedSource.Take(DataSelectedArray.Length.Min(MaxResults)).ToArray();

			GetDataSelected_Cache[Filter] = DataSelected;

			return DataSelected;
		}
	}
}
