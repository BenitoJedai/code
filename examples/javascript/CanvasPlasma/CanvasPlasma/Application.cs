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
using CanvasPlasma.HTML.Pages;
using CanvasPlasma.Styles;
using CanvasPlasma.Library;

namespace CanvasPlasma
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        // port from Z:\jsc.svn\examples\actionscript\FlashPlasma\FlashPlasmaDocument\js\OrcasScriptApplication.cs
        // code could also be shared via windows forms and java applet

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            InitializeContent();

            style.Content.AttachToHead();
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }
        
     

        #region ScriptCoreLib needs an updated
        [Script(HasNoPrototype = true)]
        public class ImageData
        {
            public readonly int width;
            public readonly int height;
            public readonly byte[] data;
        }
        #endregion


        public void InitializeContent()
		{
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            var DefaultWidth = Native.Window.Width ;
            var DefaultHeight =Native.Window.Height;


            Plasma.generatePlasma(DefaultWidth, DefaultHeight);

			var shift = 0;

			var canvas = new IHTMLCanvas();

            canvas.width = DefaultWidth;
            canvas.height = DefaultHeight;

            canvas.style.position = IStyle.PositionEnum.absolute;
            canvas.style.SetLocation(0, 0, DefaultWidth, DefaultHeight);

			var context = (CanvasRenderingContext2D)canvas.getContext("2d");

            var xx = context.getImageData(0, 0, DefaultWidth, DefaultHeight);
            var x = (ImageData)(object)xx;

            Action AtTick = null;

            AtTick = delegate
			{
                if (DefaultWidth != Native.Window.Width)
                    if (DefaultHeight !=Native.Window.Height)
                    {
                        canvas.Orphanize();
                        InitializeContent();
                        return;
                    }

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

            #region requestFullscreen
            Native.Document.body.ondblclick +=
                delegate
                {
                    //if (IsDisposed)
                    //    return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    if (canvas.parentNode == null)
                        return;

                    Native.Document.body.requestFullscreen();


                };
            #endregion



			canvas.AttachToDocument();
		}


    }
}
