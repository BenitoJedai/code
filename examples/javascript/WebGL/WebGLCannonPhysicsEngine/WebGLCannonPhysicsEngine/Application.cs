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
using WebGLCannonPhysicsEngine.Design;
using WebGLCannonPhysicsEngine.HTML.Pages;
using System.Collections.Generic;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Runtime;

using THREE;

namespace WebGLCannonPhysicsEngine
{
    using WebGLRah66Comanche.Library;
    using f = Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using Math = System.Math;

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // inspired by http://granular.cs.umu.se/cannon.js/examples/threejs_fps.html


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page = null)
        {
            //Uncaught Error: ERROR: Quaternion's .setFromEuler() now expects a Euler rotation rather than a Vector3 and order.  Please update your code. 

            // WEBGL11095: INVALID_OPERATION: clearStencil: Method not currently supported
            // IE11 does not work yet

            //DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();

            //            DEPRECATED: Quaternion's .multiplyVector3() has been removed. Use is now vector.applyQuaternion( quaternion ) instead. Three.js:913
            //Uncaught TypeError: Object [object Object] has no method 'subSelf' 
            // { REVISION: '57' };

            var boxes = new List<CANNON.RigidBody>();
            var boxMeshes = new List<THREE.Mesh>();

            var balls = new List<CANNON.RigidBody>();
            var ballMeshes = new List<THREE.Mesh>();



            Func<long> Date_now = () => (long)new IFunction("return Date.now();").apply(null);

            var time = Date_now();







            #region initCannon
            //    // Setup our world
            var world = new CANNON.World();

            world.quatNormalizeSkip = 0;
            world.quatNormalizeFast = false;
            //world.solver.setSpookParams(300, 10);
            world.solver.iterations = 5;
            world.gravity.set(0, -20, 0);
            world.broadphase = new CANNON.NaiveBroadphase();

            //    // Create a slippery material (friction coefficient = 0.0)
            var physicsMaterial = new CANNON.Material("slipperyMaterial");


            var physicsContactMaterial = new CANNON.ContactMaterial(
                physicsMaterial,
                physicsMaterial,
                0.0, // friction coefficient
                0.3  // restitution
            );

            //    // We must add the contact materials to the world
            world.addContactMaterial(physicsContactMaterial);

            var controls_sphereShape = default(CANNON.Sphere);
            var controls_sphereBody = default(CANNON.RigidBody);

            {    // Create a sphere
                var mass = 5;
                var radius = 1.3;
                var sphereShape = new CANNON.Sphere(radius);
                var sphereBody = new CANNON.RigidBody(mass, sphereShape, physicsMaterial);
                controls_sphereShape = sphereShape;
                controls_sphereBody = sphereBody;
                sphereBody.position.set(0, 5, 0);
                sphereBody.linearDamping = 0.05;
                world.add(sphereBody);

                //    // Create a plane
                var groundShape = new CANNON.Plane();
                var groundBody = new CANNON.RigidBody(0, groundShape, physicsMaterial);
                groundBody.quaternion.setFromAxisAngle(new CANNON.Vec3(1, 0, 0), -Math.PI / 2);
                world.add(groundBody);
            }
            #endregion

            #region init

            var camera = new THREE.PerspectiveCamera(75, Native.window.aspect, 0.1, 1000);

            var scene = new Scene();
            scene.fog = new Fog(0x000000, 0, 500);

            var ambient = new AmbientLight(0x111111);
            scene.add(ambient);

            var light = new SpotLight(0xffffff);
            light.position.set(10, 30, 20);
            light.target.position.set(0, 0, 0);
            //    if(true){
            light.castShadow = true;

            light.shadowCameraNear = 20;
            light.shadowCameraFar = 50;//camera.far;
            light.shadowCameraFov = 40;

            light.shadowMapBias = 0.1;
            light.shadowMapDarkness = 0.7;
            light.shadowMapWidth = 2 * 512;
            light.shadowMapHeight = 2 * 512;

            //        //light.shadowCameraVisible = true;
            //    }
            scene.add(light);



            var controls = new PointerLockControls(camera, controls_sphereBody);
            scene.add(controls.getObject());

            //    // floor
            var geometry = new THREE.PlaneGeometry(300, 300, 50, 50);
            geometry.applyMatrix(new THREE.Matrix4().makeRotationX(-Math.PI / 2));

            var material = new THREE.MeshLambertMaterial(new { color = 0xdddddd });

            //Native.Window.




            // THREE.Design.THREE.ColorUtils.adjustHSV(material.color, 0, 0, 0.9);

            //  Replaced ColorUtils.adjustHSV() with Color's .offsetHSL(). 
            //new IFunction("material", "THREE.ColorUtils.offsetHSL( material.color, 0, 0, 0.9 );").apply(null, material);

            //    

            var mesh = new Mesh(geometry, material)
            {
                castShadow = true,
                receiveShadow = true
            };

            scene.add(mesh);

            var renderer = new THREE.WebGLRenderer(new object());
            renderer.shadowMapEnabled = true;
            renderer.shadowMapSoft = true;
            //renderer.setSize(Native.Window.Width, Native.Window.Height);
            //renderer.setClearColor(scene.fog.color, 1);

            renderer.domElement.style.backgroundColor = JSColor.Black;
            renderer.domElement.AttachToDocument();



            #region onresize
            Action AtResize = delegate
            {
                camera.aspect = Native.window.aspect;
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


            var r = new Random();
            Func<f> Math_random = () => r.NextFloat();

            #region Add boxes
            {    // 

                for (var i = 0; i < 32; i++)
                {
                    var boxsize = Math_random() * 0.5;

                    var halfExtents = new CANNON.Vec3(boxsize, boxsize, boxsize);

                    var boxShape = new CANNON.Box(halfExtents);
                    var boxGeometry = new THREE.CubeGeometry(halfExtents.x * 2, halfExtents.y * 2, halfExtents.z * 2);

                    var x = (Math_random() - 0.5) * 20;
                    var y = 1 + (Math_random() - 0.5) * 1;
                    var z = (Math_random() - 0.5) * 20;
                    var boxBody = new CANNON.RigidBody(5, boxShape);
                    var boxMesh = new THREE.Mesh(boxGeometry, material);
                    world.add(boxBody);
                    scene.add(boxMesh);
                    boxBody.position.set(x, y, z);
                    boxMesh.position.set(x, y, z);
                    boxMesh.castShadow = true;
                    boxMesh.receiveShadow = true;
                    //boxMesh.useQuaternion = true;

                    boxes.Add(boxBody);
                    boxMeshes.Add(boxMesh);
                }
            }
            #endregion

            #region Add linked boxes
            {    // 
                var size = 0.5;
                var he = new CANNON.Vec3(size, size, size * 0.1);
                var boxShape = new CANNON.Box(he);
                var mass = 0.0;
                var space = 0.1 * size;
                var N = 5;
                var last = default(CANNON.RigidBody);

                var boxGeometry = new THREE.CubeGeometry(he.x * 2, he.y * 2, he.z * 2);

                for (var i = 0; i < N; i++)
                {
                    var boxbody = new CANNON.RigidBody(mass, boxShape);
                    var boxMesh = new THREE.Mesh(boxGeometry, material);
                    boxbody.position.set(5, (N - i) * (size * 2 + 2 * space) + size * 2 + space, 0);
                    boxbody.linearDamping = 0.01;
                    boxbody.angularDamping = 0.01;
                    //boxMesh.useQuaternion = true;
                    boxMesh.castShadow = true;
                    boxMesh.receiveShadow = true;

                    world.add(boxbody);
                    scene.add(boxMesh);

                    boxes.Add(boxbody);
                    boxMeshes.Add(boxMesh);

                    if (i != 0)
                    {
                        // Connect this body to the last one
                        var c1 = new CANNON.PointToPointConstraint(boxbody, new CANNON.Vec3(-size, size + space, 0), last, new CANNON.Vec3(-size, -size - space, 0));
                        var c2 = new CANNON.PointToPointConstraint(boxbody, new CANNON.Vec3(size, size + space, 0), last, new CANNON.Vec3(size, -size - space, 0));

                        world.addConstraint(c1);
                        world.addConstraint(c2);
                    }
                    else
                    {
                        mass = 0.3;
                    }
                    last = boxbody;
                }
            }
            #endregion

            #endregion


            var dt = 1.0 / 60;
            controls.enabled = true;

            // vr and tilt shift?

            Native.window.onframe += delegate
            {

                if (controls.enabled)
                {
                    // how big of a world can we hold?
                    // async ?
                    world.step(dt);

                    // Update ball positions
                    for (var i = 0; i < balls.Count; i++)
                    {
                        balls[i].position.copy(ballMeshes[i].position);
                        balls[i].quaternion.copy(ballMeshes[i].quaternion);
                    }

                    // Update box positions
                    for (var i = 0; i < boxes.Count; i++)
                    {
                        boxes[i].position.copy(boxMeshes[i].position);
                        boxes[i].quaternion.copy(boxMeshes[i].quaternion);
                    }
                }

                controls.update(Date_now() - time);
                renderer.render(scene, camera);
                time = Date_now();


            };



            #region havePointerLock

            renderer.domElement.onclick +=
                delegate
                {
                    renderer.domElement.requestPointerLock();
                };


            #endregion



            #region onmousedown
            renderer.domElement.onmousedown +=
                e =>
                {
                    if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        if (Native.document.pointerLockElement == Native.document.body)
                        {
                            // cant requestFullscreen while pointerLockElement
                            Console.WriteLine("exitPointerLock");
                            Native.document.exitPointerLock();
                            Native.document.exitFullscreen();
                            return;
                        }

                        Console.WriteLine("requestFullscreen");
                        renderer.domElement.requestFullscreen();
                        renderer.domElement.requestPointerLock();
                        return;
                    }

                    var ballradius = 0.1 + Math_random() * 0.9;

                    var ballShape = new CANNON.Sphere(ballradius);
                    var ballGeometry = new THREE.SphereGeometry(ballShape.radius);
                    var shootDirection = new THREE.Vector3();
                    var shootVelo = 15;
                    var projector = new THREE.Projector();

                    Action<THREE.Vector3> getShootDir = (targetVec) =>
                    {
                        var vector = targetVec;
                        targetVec.set(0, 0, 1);
                        projector.unprojectVector(vector, camera);
                        var ray = new THREE.Ray(controls_sphereBody.position,
                            vector
                            //.subSelf(controls_sphereBody.position)
                            .normalize()

                            );
                        targetVec.x = ray.direction.x;
                        targetVec.y = ray.direction.y;
                        targetVec.z = ray.direction.z;
                    };


                    var x = controls_sphereBody.position.x;
                    var y = controls_sphereBody.position.y;
                    var z = controls_sphereBody.position.z;

                    // could we attach physics via binding list?
                    var ballBody = new CANNON.RigidBody(1, ballShape);
                    var ballMesh = new THREE.Mesh(ballGeometry, material);
                    world.add(ballBody);
                    scene.add(ballMesh);
                    ballMesh.castShadow = true;
                    ballMesh.receiveShadow = true;
                    balls.Add(ballBody);
                    ballMeshes.Add(ballMesh);
                    getShootDir(shootDirection);
                    ballBody.velocity.set(shootDirection.x * shootVelo,
                                            shootDirection.y * shootVelo,
                                            shootDirection.z * shootVelo);

                    //        // Move the ball outside the player sphere
                    x += shootDirection.x * (controls_sphereShape.radius + ballShape.radius);
                    y += shootDirection.y * (controls_sphereShape.radius + ballShape.radius);
                    z += shootDirection.z * (controls_sphereShape.radius + ballShape.radius);
                    ballBody.position.set(x, y, z);
                    ballMesh.position.set(x, y, z);
                    //ballMesh.useQuaternion = true;
                };
            #endregion



            var ze = new ZeProperties();

            ze.Show();

            ze.Left = 0;

            ze.Add(() => renderer);
            ze.Add(() => controls);
            ze.Add(() => scene);
        }

    }
}
