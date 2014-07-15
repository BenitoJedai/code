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
using WebGLRotateCameraAroundObject;
using WebGLRotateCameraAroundObject.Design;
using WebGLRotateCameraAroundObject.HTML.Pages;
using ScriptCoreLib.Lambda;


namespace WebGLRotateCameraAroundObject
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public IHTMLCanvas canvas;
        public THREE.Scene scene;
        public THREE.Object3D group;
        public THREE.PerspectiveCamera camera;
        public double radious = 1600, theta = 45, onMouseDownTheta = 45, phi = 60, onMouseDownPhi = 60;
        public bool isMouseDown = false;
        public THREE.Vector2 onMouseDownPosition;


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            onMouseDownPosition = new THREE.Vector2();

            //camera = new THREE.PerspectiveCamera(
            //   45,
            //   Native.window.aspect,
            //   1,
            //   1000
            //    //2000
            //   );
            //camera.position.z = 400;

            camera = new THREE.PerspectiveCamera(40, Native.window.Width / Native.window.Height, 1, 10000);
            camera.position.x = radious * Math.Sin(theta * Math.PI / 360) * Math.Cos(phi * Math.PI / 360);
            camera.position.y = radious * Math.Sin(phi * Math.PI / 360);
            camera.position.z = radious * Math.Cos(theta * Math.PI / 360) * Math.Cos(phi * Math.PI / 360);
            

            scene = new THREE.Scene();
            var ambientLight = new THREE.AmbientLight(0x404040);
            scene.add(ambientLight);

            var directionalLight = new THREE.DirectionalLight(0xffeedd);
            directionalLight.position.set(0, 0, 1);
            scene.add(directionalLight);

            var obj = new THREE.Object3D();


            var buttomCylinder1 = new THREE.Mesh(
               new THREE.CylinderGeometry(50, 50, 50, 32),
               new THREE.MeshPhongMaterial(
                       new
                       {
                           specular = new THREE.Color(0xa0a0a0),
                           color = new THREE.Color(0xa0a0a0),
                       })
           );
            buttomCylinder1.rotation.x = - 90 * Math.PI / 180;

            //buttomCylinder1.position.x = 0;
            //buttomCylinder1.position.y = 0;
            //buttomCylinder1.position.z = 0;

            obj.add(buttomCylinder1);
            scene.add(obj);
            

            var renderer = new THREE.WebGLRenderer(
                   new
                   {
                       preserveDrawingBuffer = true
                   }

                );
            renderer.setSize(Native.window.Width, Native.window.Height);
            this.canvas = (IHTMLCanvas)renderer.domElement;
            this.canvas.AttachToDocument();
            this.canvas.style.SetLocation(0, 0);

            camera.lookAt(scene.position);
            scene.add(obj);

            //var plane = new THREE.Mesh(new THREE.Plane(1000, 1000));
            //plane.rotation.x = -90 * Math.PI / 180;
            //scene.add(plane);

            #region onmousedown
            this.canvas.onmousedown +=
                e =>
                {
                    e.preventDefault();

                    isMouseDown = true;

                    onMouseDownTheta = theta;
                    onMouseDownPhi = phi;
                    onMouseDownPosition.x = e.CursorX;
                    onMouseDownPosition.y = e.CursorY;

                };
            #endregion


            this.canvas.onmousewheel +=
                e =>
                {
                    radious -= e.WheelDirection;

                    camera.position.x = radious * Math.Sin(theta * Math.PI / 360) * Math.Cos(phi * Math.PI / 360);
                    camera.position.y = radious * Math.Sin(phi * Math.PI / 360);
                    camera.position.z = radious * Math.Cos(theta * Math.PI / 360) * Math.Cos(phi * Math.PI / 360);
                    camera.updateMatrix();
                };

            #region onmousemove
            this.canvas.onmousemove +=
                e =>
                {
                    e.preventDefault();

                    if (isMouseDown)
                    {

                        theta = -((e.CursorX - onMouseDownPosition.x) * 0.5) + onMouseDownTheta;
                        phi = ((e.CursorX - onMouseDownPosition.y) * 0.5) + onMouseDownPhi;

                        phi = Math.Min(360, Math.Max(0, phi));

                        camera.position.x = radious * Math.Sin(theta * Math.PI / 360) * Math.Cos(phi * Math.PI / 360);
                        camera.position.y = radious * Math.Sin(phi * Math.PI / 360);
                        camera.position.z = radious * Math.Cos(theta * Math.PI / 360) * Math.Cos(phi * Math.PI / 360);
                        camera.updateMatrix();

                    }


                };
            #endregion

            this.canvas.onmouseup += e =>
                {
                    e.preventDefault();

                    isMouseDown = false;

                    onMouseDownPosition.x = e.CursorX - onMouseDownPosition.x;
                    onMouseDownPosition.y = e.CursorY - onMouseDownPosition.y;
                };

            


            // could we 
            Native.window.onframe +=
                e =>
                {
                    renderer.clear();
                    camera.updateProjectionMatrix();
                    camera.lookAt(scene.position);
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
