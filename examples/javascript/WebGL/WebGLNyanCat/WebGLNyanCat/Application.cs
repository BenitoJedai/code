using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using WebGLNyanCat.HTML.Pages;
using WebGLNyanCat.Design;
using System.Diagnostics;

namespace WebGLNyanCat
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using WebGLNyanCat.HTML.Audio.FromAssets;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // see also: http://dl.dropbox.com/u/6213850/WebGL/nyanCat/nyan.html 

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {

            InitializeContent();

            style.Content.AttachToHead();

        }

        void InitializeContent()
        {

            #region make sure we atleast have our invisible DOM
            var page_song = new nyanlooped { loop = true };
            var page_song2 = new nyanslow { loop = true };


            #endregion

            #region container
            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            var container = new IHTMLDiv();

            container.AttachToDocument();
            container.style.backgroundColor = "#003366";
            container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);
            #endregion

            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(Native.window.Width, Native.window.Height);
            renderer.domElement.AttachTo(container);

            var numStars = 10;
            var numRainChunks = 30;
            var mouseX = 0;
            var mouseY = 0;

            var clock = new Stopwatch();
            clock.Start();

            var deltaSum = 0f;
            //tick=0, 
            var frame = 0;

            var running = true;


            #region onmousemove
            Native.document.onmousemove +=
                e =>
                {
                    if (IsDisposed)
                        return;

                    var windowHalfX = Native.window.Width / 2;
                    var windowHalfY = Native.window.Height / 2;

                    mouseX = (e.CursorX - windowHalfX);
                    mouseY = (e.CursorY - windowHalfY);
                };

            #endregion

            Action PlaySomething =
                delegate
                {
                    if (running)
                    {
                        page_song.play();
                        page_song2.pause();
                    }
                    else
                    {
                        page_song.pause();
                        page_song2.play();
                    }
                };
            #region onmousedown
            Native.document.onmousedown +=
                e =>
                {
                    if (IsDisposed)
                        return;


                    running = !running;
                    PlaySomething();
                };
            #endregion

            #region HasFocus
            var HasFocus = false;

            Native.window.onblur +=
               delegate
               {
                   HasFocus = false;

                   page_song.pause();
                   page_song2.pause();
               };

            Native.window.onfocus +=
                delegate
                {
                    HasFocus = true;
                    PlaySomething();
                };
            Native.Document.onmousemove +=
          delegate
          {
              if (HasFocus)
                  return;
              PlaySomething();
          };

            Native.Document.onmouseout +=
              delegate
              {
                  if (HasFocus)
                      return;

                  page_song.pause();
                  page_song2.pause();
              };
            #endregion


            #region helper
            Action<THREE.Object3D, f, f, f, f, f, f, int> helper =
                (o, x, y, z, w, h, d, c) =>
                {
                    //            function helper(o, x, y, z, w, h, d, c){
                    var material = new THREE.MeshLambertMaterial(new { color = c });
                    var geometry = new THREE.CubeGeometry(w, h, d, 1, 1, 1);
                    var mesh = new THREE.Mesh(geometry, material);
                    mesh.position.x = x + (w / 2);
                    mesh.position.y = y - (h / 2);
                    mesh.position.z = z + (d / 2);
                    o.add(mesh);
                };
            #endregion

            #region buildStar
            Action<THREE.Object3D, int> buildStar =
                (star, state) =>
                {
                    #region  dear JSC, please start supporting switch!
                    if (state == 0)
                    {
                        helper(star, 0, 0, 0, 1, 1, 1, 0xffffff);
                    }
                    else if (state == 1)
                    {
                        helper(star, 1, 0, 0, 1, 1, 1, 0xffffff);
                        helper(star, -1, 0, 0, 1, 1, 1, 0xffffff);
                        helper(star, 0, 1, 0, 1, 1, 1, 0xffffff);
                        helper(star, 0, -1, 0, 1, 1, 1, 0xffffff);
                    }
                    else if (state == 2)
                    {
                        helper(star, 1, 0, 0, 2, 1, 1, 0xffffff);
                        helper(star, -2, 0, 0, 2, 1, 1, 0xffffff);
                        helper(star, 0, 2, 0, 1, 2, 1, 0xffffff);
                        helper(star, 0, -1, 0, 1, 2, 1, 0xffffff);
                    }
                    else if (state == 3)
                    {
                        helper(star, 0, 0, 0, 1, 1, 1, 0xffffff);
                        helper(star, 2, 0, 0, 2, 1, 1, 0xffffff);
                        helper(star, -3, 0, 0, 2, 1, 1, 0xffffff);
                        helper(star, 0, 3, 0, 1, 2, 1, 0xffffff);
                        helper(star, 0, -2, 0, 1, 2, 1, 0xffffff);
                    }
                    else if (state == 4)
                    {
                        helper(star, 0, 3, 0, 1, 1, 1, 0xffffff);
                        helper(star, 2, 2, 0, 1, 1, 1, 0xffffff);
                        helper(star, 3, 0, 0, 1, 1, 1, 0xffffff);
                        helper(star, 2, -2, 0, 1, 1, 1, 0xffffff);
                        helper(star, 0, -3, 0, 1, 1, 1, 0xffffff);
                        helper(star, -2, -2, 0, 1, 1, 1, 0xffffff);
                        helper(star, -3, 0, 0, 1, 1, 1, 0xffffff);
                        helper(star, -2, 2, 0, 1, 1, 1, 0xffffff);
                    }
                    else if (state == 4)
                    {
                        helper(star, 2, 0, 0, 1, 1, 1, 0xffffff);
                        helper(star, -2, 0, 0, 1, 1, 1, 0xffffff);
                        helper(star, 0, 2, 0, 1, 1, 1, 0xffffff);
                        helper(star, 0, -2, 0, 1, 1, 1, 0xffffff);
                    }
                    #endregion
                };
            #endregion

            var r = new Random();
            Func<f> Math_random = () => r.NextFloat();

            var stars = new List<List<THREE.Object3D>>();


            #region  init
            var camera = new THREE.PerspectiveCamera(45,
            Native.window.Width / Native.window.Height, .1f, 10000);

            camera.position.z = 30;
            camera.position.x = 0;
            camera.position.y = 0;

            var scene = new THREE.Scene();
            scene.fog = new THREE.FogExp2(0x003366, 0.0095f);

            #region POPTART
            var poptart = new THREE.Object3D();

            //		object	   x    y    z    w    h    d	  color
            helper(poptart, 0, -2, -1, 21, 14, 3, 0x222222);
            helper(poptart, 1, -1, -1, 19, 16, 3, 0x222222);
            helper(poptart, 2, 0, -1, 17, 18, 3, 0x222222);

            helper(poptart, 1, -2, -1.5f, 19, 14, 4, 0xffcc99);
            helper(poptart, 2, -1, -1.5f, 17, 16, 4, 0xffcc99);

            helper(poptart, 2, -4, 2, 17, 10, .6f, 0xff99ff);
            helper(poptart, 3, -3, 2, 15, 12, .6f, 0xff99ff);
            helper(poptart, 4, -2, 2, 13, 14, .6f, 0xff99ff);

            helper(poptart, 4, -4, 2, 1, 1, .7f, 0xff3399);
            helper(poptart, 9, -3, 2, 1, 1, .7f, 0xff3399);
            helper(poptart, 12, -3, 2, 1, 1, .7f, 0xff3399);
            helper(poptart, 16, -5, 2, 1, 1, .7f, 0xff3399);
            helper(poptart, 8, -7, 2, 1, 1, .7f, 0xff3399);
            helper(poptart, 5, -9, 2, 1, 1, .7f, 0xff3399);
            helper(poptart, 9, -10, 2, 1, 1, .7f, 0xff3399);
            helper(poptart, 3, -11, 2, 1, 1, .7f, 0xff3399);
            helper(poptart, 7, -13, 2, 1, 1, .7f, 0xff3399);
            helper(poptart, 4, -14, 2, 1, 1, .7f, 0xff3399);

            poptart.position.x = -10.5f;
            poptart.position.y = 9;
            scene.add(poptart);
            #endregion

            #region FEET
            var feet = new THREE.Object3D();
            helper(feet, 0, -2, .49f, 3, 3, 1, 0x222222);
            helper(feet, 1, -1, .49f, 3, 3, 1, 0x222222);
            helper(feet, 1, -2, -.01f, 2, 2, 2, 0x999999);
            helper(feet, 2, -1, -.01f, 2, 2, 2, 0x999999);

            helper(feet, 6, -2, -.5f, 3, 3, 1, 0x222222);
            helper(feet, 6, -2, -.5f, 4, 2, 1, 0x222222);
            helper(feet, 7, -2, -.99f, 2, 2, 2, 0x999999);

            helper(feet, 16, -3, .49f, 3, 2, 1, 0x222222);
            helper(feet, 15, -2, .49f, 3, 2, 1, 0x222222);
            helper(feet, 15, -2, -.01f, 2, 1, 2, 0x999999);
            helper(feet, 16, -3, -.01f, 2, 1, 2, 0x999999);

            helper(feet, 21, -3, -.5f, 3, 2, 1, 0x222222);
            helper(feet, 20, -2, -.5f, 3, 2, 1, 0x222222);
            helper(feet, 20, -2, -.99f, 2, 1, 2, 0x999999);
            helper(feet, 21, -3, -.99f, 2, 1, 2, 0x999999);

            feet.position.x = -12.5f;
            feet.position.y = -6;
            scene.add(feet);
            #endregion


            #region TAIL
            var tail = new THREE.Object3D();
            helper(tail, 0, 0, -.25f, 4, 3, 1.5f, 0x222222);
            helper(tail, 1, -1, -.25f, 4, 3, 1.5f, 0x222222);
            helper(tail, 2, -2, -.25f, 4, 3, 1.5f, 0x222222);
            helper(tail, 3, -3, -.25f, 4, 3, 1.5f, 0x222222);
            helper(tail, 1, -1, -.5f, 2, 1, 2, 0x999999);
            helper(tail, 2, -2, -.5f, 2, 1, 2, 0x999999);
            helper(tail, 3, -3, -.5f, 2, 1, 2, 0x999999);
            helper(tail, 4, -4, -.5f, 2, 1, 2, 0x999999);

            tail.position.x = -16.5f;
            tail.position.y = 2;
            scene.add(tail);
            #endregion


            #region FACE
            var face = new THREE.Object3D();
            helper(face, 2, -3, -3, 12, 9, 4, 0x222222);
            helper(face, 0, -5, 0, 16, 5, 1, 0x222222);
            helper(face, 1, -1, 0, 4, 10, 1, 0x222222);
            helper(face, 11, -1, 0, 4, 10, 1, 0x222222);
            helper(face, 3, -11, 0, 10, 2, 1, 0x222222);
            helper(face, 2, 0, 0, 2, 2, 1, 0x222222);
            helper(face, 4, -2, 0, 2, 2, 1, 0x222222);
            helper(face, 12, 0, 0, 2, 2, 1, 0x222222);
            helper(face, 10, -2, 0, 2, 2, 1, 0x222222);

            helper(face, 1, -5, .5f, 14, 5, 1, 0x999999);
            helper(face, 3, -4, .5f, 10, 8, 1, 0x999999);
            helper(face, 2, -1, .5f, 2, 10, 1, 0x999999);
            helper(face, 12, -1, .5f, 2, 10, 1, 0x999999);
            helper(face, 4, -2, .5f, 1, 2, 1, 0x999999);
            helper(face, 5, -3, .5f, 1, 1, 1, 0x999999);
            helper(face, 11, -2, .5f, 1, 2, 1, 0x999999);
            helper(face, 10, -3, .5f, 1, 1, 1, 0x999999);
            //Eyes
            helper(face, 4, -6, .6f, 2, 2, 1, 0x222222);
            helper(face, 11, -6, .6f, 2, 2, 1, 0x222222);
            helper(face, 3.99f, -5.99f, .6f, 1.01f, 1.01f, 1.01f, 0xffffff);
            helper(face, 10.99f, -5.99f, .6f, 1.01f, 1.01f, 1.01f, 0xffffff);
            //MOUTH
            helper(face, 5, -10, .6f, 7, 1, 1, 0x222222);
            helper(face, 5, -9, .6f, 1, 2, 1, 0x222222);
            helper(face, 8, -9, .6f, 1, 2, 1, 0x222222);
            helper(face, 11, -9, .6f, 1, 2, 1, 0x222222);
            //CHEEKS
            helper(face, 2, -8, .6f, 2, 2, .91f, 0xff9999);
            helper(face, 13, -8, .6f, 2, 2, .91f, 0xff9999);

            face.position.x = -.5f;
            face.position.y = 4;
            face.position.z = 4;
            scene.add(face);
            #endregion

            #region RAINBOW
            var rainbow = new THREE.Object3D();
            for (var c = 0; c < numRainChunks - 1; c++)
            {
                var yOffset = 8;
                if (c % 2 == 1) yOffset = 7;
                var xOffset = (-c * 8) - 16.5f;
                helper(rainbow, xOffset, yOffset, 0, 8, 3, 1, 0xff0000);
                helper(rainbow, xOffset, yOffset - 3, 0, 8, 3, 1, 0xff9900);
                helper(rainbow, xOffset, yOffset - 6, 0, 8, 3, 1, 0xffff00);
                helper(rainbow, xOffset, yOffset - 9, 0, 8, 3, 1, 0x33ff00);
                helper(rainbow, xOffset, yOffset - 12, 0, 8, 3, 1, 0x0099ff);
                helper(rainbow, xOffset, yOffset - 15, 0, 8, 3, 1, 0x6633ff);
            }
            scene.add(rainbow);
            #endregion


            #region rainChunk
            var rainChunk = new THREE.Object3D();
            helper(rainChunk, -16.5f, 7, 0, 8, 3, 1, 0xff0000);
            helper(rainChunk, -16.5f, 4, 0, 8, 3, 1, 0xff9900);
            helper(rainChunk, -16.5f, 1, 0, 8, 3, 1, 0xffff00);
            helper(rainChunk, -16.5f, -2, 0, 8, 3, 1, 0x33ff00);
            helper(rainChunk, -16.5f, -5, 0, 8, 3, 1, 0x0099ff);
            helper(rainChunk, -16.5f, -8, 0, 8, 3, 1, 0x6633ff);
            rainChunk.position.x -= (8 * (numRainChunks - 1));
            scene.add(rainChunk);
            #endregion

            #region stars



            for (var state = 0; state < 6; state++)
            {

                stars.Add(new List<THREE.Object3D>());

                for (var c = 0; c < numStars; c++)
                {
                    var star = new THREE.Object3D();
                    star.position.x = Math_random() * 200 - 100;
                    star.position.y = Math_random() * 200 - 100;
                    star.position.z = Math_random() * 200 - 100;
                    buildStar(star, state);
                    scene.add(star);
                    stars[state].Add(star);
                }
            }
            #endregion


            var pointLight = new THREE.PointLight(0xFFFFFF);
            pointLight.position.z = 1000;
            scene.add(pointLight);



            #endregion




            #region IsDisposed

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                page_song.pause();
                page_song2.pause();

                container.Orphanize();
            };
            #endregion




            Native.window.onframe +=
                delegate
                {
                    f delta = clock.ElapsedMilliseconds * 0.001f;
                    clock.Restart();

                    if (running) deltaSum += delta;

                    if (deltaSum > .07)
                    {
                        deltaSum = deltaSum % .07f;
                        frame = (frame + 1) % 12;
                        for (var c = 0; c < numStars; c++)
                        {
                            var tempX = stars[5][c].position.x;
                            var tempY = stars[5][c].position.y;
                            var tempZ = stars[5][c].position.z;
                            for (var state = 5; state > 0; state--)
                            {
                                var star = stars[state][c];
                                var star2 = stars[state - 1][c];
                                star.position.x = star2.position.x - 8;
                                star.position.y = star2.position.y;
                                star.position.z = star2.position.z;

                                if (star.position.x < -100)
                                {
                                    star.position.x += 200;
                                    star.position.y = Math_random() * 200 - 100;
                                    star.position.z = Math_random() * 200 - 100;
                                }
                            }
                            stars[0][c].position.x = tempX;
                            stars[0][c].position.y = tempY;
                            stars[0][c].position.z = tempZ;
                        }

                        #region  dear JSC, please start supporting switch!

                        if (frame == 0)
                        {
                            face.position.x++;
                            feet.position.x++;
                        }
                        else if (frame == 1)
                        {
                            face.position.y--;
                            feet.position.x++;
                            feet.position.y--;
                            poptart.position.y--;
                            rainbow.position.x -= 9;
                            rainChunk.position.x += (8 * (numRainChunks - 1)) - 1;
                        }
                        else if (frame == 2)
                        {
                            feet.position.x--;
                        }
                        else if (frame == 3)
                        {
                            face.position.x--;
                            feet.position.x--;
                            rainbow.position.x += 9;
                            rainChunk.position.x -= (8 * (numRainChunks - 1)) - 1;

                        }
                        else if (frame == 4)
                        {
                            face.position.y++;
                        }
                        else if (frame == 5)
                        {
                            poptart.position.y++;
                            feet.position.y++;
                            rainbow.position.x -= 9;
                            rainChunk.position.x += (8 * (numRainChunks - 1)) - 1;
                        }
                        else if (frame == 6)
                        {

                            face.position.x++;
                            feet.position.x++;
                        }
                        else if (frame == 7)
                        {
                            poptart.position.y--;
                            face.position.y--;
                            feet.position.x++;
                            feet.position.y--;
                            rainbow.position.x += 9;
                            rainChunk.position.x -= (8 * (numRainChunks - 1)) - 1;
                        }
                        else if (frame == 8)
                        {
                            feet.position.x--;
                        }
                        else if (frame == 9)
                        {
                            face.position.x--;
                            feet.position.x--;
                            rainbow.position.x -= 9;
                            rainChunk.position.x += (8 * (numRainChunks - 1)) - 1;
                        }
                        else if (frame == 10)
                        {
                            face.position.y++;
                        }
                        else if (frame == 11)
                        {
                            poptart.position.y++;
                            feet.position.y++;
                            rainbow.position.x += 9;
                            rainChunk.position.x -= (8 * (numRainChunks - 1)) - 1;
                        }
                        #endregion

                    }
                    camera.position.x += (mouseX - camera.position.x) * .005f;
                    camera.position.y += (-mouseY - camera.position.y) * .005f;
                    camera.lookAt(scene.position);
                    renderer.render(scene, camera);
                };





            #region AtResize
            Action AtResize = delegate
            {
                container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);

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

            #region requestFullscreen
            Native.document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    Native.document.body.requestFullscreen();


                };
            #endregion


        }

        bool IsDisposed = false;

        public Action Dispose;

    }
}
