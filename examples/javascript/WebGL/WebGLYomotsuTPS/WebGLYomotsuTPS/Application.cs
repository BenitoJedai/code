using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
//using WebGLYomotsuTPS.Design;
using WebGLYomotsuTPS.HTML.Pages;
using System.Collections.Generic;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.Windows.Forms;

namespace WebGLYomotsuTPS
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // inspired by http://yomotsu.github.com/threejs-examples/tps/

//        Create Partial Type: THREE+SkinnedMesh
//0dc8:01:01 RewriteToAssembly error: System.NotSupportedException: Parent does not have a default constructor.The default constructor must be explicitly defined.

//020000a1 WebGLYomotsuTPS.HTML.Pages.App
//        { SourceMethod = WebGLYomotsuTPS.HTML.Pages.App Create() }
//{ SourceMethod = Void Page_InitializeInternalFontFace() }
//{ SourceMethod = ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement Initialize_0_htm
//l(WebGLYomotsuTPS.HTML.Pages.App, ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement
//) }
//script: error JSC1000: Method: Initialize_0_html, Type: WebGLYomotsuTPS.HTML.Pag
//es.App; emmiting failed : System.IndexOutOfRangeException: Index was outside the
// bounds of the array.
//   at jsc.ILInstruction.get_TargetParameter() in X:\jsc.internal.git\compiler\js
//c\CodeModel\ILInstruction.cs:line 1389
//   at jsc.IL2ScriptGenerator.<>c__DisplayClass4.<CreateInstructionHandlers>b__5(
//IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem[] s) in X:\jsc.i
//nternal.git\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.OpCodes.cs:line
// 54
//   at jsc.IL2ScriptGenerator.OpCodeHandler(IdentWriter w, Prestatement p, ILInst
//ruction i, ILFlowStackItem s) in X:\jsc.internal.git\compiler\jsc\Languages\Java
//Script\IL2ScriptGenerator.cs:line 263

        sealed class motion
        {
            public int min = 0;
            public int max = 39;
            public int fps = 9;
            public string state = "stand";
            public bool action;
        }

        sealed class md2frames
        {
            // first, last, fps

            public motion
              stand = new motion { min = 0, max = 39, fps = 9, state = "stand", action = false },   // STAND

              run = new motion { min = 40, max = 45, fps = 10, state = "stand", action = false },   // RUN

              attack = new motion { min = 46, max = 53, fps = 10, state = "stand", action = true },   // ATTACK
              pain1 = new motion { min = 54, max = 57, fps = 7, state = "stand", action = true },   // PAIN_A
              pain2 = new motion { min = 58, max = 61, fps = 7, state = "stand", action = true },   // PAIN_B
              pain3 = new motion { min = 62, max = 65, fps = 7, state = "stand", action = true },   // PAIN_C
              jump = new motion { min = 66, max = 71, fps = 7, state = "stand", action = true },   // JUMP
              flip = new motion { min = 72, max = 83, fps = 7, state = "stand", action = true },   // FLIP
              salute = new motion { min = 84, max = 94, fps = 7, state = "stand", action = true },   // SALUTE
              taunt = new motion { min = 95, max = 111, fps = 10, state = "stand", action = true },   // FALLBACK
              wave = new motion { min = 112, max = 122, fps = 7, state = "stand", action = true },   // WAVE
              point = new motion { min = 123, max = 134, fps = 6, state = "stand", action = true },   // POINT
              crstand = new motion { min = 135, max = 153, fps = 10, state = "crstand", action = false },   // CROUCH_STAND
              crwalk = new motion { min = 154, max = 159, fps = 7, state = "crstand", action = false },   // CROUCH_WALK
              crattack = new motion { min = 160, max = 168, fps = 10, state = "crstand", action = true },   // CROUCH_ATTACK
              crpain = new motion { min = 196, max = 172, fps = 7, state = "crstand", action = true },   // CROUCH_PAIN
              crdeath = new motion { min = 173, max = 177, fps = 5, state = "freeze", action = true },   // CROUCH_DEATH
              death1 = new motion { min = 178, max = 183, fps = 7, state = "freeze", action = true },   // DEATH_FALLBACK
              death2 = new motion { min = 184, max = 189, fps = 7, state = "freeze", action = true },   // DEATH_FALLFORWARD
              death3 = new motion { min = 190, max = 197, fps = 7, state = "freeze", action = true };   // DEATH_FALLBACKSLOW

            //boom    : [ 198, 198,  5 ]    // BOOM

        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page = null)
        {
            { THREE.SkinnedMesh ref0; }
            { THREE.SpeedBlendCharacter ref0; }


#if FCHROME
            #region AtFormCreated
            FormStyler.AtFormCreated =
                 s =>
                 {
                     s.Context.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                     // this is working?
                     var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDrag().AttachTo(s.Context.GetHTMLTarget());
                 };
            #endregion

            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;
                chrome.Notification.DefaultTitle = "WebGLYomotsuTPS";


                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text,
                    AtFormCreated: FormStyler.AtFormCreated
                );

                return;
            }
            #endregion
#endif


            var fov = 40;

            #region container
            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            var container = new IHTMLDiv();

            container.AttachToDocument();
            container.style.backgroundColor = "#000000";
            container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);
            #endregion

            var player_model_objects = new THREE.Object3D();
            var player_position_x = 0.0f;
            var player_position_y = 0.0f;
            var player_position_z = 0.0f;
            var player_position_direction = 0;
            var player_camera_speed = 300;
            var player_camera_distance = 5;
            var player_camera_x = 0.0f;
            var player_camera_y = 0.0f;
            var player_camera_z = 0.0f;
            var player_motion = default(motion);

            //var width = Native.window.Width;
            //var height = Native.window.Height;

            var clock = new Stopwatch();
            clock.Start();
            //var clock = new THREE.Clock();

            var scene = new THREE.Scene();
            scene.fog = new THREE.FogExp2(0x000000, 0.05f);

            scene.add(player_model_objects);

            var camera = new THREE.PerspectiveCamera(fov,
                Native.window.aspect,
                //width / height,
                1, 1000);

            scene.add(camera);

            #region light

            var light = new THREE.DirectionalLight(0xffffff, 1.5);

            light.position.set(1, 1, 1).normalize();

            light.castShadow = true;




            scene.add(light);




            #endregion

            var md2frames = new md2frames();

            var moveState_moving = false;
            var moveState_front = false;
            var moveState_Backwards = false;
            var moveState_left = false;
            var moveState_right = false;
            var moveState_speed = .1;
            var moveState_angle = 0;

            #region move
            Action move = delegate
            {
                //            if(player.model.motion !== 'run' && player.model.state === 'stand'){

                //    changeMotion('run');

                //}

                //if(player.model.motion !== 'crwalk' && player.model.state === 'crstand'){

                //    changeMotion('crwalk');

                //}

                var speed = moveState_speed;

                //if(player.model.state === 'crstand'){speed *= .5;}

                //if(player.model.state === 'freeze') {speed *= 0;}



                var direction = moveState_angle;

                if (moveState_front && !moveState_left && !moveState_Backwards && !moveState_right) { direction += 0; }
                if (moveState_front && moveState_left && !moveState_Backwards && !moveState_right) { direction += 45; }
                if (!moveState_front && moveState_left && !moveState_Backwards && !moveState_right) { direction += 90; }
                if (!moveState_front && moveState_left && moveState_Backwards && !moveState_right) { direction += 135; }
                if (!moveState_front && !moveState_left && moveState_Backwards && !moveState_right) { direction += 180; }
                if (!moveState_front && !moveState_left && moveState_Backwards && moveState_right) { direction += 225; }
                if (!moveState_front && !moveState_left && !moveState_Backwards && moveState_right) { direction += 270; }
                if (moveState_front && !moveState_left && !moveState_Backwards && moveState_right) { direction += 315; }



                player_model_objects.rotation.y = (float)(direction * Math.PI / 180);

                player_position_x -= (float)(Math.Sin(direction * Math.PI / 180) * speed);
                player_position_z -= (float)(Math.Cos(direction * Math.PI / 180) * speed);

            };
            #endregion

            #region camera rotation


            Action rotate = delegate { };

            var pointer_x = 0f;
            var pointer_y = 0f;
            var oldPointerX = 0f;
            var oldPointerY = 0f;

            container.onmousemove +=
                e =>
                {
                    if (Native.document.pointerLockElement == container)
                    {
                        oldPointerX = 0;
                        oldPointerY = 0;
                        pointer_x = e.movementX * 0.01f;
                        pointer_y = -e.movementY * 0.01f;
                        rotate();
                        return;
                    }

                    pointer_x = (e.CursorX / Native.window.Width) * 2 - 1;
                    pointer_y = -(e.CursorY / Native.window.Height) * 2 + 1;
                    rotate();
                };


            container.onmouseup +=
              e =>
              {
                  rotate = delegate { };

                  Native.document.exitPointerLock();
              };

            container.onmousedown +=
                e =>
                {

                    oldPointerX = pointer_x;
                    oldPointerY = pointer_y;

                    rotate = delegate
                    {
                        player_camera_x += (oldPointerX - pointer_x) * player_camera_speed;
                        player_camera_y += (oldPointerY - pointer_y) * player_camera_speed;

                        if (player_camera_y > 150)
                        {
                            player_camera_y = 150;
                        }

                        if (player_camera_y < -150)
                        {
                            player_camera_y = -150;
                        }

                        moveState_angle = Convert.ToInt32(player_camera_x / 2) % 360;

                        oldPointerX = pointer_x;
                        oldPointerY = pointer_y;

                    };

                    Console.WriteLine("requestPointerLock");
                    container.requestPointerLock();
                };

            #endregion

            var renderer = new THREE.WebGLRenderer();
            //renderer.setSize(width, height);
            renderer.setSize();
            renderer.shadowMapEnabled = true;
            renderer.shadowMapSoft = true;
            renderer.domElement.AttachTo(container);




            #region create field


            var planeGeometry = new THREE.PlaneGeometry(1000, 1000);
            var planeMaterial = new THREE.MeshLambertMaterial(
                new
            {
                map = THREE.ImageUtils.loadTexture(new HTML.Images.FromAssets.bg().src),
                color = 0xffffff
            }
            );

            planeMaterial.map.repeat.x = 300;
            planeMaterial.map.repeat.y = 300;
            planeMaterial.map.wrapS = THREE.RepeatWrapping;
            planeMaterial.map.wrapT = THREE.RepeatWrapping;
            var plane = new THREE.Mesh(planeGeometry, planeMaterial);
            plane.castShadow = false;
            plane.receiveShadow = true;


            {

                var parent = new THREE.Object3D();
                parent.add(plane);
                parent.rotation.x = -Math.PI / 2;

                scene.add(parent);
            }

            var random = new Random();
            var meshArray = new List<THREE.Mesh>();
            var geometry = new THREE.CubeGeometry(1, 1, 1);

            for (var i = 0; i < 100; i++)
            {

                var ii = new THREE.Mesh(geometry, new THREE.MeshLambertMaterial(
                    new
                {
                    color = (Convert.ToInt32(0xffffff * random.NextDouble()))
                }));
                ii.position.x = i % 2 * 5 - 2.5f;
                ii.position.y = .5f;
                ii.position.z = -1 * i * 4;
                ii.castShadow = true;
                ii.receiveShadow = true;


                meshArray.Add(ii);

                scene.add(ii);

            }
            #endregion







            //load converted md2 data

            var material = new THREE.MeshPhongMaterial(
                new
            {
                map = THREE.ImageUtils.loadTexture(
                        new HTML.Images.FromAssets._1().src
                    ),
                ambient = 0x999999,
                color = 0xffffff,
                specular = 0xffffff,
                shininess = 25,
                morphTargets = true
            }
            );




            var loader = new THREE.JSONLoader();

            loader.load(
                new global::WebGLYomotsuTPS.Design.droid().Content.src,
                    IFunction.OfDelegate(
                        new Action<object>(
                            xgeometry =>
                            {
                                var md2meshBody = new THREE.MorphAnimMesh(xgeometry, material);

                                md2meshBody.rotation.y = (float)(-Math.PI / 2);
                                md2meshBody.scale.set(.02, .02, .02);
                                md2meshBody.position.y = .5f;
                                md2meshBody.castShadow = true;
                                md2meshBody.receiveShadow = false;

                                #region player_motion
                                Action<motion> player_changeMotion = motion =>
                                {
                                    Console.WriteLine(
                                        new { motion, md2frames.run, md2frames.stand });

                                    player_motion = motion;

                                    //    player.state = md2frames[motion][3].state;

                                    var animMin = motion.min;
                                    var animMax = motion.max;
                                    var animFps = motion.fps;

                                    md2meshBody.time = 0;
                                    md2meshBody.duration = (int)(
                                        1000 * ((animMax - animMin) / (double)animFps)
                                    );
                                    Native.document.title = new { animMin, animMax }.ToString();

                                    md2meshBody.setFrameRange(animMin, animMax);
                                };

                                player_changeMotion(md2frames.stand);
                                #endregion

                                player_model_objects.add(md2meshBody);

                                #region onkeydown
                                Native.document.onkeydown +=
                                    e =>
                                    {
                                        if (e.KeyCode == 67)
                                        {
                                            if (player_motion == md2frames.stand)
                                                player_changeMotion(md2frames.crstand);
                                            else if (player_motion == md2frames.crstand)
                                                player_changeMotion(md2frames.stand);

                                        }
                                        else if (e.KeyCode == 87)
                                        {
                                            moveState_front = true;
                                            moveState_Backwards = false;
                                        }
                                        else if (e.KeyCode == 83)
                                        {
                                            moveState_front = false;
                                            moveState_Backwards = true;
                                        }
                                        else if (e.KeyCode == 65)
                                        {
                                            moveState_left = true;
                                            moveState_right = false;
                                        }
                                        else if (e.KeyCode == 68)
                                        {
                                            moveState_left = false;
                                            moveState_right = true;
                                        }

                                        var isStand = player_motion == md2frames.stand;
                                        Console.WriteLine(
                                            new { e.KeyCode, moveState_front, moveState_Backwards, isStand }
                                            );

                                        if (moveState_front || moveState_Backwards || moveState_left || moveState_right)
                                            if (player_motion == md2frames.stand)
                                                player_changeMotion(md2frames.run);
                                            else if (player_motion == md2frames.crstand)
                                                player_changeMotion(md2frames.crwalk);
                                    };
                                #endregion

                                #region onkeyup
                                Native.document.onkeyup +=
                                    e =>
                                    {
                                        if (e.KeyCode == 87)
                                        {
                                            moveState_front = false;
                                        }
                                        else if (e.KeyCode == 83)
                                        {
                                            moveState_Backwards = false;
                                        }
                                        else if (e.KeyCode == 65)
                                        {
                                            moveState_left = false;
                                        }
                                        else if (e.KeyCode == 68)
                                        {
                                            moveState_right = false;
                                        }

                                    };
                                #endregion




                                #region loop


                                Native.window.onframe += delegate
                                {
                                    if (moveState_front || moveState_Backwards || moveState_left || moveState_right)
                                        move();
                                    else
                                        if (player_motion == md2frames.run)
                                        player_changeMotion(md2frames.stand);
                                    else if (player_motion == md2frames.crwalk)
                                        player_changeMotion(md2frames.crstand);



                                    player_model_objects.position.x = player_position_x;
                                    player_model_objects.position.y = player_position_y;
                                    player_model_objects.position.z = player_position_z;

                                    // camera rotate x
                                    camera.position.x = (float)(player_position_x + player_camera_distance * Math.Sin((player_camera_x) * Math.PI / 360.0));
                                    camera.position.z = (float)(player_position_z + player_camera_distance * Math.Cos((player_camera_x) * Math.PI / 360.0));

                                    //camera rotate y
                                    //camera.position.x = player.position.x + player.camera.distance * Math.cos( (player.camera.y) * Math.PI / 360 );
                                    camera.position.y = (float)(player_position_y + player_camera_distance * Math.Sin((player_camera_y) * Math.PI / 360.0));
                                    //camera.position.z = player.position.z + player.camera.distance * Math.cos( (player.camera.y) * Math.PI / 360 );

                                    camera.position.y += 1;
                                    //console.log(camera.position.z)

                                    var vec3 = new THREE.Vector3(player_position_x, player_position_y, player_position_z);

                                    camera.lookAt(vec3);



                                    #region model animation

                                    var delta = clock.ElapsedMilliseconds * 0.001;
                                    clock.Restart();

                                    var isEndFleame = (player_motion.max == md2meshBody.currentKeyframe);
                                    var isAction = player_motion.action;

                                    var x = (isAction && !isEndFleame);

                                    if (!isAction || x)
                                    {
                                        md2meshBody.updateAnimation(1000 * delta);
                                    }
                                    else if (player_motion.state == "freeze")
                                    {
                                        //dead...
                                    }
                                    else
                                    {
                                        player_changeMotion(player_motion);
                                    }
                                    #endregion

                                    renderer.render(scene, camera);


                                };

                                #endregion





                                #region AtResize
                                Action AtResize = delegate
                                {
                                    container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);


                                    renderer.setSize(Native.window.Width, Native.window.Height);

                                    camera.projectionMatrix.makePerspective(fov, Native.window.aspect, 1, 1100);

                                    //camera.aspect = Native.Window.Width / Native.Window.Height;
                                    //camera.updateProjectionMatrix();
                                };

                                Native.window.onresize +=
                                    delegate
                                {
                                    AtResize();
                                };

                                AtResize();
                                #endregion

                                #region requestFullscreen
                                Native.document.body.ondblclick +=
                                    delegate
                                {
                                    if (IsDisposed)
                                        return;

                                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                                    Native.document.body.requestFullscreen();

                                    //AtResize();
                                };
                                #endregion


                            }
                        )
                    )
            );





        }
        bool IsDisposed = false;

        public Action Dispose;
    }
}
