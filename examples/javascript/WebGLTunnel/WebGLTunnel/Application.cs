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
using WebGLTunnel.HTML.Pages;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript.WebGL;
using WebGLTunnel.Library;

namespace WebGLTunnel
{
    using gl = WebGLRenderingContext;
    using WebGLFloatArray = Float32Array;
    using WebGLUnsignedShortArray = Uint16Array;
    using WebGLTunnel.Shaders;
    using System.Collections.Generic;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            // view-source:http://www.rozengain.com/files/webgl/tunnel/

            #region __sylvester -> __glUtils -> InitializeContent
            new WebGLTunnel.References.__sylvester().Content.With(
               source =>
               {
                   source.onload +=
                       delegate
                       {
                           new WebGLTunnel.References.__glUtils().Content.With(
                               source2 =>
                               {
                                   source2.onload +=
                                       delegate
                                       {
                                           InitializeContent(page);


                                       };
                               }
                            ).AttachToDocument();


                       };
               }
            ).AttachToDocument();
            #endregion


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        public readonly Action Dispose;

        void InitializeContent(IDefaultPage page)
        {
            var vertices = new List<double>();
            var indices = new List<double>();
            var colors = new List<double>();
            var uvs = new List<double>();

            var radius = 7;
            var currentRadius = radius;
            var segments = 24;
            var spacing = 2;
            var numRings = 18;
            var index = 0;
            var currentTime = 0;
            var drawingMode = 0;


            #region generateGeometry
            Action generateGeometry = delegate
            {
                for (var ring = 0; ring < numRings; ring++)
                {
                    for (var segment = 0; segment < segments; segment++)
                    {
                        var degrees = (360 / segments) * segment;
                        var radians = (Math.PI / 180) * degrees;
                        var x = Math.Cos(radians) * currentRadius;
                        var y = Math.Sin(radians) * currentRadius;
                        var z = ring * -spacing;

                        vertices.Add(x, y, z);

                        if (segment < (segments - 1) / 2)
                        {
                            uvs.Add((1.0 / (segments)) * segment * 2, (1.0 / 4) * ring);
                        }
                        else
                        {
                            uvs.Add(2.0 - ((1.0 / (segments)) * segment * 2), (1.0 / 4) * ring);
                        }

                        var color = 1.0 - ((1.0 / (numRings - 1)) * ring);
                        colors.Add(color, color, color, 1.0);

                        if (ring < numRings - 1)
                        {
                            if (segment < segments - 1)
                            {
                                indices.Add(index, index + segments + 1, index + segments);
                                indices.Add(index, index + 1, index + segments + 1);
                            }
                            else
                            {
                                indices.Add(index, index + 1, index + segments);
                                indices.Add(index, index - segments + 1, index + 1);
                            }
                        }

                        index++;
                    }
                    currentRadius -= radius / numRings;
                }
            };
            #endregion


            generateGeometry();

        }
    }

}
