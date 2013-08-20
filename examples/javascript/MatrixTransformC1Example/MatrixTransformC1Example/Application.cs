using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using MatrixTransformC1Example.Design;
using MatrixTransformC1Example.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.Runtime;
using MatrixTransformC1Example.HTML.Images.FromAssets;

namespace MatrixTransformC1Example
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);


            AddAnimation(100, 200);
            AddAnimation(300, 300);
            AddAnimation(600, 250);
            AddAnimation(900, 350);

        }

        private static void AddAnimation(int X, int Y)
        {
            XRectangle Image1;
            XRectangle Image2;
            XRectangle Image3;
            XRectangle Image4;


            Func<IHTMLImage, XRectangle> f =
                ContentImage =>
                {
                    var r = new XRectangle();

                    r.ContentImage = ContentImage;
                    r.ContentImage.style.SetSize(185, 100);
                    r.Content.appendChild(r.ContentImage);
                    r.Element.AttachToDocument().MoveTo(X, Y);


                    return r;
                };

            Image1 = f(new wood_green());
            Image2 = f(new wood_green());
            Image3 = f(new wood_green());
            Image4 = f(new wood_green());


            var t = new Timer();
            var sw = new Stopwatch();
            var x = 0.0;

            t.Tick +=
                delegate
                {
                    sw.Stop();

                    var elapsed = sw.ElapsedMilliseconds;

                    x += 0.0015 * elapsed;
                    //sw.Reset();

                    sw = new Stopwatch();
                    sw.Start();



                    var DefaultRotation = 0;


                    var z = Enumerable.ToArray(
                            from k in new[] { 
								new {Image = Image1, r = (double)0.0 + DefaultRotation }, 
								new {Image =Image2, r = (double)0.5 + DefaultRotation }, 
								new {Image =Image3, r = (double)1.0 + DefaultRotation }, 
								new {Image =Image4, r = (double)1.5 + DefaultRotation} 
							}
                            let M11 = Math.Cos(x + Math.PI * k.r)
                            let M12 = Math.Sin(x + Math.PI * k.r) * 0.5
                            orderby M12
                            select new { k.Image, M11, M12 }
                    );


                    foreach (var k in z)
                    {
                        k.Image.Element.Orphanize();


                        //k.Image.style.SetSize(System.Convert.ToInt32(100 * k.M11), 100);

                        k.Image.ApplyMatrix(0, 0,
                            k.M11, k.M12, 0, 1, 0, 0);

                        //k.Image.RenderTransform =
                        //    new MatrixTransform
                        //    {
                        //        Matrix = new Matrix
                        //        {
                        //            M11 = k.M11,
                        //            M12 = k.M12,
                        //            M21 = 0,
                        //            M22 = 1,
                        //            //M22 = -1,
                        //            //OffsetY = -75
                        //            OffsetY = 0
                        //        }
                        //    };
                    }

                    foreach (var k in z)
                    {
                        k.Image.Element.AttachToDocument();
                    }


                };

            t.StartInterval(1000 / 30);
        }

    }

    public class XRectangle
    {
        public IHTMLElement Element;
        public IHTMLElement Rotor;
        public IHTMLElement Content;
        public IHTMLImage ContentImage;

        public string Text;

        public XRectangle()
        {
            var aelement = new IHTMLDiv();



            // WPF: Rectangle, Image or Label
            var acontent = new IHTMLDiv();

            //acontent.style.backgroundColor = "#ff0000";

            this.Rotor = new IHTMLDiv();

            //this.Rotor.style.backgroundColor = "#ffd0d0";

            //this.Rotor.style.border = "1px solid black";

            //this.Rotor.style.paddingRight = "10px";

            this.Rotor.AttachTo(aelement);

            acontent.AttachTo(this.Rotor);

            Element = aelement;
            Content = acontent;
        }
    }

    static class Extensions
    {
        public static void ApplyMatrix(this XRectangle k, double OriginX, double OriginY, double M11, double M12, double M21, double M22, double Dx, double Dy)
        {
            // if this information came via WPF we should do Dx -= OriginX
            var mm = new[]
					{
						M11, M12,
						M21, M22,
						
						0, 0
						//0.838670551776886,0.5446390509605408,-0.5446390509605408,0.838670551776886,0,0
					};

            //this.Content.innerText = new
            //{
            //    OriginX,
            //    OriginY,
            //    M11,
            //    M12,
            //    M21,
            //    M22,
            //    Dx,
            //    Dy,

            //    Text
            //}.ToString();

            var code = @"
			q.style.filter = ""progid:DXImageTransform.Microsoft.Matrix(M11='"" + m[0] + ""',M12='"" + m[2] + ""',M21='"" + m[1] + ""', M22='"" + m[3] + ""', sizingmethod='auto expand');"";
	

			q.style.MozTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
			
			q.style.WebkitTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
				";

            var zx = 0;
            var zy = 0;


            //if (OriginX == -k.Content.clientWidth / 2)
            //    if (OriginY == -k.Content.clientHeight / 2)
            {

                k.Rotor.style.paddingLeft = 185 + "px";
                k.Rotor.style.paddingTop = 100 + "px";

                zy -= 185;
                zx -= 100;
            }

            //if (OriginX == k.Content.clientWidth / 2)
            //    if (OriginY == k.Content.clientHeight / 2)
            //    {
            //k.Rotor.style.paddingRight = 369 + "px";
            //k.Rotor.style.paddingBottom = 200 + "px";

            ////zx -= this.Rotor.clientWidth;
            //zy += this.Rotor.clientHeight / 2;
            //zx += this.Rotor.clientWidth / 2;
            //    }

            var Rotor = k.Rotor;

            new IFunction("q", "m", code).apply(null, Rotor, mm);

            zx += (Rotor.clientWidth - Rotor.offsetWidth) / 2;
            zy += (Rotor.clientHeight - Rotor.offsetHeight) / 2;


            Rotor.style.SetLocation(System.Convert.ToInt32(Dx) + zx, System.Convert.ToInt32(Dy) + zy);

        }


        public static T MoveTo<T>(this T e, int x, int y) where T : IHTMLElement
        {
            e.style.SetLocation(x, y);

            return e;
        }
        public static int Random(this int i)
        {
            return new Random().Next(i);
        }
    }

}
