using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.Extensions;
using Mahjong.Shared;

namespace Mahjong.Code
{
	[Script]
	public class LayoutsFuture
	{
		public readonly Future<Layout> FirstLoaded = new Future<Layout>();
		public readonly Future<Dictionary<string, Layout>> AllLoaded = new Future<Dictionary<string, Layout>>();

		public readonly Dictionary<string, Layout> ByComment = new Dictionary<string, Layout>();

		public event Action<int, int> Progress;

		public LayoutsFuture(string[] Files)
		{
			Files.ForEach(
				(string File, int Index, Action SignalNext) =>
				{
					if (Progress != null)
						Progress(Index, Files.Length);

					File.ToStringAsset(
						DataString =>
						{
							var e = new Layout(DataString);

							e.Source = File;

							ByComment[e.Comment] = e;

							if (FirstLoaded.CanSignal)
								FirstLoaded.Value = e;

							1.AtDelay(SignalNext);
						}
					);
				}

			)(
				delegate
				{
					AllLoaded.Value = ByComment;
				}
			);
		}
	}

}
