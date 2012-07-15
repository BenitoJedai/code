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
using WebGLYomotsuMD2Model.Design;
using WebGLYomotsuMD2Model.HTML.Pages;

namespace WebGLYomotsuMD2Model
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // inspired by http://yomotsu.github.com/threejs-examples/md2/

        public readonly ApplicationWebService service = new ApplicationWebService();




        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page = null)
        {
            #region await then do InitializeContent
            new[]
            {
                new global::WebGLYomotsuMD2Model.Design.Three().Content,
                //new global::WebGLYomotsuMD2Model.Design.droid().Content,
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
                    InitializeContent(page);
                }
            );
            #endregion

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }


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

                // why?
                __first_broken = null,


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

        void InitializeContent(IDefaultPage page = null)
        {
            var fov = 40;

            #region container
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            var container = new IHTMLDiv();

            container.AttachToDocument();
            container.style.backgroundColor = "#000000";
            container.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);
            #endregion



            var width = Native.Window.Width;
            var height = Native.Window.Height;



            var scene = new THREE.Scene();

            var camera = new THREE.PerspectiveCamera(fov, width / height, 1, 1000);

            scene.add(camera);



            //var ambient = new THREE.AmbientLight( 0xffffff);

            //scene.add( ambient );

            var light = new THREE.DirectionalLight(0xffffff, 0.8);
            light.position.set(1, 1, 1).normalize();
            scene.add(light);

            var light2 = new THREE.DirectionalLight(0xffffff);
            light2.position.set(-1, -1, -1).normalize();
            scene.add(light2);



            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(width, height);
            renderer.domElement.AttachTo(container);

            var md2frames = new md2frames();

            //load converted md2 data

            var material = new THREE.MeshPhongMaterial(
                new THREE.MeshPhongMaterialArguments
                {
                    map = __THREE.ImageUtils.loadTexture(
                        new HTML.Images.FromAssets._1().src
                    ),
                    ambient = 0x999999,
                    color = 0xffffff,
                    specular = 0xffffff,
                    shininess = 25,
                    morphTargets = true
                }
            );



            #region AtResize
            Action AtResize = delegate
            {
                container.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);


                renderer.setSize(Native.Window.Width, Native.Window.Height);

                camera.projectionMatrix.makePerspective(fov, Native.Window.Width / Native.Window.Height, 1, 1100);

                //camera.aspect = Native.Window.Width / Native.Window.Height;
                //camera.updateProjectionMatrix();
            };

            Native.Window.onresize +=
                delegate
                {
                    AtResize();
                };

            AtResize();
            #endregion

            #region requestFullscreen
            Native.Document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    Native.Document.body.requestFullscreen();

                    //AtResize();
                };
            #endregion



            var loader = new THREE.JSONLoader();

            loader.load(
                new global::WebGLYomotsuMD2Model.Design.droid().Content.src,
                    IFunction.OfDelegate(
                        new Action<object>(
                            geometry =>
                            {








                                var player_mesh = new THREE.MorphAnimMesh(geometry, material);

                                player_mesh.rotation.y = (float)(-Math.PI / 2);
                                //player.mesh.scale.set(2, 2, 2);
                                player_mesh.castShadow = true;
                                player_mesh.receiveShadow = false;

                                #region player_motion
                                var player_motion = default(motion);
                                Action<motion> player_changeMotion = motion =>
                                {
                                    player_motion = motion;

                                    //    player.state = md2frames[motion][3].state;

                                    var animMin = motion.min;
                                    var animMax = motion.max;
                                    var animFps = motion.fps;

                                    player_mesh.time = 0;
                                    player_mesh.duration = 1000f * ((animMax - animMin) / animFps);
                                    player_mesh.setFrameRange(animMin, animMax);
                                };

                                player_changeMotion(md2frames.stand);
                                #endregion


                                scene.add(player_mesh);

                                var theta = 0;

                                var clock = new THREE.Clock();

                                #region loop
                                Action loop = null;


                                loop = delegate
                                {
                                    var delta = clock.getDelta();
                                    var isEndFleame = (player_motion.max == player_mesh.currentKeyframe);
                                    var isAction = player_motion.action;

                                    var x = (isAction && !isEndFleame);

                                    if (!isAction || x)
                                    {

                                        player_mesh.updateAnimation(1000 * delta);

                                    }
                                    else if (player_motion.state == "freeze")
                                    {

                                        //dead...

                                    }
                                    else
                                    {

                                        player_changeMotion(player_motion);

                                    }


                                    camera.position.x = (float)(150 * Math.Sin(theta / 2 * Math.PI / 360));
                                    camera.position.y = (float)(150 * Math.Sin(theta / 2 * Math.PI / 360));
                                    camera.position.z = (float)(150 * Math.Cos(theta / 2 * Math.PI / 360));

                                    camera.lookAt(scene.position);

                                    theta++;



                                    renderer.render(scene, camera);

                                    Native.Window.requestAnimationFrame += loop;

                                };

                                loop();
                                #endregion


                                var toolbar = new Toolbar();

                                if (page != null)
                                {
                                    toolbar.Container.style.Opacity = 0.7;
                                    toolbar.Container.AttachToDocument();
                                    toolbar.Container.style.position = IStyle.PositionEnum.absolute;
                                    toolbar.Container.style.right = "0";
                                    toolbar.Container.style.bottom = "0";

                                    toolbar.HideButton.onclick +=
                                         delegate
                                         {
                                             // ScriptCoreLib.Extensions
                                             toolbar.HideTarget.ToggleVisible();
                                         };


                                    Action<IHTMLButton, motion> bind =
                                        (btn, value) => btn.onclick += delegate { player_changeMotion(value); };

                                    bind(toolbar.Stand, md2frames.stand);
                                    bind(toolbar.Run, md2frames.run);
                                    bind(toolbar.Attack, md2frames.attack);
                                    bind(toolbar.Pain1, md2frames.pain1);
                                    bind(toolbar.Pain2, md2frames.pain2);
                                    bind(toolbar.Pain3, md2frames.pain3);
                                    bind(toolbar.Jump, md2frames.jump);
                                    bind(toolbar.Flip, md2frames.flip);
                                    bind(toolbar.Salute, md2frames.salute);
                                    bind(toolbar.Taunt, md2frames.taunt);
                                    bind(toolbar.Wave, md2frames.wave);
                                    bind(toolbar.Point, md2frames.point);
                                    bind(toolbar.Crstand, md2frames.crstand);
                                    bind(toolbar.Crwalk, md2frames.crwalk);
                                    bind(toolbar.Crattack, md2frames.crattack);
                                    bind(toolbar.Crpain, md2frames.crpain);
                                    bind(toolbar.Crdeath, md2frames.crdeath);
                                    bind(toolbar.Death1, md2frames.death1);
                                    bind(toolbar.Death2, md2frames.death2);
                                    bind(toolbar.Death3, md2frames.death3);
                                }


                            }
                        )
                    )
            );



            
        
        }
        bool IsDisposed = false;

        public Action Dispose;
    }
}
