using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using System.Diagnostics;

namespace Mahjong.Shared
{
	[Script]
	public class Layout
	{
		// this is basically a voxel

		public const int DefaultCountX = 34;
		public const int DefaultCountY = 20;

		public string Version;
		public string Comment;
		public string[][] Map;

		public Layout()
			: this(null)
		{

		}

		public Layout(string DataString)
		{
			this.DataString = DataString;
		}

		string _DataString;

		public string DataString
		{
			set
			{
				if (value == null)
					return;

		
				_DataString = value;

				using (var s = new StringReader(value))
				{

					this.Version = s.ReadLine();

					if (this.Version == null)
						throw new Exception("Version missing");

					this.Comment = s.ReadLine();

					if (this.Comment == null)
						throw new Exception("Comment missing");

					var MapString = s.ReadLine();

					if (MapString == null)
						throw new Exception("MapString missing");

					if (MapString.Length < Layout.DefaultCountX * Layout.DefaultCountY)
						throw new Exception("MapString too small - " + MapString);


					this.MapString = MapString;
				}
			}
			get
			{
				return _DataString;
			}
		}

		string _MapString;
		string[] _MapString_Split;

		public string MapString
		{
			get
			{
				return _MapString;
			}

			set
			{
				_MapString = value;
				_MapString_Split = value.Split(Layout.DefaultCountX);

				Map = _MapString_Split.Split(Layout.DefaultCountY);

				Validate();
			}
		}

		private void Validate()
		{
			//Console.WriteLine("Will validate layout " + new { CountZ, _MapString.Length } );

			if (CountZ != 5)
				throw new Exception("expected 5");

			for (int z = 0; z < CountZ; z++)
			{
				var by_z = Map[z];

				if (by_z == null)
				{
					throw new Exception("by_z is missing");
				}


				for (int y = 0; y < Layout.DefaultCountY; y++)
				{
					var by_y = by_z[y];

					if (by_y == null)
					{
						throw new Exception("by_y is missing " + new { z, y, by_z.Length });
					}

					if (by_y.Length != Layout.DefaultCountX)
						throw new Exception("by_y too small " + new { CountZ, z, y, by_y, _MapString });

					//Console.WriteLine("" + new { z, y, by_y });

				}
			}
		}

		public int CountZ
		{
			get
			{
				return Map.Length;
			}
		}

		public bool this[Entry i]
		{
			get
			{
				return this[i.x, i.y, i.z];
			}

		}
		public bool this[int x, int y, int z]
		{
			get
			{
				return Map[z][y][x] == '1';
			}
		}

		[Script]
		public class Entry
		{
			public int index;

			public int x;
			public int y;
			public int z;
		}

		Entry[] _Tiles;

		public Entry[] Tiles
		{
			get
			{
				if (_Tiles == null)
				{
					var a = new List<Entry>();

					for (int z = 0; z < this.CountZ; z++)
						for (int x = Layout.DefaultCountX - 1; x >= 0; x--)
							for (int y = 0; y < Layout.DefaultCountY; y++)
							{
								if (this[x, y, z])
									a.Add(new Entry { x = x, y = y, z = z, index = a.Count });
							}


					_Tiles = a.ToArray();
				}


				return _Tiles;
			}
		}
	}

}
