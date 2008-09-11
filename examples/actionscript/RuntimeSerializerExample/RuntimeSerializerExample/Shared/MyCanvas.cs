using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;

namespace RuntimeSerializerExample.Shared
{


	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 480;


		public MyCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			Colors.White.ToGradient(Colors.Gray, DefaultHeight / 4).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();

			var t = new TextBox
			{
				AcceptsReturn = true,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0),
				Foreground = Brushes.Black,
				Background = Brushes.White,
				IsReadOnly = true,
				Width = 300,
				Height = 300
			}.MoveTo(32, 32).AttachTo(this);

			var m = new MemoryStream();
			var w = new BinaryWriter(m);

			w.Write((byte)6);
			w.Write((short)7);
			w.Write((int)8);
			w.Write("hey Ԉ \ufffc \u00ff \u0100");

			t.AppendTextLine();

			foreach (var v in m.ToArray())
			{
				t.AppendText(v + " ");
			}

			m.Position = 0;

			var r = new BinaryReader(m);

			t.AppendTextLine();
			t.AppendTextLine("bytes: " + r.BaseStream.Length);
			t.AppendTextLine("byte " + r.ReadByte());
			t.AppendTextLine("short " + r.ReadInt16());
			t.AppendTextLine("int " + r.ReadInt32());
			t.AppendTextLine("string " + r.ReadString());
		}
	}
}
