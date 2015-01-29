using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using WebGLDynamicTerrainTemplate.HTML.Pages;
using WebGLDynamicTerrainTemplate.Design;
using THREE = WebGLDynamicTerrainTemplate.Design.THREE;

namespace WebGLDynamicTerrainTemplate
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using WebGLDynamicTerrainTemplate.HTML.Audio.FromAssets;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // inspired by http://alteredqualia.com/three/examples/webgl_terrain_dynamic.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            var location = "" + Native.document.location;

#if false
            #region workaround for ThreeJS/chrome webgl upscale bug
            // workaround for not knowing how to tell three js to upscale correctly..
            // X:\jsc.svn\examples\javascript\Test\TestNestedIFrameForMoreWidth\TestNestedIFrameForMoreWidth\Application.cs
            // instead of reloading full app
            // could we run part of it instead?
            // like let jsc know that this sub application should be reloadable?
            // this will be like threading
            // the outer code wil just stop doing anything
            // and the inner app will take over.


            if (Native.window.Width < Native.screen.width)
            {
            #region make sure the url looks different to make iframe actually load
                Native.window.parent.With(
                    parent =>
                    {
                        // http://stackoverflow.com/questions/5934538/is-there-a-limitation-on-an-iframe-containing-another-iframe-with-the-same-url

                        var parentlocation = "";

                        try
                        {
                            parentlocation = parent.document.location.href;
                            Console.WriteLine(new { parentlocation });
                        }
                        catch
                        {
                            // we are sandboxed!
                        }

                        if (parentlocation.TakeUntilIfAny("#") == location.TakeUntilIfAny("#"))
                        {
                            var withouthash = location.TakeUntilIfAny("#");
                            var onlyhash = location.SkipUntilOrEmpty("#");

                            withouthash += "?";

                            if (onlyhash != "")
                            {
                                withouthash += "#" + onlyhash;
                            }

                            location = withouthash;
                        }
                    }
                );
            #endregion



                // this check only looks for default screen width
                // what about height and secondary screens?
                Console.WriteLine("will prepare... " + location);

                var iframe = new IHTMLIFrame
                {
                    frameBorder = "0",
                    allowFullScreen = true
                };

                iframe.style.minWidth = Native.screen.width + "px";
                iframe.style.minHeight = Native.screen.height + "px";

                iframe.style.position = IStyle.PositionEnum.absolute;
                iframe.style.left = "0px";
                iframe.style.top = "0px";
                iframe.style.width = "100%";
                iframe.style.height = "100%";

                Native.document.body.Clear();
                Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;

                Native.window.onmessage +=
                   e =>
                   {
                       Console.WriteLine("Native.Window.onmessage " + new { e.data });

                       // pure trickery :P
                       if ((string)e.data == "WebGLDynamicTerrainTemplate.loaded")
                       {
                           iframe.style.minWidth = "";
                           iframe.style.minHeight = "";
                       }
                   };

                iframe.onload +=
                    delegate
                    {
                        if (iframe.src != location)
                            return;

                        Native.window.requestAnimationFrame +=
                          delegate
                          {
                              Console.WriteLine("reload done! " + new { location, iframe.src });
                              iframe.contentWindow.postMessage("ready yet?");
                          };
                    };

                Native.window.requestAnimationFrame +=
                    delegate
                    {
                        Console.WriteLine("will reload... " + location);
                        iframe.AttachToDocument();
                        iframe.src = location;
                    };

                return;
            }




            #endregion

#endif


            #region await Three.js then do InitializeContent
            new[]
            {
                new global::WebGLDynamicTerrainTemplate.Design.ThreeTerrain().Content,

                new global::WebGLDynamicTerrainTemplate.Design.ShaderTerrain().Content,
                new global::WebGLDynamicTerrainTemplate.Design.ShaderExtrasTerrain().Content,
                new global::WebGLDynamicTerrainTemplate.Design.PostprocessingTerrain().Content,
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


            style.Content.AttachToHead();

        }

        sealed class MyModelGeometryColorMap
        {
            public object[] colors;
        }

        sealed class MyModelGeometryFace
        {
            public object color;
        }


        sealed class MyModelGeometry
        {
            public MyModelGeometryColorMap[] morphColors;
            public MyModelGeometryFace[] faces;
        }



        sealed class MyUniformsNoise
        {
            public THREE.ShaderExtrasModuleItem_uniforms_item time;
            public THREE.ShaderExtrasModuleItem_uniforms_item scale;
            public THREE.ShaderExtrasModuleItem_uniforms_item offset;
        }



        void InitializeContent(IDefault page = null)
        {
            #region make sure we atleast have our invisible DOM
            if (page == null)
                page = new HTML.Pages.Default();
            #endregion

            #region container
            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            var container = new IHTMLDiv();

            container.AttachToDocument();
            container.style.backgroundColor = "#000000";
            container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);
            #endregion



            #region code port

            double SCREEN_WIDTH = Native.window.Width ;
            double  SCREEN_HEIGHT = Native.window.Height;

            var animDelta = 0;
            var animDeltaDir = -1;
            var lightVal = 0;
            var lightDir = 1;
            var soundVal = 0;
            var oldSoundVal = 0;
            var soundDir = 1;

            #region animate it
            animDeltaDir *= -1;
            #endregion


            var clock = new THREE.Clock();

            var morphs = new List<THREE.MorphAnimMesh>();

            var updateNoise = true;


            var mlib = new Dictionary<string, THREE.ShaderMaterial>();

            var soundtrack = new Five_Armies { loop = true, volume = 0.9 };


            #region HasFocus
            var HasFocus = false;

            Native.window.onblur +=
               delegate
               {
                   HasFocus = false;

                   soundtrack.volume = 0.1;
               };

            Native.window.onfocus +=
                delegate
                {
                    HasFocus = true;
                    soundtrack.volume = 0.9;
                };
            //  Native.Document.onmousemove +=
            //delegate
            //{
            //    if (HasFocus)
            //        return;
            //    soundtrack.play();
            //};

            //  Native.Document.onmouseout +=
            //    delegate
            //    {
            //        if (HasFocus)
            //            return;

            //        soundtrack.pause();
            //    };
            #endregion


            var THREE_RepeatWrapping = 0;
            var THREE_FaceColors = 1;
            var THREE_LinearFilter = 6;
            var THREE_RGBFormat = 17;
            var THREE_LinearMipMapLinearFilter = 8;


            #region SCENE (RENDER TARGET)

            var sceneRenderTarget = new THREE.Scene();

            var cameraOrtho = new THREE.OrthographicCamera(
                (int)SCREEN_WIDTH / -2,
                (int)SCREEN_WIDTH / 2,
                (int)SCREEN_HEIGHT / 2,
                (int)SCREEN_HEIGHT / -2,
                -10000,
                10000
            );

            cameraOrtho.position.z = 100;

            sceneRenderTarget.add(cameraOrtho);
            #endregion

            #region SCENE (FINAL)

            var scene = new THREE.Scene();

            scene.fog = new THREE.Fog(0x050505, 2000, 4000);
            scene.fog.color.setHSV(0.102, 0.9, 0.825);

            var camera = new THREE.PerspectiveCamera(40, (int)Native.window.aspect, 2, 4000);
            camera.position.set(-1200, 800, 1200);

            scene.add(camera);

            var controls = new THREE.TrackballControls(camera);
            controls.target.set(0, 0, 0);

            controls.rotateSpeed = 1.0;
            controls.zoomSpeed = 1.2;
            controls.panSpeed = 0.8;

            controls.noZoom = false;
            controls.noPan = false;

            controls.staticMoving = false;
            controls.dynamicDampingFactor = 0.15;

            controls.keys = new[] { 65, 83, 68 };
            #endregion

            #region LIGHTS

            scene.add(new THREE.AmbientLight(0x111111));

            var spotLight = new THREE.SpotLight(0xffffff, 1.15);
            spotLight.position.set(500, 2000, 0);
            spotLight.castShadow = true;
            scene.add(spotLight);

            var pointLight = new THREE.PointLight(0xff4400, 1.5);
            pointLight.position.set(0, 0, 0);
            scene.add(pointLight);

            #endregion



            #region HEIGHT + NORMAL MAPS

            var normalShader = __THREE.ShaderExtras.normalmap;

            var rx = 256;
            var ry = 256;

            var pars = new THREE.WebGLRenderTargetArguments
            {
                minFilter = THREE_LinearMipMapLinearFilter,
                magFilter = THREE_LinearFilter,
                format = THREE_RGBFormat
            };

            var heightMap = new THREE.WebGLRenderTarget(rx, ry, pars);
            var normalMap = new THREE.WebGLRenderTarget(rx, ry, pars);

            var uniformsNoise = new MyUniformsNoise
            {
                time = new THREE.ShaderExtrasModuleItem_uniforms_item { type = "f", value = 1.0 },
                scale = new THREE.ShaderExtrasModuleItem_uniforms_item { type = "v2", value = new THREE.Vector2(1.5, 1.5) },
                offset = new THREE.ShaderExtrasModuleItem_uniforms_item { type = "v2", value = new THREE.Vector2(0, 0) }
            };

            var uniformsNormal = __THREE.UniformsUtils.clone(normalShader.uniforms);

            uniformsNormal.height.value = 0.05;
            ((THREE.Vector2)uniformsNormal.resolution.value).set(rx, ry);
            uniformsNormal.heightMap.texture = heightMap;

            var vertexShader = new Shaders.NoiseVertexShader().ToString();
            #endregion


            #region before TEXTURES
            var textureCounter = 0;


            #region RENDERER

            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(Native.window.Width, Native.window.Height);
            renderer.setClearColor(scene.fog.color, 1);

            renderer.domElement.style.position = IStyle.PositionEnum.absolute;
            renderer.domElement.style.top = "0px";
            renderer.domElement.style.left = "0px";

            container.appendChild(renderer.domElement);

            //    //

            renderer.gammaInput = true;
            renderer.gammaOutput = true;
            #endregion

            #region applyShader
            Action<THREE.ShaderExtrasModuleItem, object, object> applyShader = (shader, texture, target) =>
            {

                var shaderMaterial = new THREE.ShaderMaterial(
                    new THREE.ShaderMaterialArguments
                    {

                        fragmentShader = shader.fragmentShader,
                        vertexShader = shader.vertexShader,
                        uniforms = __THREE.UniformsUtils.clone(shader.uniforms)

                    }
                );

                shaderMaterial.uniforms.tDiffuse.texture = texture;

                var sceneTmp = new THREE.Scene();

                var meshTmp = new THREE.Mesh(new THREE.PlaneGeometry(Native.window.Width, Native.window.Height), shaderMaterial);
                meshTmp.position.z = -500;
                sceneTmp.add(meshTmp);

                renderer.render(sceneTmp, cameraOrtho, target, true);
            };
            #endregion


            var terrain = default(THREE.Mesh);

            #region loadTextures
            Action loadTextures = () =>
            {

                textureCounter += 1;

                if (textureCounter == 3)
                {

                    terrain.visible = true;

                    //document.getElementById("loading").style.display = "none";

                }

            };

            ////
            #endregion

            #endregion

            #region TEXTURES

            var specularMap = new THREE.WebGLRenderTarget(2048, 2048, pars);


            var diffuseTexture1 = default(THREE.WebGLRenderTarget);

            diffuseTexture1 = __THREE.ImageUtils.loadTexture(
                new global::WebGLDynamicTerrainTemplate.HTML.Images.FromAssets.grasslight_big().src,
                null,
                IFunction.Of(
                    delegate ()
                    {
                        loadTextures();
                        applyShader(__THREE.ShaderExtras.luminosity, diffuseTexture1, specularMap);
                    }
                )
            );

            var diffuseTexture2 = __THREE.ImageUtils.loadTexture(
                   new global::WebGLDynamicTerrainTemplate.HTML.Images.FromAssets.backgrounddetailed6().src,
                   null,
                   IFunction.Of(
                       delegate ()
                       {
                           loadTextures();
                       }
                   )
               );

            var detailTexture = __THREE.ImageUtils.loadTexture(
              new global::WebGLDynamicTerrainTemplate.HTML.Images.FromAssets.grasslight_big_nm().src,
              null,
              IFunction.Of(
                  delegate ()
                  {
                      loadTextures();
                  }
              )
          );

            diffuseTexture1.wrapS = THREE_RepeatWrapping;
            diffuseTexture1.wrapT = THREE_RepeatWrapping;


            diffuseTexture2.wrapS = THREE_RepeatWrapping;
            diffuseTexture2.wrapT = THREE_RepeatWrapping;

            detailTexture.wrapS = THREE_RepeatWrapping;
            detailTexture.wrapT = THREE_RepeatWrapping;

            specularMap.wrapS = THREE_RepeatWrapping;
            specularMap.wrapT = THREE_RepeatWrapping;
            #endregion

            #region TERRAIN SHADER

            var terrainShader = __THREE.ShaderTerrain.terrain;

            var uniformsTerrain = __THREE.UniformsUtils.clone(terrainShader.uniforms);

            uniformsTerrain.tNormal.texture = normalMap;
            uniformsTerrain.uNormalScale.value = 3.5;

            uniformsTerrain.tDisplacement.texture = heightMap;

            uniformsTerrain.tDiffuse1.texture = diffuseTexture1;
            uniformsTerrain.tDiffuse2.texture = diffuseTexture2;
            uniformsTerrain.tSpecular.texture = specularMap;
            uniformsTerrain.tDetail.texture = detailTexture;

            uniformsTerrain.enableDiffuse1.value = true;
            uniformsTerrain.enableDiffuse2.value = true;
            uniformsTerrain.enableSpecular.value = true;

            ((THREE.Color)uniformsTerrain.uDiffuseColor.value).setHex(0xffffff);
            ((THREE.Color)uniformsTerrain.uSpecularColor.value).setHex(0xffffff);
            ((THREE.Color)uniformsTerrain.uAmbientColor.value).setHex(0x111111);

            uniformsTerrain.uShininess.value = 30;
            uniformsTerrain.uDisplacementScale.value = 375;

            ((THREE.Vector2)uniformsTerrain.uRepeatOverlay.value).set(6, 6);


            var _params = new[] {
                new { id= "heightmap", fragmentShader= new Shaders.NoiseFragmentShader().ToString(), vertexShader=  vertexShader, uniforms= (object)uniformsNoise, lights = false },
                new { id="normal", fragmentShader=  normalShader.fragmentShader,  vertexShader=normalShader.vertexShader, uniforms=(object)uniformsNormal, lights = false },
                new { id="terrain", fragmentShader= terrainShader.fragmentShader, vertexShader=terrainShader.vertexShader, uniforms=(object)uniformsTerrain, lights = true }
            };

            for (var i = 0; i < _params.Length; i++)
            {

                var material = new THREE.ShaderMaterial(
                    new THREE.ShaderMaterialArguments
                    {

                        uniforms = (THREE.ShaderExtrasModuleItem_uniforms)_params[i].uniforms,
                        vertexShader = _params[i].vertexShader,
                        fragmentShader = _params[i].fragmentShader,
                        lights = _params[i].lights,
                        fog = true
                    }
                );

                mlib[(string)_params[i].id] = material;

            }


            var plane = new THREE.PlaneGeometry(Native.window.Width, Native.window.Height);

            var quadTarget = new THREE.Mesh(
                plane,
                new THREE.MeshBasicMaterial(
                    new THREE.MeshBasicMaterialArguments { color = 0xff0000 }
                    )
                    );

            quadTarget.position.z = -500;
            sceneRenderTarget.addObject(quadTarget);
            #endregion

            #region TERRAIN MESH

            var geometryTerrain = new THREE.PlaneGeometry(6000, 6000, 256, 256);
            geometryTerrain.computeFaceNormals();
            geometryTerrain.computeVertexNormals();
            geometryTerrain.computeTangents();

            terrain = new THREE.Mesh(geometryTerrain, mlib["terrain"]);
            terrain.rotation.set(-Math.PI / 2.0, 0, 0);
            terrain.position.set(0, -125, 0);
            terrain.visible = false;
            scene.add(terrain);
            #endregion






            #region COMPOSER

            renderer.autoClear = false;



            var renderTargetParameters = new THREE.WebGLRenderTargetArguments
            {
                minFilter = THREE_LinearFilter,
                magFilter = THREE_LinearFilter,
                format = THREE_RGBFormat,
                stencilBufer = false
            };

            var renderTarget = new THREE.WebGLRenderTarget((int)SCREEN_WIDTH, (int)SCREEN_HEIGHT, renderTargetParameters);

            var effectBloom = new THREE.BloomPass(0.6);
            var effectBleach = new THREE.ShaderPass(__THREE.ShaderExtras.bleachbypass);

            var hblur = new THREE.ShaderPass(__THREE.ShaderExtras.horizontalTiltShift);
            var vblur = new THREE.ShaderPass(__THREE.ShaderExtras.verticalTiltShift);

            var bluriness = 6;

            hblur.uniforms.h.value = bluriness / SCREEN_WIDTH;
            vblur.uniforms.v.value = bluriness / SCREEN_HEIGHT;

            hblur.uniforms.r.value = 0.5;
            vblur.uniforms.r.value = 0.5;

            effectBleach.uniforms.opacity.value = 0.65;

            var composer0 = new THREE.EffectComposer(renderer, renderTarget);

            var renderModel = new THREE.RenderPass(scene, camera);

            vblur.renderToScreen = true;

            var composer = new THREE.EffectComposer(renderer, renderTarget);

            composer.addPass(renderModel);

            composer.addPass(effectBloom);
            //composer.addPass( effectBleach );

            composer.addPass(hblur);
            composer.addPass(vblur);
            #endregion

            var r = new Random();

            Func<f> Math_random = () => (f)r.NextDouble();



            #region addMorph

            Action<MyModelGeometry, f, f, f, f, f> addMorph = (geometry, speed, duration, x, y, z) =>
            {

                var material = new THREE.MeshLambertMaterial(
                    new THREE.MeshLambertMaterialArguments
                    {
                        color = 0xffaa55,
                        morphTargets = true,
                        vertexColors = THREE_FaceColors
                    }
                );


                var meshAnim = new THREE.MorphAnimMesh(geometry, material);

                meshAnim.speed = speed;
                meshAnim.duration = duration;
                meshAnim.time = 600.0 * Math_random();

                meshAnim.position.set(x, y, z);
                meshAnim.rotation.y = (f)(Math.PI / 2f);

                meshAnim.castShadow = true;
                meshAnim.receiveShadow = false;

                scene.add(meshAnim);

                morphs.Add(meshAnim);

                renderer.initWebGLObjects(scene);

            };
            #endregion

            #region morphColorsToFaceColors

            Action<MyModelGeometry> morphColorsToFaceColors = (geometry) =>
            {

                if (geometry.morphColors != null)
                    if (geometry.morphColors.Length > 0)
                    {

                        var colorMap = geometry.morphColors[0];

                        for (var i = 0; i < colorMap.colors.Length; i++)
                        {

                            geometry.faces[i].color = colorMap.colors[i];

                        }

                    }

            };
            #endregion

            #region Models
            var loader = new THREE.JSONLoader();

            var startX = -3000;


            loader.load(
                new Models.parrot().Content.src,

                IFunction.OfDelegate(
                    new Action<MyModelGeometry>(
                        geometry =>
                        {
                            morphColorsToFaceColors(geometry);

                            addMorph(geometry, 250, 500, startX - 500, 500, 700);
                            addMorph(geometry, 250, 500, startX - Math_random() * 500, 500, -200);
                            addMorph(geometry, 250, 500, startX - Math_random() * 500, 500, 200);
                            addMorph(geometry, 250, 500, startX - Math_random() * 500, 500, 1000);
                        }
                    )
                )
            );

            loader.load(
                new Models.flamingo().Content.src,
                IFunction.OfDelegate(
                    new Action<MyModelGeometry>(
                        geometry =>
                        {
                            morphColorsToFaceColors(geometry);
                            addMorph(geometry, 500, 1000, startX - Math_random() * 500, 350, 40);
                        }
                    )
                )
            );


            loader.load(
                new Models.stork().Content.src,
                IFunction.OfDelegate(
                        new Action<MyModelGeometry>(
                            geometry =>
                            {
                                morphColorsToFaceColors(geometry);
                                addMorph(geometry, 350, 1000, startX - Math_random() * 500, 350, 340);
                            }
                        )
                    )
            );
            #endregion



            #region PRE-INIT

            renderer.initWebGLObjects(scene);
            #endregion


            #region onkeydown
            Native.document.body.onkeydown +=
                 (e) =>
                 {

                     if (e.KeyCode == 78) lightDir *= -1;
                     if (e.KeyCode == 77) animDeltaDir *= -1;
                     if (e.KeyCode == 66) soundDir *= -1;

                 };
            #endregion



            Action __loaded = null;

            #region event Action loaded;

            Native.window.parent.With(
                parent =>
                {
                    __loaded = delegate
                    {
                        __loaded = null;
                        parent.postMessage("WebGLDynamicTerrainTemplate.loaded");
                    };
                }
            );
            #endregion


            #region render
            Action render = () =>
            {

                var delta = clock.getDelta();

                soundVal = __THREE.Math.clamp(soundVal + delta * soundDir, 0, 1);

                if (soundVal != oldSoundVal)
                {

                    if (soundtrack != null)
                    {

                        soundtrack.volume = soundVal;
                        oldSoundVal = soundVal;

                    }

                }

                //Native.Document.title = "textureCounter " + textureCounter;


                if (terrain.visible)
                {

                    controls.update();

                    var Date_now = new IDate().getTime();
                    var time = Date_now * 0.001;

                    var fLow = 0.4;
                    var fHigh = 0.825;

                    lightVal = __THREE.Math.clamp(lightVal + 0.5 * delta * lightDir, fLow, fHigh);

                    var valNorm = (lightVal - fLow) / (fHigh - fLow);

                    var sat = __THREE.Math.mapLinear(valNorm, 0, 1, 0.95, 0.25);
                    scene.fog.color.setHSV(0.1, sat, lightVal);

                    renderer.setClearColor(scene.fog.color, 1);

                    spotLight.intensity = __THREE.Math.mapLinear(valNorm, 0, 1, 0.1, 1.15);
                    pointLight.intensity = __THREE.Math.mapLinear(valNorm, 0, 1, 0.9, 1.5);

                    uniformsTerrain.uNormalScale.value = __THREE.Math.mapLinear(valNorm, 0, 1, 0.6, 3.5);

                    if (updateNoise)
                    {

                        animDelta = __THREE.Math.clamp(animDelta + 0.00075 * animDeltaDir, 0, 0.05);
                        uniformsNoise.time.value = ((f)uniformsNoise.time.value) + delta * animDelta;

                        var uniformsNoise_offset_value = (THREE.Vector3)uniformsNoise.offset.value;
                        uniformsNoise_offset_value.x += delta * 0.05f;

                        var uniformsTerrain_uOffset_value = (THREE.Vector3)uniformsTerrain.uOffset.value;
                        uniformsTerrain_uOffset_value.x = 4.0f * uniformsNoise_offset_value.x;

                        quadTarget.material = mlib["heightmap"];
                        renderer.render(sceneRenderTarget, cameraOrtho, heightMap, true);

                        quadTarget.material = mlib["normal"];
                        renderer.render(sceneRenderTarget, cameraOrtho, normalMap, true);

                        //updateNoise = false;

                    }


                    for (var i = 0; i < morphs.Count; i++)
                    {

                        var morph = morphs[i];

                        morph.updateAnimation(1000 * delta);

                        morph.position.x += morph.speed * delta;

                        if (morph.position.x > 2000)
                        {

                            morph.position.x = -1500 - Math_random() * 500;

                        }


                    }

                    //renderer.render( scene, camera );
                    composer.render(0.1);


                    if (__loaded != null)
                        __loaded();
                }
                else
                {
                }

            };
            #endregion





            Native.window.onframe +=
                delegate
                {
                    if (IsDisposed)
                        return;


                    render();


                };




            #endregion



            #region IsDisposed

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                //page.song.pause();
                soundtrack.pause();

                container.Orphanize();
            };
            #endregion






            #region AtResize
            Action AtResize = delegate
            {
                container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);

                renderer.setSize(Native.window.Width, Native.window.Height);

                camera.aspect = Native.window.Width / Native.window.Height;
                camera.updateProjectionMatrix();
            };

            Native.window.onresize +=
                delegate
                {
                    AtResize();
                };

            AtResize();
            #endregion


            Native.document.body.onmousedown +=
                e =>
                {
                    if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        Native.document.body.requestFullscreen();
                    }
                };

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

        bool IsDisposed = false;

        public Action Dispose;

    }
}
