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
        public Application(IDefaultPage page = null)
        {
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
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
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

        sealed class MyUniformsNoiseItem
        {
            public object value;
            public string type;
        }

        sealed class MyUniformsNoise
        {
            public MyUniformsNoiseItem time;
            public MyUniformsNoiseItem scale;
            public MyUniformsNoiseItem offset;
        }



        void InitializeContent(IDefaultPage page = null)
        {
            #region make sure we atleast have our invisible DOM
            if (page == null)
                page = new HTML.Pages.DefaultPage();
            #endregion

            #region container
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            var container = new IHTMLDiv();

            container.AttachToDocument();
            container.style.backgroundColor = "#003366";
            container.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);
            #endregion



            #region code port

            var SCREEN_WIDTH = Native.Window.Width;
            var SCREEN_HEIGHT = Native.Window.Height /*- 2 * MARGIN*/;

            //var renderer, container, stats;

            //var camera, scene;
            //var cameraOrtho, sceneRenderTarget;

            //var uniformsNoise, uniformsNormal,
            //    heightMap, normalMap,
            //    quadTarget;

            //var spotLight, pointLight;

            //var terrain;
            var terrain_visible = false;

            var textureCounter = 0;

            var animDelta = 0;
            var animDeltaDir = -1;
            var lightVal = 0;
            var lightDir = 1;
            var soundVal = 0;
            var oldSoundVal = 0;
            var soundDir = 1;

            var clock = new THREE.Clock();

            //var morph, 
            var morphs = new List<THREE.MorphAnimMesh>();

            var updateNoise = true;

            var animateTerrain = false;

            //var textMesh1;

            var mlib = new Dictionary<string, THREE.ShaderMaterial>();


            //    container = document.getElementById( 'container' );

            var soundtrack = page.soundtrack;

            #region constants
            var THREE_RepeatWrapping = 0;
            var THREE_LinearFilter = 6;
            var THREE_RGBFormat = 17;
            var THREE_LinearMipMapLinearFilter = 8;
            #endregion


            #region SCENE (RENDER TARGET)

            var sceneRenderTarget = new THREE.Scene();

            var cameraOrtho = new THREE.OrthographicCamera(SCREEN_WIDTH / -2, SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2, SCREEN_HEIGHT / -2, -10000, 10000);
            cameraOrtho.position.z = 100;

            sceneRenderTarget.add(cameraOrtho);
            #endregion

            #region SCENE (FINAL)

            var scene = new THREE.Scene();

            scene.fog = new THREE.Fog(0x050505, 2000, 4000);
            scene.fog.color.setHSV(0.102, 0.9, 0.825);

            var camera = new THREE.PerspectiveCamera(40, SCREEN_WIDTH / SCREEN_HEIGHT, 2, 4000);
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
                time = new MyUniformsNoiseItem { type = "f", value = 1.0 },
                scale = new MyUniformsNoiseItem { type = "v2", value = new THREE.Vector2(1.5, 1.5) },
                offset = new MyUniformsNoiseItem { type = "v2", value = new THREE.Vector2(0, 0) }
            };

            var uniformsNormal = __THREE.UniformsUtils.clone(normalShader.uniforms);

            uniformsNormal.height.value = 0.05;
            ((THREE.Vector2)uniformsNormal.resolution.value).set(rx, ry);
            uniformsNormal.heightMap.texture = heightMap;

            var vertexShader = new Shaders.NoiseVertexShader().ToString();
            #endregion



            #region loadTextures
            Action loadTextures = () =>
            {

                textureCounter += 1;

                if (textureCounter == 3)
                {

                    terrain_visible = true;

                    //document.getElementById("loading").style.display = "none";

                }

            };

            ////
            #endregion

            #region RENDERER

            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(SCREEN_WIDTH, SCREEN_HEIGHT);
            renderer.setClearColor(scene.fog.color, 1);

            //    renderer.domElement.style.position = "absolute";
            //    renderer.domElement.style.top = MARGIN + "px";
            //    renderer.domElement.style.left = "0px";

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

                var meshTmp = new THREE.Mesh(new THREE.PlaneGeometry(SCREEN_WIDTH, SCREEN_HEIGHT), shaderMaterial);
                meshTmp.position.z = -500;
                sceneTmp.add(meshTmp);

                renderer.render(sceneTmp, cameraOrtho, target, true);
            };
            #endregion




            #region TEXTURES

            var specularMap = new THREE.WebGLRenderTarget(2048, 2048, pars);


            var diffuseTexture1 = default(THREE.WebGLRenderTarget);

            diffuseTexture1 = __THREE.ImageUtils.loadTexture(
                new global::WebGLDynamicTerrainTemplate.HTML.Images.FromAssets.grasslight_big().src,
                null,
                IFunction.Of(
                    delegate()
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
                       delegate()
                       {
                           loadTextures();
                       }
                   )
               );

            var detailTexture = __THREE.ImageUtils.loadTexture(
              new global::WebGLDynamicTerrainTemplate.HTML.Images.FromAssets.grasslight_big_nm().src,
              null,
              IFunction.Of(
                  delegate()
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

            //    uniformsTerrain[ "uDiffuseColor" ].value.setHex( 0xffffff );
            //    uniformsTerrain[ "uSpecularColor" ].value.setHex( 0xffffff );
            //    uniformsTerrain[ "uAmbientColor" ].value.setHex( 0x111111 );

            uniformsTerrain.uShininess.value = 30;
            uniformsTerrain.uDisplacementScale.value = 375;

            //    uniformsTerrain[ "uRepeatOverlay" ].value.set( 6, 6 );

            var _params = new[] {
                new object []{ "heightmap", new Shaders.NoiseFragmentShader().ToString(), 	vertexShader, uniformsNoise, false },
                new object []{ "normal", 	normalShader.fragmentShader,  normalShader.vertexShader, uniformsNormal, false },
                new object []{ "terrain", 	terrainShader.fragmentShader, terrainShader.vertexShader, uniformsTerrain, true }
            };

            for (var i = 0; i < _params.Length; i++)
            {

                var material = new THREE.ShaderMaterial(new THREE.ShaderMaterialArguments
                {

                    uniforms = (THREE.ShaderExtrasModuleItem_uniforms)_params[i][3],
                    vertexShader = _params[i][2],
                    fragmentShader = _params[i][1],
                    lights = _params[i][4],
                    fog = true
                }
                );

                mlib[(string)_params[i][0]] = material;

            }


            var plane = new THREE.PlaneGeometry(SCREEN_WIDTH, SCREEN_HEIGHT);

            var quadTarget = new THREE.Mesh(plane, new THREE.MeshBasicMaterial(new THREE.MeshBasicMaterialArguments { color = 0xff0000 }));
            quadTarget.position.z = -500;
            sceneRenderTarget.addObject(quadTarget);
            #endregion

            #region TERRAIN MESH

            var geometryTerrain = new THREE.PlaneGeometry(6000, 6000, 256, 256);
            geometryTerrain.computeFaceNormals();
            geometryTerrain.computeVertexNormals();
            geometryTerrain.computeTangents();

            var terrain = new THREE.Mesh(geometryTerrain, mlib["terrain"]);
            terrain.rotation.set(-Math.PI / 2, 0, 0);
            terrain.position.set(0, -125, 0);
            terrain.visible = false;
            scene.add(terrain);
            #endregion




            #region STATS

            //    stats = new Stats();
            //    stats.domElement.style.position = 'absolute';
            //    stats.domElement.style.top = '0px';
            //    container.appendChild( stats.domElement );

            //    stats.domElement.children[ 0 ].children[ 0 ].style.color = "#aaa";
            //    stats.domElement.children[ 0 ].style.background = "transparent";
            //    stats.domElement.children[ 0 ].children[ 1 ].style.display = "none";
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

            var renderTarget = new THREE.WebGLRenderTarget(SCREEN_WIDTH, SCREEN_HEIGHT, renderTargetParameters);

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

            //    composer = new THREE.EffectComposer( renderer, renderTarget );

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

            var THREE_FaceColors = 1;


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
                meshAnim.time = 600 * Math_random();

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
                new THREE.JSONLoaderArguments
                {
                    model = new Models.parrot().Content.src,
                    callback = IFunction.OfDelegate(
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
                }
            );

            loader.load(
                new THREE.JSONLoaderArguments
                {
                    model = new Models.flamingo().Content.src,
                    callback = IFunction.OfDelegate(
                        new Action<MyModelGeometry>(
                            geometry =>
                            {
                                morphColorsToFaceColors(geometry);
                                addMorph(geometry, 500, 1000, startX - Math_random() * 500, 350, 40);
                            }
                        )
                    )
                }
            );


            loader.load(
                   new THREE.JSONLoaderArguments
                   {
                       model = new Models.stork().Content.src,
                       callback = IFunction.OfDelegate(
                           new Action<MyModelGeometry>(
                               geometry =>
                               {
                                   morphColorsToFaceColors(geometry);
                                   addMorph(geometry, 350, 1000, startX - Math_random() * 500, 350, 340);
                               }
                           )
                       )
                   }
               );
            #endregion



            #region PRE-INIT

            renderer.initWebGLObjects(scene);
            #endregion


            #region onkeydown
            Native.Document.onkeydown +=
                 (e) =>
                 {

                     switch (e.KeyCode)
                     {

                         case 78: /*N*/  lightDir *= -1; break;
                         case 77: /*M*/  animDeltaDir *= -1; break;
                         case 66: /*B*/  soundDir *= -1; break;

                     }

                 };
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

                    if ( terrain.visible ) {

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
                            uniformsNoise_offset_value.x +=  delta * 0.05f;

                            var uniformsTerrain_uOffset_value = (THREE.Vector3)uniformsTerrain.uOffset.value;
                            uniformsTerrain_uOffset_value.x = 4 * uniformsNoise_offset_value.x;

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

                    }

            };
            #endregion


            #region animate
            //function animate() {

            //    requestAnimationFrame( animate );

            //    render();
            //    stats.update();

            //}
            #endregion


            //animate();


            #endregion



            #region IsDisposed

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                //page.song.pause();
                page.soundtrack.pause();

                container.Orphanize();
            };
            #endregion






            #region AtResize
            Action AtResize = delegate
            {
                container.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);

                renderer.setSize(Native.Window.Width, Native.Window.Height);

                camera.aspect = Native.Window.Width / Native.Window.Height;
                camera.updateProjectionMatrix();
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


                };
            #endregion


        }

        bool IsDisposed = false;

        public Action Dispose;

    }
}
