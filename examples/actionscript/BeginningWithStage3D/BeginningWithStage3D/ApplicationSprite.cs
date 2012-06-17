using System;
using com.adobe.utils;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.display3D;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;

#region [BeginningWithStage3D.AssetsLibrary]
namespace com.adobe.utils
{
    [Script(IsNative = true)]
    [BeginningWithStage3D.AssetsLibrary.SWCImport]
    public class AGALMiniAssembler
    {
        protected static object COMPONENTS;
        protected static RegExp REGEXP_OUTER_SPACES;
        protected static bool USE_NEW_SYNTAX;
        public bool verbose;

        public AGALMiniAssembler(bool debugging = false)
        { 
        }

        public ByteArray agalcode { get; set; }
        public string error { get; set;  }

        public ByteArray assemble(string mode, string source) {
            return default(ByteArray);
        }
    }

    [Script(IsNative = true)]
    [BeginningWithStage3D.AssetsLibrary.SWCImport]
    public class PerspectiveMatrix3D : Matrix3D
    {
        public PerspectiveMatrix3D(Vector<double> v = null)
        { }

        public void lookAtLH(Vector3D eye, Vector3D at, Vector3D up) { }
        public void lookAtRH(Vector3D eye, Vector3D at, Vector3D up) { }
        public void orthoLH(double width, double height, double zNear, double zFar) { }
        public void orthoOffCenterLH(double left, double right, double bottom, double top, double zNear, double zFar) { }
        public void orthoOffCenterRH(double left, double right, double bottom, double top, double zNear, double zFar) { }
        public void orthoRH(double width, double height, double zNear, double zFar) { }
        public void perspectiveFieldOfViewLH(double fieldOfViewY, double aspectRatio, double zNear, double zFar) { }
        public void perspectiveFieldOfViewRH(double fieldOfViewY, double aspectRatio, double zNear, double zFar) { }
        public void perspectiveLH(double width, double height, double zNear, double zFar) { }
        public void perspectiveOffCenterLH(double left, double right, double bottom, double top, double zNear, double zFar) { }
        public void perspectiveOffCenterRH(double left, double right, double bottom, double top, double zNear, double zFar) { }
        public void perspectiveRH(double width, double height, double zNear, double zFar) { }
    }
}
#endregion

namespace BeginningWithStage3D
{
    [SWF(backgroundColor = 0xff)]
    public sealed class ApplicationSprite : Sprite
    {
        // inspired by
        // http://rongonga.wordpress.com/2012/05/27/hello-world/
        // http://rongonga.wordpress.com/2012/05/27/beginning-stage3d-part2/
        // http://rongonga.wordpress.com/2012/05/28/beginning-stage3d-part3/
        // http://www.mcfunkypants.com/2011/flash11-stage3d-tutorial-handling-init-errors/
        // http://help.adobe.com/en_US/as3/dev/WSd6a006f2eb1dc31e-310b95831324724ec56-8000.html
        // http://blog.flash-core.com/?p=271
        // http://www.slideshare.net/JosephLabrecque/whats-new-in-flash-player-112-and-adobe-air-32

        public event Action<string> AtMessage;

        // Vertex shader code
        private const string vertexShader =
            // Transform our vertices by our projection matrix and move it into temporary register
            // Move the temporary register to out position for this vertex
        @"m44 vt0, va0, vc0
mov op, vt0";

        // Fragment shader code
        private const string fragmentShader =

              // Simply assing the fragment constant to our out color
              "mov oc, fc0";

        public ApplicationSprite()
        {


            this.WithStage3D(
                stage3D =>
                {
                    //AtMessage("WithStage3D");

                    var context3D = stage3D.context3D;

                    context3D.configureBackBuffer(300, 300, 2);

                    // Create program 3D instance for shader  
                    var program = context3D.createProgram();
                    // Assemble vertex shader from its code
                    var vertexAssembler = new AGALMiniAssembler();
                    vertexAssembler.assemble(Context3DProgramType.VERTEX, vertexShader);
                    // Assemble fragment shader from its code
                    var fragmentAssembler = new AGALMiniAssembler();
                    fragmentAssembler.assemble(Context3DProgramType.FRAGMENT, fragmentShader);
                    // Upload vertex/framgment shader to our program  
                    program.upload(vertexAssembler.agalcode, fragmentAssembler.agalcode);
                    // Set the program instance as currently active program  
                    context3D.setProgram(program);

                    // Create index buffer
                    var __i3dBuffer = context3D.createIndexBuffer(3);
                    // Upload index buffer from predefined values
                    __i3dBuffer.uploadFromVector(new uint[] { 0, 1, 2 }, 0, 3);

                    // Create vertex buffer
                    var __v3dBuffer = context3D.createVertexBuffer(3, 3);
                    // Upload vertex buffer from predefined values
                    __v3dBuffer.uploadFromVector(new double[]{
                    -1, -1, 5,
                    1, -1, 5,
                    0,  1, 5}, 0, 3);
                    // Set vertex buffer, this is what we access in vertex shader register va0
                    context3D.setVertexBufferAt(0, __v3dBuffer, 0, Context3DVertexBufferFormat.FLOAT_3);

                    // Create our projection matrix
                    var projection = new PerspectiveMatrix3D();
                    // Use a helper function to set up the projection
                    projection.perspectiveFieldOfViewLH(45 * Math.PI / 180, 1.2, 0.1, 512);
                    // Set the projection matrix as a vertex program constant, this is what we access in vertex shader register vc0
                    context3D.setProgramConstantsFromMatrix(Context3DProgramType.VERTEX, 0, projection, true);
                    // Set the out color for our polygon as fragment program constant, this is what we access in fragment shader register fc0
                    context3D.setProgramConstantsFromVector(Context3DProgramType.FRAGMENT, 0, new double[] { 1, 1, 1, 0 });

                    // Hook up enter frame event where we will do our rendering
                    this.enterFrame +=
                        delegate
                        {
                            context3D.clear(0, 0, 0, 0);

                            context3D.drawTriangles(__i3dBuffer, 0, 1);

                            context3D.present();
                        };
                }
            );
        }

    }

    static class ApplicationSpriteExtensions
    {
        public static void WithStage3D(this Sprite e, Action<Stage3D> h)
        {
            e.InvokeWhenStageIsReady(
                delegate
                {
                    var stage3D = e.stage.stage3Ds[0];

                    stage3D.context3DCreate +=
                        delegate
                        {
                            h(stage3D);
                        };

                    stage3D.requestContext3D();
                }
            );
        }
    }

}
