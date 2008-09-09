using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using Mahjong.Shared;

namespace Mahjong.Code
{
	partial class VisibleLayout
	{
		[Script]
		public class EntryPair
		{
			public Entry Left;
			public Entry Right;
		}

		public EntryPair[] Pairs;

		private void LayoutUpdatePairs(Action Done)
		{
			// we need to loop this until we have

			var p = this.TilesInfo;

			Func<bool> Condition = () => p.Tiles.Length > 0;

			var Candidates = new Stack<Entry[]>();

			var RecoverCount = 0;

			Action<Action> ExtractCandidates =
				SignalNext =>
				{
					var c = p.Tiles.Where(k => !k.BlockingSiblings.Any()).Randomize().ToArray();

					if (c.Length == 1)
					{
						// bummer we need to do a rollback
						//Console.WriteLine("bummer!");

						var c_previous = Candidates.Pop();

						foreach (var v in c_previous)
						{
							// replace with previousle chosen pair member
							var p_alternate = new TilesInfoType(p.CountZ, p.Tiles.Replace(c[0], v).ToArray());
							var c_alternate = p_alternate.Tiles.Where(k => !k.BlockingSiblings.Any()).ToArray();

							if (c_alternate.Length > 1)
							{
								// we seem to have recovered from the situation
								RecoverCount++;

								Candidates.Push(c_previous.Replace(v, c[0]).ToArray());

								p = p_alternate;
								c = c_alternate;


								break;
							}
						}

						//throw new Exception("Invalid Layout");
					}

					// jsc is still missing Linq Skip command
					if (c.Length % 2 == 1)
					{
						//Console.WriteLine("leaving 1");
						c = c.Take(c.Length - 1).ToArray();
					}


					#region sanity check
					if (c.Length == 0)
						if (p.Tiles.Length > 0)
						{
							// bummer

							var Error = "Invalid Layout: " + new { c.Length, Tiles = p.Tiles.Length, Layout.Comment };

							Console.WriteLine(Error);

							// show the invalid state for debugging
							this.TilesInfo = p;

							foreach (var v in p.Tiles)
							{
								v.Tile.Continue(
									(VisibleTile k) => k.RedFilter.Opacity = 0.6
								);
							}

							Condition = () => false;

							1.AtDelay(SignalNext);

							return;
						}
					#endregion

					// jsc is still missing Linq Except command

					// save for next round
					var except_c = p.Tiles.Where(k => !c.Contains(k)).ToArray();

					p = new TilesInfoType(p.CountZ, except_c);

					Candidates.Push(c);
					//Console.WriteLine("candidates: " + c.Length);
					SignalNext();
				};

			ExtractCandidates.While(() => Condition())(
				delegate
				{
					var a = new List<EntryPair>();

					while (Candidates.Count > 0)
					{
						var ClonedEntries = Candidates.Pop();

						for (int i = 0; i < ClonedEntries.Length; i += 2)
						{
							a.Add(
								new EntryPair
								{
									Left = this.TilesInfo.TilesByPointer[ClonedEntries[i].Pointer],
									Right = this.TilesInfo.TilesByPointer[ClonedEntries[i + 1].Pointer]
								}
							);
						}


					}

					this.Pairs = a.ToArray();

					Done();
				}
			);
		}



	}
}
