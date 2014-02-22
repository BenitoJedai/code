using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGLEarthByBjorn;
using WebGLEarthByBjorn.Design;
using WebGLEarthByBjorn.HTML.Images.FromAssets;
using WebGLEarthByBjorn.HTML.Pages;

namespace WebGLEarthByBjorn
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public IHTMLCanvas canvas;



        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //http://thematicmapping.org/playground/webgl/earth/
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140222


            // Earth params
            var radius = 0.5;
            var segments = 32;
            var rotation = 6;

            var scene = new THREE.Scene();

            var camera = new THREE.PerspectiveCamera(45, Native.window.aspect, 0.01, 1000);
            camera.position.z = 1.5;

            var renderer = new THREE.WebGLRenderer(
                   new
                   {
                       preserveDrawingBuffer = true
                   }

                );
            renderer.setSize();

            scene.add(new THREE.AmbientLight(0x333333));

            var light = new THREE.DirectionalLight(0xffffff, 1);
            light.position.set(5, 3, 5);
            scene.add(light);









            var sphere = new THREE.Mesh(
                            new THREE.SphereGeometry(radius, segments, segments),
                            new THREE.MeshPhongMaterial(
                                    new
                                    {
                                        map = THREE.ImageUtils.loadTexture(new _2_no_clouds_4k().src),
                                        bumpMap = THREE.ImageUtils.loadTexture(new elev_bump_4k().src),
                                        bumpScale = 0.005,
                                        specularMap = THREE.ImageUtils.loadTexture(new water_4k().src),
                                        //specular =    new THREE.Color("grey")								
                                        specular = new THREE.Color(0xa0a0a0)
                                    })
                        );

            sphere.rotation.y = rotation;
            scene.add(sphere);


            var clouds = new THREE.Mesh(
                    new THREE.SphereGeometry(radius + 0.003, segments, segments),
                    new THREE.MeshPhongMaterial(
                        new
                        {
                            map = THREE.ImageUtils.loadTexture(new fair_clouds_4k().src),
                            transparent = true
                        })
                );
            clouds.rotation.y = rotation;
            scene.add(clouds);






            var stars = new THREE.Mesh(
                    new THREE.SphereGeometry(90, 64, 64),
                    new THREE.MeshBasicMaterial(
                    new
                    {
                        map = THREE.ImageUtils.loadTexture(new galaxy_starfield().src),
                        side = THREE.BackSide
                    })
                );
            scene.add(stars);

            //var controls = new THREE.TrackballControls(camera);
            //Native.document.body.style.margin = "0";
            //Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;

            this.canvas = (IHTMLCanvas)renderer.domElement;

            //renderer.domElement.AttachToDocument();
            this.canvas.AttachToDocument();
            this.canvas.style.SetLocation(0, 0);

            this.canvas.onmousemove +=
                e =>
                {
                    if (e.MouseButton == IEvent.MouseButtonEnum.Left)
                    {
                        sphere.rotation.x = Math.PI * e.OffsetY / canvas.clientHeight;
                        clouds.rotation.x = Math.PI * e.OffsetY / canvas.clientHeight;
                    }

                };

            Native.window.onframe +=
                delegate
                {
                    if (this.canvas.parentNode == null)
                        return;

                    camera.aspect = canvas.clientWidth / (double)canvas.clientHeight;
                    camera.updateProjectionMatrix();

                    // the larger the vew the slower the rotation shall be
                    var speed = 0.0005 + 0.009 * 96.0 / canvas.clientHeight;

                    //Native.document.title = new { s = 96.0 / canvas.clientHeight }.ToString();
                    //Native.document.title = new { speed }.ToString();


                    //controls.update();
                    sphere.rotation.y += speed;
                    clouds.rotation.y += speed;
                    renderer.render(scene, camera);
                };

            Native.window.onresize +=
                delegate
                {


                    //if (canvas.parentNode == Native.document.body)

                    // are we embedded?
                    if (page != null)
                        renderer.setSize();
                };


        }

    }
}
