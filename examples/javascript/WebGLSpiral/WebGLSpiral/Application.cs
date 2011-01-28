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
using WebGLSpiral.HTML.Pages;

namespace WebGLSpiral
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        // This example shall implement a Rotating Spiral 
        // 01. http://www.brainjam.ca/stackoverflow/webglspiral.html
        // 02. Build this empty project to verify jsc does its thing.
        // 03. Running this project shows up as a web page
        // 04. Start looking at "view-source:http://www.brainjam.ca/stackoverflow/webglspiral.html"
        // 05. Extract fragment shader
        // 06. Save work and commit to svn.

        const string FragmentShader = @"
			attribute vec3 position;
 
			void main() {
 
				gl_Position = vec4( position, 1.0 );
 
			}
        ";

        const string VertexShader = @"
            uniform float time;
			uniform vec2 resolution;
			uniform vec2 aspect;
 
			void main( void ) {
 
				vec2 position = -aspect.xy + 2.0 * gl_FragCoord.xy / resolution.xy * aspect.xy;
                float angle = 0.0 ;
                float radius = sqrt(position.x*position.x + position.y*position.y) ;
                if (position.x != 0.0 && position.y != 0.0){
                    angle = degrees(atan(position.y,position.x)) ;
                }
                float amod = mod(angle+30.0*time-120.0*log(radius), 30.0) ;
                if (amod<15.0){
                    gl_FragColor = vec4( 0.0, 0.0, 0.0, 1.0 );
                } else{
                    gl_FragColor = vec4( 1.0, 1.0, 1.0, 1.0 );                    
                }
			}
        ";

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
