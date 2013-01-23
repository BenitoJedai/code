using com.adobe.utils;
using EasyAGALBasicRenderer.Library;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.display3D;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Extensions;
using System;
using System.Diagnostics;

namespace EasyAGALBasicRenderer
{
    public sealed class ApplicationSprite : Sprite
    {

        private Context3D context;
        private Stage3D stage3D;
        private BasicRender shader;

        private const int CONTEXT_WIDTH = 600;
        private const int CONTEXT_HEIGHT = 600;

        private const double DEGS_TO_RADIANS = Math.PI / 180;


        public ApplicationSprite()
        {
            // Set the default stage behavior
            stage.scaleMode = StageScaleMode.NO_SCALE;
            stage.align = StageAlign.TOP_LEFT;

            // Request a 3D context instance
            stage3D = stage.stage3Ds[0];


            //stage3D.addEventListener(Event.CONTEXT3D_CREATE, contextReady, false, 0, true);
            stage3D.context3DCreate +=
                e =>
                {
                    //stage3D.removeEventListener(Event.CONTEXT3D_CREATE, contextReady);
                    //trace("Got context!");

                    // Get the new context
                    context = stage3D.context3D;

                    // Configure back buffer
                    context.configureBackBuffer(CONTEXT_WIDTH, CONTEXT_HEIGHT, 2, true);
                    stage3D.x = stage3D.y = 0;

                    // Prepare vertex data
                    Vector<double> vertexData = new[]{
				            -0.5, -0.5,	0,		1.0, 0.0, 0.0,	//<- 1st vertex x,y,z, r,g,b
				            -0.5, 0.5,	0,		0.0, 1.0, 0.0,	//<- 2nd vertex x,y,z, r,g,b
				            0.5,  0.0,	0,		0.0, 0.0, 1.0	//<- 3rd vertex x,y,z, r,g,b
                        };

                    // Connect the vertices into a triangle (in counter-clockwise order)
                    Vector<uint> indexData = new uint[] { 0, 1, 2 };

                    // Prepare a shader for rendering
                    shader = new BasicRender();
                    shader.upload(context);
                    shader.setGeometry(vertexData, indexData);

                    // ...and start rendering frames!
                    //addEventListener(Event.ENTER_FRAME, renderFrame, false, 0, true);

                    var sw = new Stopwatch();
                    sw.Start();

                    Func<Matrix3D> makeViewMatrix = delegate
                    {
                        var aspect = CONTEXT_WIDTH / CONTEXT_HEIGHT;
                        var zNear = 0.01;
                        var zFar = 1000;
                        var fov = 45 * DEGS_TO_RADIANS;

                        var view = new PerspectiveMatrix3D();
                        view.perspectiveFieldOfViewLH(fov, aspect, zNear, zFar);

                        var m = new Matrix3D();

                        m.appendRotation(sw.ElapsedMilliseconds / 30, Vector3D.Z_AXIS);
                        m.appendTranslation(0, 0, 2);
                        m.append(view);

                        return m;
                    };



                    this.enterFrame +=
                        delegate
                        {
                            // Clear away the old frame render
                            context.clear();

                            // Calculate the view matrix, and run the shader program!
                            shader.render(makeViewMatrix());

                            // Show the newly rendered frame on screen
                            context.present();
                        };
                };

            stage3D.requestContext3D(Context3DRenderMode.AUTO);

            //trace("Awaiting context...");
        }





    }
}
