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
using WebGLInteractiveCubes;
using WebGLInteractiveCubes.Design;
using WebGLInteractiveCubes.HTML.Pages;
using ScriptCoreLib.Lambda;


namespace WebGLInteractiveCubes
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public IHTMLCanvas canvas;
        public THREE.Mesh[] objects;


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            var camera = new THREE.PerspectiveCamera(70, Native.window.aspect, 1, 10000);
            camera.position.set(0, 300, 500);

            var scene = new THREE.Scene();

            var geometry = new THREE.BoxGeometry(100, 100, 100);

            Random random = new Random();

            objects = new THREE.Mesh[10];
            for (var i = 0; i < 10; i++)
            {
                var rgb = (int)(random.NextDouble() * 0xffffff);
                Console.WriteLine(new { i, rgb });

                var obj = new THREE.Mesh(geometry,
                    new THREE.MeshBasicMaterial(new { color = new THREE.Color(rgb) })
                    );


                obj.position.x = random.NextDouble() * 800 - 400;
                obj.position.y = random.NextDouble() * 800 - 400;
                obj.position.z = random.NextDouble() * 800 - 400;

                obj.scale.x = random.NextDouble() * 2 + 1;
                obj.scale.y = random.NextDouble() * 2 + 1;
                obj.scale.z = random.NextDouble() * 2 + 1;

                obj.rotation.x = random.NextDouble() * 2 * Math.PI;
                obj.rotation.y = random.NextDouble() * 2 * Math.PI;
                obj.rotation.z = random.NextDouble() * 2 * Math.PI;

                scene.add(obj);

                objects[i] = obj;

            }


            // THREE.Projector has been moved to /examples/js/renderers/Projector.js.
            var projector = new THREE.Projector();

            // ?
            var renderer = new THREE.WebGLRenderer();
            renderer.setClearColor(new THREE.Color(0xf0f0f0));
            //renderer.setSize(Native.window.Width, Native.window.Height);
            renderer.setSize();

            this.canvas = (IHTMLCanvas)renderer.domElement;
            //renderer.domElement.AttachToDocument();
            this.canvas.AttachToDocument();
            this.canvas.style.SetLocation(0, 0);
            this.canvas.css.style.cursor = IStyle.CursorEnum.pointer;
            this.canvas.css.active.style.cursor = IStyle.CursorEnum.move;

            canvas.onresize += delegate
            {
                camera.aspect = Native.window.aspect;
                camera.updateProjectionMatrix();

                //renderer.setSize(Native.window.Width, Native.window.Height);
                renderer.setSize();
            };

            canvas.onmousedown += (
                e =>
                {

                    e.preventDefault();

                    var px = (e.CursorX / (double)Native.window.Width);
                    var py = (e.CursorY / (double)Native.window.Height);

                    var vx = px * 2 - 1;
                    var vy = -py * 2 + 1;

                    Console.WriteLine(
                        new { e.CursorX, e.CursorY, px, py, vx, vy }
                        );

                    //var vector = new THREE.Vector3(
                    //  vx,
                    //  vy,
                    //  0.5);

                    var vector = new THREE.Vector3(
                  (e.CursorX / (double)Native.window.Width) * 2 - 1,
                 vy,
                 0.5);

                    //var vector = new THREE.Vector3(e.CursorX , e.CursorY, 1);

                    projector.unprojectVector(vector, camera);

                    //var raycaster = new THREE.Raycaster();
                    //raycaster.set(camera.position, vector.sub(camera.position).normalize());

                    // 
                    var raycaster = new THREE.Raycaster(camera.position, vector.sub(camera.position).normalize());
                    //var raycaster = projector.pickingRay(vector.sub(camera.position).normalize(), camera);

                    //var intersects = raycaster.intersectObjects(objects, true);
                    var intersects = raycaster.intersectObjects(objects);

                    Console.WriteLine("Intersects len " + intersects.Length);

                    if (intersects.Length > 0)
                    {
                        var holder = ((THREE.Mesh)intersects[0].@object);

                        var forthSmallestInnerCilinder = new THREE.Mesh(
                              new THREE.CylinderGeometry(10, 10, 10, 32),
                              new THREE.MeshPhongMaterial(
                                      new
                                      {
                                          specular = new THREE.Color(0xa0a0a0)
                                      })
                          );
                        forthSmallestInnerCilinder.position.y = ((THREE.Raycaster_intersectObject)intersects[0]).point.y;
                        forthSmallestInnerCilinder.position.x = ((THREE.Raycaster_intersectObject)intersects[0]).point.x;
                        forthSmallestInnerCilinder.position.z = ((THREE.Raycaster_intersectObject)intersects[0]).point.z;
                        scene.add(forthSmallestInnerCilinder);

                        // Console.WriteLine(holder.visible);
                        //intersects[0].material.color.setHex(random.NextDouble()*0xffffff);
                        Console.WriteLine("" + ((THREE.Raycaster_intersectObject)intersects[0]).point.x);
                        holder.material.color = new THREE.Color(0xf000ff);
                        foreach (var i in objects)
                        {
                            if (i == holder)
                            {
                                Console.WriteLine("Equals true");
                                i.material.color.setRGB(200, 0, 0);
                            }
                        }
                    }

                });

            var radius = 600;
            double theta = 0;



            Native.window.onframe += (e =>
            {
                if (this.canvas.parentNode == null)
                    return;

                theta += 0.1;

                camera.position.x = radius * Math.Sin(THREE.Math.degToRad(theta));
                camera.position.y = radius * Math.Sin(THREE.Math.degToRad(theta));
                camera.position.z = radius * Math.Cos(THREE.Math.degToRad(theta));
                camera.lookAt(scene.position);

                renderer.render(scene, camera);



            });


        }
    }
}
