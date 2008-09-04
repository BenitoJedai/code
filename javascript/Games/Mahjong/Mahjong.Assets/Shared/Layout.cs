using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;

namespace Mahjong.Shared
{
	[Script]
	public class Layout
	{
		// this is basically a voxel

		public const int CountX = 20;
		public const int CountY = 34;

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

		public string DataString
		{
			set
			{
				if (value == null)
					return;

				using (var s = new StringReader(value))
				{

					this.Version = s.ReadLine();
					this.Comment = s.ReadLine();
					this.MapString = s.ReadLine();

				}
			}
		}

		public string MapString
		{
			set
			{
				Map = value.Split(Layout.CountY).Split(Layout.CountX);
			}
		}

		public int CountZ
		{
			get
			{
				return Map.Length;
			}
		}

		public bool this[int x, int y, int z]
		{
			get
			{
				return Map[z][x][y] == '1';
			}
		}
	}

}
