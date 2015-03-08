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

namespace ChromeShaderToyColumns
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
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

            // what does it take to import those nice shaders into jsc world?

            // x:\jsc.svn\examples\javascript\webgl\webglchocolux\webglchocolux\application.cs

            // it looks there are no channels.

            // is it a vert or frag?
            //  fragColor = vec4( col, 1.0 );
            // must be a frag


            var fs = new Shaders.ProgramFragmentShader();
        }

    }
}
