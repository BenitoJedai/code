using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.Extensions;

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

        public event Action<string> AtMessage;


        public void InitializeContent()
        {
            AtMessage("InitializeContent");

            this.WithStage3D(
                stage3D =>
                {
                    AtMessage("WithStage3D");

                    var context3D = stage3D.context3D;

                    context3D.configureBackBuffer(200, 200, 2);


                    #region  Create a vertexBuffer
                    var vertexBuffer = context3D.createVertexBuffer(4, 3);

                    // Put some vertices into the buffer, 
                    // and upload it to our GPU. 
                    double[] vertexBuffer_data =
                    {
		                -0.5,-0.5, 0, 		// x,y,z
		                0.5, -0.5, 0, 
		                -0.5, 0.5, 0, 
		                0.5, 0.5, 0 
                    };


                    vertexBuffer.uploadFromVector(vertexBuffer_data,
                    0, 4);
                    #endregion


                    #region Create an indexBuffer
                    var indexBuffer = context3D.createIndexBuffer(6);

                    // Upload the contents of the indexBuffer
                    // to our GPU

                    uint[] indexBuffer_data = 
                    {
	                        1, 0, 2,
		                2, 3, 1
                    };


                    indexBuffer.uploadFromVector(indexBuffer_data,
                        0, 6);
                    #endregion


                    AtMessage("uploadFromVector");

                    context3D.drawTriangles(indexBuffer, 0, 1);
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
