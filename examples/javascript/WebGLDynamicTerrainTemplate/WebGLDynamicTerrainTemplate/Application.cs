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
    using WebGLDynamicTerrainTemplate.Design.THREE;



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
                //new global::WebGLDynamicTerrainTemplate.Models.flamingo().Content,
                //new global::WebGLDynamicTerrainTemplate.Models.parrot().Content,
                //new global::WebGLDynamicTerrainTemplate.Models.stork().Content,
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
            var morphs = new List<MorphAnimMesh>();

            var updateNoise = true;

            var animateTerrain = false;

            //var textMesh1;

            //var mlib = {};


            //    container = document.getElementById( 'container' );

            var soundtrack = page.soundtrack;

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

            //    var normalShader = THREE.ShaderExtras[ 'normalmap' ];

            //    var rx = 256, ry = 256;
            //    var pars = { minFilter: THREE.LinearMipmapLinearFilter, magFilter: THREE.LinearFilter, format: THREE.RGBFormat };

            //    heightMap  = new THREE.WebGLRenderTarget( rx, ry, pars );
            //    normalMap = new THREE.WebGLRenderTarget( rx, ry, pars );

            //    uniformsNoise = {

            //        time:   { type: "f", value: 1.0 },
            //        scale:  { type: "v2", value: new THREE.Vector2( 1.5, 1.5 ) },
            //        offset: { type: "v2", value: new THREE.Vector2( 0, 0 ) }

            //    };

            //    uniformsNormal = THREE.UniformsUtils.clone( normalShader.uniforms );

            //    uniformsNormal.height.value = 0.05;
            //    uniformsNormal.resolution.value.set( rx, ry );
            //    uniformsNormal.heightMap.texture = heightMap;

            //    var vertexShader = document.getElementById( 'vertexShader' ).textContent;
            #endregion

            #region TEXTURES

            //    var specularMap = new THREE.WebGLRenderTarget( 2048, 2048, pars );

            //    var diffuseTexture1 = THREE.ImageUtils.loadTexture( "textures/terrain/grasslight-big.jpg", null, function () {

            //        loadTextures();
            //        applyShader( THREE.ShaderExtras[ 'luminosity' ], diffuseTexture1, specularMap );

            //    } );

            //    var diffuseTexture2 = THREE.ImageUtils.loadTexture( "textures/terrain/backgrounddetailed6.jpg", null, loadTextures );
            //    var detailTexture = THREE.ImageUtils.loadTexture( "textures/terrain/grasslight-big-nm.jpg", null, loadTextures );

            //    diffuseTexture1.wrapS = diffuseTexture1.wrapT = THREE.RepeatWrapping;
            //    diffuseTexture2.wrapS = diffuseTexture2.wrapT = THREE.RepeatWrapping;
            //    detailTexture.wrapS = detailTexture.wrapT = THREE.RepeatWrapping;
            //    specularMap.wrapS = specularMap.wrapT = THREE.RepeatWrapping;
            #endregion

            #region TERRAIN SHADER

            //    var terrainShader = THREE.ShaderTerrain[ "terrain" ];

            //    uniformsTerrain = THREE.UniformsUtils.clone( terrainShader.uniforms );

            //    uniformsTerrain[ "tNormal" ].texture = normalMap;
            //    uniformsTerrain[ "uNormalScale" ].value = 3.5;

            //    uniformsTerrain[ "tDisplacement" ].texture = heightMap;

            //    uniformsTerrain[ "tDiffuse1" ].texture = diffuseTexture1;
            //    uniformsTerrain[ "tDiffuse2" ].texture = diffuseTexture2;
            //    uniformsTerrain[ "tSpecular" ].texture = specularMap;
            //    uniformsTerrain[ "tDetail" ].texture = detailTexture;

            //    uniformsTerrain[ "enableDiffuse1" ].value = true;
            //    uniformsTerrain[ "enableDiffuse2" ].value = true;
            //    uniformsTerrain[ "enableSpecular" ].value = true;

            //    uniformsTerrain[ "uDiffuseColor" ].value.setHex( 0xffffff );
            //    uniformsTerrain[ "uSpecularColor" ].value.setHex( 0xffffff );
            //    uniformsTerrain[ "uAmbientColor" ].value.setHex( 0x111111 );

            //    uniformsTerrain[ "uShininess" ].value = 30;

            //    uniformsTerrain[ "uDisplacementScale" ].value = 375;

            //    uniformsTerrain[ "uRepeatOverlay" ].value.set( 6, 6 );

            //    var params = [
            //                    [ 'heightmap', 	document.getElementById( 'fragmentShaderNoise' ).textContent, 	vertexShader, uniformsNoise, false ],
            //                    [ 'normal', 	normalShader.fragmentShader,  normalShader.vertexShader, uniformsNormal, false ],
            //                    [ 'terrain', 	terrainShader.fragmentShader, terrainShader.vertexShader, uniformsTerrain, true ]
            //                 ];

            //    for( var i = 0; i < params.length; i ++ ) {

            //        material = new THREE.ShaderMaterial( {

            //            uniforms: 		params[ i ][ 3 ],
            //            vertexShader: 	params[ i ][ 2 ],
            //            fragmentShader: params[ i ][ 1 ],
            //            lights: 		params[ i ][ 4 ],
            //            fog: 			true
            //            } );

            //        mlib[ params[ i ][ 0 ] ] = material;

            //    }


            //    var plane = new THREE.PlaneGeometry( SCREEN_WIDTH, SCREEN_HEIGHT );

            //    quadTarget = new THREE.Mesh( plane, new THREE.MeshBasicMaterial( { color: 0xff0000 } ) );
            //    quadTarget.position.z = -500;
            //    sceneRenderTarget.addObject( quadTarget );
            #endregion

            #region TERRAIN MESH

            var geometryTerrain = new THREE.PlaneGeometry(6000, 6000, 256, 256);
            geometryTerrain.computeFaceNormals();
            geometryTerrain.computeVertexNormals();
            geometryTerrain.computeTangents();

            //    terrain = new THREE.Mesh( geometryTerrain, mlib[ "terrain" ] );
            //    terrain.rotation.set( -Math.PI/2, 0, 0 );
            //    terrain.position.set( 0, -125, 0 );
            //    terrain.visible = false;
            //    scene.add( terrain );
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

            //    renderTargetParameters = { minFilter: THREE.LinearFilter, magFilter: THREE.LinearFilter, format: THREE.RGBFormat, stencilBufer: false };
            //    renderTarget = new THREE.WebGLRenderTarget( SCREEN_WIDTH, SCREEN_HEIGHT, renderTargetParameters );

            //var effectBloom = new THREE.BloomPass( 0.6 );
            //    var effectBleach = new THREE.ShaderPass( THREE.ShaderExtras[ "bleachbypass" ] );

            //    hblur = new THREE.ShaderPass( THREE.ShaderExtras[ "horizontalTiltShift" ] );
            //    vblur = new THREE.ShaderPass( THREE.ShaderExtras[ "verticalTiltShift" ] );

            //    var bluriness = 6;

            //    hblur.uniforms[ 'h' ].value = bluriness / SCREEN_WIDTH;
            //    vblur.uniforms[ 'v' ].value = bluriness / SCREEN_HEIGHT;

            //    hblur.uniforms[ 'r' ].value = vblur.uniforms[ 'r' ].value = 0.5;

            //    effectBleach.uniforms[ 'opacity' ].value = 0.65;

            //    composer = new THREE.EffectComposer( renderer, renderTarget );

            //    var renderModel = new THREE.RenderPass( scene, camera );

            //    vblur.renderToScreen = true;

            //    composer = new THREE.EffectComposer( renderer, renderTarget );

            //    composer.addPass( renderModel );

            //    composer.addPass( effectBloom );
            //    //composer.addPass( effectBleach );

            //    composer.addPass( hblur );
            //    composer.addPass( vblur );
            #endregion

            var r = new Random();

            Func<f> Math_random = () => (f)r.NextDouble();

            var THREE_FaceColors = 1;


            #region addMorph

            Action<MyModelGeometry, f, f, f, f, f> addMorph = (geometry, speed, duration, x, y, z) =>
            {

                var material = new THREE.MeshLambertMaterial(
                    new MeshLambertMaterialArguments
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



            // PRE-INIT

            renderer.initWebGLObjects(scene);


            #region EVENTS


            //    document.addEventListener( 'keydown', onKeyDown, false );
            #endregion


            #region onKeyDown
            //function onKeyDown ( event ) {

            //    switch( event.keyCode ) {

            //        case 78: /*N*/  lightDir *= -1; break;
            //        case 77: /*M*/  animDeltaDir *= -1; break;
            //        case 66: /*B*/  soundDir *= -1; break;

            //    }

            //};

            ////
            #endregion

            #region applyShader
            //function applyShader( shader, texture, target ) {

            //    var shaderMaterial = new THREE.ShaderMaterial( {

            //        fragmentShader: shader.fragmentShader,
            //        vertexShader: shader.vertexShader,
            //        uniforms: THREE.UniformsUtils.clone( shader.uniforms )

            //    } );

            //    shaderMaterial.uniforms[ "tDiffuse" ].texture = texture;

            //    var sceneTmp = new THREE.Scene();

            //    var meshTmp = new THREE.Mesh( new THREE.PlaneGeometry( SCREEN_WIDTH, SCREEN_HEIGHT ), shaderMaterial );
            //    meshTmp.position.z = -500;
            //    sceneTmp.add( meshTmp );

            //    renderer.render( sceneTmp, cameraOrtho, target, true );

            //};

            ////
            #endregion


            #region loadTextures
            //function loadTextures() {

            //    textureCounter += 1;

            //    if ( textureCounter == 3 )	{

            //        terrain.visible = true;

            //        document.getElementById( "loading" ).style.display = "none";

            //    }

            //}

            ////
            #endregion


            #region render
            //function render() {

            //    var delta = clock.getDelta();

            //    soundVal = THREE.Math.clamp( soundVal + delta * soundDir, 0, 1 );

            //    if ( soundVal !== oldSoundVal ) {

            //        if ( soundtrack ) {

            //            soundtrack.volume = soundVal;
            //            oldSoundVal = soundVal;

            //        }

            //    }

            //    if ( terrain.visible ) {

            //        controls.update();

            //        var time = Date.now() * 0.001;

            //        var fLow = 0.4, fHigh = 0.825;

            //        lightVal = THREE.Math.clamp( lightVal + 0.5 * delta * lightDir, fLow, fHigh );

            //        var valNorm = ( lightVal - fLow ) / ( fHigh - fLow );

            //        var sat = THREE.Math.mapLinear( valNorm, 0, 1, 0.95, 0.25 );
            //        scene.fog.color.setHSV( 0.1, sat, lightVal );

            //        renderer.setClearColor( scene.fog.color, 1 );

            //        spotLight.intensity = THREE.Math.mapLinear( valNorm, 0, 1, 0.1, 1.15 );
            //        pointLight.intensity = THREE.Math.mapLinear( valNorm, 0, 1, 0.9, 1.5 );

            //        uniformsTerrain[ "uNormalScale" ].value = THREE.Math.mapLinear( valNorm, 0, 1, 0.6, 3.5 );

            //        if ( updateNoise ) {

            //            animDelta = THREE.Math.clamp( animDelta + 0.00075 * animDeltaDir, 0, 0.05 );
            //            uniformsNoise[ "time" ].value += delta * animDelta;

            //            uniformsNoise[ "offset" ].value.x += delta * 0.05;

            //            uniformsTerrain[ "uOffset" ].value.x = 4 * uniformsNoise[ "offset" ].value.x;

            //            quadTarget.material = mlib[ "heightmap" ];
            //            renderer.render( sceneRenderTarget, cameraOrtho, heightMap, true );

            //            quadTarget.material = mlib[ "normal" ];
            //            renderer.render( sceneRenderTarget, cameraOrtho, normalMap, true );

            //            //updateNoise = false;

            //        }


            //        for ( var i = 0; i < morphs.length; i ++ ) {

            //            morph = morphs[ i ];

            //            morph.updateAnimation( 1000 * delta );

            //            morph.position.x += morph.speed * delta;

            //            if ( morph.position.x  > 2000 )  {

            //                morph.position.x = -1500 - Math.random() * 500;

            //            }


            //        }

            //        //renderer.render( scene, camera );
            //        composer.render( 0.1 );

            //    }

            //}
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
        private THREE.Fog fog;

    }
}
