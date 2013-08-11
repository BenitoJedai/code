using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
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
        public Application(IApp page = null)
        {
            Console.WriteLine("before three");


            Action Toggle = DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();


            Console.WriteLine("InitializeContent");
            // http://oos.moxiecode.com/js_webgl/md5_test/

            var has_gl = false;
            THREE.WebGLRenderer renderer = null;

            var oldTime = 0L;

            var cameraTarget = new THREE.Vector3 { x = 0, y = 300, z = 0 };

            var positionVector = new THREE.Vector3();
            var lookVector = new THREE.Vector3();

            var lastframe = 0;

            var scene = new THREE.Scene();
            scene.fog = new THREE.Fog(0x000000, 1000, 5000);

            var camera = new THREE.PerspectiveCamera(50, (double)Native.window.Width / (double)Native.window.Height, 1, 10000);
            camera.position.z = 800;
            camera.position.y = 100;

            camera.lookAt(scene.position);
            scene.add(camera);

            // floor
            var plane = new THREE.PlaneGeometry(10000, 10000, 50, 50);
            var floorMaterial = new THREE.MeshBasicMaterial(new { wireframe = true, color = 0x333333 });
            var floor = new THREE.Mesh(plane, floorMaterial);
            floor.rotation.x = -Math.PI / 2;
            scene.add(floor);

            // model
            var loader = new THREE.JSONLoader();




            var harvesterLoaded = IFunction.OfDelegate(
                new Action<dynamic>(
                     (geometry) =>
                     {
                         Console.WriteLine("geometry ready!");

                         //console.log("Number of bones: "+geometry.bones.length);

                         var material = new THREE.MeshBasicMaterial(new
                         {
                             color = 0xffffff,
                             wireframe = true,
                             opacity = 0.25,
                             transparent = true,
                             skinning = true
                         });

                         object geometry_object = geometry;
                         var skin = new THREE.SkinnedMesh(geometry_object, material);
                         scene.add(skin);


                         object geometry_animation = geometry.animation;
                         THREE.AnimationHandler.add(geometry_animation);

                         var animation = new THREE.Animation(skin, "walk1");
                         animation.play();

                         skin.rotation.x = -Math.PI / 2;
                         skin.rotation.z = -Math.PI / 2;





                         var boneArray = new Dictionary<int, THREE.Mesh>();


                         var boneContainer = new THREE.Object3D();

                         boneContainer.rotation.x = -Math.PI / 2;
                         boneContainer.rotation.z = -Math.PI / 2;

                         scene.add(boneContainer);

                         var index = 0;
                         var pmaterial = new THREE.MeshPhongMaterial(new { color = 0xff0000 });

                         for (var b = 1; b != skin.bones.Length; b++)
                         {

                             var bone = skin.bones[b];

                             var nc = bone.children.Length;

                             for (var c = 0; c != nc; c++)
                             {
                                 var child = bone.children[c];

                                 var size = Math.Min(child.position.length() * 0.05, 8);

                                 var cylinder = new THREE.CylinderGeometry(size, 0.1, child.position.length(), 6);

                                  cylinder.applyMatrix(new THREE.Matrix4().makeRotationFromEuler(new THREE.Vector3(Math.PI / 2, 0, 0)));

                                 cylinder.applyMatrix(new THREE.Matrix4().setPosition(new THREE.Vector3(0, 0, 0.5 * child.position.length())));
                                 var mesh = new THREE.Mesh(cylinder, pmaterial);

                                 boneArray[child.id] = mesh;
                                 boneContainer.add(mesh);
                             }

                         }




                         #region render


                         Action render = null;


                         render = delegate
                         {
                             Func<long> Date_now = () => (long)new IFunction("return Date.now();").apply(null);

                             var time = Date_now();
                             double delta = time - oldTime;


                             if (oldTime == 0)
                             {
                                 delta = 1000 / 60.0;
                             }

                             oldTime = time;




                             THREE.AnimationHandler.update(delta / 1000.0);




                             for (var b = 1; b != skin.bones.Length; b++)
                             {

                                 var bone = skin.bones[b];
                                 var nc = bone.children.Length;

                                 for (var c = 0; c != nc; c++)
                                 {

                                     var child = bone.children[c];
                                     var child_bone = (THREE.Bone)(object)child;
                                     var id = child.id;
                                     var mesh = boneArray[id];

                                     positionVector.getPositionFromMatrix(child_bone.skinMatrix);
                                     mesh.position.copy(positionVector);

                                     var child_parent_bone = (THREE.Bone)(object)child.parent;
                                     lookVector.getPositionFromMatrix(child_parent_bone.skinMatrix);
                                     mesh.lookAt(lookVector);

                                 }

                             }


                             boneContainer.position.z = skin.position.z;

                             var frame = (int)Math.Floor(animation.currentTime * 24.0);

                             if (frame >= 0 && lastframe > frame)
                             {
                                 skin.position.z += 304.799987793; // got that from the root bone, total movement of one walk cycle
                             }
                             lastframe = frame;

                             var speed = delta * 0.131;

                             cameraTarget.z += speed;

                             if (skin.position.z > floor.position.z + 1000.0)
                             {
                                 floor.position.z += 1000.0;
                             };


                             camera.position.x = 800.0 * Math.Sin(time / 3000.0);
                             camera.position.z = cameraTarget.z + 800.0 * Math.Cos(time / 3000.0);

                             camera.lookAt(cameraTarget);

                             if (has_gl)
                             {
                                 renderer.render(scene, camera);
                             }

                             Native.window.requestAnimationFrame += render;
                         };
                         #endregion

                         Native.window.requestAnimationFrame += render;

                         Console.WriteLine("requestAnimationFrame ready!");
                     }
                )
            );

            var harvester_src = new WebGLBossHarvesterByOutsideOfSociety.Models.harvester().Content.src;

            Console.WriteLine("before harvester " + new { harvester_src });
            loader.load(harvester_src, harvesterLoaded);

            // lights
            var pointLight = new THREE.PointLight(0xffffff, 1.0, z: 0);
            camera.add(pointLight);

            try
            {
                // renderer
                renderer = new THREE.WebGLRenderer(new { antialias = true });
                renderer.setClearColorHex(0x000000);

                renderer.domElement.AttachToDocument();

                Action AtResize = delegate
                {
                    camera.aspect = Native.window.Width / Native.window.Height;
                    camera.updateProjectionMatrix();
                    renderer.setSize(Native.window.Width, Native.window.Height);
                };
                Native.window.onresize +=
                  delegate
                  {
                      AtResize();
                  };

                AtResize();

                has_gl = true;

                Console.WriteLine("renderer ready!");
            }
            catch
            {

            }




            Native.document.onmousedown +=
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
                         Native.document.body.requestFullscreen();
                         Native.document.body.requestPointerLock();
                         return;
                     }
                 };




        }
    }
}
