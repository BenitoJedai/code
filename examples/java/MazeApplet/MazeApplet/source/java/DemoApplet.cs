using ScriptCoreLib;

using java.lang;
using java.applet;
using java.awt;
using java.awt.@event;
using javax.common.runtime;
using System;

namespace MazeApplet.source.java
{
	[Script]
	class PointInt32
	{
		public int X;
		public int Y;
	}

	[Script]
	class RectInt32
	{
		public int X;
		public int Y;

		public int Width;
		public int Height;

		public bool IsBorder(PointInt32 e)
		{
			if (e.X == X)
				return true;

			if (e.Y == Y)
				return true;

			if (e.X == X + Width - 1)
				return true;

			if (e.Y == Y + Height - 1)
				return true;

			return false;
		}
		public bool Contains(PointInt32 e)
		{
			if (e.X < X)
				return false;

			if (e.X >= Width)
				return false;

			if (e.Y < Y)
				return false;

			if (e.Y >= Height)
				return false;

			return true;
		}
	}


	[Script]
	public partial class MazeApplet : Applet
	{
		// http://www.astrolog.org/labyrnth/algrithm.htm
		// http://en.wikipedia.org/wiki/Maze_generation_algorithm
		// http://en.wikipedia.org/wiki/Image:Maze.png
		// http://www.siteexperts.com/tips/functions/ts20/page3.asp

		int[][] maze;

		public static double random()
		{
			return new Random().NextDouble();
		}

		public override void init()
		{
			//this.InitializeComponents();

			base.resize(Settings.DefaultWidth, Settings.DefaultHeight);

			var clip = new RectInt32 { Width = 20, Height = 20 };


			int x, y, n, d;
			int[] dx = { 0, 0, -1, 1 };
			int[] dy = { -1, 1, 0, 0 };
			int[] todo = new int[clip.Width * clip.Height];
			int todonum = 0;

			/* We want to create a maze on a grid. */
			maze = new int[clip.Width][];

			for (int maze_x = 0; maze_x < clip.Width; maze_x++)
			{
				maze[maze_x] = new int[clip.Height];
			}

		

			/* We start with a grid full of walls. */
			for (x = 0; x < clip.Width; ++x)
				for (y = 0; y < clip.Height; ++y)
					if (clip.IsBorder(new PointInt32 { X = x, Y = y }))
					{
						maze[x][y] = 32;
					}
					else
					{
						maze[x][y] = 63;
					}

			/* Select any square of the grid, to start with. */
			x = (int)(1 + random() * 18);
			y = (int)(1 + random() * 18);

			/* Mark this square as connected to the maze. */
			maze[x][y] &= ~48;

			/* Remember the surrounding squares, as we will */
			for (d = 0; d < 4; ++d)
				if ((maze[x + dx[d]][y + dy[d]] & 16) != 0)
				{
					/* want to connect them to the maze. */

					/* alternately, you could use a struct to store the two integers
					 * this would result in easier to read code, though not as speedy
					 * of course, if you were worried about speed, you wouldn't be using Java
					 * you could also use a single integer which represents (x + y * width)
					 * this would actually be faster than the current approach
					 * - quin/10-24-06
					 *    Actually, the former wouldn't work in Java- there's no such thing as a
					 *    struct. It's a class or nothing, I'm afraid.
					 *    - Jae Armstrong/23-03-07
					 */

					todo[todonum++] = ((x + dx[d]) << 16) | (y + dy[d]);
					maze[x + dx[d]][y + dy[d]] &= ~16;
				}

			/* We won't be finished until all is connected. */
			while (todonum > 0)
			{
				/* We select one of the squares next to the maze. */
				n = (int)(random() * todonum);

				if (n < 0)
					throw new csharp.RuntimeException("n = " + n);

				if (n >= todo.Length)
					throw new csharp.RuntimeException("n = " + n);


				x = todo[n] >> 16; /* the top 2 bytes of the data */
				y = todo[n] & 65535; /* the bottom 2 bytes of the data */

				/* We will connect it, so remove it from the queue. */
				todonum--;

				todo[n] = todo[todonum];

				/* Select a direction, which leads to the maze. */

				var do_enabled = true;

				while (do_enabled)
				{
					d = (int)(random() * 4);
					do_enabled = ((maze[x + dx[d]][y + dy[d]] & 32) != 0);
				}

				/* Connect this square to the maze. */
				maze[x][y] &= ~((1 << d) | 32);
				maze[x + dx[d]][y + dy[d]] &= ~(1 << (d ^ 1));

				/* Remember the surrounding squares, which aren't */
				for (d = 0; d < 4; ++d)
					if ((maze[x + dx[d]][y + dy[d]] & 16) != 0)
					{

						/* connected to the maze, and aren't yet queued to be. */
						todo[todonum++] = ((x + dx[d]) << 16) | (y + dy[d]);
						maze[x + dx[d]][y + dy[d]] &= ~16;
					}
				/* Repeat until finished. */
			}

			/* One may want to add an entrance and exit. */
			maze[1][1] &= ~1;
			maze[18][18] &= ~2;

		}

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
			//g.drawString("hello world, this is the sample applet", 16, 64);

			int x, y;

			for (x = 1; x < 19; ++x)
				for (y = 1; y < 19; ++y)
				{
					if ((maze[x][y] & 1) != 0) /* This cell has a top wall */
						g.drawLine(x * 10, y * 10, x * 10 + 10, y * 10);
					if ((maze[x][y] & 2) != 0) /* This cell has a bottom wall */
						g.drawLine(x * 10, y * 10 + 10, x * 10 + 10, y * 10 + 10);
					if ((maze[x][y] & 4) != 0) /* This cell has a left wall */
						g.drawLine(x * 10, y * 10, x * 10, y * 10 + 10);
					if ((maze[x][y] & 8) != 0) /* This cell has a right wall */
						g.drawLine(x * 10 + 10, y * 10, x * 10 + 10, y * 10 + 10);
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
	}
}
