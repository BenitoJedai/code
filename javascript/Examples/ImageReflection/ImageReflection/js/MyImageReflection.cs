using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using System;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.ImageReflection;


namespace ImageReflection.js
{


    [Script, ScriptApplicationEntryPoint(ScriptedLoading = true)]
    public class MyImageReflection
    {
        
        public MyImageReflection()
        {
            Native.Document.body.style.background = "url(" + Assets.Path + "/bg.jpg)";
            Native.Document.body.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

            new IHTMLImage(Assets.Path + "/pt-tokyotaxiorange-01t.png").InvokeOnComplete(
                img =>
                {
                    var Size = new Point(img.width, img.height);
                    var Position = new Point(
                        32 + Size.X,
                        32 + Size.Y
                        );


                    var Control =
                        new ReflectionSetup
                        {
                            Image = img,
                            Right = 2,
                            Left = 2,
                            Top = 2,
                            Bottom = 2,
                            Position = Position,
                            Size = Size,
                            ReflectionZoom = 1.0,
                            
                        }.ConvertToImageReflection();

                    Control.AttachToDocument();
                }
            );

            new IHTMLImage(Assets.Path + "/old-Preview.png").InvokeOnComplete(
                img =>
                {
                    var Size = new Point(img.width, img.height);
                    var ReflectionZoom = 1.0;
                    var Position = new Point(
                        Native.Window.Width - (Size.X + Size.X * ReflectionZoom * 2).ToInt32(),
                        Native.Window.Height - (Size.X + Size.Y * ReflectionZoom * 2).ToInt32()
                        );


                    var Control =
                         new ReflectionSetup
                         {
                             Image = img,
                             Position = Position,
                             Size = Size,
                             ReflectionZoom = 0.5,

                             Bottom = 2
                         }.ConvertToImageReflection();
                    Control.AttachToDocument();
                }
            );

            new IHTMLImage(Assets.Path + "/Preview.png").InvokeOnComplete(
                img =>
                {
                    var Size = new Point(img.width, img.height);
                    var ReflectionZoom = 1.0;
                    var Position = new Point(
                        (Size.X * ReflectionZoom * 2).ToInt32(),
                        Native.Window.Height - (Size.X + Size.Y * ReflectionZoom * 2).ToInt32()
                        );

                    
                    var Control = 
                         new ReflectionSetup
                         {
                             Image = img,
                             Position = Position,
                             Size = Size,
                             ReflectionZoom = 0.5,

                             Bottom = 2
                         }.ConvertToImageReflection();

                    Control.AttachToDocument();
                }
            );
        }


        static MyImageReflection()
        {
            typeof(MyImageReflection).SpawnTo(i => new MyImageReflection());
        }

    }

}
