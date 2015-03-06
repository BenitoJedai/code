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
using THREELibrary.HTML.Pages;
//using THREE;

namespace THREELibrary
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
        public Application(IApp page)
        {
            Native.body.Clear();
            Native.body.style.margin = "0px";

            // http://www.highlander.co.uk/blog/2011/09/23/learning-three-js/

            //this sets the canvas size.
            var WIDTH = Native.window.Width;
            var HEIGHT = Native.window.Height;

            // camera attributes
            var VIEW_ANGLE = 45;
            var ASPECT = WIDTH / HEIGHT;
            var NEAR = 0.1;
            var FAR = 10000;

            var renderer = new THREE.WebGLRenderer();
            //I am choosing the WebGL renderer here, but you have others to choose from
            var camera = new THREE.PerspectiveCamera(VIEW_ANGLE, ASPECT, NEAR, FAR);
            //these variables have been set at the top of  our script
            var scene = new THREE.Scene(); //create a new scene
            // the camera starts at 0,0,0 so we need to pull back
            camera.position.z = 200;
            // start the renderer
            renderer.setSize(WIDTH, HEIGHT);

            // http://www.highlander.co.uk/blog/2011/09/23/learning-three-js/



            //create materials
            var material = new THREE.MeshLambertMaterial(new { color = 0xCC0000 });
            //var material = new THREE.MeshBasicMaterial({color: 0xCC0000});  //gives you just a flat colour – ugly

            // create a new mesh with sphere geometry
            var radius = 50;
            var segments = 16;
            var rings = 16;
            var mesh = new THREE.Mesh(new THREE.SphereGeometry(radius, segments, rings), material).AttachTo(scene);
            //scene.addChild(mesh);
            scene.add(mesh);


            renderer.domElement.AttachToDocument();

            var pointLight = new THREE.PointLight(0xFFFFFF);
            // set its position
            pointLight.position.x = 50;
            pointLight.position.y = 50;
            pointLight.position.z = 130;
            // add to the scene
            scene.add(pointLight);
            //scene.addLight(pointLight);
            // render our scene

            Native.window.onframe +=
                delegate
                {
                    renderer.render(scene, camera);
                };
        }

    }
}
