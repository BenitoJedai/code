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
using CANNON.Design;
using THREE.Design;

namespace WebGLCannonPhysicsEngine
{
    using f = Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;


    sealed class MeshLambertMaterialDictionary
    {
        public int color;
        public bool morphTargets;
        public int vertexColors;
    }


    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // inspired by http://granular.cs.umu.se/cannon.js/examples/threejs_fps.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page = null)
        {
            //            DEPRECATED: Quaternion's .multiplyVector3() has been removed. Use is now vector.applyQuaternion( quaternion ) instead. Three.js:913
            //Uncaught TypeError: Object [object Object] has no method 'subSelf' 

            #region await Three.js then do InitializeContent
            new[]
            {
                new CANNON.opensource.github.cannon.js.build.cannon().Content,
                new THREE.opensource.gihtub.three.js.build.three().Content,
                new global::WebGLCannonPhysicsEngine.Design.References.PointerLockControls().Content,
            }.ForEach(
                (SourceScriptElement, i, MoveNext) =>
                {
                    SourceScriptElement.AttachToDocument().onload +=
                        delegate
                        {
                            MoveNext();
                        };
                }
            )(
                delegate
                {
                    InitializeContent();
                }
            );
            #endregion



        }

        private static void InitializeContent()
        {
            var boxes = new List<CANNON_RigidBody>();
            var boxMeshes = new List<THREE_Mesh>();

            var balls = new List<CANNON_RigidBody>();
            var ballMeshes = new List<THREE_Mesh>();



            Func<long> Date_now = () => (long)new IFunction("return Date.now();").apply(null);

            var time = Date_now();


            #region havePointerLock

            Native.Document.body.onclick +=
                delegate
                {
                    Native.Document.body.requestPointerLock();
                };


            #endregion




            #region initCannon
            //    // Setup our world
            var world = new CANNON_World();

            world.quatNormalizeSkip = 0;
            world.quatNormalizeFast = false;
            world.solver.setSpookParams(300, 10);
            world.solver.iterations = 5;
            world.gravity.set(0, -20, 0);
            world.broadphase = new CANNON_NaiveBroadphase();

            //    // Create a slippery material (friction coefficient = 0.0)
            var physicsMaterial = new CANNON_Material("slipperyMaterial");


            var physicsContactMaterial = new CANNON_ContactMaterial(
                physicsMaterial,
                physicsMaterial,
                0.0, // friction coefficient
                0.3  // restitution
            );

            //    // We must add the contact materials to the world
            world.addContactMaterial(physicsContactMaterial);

            var controls_sphereShape = default(CANNON_Sphere);
            var controls_sphereBody = default(CANNON_RigidBody);

            {    // Create a sphere
                var mass = 5;
                var radius = 1.3;
                var sphereShape = new CANNON_Sphere(radius);
                var sphereBody = new CANNON_RigidBody(mass, sphereShape, physicsMaterial);
                controls_sphereShape = sphereShape;
                controls_sphereBody = sphereBody;
                sphereBody.position.set(0, 5, 0);
                sphereBody.linearDamping = 0.05;
                world.add(sphereBody);

                //    // Create a plane
                var groundShape = new CANNON_Plane();
                var groundBody = new CANNON_RigidBody(0, groundShape, physicsMaterial);
                groundBody.quaternion.setFromAxisAngle(new CANNON_Vec3(1, 0, 0), -Math.PI / 2);
                world.add(groundBody);
            }
            #endregion

            #region init

            var camera = new THREE_PerspectiveCamera(75, Native.Window.Width / Native.Window.Height, 0.1, 1000);

            var scene = new THREE_Scene();
            scene.fog = new THREE_Fog(0x000000, 0, 500);

            var ambient = new THREE_AmbientLight(0x111111);
            scene.add(ambient);

            var light = new THREE_SpotLight(0xffffff);
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
            var geometry = new THREE_PlaneGeometry(300, 300, 50, 50);
            geometry.applyMatrix(new THREE_Matrix4().makeRotationX(-Math.PI / 2));

            var material = new THREE_MeshLambertMaterial(new MeshLambertMaterialDictionary { color = 0xdddddd });

            //Native.Window.
            THREE.Design.THREE.ColorUtils.adjustHSV(material.color, 0, 0, 0.9);
            //new IFunction("material", "THREE.ColorUtils.adjustHSV( material.color, 0, 0, 0.9 );").apply(null, material);

            //    

            var mesh = new THREE_Mesh(geometry, material);
            mesh.castShadow = true;
            mesh.receiveShadow = true;
            scene.add(mesh);

            var renderer = new THREE_WebGLRenderer(new object());
            renderer.shadowMapEnabled = true;
            renderer.shadowMapSoft = true;
            //renderer.setSize(Native.Window.Width, Native.Window.Height);
            renderer.setClearColor(scene.fog.color, 1);

            renderer.domElement.AttachToDocument();

            Action AtResize = delegate
            {
                camera.aspect = Native.Window.Width / Native.Window.Height;
                camera.updateProjectionMatrix();
                renderer.setSize(Native.Window.Width, Native.Window.Height);
            };
            Native.Window.onresize +=
              delegate
              {
                  AtResize();
              };

            AtResize();


            var r = new Random();
            Func<f> Math_random = () => r.NextFloat();

            #region Add boxes
            {    // 

                for (var i = 0; i < 32; i++)
                {
                    var boxsize = Math_random() * 0.5;

                    var halfExtents = new CANNON_Vec3(boxsize, boxsize, boxsize);

                    var boxShape = new CANNON_Box(halfExtents);
                    var boxGeometry = new THREE_CubeGeometry(halfExtents.x * 2, halfExtents.y * 2, halfExtents.z * 2);

                    var x = (Math_random() - 0.5) * 20;
                    var y = 1 + (Math_random() - 0.5) * 1;
                    var z = (Math_random() - 0.5) * 20;
                    var boxBody = new CANNON_RigidBody(5, boxShape);
                    var boxMesh = new THREE_Mesh(boxGeometry, material);
                    world.add(boxBody);
                    scene.add(boxMesh);
                    boxBody.position.set(x, y, z);
                    boxMesh.position.set(x, y, z);
                    boxMesh.castShadow = true;
                    boxMesh.receiveShadow = true;
                    boxMesh.useQuaternion = true;

                    boxes.Add(boxBody);
                    boxMeshes.Add(boxMesh);
                }
            }
            #endregion

            #region Add linked boxes
            {    // 
                var size = 0.5;
                var he = new CANNON_Vec3(size, size, size * 0.1);
                var boxShape = new CANNON_Box(he);
                var mass = 0.0;
                var space = 0.1 * size;
                var N = 5;
                var last = default(CANNON_RigidBody);

                var boxGeometry = new THREE_CubeGeometry(he.x * 2, he.y * 2, he.z * 2);

                for (var i = 0; i < N; i++)
                {
                    var boxbody = new CANNON_RigidBody(mass, boxShape);
                    var boxMesh = new THREE_Mesh(boxGeometry, material);
                    boxbody.position.set(5, (N - i) * (size * 2 + 2 * space) + size * 2 + space, 0);
                    boxbody.linearDamping = 0.01;
                    boxbody.angularDamping = 0.01;
                    boxMesh.useQuaternion = true;
                    boxMesh.castShadow = true;
                    boxMesh.receiveShadow = true;

                    world.add(boxbody);
                    scene.add(boxMesh);

                    boxes.Add(boxbody);
                    boxMeshes.Add(boxMesh);

                    if (i != 0)
                    {
                        // Connect this body to the last one
                        var c1 = new CANNON_PointToPointConstraint(boxbody, new CANNON_Vec3(-size, size + space, 0), last, new CANNON_Vec3(-size, -size - space, 0));
                        var c2 = new CANNON_PointToPointConstraint(boxbody, new CANNON_Vec3(size, size + space, 0), last, new CANNON_Vec3(size, -size - space, 0));

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


            #region animate
            var dt = 1.0 / 60;
            Action animate = null;
            controls.enabled = true;

            animate = delegate
            {

                if (controls.enabled)
                {
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

                Native.Window.requestAnimationFrame += animate;

            };

            animate();

            #endregion




            Native.Document.onmousedown +=
                e =>
                {
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

                    var ballradius = 0.1 + Math_random() * 0.9;

                    var ballShape = new CANNON_Sphere(ballradius);
                    var ballGeometry = new THREE_SphereGeometry(ballShape.radius);
                    var shootDirection = new THREE_Vector3();
                    var shootVelo = 15;
                    var projector = new THREE_Projector();

                    Action<THREE_Vector3> getShootDir = (targetVec) =>
                    {
                        var vector = targetVec;
                        targetVec.set(0, 0, 1);
                        projector.unprojectVector(vector, camera);
                        var ray = new THREE_Ray(controls_sphereBody.position,
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

                    var ballBody = new CANNON_RigidBody(1, ballShape);
                    var ballMesh = new THREE_Mesh(ballGeometry, material);
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
                    ballMesh.useQuaternion = true;
                };
        }

    }
}
