using System;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using WebGLClouds.HTML.Pages;
using WebGLClouds.Shaders;

namespace WebGLClouds
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using THREE = WebGLClouds.Design.THREE;


    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* Source: view-source:http://mrdoob.com/lab/javascript/webgl/clouds/
         */

        public static bool DisableBackground;
        public static double DefaultMouseY = 0.4;

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page = null)
        {
            #region ThreeExtras - InitializeContent
            new WebGLClouds.Design.THREE.__ThreeWebGL().Content.With(
               source2 =>
               {
                   source2.onload +=
                       delegate
                       {

                           new WebGLClouds.Design.THREE.__ThreeExtras().Content.With(
                              source =>
                              {
                                  source.onload +=
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

        sealed class MyUniforms
        {
            public sealed class MyUniform
            {
                public string type;
                public object value;
                public object texture;

            }

            public MyUniform map;
            public MyUniform fogColor;
            public MyUniform fogNear;
            public MyUniform fogFar;
        }

        void InitializeContent(IDefaultPage page)
        {
            //page.PageContainer.style.color = Color.Blue;

            if (DisableBackground)
            {
                // nop
            }
            else
            {
                Native.Document.body.style.backgroundColor = "#4584b4";
            }

            // Bg gradient

            //var canvas = new IHTMLCanvas();
            //canvas.width = 32;
            //canvas.height = Native.Window.Height;

            //var context = (CanvasRenderingContext2D)canvas.getContext("2d");

            //var gradient = context.createLinearGradient(0, 0, 0, canvas.height);
            //gradient.addColorStop(0f, "#1e4877");
            //gradient.addColorStop(0.5f, "#4584b4");

            //context.fillStyle = gradient;
            //context.fillRect(0, 0, canvas.width, canvas.height);


            // Clouds

            //var , , renderer, sky, mesh, , material,
            //, h, color, colors = [], sprite, size, x, y, z;

            var mouseX = 0f;
            var start_time = new IDate().getTime();

            var windowHalfX = Native.Window.Width / 2;
            var windowHalfY = Native.Window.Height / 2;

            Console.WriteLine(new { DefaultMouseY });

            var mouseY = (float)((Native.Window.Height * DefaultMouseY - windowHalfY) * 0.15);

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            var container = new IHTMLDiv();

            container.AttachToDocument();
            container.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);
            //container.style.background = "url(" + canvas.toDataURL("image/png") + ")";

            #region Dispose
            var IsDisposed = false;

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                container.Orphanize();
            };
            #endregion





            var camera = new THREE.Camera(30, Native.Window.Width / Native.Window.Height, 1, 3000);
            camera.position.z = 6000;

            var scene = new THREE.Scene();

            var geometry = new THREE.Geometry();

            var texture = THREE.__ThreeExtras.ImageUtils.loadTexture(new HTML.Images.FromAssets.cloud10().src);

            var THREE_LinearMipMapLinearFilter = 8;


            texture.magFilter = THREE_LinearMipMapLinearFilter;
            texture.minFilter = THREE_LinearMipMapLinearFilter;

            var fog = new THREE.Fog(0x4584b4, -100, 3000);

            #region material
            var material = new THREE.MeshShaderMaterial(

                new THREE.MeshShaderMaterialArguments39
                {
                    uniforms = new MyUniforms
                    {
                        map = new MyUniforms.MyUniform { type = "t", value = 2, texture = texture },
                        fogColor = new MyUniforms.MyUniform { type = "c", value = fog.color },
                        fogNear = new MyUniforms.MyUniform { type = "f", value = fog.near },
                        fogFar = new MyUniforms.MyUniform { type = "f", value = fog.far },
                    },

                    vertexShader = new GeometryVertexShader().ToString(),
                    fragmentShader = new GeometryFragmentShader().ToString(),
                    depthTest = false

                }
            );
            #endregion


            var r = new Random();
            Func<float> Math_random = () => (float)r.NextDouble();

            var plane = new THREE.Mesh(new THREE.Plane(64, 64));

            for (var i = 0; i < 8000; i++)
            {

                plane.position.x = Math_random() * 1000 - 500;
                plane.position.y = -Math_random() * Math_random() * 200 - 15;
                plane.position.z = i;
                plane.rotation.z = (f)(Math_random() * Math.PI);
                plane.scale.x = Math_random() * Math_random() * 1.5f + 0.5f;
                plane.scale.y = plane.scale.x;

                THREE.__ThreeExtras.GeometryUtils.merge(geometry, plane);

            }

            var mesh = new THREE.Mesh(geometry, material);
            scene.addObject(mesh);

            mesh = new THREE.Mesh(geometry, material);
            mesh.position.z = -8000;
            scene.addObject(mesh);

            var renderer = new THREE.WebGLRenderer(new THREE.WebGLRendererArguments { antialias = false });
            renderer.setSize(Native.Window.Width, Native.Window.Height);
            container.appendChild(renderer.domElement);


            #region onresize
            Native.Window.onresize +=
                delegate
                {
                    container.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);

                    camera.aspect = Native.Window.Width / Native.Window.Height;
                    camera.updateProjectionMatrix();

                    renderer.setSize(Native.Window.Width, Native.Window.Height);
                };
            #endregion


            #region render
            Action render = () =>
            {

                var position = ((new IDate().getTime() - start_time) * 0.03) % 8000;

                camera.position.x += (float)((mouseX - camera.target.position.x) * 0.01);
                camera.position.y += (float)((-mouseY - camera.target.position.y) * 0.01);
                camera.position.z = (f)(-position + 8000);

                camera.target.position.x = camera.position.x;
                camera.target.position.y = camera.position.y;
                camera.target.position.z = camera.position.z - 1000;

                renderer.render(scene, camera);

            };
            #endregion


            #region animate
            Action animate = null;

            animate = delegate
            {
                if (IsDisposed)
                    return;

                render();

                Native.Window.requestAnimationFrame += animate;
            };

            Native.Window.requestAnimationFrame += animate;
            #endregion

            Native.Document.onmousemove +=
                e =>
                {
                    mouseX = (float)((e.CursorX - windowHalfX) * 0.25);
                    mouseY = (float)((e.CursorY - windowHalfY) * 0.15);
                };



            #region requestFullscreen
            Native.Document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    Native.Document.body.requestFullscreen();


                };
            #endregion

        }
        public Action Dispose;

    }


}
