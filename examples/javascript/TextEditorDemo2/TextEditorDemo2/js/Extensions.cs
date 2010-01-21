using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;

namespace TextEditorDemo2.js
{
	[Script]
	static class Extensions
	{
		public static int Random(this int i)
		{
			return new Random().Next(i);
		}

		public static IEnumerable<GoogleThreeDWarehouseImage> AttachTo(this IEnumerable<GoogleThreeDWarehouseImage> source, INode n)
		{
			foreach (var item in source)
			{
				n.appendChild(item.Image);
			}
			return source;
		}

		public static IEnumerable<T> AttachTo<T>(this IEnumerable<T> source, INode n) where T : INode
		{
			foreach (var item in source.AsEnumerable())
			{
				n.appendChild(item);
			}
			return source;
		}
		public static IEnumerable<IHTMLImage> GetClonedImages(this IHTMLDocument e)
		{
			return
				from k in e.getElementsByTagName("img")
				select CloneImage((IHTMLImage)k);

		}

		static IHTMLImage CloneImage(IHTMLImage xi)
		{
			var iii = new IHTMLImage(xi.src)
				{
					title = xi.title,
					alt = xi.alt
				};


			iii.style.SetSize(xi);

			return iii;
		}

		[Script]
		public class GoogleThreeDWarehouseImage
		{
			public string Token;

			public IHTMLImage Image;

			public IHTMLImage[] Frames;

			public int Interval = 1000 / 15;

			int _Width;
			int _Height;

			public void Animate()
			{
				//Image.title = "Loading ...";

				Image.InvokeOnComplete(
					delegate
					{
						_Width = Image.width;
						_Height = Image.height;

						var details = new IHTMLAnchor("http://sketchup.google.com/3dwarehouse/details?mid=" + Token, "")
						{
							target = "_blank",
							title = Image.title
						};

						Image.border = 0;
						Image.parentNode.replaceChild(details, Image);
						details.appendChild(Image);
						//details.style.display = IStyle.DisplayEnum.block;
						//ApplyZoom(details);

						details.onmousewheel +=
							e =>
							{
								this.AnimationZoom += 0.1 * e.WheelDirection;
							};
						details.onmouseover +=
							delegate
							{
								__Skip = true;
							};

						details.onmouseout +=
							delegate
							{
								__Skip = false;
							};

						var c = Frames.Length;
						foreach (var k_ in Frames)
						{
							var k = k_;
							k.style.verticalAlign = Image.style.verticalAlign;
							k.InvokeOnComplete(
								delegate
								{
									
									ApplyZoom(k);

									c--;
									//Image.title = "Loading #" + c;

									if (c == 0)
										InternalAnimate();
								}
							);
						}
					}
				);
			}

			double _Zoom = 1.0;

			public double AnimationZoom
			{
				get
				{
					return _Zoom;
				}
				set
				{
					_Zoom = value;
					ApplyZoom(Image);

					foreach (var item in Frames)
					{
						ApplyZoom(item);
					}
				}
			}
			private void ApplyZoom(IHTMLElement k)
			{
				k.style.SetSize(
				System.Convert.ToInt32(_Width * _Zoom),
				System.Convert.ToInt32(_Height * _Zoom)
				);
			}

			bool __Skip;

			private void InternalAnimate()
			{
				var c = Frames.AsCyclicEnumerator();
				var x = Image;

				new Timer(
					t =>
					{
						if (x.parentNode == null)
							return;

						if (__Skip)
							return;

						c.MoveNext();
						//1.
						x.parentNode.replaceChild(c.Current, x);

						//2.
						//x.parentNode.insertBefore(c.Current, x);
						//x.parentNode.removeChild(x);

						//3.
						//Image.src = c.Current.src;
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

			n.Token = mid;

			n.Frames = Enumerable.ToArray(
				from imagenum in Enumerable.Range(0, 36)
				select new IHTMLImage("http://sketchup.google.com/3dwarehouse/download?mid=" + mid + "&rtyp=swivel&setnum=0&imagenum=" + imagenum)
				{
					border = 0
				}
			);

			//n.Image.style.border = "4px solid red";



			return n;
		}

		public static IEnumerable<GoogleThreeDWarehouseImage> ToGoogleThreeDWarehouseImages(this IEnumerable<IHTMLElement> source)
		{
			return from k in source
				   where k.nodeName == "img"
				   select ((IHTMLImage)k).ToGoogleThreeDWarehouseImage();
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
