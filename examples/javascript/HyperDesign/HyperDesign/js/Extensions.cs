using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Runtime;

namespace HyperDesign.js
{
	[Script]
	static class Extensions
	{
		public static int Random(this int i)
		{
			return new Random().Next(i);
		}

		[Script]
		public class GoogleThreeDWarehouseImage
		{
			public IHTMLImage Image;

			public IHTMLImage[] Frames;

			public int Interval = 1000 / 15;

		
		
			public void Animate()
			{
				Image.InvokeOnComplete(
					delegate
					{
						
						var c = Frames.Length;
						foreach (var k_ in Frames)
						{
							var k = k_;

							k.InvokeOnComplete(
								delegate
								{
									k.width = Image.width;
									k.height = Image.height;

									c--;
									if (c == 0)
										InternalAnimate();
								}
							);
						}
					}
				);
			}


			private void InternalAnimate()
			{
				var c = Frames.AsCyclicEnumerator();
				var x = Image;

				var t = new Timer(
					delegate
					{
						c.MoveNext();
						x.parentNode.replaceChild(c.Current, x);
						//x.parentNode.insertBefore(c.Current, x);
						//x.parentNode.removeChild(x);
						x = c.Current;
					}, 1, Interval
				);
			}
		}

		public static GoogleThreeDWarehouseImage ToGoogleThreeDWarehouseImage(this IHTMLImage source)
		{
			var n = new GoogleThreeDWarehouseImage
			{
				Image = source
			};


			string mid = null;

			var u = new Uri(source.src);

			if (u.Host != "sketchup.google.com")
				return null;

			if (u.AbsolutePath != "/3dwarehouse/download")
				return null;

			foreach (var p in u.Query.Split('&'))
			{
				var kv = p.Split('=');
				if (kv.Length == 2)
				{
					if (kv[0] == "mid")
					{
						mid = kv[1];
					}
				}
			}

			if (mid == null)
				return null;

			n.Frames = Enumerable.ToArray(
				from imagenum in Enumerable.Range(0, 36)
				select new IHTMLImage("http://sketchup.google.com/3dwarehouse/download?mid=" + mid + "&rtyp=swivel&setnum=0&imagenum=" + imagenum)
			);

			//n.Image.style.border = "4px solid red";

			

			return n;
		}

		public static IEnumerable<GoogleThreeDWarehouseImage> ToGoogleThreeDWarehouseImages(this IEnumerable<IHTMLImage> source)
		{
			return from k in source
				   let n = ToGoogleThreeDWarehouseImage(k)
				   where n != null
				   select n;
		}

		public static IEnumerable<GoogleThreeDWarehouseImage> Animate(this IEnumerable<GoogleThreeDWarehouseImage> source)
		{
			foreach (var k in source)
			{
				k.Animate();
			}

			return source;
		}
	}
}
