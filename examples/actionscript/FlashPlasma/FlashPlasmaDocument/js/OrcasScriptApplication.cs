﻿using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using FlashPlasmaEngine;
using System;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.Runtime;


namespace FlashPlasmaDocument.js
{
	[Script(HasNoPrototype = true)]
	public class ImageData
	{
		public readonly int width;
		public readonly int height;
		public readonly byte[] data;
	}

	[Script, ScriptApplicationEntryPoint]
	public class FlashPlasmaDocument
	{
		const int DefaultWidth = 300;
		const int DefaultHeight = 300;

		public FlashPlasmaDocument()
		{
			Plasma.generatePlasma(DefaultWidth, DefaultHeight);

			var shift = 0;

			var canvas = new IHTMLCanvas();

			canvas.width = DefaultWidth;
			canvas.height = DefaultHeight;

			var context = (CanvasRenderingContext2D)canvas.getContext("2d");

            var xx = context.getImageData(0, 0, DefaultWidth, DefaultHeight);
            var x = (ImageData)(object)xx;

            Action AtTick = null;

            AtTick = delegate
			{
				var buffer = Plasma.shiftPlasma(shift);
					
				//var x = context.createImageData(DefaultWidth, DefaultHeight);


				var k = 0;
				for (int i = 0; i < DefaultWidth; i++)
					for (int j = 0; j < DefaultHeight; j++)
					{
						var i4 = i * 4;
						var j4 = j * 4;


						x.data[i4 + j4 * DefaultWidth + 2] = (byte)((buffer[k] >> (0 * 8)) & 0xff);
						x.data[i4 + j4 * DefaultWidth + 1] = (byte)((buffer[k] >> (1 * 8)) & 0xff);
						x.data[i4 + j4 * DefaultWidth + 0] = (byte)((buffer[k] >> (2 * 8)) & 0xff);
						x.data[i4 + j4 * DefaultWidth + 3] = 0xff;

						k++;
					}

				context.putImageData(xx, 0, 0, 0, 0, DefaultWidth, DefaultHeight);
				shift++;
                Native.Window.requestAnimationFrame += AtTick;
            };

            Native.Window.requestAnimationFrame += AtTick;


			canvas.AttachToDocument();
		}

		static FlashPlasmaDocument()
		{
			typeof(FlashPlasmaDocument).SpawnTo(i => new FlashPlasmaDocument());
		}

	}

}
