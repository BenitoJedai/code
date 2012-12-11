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

namespace WebGLCannonPhysicsEngine
{
    using WebGLCannonPhysicsEngine.Design.Cannon;
    using WebGLCannonPhysicsEngine.Design.THREE;
    using f = Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;


    sealed class MeshLambertMaterialArguments
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
        public Application(IApp page)
        {
            //var sphereShape, sphereBody, world, physicsMaterial, walls=[], balls=[], ballMeshes=[], boxes=[], boxMeshes=[];

            //var camera, scene, renderer;
            //var geometry, material, mesh;
            //var controls,time = Date.now();

            //var blocker = document.getElementById( 'blocker' );
            //var instructions = document.getElementById( 'instructions' );

            //var havePointerLock = 'pointerLockElement' in document || 'mozPointerLockElement' in document || 'webkitPointerLockElement' in document;

            //if ( havePointerLock ) {

            //    var element = document.body;

            //    var pointerlockchange = function ( event ) {

            //        if ( document.pointerLockElement === element || document.mozPointerLockElement === element || document.webkitPointerLockElement === element ) {

            //            controls.enabled = true;

            //            blocker.style.display = 'none';

            //        } else {

            //            controls.enabled = false;

            //            blocker.style.display = '-webkit-box';
            //            blocker.style.display = '-moz-box';
            //            blocker.style.display = 'box';

            //            instructions.style.display = '';

            //        }

            //    }

            //    var pointerlockerror = function ( event ) {
            //        instructions.style.display = '';
            //    }

            //    // Hook pointer lock state change events
            //    document.addEventListener( 'pointerlockchange', pointerlockchange, false );
            //    document.addEventListener( 'mozpointerlockchange', pointerlockchange, false );
            //    document.addEventListener( 'webkitpointerlockchange', pointerlockchange, false );

            //    document.addEventListener( 'pointerlockerror', pointerlockerror, false );
            //    document.addEventListener( 'mozpointerlockerror', pointerlockerror, false );
            //    document.addEventListener( 'webkitpointerlockerror', pointerlockerror, false );

            //    instructions.addEventListener( 'click', function ( event ) {
            //        instructions.style.display = 'none';

            //        // Ask the browser to lock the pointer
            //        element.requestPointerLock = element.requestPointerLock || element.mozRequestPointerLock || element.webkitRequestPointerLock;

            //        if ( /Firefox/i.test( navigator.userAgent ) ) {

            //            var fullscreenchange = function ( event ) {

            //                if ( document.fullscreenElement === element || document.mozFullscreenElement === element || document.mozFullScreenElement === element ) {

            //                    document.removeEventListener( 'fullscreenchange', fullscreenchange );
            //                    document.removeEventListener( 'mozfullscreenchange', fullscreenchange );

            //                    element.requestPointerLock();
            //                }

            //            }

            //            document.addEventListener( 'fullscreenchange', fullscreenchange, false );
            //            document.addEventListener( 'mozfullscreenchange', fullscreenchange, false );

            //            element.requestFullscreen = element.requestFullscreen || element.mozRequestFullscreen || element.mozRequestFullScreen || element.webkitRequestFullscreen;

            //            element.requestFullscreen();

            //        } else {

            //            element.requestPointerLock();

            //        }

            //    }, false );

            //} else {

            //    instructions.innerHTML = 'Your browser doesn\'t seem to support Pointer Lock API';

            //}

            //initCannon();
            //init();
            //animate();

            #region initCannon
            //    // Setup our world
            var world = new World();

            world.quatNormalizeSkip = 0;
            world.quatNormalizeFast = false;
            //    world.solver.setSpookParams(300,10);
            //    world.solver.iterations = 5;
            //    world.gravity.set(0,-20,0);
            world.broadphase = new NaiveBroadphase();

            //    // Create a slippery material (friction coefficient = 0.0)
            var physicsMaterial = new Material("slipperyMaterial");


            var physicsContactMaterial = new ContactMaterial(
                physicsMaterial,
                physicsMaterial,
                0.0, // friction coefficient
                0.3  // restitution
            );

            //    // We must add the contact materials to the world
            world.addContactMaterial(physicsContactMaterial);

            {    // Create a sphere
                var mass = 5;
                var radius = 1.3;
                var sphereShape = new Sphere(radius);
                var sphereBody = new RigidBody(mass, sphereShape, physicsMaterial);
                //    sphereBody.position.set(0,5,0);
                sphereBody.linearDamping = 0.05;
                world.add(sphereBody);

                //    // Create a plane
                var groundShape = new Plane();
                var groundBody = new RigidBody(0, groundShape, physicsMaterial);
                //    groundBody.quaternion.setFromAxisAngle(new CANNON.Vec3(1,0,0),-Math.PI/2);
                world.add(groundBody);
            }
            #endregion

            #region init

            var camera = new PerspectiveCamera(75, Native.Window.Width / Native.Window.Height, 0.1, 1000);

            var scene = new Scene();
            scene.fog = new Fog(0x000000, 0, 500);

            var ambient = new AmbientLight(0x111111);
            scene.add(ambient);

            var light = new SpotLight(0xffffff);
            //    light.position.set( 10, 30, 20 );
            //    light.target.position.set( 0, 0, 0 );
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



            //    controls = new PointerLockControls( camera , sphereBody );
            //    scene.add( controls.getObject() );

            //    // floor
            var geometry = new PlaneGeometry(300, 300, 50, 50);
            geometry.applyMatrix(new Matrix4().makeRotationX(-Math.PI / 2));

            var material = new MeshLambertMaterial(new MeshLambertMaterialArguments { color = 0xdddddd });
            //    THREE.ColorUtils.adjustHSV( material.color, 0, 0, 0.9 );

            var mesh = new Mesh(geometry, material);
            mesh.castShadow = true;
            mesh.receiveShadow = true;
            scene.add(mesh);

            var renderer = new WebGLRenderer();
            renderer.shadowMapEnabled = true;
            renderer.shadowMapSoft = true;
            renderer.setSize(Native.Window.Width, Native.Window.Height);
            //    renderer.setClearColor( scene.fog.color, 1 );

            renderer.domElement.AttachToDocument();

            //    window.addEventListener( 'resize', onWindowResize, false );

            {    // Add boxes
                var halfExtents = new Vec3(1, 1, 1);
                var boxShape = new Box(halfExtents);
                var boxGeometry = new CubeGeometry(halfExtents.x * 2, halfExtents.y * 2, halfExtents.z * 2);
                for (var i = 0; i < 7; i++)
                {
                    //        var x = (Math.random()-0.5)*20;
                    //        var y = 1 + (Math.random()-0.5)*1;
                    //        var z = (Math.random()-0.5)*20;
                    var boxBody = new RigidBody(5, boxShape);
                    var boxMesh = new Mesh(boxGeometry, material);
                    world.add(boxBody);
                    scene.add(boxMesh);
                    //        boxBody.position.set(x,y,z);
                    //        boxMesh.position.set(x,y,z);
                    boxMesh.castShadow = true;
                    boxMesh.receiveShadow = true;
                    boxMesh.useQuaternion = true;
                    //        boxes.push(boxBody);
                    //        boxMeshes.push(boxMesh);
                }
            }


            {    // Add linked boxes
                var size = 0.5;
                var he = new Vec3(size, size, size * 0.1);
                var boxShape = new Box(he);
                var mass = 0.0;
                var space = 0.1 * size;
                var N = 5;
                var last = default(RigidBody);

                var boxGeometry = new CubeGeometry(he.x * 2, he.y * 2, he.z * 2);

                for (var i = 0; i < N; i++)
                {
                    var boxbody = new RigidBody(mass, boxShape);
                    var boxMesh = new Mesh(boxGeometry, material);
                    //        boxbody.position.set(5,(N-i)*(size*2+2*space) + size*2+space,0);
                    boxbody.linearDamping = 0.01;
                    boxbody.angularDamping = 0.01;
                    boxMesh.useQuaternion = true;
                    boxMesh.castShadow = true;
                    boxMesh.receiveShadow = true;
                    //        world.add(boxbody);
                    //        scene.add(boxMesh);
                    //        boxes.push(boxbody);
                    //        boxMeshes.push(boxMesh);

                    if (i != 0)
                    {
                        // Connect this body to the last one
                        var c1 = new PointToPointConstraint(boxbody, new Vec3(-size, size + space, 0), last, new Vec3(-size, -size - space, 0));
                        var c2 = new PointToPointConstraint(boxbody, new Vec3(size, size + space, 0), last, new Vec3(size, -size - space, 0));
                        
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


            //function onWindowResize() {
            //    camera.aspect = window.innerWidth / window.innerHeight;
            //    camera.updateProjectionMatrix();
            //    renderer.setSize( window.innerWidth, window.innerHeight );
            //}

            #region animate
            //var dt = 1/60;
            //function animate() {
            //    requestAnimationFrame( animate );
            //    if(controls.enabled){
            //        world.step(dt);

            //        // Update ball positions
            //        for(var i=0; i<balls.length; i++){
            //            balls[i].position.copy(ballMeshes[i].position);
            //            balls[i].quaternion.copy(ballMeshes[i].quaternion);
            //        }

            //        // Update box positions
            //        for(var i=0; i<boxes.length; i++){
            //            boxes[i].position.copy(boxMeshes[i].position);
            //            boxes[i].quaternion.copy(boxMeshes[i].quaternion);
            //        }
            //    }

            //    controls.update( Date.now() - time );
            //    renderer.render( scene, camera );
            //    time = Date.now();

            //}
            #endregion

            //var ballShape = new CANNON.Sphere(0.2);
            //var ballGeometry = new THREE.SphereGeometry(ballShape.radius);
            //var shootDirection = new THREE.Vector3();
            //var shootVelo = 15;
            //var projector = new THREE.Projector();
            //function getShootDir(targetVec){
            //    var vector = targetVec;
            //    targetVec.set(0,0,1);
            //    projector.unprojectVector(vector, camera);
            //    var ray = new THREE.Ray(sphereBody.position, vector.subSelf(sphereBody.position).normalize() );
            //    targetVec.x = ray.direction.x;
            //    targetVec.y = ray.direction.y;
            //    targetVec.z = ray.direction.z;
            //}

            //window.addEventListener("click",function(e){ 
            //    if(controls.enabled==true){
            //        var x = sphereBody.position.x;
            //        var y = sphereBody.position.y;
            //        var z = sphereBody.position.z;
            //        var ballBody = new CANNON.RigidBody(1,ballShape);
            //        var ballMesh = new THREE.Mesh( ballGeometry, material );
            //        world.add(ballBody);
            //        scene.add(ballMesh);
            //        ballMesh.castShadow = true;
            //        ballMesh.receiveShadow = true;
            //        balls.push(ballBody);
            //        ballMeshes.push(ballMesh);
            //        getShootDir(shootDirection);
            //        ballBody.velocity.set(  shootDirection.x * shootVelo,
            //                                shootDirection.y * shootVelo,
            //                                shootDirection.z * shootVelo);

            //        // Move the ball outside the player sphere
            //        x += shootDirection.x * (sphereShape.radius + ballShape.radius);
            //        y += shootDirection.y * (sphereShape.radius + ballShape.radius);
            //        z += shootDirection.z * (sphereShape.radius + ballShape.radius);
            //        ballBody.position.set(x,y,z);
            //        ballMesh.position.set(x,y,z);
            //        ballMesh.useQuaternion = true;
            //    }
            //});

        }

    }
}
