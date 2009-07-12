using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Maze
{
	public class BlockMaze
	{
		public bool[][] Walls;

		public int Width;
		public int Height;

		public BlockMaze(MazeGenerator maze)
		{
			var sx = maze.Width * 2 - 3;
			var sy = maze.Height * 2 - 3;

			this.Width = sx;
			this.Height = sy;

			var w = new bool[sx][];

			this.Walls = w;

			for (var x = 0; x < w.Length; x++)
			{
				w[x] = new bool[sy];

				for (var y = 0; y < sy; y++)
					w[x][y] = true;

			}

			for (var x = 1; x < maze.Width - 1; x++)
			{
				var dx = x * 2 - 1;

				for (var y = 1; y < maze.Height - 1; y++)
				{
					var dy = y * 2 - 1;

					w[dx][dy] = false;

					var v = maze[x, y];

					var IsTop = (v & 1) != 0;
					var IsLeft = (v & 4) != 0;
					var IsBottom = (v & 2) != 0;
					var IsRight = (v & 8) != 0;

					var NotTop = (v & 1) == 0;
					var NotLeft = (v & 4) == 0;
					var NotBottom = (v & 2) == 0;
					var NotRight = (v & 8) == 0;

					if (NotTop)
						w[dx][dy - 1] = false;

					if (NotBottom)
						w[dx][dy + 1] = false;

					if (NotLeft)
						w[dx - 1][dy] = false;

					if (NotRight)
						w[dx + 1][dy] = false;
				}
			}
		}	
	}

	public class MazeGenerator
	{
		public class PointInt32
		{
			public int X;
			public int Y;
		}

		public class RectInt32
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

		static readonly Random random_cache = new Random();

		internal static double random()
		{
			return random_cache.NextDouble();
		}

		RectInt32 clip;

		int[][] maze;

		public int this[int x, int y]
		{
			get
			{
				return maze[x][y];
			}
		}

		public readonly int Width;
		public readonly int Height;

		static int ToInt32(double e)
		{
			return (int)Math.Floor(e);
			//return (int)Math.Ceiling(e);
		}

		public interface IFeedback
		{
			void Invoke(string e);
		}

		public MazeGenerator(int Width, int Height, IFeedback Handler)
		{
			// port from http://en.wikipedia.org/wiki/Image:Maze.png

			this.Width = Width;
			this.Height = Height;

			clip = new RectInt32 { Width = Width, Height = Height };

			int x, y, n, d;
			int[] dx = { 0, 0, -1, 1 };
			int[] dy = { -1, 1, Convert.ToInt32(0), Convert.ToInt32(0) };
			int[] todo = new int[Width * Height];
			int todonum = 0;

			/* We want to create a maze on a grid. */
			maze = new int[Width][];

			/* We start with a grid full of walls. */
			for (x = 0; x < Width; x++)
			{
				maze[x] = new int[Height];
				for (y = 0; y < Height; y++)
				{
					var p = new PointInt32 { X = x, Y = y };

					if (clip.IsBorder(p))
					{
						maze[x][y] = 32;
					}
					else
					{
						maze[x][y] = 63;
					}
				}
			}

			/* Select any square of the grid, to start with. */
			x = ToInt32(1 + random() * (Width - 2));
			y = ToInt32(1 + random() * (Height - 2));

			/* Mark this square as connected to the maze. */
			maze[x][y] &= ~48;

			/* Remember the surrounding squares, as we will */
			for (d = 0; d < 4; d++)
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
				if (Handler != null)
					Handler.Invoke("todonum = " + todonum);

				/* We select one of the squares next to the maze. */
				n = ToInt32(random() * todonum);
				x = todo[n] >> 16; /* the top 2 bytes of the data */
				y = todo[n] & 65535; /* the bottom 2 bytes of the data */

				/* We will connect it, so remove it from the queue. */
				todonum--;

				todo[n] = todo[todonum];

				/* Select a direction, which leads to the maze. */
				d = ToInt32(random() * 4);

				if ((maze[x + dx[d]][y + dy[d]] & 32) != 0)
					d = (d + 1) % dx.Length;

				if ((maze[x + dx[d]][y + dy[d]] & 32) != 0)
					d = (d + 1) % dx.Length;

				if ((maze[x + dx[d]][y + dy[d]] & 32) != 0)
					d = (d + 1) % dx.Length;

			
				/* Connect this square to the maze. */
				maze[x][y] &= ~((1 << d) | 32);
				maze[x + dx[d]][y + dy[d]] &= ~(1 << (d ^ 1));

				/* Remember the surrounding squares, which aren't */
				for (d = 0; d < 4; d++)
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
			maze[Width - 2][Height - 2] &= ~2;

		}
	}
}
