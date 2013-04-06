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
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            #region await Three.js then do InitializeContent
            new[]
            {
                new CANNONLibrary.opensource.github.cannon.js.build.cannon().Content,
                new THREELibrary.opensource.gihtub.three.js.build.three().Content,
                //new global::WebGLCannonPhysicsEngine.Design.References.PointerLockControls().Content,
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
            //var spheres = [], bodies = [];

            //var world;
            //var time, lastTime = performance.now();

            //var ballGeometry, ballMaterial, ballBodyMaterial;

            //var intersectionPlane,
                var origin = new THREE.Vector3( 0, 15, 0 );
			var isMouseDown = false;


#region init

				var renderer = new THREE.WebGLRenderer(new __WebGLRendererDictionary { antialias = true, alpha = false } );
				renderer.setClearColor( new THREE.Color( 0x101010 ) );
                //renderer.setSize( window.innerWidth, window.innerHeight );
                //document.body.appendChild( renderer.domElement );

				// scene

				var camera = new THREE.PerspectiveCamera( 40, Native.Window.Width / Native.Window.Height, 1, 1000 );
				camera.position.x = - 30;
				camera.position.y = 10;
				camera.position.z = 30;
				camera.lookAt( new THREE.Vector3( 0, 10, 0 ) );

				var scene = new THREE.Scene();

				var light = new THREE.HemisphereLight( 0xffffff, 0x606060, 1.2 );
				light.position.set( -10, 10, 10 );
				scene.add( light );

            {
				var geometry = new THREE.CubeGeometry( 20, 20, 20 );
				var material = new THREE.MeshBasicMaterial( new __MeshBasicMaterialDictionary { wireframe = true, opacity = 0.1, transparent =  true } );
				var mesh = new THREE.Mesh( geometry, material );
				mesh.position.y = 10;
				scene.add( mesh );
        }

            {
				var geometry = new THREE.PlaneGeometry( 20, 20, 8, 8 );
				var intersectionPlane = new THREE.Mesh( geometry );
				intersectionPlane.position.y = 10;
				intersectionPlane.visible = false;
				scene.add( intersectionPlane );
        }

				// geometry

				var ballGeometry = new THREE.Geometry();

				var ballMaterial = new THREE.MeshPhongMaterial(
            
                    new __MeshPhongMaterialDictionary{
					    vertexColors = THREE.FaceColors,
					    specular = 0x808080,
					    shininess = 2000
				    } 
                 );

				//

				var colors = new [] {
					new THREE.Color( 0xe52b30 ),
					new THREE.Color( 0xe52b30 ),
					new THREE.Color( 0x2e1b6a ),
					new THREE.Color( 0xdac461 ),
					new THREE.Color( 0xf07017 ),
					new THREE.Color( 0x38b394 ),
					new THREE.Color( 0xeaf1f7 )
				};

				var amount = colors.Length;

				var geometryTop = new THREE.SphereGeometry( 1, 5 * amount, 2, 0, Math.PI * 2, 0, 0.30 );

				for ( var j = 0, jl = geometryTop.faces.length; j < jl; j ++ ) {

					geometryTop.faces[ j ].color = colors[ 0 ]

				}

				THREE.GeometryUtils.merge( ballGeometry, geometryTop );

				var geometryBottom = new THREE.SphereGeometry( 1, 5 * amount, 2, 0, Math.PI * 2, Math.PI - 0.30, 0.30 );

				for ( var j = 0, jl = geometryBottom.faces.length; j < jl; j ++ ) {

					geometryBottom.faces[ j ].color = colors[ 0 ]

				}

				THREE.GeometryUtils.merge( ballGeometry, geometryBottom );

				var sides = amount - 1;
				var size = ( Math.PI * 2 ) / sides;

				for ( var i = 0; i < sides; i ++ ) {

					var patch = new THREE.SphereGeometry( 1, 5, 10, i * size, size, 0.30, Math.PI - 0.60 );

					for ( var j = 0, jl = patch.faces.length; j < jl; j ++ ) {

						patch.faces[ j ].color = colors[ i + 1 ];

					}

					THREE.GeometryUtils.merge( ballGeometry, patch );

				}

				// physics

				var world = new CANNON.World();
				world.broadphase = new CANNON.NaiveBroadphase();
				world.gravity.set( 0, - 15, 0 );
				world.solver.iterations = 7;
				world.solver.tolerance = 0.1;

				var groundShape = new CANNON.Plane();
				var groundMaterial = new CANNON.Material();
				var groundBody = new CANNON.RigidBody( 0, groundShape, groundMaterial);
				groundBody.quaternion.setFromAxisAngle( new CANNON.Vec3( 1, 0, 0 ), - Math.PI / 2 );
				world.add( groundBody );

				var planeShapeXmin = new CANNON.Plane();
				var planeXmin = new CANNON.RigidBody( 0, planeShapeXmin, groundMaterial );
				planeXmin.quaternion.setFromAxisAngle( new CANNON.Vec3( 0, 1, 0 ), Math.PI / 2 );
				planeXmin.position.set( - 10, 0, 0 );
				world.add( planeXmin );

				var planeShapeXmax = new CANNON.Plane();
				var planeXmax = new CANNON.RigidBody( 0, planeShapeXmax, groundMaterial );
				planeXmax.quaternion.setFromAxisAngle( new CANNON.Vec3( 0, 1, 0 ), - Math.PI / 2 );
				planeXmax.position.set( 10, 0, 0 );
				world.add( planeXmax );

				var planeShapeYmin = new CANNON.Plane();
				var planeZmin = new CANNON.RigidBody( 0, planeShapeYmin, groundMaterial );
				planeZmin.position.set( 0, 0, - 10 );
				world.add( planeZmin );

				var planeShapeYmax = new CANNON.Plane();
				var planeZmax = new CANNON.RigidBody( 0, planeShapeYmax, groundMaterial );
				planeZmax.quaternion.setFromAxisAngle( new CANNON.Vec3( 0, 1, 0 ),Math.PI );
				planeZmax.position.set( 0, 0, 10 );
				world.add( planeZmax );

				var ballBodyMaterial = new CANNON.Material();
				world.addContactMaterial( new CANNON.ContactMaterial( groundMaterial, ballBodyMaterial, 0.2, 0.5 ) );
				world.addContactMaterial( new CANNON.ContactMaterial( ballBodyMaterial, ballBodyMaterial, 0.2, 0.8 ) );

				for ( var i = 0; i < 100; i ++ ) {

					addBall(
						Math.random() * 10 - 5,
						Math.random() * 20,
						Math.random() * 10 - 5
					);

				}

				//

				var projector = new THREE.Projector();
				var ray = new THREE.Raycaster();
				var mouse3D = new THREE.Vector3();

				document.body.style.cursor = 'pointer';
				document.addEventListener( 'mousedown', function ( event ) {

					event.preventDefault();
					isMouseDown = true;

				}, false );
				document.addEventListener( 'mousemove', function ( event ) {

					mouse3D.set(
						( event.clientX / window.innerWidth ) * 2 - 1,
						- ( event.clientY / window.innerHeight ) * 2 + 1,
						0.5
					);

					projector.unprojectVector( mouse3D, camera );

					ray.set( camera.position, mouse3D.sub( camera.position ).normalize() );

					var intersects = ray.intersectObject( intersectionPlane );

					if ( intersects.length > 0 ) {

						origin.copy( intersects[ 0 ].point );

					}

				}, false );

				document.addEventListener( 'mouseup', function ( event ) {

					isMouseDown = false;

				}, false );

				// firefox

				document.addEventListener( 'visibilitychange', function ( event ) {

					if ( document.hidden === false ) {

						lastTime = performance.now();

					}

				}, false );

				// webkit

				document.addEventListener( 'webkitvisibilitychange', function ( event ) {

					if ( document.webkitHidden === false ) {

						lastTime = performance.now();

					}

				}, false );

				window.addEventListener( 'resize', onWindowResized, false );

				onWindowResized( null );

#endregion


			function addBall( x, y, z ) {

				x = Math.max( -10, Math.min( 10, x ) );
				y = Math.max( 5, y );
				z = Math.max( -10, Math.min( 10, z ) );

				var size = 1.25;

				var sphere = new THREE.Mesh( ballGeometry, ballMaterial );
				sphere.scale.multiplyScalar( size );
				sphere.useQuaternion = true;
				scene.add( sphere );

				spheres.push( sphere );

				var sphereShape = new CANNON.Sphere( size );
				var sphereBody = new CANNON.RigidBody( 0.1, sphereShape, ballBodyMaterial );
				sphereBody.position.set( x, y, z );
				sphereBody.quaternion.set( Math.random() * 3, Math.random() * 3, Math.random() * 3, Math.random() * 3 );
				world.add( sphereBody );

				bodies.push( sphereBody );

			}

			function removeBall() {

				scene.remove( spheres.shift() );
				world.remove( bodies.shift() );

			}

			function onWindowResized( event ) {

				camera.aspect = window.innerWidth / window.innerHeight;
				camera.updateProjectionMatrix();

				renderer.setSize( window.innerWidth, window.innerHeight );

			}

		

#region animate
			function render() {

				time = performance.now();

				camera.position.x = - Math.cos( time * 0.0001 ) * 40;
				camera.position.z = Math.sin( time * 0.0001 ) * 40;
				camera.lookAt( new THREE.Vector3( 0, 10, 0 ) );

				intersectionPlane.lookAt( camera.position );

				world.step( ( time - lastTime ) * 0.001 );
				lastTime = time;

				for ( var i = 0, l = spheres.length; i < l; i ++ ) {

					var sphere = spheres[ i ];
					var body = bodies[ i ];

					sphere.position.copy( body.position );
					sphere.quaternion.copy( body.quaternion );

				}

				renderer.render( scene, camera );

			}

	        function animate() {

				requestAnimationFrame( animate );

				if ( isMouseDown ) {

					if ( spheres.length > 200 ) {

						removeBall();

					}

					addBall(
						origin.x + ( Math.random() * 4 - 2 ),
						origin.y + ( Math.random() * 4 - 2 ),
						origin.z + ( Math.random() * 4 - 2 )
					);

				}

				render();

			}
#endregion

			animate();

        }

    }
}
