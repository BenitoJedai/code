using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLInvade.HTML.Pages;
using WebGLInvade.Library;

namespace WebGLInvade
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using ScriptCoreLib.Shared.Lambda;
    using ScriptCoreLib.Shared.Drawing;
    using WebGLInvade.Shaders;
    using WebGLInvade.Library;
    using System.Collections.Generic;
    using ScriptCoreLib.JavaScript.Runtime;


    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* Source: http://www.ro.me/tech/demos/6/index.html
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page = null)
        {
            new[]
            {
                new global::WebGLInvade.Library.Three().Content,
                new global::WebGLInvade.Library.ShaderExtras().Content,
                new global::WebGLInvade.Library.postprocessing.EffectComposer().Content,
                new global::WebGLInvade.Library.postprocessing.ShaderPass().Content,
                new global::WebGLInvade.Library.postprocessing.MaskPass().Content,
                new global::WebGLInvade.Library.postprocessing.RenderPass().Content,
                new global::WebGLInvade.Library.postprocessing.FilmPass().Content,
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





            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        

        void InitializeContent(IDefaultPage page = null)
        {

var SCREEN_WIDTH = window.innerWidth;
			var SCREEN_HEIGHT = window.innerHeight;

			var container,stats;

			var camera, scene;
			var renderer;

			var mesh, zmesh, geometry;

			var mouseX = 0, mouseY = 0;

			var windowHalfX = window.innerWidth / 2;
			var windowHalfY = window.innerHeight / 2;
			var lastUpdate = new Date().getTime();
			
			document.addEventListener('mousemove', onDocumentMouseMove, false);

			init();
			animate();

			function init() {

				container = document.createElement('div');
				document.body.appendChild(container);

				camera = new THREE.Camera( 75, SCREEN_WIDTH / SCREEN_HEIGHT, 1, 100000 );
				camera.position.z = 50;
				camera.updateMatrix();

				scene = new THREE.Scene();

				// LIGHTS

				var ambient = new THREE.AmbientLight( 0x222222 );
				ambient.position.z = -300;
				scene.addLight( ambient );

				var directionalLight = new THREE.DirectionalLight( 0xffeedd );
				directionalLight.position.set( -1, 0, 1 );
				directionalLight.position.normalize();
				scene.addLight( directionalLight );

				var dLight = new THREE.DirectionalLight( 0xffeedd );
				dLight.position.set( 1, 0, 1 );
				dLight.position.normalize();
				scene.addLight( dLight );

				// init the WebGL renderer and append it to the Dom
				renderer = new THREE.WebGLRenderer();
				renderer.setSize( window.innerWidth, window.innerHeight );
				renderer.autoClear = false;
				container.appendChild( renderer.domElement );

				var loader = new THREE.JSONLoader(),
					callbackMale   = function( geometry ) { createScene( geometry,  90, 50, 0, 105 ) };

				loader.load( { model: "invade.js", callback: callbackMale } );
				
				// postprocessing

		var renderModel = new THREE.RenderPass( scene, camera );
		var effectFilm = new THREE.FilmPass( 0.35, 0.50, 2048, false ); //( 0.35, 0.75, 2048, false );

		effectFilm.renderToScreen = true;

		composer = new THREE.EffectComposer( renderer );

		composer.addPass( renderModel );
		composer.addPass( effectFilm );
			}

			function createScene( geometry, x, y, z, b ) {

				zmesh = new THREE.Mesh( geometry, new THREE.MeshFaceMaterial() );
				zmesh.position.x = x;
				zmesh.position.z = y;
				zmesh.position.y = z;
				zmesh.scale.x = zmesh.scale.y = zmesh.scale.z = 5;
				zmesh.overdraw = true;
				zmesh.updateMatrix();
				scene.addObject(zmesh);
			}

			function onDocumentMouseMove(event) {

				mouseX = ( event.clientX - windowHalfX );
				mouseY = ( event.clientY - windowHalfY );

			}

			function animate() {
				requestAnimationFrame( animate );
				render();
			}

			function render() {
				delta = this.getFrametime();
				camera.position.x += ( mouseX - camera.position.x ) * .05;
				camera.position.y += ( - mouseY - camera.position.y ) * .05;
				camera.updateMatrix();

				//renderer.render( scene, camera );
				renderer.clear();
				composer.render( delta );
			}
			function getFrametime() {

        var now = new Date().getTime();
        var tdiff = ( now - lastUpdate ) / 1000;
        lastUpdate = now;
        return tdiff;

        }

        public Action Dispose;

    }


}
