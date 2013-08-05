using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.Languages;
using ScriptCoreLib.Ultra.Studio.InteractiveExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.Studio.StockMethods;

namespace ScriptCoreLib.Ultra.Studio
{
    public static class SolutionBuilderWithAdobeFlash
    {
        class CreateMySprite : PseudoCallExpression
        {
            public CreateMySprite(StockSpriteType Type, SolutionProjectLanguageField sprite)
            {
                var page_Page1 =
                    new PseudoCallExpression
                    {
                        // Application(page)
                        Object = "page",

                        Method =
                            new SolutionProjectLanguageMethod
                            {
                                IsProperty = true,
                                Name = "get_Content",
                                ReturnType = new SolutionProjectLanguageType
                                {
                                    Name = "IHTMLElement"
                                }
                            }
                    };

                //var new_Page1 =
                //    new PseudoCallExpression
                //    {
                //        // Application(page)

                //        Method =
                //            new SolutionProjectLanguageMethod
                //            {
                //                Name = SolutionProjectLanguageMethod.ConstructorName,
                //                DeclaringType = new SolutionProjectLanguageType
                //                {
                //                    Namespace = Type.Namespace,
                //                    Name = Type.Name
                //                }
                //            }
                //    };

                this.Comment = "Initialize " + Type.Name;

                this.Method = new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.SpriteExtensions.AttachSpriteTo();

                this.ParameterExpressions = new object[]
			    {
				    sprite,
				    page_Page1
			    };
            }
        }

        public static SolutionBuilder WithAdobeFlash(this SolutionBuilder sln)
        {
            Func<StockSpriteType> GetType = () => new StockSpriteType(sln.Name, "ApplicationSprite");

            var sprite = default(SolutionProjectLanguageField);

            sln.Interactive.GenerateTypes +=
                AddType =>
                {
                    var ApplicationSprite = GetType();

                    sprite = ApplicationSprite.ToInitializedField("sprite");

                    sprite.DeclaringType = sln.Interactive.ApplicationType;

                    AddType(ApplicationSprite);
                };




            sln.Interactive.GenerateApplicationExpressions +=
                AddCode =>
                {
                    AddCode(new CreateMySprite(GetType(), sprite));
                };

            return sln;
        }

        public static SolutionBuilder WithAdobeFlashWithFlare3D(this SolutionBuilder sln)
        {
            Func<StockSpriteType> GetType = () => new StockSpriteType(sln.Name, "ApplicationSprite");

            var sprite = default(SolutionProjectLanguageField);

            sln.Interactive.GenerateTypes +=
                AddType =>
                {
                    var ApplicationSprite = GetType();

                    sprite = ApplicationSprite.ToInitializedField("sprite");

                    sprite.DeclaringType = sln.Interactive.ApplicationType;

                    AddType(ApplicationSprite);
                };




            sln.Interactive.GenerateApplicationExpressions +=
                AddCode =>
                {
                    AddCode(new CreateMySprite(GetType(), sprite));
                };

            // ..\packages\Flare3D.1.0.0.0\lib\Flare3D.dll
            sln.NuGetReferences.Add(
                new ScriptCoreLib.Ultra.Studio.SolutionBuilder.package
                {
                    id = "Flare3D"
                }
            );



            return sln;
        }

        public static SolutionBuilder WithAdobeFlashCamera(this SolutionBuilder sln)
        {
            Func<StockSpriteType> GetType = () => new StockSpriteType(sln.Name, "ApplicationSprite");

            var sprite = default(SolutionProjectLanguageField);

            sln.Interactive.GenerateTypes +=
                AddType =>
                {
                    var ApplicationSprite = GetType();

                    sprite = ApplicationSprite.ToInitializedField("sprite");

                    sprite.DeclaringType = sln.Interactive.ApplicationType;

                    #region video <- new Video(500, 400)
                    var VideoType = new KnownStockTypes.ScriptCoreLib.ActionScript.flash.media.Video();
                    var video = VideoType.ToInitializedField("video");

                    video.FieldConstructor.ParameterExpressions = new[]
                    {
                        (PseudoInt32ConstantExpression)ScriptApplicationEntryPointAttribute.DefaultWidth,
                        (PseudoInt32ConstantExpression)ScriptApplicationEntryPointAttribute.DefaultHeight
                    };

                    ApplicationSprite.Fields.Add(video);
                    #endregion


                    var Camera_getCamera = new KnownStockTypes.ScriptCoreLib.ActionScript.flash.media.Camera.getCamera().ToCallExpression();

                    var LinqExtensions_With = new KnownStockTypes.ScriptCoreLib.Extensions.LinqExtensions.With().ToCallExpression(
                        Camera_getCamera,
                        new StockMethodInitializeCamera(video)
                    );

                    ApplicationSprite.Constructor.Code.Add(LinqExtensions_With);


                    AddType(ApplicationSprite);
                };

            sln.Interactive.GenerateApplicationExpressions +=
                AddCode =>
                {
                    AddCode(new CreateMySprite(GetType(), sprite));
                };

            return sln;
        }
    }
}
