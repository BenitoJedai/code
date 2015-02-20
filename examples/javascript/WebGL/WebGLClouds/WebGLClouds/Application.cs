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
    using WebGLRah66Comanche.Library;
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;


    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /* Source: view-source:http://mrdoob.com/lab/javascript/webgl/clouds/
         */

        public static int FogColor = 0x4584b4;

        public static bool DisableBackground;
        public static double DefaultMouseY = 0.4;

        public static string CloudTexture = new HTML.Images.FromAssets.cloud10().src;



        // hack for async ctor
        public static event Action<Application> Loaded;

        public IHTMLDiv container = new IHTMLDiv();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            // used by
            // X:\jsc.svn\examples\javascript\WebGL\WebGLGoldDropletTransactions\WebGLGoldDropletTransactions\Application.cs


            if (DisableBackground)
            {
                // nop
            }
            else
            {
                //page.body.style.backgroundColor = "#4584b4";

                container.style.backgroundColor = "#4584b4";
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

            Console.WriteLine(new { Native.window.Width, Native.window.Height });

            var windowHalfX = Native.window.Width / 2;
            var windowHalfY = Native.window.Height / 2;

            Console.WriteLine(new { DefaultMouseY });

            var mouseY = (float)((Native.window.Height * DefaultMouseY - windowHalfY) * 0.15);

            //Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;

            if (page == null)
            {
                container.AttachToDocument();
            }
            else
            {
                container.AttachTo(page.body);
            }

            container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);
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





            var camera = new THREE.PerspectiveCamera(30, Native.window.aspect, 1, 3000);
            camera.position.z = 6000;

            var scene = new THREE.Scene();

            var geometry = new THREE.Geometry();

            //var texture = THREE.ImageUtils.loadTexture(new HTML.Images.FromAssets.cloud10().src);
            var texture = THREE.ImageUtils.loadTexture(CloudTexture);

            texture.magFilter = THREE.LinearMipMapLinearFilter;
            texture.minFilter = THREE.LinearMipMapLinearFilter;

            // FogColor
            //var fog = new THREE.Fog(0x4584b4, -100, 3000);
            var fog = new THREE.Fog(FogColor, -100, 3000);


            // what about sprites?
            var material = new THREE.ShaderMaterial(

                new
                {
                    uniforms = new
                    {
                        map = new { type = "t", value = texture },
                        fogColor = new { type = "c", value = fog.color },
                        fogNear = new { type = "f", value = fog.near },
                        fogFar = new { type = "f", value = fog.far },
                    },

                    vertexShader = new GeometryVertexShader().ToString(),
                    fragmentShader = new GeometryFragmentShader().ToString(),
                    depthWrite = false,
                    depthTest = false,
                    transparent = true

                }
            );

            var r = new Random();
            Func<float> Math_random = () => (float)r.NextDouble();

            var plane = new THREE.Mesh(new THREE.PlaneGeometry(64, 64));

            for (var i = 0; i < 8000; i++)
            {

                plane.position.x = Math_random() * 1000 - 500;
                plane.position.y = -Math_random() * Math_random() * 200 - 15;
                plane.position.z = i;
                plane.rotation.z = (f)(Math_random() * Math.PI);
                plane.scale.x = Math_random() * Math_random() * 1.5f + 0.5f;
                plane.scale.y = plane.scale.x;

                THREE.GeometryUtils.merge(geometry, plane);

            }

            var mesh = new THREE.Mesh(geometry, material);
            scene.add(mesh);

            mesh = new THREE.Mesh(geometry, material);
            mesh.position.z = -8000;
            scene.add(mesh);

            var renderer = new THREE.WebGLRenderer(new { antialias = false });
            renderer.setSize(Native.window.Width, Native.window.Height);
            container.appendChild(renderer.domElement);

            container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);

            #region onresize
            Native.window.onresize +=
                delegate
                {
                    container.style.SetSize(Native.window.Width, Native.window.Height);

                    camera.aspect = Native.window.aspect;
                    camera.updateProjectionMatrix();

                    renderer.setSize(Native.window.Width, Native.window.Height);
                };
            #endregion





            #region animate

            Native.window.onframe += delegate
            {
                if (IsDisposed)
                    return;

                var position = ((new IDate().getTime() - start_time) * 0.03) % 8000;


                camera.position.x += (float)((mouseX - camera.position.x) * 0.01);
                camera.position.y += (float)((-mouseY - camera.position.y) * 0.01);

                camera.position.z = (f)(-position + 8000);



                renderer.render(scene, camera);

            };

            #endregion

            Native.document.onmousemove +=
                e =>
                {
                    mouseX = (float)((e.CursorX - windowHalfX) * 0.25);
                    mouseY = (float)((e.CursorY - windowHalfY) * 0.15);
                };



            var ze = new ZeProperties();

            ze.Show();


            ze.Add(() => renderer);
            //ze.Add(() => controls);
            ze.Add(() => scene);

        }
        public Action Dispose;

    }


}
