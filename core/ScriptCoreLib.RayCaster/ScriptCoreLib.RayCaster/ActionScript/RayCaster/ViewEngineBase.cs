using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.RayCaster.Extensions;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.RayCaster
{
	[Script]
	public partial class ViewEngineBase
	{
		protected readonly int _ViewWidth;
		protected readonly int _ViewHeight;

		public int ViewWidth { get { return _ViewWidth; } }
		public int ViewHeight { get { return _ViewHeight; } }

		public ViewEngineBase(int w, int h)
		{
			this._ViewWidth = w;
			this._ViewHeight = h;

			this.buffer = new BitmapData(w, h, false, 0x0);
			this._Image = new Bitmap(this.buffer);

			this.posX = 1.5;
			this.posY = 1.5;

			this.dirX = -1;
			this.dirY = 0;

			this.planeX = 0;
			this.planeY = 0.66;

			this._WallMap = new Texture32();

			this.Map = new MapInfo();
			this.Map.Changed += () =>
					{
						this._textures = this.Map.Textures.Values.ToArray();
						this._WallMap = this.Map.WallMap;

					};



		}

		public BitmapData Buffer
		{
			get
			{
				return buffer;
			}
		}

		protected BitmapData buffer;

		Bitmap _Image;

		public Bitmap Image
		{
			get
			{
				return _Image;
			}
		}

		#region ViewPosition
		protected double posX;
		protected double posY;  //x and y start position

		public Point ViewPosition
		{
			get
			{
				return new Point { x = posX, y = posY };
			}
			set
			{
				MoveTo(value.x, value.y);
			}
		}

		public double ViewPositionX
		{
			get
			{
				return posX;
			}


		}

		public double ViewPositionY
		{

			get
			{
				return posY;
			}
		}


		public Action ViewPositionChanged;

		public void MoveTo(double x, double y)
		{
			if (_WallMap[x.Floor(), y.Floor()] == 0)
			{
				posX = x;
				posY = y;

				if (ViewPositionChanged != null)
					ViewPositionChanged();
			}
		}
		#endregion



		protected Texture32 _WallMap;



		public readonly MapInfo Map;

		public uint CurrentTile
		{
			get
			{
				return _WallMap[posX.Floor(), posY.Floor()];
			}
		}


		#region ViewDirection
		protected double planeX;
		protected double planeY; //the 2d raycaster version of camera plane


		protected double dirX;
		protected double dirY; //initial direction vector

		protected double dir = 0;

		public event Action ViewDirectionChanged;

		public double ViewDirection
		{
			get { return dir; }
			set
			{
				DoRotateView(value - dir);

				if (ViewDirectionChanged != null)
					ViewDirectionChanged();
			}
		}

		public void DoRotateView(double rotSpeed)
		{
			var oldDirX = dirX;
			dirX = dirX * Math.Cos(rotSpeed) - dirY * Math.Sin(rotSpeed);
			dirY = oldDirX * Math.Sin(rotSpeed) + dirY * Math.Cos(rotSpeed);
			dir = new Point { x = dirX, y = dirY }.GetRotation();

			var oldPlaneX = planeX;
			planeX = planeX * Math.Cos(rotSpeed) - planeY * Math.Sin(rotSpeed);
			planeY = oldPlaneX * Math.Sin(rotSpeed) + planeY * Math.Cos(rotSpeed);
		}
		#endregion

		public List<SpriteInfo> Sprites = new List<SpriteInfo>();

		public Texture64 FloorTexture;
		public Texture64 CeilingTexture;
	}
}
