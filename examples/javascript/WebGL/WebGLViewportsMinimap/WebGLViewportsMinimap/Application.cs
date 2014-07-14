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
using WebGLViewportsMinimap;
using WebGLViewportsMinimap.Design;
using WebGLViewportsMinimap.HTML.Pages;
using ScriptCoreLib.Lambda;
using WebGLViewportsMinimap.HTML.Images.FromAssets;


namespace WebGLViewportsMinimap
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public IHTMLCanvas container;
        public THREE.Scene scene;
        public THREE.Object3D group;
        public THREE.WebGLRenderer renderer;
        public THREE.PerspectiveCamera perspectiveCamera;
        public THREE.OrthographicCamera mapCamera;
        public int mapWidth = 240, mapHeight = 160;



        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

           // SCENE
	         scene = new THREE.Scene();
	        // CAMERA
	        var SCREEN_WIDTH = Native.window.Width;
            var SCREEN_HEIGHT = Native.window.Height;
	        var VIEW_ANGLE = 45;
            var ASPECT = SCREEN_WIDTH / SCREEN_HEIGHT;
            var NEAR = 0.1;
            var FAR = 20000;

	        // perspective cameras
	        perspectiveCamera = new THREE.PerspectiveCamera(VIEW_ANGLE, ASPECT, NEAR, FAR);
	        perspectiveCamera.position.set(0,200,550);
	        perspectiveCamera.lookAt(scene.position);
	        scene.add(perspectiveCamera);

	        // orthographic cameras
	        mapCamera = new THREE.OrthographicCamera(
            Native.window.Width / -2,		// Left
            Native.window.Width / 2,		// Right
            Native.window.Height / 2,		// Top
            Native.window.Height / -2,	// Bottom
            -5000,            			// Near 
            10000 );           			// Far 
	        mapCamera.up = new THREE.Vector3(0,0,-1);
	        mapCamera.lookAt( new THREE.Vector3(0,-1,0) );
	        scene.add(mapCamera);

            renderer = new THREE.WebGLRenderer(
                   new
                   {
                       antialias = true
                   }
                );
            

	        // RENDERER
	        renderer.setSize(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.container = (IHTMLCanvas)renderer.domElement;
            this.container.AttachToDocument();
            this.container.style.SetLocation(0, 0);
	
	        // EVENTS
            //THREEx.WindowResize(renderer, mapCamera);
            //THREEx.FullScreen.bindKey({ charCode : 'm'.charCodeAt(0) });

	        // STATS
            //stats = new Stats();
            //stats.domElement.style.position = 'absolute';
            //stats.domElement.style.bottom = '0px';
            //stats.domElement.style.zIndex = 100;
            //container.appendChild( stats.domElement );
	        // LIGHT
	        var light = new THREE.PointLight(0xffffff);
	        light.position.set(0,250,0);
	        scene.add(light);
	        // FLOOR
            var img = new uvgrid01();
            var floorTexture = new THREE.Texture().With(
                                async s =>
                                {
                                    s.image = await img;
                                    s.needsUpdate = true;
                                });
            floorTexture.wrapS = floorTexture.wrapT = THREE.RepeatWrapping; 
            floorTexture.repeat.set( 10, 10 );
	        var floorMaterial = new THREE.MeshBasicMaterial( new { map = floorTexture, side = THREE.DoubleSide } );
	        var floorGeometry = new THREE.PlaneGeometry(2000, 2000, 10, 10);
	        var floor = new THREE.Mesh(floorGeometry, floorMaterial);
	        floor.position.y = -0.5;
	        floor.rotation.x = Math.PI / 2;
	        scene.add(floor);
	
	        ////////////
	        // CUSTOM //
	        ////////////
	

    //        materialArray.push(new THREE.MeshBasicMaterial( { map: THREE.ImageUtils.loadTexture( 'images/xpos.png' ) }));
    //materialArray.push(new THREE.MeshBasicMaterial( { map: THREE.ImageUtils.loadTexture( 'images/xneg.png' ) }));
    //materialArray.push(new THREE.MeshBasicMaterial( { map: THREE.ImageUtils.loadTexture( 'images/ypos.png' ) }));
    //materialArray.push(new THREE.MeshBasicMaterial( { map: THREE.ImageUtils.loadTexture( 'images/yneg.png' ) }));
    //materialArray.push(new THREE.MeshBasicMaterial( { map: THREE.ImageUtils.loadTexture( 'images/zpos.png' ) }));
    //materialArray.push(new THREE.MeshBasicMaterial( { map: THREE.ImageUtils.loadTexture( 'images/zneg.png' ) }));
	        // create an array with six textures for cube
	        var materialArray = new THREE.MeshBasicMaterial[6];
	        materialArray[0] = (new THREE.MeshBasicMaterial( new { map = new THREE.Texture().With(
                                async s =>
                                {
                                    s.image = await new xpos();
                                    s.needsUpdate = true;
                                }) 
            }));
	        materialArray[1] = (new THREE.MeshBasicMaterial( new {  map = new THREE.Texture().With(
                                async s =>
                                {
                                    s.image = await new xneg();
                                    s.needsUpdate = true;
                                }) 
            }));
	        materialArray[2] = (new THREE.MeshBasicMaterial( new { map = new THREE.Texture().With(
                                async s =>
                                {
                                    s.image = await new ypos();
                                    s.needsUpdate = true;
                                }) 
            }));
	        materialArray[3] = (new THREE.MeshBasicMaterial( new { map = new THREE.Texture().With(
                                async s =>
                                {
                                    s.image = await new yneg();
                                    s.needsUpdate = true;
                                }) 
            }));
	        materialArray[4] = (new THREE.MeshBasicMaterial( new {map = new THREE.Texture().With(
                                async s =>
                                {
                                    s.image = await new zpos();
                                    s.needsUpdate = true;
                                }) 
            }));
	        materialArray[5] = (new THREE.MeshBasicMaterial( new { 
            map = new THREE.Texture().With(
                                async s =>
                                {
                                    s.image = await new zneg();
                                    s.needsUpdate = true;
                                }) 
            }));

            //var MovingCubeMat = new THREE.MeshFaceMaterial(new { material = materialArray });

            var MovingCubeGeom = new THREE.CubeGeometry(50, 50, 50, 1, 1, 1);
            var MovingCube = new THREE.Mesh(MovingCubeGeom, new THREE.MeshBasicMaterial(new
                                            {
                                                color = new THREE.Color(0xFFFFFF)
                                            }));
            MovingCube.position.set(0, 25.1, 0);
            scene.add(MovingCube);	
	
	        // a little bit of scenery...

	        var ambientlight = new THREE.AmbientLight(0x111111);
	        scene.add( ambientlight );

            // torus knot
            var colorMaterial = new THREE.MeshLambertMaterial(new { color = new THREE.Color(0xff3333) });
            var shape = new THREE.Mesh(new THREE.TorusKnotGeometry(30, 6, 160, 10, 2, 5, 4), colorMaterial);
            shape.position.set(-200, 50, -200);
            scene.add(shape);
            // torus knot
            var colorMaterial2 = new THREE.MeshLambertMaterial(new { color = new THREE.Color(0x33ff33) });
            var shape2 = new THREE.Mesh(new THREE.TorusKnotGeometry(30, 6, 160, 10, 3, 2, 4), colorMaterial2);
            shape2.position.set(200, 50, -200);
            scene.add(shape2);
            // torus knot
            var colorMaterial3 = new THREE.MeshLambertMaterial(new { color = new THREE.Color(0xffff33) });
            var shape3 = new THREE.Mesh(new THREE.TorusKnotGeometry(30, 6, 160, 10, 4, 3, 4), colorMaterial3);
            shape3.position.set(200, 50, 200);
            scene.add(shape3);
            // torus knot
            var colorMaterial4 = new THREE.MeshLambertMaterial(new { color = new THREE.Color(0x3333ff) });
            var shape4 = new THREE.Mesh(new THREE.TorusKnotGeometry(30, 6, 160, 10, 3, 4, 4), colorMaterial4);
            shape4.position.set(-200, 50, 200);
            scene.add(shape4);

            renderer.setSize(Native.window.Width, Native.window.Height);
	        renderer.setClearColor( 0x000000, 1 );
	        renderer.autoClear = false;

            Native.window.onframe +=
                e =>
                {
                    perspectiveCamera.updateProjectionMatrix();

                    render();
                };
            var isPushed = false;

            this.container.onkeydown +=
                     m =>
                     {
                         isPushed = true;

                         if (isPushed)
                         {
                             //var delta = clock.getDelta(); // seconds.
                             var moveDistance = 200 * 2; // 200 pixels per second
                             var rotateAngle = Math.PI / 2 * 2;   // pi/2 radians (90 degrees) per second

                             // local transformations

                             // move forwards/backwards/left/right
                             if (m.KeyCode == 87)
                                 MovingCube.translateZ(-moveDistance);
                             if (m.KeyCode == 83)
                                 MovingCube.translateZ(moveDistance);
                             if (m.KeyCode == 81)
                                 MovingCube.translateX(-moveDistance);
                             if (m.KeyCode == 69)
                                 MovingCube.translateX(moveDistance);

                             //// rotate left/right/up/down
                             //var rotation_matrix = new THREE.Matrix4().identity();
                             //if (m.KeyCode == 65)
                             //    MovingCube.rotateOnAxis(new THREE.Vector3(0, 1, 0), rotateAngle);
                             //if (m.KeyCode == 68)
                             //    MovingCube.rotateOnAxis(new THREE.Vector3(0, 1, 0), -rotateAngle);

                             //if (m.KeyCode == 90)
                             //{
                             //    MovingCube.position.set(0, 25.1, 0);
                             //    MovingCube.rotation.set(0, 0, 0);
                             //}
                         }
             };

            this.container.onkeyup += e =>
            {
                isPushed = false;
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
        public void render()
        {
            var w = Native.window.Width;
            var h = Native.window.Height;

            // setViewport parameters:
            //  lower_left_x, lower_left_y, viewport_width, viewport_height
            renderer.setViewport(0, 0, w, h);
            renderer.clear();

            // full display
            // renderer.setViewport( 0, 0, SCREEN_WIDTH - 2, 0.5 * SCREEN_HEIGHT - 2 );
            renderer.render(scene, perspectiveCamera);

            // minimap (overhead orthogonal camera)
            //  lower_left_x, lower_left_y, viewport_width, viewport_height
            renderer.setViewport(10, h - mapHeight - 10, mapWidth, mapHeight);
            renderer.render(scene, mapCamera);
        }
    }
}
