using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace Mahjong.Shared
{
	[Script]
	public class RankAsset : AbstractAsset
	{
		public string Suit;
		public string Rank;

		public string ResourceAlias
		{
			get
			{
				return this.Source + "/" + this.Suit + "/" + this.Rank + ".png";
			}
		}
	}


	[Script]
	public abstract class AbstractAsset
	{
		public string Source = "assets/Mahjong";


		[Script]
		public class Settings
		{
			public double Scale = 1;

			public int ShadowWidth = 32 + 8;
			public int ShadowHeight = 46 + 8;

			public int OuterWidth = 32;
			public int OuterHeight = 46;

			public int InnerWidth = 24;
			public int InnerHeight = 38;

			public SpecialAsset BackgroundTile = "tile0";
			public SpecialAsset BackgroundTileBlack = "tile0_black";
			public SpecialAsset BackgroundTileShadow = "tile8_black";
			public SpecialAsset BackgroundTileYellow = "tile8_yellow";
			public SpecialAsset BackgroundTileGreen = "tile8_green";

			public double ScaledShadowHeight
			{
				get
				{
					return ShadowHeight * Scale;
				}
			}


			public double ScaledShadowWidth
			{
				get
				{
					return ShadowWidth * Scale;
				}
			}

			public double ScaledOuterWidth
			{
				get
				{
					return (int)((double)OuterWidth * Scale);
				}
			}

			public double ScaledOuterHeight
			{
				get
				{
					return (int)((double)OuterHeight * Scale);
				}
			}

			public int Spacing = 6;

			public double ScaledSpacing
			{
				get
				{
					return Spacing * Scale;

				}
			}

			public double ScaledInnerWidth
			{
				get
				{
					return (int)((double)InnerWidth * Scale);
				}
			}

			public double ScaledInnerHeight
			{
				get
				{
					return (int)((double)InnerHeight * Scale);
				}
			}

			public double ScaledBorderWidth
			{
				get
				{
					return (int)((double)(OuterWidth - InnerWidth) * Scale);
				}
			}

			public double ScaledBorderHeight
			{
				get
				{
					return (int)((double)(OuterHeight - InnerHeight) * Scale);
				}
			}
		}

		static public RankAsset[] Bamboo { get { return BambooAsset.Collection; } }
		static public RankAsset[] Characters { get { return CharacterAsset.Collection; } }
		static public RankAsset[] Dots { get { return DotsAsset.Collection; } }
		static public RankAsset[] Dragons { get { return DragonAsset.Collection; } }
		static public RankAsset[] Flowers { get { return FlowerAsset.Collection; } }
		static public RankAsset[] Seasons { get { return SeasonAsset.Collection; } }
		static public RankAsset[] Winds { get { return WindAsset.Collection; } }

	}

	[Script]
	public class SpecialAsset : RankAsset
	{
		public SpecialAsset()
		{
			this.Suit = "special";
		}

		public static implicit operator SpecialAsset(string Rank)
		{
			return new SpecialAsset { Rank = Rank };
		}
	}

	[Script]
	class BambooAsset : RankAsset
	{
		public BambooAsset()
		{
			this.Suit = "bamboo";
		}
		public static RankAsset[] Collection
		{
			get
			{
				return new BambooAsset[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
			}
		}

		public static implicit operator BambooAsset(string Rank)
		{
			return new BambooAsset { Rank = Rank };
		}
	}

	[Script]
	class CharacterAsset : RankAsset
	{
		public CharacterAsset()
		{
			this.Suit = "characters";
		}

		public static RankAsset[] Collection
		{
			get
			{
				return new CharacterAsset[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", 
                               };
			}
		}

		public static implicit operator CharacterAsset(string Rank)
		{
			return new CharacterAsset { Rank = Rank };
		}
	}

	[Script]
	class DragonAsset : RankAsset
	{
		public DragonAsset()
		{
			this.Suit = "dragons";
		}

		public static RankAsset[] Collection
		{
			get
			{
				return new DragonAsset[] { "c", "f" };
			}
		}

		public static implicit operator DragonAsset(string Rank)
		{
			return new DragonAsset { Rank = Rank };
		}
	}

	[Script]
	class WindAsset : RankAsset
	{
		public WindAsset()
		{
			this.Suit = "winds";
		}

		public static RankAsset[] Collection
		{
			get
			{
				return new WindAsset[] { "e", "n", "s", "w" };
			}
		}

		public static implicit operator WindAsset(string Rank)
		{
			return new WindAsset { Rank = Rank };
		}
	}

	[Script]
	class DotsAsset : RankAsset
	{
		public DotsAsset()
		{
			this.Suit = "dots";
		}

		public static RankAsset[] Collection
		{
			get
			{
				return new DotsAsset[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
			}
		}

		public static implicit operator DotsAsset(string Rank)
		{
			return new DotsAsset { Rank = Rank };
		}
	}

	[Script]
	class SeasonAsset : RankAsset
	{
		public SeasonAsset()
		{
			this.Suit = "seasons";
		}

		public static RankAsset[] Collection
		{
			get
			{
				return new SeasonAsset[] { "aut", "spr", "sum", "win" };
			}
		}

		public static implicit operator SeasonAsset(string Rank)
		{
			return new SeasonAsset { Rank = Rank };
		}
	}

	[Script]
	class FlowerAsset : RankAsset
	{
		public FlowerAsset()
		{
			this.Suit = "flowers";
		}

		public static RankAsset[] Collection
		{
			get
			{
				return new FlowerAsset[] { "bam", "flum", "mum", "orc" };
			}
		}

		public static implicit operator FlowerAsset(string Rank)
		{
			return new FlowerAsset { Rank = Rank };
		}
	}
}
