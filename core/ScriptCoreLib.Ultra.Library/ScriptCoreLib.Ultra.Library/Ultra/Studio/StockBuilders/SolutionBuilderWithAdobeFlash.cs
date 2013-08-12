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





                    // as per X:\jsc.svn\market\synergy\actionscript\Flare3DWaterShipComponent\Flare3DWaterShipComponent\ApplicationSprite.cs
                    // jsc does not like field initializers to properties in anonymous type?
                    var Types_ship = new SolutionProjectLanguageType { Namespace = "Flare3DWaterShipComponent", Name = "ship" };
                    var Types_Viewer3D = new SolutionProjectLanguageType { Namespace = "flare.basic", Name = "Viewer3D" };
                    var Types_Camera3D = new SolutionProjectLanguageType { Namespace = "flare.core", Name = "Camera3D" };

                    var Methods_set_scene = new SolutionProjectLanguageMethod
                        {
                            Name = "set_scene",
                            IsProperty = true,
                            DeclaringType = ApplicationSprite
                        };

                    var Methods_set_camera = new SolutionProjectLanguageMethod
                    {
                        Name = "set_camera",
                        IsProperty = true,
                        DeclaringType = Types_Viewer3D
                    };

                    var Methods_setPosition = new SolutionProjectLanguageMethod
                    {
                        Name = "setPosition",
                        DeclaringType = Types_Camera3D
                    };
                    var Methods_lookAt = new SolutionProjectLanguageMethod
                   {
                       Name = "lookAt",
                       DeclaringType = Types_Camera3D
                   };
                    var Methods_addChild = new SolutionProjectLanguageMethod
                    {
                        Name = "addChild",
                        DeclaringType = Types_Viewer3D
                    };

                    var this_camera = Types_Camera3D.ToInitializedField("camera");
                    var this_ship = Types_ship.ToInitializedField("ship");

                    var this_scene = new SolutionProjectLanguageField
                    {
                        FieldType = Types_Viewer3D,
                        Name = "scene",
                        IsReadOnly = true
                    };

                    //ApplicationSprite.Fields.Add(this_ship);
                    ApplicationSprite.Fields.Add(this_camera);
                    ApplicationSprite.Fields.Add(this_scene);


                    var newobj_Viewer3D = new PseudoCallExpression
                    {

                        Method = Types_Viewer3D.GetDefaultConstructorDefinition(),

                        ParameterExpressions = new object[] {
                            new PseudoThisExpression()
                        }
                    };


                    var set_scene_to_newobj_Viewer3D = Methods_set_scene.ToCallExpression(
                        new PseudoThisExpression(), newobj_Viewer3D
                    );

                    ApplicationSprite.Constructor.Code.Add(set_scene_to_newobj_Viewer3D);


                    var this_camera_setPosition = Methods_setPosition.ToCallExpression(this_camera,
                          (PseudoInt32ConstantExpression)120,
                          (PseudoInt32ConstantExpression)40,
                          (PseudoInt32ConstantExpression)(-30)
                      );

                    ApplicationSprite.Constructor.Code.Add(this_camera_setPosition);

                    var this_camera_lookAt = Methods_lookAt.ToCallExpression(this_camera,
                            (PseudoInt32ConstantExpression)0,
                            (PseudoInt32ConstantExpression)0,
                            (PseudoInt32ConstantExpression)0
                    );

                    ApplicationSprite.Constructor.Code.Add(this_camera_lookAt);


                    ApplicationSprite.Constructor.Code.Add(
                          Methods_set_camera.ToCallExpression(this_scene, this_camera)
                    );


                    ApplicationSprite.Constructor.Code.Add(
                        Methods_addChild.ToCallExpression(this_scene, Types_ship.GetDefaultConstructor())
                    );

                    AddType(ApplicationSprite);
                };




            sln.Interactive.GenerateApplicationExpressions +=
                AddCode =>
                {
                    AddCode(new CreateMySprite(GetType(), sprite));
                };

            // ..\packages\Flare3D.1.0.0.0\lib\Flare3D.dll
            sln.NuGetReferences.Add(
                new ScriptCoreLib.Ultra.Studio.SolutionBuilder.package { id = "Flare3D" }
            );
            sln.NuGetReferences.Add(
                new ScriptCoreLib.Ultra.Studio.SolutionBuilder.package { id = "Flare3DWaterShipComponent" }
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
