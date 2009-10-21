using ScriptCoreLib;

using java.lang;
using java.applet;
using java.awt;
using java.awt.@event;
using javax.common.runtime;
using System;
using ScriptCoreLib.Shared.Maze;

namespace MazeApplet.source.java
{



	[Script]
	public partial class MazeApplet : Applet, MazeGenerator.IFeedback
	{
		// http://www.astrolog.org/labyrnth/algrithm.htm
		// http://en.wikipedia.org/wiki/Maze_generation_algorithm
		// http://en.wikipedia.org/wiki/Image:Maze.png
		// http://www.siteexperts.com/tips/functions/ts20/page3.asp

		MazeGenerator Maze;

		public override void init()
		{
			this.InitializeComponents();

			base.resize(Settings.DefaultWidth, Settings.DefaultHeight);

			//try
			//{
				//System.Console.WriteLine("MazeGenerator");
				Maze = new MazeGenerator(20, 20, this);

			//}
			//catch (csharp.ThrowableException ex)
			//{
			//    Error = ex.Message;

			//}
		}

		string Error = "";

		static Color GetBlue(double b)
		{
			int u = (int)(0xff * b);

			return new Color(u);
		}

		public override void paint(global::java.awt.Graphics g)
		{
			// old school gradient :)

			var h = this.getHeight();
			var w = this.getWidth();

			for (int i = 0; i < h; i++)
			{

				g.setColor(GetBlue(1 - (double)i / (double)h));
				g.drawLine(0, i, w, i);
			}

			g.setColor(new Color(0xffff00));
			if (!string.IsNullOrEmpty(Error))
				g.drawString(Error, 16, 64);

			if (this.Maze != null)
			{
				try
				{
					int x, y;
					int z = 12;

					for (x = 1; x < Maze.Width - 1; x++)
						for (y = 1; y < Maze.Height - 1; y++)
						{
							var v = Maze[x, y];

							if ((v & 1) != 0) /* This cell has a top wall */
								g.drawLine(x * z, y * z, x * z + z, y * z);

							if ((v & 4) != 0) /* This cell has a left wall */
								g.drawLine(x * z, y * z, x * z, y * z + z);


							if ((v & 8) != 0) /* This cell has a right wall */
								g.drawLine(x * z + z, y * z, x * z + z, y * z + z);

							if ((v & 2) != 0) /* This cell has a bottom wall */
								g.drawLine(x * z, y * z + z, x * z + z, y * z + z);
						}

					var offset = Maze.Width * z;

					g.setColor(new Color(0x00ff00));
					g.drawRect(offset + z, z, (Maze.Width - 2) * z, (Maze.Height - 2) * z);


					for (x = 1; x < Maze.Width - 1; x++)
						for (y = 1; y < Maze.Height - 1; y++)
						{
							var v = Maze[x, y];
							var IsTop = (v & 1) != 0;
							var IsLeft = (v & 4) != 0;

							if (IsTop) /* This cell has a top wall */
								g.fillRect(offset + x * z, y * z, z, z / 3);

							if (IsLeft) /* This cell has a left wall */
								g.fillRect(offset + x * z, y * z, z / 3, z);

							var IsBottom = (v & 2) != 0;
							var IsRight = (v & 8) != 0;

							if (IsRight)
							{
								g.fillRect(offset + x * z + z - z / 3, y * z, z / 3, z);
							}

							if (IsBottom)
							{
								g.fillRect(offset + x * z, y * z + z - z / 3, z, z / 3);
							}


						}
				}
				catch
				{

				}
			}
		}

		#region [this.Button1_Clicked]
		[Script]
		class Button1_Clicked_Handler : AnonymouseDelegate
		{
			public MazeApplet Target;

			public override void actionPerformed(ActionEvent e)
			{
				Target.Button1_Clicked();
			}
		}
		#endregion

		public void Button1_Clicked()
		{


			EvaluateJavaScript(this, "alert('script was evaluated!');");

		}

		#region IFeedback Members

		public void Invoke(string e)
		{
			//System.Console.WriteLine(e);

			this.showStatus(e);
		}

		#endregion
	}
}
