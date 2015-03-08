using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChromeShaderToyColumns;
using ChromeShaderToyColumns.Design;
using ChromeShaderToyColumns.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.WebAudio;

namespace ChromeShaderToyColumns
{
	using gl = WebGLRenderingContext;

	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		delegate void RefreshTexturThumbailDelegate(
			object myself,
			int slot,
			object img,
			bool forceFrame,
			bool gui,
			int guiID,
			int time
			);




		static WebGLBuffer createQuadVBO(gl gl)
		{
			var vertices = new Float32Array(new float[]
				{ -1.0f, -1.0f, 1.0f, -1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f }
				);

			var vbo = gl.createBuffer();
			gl.bindBuffer(gl.ARRAY_BUFFER, vbo);
			gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);
			gl.bindBuffer(gl.ARRAY_BUFFER, null);

			return vbo;
		}


		class CreateShaderResult
		{
			public string mInfo;
			public bool mSuccess;

			public WebGLProgram mProgram;
		}

		static CreateShaderResult CreateShader(WebGLRenderingContext gl, string tvs, string tfs, bool nativeDebug)
		{
			var tmpProgram = gl.createProgram();

			var vs = gl.createShader(gl.VERTEX_SHADER);
			var fs = gl.createShader(gl.FRAGMENT_SHADER);

			gl.shaderSource(vs, tvs);
			gl.shaderSource(fs, tfs);

			gl.compileShader(vs);
			gl.compileShader(fs);

			if (gl.getShaderParameter(vs, gl.COMPILE_STATUS) != null)
			{
				var infoLog = gl.getShaderInfoLog(vs);
				gl.deleteProgram(tmpProgram);
				return new CreateShaderResult { mSuccess = false, mInfo = infoLog };
			}

			if (gl.getShaderParameter(fs, gl.COMPILE_STATUS) != null)
			{
				var infoLog = gl.getShaderInfoLog(fs);
				gl.deleteProgram(tmpProgram);
				return new CreateShaderResult { mSuccess = false, mInfo = infoLog };
			}

			if (nativeDebug)
			{
				var dbgext = gl.getExtension("WEBGL_debug_shaders");
				if (dbgext != null)
				{
					//var hlsl = dbgext.getTranslatedShaderSource(fs);
					//console.log("------------------------\nHLSL code\n------------------------\n" + hlsl + "\n------------------------\n");
				}
			}

			gl.attachShader(tmpProgram, vs);
			gl.attachShader(tmpProgram, fs);

			gl.deleteShader(vs);
			gl.deleteShader(fs);

			gl.linkProgram(tmpProgram);

			if (gl.getProgramParameter(tmpProgram, gl.LINK_STATUS) != null)
			{
				var infoLog = gl.getProgramInfoLog(tmpProgram);
				gl.deleteProgram(tmpProgram);
				return new CreateShaderResult { mSuccess = false, mInfo = infoLog };
			}

			return new CreateShaderResult { mSuccess = true, mProgram = tmpProgram };
		}

		static string DetermineShaderPrecission(WebGLRenderingContext gl)
		{
			var h1 = "#ifdef GL_ES\n" +
					 "precision highp float;\n" +
					 "#endif\n";

			var h2 = "#ifdef GL_ES\n" +
					 "precision mediump float;\n" +
					 "#endif\n";

			var h3 = "#ifdef GL_ES\n" +
					 "precision lowp float;\n" +
					 "#endif\n";

			var vstr = "void main() { gl_Position = vec4(1.0); }\n";
			var fstr = "void main() { gl_FragColor = vec4(1.0); }\n";

			if (CreateShader(gl, vstr, h1 + fstr, false).mSuccess == true) return h1;
			if (CreateShader(gl, vstr, h2 + fstr, false).mSuccess == true) return h2;
			if (CreateShader(gl, vstr, h3 + fstr, false).mSuccess == true) return h3;

			return "";
		}


		class EffectPass
		{

			public Action MakeHeader_Image;

			public Action NewShader_Image;

			public EffectPass(
				string precission,
				bool supportDerivatives,
				RefreshTexturThumbailDelegate callback,
				object obj,
				bool forceMuted,
				bool forcePaused,
				WebGLBuffer quadVBO,
				GainNode outputGainNode
				)
			{
				var mInputs = new object[4];

				#region MakeHeader_Image
				this.MakeHeader_Image = delegate
				{
					var header = precission;
					var headerlength = 3;

					if (supportDerivatives) { header += "#extension GL_OES_standard_derivatives : enable\n"; headerlength++; }

					header += "uniform vec3      iResolution;\n" +
							  "uniform float     iGlobalTime;\n" +
							  "uniform float     iChannelTime[4];\n" +
							  "uniform vec4      iMouse;\n" +
							  "uniform vec4      iDate;\n" +
							  "uniform float     iSampleRate;\n" +
							  "uniform vec3      iChannelResolution[4];\n";
					headerlength += 7;

					for (var i = 0; i < mInputs.Length; i++)
					{
						var inp = mInputs[i];

						//if (inp != null && inp.mInfo.mType == "cubemap")
						if (inp != null)
							header += "uniform samplerCube iChannel" + i + ";\n";
						else
							header += "uniform sampler2D iChannel" + i + ";\n";
						headerlength++;
					}

					var mImagePassFooter = "\nvoid main( void )" +
					"{" +
						//"vec4 color[4];" +
						//"mainImage( color[0], gl_FragCoord.xy );" +
						//"gl_FragColor = color[0];" +
						"vec4 color;" +
						"mainImage( color, gl_FragCoord.xy );" +
						"color.w = 1.0;" +
						"gl_FragColor = color;" +
					"}";



					NewShader_Image = delegate
					{

					};
				};
				#endregion

			}
		}

		class Effect
		{
			private GainNode mGainNode;
			private EffectPass[] mPasses;
			private WebGLBuffer mQuadVBO;
			private bool mSupportTextureFloat;

			public Effect(
				AudioContext ac,
				WebGLRenderingContext gl,
				int xres,
				int yres,
				RefreshTexturThumbailDelegate callback,
				object obj,
				bool forceMuted,
				bool forcePaused)
			{

				var ext = gl.getExtension("OES_standard_derivatives");
				var supportsDerivatives = (ext != null);

				//if (supportsDerivatives) gl.hint(ext.FRAGMENT_SHADER_DERIVATIVE_HINT_OES, gl.NICEST);

				var ext2 = gl.getExtension("OES_texture_float");
				this.mSupportTextureFloat = (ext2 != null);

				var precision = DetermineShaderPrecission(gl);

				this.mGainNode = ac.createGain();
				this.mGainNode.connect(ac.destination);

				this.mQuadVBO = createQuadVBO(gl);


				this.mPasses = new EffectPass[2];

				for (var i = 0; i < 2; i++)
				{
					this.mPasses[i] = new EffectPass(
						precision, supportsDerivatives, callback, obj, forceMuted, forcePaused, this.mQuadVBO, this.mGainNode);
				}
			}


		}


		public Application(IApp page)
		{
			// view-source:https://www.shadertoy.com/view/Xls3WS
			// https://www.shadertoy.com/api

			// https://www.shadertoy.com/view/Xls3WS
			// https://www.shadertoy.com/js/cmRenderUtils.js

			#region renderUtils
			//            CreateShader = function(gl, tvs, tfs, nativeDebug)
			//{
			//                if (gl == null) return { mSuccess: false, mInfo: "no GL"};

			//                var tmpProgram = gl.createProgram();

			//                var vs = gl.createShader(gl.VERTEX_SHADER);
			//                var fs = gl.createShader(gl.FRAGMENT_SHADER);

			//                gl.shaderSource(vs, tvs);
			//                gl.shaderSource(fs, tfs);

			//                gl.compileShader(vs);
			//                gl.compileShader(fs);

			//                if (!gl.getShaderParameter(vs, gl.COMPILE_STATUS))
			//                {
			//                    var infoLog = gl.getShaderInfoLog(vs);
			//                    gl.deleteProgram(tmpProgram);
			//                    return { mSuccess: false, mInfo: infoLog};
			//                }

			//                if (!gl.getShaderParameter(fs, gl.COMPILE_STATUS))
			//                {
			//                    var infoLog = gl.getShaderInfoLog(fs);
			//                    gl.deleteProgram(tmpProgram);
			//                    return { mSuccess: false, mInfo: infoLog};
			//                }

			//                if (nativeDebug)
			//                {
			//                    var dbgext = gl.getExtension("WEBGL_debug_shaders");
			//                    if (dbgext != null)
			//                    {
			//                        var hlsl = dbgext.getTranslatedShaderSource(fs);
			//                        console.log("------------------------\nHLSL code\n------------------------\n" + hlsl + "\n------------------------\n");
			//                    }
			//                }

			//                gl.attachShader(tmpProgram, vs);
			//                gl.attachShader(tmpProgram, fs);

			//                gl.deleteShader(vs);
			//                gl.deleteShader(fs);

			//                gl.linkProgram(tmpProgram);

			//                if (!gl.getProgramParameter(tmpProgram, gl.LINK_STATUS))
			//                {
			//                    var infoLog = gl.getProgramInfoLog(tmpProgram);
			//                    gl.deleteProgram(tmpProgram);
			//                    return { mSuccess: false, mInfo: infoLog};
			//                }

			//                return { mSuccess: true, mProgram: tmpProgram};
			//            }


			//            function createGLTexture(ctx, image, format, texture)
			//            {
			//                if (ctx == null) return;

			//                ctx.bindTexture(ctx.TEXTURE_2D, texture);
			//                ctx.pixelStorei(ctx.UNPACK_FLIP_Y_WEBGL, false);
			//                ctx.texImage2D(ctx.TEXTURE_2D, 0, format, ctx.RGBA, ctx.UNSIGNED_BYTE, image);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_MAG_FILTER, ctx.LINEAR);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_MIN_FILTER, ctx.LINEAR_MIPMAP_LINEAR);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_WRAP_S, ctx.REPEAT);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_WRAP_T, ctx.REPEAT);
			//                ctx.generateMipmap(ctx.TEXTURE_2D);
			//                ctx.bindTexture(ctx.TEXTURE_2D, null);
			//            }

			//            function createGLTextureLinear(ctx, image, texture)
			//{
			//                if (ctx == null) return;

			//                ctx.bindTexture(ctx.TEXTURE_2D, texture);
			//                ctx.pixelStorei(ctx.UNPACK_FLIP_Y_WEBGL, false);
			//                ctx.texImage2D(ctx.TEXTURE_2D, 0, ctx.RGBA, ctx.RGBA, ctx.UNSIGNED_BYTE, image);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_MAG_FILTER, ctx.LINEAR);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_MIN_FILTER, ctx.LINEAR);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_WRAP_S, ctx.CLAMP_TO_EDGE);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_WRAP_T, ctx.CLAMP_TO_EDGE);
			//                ctx.bindTexture(ctx.TEXTURE_2D, null);
			//            }


			//            function createGLTextureNearestRepeat(ctx, image, texture)
			//            {
			//                if (ctx == null) return;

			//                ctx.bindTexture(ctx.TEXTURE_2D, texture);
			//                ctx.pixelStorei(ctx.UNPACK_FLIP_Y_WEBGL, false);
			//                ctx.texImage2D(ctx.TEXTURE_2D, 0, ctx.RGBA, ctx.RGBA, ctx.UNSIGNED_BYTE, image);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_MAG_FILTER, ctx.NEAREST);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_MIN_FILTER, ctx.NEAREST);
			//                ctx.bindTexture(ctx.TEXTURE_2D, null);
			//            }

			//            function createGLTextureNearest(ctx, image, texture)
			//{
			//                if (ctx == null) return;

			//                ctx.bindTexture(ctx.TEXTURE_2D, texture);
			//                ctx.pixelStorei(ctx.UNPACK_FLIP_Y_WEBGL, false);
			//                ctx.texImage2D(ctx.TEXTURE_2D, 0, ctx.RGBA, ctx.RGBA, ctx.UNSIGNED_BYTE, image);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_MAG_FILTER, ctx.NEAREST);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_MIN_FILTER, ctx.NEAREST);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_WRAP_S, ctx.CLAMP_TO_EDGE);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_WRAP_T, ctx.CLAMP_TO_EDGE);

			//                ctx.bindTexture(ctx.TEXTURE_2D, null);
			//            }

			//            function createEmptyTextureNearest(gl, xres, yres)
			//            {
			//                var tex = gl.createTexture();
			//                gl.bindTexture(gl.TEXTURE_2D, tex);
			//                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.NEAREST);
			//                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.NEAREST);
			//                gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, xres, yres, 0, gl.RGBA, gl.UNSIGNED_BYTE, null);
			//                gl.bindTexture(gl.TEXTURE_2D, null);
			//                return tex;
			//            }

			//            function createAudioTexture(ctx, texture)
			//{
			//                if (ctx == null) return;

			//                ctx.bindTexture(ctx.TEXTURE_2D, texture);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_MAG_FILTER, ctx.LINEAR);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_MIN_FILTER, ctx.LINEAR);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_WRAP_S, ctx.CLAMP_TO_EDGE);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_WRAP_T, ctx.CLAMP_TO_EDGE);
			//                ctx.texImage2D(ctx.TEXTURE_2D, 0, ctx.LUMINANCE, 512, 2, 0, ctx.LUMINANCE, ctx.UNSIGNED_BYTE, null);
			//                ctx.bindTexture(ctx.TEXTURE_2D, null);
			//            }

			//            function createKeyboardTexture(ctx, texture)
			//            {
			//                if (ctx == null) return;

			//                ctx.bindTexture(ctx.TEXTURE_2D, texture);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_MAG_FILTER, ctx.NEAREST);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_MIN_FILTER, ctx.NEAREST);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_WRAP_S, ctx.CLAMP_TO_EDGE);
			//                ctx.texParameteri(ctx.TEXTURE_2D, ctx.TEXTURE_WRAP_T, ctx.CLAMP_TO_EDGE);
			//                ctx.texImage2D(ctx.TEXTURE_2D, 0, ctx.LUMINANCE, 256, 2, 0, ctx.LUMINANCE, ctx.UNSIGNED_BYTE, null);
			//                ctx.bindTexture(ctx.TEXTURE_2D, null);
			//            }

			//            function deleteTexture(gl, tex)
			//{
			//                gl.deleteTexture(tex);
			//            }


			//            //============================================================================================================

			//            function createQuadVBO(gl)
			//{
			//                var vertices = new Float32Array( [-1.0, -1.0, 1.0, -1.0, -1.0, 1.0, 1.0, -1.0, 1.0, 1.0, -1.0, 1.0] );

			//                var vbo = gl.createBuffer();
			//                gl.bindBuffer(gl.ARRAY_BUFFER, vbo);
			//                gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);
			//                gl.bindBuffer(gl.ARRAY_BUFFER, null);

			//                return vbo;
			//            }

			//            //============================================================================================================

			//            function createFBO(gl, texture0)
			//            {
			//                var fbo = gl.createFramebuffer();
			//                gl.bindFramebuffer(gl.FRAMEBUFFER, fbo);
			//                gl.framebufferTexture2D(gl.FRAMEBUFFER, gl.COLOR_ATTACHMENT0, gl.TEXTURE_2D, texture0, 0);
			//                gl.bindFramebuffer(gl.FRAMEBUFFER, null);

			//                return fbo;
			//            }

			//            function deleteFBO(gl, fbo)
			//{
			//                gl.deleteFramebuffer(fbo);
			//            }


			//            //============================================================================================================

			//            function DetermineShaderPrecission(gl)
			//{
			//                var h1 = "#ifdef GL_ES\n" +
			//                         "precision highp float;\n" +
			//                         "#endif\n";

			//                var h2 = "#ifdef GL_ES\n" +
			//                         "precision mediump float;\n" +
			//                         "#endif\n";

			//                var h3 = "#ifdef GL_ES\n" +
			//                         "precision lowp float;\n" +
			//                         "#endif\n";

			//                var vstr = "void main() { gl_Position = vec4(1.0); }\n";
			//                var fstr = "void main() { gl_FragColor = vec4(1.0); }\n";

			//                if (CreateShader(gl, vstr, h1 + fstr, false).mSuccess == true) return h1;
			//                if (CreateShader(gl, vstr, h2 + fstr, false).mSuccess == true) return h2;
			//                if (CreateShader(gl, vstr, h3 + fstr, false).mSuccess == true) return h3;

			//                return "";
			//            }

			#endregion

			// https://www.shadertoy.com/js/effect.js

			#region effect
			// called by?
			//			EffectPass(effect.js:5)
			//Effect(effect.js:1118)
			//ShaderToy(pgWatch.js:308)
			//watchInit(pgWatch.js:1281)
			//onload(Xls3WS: 78)



			//            var vsSource = "attribute vec2 pos; void main() { gl_Position = vec4(pos.xy,0.0,1.0); }";

			//            function EffectPass(precission, supportDerivatives, callback, obj, forceMuted, forcePaused, quadVBO, outputGainNode)
			//            {
			//                this.mInputs = new Array(4);
			//                this.mInputs[0] = null;
			//                this.mInputs[1] = null;
			//                this.mInputs[2] = null;
			//                this.mInputs[3] = null;
			//                this.mSource = null;
			//                this.mUsed = false;
			//                this.mGainNode = outputGainNode;

			//                this.mQuadVBO = quadVBO;

			//                this.mType = "image";
			//                this.mFrame = 0;

			//                this.mPrecision = precission;
			//                this.mSupportsDerivatives = supportDerivatives;
			//                this.mTextureCallbackFun = callback;
			//                this.mTextureCallbackObj = obj;
			//                this.mForceMuted = forceMuted;
			//                this.mForcePaused = forcePaused;

			//                this.mProgram = null;
			//            }

			//            EffectPass.prototype.MakeHeader_Image = function(precission, supportDerivatives)
			//{
			//                var header = this.mPrecision;
			//                var headerlength = 3;

			//                if (this.mSupportsDerivatives) { header += "#extension GL_OES_standard_derivatives : enable\n"; headerlength++; }

			//                header += "uniform vec3      iResolution;\n" +
			//                          "uniform float     iGlobalTime;\n" +
			//                          "uniform float     iChannelTime[4];\n" +
			//                          "uniform vec4      iMouse;\n" +
			//                          "uniform vec4      iDate;\n" +
			//                          "uniform float     iSampleRate;\n" +
			//                          "uniform vec3      iChannelResolution[4];\n";
			//                headerlength += 7;

			//                for (var i = 0; i < this.mInputs.length; i++)
			//                {
			//                    var inp = this.mInputs[i];

			//                    if (inp != null && inp.mInfo.mType == "cubemap")
			//                        header += "uniform samplerCube iChannel" + i + ";\n";
			//                    else
			//                        header += "uniform sampler2D iChannel" + i + ";\n";
			//                    headerlength++;
			//                }

			//                this.mImagePassFooter = "\nvoid main( void )" +
			//                "{" +
			//                    //"vec4 color[4];" +
			//                    //"mainImage( color[0], gl_FragCoord.xy );" +
			//                    //"gl_FragColor = color[0];" +
			//                    "vec4 color;" +
			//                    "mainImage( color, gl_FragCoord.xy );" +
			//                    "color.w = 1.0;" +
			//                    "gl_FragColor = color;" +
			//                "}";


			//                this.mHeader = header;
			//                this.mHeaderLength = headerlength;
			//            }


			//            EffectPass.prototype.MakeHeader_Sound = function(precission, supportDerivatives)
			//{
			//                var header = this.mPrecision;
			//                var headerlength = 3;

			//                if (this.mSupportsDerivatives) { header += "#extension GL_OES_standard_derivatives : enable\n"; headerlength++; }

			//                header += "uniform float     iChannelTime[4];\n" +
			//                          "uniform float     iBlockOffset;\n" +
			//                          "uniform vec4      iDate;\n" +
			//                          "uniform float     iSampleRate;\n" +
			//                          "uniform vec3      iChannelResolution[4];\n";
			//                headerlength += 5;

			//                for (var i = 0; i < this.mInputs.length; i++)
			//                {
			//                    var inp = this.mInputs[i];

			//                    if (inp != null && inp.mInfo.mType == "cubemap")
			//                        header += "uniform samplerCube iChannel" + i + ";\n";
			//                    else
			//                        header += "uniform sampler2D iChannel" + i + ";\n";
			//                    headerlength++;
			//                }

			//                this.mHeader = header;
			//                this.mHeaderLength = headerlength;
			//            }

			//            EffectPass.prototype.MakeHeader = function(precission, supportDerivatives)
			//{
			//                if (this.mType == "image") this.MakeHeader_Image(precission, supportDerivatives);
			//                else this.MakeHeader_Sound(precission, supportDerivatives);
			//            }

			//            EffectPass.prototype.Create_Image = function(wa, gl)
			//{
			//                this.MakeHeader();

			//                this.mSampleRate = 44100;
			//            }
			//            EffectPass.prototype.Destroy_Image = function(wa, gl)
			//{
			//            }

			//            EffectPass.prototype.Create_Sound = function(wa, gl)
			//{
			//                this.MakeHeader();

			//                this.mSoundPassFooter = "void main()" +
			//                "{" +
			//                    "float t = iBlockOffset + (gl_FragCoord.x + gl_FragCoord.y*512.0)/44100.0;" +

			//                    "vec2 y = mainSound( t );" +

			//                    "vec2 v  = floor((0.5+0.5*y)*65536.0);" +
			//                    "vec2 vl =   mod(v,256.0)/255.0;" +
			//                    "vec2 vh = floor(v/256.0)/255.0;" +
			//                    "gl_FragColor = vec4(vl.x,vh.x,vl.y,vh.y);" +
			//                "}";


			//                this.mSampleRate = 44100;
			//                this.mPlayTime = 60;
			//                this.mPlaySamples = this.mPlayTime * this.mSampleRate;
			//                this.mBuffer = wa.createBuffer(2, this.mPlaySamples, this.mSampleRate);


			//                //-------------------
			//                this.mTextureDimensions = 512;
			//                this.mRenderTexture = createEmptyTextureNearest(gl, this.mTextureDimensions, this.mTextureDimensions);
			//                this.mRenderFBO = createFBO(gl, this.mRenderTexture);

			//                //-----------------------------

			//                // ArrayBufferView pixels;
			//                this.mTmpBufferSamples = this.mTextureDimensions * this.mTextureDimensions;
			//                this.mData = new Uint8Array(this.mTmpBufferSamples * 4);

			//                this.mPlayNode = null;
			//            }

			//            EffectPass.prototype.Destroy_Sound = function(wa, gl)
			//{
			//                if (this.mPlayNode != null) this.mPlayNode.stop();
			//                this.mPlayNode = null;
			//                this.mBuffer = null;
			//                this.mData = null;
			//                deleteFBO(gl, this.mRenderFBO);
			//                deleteTexture(gl, this.mRenderTexture);
			//            }

			//            EffectPass.prototype.Create = function(passType, wa, gl)
			//{
			//                this.mType = passType;
			//                this.mUsed = true;
			//                this.mSource = null;

			//                if (passType == "image") this.Create_Image(wa, gl);
			//                else this.Create_Sound(wa, gl);
			//            }

			//            EffectPass.prototype.Destroy = function(wa, gl)
			//{
			//                this.mUsed = false;
			//                this.mSource = null;
			//                if (this.mType == "image") this.Destroy_Image(wa, gl);
			//                else this.Destroy_Sound(wa, gl);
			//            }

			//            EffectPass.prototype.NewShader_Sound = function(gl, shaderCode)
			//{
			//                var res = CreateShader(gl, vsSource, this.mHeader + shaderCode + this.mSoundPassFooter, false);
			//                if (res.mSuccess == false)
			//                    return res.mInfo;

			//                if (this.mProgram != null)
			//                    gl.deleteProgram(this.mProgram);

			//                this.mProgram = res.mProgram;

			//                // force sound to be regenerated
			//                this.mFrame = 0;

			//                return null;
			//            }

			//            EffectPass.prototype.NewShader_Image = function(gl, shaderCode)
			//{
			//                var res = CreateShader(gl, vsSource, this.mHeader + shaderCode + this.mImagePassFooter, false);

			//                if (res.mSuccess == false)
			//                    return res.mInfo;

			//                if (this.mProgram != null)
			//                    gl.deleteProgram(this.mProgram);

			//                this.mProgram = res.mProgram;

			//                return null;
			//            }

			//            EffectPass.prototype.NewShader = function(gl, shaderCode)
			//{
			//                if (gl == null) return "No GL";

			//                var res = null;

			//                if (this.mType == "sound") res = this.NewShader_Sound(gl, shaderCode);
			//                else res = this.NewShader_Image(gl, shaderCode);

			//                this.mSource = shaderCode;

			//                return res;
			//            }


			//            EffectPass.prototype.DestroyInput = function(gl, id)
			//{
			//                if (gl == null) return;

			//                if (this.mInputs[id] == null) return;

			//                if (this.mInputs[id].mInfo.mType == "texture")
			//                {
			//                    gl.deleteTexture(this.mInputs[id].globject);
			//                }
			//                else if (this.mInputs[id].mInfo.mType == "webcam")
			//                {
			//                    gl.deleteTexture(this.mInputs[id].globject);
			//                }
			//                else if (this.mInputs[id].mInfo.mType == "video")
			//                {
			//                    this.mInputs[id].video.pause();
			//                    this.mInputs[id].video = null;
			//                    gl.deleteTexture(this.mInputs[id].globject);
			//                }
			//                else if (this.mInputs[id].mInfo.mType == "music")
			//                {
			//                    this.mInputs[id].audio.pause();
			//                    this.mInputs[id].audio = null;
			//                    gl.deleteTexture(this.mInputs[id].globject);
			//                }
			//                else if (this.mInputs[id].mInfo.mType == "cubemap")
			//                {
			//                    gl.deleteTexture(this.mInputs[id].globject);
			//                }
			//                else if (this.mInputs[id].mInfo.mType == "keyboard")
			//                {
			//                    gl.deleteTexture(this.mInputs[id].globject);
			//                }

			//                this.mInputs[id] = null;
			//            }

			//            EffectPass.prototype.PauseInput = function(id)
			//{
			//                var me = this;
			//                var inp = this.mInputs[id];

			//                if (inp == null)
			//                {
			//                }
			//                else if (inp.mInfo.mType == "texture")
			//                {
			//                }
			//                else if (inp.mInfo.mType == "video")
			//                {
			//                    if (inp.video.mPaused)
			//                    {
			//                        inp.video.play();
			//                        inp.video.paused = false;
			//                        inp.video.mPaused = false;
			//                    }
			//                    else
			//                    {
			//                        inp.video.pause();
			//                        inp.video.paused = true;
			//                        inp.video.mPaused = true;
			//                    }
			//                    return inp.video.mPaused;
			//                }
			//                else if (inp.mInfo.mType == "music")
			//                {
			//                    if (inp.audio.mPaused)
			//                    {
			//                        inp.audio.play();
			//                        inp.audio.mPaused = false;
			//                    }
			//                    else
			//                    {
			//                        inp.audio.pause();
			//                        inp.audio.mPaused = true;
			//                    }
			//                    return inp.audio.mPaused;
			//                }

			//                return null;
			//            }

			//            EffectPass.prototype.RewindInput = function(id)
			//{
			//                var me = this;
			//                var inp = this.mInputs[id];

			//                if (inp == null)
			//                {
			//                }
			//                else if (inp.mInfo.mType == "texture")
			//                {
			//                }
			//                else if (inp.mInfo.mType == "video")
			//                {
			//                    inp.video.currentTime = 0;
			//                }
			//                else if (inp.mInfo.mType == "music")
			//                {
			//                    inp.audio.currentTime = 0;
			//                }
			//            }

			//            EffectPass.prototype.MuteInput = function(wa, id)
			//{
			//                var me = this;
			//                var inp = this.mInputs[id];

			//                if (inp == null)
			//                {
			//                }
			//                else if (inp.mInfo.mType == "texture")
			//                {
			//                }
			//                else if (inp.mInfo.mType == "video")
			//                {
			//                    if (inp.video.mMuted)
			//                    {
			//                        inp.video.muted = false;
			//                        //inp.video.volume = 100;
			//                        inp.video.mMuted = false;
			//                    }
			//                    else
			//                    {
			//                        inp.video.muted = true;
			//                        //inp.video.volume = 0;
			//                        inp.video.mMuted = true;
			//                    }
			//                    return inp.video.mMuted;
			//                }
			//                else if (inp.mInfo.mType == "music")
			//                {
			//                    if (inp.audio.mMuted)
			//                    {
			//                        if (wa != null)
			//                            inp.audio.mSound.mGain.gain.value = 1.0;
			//                        inp.audio.mMuted = false;
			//                    }
			//                    else
			//                    {
			//                        if (wa != null)
			//                            inp.audio.mSound.mGain.gain.value = 0.0;
			//                        inp.audio.mMuted = true;
			//                    }
			//                    return inp.audio.mMuted;
			//                }

			//                return null;
			//            }

			//            EffectPass.prototype.UpdateInputs = function(wa, forceUpdate)
			//{
			//                for (var i = 0; i < this.mInputs.length; i++)
			//                {
			//                    var inp = this.mInputs[i];

			//                    if (inp == null)
			//                    {
			//                        if (forceUpdate)
			//                        {
			//                            if (this.mTextureCallbackFun != null)
			//                                this.mTextureCallbackFun(this.mTextureCallbackObj, i, null, false, true, 0, -1.0);
			//                        }
			//                    }
			//                    else if (inp.mInfo.mType == "texture")
			//                    {
			//                        if (inp.loaded && forceUpdate)
			//                        {
			//                            if (this.mTextureCallbackFun != null)
			//                                this.mTextureCallbackFun(this.mTextureCallbackObj, i, inp.image, true, true, 0, -1.0);
			//                        }

			//                    }
			//                    else if (inp.mInfo.mType == "video")
			//                    {
			//                        if (inp.video.readyState === inp.video.HAVE_ENOUGH_DATA)
			//                        {
			//                            if (this.mTextureCallbackFun != null)
			//                                this.mTextureCallbackFun(this.mTextureCallbackObj, i, inp.video, false, false, 0, -1);
			//                        }
			//                    }
			//                    else if (inp.mInfo.mType == "music")
			//                    {
			//                        if (inp.audio.mPaused == false && inp.audio.mForceMuted == false)
			//                        {
			//                            if (wa != null)
			//                            {
			//                                inp.audio.mSound.mAnalyser.getByteFrequencyData(inp.audio.mSound.mFreqData);
			//                                inp.audio.mSound.mAnalyser.getByteTimeDomainData(inp.audio.mSound.mWaveData);
			//                            }
			//                            if (this.mTextureCallbackFun != null)
			//                                this.mTextureCallbackFun(this.mTextureCallbackObj, i, (wa == null) ? null : inp.audio.mSound.mFreqData, false, false, 2, inp.audio.currentTime);
			//                        }
			//                    }
			//                }
			//            }

			//            EffectPass.prototype.NewTexture = function(wa, gl, slot, url)
			//{
			//                var me = this;

			//                var texture = null;

			//                if (url != null && url.mType == "webcam" && this.mForceMuted)
			//                {
			//                    url.mType = "texture";
			//                }

			//                if (url == null)
			//                {
			//                    if (me.mTextureCallbackFun != null)
			//                        me.mTextureCallbackFun(this.mTextureCallbackObj, slot, null, false, true, 0, -1.0);
			//                    return false;
			//                }
			//                else if (url.mType == "texture")
			//                {
			//                    texture = { };
			//                    texture.mInfo = url;
			//                    texture.globject = (gl != null) ? gl.createTexture() : null;
			//                    texture.loaded = false;
			//                    texture.image = new Image();
			//                    texture.image.crossOrigin = '';
			//                    texture.image.onload = function()
			//                    {
			//                        var format = gl.RGBA;
			//                        if (url.mSrc == "/presets/tex15.png" || url.mSrc == "/presets/tex17.png")
			//                            format = gl.LUMINANCE;

			//                        if (url.mSrc == "/presets/tex14.png")
			//                            createGLTextureNearest(gl, texture.image, texture.globject);
			//                        else if (url.mSrc == "/presets/tex15.png")
			//                            createGLTextureNearestRepeat(gl, texture.image, texture.globject);
			//                        else
			//                            createGLTexture(gl, texture.image, format, texture.globject);

			//                        texture.loaded = true;
			//                        if (me.mTextureCallbackFun != null)
			//                            me.mTextureCallbackFun(me.mTextureCallbackObj, slot, texture.image, true, true, 0, -1.0);
			//                    }
			//                    texture.image.src = url.mSrc;
			//                }
			//                else if (url.mType == "cubemap")
			//                {
			//                    texture = { };
			//                    texture.mInfo = url;
			//                    texture.globject = (gl != null) ? gl.createTexture() : null;
			//                    texture.loaded = false;
			//                    texture.image = [new Image(), new Image(), new Image(), new Image(), new Image(), new Image()];

			//                    gl.bindTexture(gl.TEXTURE_CUBE_MAP, texture.globject);
			//                    gl.texParameteri(gl.TEXTURE_CUBE_MAP, gl.TEXTURE_MAG_FILTER, gl.LINEAR);
			//                    gl.texParameteri(gl.TEXTURE_CUBE_MAP, gl.TEXTURE_MIN_FILTER, gl.LINEAR);
			//                    gl.bindTexture(gl.TEXTURE_CUBE_MAP, null);

			//                    texture.loaded = true;

			//                    for (var i = 0; i < 6; i++)
			//                    {
			//                        texture.image[i].mId = i;
			//                        texture.image[i].crossOrigin = '';
			//                        texture.image[i].onload = function()
			//                        {
			//                            var id = this.mId;
			//                            gl.bindTexture(gl.TEXTURE_CUBE_MAP, texture.globject);
			//                            gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, false);
			//                            gl.texImage2D(gl.TEXTURE_CUBE_MAP_POSITIVE_X + id, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, texture.image[id]);
			//                            gl.bindTexture(gl.TEXTURE_CUBE_MAP, null);
			//                            if (me.mTextureCallbackFun != null)
			//                                me.mTextureCallbackFun(me.mTextureCallbackObj, slot, texture.image[0], true, true, 0, -1.0);
			//                        }

			//                        texture.image[i].src = url.mSrc.replace("_0.", "_" + i + ".");
			//                    }
			//                }
			//                else if (url.mType == "webcam")
			//                {
			//                    texture = { };
			//                    texture.mInfo = url;
			//                    texture.globject = null;
			//                    texture.loaded = false;

			//                    texture.video = document.createElement('video');
			//                    texture.video.width = 320;
			//                    texture.video.height = 240;
			//                    texture.video.autoplay = true;
			//                    texture.video.loop = true;
			//                    texture.video.paused = true;

			//                    navigator.getUserMedia( { "video": true, "audio": false },
			//                                function(stream)
			//                                            {
			//                        texture.video.src = window.URL.createObjectURL(stream);

			//                        texture.globject = gl.createTexture();
			//                        try
			//                        {
			//                            createGLTextureLinear(gl, texture.video, texture.globject);
			//                            texture.loaded = true;
			//                        }
			//                        catch (e)
			//                        {
			//                            alert('Your browser can not transfer webcam data to the GPU.');
			//                        }
			//                    },
			//                                function(error)
			//                                            {
			//                        alert('Unable to capture WebCam. Please reload the page.');
			//                    } );
			//                }
			//                else if (url.mType == "video")
			//                {
			//                    texture = { };
			//                    texture.mInfo = url;
			//                    texture.globject = null;
			//                    texture.loaded = false;
			//                    texture.video = document.createElement('video');
			//                    texture.video.width = 256;
			//                    texture.video.height = 256;
			//                    texture.video.loop = true;
			//                    texture.video.paused = true;//this.mForcePaused;
			//                    texture.video.mPaused = true;//this.mForcePaused;
			//                    texture.video.mMuted = this.mForceMuted;
			//                    texture.video.muted = this.mForceMuted;
			//                    if (this.mForceMuted == true)
			//                        texture.video.volume = 0;
			//                    texture.video.autoplay = false;
			//                    texture.video.hasFalled = false;

			//                    texture.video.addEventListener("canplay", function(e)
			//                    {
			//                        texture.video.play();
			//                        texture.video.paused = false;
			//                        texture.video.mPaused = false;

			//                        texture.globject = gl.createTexture();
			//                        createGLTextureLinear(gl, texture.video, texture.globject);
			//                        texture.loaded = true;

			//                        if (me.mTextureCallbackFun != null)
			//                            me.mTextureCallbackFun(me.mTextureCallbackObj, slot, texture.video, true, true, 1, -1.0);

			//                    } );

			//                    texture.video.addEventListener("error", function(e)
			//                    {
			//                        if (texture.video.hasFalled == true) { alert("Error: cannot load video"); return; }
			//                        var str = texture.video.src;
			//                        str = str.substr(0, str.lastIndexOf('.')) + ".mp4";
			//                        texture.video.src = str;
			//                        texture.video.hasFalled = true;
			//                    } );


			//                    texture.video.src = url.mSrc;
			//                }
			//                else if (url.mType == "music")
			//                {
			//                    texture = { };
			//                    texture.mInfo = url;
			//                    texture.globject = null;
			//                    texture.loaded = false;
			//                    texture.audio = document.createElement('audio');
			//                    texture.audio.loop = true;
			//                    texture.audio.mMuted = this.mForceMuted;
			//                    texture.audio.mForceMuted = this.mForceMuted;

			//                    texture.audio.muted = this.mForceMuted;
			//                    if (this.mForceMuted == true)
			//                        texture.audio.volume = 0;
			//                    texture.audio.autoplay = true;
			//                    texture.audio.hasFalled = false;
			//                    texture.audio.paused = true;
			//                    texture.audio.mPaused = true;
			//                    texture.audio.mSound = { };

			//                    if (this.mForceMuted)
			//                    {
			//                        texture.globject = gl.createTexture();
			//                        createAudioTexture(gl, texture.globject);
			//                        var num = 512;
			//                        texture.audio.mSound.mFreqData = new Uint8Array(num);
			//                        texture.audio.mSound.mWaveData = new Uint8Array(num);
			//                        texture.loaded = true;
			//                        texture.audio.paused = false;
			//                        texture.audio.mPaused = false;
			//                    }

			//                    texture.audio.addEventListener("canplay", function()
			//                    {
			//                        if (this.mForceMuted) return;

			//                        texture.globject = gl.createTexture();
			//                        createAudioTexture(gl, texture.globject);

			//                        if (wa != null)
			//                        {
			//                            texture.audio.mSound.mSource = wa.createMediaElementSource(texture.audio);
			//                            texture.audio.mSound.mAnalyser = wa.createAnalyser();
			//                            texture.audio.mSound.mGain = wa.createGain();

			//                            texture.audio.mSound.mSource.connect(texture.audio.mSound.mAnalyser);
			//                            texture.audio.mSound.mAnalyser.connect(texture.audio.mSound.mGain);
			//                            texture.audio.mSound.mGain.connect(wa.destination);

			//                            texture.audio.mSound.mFreqData = new Uint8Array(texture.audio.mSound.mAnalyser.frequencyBinCount);
			//                            texture.audio.mSound.mWaveData = new Uint8Array(texture.audio.mSound.mAnalyser.frequencyBinCount);

			//                            texture.loaded = true;
			//                            texture.audio.paused = false;
			//                            texture.audio.mPaused = false;
			//                        }
			//                        else
			//                        {
			//                            if (me.mNoAudioMessageShowed == false)
			//                            {
			//                                var ve = document.getElementById("centerScreen");
			//                                doAlert(getCoords(ve), { mX: 420,mY: 160}, "Error", "Your browser does not support WebAudio.<br><br>This shader will not work as the author intended. Please consider using a WebAudio-friendly browser (Chrome).", false, null );
			//                                me.mNoAudioMessageShowed = true;
			//                            }
			//                        }
			//                    } );

			//                    texture.audio.addEventListener("error", function(e)
			//                    {
			//                        if (this.mForceMuted) return;

			//                        if (texture.audio.hasFalled == true) { /*alert("Error: cannot load music" ); */return; }
			//                        var str = texture.audio.src;
			//                        str = str.substr(0, str.lastIndexOf('.')) + ".ogg";
			//                        texture.audio.src = str;
			//                        texture.audio.hasFalled = true;
			//                    } );

			//                    if (!this.mForceMuted)
			//                    {
			//                        texture.audio.src = url.mSrc;
			//                    }


			//                    if (me.mTextureCallbackFun != null)
			//                        me.mTextureCallbackFun(me.mTextureCallbackObj, slot, null, false, true, 2, -1.0);
			//                }
			//                else if (url.mType == "keyboard")
			//                {
			//                    texture = { };
			//                    texture.mInfo = url;
			//                    texture.globject = gl.createTexture();
			//                    texture.loaded = true;

			//                    texture.keyboard = { };

			//                    texture.keyboard.mImage = new Image();
			//                    texture.keyboard.mImage.onload = function()
			//                    {
			//                        texture.loaded = true;
			//                        if (me.mTextureCallbackFun != null)
			//                            me.mTextureCallbackFun(me.mTextureCallbackObj, slot, { mImage: texture.keyboard.mImage,mData: texture.keyboard.mData}, false, false, 4, -1.0 );
			//                    }
			//                    texture.keyboard.mImage.src = "/img/keyboard.png";


			//                    texture.keyboard.mNewTextureReady = true;
			//                    texture.keyboard.mData = new Uint8Array(256 * 2);

			//                    createKeyboardTexture(gl, texture.globject);

			//                    for (var j = 0; j < (256 * 2); j++)
			//                    {
			//                        texture.keyboard.mData[j] = 0;
			//                    }

			//                    if (me.mTextureCallbackFun != null)
			//                        me.mTextureCallbackFun(me.mTextureCallbackObj, slot, { mImage: texture.keyboard.mImage,mData: texture.keyboard.mData}, false, false, 4, -1.0 );
			//                }
			//                else if (url.mType == null)
			//                {
			//                    if (me.mTextureCallbackFun != null)
			//                        me.mTextureCallbackFun(this.mTextureCallbackObj, slot, null, false, true, 0, -1.0);
			//                }
			//                else
			//                {
			//                    alert("texture type error");
			//                    return;
			//                }

			//                this.DestroyInput(gl, slot);
			//                this.mInputs[slot] = texture;

			//                this.MakeHeader();
			//            }

			//            EffectPass.prototype.Paint_Image = function(wa, gl, time, mouseOriX, mouseOriY, mousePosX, mousePosY, xres, yres)
			//{
			//                gl.viewport(0, 0, xres, yres);

			//                gl.useProgram(this.mProgram);

			//                var times = [0.0, 0.0, 0.0, 0.0];

			//                var d = new Date();
			//                var dates = [d.getFullYear(), // the year (four digits)
			//                              d.getMonth(),    // the month (from 0-11)
			//                              d.getDate(),     // the day of the month (from 1-31)
			//                              d.getHours() * 60.0 * 60 + d.getMinutes() * 60 + d.getSeconds() + d.getMilliseconds() / 1000.0];

			//                var mouse = [mousePosX, mousePosY, mouseOriX, mouseOriY];

			//                var resos = [0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0];

			//                var l2 = gl.getUniformLocation(this.mProgram, "iGlobalTime"); if (l2 != null) gl.uniform1f(l2, time);
			//                var l3 = gl.getUniformLocation(this.mProgram, "iResolution"); if (l3 != null) gl.uniform3f(l3, xres, yres, 1.0);
			//                var l4 = gl.getUniformLocation(this.mProgram, "iMouse"); if (l4 != null) gl.uniform4fv(l4, mouse);
			//                var l5 = gl.getUniformLocation(this.mProgram, "iChannelTime");
			//                var l7 = gl.getUniformLocation(this.mProgram, "iDate"); if (l7 != null) gl.uniform4fv(l7, dates);
			//                var l8 = gl.getUniformLocation(this.mProgram, "iChannelResolution");
			//                var l9 = gl.getUniformLocation(this.mProgram, "iSampleRate"); if (l9 != null) gl.uniform1f(l9, this.mSampleRate);

			//                var ich0 = gl.getUniformLocation(this.mProgram, "iChannel0"); if (ich0 != null) gl.uniform1i(ich0, 0);
			//                var ich1 = gl.getUniformLocation(this.mProgram, "iChannel1"); if (ich1 != null) gl.uniform1i(ich1, 1);
			//                var ich2 = gl.getUniformLocation(this.mProgram, "iChannel2"); if (ich2 != null) gl.uniform1i(ich2, 2);
			//                var ich3 = gl.getUniformLocation(this.mProgram, "iChannel3"); if (ich3 != null) gl.uniform1i(ich3, 3);

			//                var l1 = gl.getAttribLocation(this.mProgram, "pos");
			//                gl.bindBuffer(gl.ARRAY_BUFFER, this.mQuadVBO);
			//                gl.vertexAttribPointer(l1, 2, gl.FLOAT, false, 0, 0);
			//                gl.enableVertexAttribArray(l1);

			//                for (var i = 0; i < this.mInputs.length; i++)
			//                {
			//                    var inp = this.mInputs[i];

			//                    gl.activeTexture(gl.TEXTURE0 + i);

			//                    if (inp == null)
			//                    {
			//                        gl.bindTexture(gl.TEXTURE_2D, null);
			//                    }
			//                    else if (inp.mInfo.mType == "texture")
			//                    {
			//                        if (inp.loaded == false)
			//                            gl.bindTexture(gl.TEXTURE_2D, null);
			//                        else
			//                        {
			//                            gl.bindTexture(gl.TEXTURE_2D, inp.globject);
			//                            resos[3 * i + 0] = inp.image.width;
			//                            resos[3 * i + 1] = inp.image.height;
			//                            resos[3 * i + 2] = 1;
			//                        }
			//                    }
			//                    else if (inp.mInfo.mType == "keyboard")
			//                    {
			//                        if (inp.loaded == false)
			//                            gl.bindTexture(gl.TEXTURE_2D, null);
			//                        else
			//                        {
			//                            gl.bindTexture(gl.TEXTURE_2D, inp.globject);
			//                            if (inp.keyboard.mNewTextureReady == true)
			//                            {

			//                                gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, false);
			//                                gl.texSubImage2D(gl.TEXTURE_2D, 0, 0, 0, 256, 2, gl.LUMINANCE, gl.UNSIGNED_BYTE, inp.keyboard.mData);
			//                                inp.keyboard.mNewTextureReady = false;

			//                                if (this.mTextureCallbackFun != null)
			//                                    this.mTextureCallbackFun(this.mTextureCallbackObj, i, { mImage: inp.keyboard.mImage,mData: inp.keyboard.mData}, false, false, 4, -1.0 );
			//            }
			//        }
			//    }
			//          else if( inp.mInfo.mType=="cubemap" )
			//          {
			//              if( inp.loaded==false  )
			//                  gl.bindTexture( gl.TEXTURE_CUBE_MAP, null );
			//              else
			//                  gl.bindTexture( gl.TEXTURE_CUBE_MAP, inp.globject );
			//          }
			//          else if( inp.mInfo.mType=="webcam" )
			//          {
			//              if( inp.video.readyState === inp.video.HAVE_ENOUGH_DATA )
			//              {
			//                  if( this.mTextureCallbackFun!=null )
			//                      this.mTextureCallbackFun( this.mTextureCallbackObj, i, inp.video, false, false, 0, -1 );

			//                  if( inp.loaded==false )
			//                  {
			//    gl.bindTexture(gl.TEXTURE_2D, null);
			//}
			//                  else
			//                  {
			//    gl.bindTexture(gl.TEXTURE_2D, inp.globject);
			//    gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, true);
			//    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, inp.video);
			//    resos[3 * i + 0] = inp.video.width;
			//    resos[3 * i + 1] = inp.video.height;
			//    resos[3 * i + 2] = 1;
			//}
			//}
			//          }
			//          else if( inp.mInfo.mType=="video" )
			//          {
			//                if( inp.video.mPaused == false )
			//                {
			//                    if( this.mTextureCallbackFun!=null )
			//                        this.mTextureCallbackFun( this.mTextureCallbackObj, i, inp.video, false, false, 0, inp.video.currentTime );
			//}

			//                if( inp.loaded==false )
			//                {
			//                    gl.bindTexture( gl.TEXTURE_2D, null );
			//                }
			//                else
			//                {
			//                    times[i] = inp.video.currentTime;

			//                    gl.bindTexture( gl.TEXTURE_2D, inp.globject );

			//      	          if( inp.video.mPaused == false )
			//                    {
			//                        gl.pixelStorei( gl.UNPACK_FLIP_Y_WEBGL, true);
			//                        gl.texImage2D(  gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, inp.video );
			//                    }
			//                    resos[3 * i + 0] = inp.video.width;
			//                    resos[3 * i + 1] = inp.video.height;
			//                    resos[3 * i + 2] = 1;
			//                }
			//            }
			//          else if( inp.mInfo.mType=="music" )
			//          {
			//                if( inp.audio.mPaused == false && inp.audio.mForceMuted == false )
			//                {
			//                    if( wa != null )
			//                    {
			//                        inp.audio.mSound.mAnalyser.getByteFrequencyData(  inp.audio.mSound.mFreqData );
			//                        inp.audio.mSound.mAnalyser.getByteTimeDomainData( inp.audio.mSound.mWaveData );
			//                    }

			//                    if( this.mTextureCallbackFun!=null )
			//                        this.mTextureCallbackFun( this.mTextureCallbackObj, i, (wa==null)?null:inp.audio.mSound.mFreqData, false, false, 2, inp.audio.currentTime );
			//}

			//                if( inp.loaded==false )
			//                {
			//                    gl.bindTexture( gl.TEXTURE_2D, null );
			//                }
			//                else
			//                {
			//                    times[i] = inp.audio.currentTime;

			//                    gl.bindTexture( gl.TEXTURE_2D, inp.globject );
			//                    if( inp.audio.mForceMuted == true )
			//                    {
			//                        times[i] = 10.0 + time;
			//                        var num = inp.audio.mSound.mFreqData.length;
			//                        for( var j = 0; j<num; j++ )
			//                        {
			//                            var x = j / num;
			//var f = (0.75 + 0.25 * Math.sin(10.0 * j + 13.0 * time)) * Math.exp(-3.0 * x);

			//                            if( j<3 )
			//                                f =  Math.pow( 0.50 + 0.5* Math.sin( 6.2831*time ), 4.0 ) * (1.0-j/3.0);

			//                            inp.audio.mSound.mFreqData[j] = Math.floor(255.0* f) | 0;
			//                        }

			//                        var num = inp.audio.mSound.mFreqData.length;
			//                        for( var j = 0; j<num; j++ )
			//                        {
			//                            var f = 0.5 + 0.15 * Math.sin(17.0 * time + 10.0 * 6.2831 * j / num) * Math.sin(23.0 * time + 1.9 * j / num);
			//inp.audio.mSound.mWaveData[j] = Math.floor(255.0* f) | 0;
			//                        }

			//                    }

			//      	            if( inp.audio.mPaused == false )
			//                    {
			//                        var waveLen = Math.min(inp.audio.mSound.mWaveData.length, 512);
			//gl.texSubImage2D(  gl.TEXTURE_2D, 0, 0, 0, 512,     1, gl.LUMINANCE, gl.UNSIGNED_BYTE, inp.audio.mSound.mFreqData );
			//                        gl.texSubImage2D(  gl.TEXTURE_2D, 0, 0, 1, waveLen, 1, gl.LUMINANCE, gl.UNSIGNED_BYTE, inp.audio.mSound.mWaveData );
			//                    }
			//                }
			//            }

			//      }

			//      if( l5!=null ) gl.uniform1fv( l5, times );
			//      if( l8!=null ) gl.uniform3fv( l8, resos );


			//      gl.drawArrays(gl.TRIANGLES, 0, 6);
			//      gl.disableVertexAttribArray(l1);
			//}

			//EffectPass.prototype.Paint_Sound = function(wa, gl )
			//{
			//    var d = new Date();
			//    var dates = [d.getFullYear(), // the year (four digits)
			//                  d.getMonth(),	   // the month (from 0-11)
			//                  d.getDate(),     // the day of the month (from 1-31)
			//                  d.getHours() * 60.0 * 60 + d.getMinutes() * 60 + d.getSeconds()];

			//    var resos = [0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0];


			//    gl.bindFramebuffer(gl.FRAMEBUFFER, this.mRenderFBO);

			//    gl.viewport(0, 0, this.mTextureDimensions, this.mTextureDimensions);
			//    gl.useProgram(this.mProgram);


			//    for (var i = 0; i < this.mInputs.length; i++)
			//    {
			//        var inp = this.mInputs[i];

			//        gl.activeTexture(gl.TEXTURE0 + i);

			//        if (inp == null)
			//        {
			//            gl.bindTexture(gl.TEXTURE_2D, null);
			//        }
			//        else if (inp.mInfo.mType == "texture")
			//        {
			//            if (inp.loaded == false)
			//            {
			//                gl.bindTexture(gl.TEXTURE_2D, null);
			//            }
			//            else
			//            {
			//                gl.bindTexture(gl.TEXTURE_2D, inp.globject);
			//                resos[3 * i + 0] = inp.image.width;
			//                resos[3 * i + 1] = inp.image.height;
			//                resos[3 * i + 2] = 1;
			//            }
			//        }
			//    }

			//    var l2 = gl.getUniformLocation(this.mProgram, "iBlockOffset");
			//    var l7 = gl.getUniformLocation(this.mProgram, "iDate"); gl.uniform4fv(l7, dates);
			//    var l8 = gl.getUniformLocation(this.mProgram, "iChannelResolution"); gl.uniform3fv(l8, resos);
			//    var l9 = gl.getUniformLocation(this.mProgram, "iSampleRate"); gl.uniform1f(l9, this.mSampleRate);
			//    var ich0 = gl.getUniformLocation(this.mProgram, "iChannel0"); if (ich0 != null) gl.uniform1i(ich0, 0);
			//    var ich1 = gl.getUniformLocation(this.mProgram, "iChannel1"); if (ich1 != null) gl.uniform1i(ich1, 1);
			//    var ich2 = gl.getUniformLocation(this.mProgram, "iChannel2"); if (ich2 != null) gl.uniform1i(ich2, 2);
			//    var ich3 = gl.getUniformLocation(this.mProgram, "iChannel3"); if (ich3 != null) gl.uniform1i(ich3, 3);



			//    var l1 = gl.getAttribLocation(this.mProgram, "pos");
			//    gl.bindBuffer(gl.ARRAY_BUFFER, this.mQuadVBO);
			//    gl.vertexAttribPointer(l1, 2, gl.FLOAT, false, 0, 0);
			//    gl.enableVertexAttribArray(l1);

			//    //--------------------------------

			//    var bufL = this.mBuffer.getChannelData(0); // Float32Array
			//    var bufR = this.mBuffer.getChannelData(1); // Float32Array
			//    var numBlocks = this.mPlaySamples / this.mTmpBufferSamples;
			//    for (var j = 0; j < numBlocks; j++)
			//    {
			//        var off = j * this.mTmpBufferSamples;

			//        gl.uniform1f(l2, off / this.mSampleRate);
			//        gl.drawArrays(gl.TRIANGLES, 0, 6);
			//        gl.readPixels(0, 0, this.mTextureDimensions, this.mTextureDimensions, gl.RGBA, gl.UNSIGNED_BYTE, this.mData);

			//        for (var i = 0; i < this.mTmpBufferSamples; i++)
			//        {
			//            bufL[off + i] = -1.0 + 2.0 * (this.mData[4 * i + 0] + 256.0 * this.mData[4 * i + 1]) / 65535.0;
			//            bufR[off + i] = -1.0 + 2.0 * (this.mData[4 * i + 2] + 256.0 * this.mData[4 * i + 3]) / 65535.0;
			//        }
			//    }

			//    gl.disableVertexAttribArray(l1);
			//    gl.useProgram(null);
			//    gl.bindFramebuffer(gl.FRAMEBUFFER, null);

			//    //-------------------------------

			//    if (this.mPlayNode != null) { this.mPlayNode.disconnect(); this.mPlayNode.stop(); }

			//    this.mPlayNode = wa.createBufferSource();
			//    this.mPlayNode.buffer = this.mBuffer;
			//    this.mPlayNode.connect(this.mGainNode);
			//    this.mPlayNode.state = this.mPlayNode.noteOn;
			//    this.mPlayNode.start(0);
			//}

			//EffectPass.prototype.Paint = function(wa, gl, time, mouseOriX, mouseOriY, mousePosX, mousePosY, xres, yres, isPaused )
			//{
			//    if (this.mType == "sound")
			//    {
			//        if (this.mFrame == 0 && !isPaused)
			//        {
			//            // make sure all textures are loaded
			//            for (var i = 0; i < this.mInputs.length; i++)
			//            {
			//                var inp = this.mInputs[i];
			//                if (inp == null) continue;

			//                if (inp.mInfo.mType == "texture" && !inp.loaded) return;
			//                if (inp.mInfo.mType == "cubemap" && !inp.loaded) return;
			//            }

			//            this.Paint_Sound(wa, gl);
			//            this.mFrame++;
			//        }
			//    }
			//    else
			//    {
			//        this.Paint_Image(wa, gl, time, mouseOriX, mouseOriY, mousePosX, mousePosY, xres, yres);
			//        this.mFrame++;
			//    }

			//}

			//EffectPass.prototype.StopOutput_Sound = function(wa, gl )
			//{
			//    this.mPlayNode.disconnect();
			//}
			//EffectPass.prototype.ResumeOutput_Sound = function(wa, gl )
			//{
			//    this.mPlayNode.connect(this.mGainNode);
			//}

			//EffectPass.prototype.StopOutput_Image = function(wa, gl )
			//{
			//}
			//EffectPass.prototype.ResumeOutput_Image = function(wa, gl )
			//{
			//}

			//EffectPass.prototype.StopOutput = function(wa, gl )
			//{
			//    if (this.mType == "sound")
			//        this.StopOutput_Sound(wa, gl);
			//    else
			//        this.StopOutput_Image(wa, gl);
			//}


			//EffectPass.prototype.ResumeOutput = function(wa, gl )
			//{
			//    if (this.mType == "sound")
			//        this.ResumeOutput_Sound(wa, gl);
			//    else
			//        this.ResumeOutput_Image(wa, gl);
			//}

			////============================================================================================================

			//ac: AudioContext
			//callback: function ( myself, slot, img, forceFrame, gui, guiID, time )
			//ext: OESStandardDerivatives
			//ext2: OESTextureFloat
			//forceMuted: false
			//forcePaused: false
			//gl: WebGLRenderingContext
			//i: 0
			//obj: ShaderToy
			//precision: "#ifdef GL_ES↵precision highp float;↵#endif↵"
			//supportsDerivatives: true
			//this: Effect
			//xres: 460
			//yres: 237

			//function Effect(ac, gl, xres, yres, callback, obj, forceMuted, forcePaused )
			//{
			//    this.mAudioContext = ac;
			//    this.mNoAudioMessageShowed = false;
			//    this.mGLContext = gl;
			//    this.mQuadVBO = null;
			//    this.mXres = xres;
			//    this.mYres = yres;
			//    this.mForceMuted = forceMuted;
			//    this.mGainNode = null;
			//    this.mPasses = new Array(2)

			//    //-------------

			//    if (gl == null) return;

			//    var ext = gl.getExtension('OES_standard_derivatives');
			//    var supportsDerivatives = (ext != null);

			//    if (supportsDerivatives) gl.hint(ext.FRAGMENT_SHADER_DERIVATIVE_HINT_OES, gl.NICEST);

			//    var ext2 = gl.getExtension('OES_texture_float');
			//    this.mSupportTextureFloat = (ext2 != null);

			//    var precision = DetermineShaderPrecission(gl);

			//    //-------------
			//    if (ac != null)
			//    {
			//        this.mGainNode = ac.createGain();
			//        this.mGainNode.connect(ac.destination);
			//        this.mGainNode.gain.value = (this.mForceMuted) ? 0.0 : 1.0;
			//    }

			//    //-------------
			//    this.mQuadVBO = createQuadVBO(gl);

			//    for (var i = 0; i < 2; i++)
			//    {
			//        this.mPasses[i] = new EffectPass(precision, supportsDerivatives, callback, obj, forceMuted, forcePaused, this.mQuadVBO, this.mGainNode);
			//    }
			//}

			//Effect.prototype.NewTexture = function(passid, slot, url )
			//{
			//    this.mPasses[passid].NewTexture(this.mAudioContext, this.mGLContext, slot, url);
			//}

			//Effect.prototype.GetHeaderSize = function(passid )
			//{
			//    return this.mPasses[passid].mHeaderLength;
			//}

			//Effect.prototype.ToggleVolume = function(passid )
			//{
			//    this.mForceMuted = !this.mForceMuted;

			//    if (this.mForceMuted)
			//        this.mGainNode.disconnect();
			//    else
			//        this.mGainNode.connect(this.mAudioContext.destination);

			//    return this.mForceMuted;
			//}

			//Effect.prototype.SetKeyDown = function(passid, k )
			//{
			//    for (var i = 0; i < this.mPasses[passid].mInputs.length; i++)
			//    {
			//        var inp = this.mPasses[passid].mInputs[i];
			//        if (inp != null && inp.mInfo.mType == "keyboard")
			//        {
			//            inp.keyboard.mData[k] = 255;
			//            inp.keyboard.mData[k + 256] = 255 - inp.keyboard.mData[k + 256];
			//            inp.keyboard.mNewTextureReady = true;
			//            break;
			//        }
			//    }
			//}
			//Effect.prototype.SetKeyUp = function(passid, k )
			//{
			//    for (var i = 0; i < this.mPasses[passid].mInputs.length; i++)
			//    {
			//        var inp = this.mPasses[passid].mInputs[i];
			//        if (inp != null && inp.mInfo.mType == "keyboard")
			//        {
			//            inp.keyboard.mData[k] = 0;
			//            inp.keyboard.mNewTextureReady = true;
			//            break;
			//        }
			//    }
			//}

			//Effect.prototype.StopOutputs = function()
			//{
			//    var gl = this.mGLContext;
			//    var wa = this.mAudioContext;

			//    var num = this.mPasses.length;
			//    for (var i = 0; i < num; i++)
			//    {
			//        if (this.mPasses[i].mProgram == null) continue;
			//        this.mPasses[i].StopOutput(wa, gl);
			//    }
			//}

			//Effect.prototype.ResumeOutputs = function()
			//{
			//    var gl = this.mGLContext;
			//    var wa = this.mAudioContext;
			//    if (gl == null) return;

			//    var num = this.mPasses.length;
			//    for (var i = 0; i < num; i++)
			//    {
			//        if (this.mPasses[i].mProgram == null) continue;
			//        this.mPasses[i].ResumeOutput(wa, gl);
			//    }
			//}


			//Effect.prototype.SetSize = function(xres, yres)
			//{
			//    this.mXres = xres;
			//    this.mYres = yres;
			//}

			//Effect.prototype.PauseInput = function(passid, id )
			//{
			//    return this.mPasses[passid].PauseInput(id);
			//}

			//Effect.prototype.MuteInput = function(passid, id )
			//{
			//    return this.mPasses[passid].MuteInput(this.mAudioContext, id);
			//}

			//Effect.prototype.RewindInput = function(passid, id )
			//{
			//    this.mPasses[passid].RewindInput(id);
			//}

			//Effect.prototype.UpdateInputs = function(passid, forceUpdate )
			//{
			//    this.mPasses[passid].UpdateInputs(this.mAudioContext, forceUpdate);
			//}

			//Effect.prototype.ResetTime = function()
			//{
			//    var num = this.mPasses.length;
			//    for (var i = 0; i < num; i++)
			//        this.mPasses[i].mFrame = 0;
			//}


			//Effect.prototype.Paint = function(time, mouseOriX, mouseOriY, mousePosX, mousePosY, isPaused)
			//{
			//    var gl = this.mGLContext;
			//    var wa = this.mAudioContext;

			//    if (gl == null) return;

			//    var num = this.mPasses.length;
			//    for (var i = 0; i < num; i++)
			//    {
			//        if (!this.mPasses[i].mUsed) continue;
			//        if (this.mPasses[i].mProgram == null) continue;

			//        this.mPasses[i].Paint(wa, gl, time, mouseOriX, mouseOriY, mousePosX, mousePosY, this.mXres, this.mYres, isPaused);
			//    }
			//}

			//Effect.prototype.NewShader = function(shaderCode, passid )
			//{
			//    return this.mPasses[passid].NewShader(this.mGLContext, shaderCode);
			//}
			//Effect.prototype.GetNumPasses = function()
			//{
			//    var id = 0;
			//    for (var j = 0; j < this.mPasses.length; j++)
			//    {
			//        if (!this.mPasses[j].mUsed) continue;
			//        //res[id] = { mType : this.mPasses[j].mType };
			//        id++;
			//    }
			//    return id;
			//}

			//Effect.prototype.GetPassType = function(id )
			//{
			//    return this.mPasses[id].mType;
			//}

			///*
			//Effect.prototype.getPasses = function()
			//{
			//    var res = [];
			//    var id = 0;
			//    for( var j=0; j<this.mPasses.length; j++ )
			//    {
			//        if( !this.mPasses[j].mUsed ) continue;
			//        res[id] = { mType : this.mPasses[j].mType };
			//        id++;
			//    }
			//    return res;
			//}*/

			//Effect.prototype.newScriptJSON = function(jobj )
			//{
			//    if (jobj.ver != "0.1")
			//    {
			//        return { mFailed: true };
			//    }

			//    var numPasses = jobj.renderpass.length;

			//    if (numPasses == 0 || numPasses > 2)
			//    {
			//        return { mFailed: true, mError: "Incorrect number of passes, Shadertoy supports up to two-pass shaders at this moment.", mShader: null };
			//    }

			//    var res = [];// = new Array( numPasses );
			//    res.mFailed = false;
			//    for (var j = 0; j < numPasses; j++)
			//    {
			//        var rpass = jobj.renderpass[j];

			//        // skip sound passes if in thumbnail mode
			//        if (this.mForceMuted && rpass.type == "sound") continue;
			//        var numInputs = rpass.inputs.length;

			//        for (var i = 0; i < 4; i++)
			//        {
			//            this.mPasses[j].NewTexture(this.mAudioContext, this.mGLContext, i, null);
			//        }

			//        for (var i = 0; i < numInputs; i++)
			//        {
			//            var lid = rpass.inputs[i].channel;
			//            var styp = rpass.inputs[i].ctype;
			//            var sid = rpass.inputs[i].id;
			//            var ssrc = rpass.inputs[i].src;
			//            this.mPasses[j].NewTexture(this.mAudioContext, this.mGLContext, lid, { mType: styp, mID: sid, mSrc: ssrc } );
			//}
			//        //------------------------
			//        this.mPasses[j].Create( rpass.type, this.mAudioContext, this.mGLContext );

			//var shaderStr = rpass.code;

			//var result = this.mPasses[j].NewShader( this.mGLContext, shaderStr );

			//        if( result!=null )
			//        {
			//    res.mFailed = true;
			//    res[j] = {
			//        mFailed: true,
			//                       mError: result,
			//                       mType: rpass.type,
			//                       mShader: shaderStr };
			//}
			//        else
			//        {
			//    res[j] = {
			//        mFailed: false,
			//                       mError: null,
			//                       mType: rpass.type,
			//                       mShader: shaderStr };
			//}
			//}

			//    return res;
			//}

			//Effect.prototype.DestroyPass = function(id )
			//{
			//    this.mPasses[id].Destroy(this.mAudioContext, this.mGLContext);
			//}

			//Effect.prototype.AddPass = function(passType )
			//{
			//    var shaderStr = "vec2 mainSound(float time)\n{\n    return vec2( sin(6.2831*440.0*time)*exp(-3.0*time) );\n}";

			//    var id = this.GetNumPasses();
			//    this.mPasses[id].Create(passType, this.mAudioContext, this.mGLContext);
			//    var res = this.mPasses[id].NewShader(this.mGLContext, shaderStr);

			//    return { mId: id, mShader: shaderStr, mError: res };
			//}

			//Effect.prototype.exportToJSON = function()
			//{

			//    var result = { };

			//    result.ver = "0.1";

			//    result.renderpass = new Array(1)

			//    var numPasses = this.mPasses.length;
			//    for (var j = 0; j < numPasses; j++)
			//    {
			//        if (!this.mPasses[j].mUsed) continue;

			//        result.renderpass[j] = { };

			//        result.renderpass[j].outputs = new Array();
			//        result.renderpass[j].outputs.push( { channel: 0, dst: "-1" } );

			//    result.renderpass[j].inputs = new Array();
			//    for (var i = 0; i < 4; i++)
			//    {
			//        if (this.mPasses[j].mInputs[i] == null) continue;

			//        result.renderpass[j].inputs.push( {
			//            channel: i,
			//                                               ctype: this.mPasses[j].mInputs[i].mInfo.mType,
			//                                               id: this.mPasses[j].mInputs[i].mInfo.mID,
			//                                               src: this.mPasses[j].mInputs[i].mInfo.mSrc } );
			//}

			//result.renderpass[j].code = this.mPasses[j].mSource;
			//        result.renderpass[j].name = "";
			//        result.renderpass[j].description = "";
			//        result.renderpass[j].type = this.mPasses[j].mType;
			//    }

			//    return result;
			//}

			#endregion

			#region pgWatch


			//			function buildInputsUI(me)
			//{
			//				var ww = 158;
			//				var hh = 100;
			//				var ss = 22;

			//				me.mMarks = null;

			//				for (var i = 0; i < 4; i++)
			//				{
			//					var par = document.getElementById("texture" + i);
			//					par.mId = i;

			//					ww = par.offsetWidth;
			//					hh = par.offsetHeight;

			//					par.onmouseover = function(ev)
			//			          {
			//						var ele = getSourceElement(ev);
			//						var pattern = "iChannel" + ele.mId;

			//						me.mMarks = new Array();
			//						var cm = me.mCodeEditor;
			//						var num = cm.lineCount();
			//						for (var j = 0; j < num; j++)
			//						{
			//							var str = cm.getLine(j);
			//							var res = str.indexOf(pattern);
			//							if (res < 0) continue;
			//							cm.addLineClass(j, "background", "cm-highlight");
			//							me.mMarks.push(j);
			//						}
			//					}

			//					par.onmouseout = function(ev)
			//			          {
			//						var cm = me.mCodeEditor;
			//						if (me.mMarks == null) return;
			//						var num = me.mMarks.length;
			//						for (var j = 0; j < num; j++)
			//						{
			//							var l = me.mMarks.pop();
			//							cm.removeLineClass(l, "background", "cm-highlight");
			//						}
			//						me.mMarks = null;
			//					}

			//					var can = document.createElement("canvas");
			//					can.width = ww;
			//					can.height = hh;
			//					can.style.width = "100%";//ww + "px";
			//					can.style.height = "100%";//hh + "px";
			//					can.style.left = "0px";
			//					can.style.bottom = "0px";
			//					can.style.position = "absolute";
			//					can.className = "inputSelectorCanvas";
			//					can.id = "myUnitCanvas" + i;
			//					can.mId = i;
			//					can.onclick = function(ev)
			//			          {
			//						var passType = me.mEffect.GetPassType(me.mActiveDoc);
			//						overlay(getSourceElement(ev).mId, passType);
			//					}
			//					par.appendChild(can);


			//					var bar = document.createElement("div");
			//					bar.style.width = "24px";
			//					bar.style.height = hh + "px";
			//					bar.style.position = "absolute";
			//					bar.className = "inputSelectorControls";
			//					bar.id = "inputSelectorControls" + i;
			//					par.appendChild(bar);


			//					var z = document.createElement("img");
			//					z.src = "/img/pause.png";
			//					z.title = "pause/resume";
			//					z.id = "myPauseButton" + i;
			//					z.style.left = "1px";
			//					z.style.top = "10px";
			//					z.className = "uiButton";
			//					z.mId = i;
			//					z.onclick = function(ev) { var ele = getSourceElement(ev); var r = me.PauseInput(ele.mId); if (r === true) ele.src = "/img/play.png"; else ele.src = "/img/pause.png"; }
			//					bar.appendChild(z);


			//					z = document.createElement("img");
			//					z.src = "/img/rewind.png";
			//					z.title = "rewind";
			//					z.id = "myRewindButton" + i;
			//					z.style.left = "1px";
			//					z.style.top = "40px";
			//					z.className = "uiButton";
			//					z.mId = i;
			//					z.onclick = function(ev) { var ele = getSourceElement(ev); var r = me.RewindInput(ele.mId); }
			//					bar.appendChild(z);

			//					z = document.createElement("img");
			//					z.src = "/img/speakerOn.png";
			//					z.title = "mute";
			//					z.id = "myMuteButton" + i;
			//					z.style.left = "1px";
			//					z.style.top = "70px";
			//					z.className = "uiButton";
			//					z.mId = i;
			//					z.onclick = function(ev) { var ele = getSourceElement(ev); var r = me.MuteInput(ele.mId); if (r === true) ele.src = "/img/speakerOff.png"; else ele.src = "/img/speakerOn.png"; }
			//					bar.appendChild(z);

			//				}
			//			}


			//			function ShaderToy(parentElement, editorParent, passParent)
			//			{
			//			if (parentElement == null) return;
			//			if (editorParent == null) return;
			//			if (passParent == null) return;

			//			this.mPassParent = passParent

			//				this.mNeedsSave = false;
			//			this.mAudioContext = null;
			//			this.mCreated = false;
			//			this.mGLContext = null;
			//			this.mHttpReq = null;
			//			this.mEffect = null;
			//			this.mTo = null;
			//			this.mTOffset = 0;
			//			this.mCanvas = null;
			//			this.mFpsFrame = 0;
			//			this.mFpsTo = null;
			//			this.mIsPaused = false;
			//			this.mForceFrame = false;
			//			this.mInfo = null;
			//			this.mCharCounter = document.getElementById("numCharacters");
			//			this.mDocs = { };
			//			this.mActiveDoc = 0;

			//			this.mIsEditorFullScreen = false;
			//			this.mFontSize = 0;


			//			buildInputsUI(this);

			//			this.mCanvas = document.getElementById("demogl");
			//			this.mCanvas.tabIndex = "0"; // make it react to keyboard
			//this.mCanvas.width = this.mCanvas.offsetWidth;
			//			this.mCanvas.height = this.mCanvas.offsetHeight;


			//			this.mHttpReq = createHttpReques();
			//			this.mTo = (new Date()).getTime();
			//			this.mTf = 0;
			//			this.mFpsTo = this.mTo;
			//			this.mMouseIsDown = false;
			//			this.mMouseOriX = 0;
			//			this.mMouseOriY = 0;
			//			this.mMousePosX = 0;
			//			this.mMousePosY = 0;

			//// --- rendering context ---------------------

			//this.mGLContext = createGlContext(this.mCanvas, false, true);
			//			if (this.mGLContext == null)
			//			{
			//				createNoWebGLMessage(parentElement, this.mCanvas);
			//				this.mIsPaused = true;
			//				this.mForceFrame = false;
			//			}


			//// --- audio context ---------------------

			//try
			//			{
			//				if (window.AudioContext) this.mAudioContext = new AudioContext();
			//				if (this.mAudioContext == null && window.webkitAudioContext) this.mAudioContext = new webkitAudioContext();

			//			}
			//			catch (e)
			//			{
			//				this.mAudioContext = null;
			//			}

			//			if (this.mAudioContext == null)
			//			{
			//	//alert( "no audio!" );
			//}

			//			var me = this;

			//			window.onfocus = function()
			//			    {
			//				if (!this.mIsPaused)
			//				{
			//					me.mTOffset = me.mTf;
			//					me.mTo = (new Date()).getTime();
			//				}
			//			};

			//			var refreshChars = function()
			//			    {
			//				me.setChars();
			//				setTimeout(refreshChars, 1000);
			//			}
			//// ---------------

			//this.mErrors = new Array();
			//			this.mCodeEditor = CodeMirror(editorParent,
			//			                                   {
			//				lineNumbers: true,
			//												   matchBrackets: true,
			//												   indentWithTabs: false,
			//												   tabSize: 4,
			//												   indentUnit: 4,
			//												   mode: "text/x-glsl",
			//												   foldGutter: true,
			//												   gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"],
			//												   extraKeys: {
			//				"Ctrl-S":    function(instance) { doSaveShader(); me.mNeedsSave = false; } ,
			//                                                    "Alt-Enter": function(instance) { me.SetShaderFromEditor(); },
			//                                                    "Alt--": function(instance) { me.decreaseFontSize(); },
			//                                                    "Alt-=": function(instance) { me.increaseFontSize(); },
			//                                                    "Alt-F":     function(instance) { me.changeEditorFullScreen(); }
			//			}
			//		} );
			//    this.mCodeEditor.on( "change", function(instance, ev) { me.mNeedsSave = true; } );

			//	//--------------

			//	refreshChars(this);


			//    this.mCanvas.onmousedown = function(ev)
			//		{
			//			var pos = getCoords(me.mCanvas);
			//			me.mMouseOriX = (ev.pageX - pos.mX) * me.mCanvas.width / me.mCanvas.offsetWidth;
			//			me.mMouseOriY = me.mCanvas.height - (ev.pageY - pos.mY) * me.mCanvas.height / me.mCanvas.offsetHeight;
			//			me.mMousePosX = me.mMouseOriX;
			//			me.mMousePosY = me.mMouseOriY;
			//			me.mForceFrame = true;
			//			me.mMouseIsDown = true;
			//			//        return false; // prevent mouse pointer change
			//		}
			//    this.mCanvas.onmousemove = function(ev)
			//		{
			//			if (me.mMouseIsDown)
			//			{
			//				var pos = getCoords(me.mCanvas);
			//				me.mMousePosX = (ev.pageX - pos.mX) * me.mCanvas.width / me.mCanvas.offsetWidth;
			//				me.mMousePosY = me.mCanvas.height - (ev.pageY - pos.mY) * me.mCanvas.height / me.mCanvas.offsetHeight;
			//				me.mForceFrame = true;
			//			}
			//		}


			//    this.mCanvas.onmouseup = function(ev)
			//		{
			//			me.mMouseIsDown = false;
			//			me.mForceFrame = true;
			//			me.mMouseOriX = -Math.abs(me.mMouseOriX);
			//			me.mMouseOriY = -Math.abs(me.mMouseOriY);
			//		}

			//    this.mCanvas.onkeydown = function(ev)
			//		{
			//			me.mEffect.SetKeyDown(me.mActiveDoc, ev.keyCode);
			//		}
			//    this.mCanvas.onkeyup = function(ev)
			//		{
			//			me.mEffect.SetKeyUp(me.mActiveDoc, ev.keyCode);
			//		}

			//		document.getElementById("myResetButton").onclick = function(ev )
			//		{
			//			me.resetTime();
			//		}
			//		document.getElementById("myPauseButton").onclick = function(ev )
			//		{
			//			me.pauseTime();
			//		}
			//		document.getElementById("myVolume").onclick = function(ev)
			//		{
			//			var res = me.mEffect.ToggleVolume();
			//			if (res)
			//				this.style.background = "url('/img/speakerOff.png')";
			//			else
			//				this.style.background = "url('/img/speakerOn.png')";

			//		}

			//    this.mCanvas.ondblclick = function(ev )
			//		{
			//			if (IsFullScreen() == false)
			//				RequestFullScreen(me.mCanvas);
			//			else
			//				exitFullScreen();
			//		}

			//		document.getElementById("myFullScreen").onclick = function(ev )
			//		{
			//			RequestFullScreen(me.mCanvas);
			//		}

			//    //-------------------------

			//    this.mEffect = new Effect(this.mAudioContext, this.mGLContext, this.mCanvas.width, this.mCanvas.height, this.RefreshTexturThumbail, this, false, false );
			//    this.mCreated = true;
			//}

			//	ShaderToy.prototype.setFontSize = function(id )
			//	{
			//		if (id < 0) id = 0;
			//		if (id > 3) id = 3;

			//		this.mFontSize = id;
			//		var edi = document.getElementById("editor");
			//		edi.style.fontSize = (100 + id * 35) + '%';
			//		this.mCodeEditor.refresh();
			//	}

			//	ShaderToy.prototype.decreaseFontSize = function()
			//	{
			//		this.setFontSize(this.mFontSize - 1);

			//		var ele = document.getElementById("uiFontSelector");
			//		ele.selectedIndex = this.mFontSize;
			//	}

			//	ShaderToy.prototype.increaseFontSize = function()
			//	{
			//		this.setFontSize(this.mFontSize + 1);

			//		var ele = document.getElementById("uiFontSelector");
			//		ele.selectedIndex = this.mFontSize;
			//	}



			//	ShaderToy.prototype.changeEditorFullScreen = function()
			//	{
			//		this.mIsEditorFullScreen = !this.mIsEditorFullScreen;

			//		var con = document.getElementById("content");
			//		var ele = document.getElementById("editorWrapper");
			//		var lef = document.getElementById("leftColumn");
			//		var rig = document.getElementById("rightColumn");

			//		if (this.mIsEditorFullScreen)
			//		{
			//			lef.style.visibility = "hidden";
			//			rig.style.visibility = "hidden";

			//			rig.removeChild(ele);
			//			con.appendChild(ele);

			//			ele.className = "yesFullScreen";
			//		}
			//		else
			//		{
			//			lef.style.visibility = "visible";
			//			rig.style.visibility = "visible";

			//			con.removeChild(ele);
			//			rig.appendChild(ele);

			//			ele.className = "noFullScreen";
			//		}
			//		this.mCodeEditor.focus();
			//	}

			//	ShaderToy.prototype.GetNeedSave = function()
			//	{
			//		return this.mNeedsSave;
			//	}
			//	ShaderToy.prototype.SetNeedSave = function(v)
			//	{
			//		this.mNeedsSave = v;
			//	}

			//	ShaderToy.prototype.startRendering = function()
			//	{
			//		var me = this;

			//		function renderLoop2()
			//	    {
			//			if (me.mGLContext == null) return;

			//			requestAnimFrame(renderLoop2);

			//			if (me.mIsPaused && !me.mForceFrame)
			//			{
			//				me.mEffect.UpdateInputs(me.mActiveDoc, false);
			//				return;
			//			}

			//			me.mForceFrame = false;
			//			var time = (new Date()).getTime();
			//			var ltime = me.mTOffset + time - me.mTo;

			//			if (me.mIsPaused) ltime = me.mTf; else me.mTf = ltime;

			//			me.mEffect.Paint(ltime / 1000.0, me.mMouseOriX, me.mMouseOriY, me.mMousePosX, me.mMousePosY, me.mIsPaused);
			//			me.mGLContext.flush();

			//			me.mFpsFrame++;

			//			document.getElementById("myTime").innerHTML = (ltime / 1000.0).toFixed(2);
			//			if ((time - me.mFpsTo) > 1000)
			//			{
			//				var ffps = 1000.0 * me.mFpsFrame / (time - me.mFpsTo);
			//				document.getElementById("myFramerate").innerHTML = ffps.toFixed(1) + " fps";
			//				me.mFpsFrame = 0;
			//				me.mFpsTo = time;
			//			}

			//		}

			//		renderLoop2();
			//	}

			//	ShaderToy.prototype.resize = function(xres, yres )
			//	{
			//		this.mCanvas.setAttribute("width", xres);
			//		this.mCanvas.setAttribute("height", yres);
			//		this.mCanvas.width = xres;
			//		this.mCanvas.height = yres;

			//		this.mEffect.SetSize(xres, yres);
			//		this.mForceFrame = true;
			//	}

			//	//---------------------------------


			//	ShaderToy.prototype.pauseTime = function()
			//	{
			//		var time = (new Date()).getTime();
			//		if (!this.mIsPaused)
			//		{
			//			document.getElementById("myPauseButton").style.background = "url('/img/play.png')";
			//			this.mIsPaused = true;
			//			this.mEffect.StopOutputs();
			//		}
			//		else
			//		{
			//			document.getElementById("myPauseButton").style.background = "url('/img/pause.png')";
			//			this.mTOffset = this.mTf;
			//			this.mTo = time;
			//			this.mIsPaused = false;
			//			this.mEffect.ResumeOutputs();
			//		}
			//	}

			//	ShaderToy.prototype.resetTime = function()
			//	{
			//		this.mTOffset = 0;
			//		this.mTo = (new Date()).getTime();
			//		this.mTf = 0;
			//		this.mFpsTo = this.mTo;
			//		this.mFpsFrame = 0;
			//		this.mForceFrame = true;
			//		this.mEffect.ResetTime();
			//	}


			//	ShaderToy.prototype.SetErrors = function(result, fromScript )
			//	{
			//		var eleWrapper = document.getElementById('editorWrapper');

			//		while (this.mErrors.length > 0)
			//		{
			//			var mark = this.mErrors.pop();
			//			this.mCodeEditor.removeLineWidget(mark);
			//		}

			//		var eleWrapper = document.getElementById('editorWrapper');

			//		if (result == null)
			//		{
			//			this.mForceFrame = true;
			//			if (fromScript == false)
			//			{
			//				eleWrapper.className = "errorNo";
			//				setTimeout(function() { eleWrapper.className = ""; }, 500 );
			//			}
			//		}
			//		else
			//		{
			//			eleWrapper.className = "errorYes";

			//			var lineOffset = this.mEffect.GetHeaderSize(this.mActiveDoc);
			//			var lines = result.match(/^.* ((\r\n |\n |\r) |$)/ gm);
			//			for (var i = 0; i < lines.length; i++)
			//			{
			//				var parts = lines[i].split(":");
			//				if (parts.length === 5 || parts.length === 6)
			//				{
			//					var lineNumber = parseInt(parts[2]) - lineOffset;
			//					var msg = document.createElement("div");
			//					msg.appendChild(document.createTextNode(parts[3] + " : " + parts[4]));
			//					msg.className = "errorMessage";
			//					var mark = this.mCodeEditor.addLineWidget(lineNumber - 1, msg, { coverGutter: false, noHScroll: true} );

			//			this.mErrors.push(mark);
			//		}
			//            else if (lines[i] != null && lines[i] != "" && lines[i].length > 1 && parts[0] != "Warning")
			//		{
			//			console.log(parts.length + " **" + lines[i]);

			//			var txt = "";
			//			if (parts.length == 4)
			//				txt = parts[2] + " : " + parts[3];
			//			else
			//				txt = "Unknown error";

			//			var msg = document.createElement("div");
			//			msg.appendChild(document.createTextNode(txt));
			//			msg.className = "errorMessage";
			//			var mark = this.mCodeEditor.addLineWidget(0, msg, { coverGutter: false, noHScroll: true, above: true} );
			//			this.mErrors.push(mark);

			//		}
			//	}
			//}
			//}

			//ShaderToy.prototype.PauseInput = function(id )
			//{
			//	return this.mEffect.PauseInput(this.mActiveDoc, id);
			//}

			//ShaderToy.prototype.MuteInput = function(id )
			//{
			//	return this.mEffect.MuteInput(this.mActiveDoc, id);
			//}

			//ShaderToy.prototype.RewindInput = function(id )
			//{
			//	this.mEffect.RewindInput(this.mActiveDoc, id);
			//}

			//ShaderToy.prototype.SetTexture = function(slot, url )
			//{
			//	this.mEffect.NewTexture(this.mActiveDoc, slot, url);
			//}


			//ShaderToy.prototype.SetShaderFromEditor = function()
			//{
			//	var shaderCode = this.mCodeEditor.getValue();

			//	this.setChars();

			//	var result = this.mEffect.NewShader(shaderCode, this.mActiveDoc);
			//	if (result == null)
			//	{
			//		this.mForceFrame = true;
			//	}

			//	return this.SetErrors(result, false);
			//}


			//ShaderToy.prototype.RefreshTexturThumbail = function(myself, slot, img, forceFrame, gui, guiID, time )
			//{
			//	var canvas = document.getElementById('myUnitCanvas' + slot);

			//	var w = canvas.width;
			//	var h = canvas.height;

			//	var ctx = canvas.getContext('2d');
			//	if (img == null)
			//	{
			//		ctx.fillStyle = "#000000";
			//		ctx.fillRect(0, 0, w, h);

			//		if (guiID == 2)
			//		{
			//			ctx.strokeStyle = "#808080";
			//			ctx.lineWidth = 1;
			//			ctx.beginPath();
			//			var num = w / 2;
			//			for (var i = 0; i < num; i++)
			//			{
			//				var y = Math.sin(64.0 * 6.2831 * i / num + time) * Math.sin(2.0 * 6.2831 * i / num + time);
			//				var ix = w * i / num;
			//				var iy = h * (0.5 + 0.4 * y);
			//				if (i == 0) ctx.moveTo(ix, iy);
			//				else ctx.lineTo(ix, iy);
			//			}
			//			ctx.stroke();

			//			var str = "Audio error";
			//			ctx.font = "normal bold 20px Arial";
			//			ctx.lineWidth = 4;
			//			ctx.strokeStyle = "#000000";
			//			ctx.strokeText(str, 14, h / 2);
			//			ctx.fillStyle = "#ff0000";
			//			ctx.fillText(str, 14, h / 2);

			//			document.getElementById("myPauseButton" + slot).src = "/img/pause.png";
			//		}
			//	}
			//	else
			//	{
			//		if (guiID == 0 || guiID == 1 || guiID == 3)
			//		{
			//			ctx.fillStyle = "#000000";
			//			ctx.fillRect(0, 0, w, h);
			//			ctx.drawImage(img, 0, 0, w, h);
			//		}
			//		else if (guiID == 2)
			//		{
			//			ctx.fillStyle = "#000000";
			//			ctx.fillRect(0, 0, w - 24, h);

			//			ctx.fillStyle = "#ffffff";

			//			var numfft = img.length; numfft /= 2; if (numfft > 512) numfft = 512;
			//			var num = 32;
			//			var numb = (numfft / num) | 0;
			//			var s = ((w - 24 - 8 * 2) / num);
			//			var k = 0;
			//			for (var i = 0; i < num; i++)
			//			{
			//				var f = 0.0;
			//				for (var j = 0; j < numb; j++)
			//				{
			//					f += img[k++];
			//				}
			//				f /= numb;
			//				f /= 255.0;

			//				var fr = f;
			//				var fg = 4.0 * f * (1.0 - f);
			//				var fb = 1.0 - f;

			//				var rr = (255.0 * fr) | 0;
			//				var gg = (255.0 * fg) | 0;
			//				var bb = (255.0 * fb) | 0;
			//				//             ctx.fillStyle = "rgb(" + rr + "," + gg + "," + bb + ");"

			//				var decColor = 0x1000000 + bb + 0x100 * gg + 0x10000 * rr;
			//				ctx.fillStyle = '#' + decColor.toString(16).substr(1);


			//				var a = Math.max(2, f * (h - 2 * 20));
			//				ctx.fillRect(8 + i * s, h - 20 - a, 3 * s / 4, a);
			//			}
			//		}
			//		else if (guiID == 4)
			//		{
			//			/*
			//					 ctx.fillStyle = "#404040";
			//					 ctx.fillRect(0,0,w,h);
			//					 ctx.lineWidth = 2;
			//					 ctx.strokeStyle = "#ffffff";
			//					 ctx.strokeRect(w/10,3*h/10,8*w/10,5*h/10);
			//					 ctx.fillStyle = "#ffffff";
			//					 var s = (7*w/10)/(2*12);
			//					 for( var i=0; i<48; i++ )
			//					 {
			//						 var u = (i%12) | 0;
			//						 var v = (i/12) | 0;
			//						 ctx.fillRect( w/10+s+s*2*u, 3*h/10 + s + s*2*v, s, s );
			//					 }
			//			  */
			//			var thereskey = false;
			//			ctx.fillStyle = "#ffffff";
			//			for (var i = 0; i < 256; i++)
			//			{
			//				var x = (w * i / 256) | 0;
			//				if (img.mData[i] > 0)
			//				{
			//					thereskey = true;
			//					//ctx.fillRect( x, 0+h/4, 1, h/2 );
			//					break;
			//				}
			//			}


			//			ctx.fillStyle = "#000000";
			//			ctx.fillRect(0, 0, w, h);

			//			ctx.drawImage(img.mImage, 0, 20, w, h - 20);

			//			if (thereskey)
			//			{
			//				ctx.fillStyle = "#ff8040";
			//				ctx.globalAlpha = 0.4;
			//				ctx.fillRect(0, 0, w, h);
			//				ctx.globalAlpha = 1.0;
			//			}

			//		}

			//	}

			//	ctx.font = "normal normal 12px Arial";
			//	ctx.strokeStyle = "#000000";
			//	ctx.fillStyle = "#000000";
			//	ctx.lineWidth = 4;
			//	ctx.strokeText("iChannel" + slot, 4, 14);
			//	ctx.fillStyle = "#ffffff";
			//	ctx.strokeStyle = "#ffffff";
			//	ctx.fillText("iChannel" + slot, 4, 14);

			//	if (time > 0.0)
			//	{
			//		var str = time.toFixed(2) + "s";
			//		ctx.font = "normal normal 10px Arial";
			//		ctx.strokeStyle = "#000000";
			//		ctx.lineWidth = 4;
			//		ctx.strokeText(str, 4, 96);
			//		ctx.fillStyle = "#ffffff";
			//		ctx.fillText(str, 4, 96);
			//	}

			//	//--------------

			//	if (gui == true)
			//	{
			//		var ele = document.getElementById("inputSelectorControls" + slot);

			//		if (guiID == 0) ele.style.visibility = "hidden";
			//		if (guiID == 1) ele.style.visibility = "visible";
			//		if (guiID == 2) ele.style.visibility = "visible";
			//		if (guiID == 3) ele.style.visibility = "visible";

			//		if (guiID == 3)
			//		{
			//			var me = this;

			//			var ele1 = document.getElementById("myPauseButton" + slot);
			//			ele1.src = "/img/next.png";
			//			ele1.title = "next";
			//			ele1.onclick = function(ev) { var ele = getSourceElement(ev); var r = me.PauseInput(ele.mId); }


			//			var ele2 = document.getElementById("myRewindButton" + slot);
			//			ele2.src = "/img/previous.png";
			//			ele2.title = "previous";
			//			ele2.onclick = function(ev) { var ele = getSourceElement(ev); var r = me.RewindInput(ele.mId); }

			//			var ele3 = document.getElementById("myMuteButton" + slot);
			//			ele3.src = "/img/rewind.png";
			//			ele3.title = "rewind";
			//			ele3.onclick = function(ev) { var ele = getSourceElement(ev); var r = me.MuteInput(ele.mId); }
			//		}

			//	}

			//	//--------------

			//	myself.mForceFrame = forceFrame;
			//}

			//function isSpace(str, i )
			//{
			//	return (str[i] === ' ') || (str[i] === '\t');
			//}
			//function isLine(str, i )
			//{
			//	return (str[i] === '\n');
			//}

			//function replaceChars(str)
			//{
			//	var dst = "";
			//	var num = str.length;
			//	for (var i = 0; i < num; i++)
			//	{
			//		if (str[i] === '\r') continue;
			//		if (str[i] === '\t') { dst = dst + " "; continue; }

			//		dst = dst + str[i];
			//	}
			//	return dst;
			//}


			//function removeEmptyLines(str)
			//{
			//	var dst = "";
			//	var num = str.length;
			//	var isPreprocessor = false;
			//	for (var i = 0; i < num; i++)
			//	{
			//		if (str[i] === '#') isPreprocessor = true;
			//		var isDestroyableChar = isLine(str, i);

			//		if (isDestroyableChar && !isPreprocessor) continue;
			//		if (isDestroyableChar && isPreprocessor) isPreprocessor = false;

			//		dst = dst + str[i];
			//	}
			//	return dst;
			//}

			//function removeMultiSpaces(str)
			//{
			//	var dst = "";
			//	var num = str.length;
			//	for (var i = 0; i < num; i++)
			//	{
			//		if (isSpace(str, i) && (i === (num - 1))) continue;
			//		if (isSpace(str, i) && isLine(str, i - 1)) continue;
			//		if (isSpace(str, i) && isLine(str, i + 1)) continue;
			//		if (isSpace(str, i) && isSpace(str, i + 1)) continue;
			//		dst = dst + str[i];
			//	}
			//	return dst;
			//}
			//function removeSingleSpaces(str)
			//{
			//	var dst = "";
			//	var num = str.length;
			//	for (var i = 0; i < num; i++)
			//	{
			//		if (i > 0)
			//		{
			//			if (isSpace(str, i) && ((str[i - 1] === ';') ||
			//									(str[i - 1] === ',') ||
			//									(str[i - 1] === '}') ||
			//									(str[i - 1] === '{') ||
			//									(str[i - 1] === '(') ||
			//									(str[i - 1] === ')') ||
			//									(str[i - 1] === '+') ||
			//									(str[i - 1] === '-') ||
			//									(str[i - 1] === '*') ||
			//									(str[i - 1] === '/') ||
			//									(str[i - 1] === '?') ||
			//									(str[i - 1] === '<') ||
			//									(str[i - 1] === '>') ||
			//									(str[i - 1] === '[') ||
			//									(str[i - 1] === ']') ||
			//									(str[i - 1] === ':') ||
			//									(str[i - 1] === '=') ||
			//									(str[i - 1] === '^') ||
			//									(str[i - 1] === '\n') ||
			//									(str[i - 1] === '\r')

			//								))
			//				continue;
			//		}

			//		if (isSpace(str, i) && ((str[i + 1] === ';') ||
			//								(str[i + 1] === ',') ||
			//								(str[i + 1] === '}') ||
			//								(str[i + 1] === '{') ||
			//								(str[i + 1] === '(') ||
			//								(str[i + 1] === ')') ||
			//								(str[i + 1] === '+') ||
			//								(str[i + 1] === '-') ||
			//								(str[i + 1] === '*') ||
			//								(str[i + 1] === '/') ||
			//								(str[i + 1] === '?') ||
			//								(str[i + 1] === '<') ||
			//								(str[i + 1] === '>') ||
			//								(str[i + 1] === '[') ||
			//								(str[i + 1] === ']') ||
			//								(str[i + 1] === ':') ||
			//								(str[i + 1] === '=') ||
			//								(str[i + 1] === '^') ||
			//								(str[i + 1] === '\n') ||
			//								(str[i + 1] === '\r')

			//							))
			//			continue;

			//		dst = dst + str[i];
			//	}
			//	return dst;
			//}

			//function removeSingleComments(str)
			//{
			//	var dst = "";
			//	var num = str.length;
			//	var detected = false;
			//	for (var i = 0; i < num; i++)
			//	{
			//		if (i <= (num - 2))
			//		{
			//			if (str[i] === '/' && str[i + 1] === '/')
			//				detected = true;
			//		}

			//		if (detected && (str[i] === "\n" || str[i] === "\r"))
			//			detected = false;

			//		if (!detected)
			//			dst = dst + str[i];
			//	}
			//	return dst;
			//}

			//function removeMultiComments(str)
			//{
			//	var dst = "";
			//	var num = str.length;
			//	var detected = false;
			//	for (var i = 0; i < num; i++)
			//	{
			//		if (i <= (num - 2))
			//		{
			//			if (str[i] === '/' && str[i + 1] === '*')
			//			{
			//				detected = true;
			//				continue;
			//			}

			//			if (detected && str[i] === "*" && str[i + 1] === "/")
			//			{
			//				detected = false;
			//				i += 2;
			//				continue;
			//			}
			//		}

			//		if (!detected)
			//			dst = dst + str[i];
			//	}
			//	return dst;
			//}

			//ShaderToy.prototype.setChars = function()
			//{
			//	var str = this.mCodeEditor.getValue();

			//	str = replaceChars(str);
			//	str = removeSingleComments(str);
			//	str = removeMultiComments(str);
			//	str = removeMultiSpaces(str);
			//	str = removeSingleSpaces(str);
			//	str = removeEmptyLines(str);
			//	//console.log( "***"+str+"***" );

			//	this.mCharCounter.innerHTML = str.length + " chars";
			//}

			//ShaderToy.prototype.showChars = function()
			//{
			//	var str = this.mCodeEditor.getValue();

			//	str = replaceChars(str);
			//	str = removeSingleComments(str);
			//	str = removeMultiComments(str);
			//	str = removeMultiSpaces(str);
			//	str = removeSingleSpaces(str);
			//	str = removeEmptyLines(str);

			//	//alert( str );
			//	var ve = document.getElementById("centerScreen");
			//	doAlert(getCoords(ve), { mX: 480,mY: 400}, "Minimal Shader Code, (" + str.length + " chars)", "<pre>" + str + "</pre>", false, null );
			//}


			//ShaderToy.prototype.ChangePass = function(id )
			//{
			//	this.mActiveDoc = id;
			//	this.mCodeEditor.swapDoc(this.mDocs[id]);
			//	this.setChars();
			//	this.mEffect.UpdateInputs(id, true);

			//	//this.SetErrors( res[0].mError, true );
			//	var num = 2;
			//	for (var i = 0; i < num; i++)
			//	{
			//		var eleLab = document.getElementById("tab" + i);
			//		if (eleLab == null) break;
			//		if (i == id)
			//			eleLab.className = "tab selected";
			//		else
			//			eleLab.className = "tab";
			//	}
			//}

			//ShaderToy.prototype.KillPass = function(id )
			//{
			//	this.mEffect.DestroyPass(id);

			//	var eleLab = document.getElementById("tab" + id);
			//	this.mPassParent.removeChild(eleLab);
			//	this.AddPlusTab();

			//	if (id == this.mActiveDoc)
			//	{
			//		this.ChangePass((id - 1 < 0) ? 0 : id - 1);
			//	}
			//}

			//ShaderToy.prototype.AddPass = function()
			//{
			//	var res = this.mEffect.AddPass("sound");
			//	var id = res.mId;

			//	if (this.mEffect.GetNumPasses() > 1)
			//	{
			//		var eleLab = document.getElementById("tabAdd");
			//		if (eleLab != null)
			//			this.mPassParent.removeChild(eleLab);
			//	}

			//	this.AddTab("sound", id, true);

			//	this.mDocs[id] = CodeMirror.Doc(res.mShader, "text/x-glsl");

			//	this.ChangePass(id);
			//	/*

			//		//-------
			//		this.mActiveDoc = id;

			//		this.mCodeEditor.swapDoc( this.mDocs[this.mActiveDoc] );
			//		this.setChars();
			//		this.mCodeEditor.clearHistory()
			//		this.SetErrors( res.mError, true );
			//	  */
			//}

			////-------------------------

			//ShaderToy.prototype.AddPlusTab = function(passes )
			//{
			//	var me = this;
			//	var num = this.mEffect.GetNumPasses();

			//	if (num > 1)
			//	{
			//		var eleLab = document.getElementById("tabAdd");
			//		if (eleLab != null)
			//			this.mPassParent.removeChild(eleLab);
			//	}
			//	else
			//	{
			//		var eleTab = document.createElement("div");
			//		eleTab.id = "tabAdd";
			//		eleTab.className = "tabAdd";
			//		eleTab.onclick = function(ev) { me.AddPass(); ev.stopPropagation(); }

			//		var eleImg = document.createElement("img");
			//		eleImg.className = "tabImage";
			//		eleImg.src = "/img/add.png";
			//		eleTab.appendChild(eleImg);

			//		this.mPassParent.appendChild(eleTab);
			//	}
			//}

			//ShaderToy.prototype.AddTab = function(passType, id, isSelected )
			//{
			//	var me = this;

			//	var eleTab = document.createElement("div");
			//	eleTab.mNum = id;
			//	eleTab.onclick = function(ev) { me.ChangePass(this.mNum); }
			//	eleTab.id = "tab" + id;
			//	eleTab.className = "tab";
			//	if (isSelected)
			//		eleTab.className = "tab selected";

			//	var eleImg = document.createElement("img");
			//	eleImg.className = "tabImage";
			//	if (passType == "sound") eleImg.src = "/img/music.png";
			//	if (passType == "image") eleImg.src = "/img/image.png";
			//	eleTab.appendChild(eleImg);

			//	var eleLab = document.createElement("label");
			//	eleLab.textContent = passType;
			//	if (passType == "image") eleLab.textContent = "Image";
			//	if (passType == "sound") eleLab.textContent = "Sound";
			//	eleTab.appendChild(eleLab);

			//	if (id > 0)
			//	{
			//		eleImg = document.createElement("img");
			//		eleImg.src = "/img/closeSmall.png";
			//		eleImg.className = "tabClose";
			//		eleImg.mNum = id;
			//		eleImg.onclick = function(ev) { me.KillPass(this.mNum); ev.stopPropagation(); }
			//		eleTab.appendChild(eleImg);
			//	}

			//	this.mPassParent.appendChild(eleTab);
			//}


			//ShaderToy.prototype.SetPasses = function(passes )
			//{
			//	for (var i = 0; i < passes.length; i++)
			//		this.AddTab(passes[i].mType, i, i == 0);
			//	this.AddPlusTab();
			//}

			//ShaderToy.prototype.newScriptJSON = function(jsn )
			//{
			//	try
			//	{
			//		var res = this.mEffect.newScriptJSON(jsn);


			//		var num = res.length;
			//		for (var i = 0; i < num; i++)
			//		{
			//			this.mDocs[i] = CodeMirror.Doc(res[i].mShader, "text/x-glsl");
			//		}

			//		this.mActiveDoc = 0;

			//		this.mCodeEditor.swapDoc(this.mDocs[this.mActiveDoc]);
			//		this.setChars();
			//		this.mCodeEditor.clearHistory()
			//        this.SetErrors(res[this.mActiveDoc].mError, true);

			//		this.SetPasses(res);
			//		this.resetTime();

			//		this.mInfo = jsn.info;

			//		return {
			//			mFailed: res.mFailed,
			//                 mDate: jsn.info.date,
			//                 mViewed: jsn.info.viewed,
			//                 mName: jsn.info.name,
			//                 mUserName: jsn.info.username,
			//                 mDescription: jsn.info.description,
			//                 mLikes: jsn.info.likes,
			//                 mPublished: jsn.info.published,
			//                 mHasLiked: jsn.info.hasliked,
			//                 mTags: jsn.info.tags };

			//	}
			//	catch (e)
			//	{
			//		return { mFailed: true };
			//	}

			//}


			//ShaderToy.prototype.exportToJSON = function()
			//{
			//	var res = this.mEffect.exportToJSON();

			//	if (this.mNeedsSave)
			//	{
			//		for (var i = 0; i < res.renderpass.length; i++)
			//			res.renderpass[i].code = this.mDocs[i].getValue();
			//	}

			//	res.info = this.mInfo;

			//	return res;
			//}


			////----------------------------------------------------------------------------

			//var gShaderToy = null;
			//var gCode = null;
			//var gIsLiked = 0;
			//var gRes = null;
			//var gNnumComments = 0;

			//function loadNew()
			//{
			//	var xmlC = "[]";
			//	var kk = {
			//				"ver":"0.1",
			//		    	"info":{
			//		"id":"-1",
			//		    		"date":"1358124981",
			//		    		"viewed":0,
			//		    		"name":"",
			//		    		"username": "None",
			//		    		"description":"",
			//		    		"likes":0,
			//                    "hasliked":0,
			//		    		"tags":[],
			//		    		"published":0
			//		    		},
			//		    	"renderpass":[
			//		    		{"inputs":
			//		    			{"id":"3",
			//		    			"src":["/presets/tex03.jpg"],
			//		    			"ctype":["texture"],
			//		    			"channel":[0]
			//		    			},
			//		    		"outputs":
			//		    			{"channel":[],
			//		    			"dst":[]
			//		    			},
			//                    "type":"image",
			//		    		"code":"void mainImage( out vec4 fragColor, in vec2 fragCoord )\n{\n\tvec2 uv = fragCoord.xy / iResolution.xy;\n\tfragColor = vec4(uv,0.5+0.5*sin(iGlobalTime),1.0);\n}",
			//		    		"name":"",
			//		    		"description":""
			//		    		}]
			//		    		};

			//    var xmlS = JSON.stringify(kk, null);

			//    return { mComments:xmlC, mShader: "[" + xmlS + "]" };
			//}

			//function loadShader()
			//{
			//	// -- comments --------------------------------------------------------

			//	var xmlC = null;
			//	try
			//	{
			//		var httpReq = createHttpReques();
			//		httpReq.open("POST", "/comment", false);
			//		httpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
			//		httpReq.send("s=" + gShaderID);
			//		xmlC = httpReq.responseText;
			//	}
			//	catch (e)
			//	{
			//	}

			//	// -- shader --------------------------------------------------------

			//	var xmlS = null;
			//	try
			//	{
			//		var httpReq = createHttpReques();
			//		httpReq.open("POST", "/shadertoy", false);
			//		httpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

			//		var str = "{ \"shaders\" : [\"" + gShaderID + "\"] }";
			//		str = "s=" + encodeURIComponent(str);

			//		httpReq.send(str);
			//		xmlS = httpReq.responseText;
			//	}
			//	catch (e)
			//	{
			//	}

			//	return { mComments: xmlC, mShader: xmlS };
			//}

			//function watchResize()
			//{
			//	var srdiv = document.getElementById("demogl");
			//	if (srdiv)
			//	{
			//		var xres = srdiv.offsetWidth;
			//		var yres = srdiv.offsetHeight;
			//		gShaderToy.resize(xres, yres);
			//	}
			//}

			//function changeEditorFont()
			//{
			//	var ele = document.getElementById("uiFontSelector");
			//	gShaderToy.setFontSize(ele.selectedIndex);
			//}

			//function watchInit()
			//{
			//	var editorHeader = document.getElementById("editorHeader");
			//	editorHeader.mExpanded = false;

			//	//-- shadertoy --------------------------------------------------------

			//	var editorParent = document.getElementById("editor");
			//	var viewerParent = document.getElementById("player");
			//	var passParent = document.getElementById("passManager")

			//	// cancel/intercept browsers default behaviour for CTRL+S (save the web page to disk)
			//	document.addEventListener("keydown", function(e){ if (e.keyCode == 83 && (navigator.platform.match("Mac") ? e.metaKey : e.ctrlKey)) { e.preventDefault(); } }, false);

			//	// cancel/intercept browsers default behaviour for CTRL+F (search)
			//	document.addEventListener("keydown", function(e){ if (e.keyCode == 70 && (navigator.platform.match("Mac") ? e.metaKey : e.ctrlKey)) { e.preventDefault(); } }, false);

			//	// prevent unloading page without saving changes to shader
			//	window.onbeforeunload = function(e) { if (gShaderToy != null && gShaderToy.GetNeedSave()) return "You are about to lose your changes in the code editor."; };

			//	gShaderToy = new ShaderToy(viewerParent, editorParent, passParent);
			//	if (!gShaderToy.mCreated)
			//		return;

			//	//-- get info --------------------------------------------------------

			//	var res = { mComments:null, mShader: null };

			//    if( gShaderID==null )
			//    {
			//         res = loadNew();
			//    }
			//    else
			//    {
			//         res = loadShader(gShaderID );
			//    }

			//    //-- comments --------------------------------------------------------

			//    if( res.mComments == null )
			//        return;

			//    var jsn = JSON.parse(res.mComments);

			//	updatepage(jsn );

			//    //-- shader --------------------------------------------------------

			//    if( res.mShader==null ) return;

			//    var jsnShader = null;
			//    try { jsnShader = JSON.parse( res.mShader ); } catch(e) { alert( "ERROR in JSON: " + res.mShader ); return; }

			//    gRes = gShaderToy.newScriptJSON( jsnShader[0] )

			//    document.title = gRes.mName;

			//    var st = document.getElementById("shaderTitle");      if( st ) { if( st.value === undefined ) { st.innerHTML = makeShort(gRes.mName, 23); st.title = gRes.mName; } else st.value = gRes.mName;        }
			//    var sd = document.getElementById("shaderDescription"); if( sd ) { if( sd.value === undefined ) { sd.innerHTML = gRes.mDescription;                                } else sd.value = gRes.mDescription; }
			//    var sp = document.getElementById("published");
			//    if( sp && sp!== undefined && sp.length==4 )
			//    {
			//        if( gRes.mPublished == 0 )
			//        {
			//            sp.selectedIndex = 3;
			//        }
			//        else if( gRes.mPublished == 1 )
			//        {
			//            sp.selectedIndex = 1;
			//        }
			//        else if( gRes.mPublished == 2 )
			//        {
			//            sp.selectedIndex = 2;
			//        }
			//        else if( gRes.mPublished == 3 )
			//        {
			//            sp.selectedIndex = 0;
			//        }
			//    }

			//	updateLikes();
			//var timeVar = "-";
			//    if( gRes.mDate != 0 )
			//    {
			//	    timeVar = getTime(gRes.mDate);
			//    }
			//    var shaderAuthor = document.getElementById("shaderAuthor"); if( shaderAuthor ) shaderAuthor.innerHTML = ((gShaderID==null) ? "Created" : "Uploaded" ) + " by <a class='user' href='/user/" + gRes.mUserName +"'>" + gRes.mUserName + "</a> in " + timeVar;

			//    var txtHtml = "Tags: ";
			//var txtPlain = "";
			//var numTags = gRes.mTags.length;
			//    for( var i = 0; i<numTags; i++ )
			//    {
			//        txtHtml += "<a class='user' href='/results?query=tag%3D" + gRes.mTags[i] + "'>" + gRes.mTags[i] + "</a>";
			//        txtPlain += gRes.mTags[i];
			//        if( i != (numTags-1) ) { txtHtml += ", "; txtPlain += ", "; }
			//    }
			//    var sts = document.getElementById("shaderTags"); if( sts ) { if( sts.value === undefined ) sts.innerHTML = txtHtml; else sts.value = txtPlain; }

			//    var shareShader = document.getElementById("shaderShare");

			//// like
			//var shaderLike = document.getElementById("shaderLike");
			//    if( shaderLike !=null )
			//    {
			//        gIsLiked = gRes.mHasLiked;
			//        updateLikes();
			//shaderLike.onclick = function()
			//{
			//	var url = "s=" + gShaderID + "&l=" + ((gIsLiked == 1) ? 0 : 1);
			//	var mHttpReq = createHttpReques();
			//	mHttpReq.open("POST", "/shadertoy", false);
			//	mHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
			//	mHttpReq.send(url);
			//	var res = mHttpReq.responseText;
			//	if (res == "true")
			//	{
			//		if (gIsLiked == 1) gRes.mLikes--; else gRes.mLikes++;
			//		gIsLiked = 1 - gIsLiked;
			//		updateLikes();
			//	}
			//}
			//    }

			//    gShaderToy.resetTime();
			//    gShaderToy.startRendering();
			//    if( gRes.mFailed )
			//        gShaderToy.pauseTime();

			//	postLoad();
			//}

			//function updateLikes()
			//{
			//	//if( gRes.mFailed ) return;

			//	var shaderLike = document.getElementById("shaderLike");
			//	if (shaderLike != null)
			//	{
			//		if (gIsLiked == 1)
			//		{
			//			shaderLike.src = "/img/likeYes.png";
			//		}
			//		else
			//		{
			//			shaderLike.src = "/img/likeNo.png";
			//		}
			//	}

			//	var shaderStats = document.getElementById("shaderStats");
			//	if (shaderStats)
			//		shaderStats.innerHTML = "<img src='/img/views.png' class='viewsIcon'></img><span style='font-weight:bold'>" + gRes.mViewed + "</span>    &nbsp;&nbsp;  <img src='/img/likes.png' class='likesIcon'></img><span style='font-weight:bold'>" + gRes.mLikes + "</span> &nbsp;&nbsp; <img src='/img/comments.png' class='commentsIcon'></img><span style='font-weight:bold'>" + gNnumComments;
			//}

			//function updatepage(jsn )
			//{

			//	var txt = "";
			//	gNnumComments = jsn.text ? jsn.text.length : 0;
			//	for (var i = 0; i < gNnumComments; i++)
			//	{
			//		var timeVar = "-";
			//		if (jsn.date[i] != 0)
			//		{
			//			timeVar = getTime(jsn.date[i]);
			//		}
			//		if ((i & 1) == 0)
			//			txt += "<div class=\"comment1\">";
			//		else
			//			txt += "<div class=\"comment2\">";
			//		txt += "<table width=\"100%\"><tr><td class=\"commentPicture\"><img class=\"userPictureSmall\" src=\"" + jsn.userpicture[i] + "\"></img></td>";
			//		txt += "<td class=\"commentContent\"><a class='user' href='/user/" + jsn.username[i] + "'>" + jsn.username[i] + "</a>, " + timeVar + "<br>" + jsn.text[i] + "</td></tr></table>";
			//		txt += "</div>";
			//	}

			//	var cc = document.getElementById("myComments"); if (cc) cc.innerHTML = txt;
			//	var dd = document.getElementById("commentTextArea"); if (dd) dd.value = "";
			//}

			//function addComment(usr, form)
			//{
			//	var xmlHttp = createHttpReques();
			//	if (xmlHttp == null) return;

			//	// disable comment input elements while we process the comment submision
			//	form.mybutton.disabled = true;
			//	form.comment.disabled = true;

			//	// encode comments
			//	var commentsformated = form.comment.value;
			//	commentsformated = encodeURIComponent(commentsformated);


			//	var url = "s=" + usr + "&comment=" + commentsformated;
			//	xmlHttp.open('POST', "/comment", true);
			//	xmlHttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
			//	xmlHttp.onreadystatechange = function()
			//    {
			//		if (xmlHttp.readyState == 4)
			//		{
			//			var str = xmlHttp.responseText;
			//			var jsn = JSON.parse(str);

			//			if (jsn.added && jsn.added == 0)
			//			{
			//				var ve = document.getElementById("centerScreen");
			//				doAlert(getCoords(ve), { mX: 320,mY: 100}, "Error", "We are sorry, we couldn't submit your comment", false, null );
			//			}
			//			else
			//			{
			//				updatepage(jsn);
			//			}

			//			// reenable comment input elements
			//			form.mybutton.disabled = false;
			//			form.comment.disabled = false;
			//		}
			//	}
			//	xmlHttp.send(url);
			//}

			//function checkFormComment(str)
			//{
			//	var str2 = strip_html(str);

			//	if (str != str2)
			//	{
			//		var ve = document.getElementById("centerScreen");
			//		doAlert(getCoords(ve), { mX: 320,mY: 100}, "Error", "HTML is not allowed here", false, null );
			//		return false;
			//	}

			//	if (str == "")
			//	{
			//		var ve = document.getElementById("centerScreen");
			//		doAlert(getCoords(ve), { mX: 320,mY: 100}, "Error", "You need to write at least 1 character", false, null );
			//		return false;
			//	}
			//	return true;
			//}

			//function validateComment(form )
			//{
			//	if (checkFormComment(form.comment.value))
			//	{
			//		addComment(gShaderID, form);
			//		return true;
			//	}

			//	form.comment.focus();
			//	return false;
			//}

			//function openSubmitShaderForm(isUpdate )
			//{
			//	var ve = document.getElementById("centerScreen");
			//	var s1 = document.getElementById('shaderTitle');
			//	var s2 = document.getElementById('shaderTags');
			//	var s3 = document.getElementById('shaderDescription');

			//	if (!s1.validity.valid) { doAlert(getCoords(ve), { mX: 320,mY: 100}, "Error", "You must give a name to your shader", false, null ); return; }
			//	if (!s2.validity.valid) { doAlert(getCoords(ve), { mX: 320,mY: 100}, "Error", "You must assign at least one tag to your shader", false, null ); return; }
			//	if (!s3.validity.valid) { doAlert(getCoords(ve), { mX: 320,mY: 100}, "Error", "You must give a description to your shader", false, null ); return; }
			//	if (!checkFormComment(s1.value)) return false;
			//	//if( !checkFormComment(s1.value) ) return false;
			//	if (!checkFormComment(s3.value)) return false;

			//	var publishedStatus = 3;
			//	var sp = document.getElementById('published');
			//	var op = sp.options[sp.selectedIndex].value;
			//	// HTML: 0: draft   1: private   2: public    3: public+api
			//	// DB  : 0: draft   2: private   1: public    3: public + api
			//	if (op == "0") publishedStatus = 0;
			//	else if (op == "1") publishedStatus = 2;
			//	else if (op == "2") publishedStatus = 1;
			//	else if (op == "3") publishedStatus = 3;

			//	var dataJSON = gShaderToy.exportToJSON();

			//	dataJSON.info.name = s1.value;
			//	dataJSON.info.tags = s2.value.split(",");
			//	dataJSON.info.description = s3.value;

			//	// Initial support for drafts
			//	dataJSON.info.published = publishedStatus;

			//	// Generate the screenshot
			//	var canvas = document.getElementById("demogl");
			//	var dataURL = canvas.toDataURL("image/jpeg");

			//	var dataTXT = JSON.stringify(dataJSON, null);
			//	dataTXT = encodeURIComponent(dataTXT);

			//	// Submit the values to the cloud
			//	var mHttpReq = createHttpReques();
			//	mHttpReq.open("POST", "/shadertoy", false);
			//	mHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

			//	var url = "a="; if (isUpdate) url = "u=";
			//	url += dataTXT;
			//	url += "&ss=" + dataURL;

			//	mHttpReq.send(url);
			//	var res = mHttpReq.responseText;


			//	if (res == 0)
			//	{
			//		gShaderToy.SetNeedSave(false);
			//		if (isUpdate)
			//		{
			//			//doAlert( getCoords(ve), {mX:400,mY:160}, "Update", "The shader was updated successfully", false, null );
			//			var eleWrapper = document.getElementById('editorWrapper');
			//			eleWrapper.className = "saved";
			//			setTimeout(function() { eleWrapper.className = ""; }, 500 );

			//		}
			//		else
			//		{
			//			window.location = "/profile";
			//		}
			//	}
			//	else if (res == -2)
			//	{
			//		doAlert(getCoords(ve), { mX: 400,mY: 160}, "Error", "Shader name \"" + dataJSON.info.name + "\" is already used by another shader. Please change the name of your shader.", false, null );
			//	}
			//	else
			//	{
			//		doAlert(getCoords(ve), { mX: 400,mY: 180}, "Error", "The shader could not be " + ((isUpdate == true) ? "updated" : "added") + ", please try again. Error code : " + res, false, null);
			//	}
			//}

			//function saveCodeFromAdmin()
			//{
			//	var dataJSON = gShaderToy.exportToJSON();
			//	var dataTXT = JSON.stringify(dataJSON, null);
			//	dataTXT = encodeURIComponent(dataTXT);

			//	var mHttpReq = createHttpReques();
			//	mHttpReq.open("POST", "/shadertoy", false);
			//	mHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

			//	var url = "z=" + dataTXT;

			//	mHttpReq.send(url);
			//	var res = mHttpReq.responseText;

			//	if (res == 0)
			//	{
			//		gShaderToy.SetNeedSave(false);
			//		//doAlert( getCoords(ve), {mX:400,mY:160}, "Update", "The shader was updated successfully", false, null );
			//		var eleWrapper = document.getElementById('editorWrapper');
			//		eleWrapper.className = "saved";
			//		setTimeout(function() { eleWrapper.className = ""; }, 500 );
			//	}
			//	else
			//	{
			//		var ve = document.getElementById("centerScreen");
			//		doAlert(getCoords(ve), { mX: 400,mY: 180}, "Error", "The shader could not be updated, please try again. Error code : " + res, false, null);
			//	}
			//}

			//function doReport(coords )
			//{
			//	var url = "s=" + gShaderID + "&r=1";
			//	var mHttpReq = createHttpReques();
			//	mHttpReq.open("POST", "/shadertoy", false);
			//	mHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
			//	mHttpReq.send(url);
			//	var res = mHttpReq.responseText;
			//	if (res == "true")
			//	{
			//		doReportShader(coords);
			//	}
			//	var ele = document.getElementById("shaderReport");
			//	ele.onclick = null;
			//}
			#endregion

			// what does it take to import those nice shaders into jsc world?

			// x:\jsc.svn\examples\javascript\webgl\webglchocolux\webglchocolux\application.cs

			// it looks there are no channels.

			// is it a vert or frag?
			//  fragColor = vec4( col, 1.0 );
			// must be a frag


			// 308
			// this.mEffect = new Effect( this.mAudioContext, this.mGLContext, this.mCanvas.width, this.mCanvas.height, this.RefreshTexturThumbail, this, false, false );
			// <body onload="watchInit()" 


			//public class ProgramFragmentShader : FragmentShader
			var fs = new Shaders.ProgramFragmentShader();

			//  res = loadShader( gShaderID );
			// updatepage( jsn );
			//  gRes = gShaderToy.newScriptJSON( jsnShader[0] )
			// EffectPass.prototype.NewTexture = function( wa, gl, slot, url )
			// EffectPass.prototype.MakeHeader_Image = function( precission, supportDerivatives )
			// var shaderStr = rpass.code;
			// EffectPass.prototype.NewShader = function( gl, shaderCode )
			// EffectPass.prototype.NewShader_Image = function( gl, shaderCode )


		}

	}
}
