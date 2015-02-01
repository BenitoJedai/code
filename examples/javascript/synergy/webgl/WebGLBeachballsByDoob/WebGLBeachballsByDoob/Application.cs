using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLBeachballsByDoob.Design;
using WebGLBeachballsByDoob.HTML.Pages;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebGLBeachballsByDoob
{
    sealed class __WebGLRendererDictionary
    {
        public bool antialias;
        public bool alpha;
    }

    sealed class __MeshBasicMaterialDictionary
    {
        public bool wireframe;
        public double opacity;
        public bool transparent;

    }

    sealed class __MeshPhongMaterialDictionary
    {
        public int vertexColors;
        public int specular;
        public int shininess;
    }

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page = null)
        {
            // 20140704 no balls shown?
            // broken?
            // view-source:http://www.mrdoob.com/lab/javascript/beachballs/
            //Action Toggle = DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();


            var origin = new THREE.Vector3(0, 15, 0);
            var isMouseDown = false;



            var renderer = new THREE.WebGLRenderer(
                new
                {
                    antialias = true,
                    alpha = false
                });
            renderer.setClearColor(new THREE.Color(0x101010));

            renderer.domElement.AttachToDocument();



            // scene

            var camera = new THREE.PerspectiveCamera(
                40,
                Native.window.aspect, 1,
                1000
                );

            camera.position.x = -30;
            camera.position.y = 10;
            camera.position.z = 30;
            camera.lookAt(new THREE.Vector3(0, 10, 0));


            #region AtResize
            Action AtResize = delegate
            {
                camera.aspect = (double)Native.window.aspect;
                camera.updateProjectionMatrix();
                renderer.setSize(Native.window.Width, Native.window.Height);
            };
            Native.window.onresize +=
              delegate
              {
                  AtResize();
              };

            AtResize();
            #endregion


            var scene = new THREE.Scene();

            var light = new THREE.HemisphereLight(0xffffff, 0x606060, 1.2);
            light.position.set(-10, 10, 10);
            scene.add(light);

            {
                var geometry = new THREE.CubeGeometry(20, 20, 20);
                var material = new THREE.MeshBasicMaterial(
                    new { wireframe = true, opacity = 0.1, transparent = true });
                var mesh = new THREE.Mesh(geometry, material);
                mesh.position.y = 10;
                scene.add(mesh);
            }

            var intersectionPlane = new THREE.Mesh(new THREE.PlaneGeometry(20, 20, 8, 8));
            intersectionPlane.position.y = 10;
            intersectionPlane.visible = false;
            scene.add(intersectionPlane);

            // geometry

            var ballGeometry = new THREE.Geometry();

            var ballMaterial = new THREE.MeshPhongMaterial(

                new __MeshPhongMaterialDictionary
                {
                    vertexColors = THREE.FaceColors,
                    specular = 0x808080,
                    shininess = 2000
                }
             );

            //

            var colors = new[] {
                    new THREE.Color( 0xe52b30 ),
                    new THREE.Color( 0xe52b30 ),
                    new THREE.Color( 0x2e1b6a ),
                    new THREE.Color( 0xdac461 ),
                    new THREE.Color( 0xf07017 ),
                    new THREE.Color( 0x38b394 ),
                    new THREE.Color( 0xeaf1f7 )
                };

            var amount = colors.Length;

            var geometryTop = new THREE.SphereGeometry(1, 5 * amount, 2, 0, Math.PI * 2.0, 0, 0.30);

            for (var j = 0; j < geometryTop.faces.Length; j++)
            {

                geometryTop.faces[j].color = colors[0];

            }

            THREE.GeometryUtils.merge(ballGeometry, geometryTop);

            var geometryBottom = new THREE.SphereGeometry(1, 5 * amount, 2, 0, Math.PI * 2, Math.PI - 0.30, 0.30);

            for (var j = 0; j < geometryBottom.faces.Length; j++)
            {

                geometryBottom.faces[j].color = colors[0];

            }

            THREE.GeometryUtils.merge(ballGeometry, geometryBottom);

            {
                var sides = amount - 1;
                var size = (Math.PI * 2) / sides;

                for (var i = 0; i < sides; i++)
                {

                    var patch = new THREE.SphereGeometry(1, 5, 10, i * size, size, 0.30, Math.PI - 0.60);

                    for (var j = 0; j < patch.faces.Length; j++)
                    {

                        patch.faces[j].color = colors[i + 1];

                    }

                    THREE.GeometryUtils.merge(ballGeometry, patch);

                }

            }
            // physics

            var world = new CANNON.World();
            world.broadphase = new CANNON.NaiveBroadphase();
            world.gravity.set(0, -15, 0);
            world.solver.iterations = 7;
            world.solver.tolerance = 0.1;

            var groundShape = new CANNON.Plane();
            var groundMaterial = new CANNON.Material();
            var groundBody = new CANNON.RigidBody(0, groundShape, groundMaterial);
            groundBody.quaternion.setFromAxisAngle(new CANNON.Vec3(1, 0, 0), -Math.PI / 2.0);
            world.add(groundBody);

            var planeShapeXmin = new CANNON.Plane();
            var planeXmin = new CANNON.RigidBody(0, planeShapeXmin, groundMaterial);
            planeXmin.quaternion.setFromAxisAngle(new CANNON.Vec3(0, 1, 0), Math.PI / 2.0);
            planeXmin.position.set(-10, 0, 0);
            world.add(planeXmin);

            var planeShapeXmax = new CANNON.Plane();
            var planeXmax = new CANNON.RigidBody(0, planeShapeXmax, groundMaterial);
            planeXmax.quaternion.setFromAxisAngle(new CANNON.Vec3(0, 1, 0), -Math.PI / 2.0);
            planeXmax.position.set(10, 0, 0);
            world.add(planeXmax);

            var planeShapeYmin = new CANNON.Plane();
            var planeZmin = new CANNON.RigidBody(0, planeShapeYmin, groundMaterial);
            planeZmin.position.set(0, 0, -10);
            world.add(planeZmin);

            var planeShapeYmax = new CANNON.Plane();
            var planeZmax = new CANNON.RigidBody(0, planeShapeYmax, groundMaterial);
            planeZmax.quaternion.setFromAxisAngle(new CANNON.Vec3(0, 1, 0), Math.PI);
            planeZmax.position.set(0, 0, 10);
            world.add(planeZmax);

            var ballBodyMaterial = new CANNON.Material();
            world.addContactMaterial(new CANNON.ContactMaterial(groundMaterial, ballBodyMaterial, 0.2, 0.5));
            world.addContactMaterial(new CANNON.ContactMaterial(ballBodyMaterial, ballBodyMaterial, 0.2, 0.8));



            var spheres = new Queue<THREE.Mesh>();
            var bodies = new Queue<CANNON.RigidBody>();

            Func<double> random = new Random().NextDouble;


            #region addBall
            Action<double, double, double> addBall = (x, y, z) =>
            {

                x = Math.Max(-10, Math.Min(10, x));
                y = Math.Max(5, y);
                z = Math.Max(-10, Math.Min(10, z));

                var size = 1.25;

                var sphere = new THREE.Mesh(ballGeometry, ballMaterial);
                sphere.scale.multiplyScalar(size);
                //sphere.useQuaternion = true;
                scene.add(sphere);

                spheres.Enqueue(sphere);

                var sphereShape = new CANNON.Sphere(size);
                var sphereBody = new CANNON.RigidBody(0.1, sphereShape, ballBodyMaterial);
                sphereBody.position.set(x, y, z);

                var q = new
                {
                    a = random() * 3.0,
                    b = random() * 3.0,
                    c = random() * 3.0,
                    d = random() * 3.0
                };
                Console.WriteLine("addBall " + new { x, y, z, q });

                //sphereBody.quaternion.set(q.a, q.b, q.c, q.d);
                world.add(sphereBody);

                bodies.Enqueue(sphereBody);

            };
            #endregion


            for (var i = 0; i < 100; i++)
            {

                addBall(
                   random() * 10 - 5,
                   random() * 20,
                   random() * 10 - 5
                );

            }

            //

            var projector = new THREE.Projector();
            var ray = new THREE.Raycaster();
            var mouse3D = new THREE.Vector3();

            Native.Document.body.ontouchstart +=
               e =>
               {
                   e.preventDefault();
                   isMouseDown = true;
               };


            Native.Document.body.ontouchmove +=
               e =>
               {
                   e.preventDefault();
               };

            Native.Document.body.ontouchend +=
            e =>
            {
                e.preventDefault();
                isMouseDown = false;
            };

            #region onmousemove
            Native.document.body.onmousedown +=
                e =>
                {
                    e.preventDefault();
                    isMouseDown = true;
                };
            Native.document.body.onmouseup +=
                 e =>
                 {
                     e.preventDefault();
                     isMouseDown = false;



                     if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                     {
                         if (Native.Document.pointerLockElement == Native.Document.body)
                         {
                             // cant requestFullscreen while pointerLockElement
                             Console.WriteLine("exitPointerLock");
                             Native.Document.exitPointerLock();
                             Native.Document.exitFullscreen();
                             return;
                         }

                         Console.WriteLine("requestFullscreen");
                         Native.Document.body.requestFullscreen();
                         Native.Document.body.requestPointerLock();
                         return;
                     }
                 };
            Native.document.body.onmousemove +=
               e =>
               {

                   mouse3D.set(
                       ((double)e.CursorX / (double)Native.window.Width) * 2 - 1,
                       -((double)e.CursorY / (double)Native.window.Height) * 2 + 1,
                       0.5
                   );

                   projector.unprojectVector(mouse3D, camera);

                   ray.set(camera.position, mouse3D.sub(camera.position).normalize());

                   var intersects = ray.intersectObject(intersectionPlane);

                   if (intersects.Length > 0)
                   {

                       origin.copy(intersects[0].point);

                   }
               };
            #endregion








            #region removeBall
            Action removeBall = delegate
            {

                scene.remove(spheres.Dequeue());
                world.remove(bodies.Dequeue());

            };
            #endregion




            var sw0 = Stopwatch.StartNew();
            var sw = Stopwatch.StartNew();
            //var time = Native.window.performance.now();
            //var lastTime = Native.window.performance.now();

            #region animate
            Action render =
                delegate
                {

                    var delta = sw.ElapsedMilliseconds;
                    sw.Restart();

                    //time = Native.window.performance.now();

                    camera.position.x = -Math.Cos(sw.ElapsedMilliseconds * 0.0001) * 40;
                    camera.position.z = Math.Sin(sw.ElapsedMilliseconds * 0.0001) * 40;
                    camera.lookAt(new THREE.Vector3(0, 10, 0));

                    intersectionPlane.lookAt(camera.position);

                    world.step(delta * 0.001);
                    //lastTime = time;

                    for (var i = 0; i < spheres.Count; i++)
                    {

                        var sphere = spheres.ElementAt(i);
                        var body = bodies.ElementAt(i);

                        sphere.position.copy(body.position);
                        sphere.quaternion.copy(body.quaternion);

                    }

                    renderer.render(scene, camera);

                };



            Native.window.onframe += delegate
            {


                if (isMouseDown)
                {

                    if (spheres.Count > 200)
                    {

                        removeBall();

                    }

                    addBall(
                        origin.x + (random() * 4.0 - 2),
                        origin.y + (random() * 4.0 - 2),
                        origin.z + (random() * 4.0 - 2)
                    );

                }

                render();

            };

            #endregion


        }

    }
}
