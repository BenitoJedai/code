using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Diagnostics;


namespace Mandelbrot.Document.js
{
	[Script(HasNoPrototype = true)]
	public class IHTMLCanvas : IHTMLElement
	{
		public object getContext(string contextId)
		{
			return default(object);
		}


	}


	[Script(HasNoPrototype = true)]
	public class CanvasRenderingContext2D
	{
		public readonly IHTMLCanvas canvas;

		public ImageData createImageData(int w, int h)
		{
			return default(ImageData);
		}

		public ImageData getImageData(double dx, double dy, double sw, double sh)
		{
			return default(ImageData);
		}

		public void putImageData(object imagedata, double dx, double dy)
		{
		}

		public void putImageData(object imagedata, double dx, double dy,
		double dirtyX, double dirtyY, double dirtyWidth, double dirtyHeight)
		{
		}
	}

	[Script(HasNoPrototype = true)]
	public class ImageData
	{
		public readonly int width;
		public readonly int height;
		public readonly byte[] data;
	}

	[Script, ScriptApplicationEntryPoint]
	public class MandelbrotDocument
	{

		public MandelbrotDocument()
		{

			var shift = 0;
			Debugger.Break();
			var buffer = MandelbrotProvider.DrawMandelbrotSet(shift);
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = 0;
			}

			var canvas = (IHTMLCanvas)Native.Document.createElement("canvas");

			canvas.width = MandelbrotProvider.DefaultWidth;
			canvas.height = MandelbrotProvider.DefaultHeight;

			var context = (CanvasRenderingContext2D)canvas.getContext("2d");

			var t = new Timer();

			//var x = new MyImageData(DefaultWidth, DefaultHeight);
			var x = context.getImageData(0, 0, MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight);

			Action Refresh =
				delegate
				{
					buffer = MandelbrotProvider.DrawMandelbrotSet(shift);

					//var x = context.createImageData(DefaultWidth, DefaultHeight);


					var k = 0;
					for (int i = 0; i < MandelbrotProvider.DefaultWidth; i++)
						for (int j = 0; j < MandelbrotProvider.DefaultHeight; j++)
						{
							var i4 = i * 4;
							var j4 = j * 4;

							var offset = i4 + j4 * MandelbrotProvider.DefaultWidth;

							x.data[offset + 2] = (byte)((buffer[k] >> (0 * 8)) & 0xff);
							x.data[offset + 1] = (byte)((buffer[k] >> (1 * 8)) & 0xff);
							x.data[offset + 0] = (byte)((buffer[k] >> (2 * 8)) & 0xff);
							x.data[offset + 3] = 0xff;

							//x.data[offset + 2] = 0x0f;
							//x.data[offset + 1] = 0xff;
							//x.data[offset + 0] = 0x0f;
							//x.data[offset + 3] = 0xff;

							k++;
						}

					context.putImageData(x, 0, 0, 0, 0, MandelbrotProvider.DefaultWidth, MandelbrotProvider.DefaultHeight);

				};

			t.Tick +=
				delegate
				{

					Refresh();

					shift++;
				};

			t.StartInterval(50);
			//Refresh();

			canvas.AttachToDocument();

		}


		static MandelbrotDocument()
		{
			typeof(MandelbrotDocument).SpawnTo(i => new MandelbrotDocument());
		}

	}

}
