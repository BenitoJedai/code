using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.RayCaster
{

	[Script]
	public class PortalInfo
	{
		public ViewEngineBase View;
		public SpriteInfo Sprite;
		public Texture64 Frame;

		public PortalInfo()
		{
			this.View = new ViewEngineBase(Texture64.SizeConstant, Texture64.SizeConstant);

			this.Frame = new Bitmap(new BitmapData(Texture64.SizeConstant, Texture64.SizeConstant, true, 0x0));

			this.Sprite = new SpriteInfo
			{
				Frames = new[] { this.Frame },
				Range = 0.3
			};

			Color = 0xff;
		}

		public IVector ViewVector
		{
			get
			{
				return View;
			}
			set
			{
				View.ViewPosition = value.Position;
				View.ViewDirection = value.Direction;
			}
		}

		public IVector SpriteVector
		{
			get
			{
				return Sprite;
			}
			set
			{
				Sprite.Position = value.Position;
				Sprite.Direction = value.Direction;
			}
		}
		uint _Color;
		public uint Color
		{
			get { return _Color; }
			set
			{
				_Color = value;

				this.Frame.Bitmap.filters = new[] { new GlowFilter(value) };
			}
		}

		public Bitmap AlphaMask;

		public void Update()
		{
			this.View.RenderScene();

			var m = default(BitmapData);

			if (AlphaMask != null)
				m = AlphaMask.bitmapData;

			this.Frame.Bitmap.bitmapData.copyPixels(this.View.Buffer, this.View.Buffer.rect, new Point(), m);
			this.Frame.Update();
		}
	}

}
