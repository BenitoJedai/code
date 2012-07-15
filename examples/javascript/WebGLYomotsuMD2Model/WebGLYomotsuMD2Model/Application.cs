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
        public Application(IDefaultPage page)
        {
            #region await Three.js then do InitializeContent
            new[]
            {
                new global::WebGLYomotsuMD2Model.Design.Three().Content,
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

        void InitializeContent(IDefaultPage page = null)
        {
            var width = Native.Window.Width;

            var height = Native.Window.Height;



            var scene = new THREE.Scene();



            var camera = new THREE.PerspectiveCamera(40, width / height, 1, 1000);

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

            renderer.domElement.AttachToDocument();

            //document.body.appendChild( renderer.domElement );



            var md2frames = new
            {

                // first, last, fps

                stand = new { min = 0, max = 39, fps = 9, state = "stand", action = false },   // STAND
                run = new { min = 40, max = 45, fps = 10, state = "stand", action = false },   // RUN
                attack = new { min = 46, max = 53, fps = 10, state = "stand", action = true },   // ATTACK
                pain1 = new { min = 54, max = 57, fps = 7, state = "stand", action = true },   // PAIN_A
                pain2 = new { min = 58, max = 61, fps = 7, state = "stand", action = true },   // PAIN_B
                pain3 = new { min = 62, max = 65, fps = 7, state = "stand", action = true },   // PAIN_C
                jump = new { min = 66, max = 71, fps = 7, state = "stand", action = true },   // JUMP
                flip = new { min = 72, max = 83, fps = 7, state = "stand", action = true },   // FLIP
                salute = new { min = 84, max = 94, fps = 7, state = "stand", action = true },   // SALUTE
                taunt = new { min = 95, max = 111, fps = 10, state = "stand", action = true },   // FALLBACK
                wave = new { min = 112, max = 122, fps = 7, state = "stand", action = true },   // WAVE
                point = new { min = 123, max = 134, fps = 6, state = "stand", action = true },   // POINT
                crstand = new { min = 135, max = 153, fps = 10, state = "crstand", action = false },   // CROUCH_STAND
                crwalk = new { min = 154, max = 159, fps = 7, state = "crstand", action = false },   // CROUCH_WALK
                crattack = new { min = 160, max = 168, fps = 10, state = "crstand", action = true },   // CROUCH_ATTACK
                crpain = new { min = 196, max = 172, fps = 7, state = "crstand", action = true },   // CROUCH_PAIN
                crdeath = new { min = 173, max = 177, fps = 5, state = "freeze", action = true },   // CROUCH_DEATH
                death1 = new { min = 178, max = 183, fps = 7, state = "freeze", action = true },   // DEATH_FALLBACK
                death2 = new { min = 184, max = 189, fps = 7, state = "freeze", action = true },   // DEATH_FALLFORWARD
                death3 = new { min = 190, max = 197, fps = 7, state = "freeze", action = true },   // DEATH_FALLBACKSLOW

                //boom    : [ 198, 198,  5 ]    // BOOM

            };



             //player.changeMotion = function(motion){

            //    player.motion = motion;

            //    player.state = md2frames[motion][3].state;

            //    var animMin = md2frames[motion][0];

            //    var animMax = md2frames[motion][1];

            //    var animFps = md2frames[motion][2];

            //    player.mesh.time = 0;

            //    player.mesh.duration = 1000 * (( animMax - animMin ) / animFps);

            //    player.mesh.setFrameRange( animMin, animMax );

            //}



            //load converted md2 data

            //var material = new THREE.MeshPhongMaterial( { map: THREE.ImageUtils.loadTexture(

            //    new HTML.Images.FromAssets._1().src

            //    ), ambient: 0x999999, color: 0xffffff, specular: 0xffffff, shininess: 25, morphTargets: true } );

            var loader = new THREE.JSONLoader();

            //loader.load( 'droid.js', function( geometry ) {

            //    player.mesh = new THREE.MorphAnimMesh( geometry, material );

            //    player.mesh.rotation.y = -Math.PI / 2;

            //    //player.mesh.scale.set(2, 2, 2);

            //    player.mesh.castShadow = true;

            //    player.mesh.receiveShadow = false;

            //    player.changeMotion('stand');

            //    scene.add( player.mesh );



            //} );





            var theta = 0;

            var clock = new THREE.Clock();

            Action loop = null;


            loop = delegate
            {



                var delta = clock.getDelta();

                //if (player.mesh && player.motion) {

                //    var isEndFleame = (md2frames[player.motion][1] === player.mesh.currentKeyframe);

                //    var isAction = md2frames[player.motion][3].action;



                //    if(!isAction || (isAction && !isEndFleame)){

                //        player.mesh.updateAnimation(1000 * delta);

                //    }else if(/freeze/.test(md2frames[player.motion][3].state)){

                //        //dead...

                //    }else{

                //        player.changeMotion(player.state);

                //    }

                //}

                camera.position.x = (float)( 150 * Math.Sin(theta / 2 * Math.PI / 360));

                camera.position.y = (float)( 150 * Math.Sin(theta / 2 * Math.PI / 360));

                camera.position.z = (float)(150 * Math.Cos(theta / 2 * Math.PI / 360));

                camera.lookAt(scene.position);

                theta++;



                renderer.render(scene, camera);

                Native.Window.requestAnimationFrame += loop;

            };

            loop();

        }

    }
}
