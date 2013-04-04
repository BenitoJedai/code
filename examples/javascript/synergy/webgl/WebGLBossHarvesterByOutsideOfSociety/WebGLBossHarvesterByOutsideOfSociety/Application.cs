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
using THREE.Design;
using WebGLBossHarvesterByOutsideOfSociety.Design;
using WebGLBossHarvesterByOutsideOfSociety.HTML.Pages;

namespace WebGLBossHarvesterByOutsideOfSociety
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
            #region await Three.js then do InitializeContent
            new[]
            {
                //new CANNON.opensource.github.cannon.js.build.cannon().Content,
                new THREE.opensource.gihtub.three.js.build.three().Content,
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
            //var container;

            //var camera, scene, renderer;

			var has_gl = false;

            //var delta;
            //var time;
            //var oldTime;

			var cameraTarget = new THREE_Vector3 { x = 0, y = 300, z = 0};

            //var harvesterMesh;
            //var animation;

            //var floor;

            //var boneArray;
            //var boneContainer;
			var positionVector = new THREE_Vector3();
			var lookVector = new THREE_Vector3();

			var lastframe = 0;







		

            //container = document.createElement( 'div' );
            //document.body.appendChild( container );

			var scene = new THREE_Scene();
			scene.fog = new THREE_Fog( 0x000000, 1000, 5000 );
				
			var camera = new THREE_PerspectiveCamera( 50, (double)Native.Window.Width / (double)Native.Window.Height, 1, 10000 );
			camera.position.z = 800;
			camera.position.y = 100;
				
			camera.lookAt(scene.position);
			scene.add( camera );

			// floor
			var plane = new THREE_PlaneGeometry(10000,10000,50,50);
			var floorMaterial = new THREE_MeshBasicMaterial( {wireframe: true, color: 0x333333} );
			var floor = new THREE_Mesh(plane, floorMaterial);
			floor.rotation.x = -Math.PI/2;
			scene.add(floor);

			// model
			var loader = new THREE_JSONLoader();


       

            var harvesterLoaded = IFunction.OfDelegate(
                new Action<object>(
                     (geometry) => 
                     {

                        //console.log("Number of bones: "+geometry.bones.length);

                        var material = new THREE_MeshBasicMaterial( { color: 0xffffff, wireframe: true, opacity: 0.25, transparent: true, skinning: true } );

                        harvesterMesh = new THREE_SkinnedMesh( geometry, material );
                        scene.add( harvesterMesh );

                        THREE.AnimationHandler.add( geometry.animation );
                        animation = new THREE_Animation( harvesterMesh, "walk1" );
                        animation.play();

                        harvesterMesh.rotation.x = -Math.PI/2;
                        harvesterMesh.rotation.z = -Math.PI/2;


                        var skin = harvesterMesh;

                        //setupBones(harvesterMesh);


            	        boneArray = [];
				        boneContainer = new THREE_Object3D();

				        boneContainer.rotation.x = -Math.PI/2;
				        boneContainer.rotation.z = -Math.PI/2;

				        scene.add(boneContainer);

				        var index = 0;
				        var material = new THREE_MeshPhongMaterial( { color: 0xff0000 } );
				
				        for ( var b = 1; b != skin.bones.length; b++ ) 
                        {
					
					        var bone = skin.bones[ b ];

					        var nc = bone.children.length;

					        for( var c = 0; c != nc; c++ ) {
						        var child = bone.children[ c ];

						        var size = Math.Min( child.position.length()*0.05, 8);
						
						        var cylinder = new THREE_CylinderGeometry( size, 0.1, child.position.length(), 6 );
					            cylinder.applyMatrix( new THREE_Matrix4().setRotationFromEuler( new THREE_Vector3( Math.PI / 2, 0, 0 ) ) );

						        cylinder.applyMatrix( new THREE_Matrix4().setPosition( new THREE_Vector3( 0, 0, 0.5 * child.position.length() ) ) );
						        var mesh = new THREE_Mesh( cylinder, material );

						        boneArray[child.id] = mesh;
						        boneContainer.add( mesh );
					        }
					
				        }




            
           //function updateBones ( skin ) {

           //     if (!boneArray) return;

           //     for ( var b = 1; b != skin.bones.length; b++ ) {
					
           //         var bone = skin.bones[b];
           //         var nc = bone.children.length;

           //         for( var c = 0; c != nc; c++ ) {
						
           //             var child = bone.children[c];
           //             var id = child.id;
           //             var mesh = boneArray[id];

           //             positionVector.getPositionFromMatrix(child.skinMatrix);
           //             mesh.position.copy(positionVector);

           //             lookVector.getPositionFromMatrix( child.parent.skinMatrix );
           //             mesh.lookAt( lookVector );

           //         }
					
           //     }

           // }

           // function render() {

           //     time = Date.now();
           //     delta = time - oldTime;
           //     oldTime = time;

           //     if (isNaN(delta)) {
           //         delta = 1000/60;
           //     }

           //     if (harvesterMesh) {

           //         THREE.AnimationHandler.update( delta/1000 );

           //         updateBones(harvesterMesh);

           //         boneContainer.position.z = harvesterMesh.position.z;
					
           //         var frame = Math.floor(animation.currentTime*24);
					
           //         if (frame >= 0 && lastframe > frame ) {
           //             harvesterMesh.position.z += 304.799987793; // got that from the root bone, total movement of one walk cycle
           //         }
           //         lastframe = frame;

           //         var speed = delta*0.131;

           //         cameraTarget.z += speed;

           //         if (harvesterMesh.position.z > floor.position.z+1000) {
           //             floor.position.z += 1000;
           //         };

           //     }

           //     camera.position.x = 800 * Math.sin(time/3000);
           //     camera.position.z = cameraTarget.z + 800 * Math.cos(time/3000);

           //     camera.lookAt(cameraTarget);

           //     if (has_gl) {
           //         renderer.render( scene, camera );
           //     }

           // }
                        //setInterval(render, 1000/60);
                    }
                )
            );

			loader.load( new WebGLBossHarvesterByOutsideOfSociety.Models.harvester().Content.src, harvesterLoaded );

			// lights
			var pointLight = new THREE_PointLight( 0xffffff, 1.0, z: 0 );
			camera.add( pointLight );

			try {
				// renderer
				renderer = new THREE_WebGLRenderer({antialias: true});
				renderer.setClearColorHex(0x000000);
				renderer.setSize(  (double)Native.Window.Width ,  (double)Native.Window.Height  );
                //THREEx.WindowResize(renderer, camera);

				container.appendChild( renderer.domElement );
				has_gl = true;
			}
			catch // (e) 
            {
				// need webgl
                //document.getElementById('info').innerHTML = "<P><BR><B>Note.</B> You need a modern browser that supports WebGL for this to run the way it is intended.<BR>For example. <a href='http://www.google.com/landing/chrome/beta/' target='_blank'>Google Chrome 9+</a> or <a href='http://www.mozilla.com/firefox/beta/' target='_blank'>Firefox 4+</a>.<BR><BR>If you are already using one of those browsers and still see this message, it's possible that you<BR>have old blacklisted GPU drivers. Try updating the drivers for your graphic card.<BR>Or try to set a '--ignore-gpu-blacklist' switch for the browser.</P><CENTER><BR><img src='../general/WebGL_logo.png' border='0'></CENTER>";
                //document.getElementById('info').style.display = "block";
                //return;
			}


		


		

        }
    }
}
