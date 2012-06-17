using System;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.Extensions;

namespace BeginningWithStage3D
{
    public sealed class ApplicationSprite : Sprite
    {
        // inspired by
        // http://rongonga.wordpress.com/2012/05/27/hello-world/
        // http://rongonga.wordpress.com/2012/05/27/beginning-stage3d-part2/
        // http://rongonga.wordpress.com/2012/05/28/beginning-stage3d-part3/
        // http://www.mcfunkypants.com/2011/flash11-stage3d-tutorial-handling-init-errors/

        public event Action<string> AtMessage;
        


        public ApplicationSprite()
        {
            AtMessage += delegate { };

            AtMessage("ApplicationSprite");

            this.InvokeWhenStageIsReady(
                delegate
                {
                    AtMessage("InvokeWhenStageIsReady");

                    // entry point
                    // Init stage3D 
                    var stage3D = stage.stage3Ds[0];

                    stage3D.context3DCreate +=
                        e =>
                        {
                            AtMessage("context3DCreate");

                            //var stage3D = e.currentTarget as Stage3D;
                            
                            var context3D = stage3D.context3D;

                            context3D.configureBackBuffer(800, 600, 2);

                            		
	                        // Create a vertexBuffer
	                        var vertexBuffer = context3D.createVertexBuffer(4,3);
		
	                        // Put some vertices into the buffer, 
	                        // and upload it to our GPU. 
                            double [] vertexBuffer_data =
                            {
		                        -0.5,-0.5, 0, 		// x,y,z
		                        0.5, -0.5, 0, 
		                        -0.5, 0.5, 0, 
		                        0.5, 0.5, 0 
                            };


	                         vertexBuffer.uploadFromVector(vertexBuffer_data, 
		                        0, 4);
			
	                        // Create an indexBuffer
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
			
                        };

                    AtMessage("requestContext3D");

                    stage3D.requestContext3D();
                }
            );
        }

    }
}
