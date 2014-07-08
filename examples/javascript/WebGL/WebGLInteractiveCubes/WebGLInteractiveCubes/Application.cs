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
           var scene = new THREE.Scene();

           var camera = new THREE.PerspectiveCamera(70, Native.window.Width / Native.window.Height, 1, 10000);
           camera.position.set(0, 300, 500);

           var geometry = new THREE.CubeGeometry(100, 100, 100);

            Random random = new Random();

            objects = new THREE.Mesh[10];
            for ( var i = 0; i < 10; i ++ ) {

                var obj = new THREE.Mesh(geometry, new THREE.MeshBasicMaterial(new { color = new THREE.Color((int)(random.NextDouble() * 0xffffff)) }));
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
           

                var projector = new THREE.Projector();

                var renderer = new THREE.CanvasRenderer();
				renderer.setClearColor(new THREE.Color(0xf0f0f0));
				renderer.setSize(Native.window.Width,Native.window.Height);

                this.canvas = (IHTMLCanvas)renderer.domElement;
                //renderer.domElement.AttachToDocument();
                this.canvas.AttachToDocument();
                this.canvas.style.SetLocation(0, 0);
                this.canvas.css.style.cursor = IStyle.CursorEnum.pointer;
                this.canvas.css.active.style.cursor = IStyle.CursorEnum.move;

                canvas.onresize += delegate
                {
                    camera.aspect = Native.window.Width / Native.window.Height;
                    camera.updateProjectionMatrix();

                    renderer.setSize(Native.window.Width, Native.window.Height);
                };

                canvas.onmousedown += (
                    e => {

                    e.preventDefault();

                    var vector = new THREE.Vector3((e.CursorX / Native.window.Width) * 2 - 1, -(e.CursorY / Native.window.Height) * 2 + 1, 0.5);
                    projector.unprojectVector( vector, camera );

                    var raycaster = new THREE.Raycaster();
                    raycaster.set(camera.position, vector.sub(camera.position).normalize());

                
                    //var raycaster = projector.pickingRay(vector.clone(), camera);

				    var intersects = raycaster.intersectObjects( objects, true );
                    Console.WriteLine("Intersects len " + intersects.Length);

				    if ( intersects.Length > 0 ) {
                        var holder = ((THREE.Mesh)intersects[0]);
                       // Console.WriteLine(holder.visible);
                        //intersects[0].material.color.setHex(random.NextDouble()*0xffffff);
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
