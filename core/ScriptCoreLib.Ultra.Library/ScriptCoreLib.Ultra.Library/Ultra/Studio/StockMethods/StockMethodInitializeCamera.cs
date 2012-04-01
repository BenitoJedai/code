using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;

namespace ScriptCoreLib.Ultra.Studio.StockMethods
{
    public class StockMethodInitializeCamera : SolutionProjectLanguageMethod
    {
        public StockMethodInitializeCamera(SolutionProjectLanguageField video)
        {
            var camera = new SolutionProjectLanguageArgument
            {
                Name = "camera",
                Type = new KnownStockTypes.ScriptCoreLib.ActionScript.flash.media.Camera()
            };

            this.Parameters.Add(camera);



            var camera_setMode =new KnownStockTypes.ScriptCoreLib.ActionScript.flash.media.Camera.setMode().ToCallExpression(
                  camera,
                (PseudoInt32ConstantExpression)500,
                (PseudoInt32ConstantExpression)400,
                (PseudoInt32ConstantExpression)(1000 / 24)
            );

            var video_attachCamera = new KnownStockTypes.ScriptCoreLib.ActionScript.flash.media.Video.attachCamera().ToCallExpression(
                video,
                camera
            );

         


            var AttachTo = new SolutionProjectLanguageMethod
            {
                IsStatic = true,
                IsExtensionMethod = true,
                Name = "AttachTo",
                DeclaringType = new KnownStockTypes.ScriptCoreLib.ActionScript.Extensions.CommonExtensions()
            };

            var video_AttachTo_this =
                new PseudoCallExpression
                {
                    Method = AttachTo,
                    ParameterExpressions = new object[]
                    {
                        video,
                        new PseudoThisExpression()
                    }
                };

            this.Code = new SolutionProjectLanguageCode
            {
                camera_setMode,
                video_attachCamera,
                video_AttachTo_this
            };
        }

    }
}
