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
using WebGLShaderDisturb.HTML.Pages;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLShaderDisturb
{
    using gl = WebGLRenderingContext;
    using WebGLFloatArray = Float32Array;
    using WebGLUnsignedShortArray = Uint16Array;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            // view-source:http://mrdoob.com/lab/javascript/webgl/glsl/04/

            var canvas = new IHTMLCanvas();

            canvas.AttachToDocument();

            // Initialise WebGL

            var gl = default(WebGLRenderingContext);

            try
            {

                gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl");

            }
            catch { }

            if (gl == null)
            {

                Native.Window.alert("WebGL not supported");
                throw new InvalidOperationException("cannot create webgl context");

            }

            // Create Vertex buffer (2 triangles)

            var buffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(-1.0f, -1.0f, 1.0f, -1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f), gl.STATIC_DRAW);

            // Create Program

            Func<string, ulong, WebGLShader> createShader = ( src, type ) =>
            {
 
				var shader = gl.createShader( type );
 
				gl.shaderSource( shader, src );
				gl.compileShader( shader );
 
				if ( gl.getShaderParameter( shader, gl.COMPILE_STATUS ) == null ) 
                {
                    if (type == gl.VERTEX_SHADER )
					    Native.Window.alert(  "VERTEX SHADER:\n" + gl.getShaderInfoLog( shader ));
					else
                        Native.Window.alert( "FRAGMENT SHADER:\n" + gl.getShaderInfoLog( shader  ));
					return null;
 
				}
 
				return shader;
 
			};

            Func<string, string, WebGLProgram> createProgram = ( vertex, fragment ) =>
            {
 
				var program = gl.createProgram();
 
				var vs = createShader( vertex, gl.VERTEX_SHADER );
				var fs = createShader( "#ifdef GL_ES\nprecision highp float;\n#endif\n\n" + fragment, gl.FRAGMENT_SHADER );
 
				if ( vs == null || fs == null ) return null;
 
				gl.attachShader( program, vs );
				gl.attachShader( program, fs );
 
				gl.deleteShader( vs );
				gl.deleteShader( fs );
 
				gl.linkProgram( program );
 
				if ( gl.getProgramParameter( program, gl.LINK_STATUS ) == null ) {
 
					Native.Window.alert( "ERROR:\n" +
					"VALIDATE_STATUS: " + gl.getProgramParameter( program, gl.VALIDATE_STATUS ) + "\n" +
					"ERROR: " + gl.getError() + "\n\n" +
					"- Vertex Shader -\n" + vertex + "\n\n" +
					"- Fragment Shader -\n" + fragment );
 
					return null;
 
				}
 
				return program;
 
			};

            Action loop = delegate
            {
 
				if ( currentProgram  == null) return;
 
				parameters.time = new IDate().getTime() - parameters.start_time;
 
				gl.clear( gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT );
 
				// Load program into GPU
 
				gl.useProgram( currentProgram );
 
				// Get var locations
 
				vertexPositionLocation = gl.getAttribLocation( currentProgram, "position" );
				textureLocation = gl.getUniformLocation( currentProgram, "texture" );
 
				// Set values to program variables
 
				gl.uniform1f( gl.getUniformLocation( currentProgram, "time" ), parameters.time / 1000 );
				gl.uniform2f( gl.getUniformLocation( currentProgram, "resolution" ), parameters.screenWidth, parameters.screenHeight );
 
				gl.uniform1i( textureLocation, 0 );
				gl.activeTexture( gl.TEXTURE0);
				gl.bindTexture( gl.TEXTURE_2D, texture );
 
				// Render geometry
 
				gl.bindBuffer( gl.ARRAY_BUFFER, buffer );
				gl.vertexAttribPointer( vertexPositionLocation, 2, gl.FLOAT, false, 0, 0 );
				gl.enableVertexAttribArray( vertexPositionLocation );
				gl.drawArrays( gl.TRIANGLES, 0, 6 );
				gl.disableVertexAttribArray( vertexPositionLocation );
 
			}
            };

            //var currentProgram = createProgram( vertex_shader, fragment_shader );

            //texture = loadTexture( 'disturb.jpg' );


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
