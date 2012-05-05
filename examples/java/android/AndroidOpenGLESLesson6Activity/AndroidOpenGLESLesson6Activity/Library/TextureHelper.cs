
using android.content;
using android.graphics;
using android.opengl;
using ScriptCoreLib;
namespace AndroidOpenGLESLesson6Activity.Library
{
    class TextureHelper
    {
        public static int loadTexture(Context context, int resourceId)
        {
            int[] textureHandle = new int[1];

            GLES20.glGenTextures(1, textureHandle, 0);

            if (textureHandle[0] != 0)
            {
                BitmapFactory.Options options = new BitmapFactory.Options();
                options.inScaled = false;	// No pre-scaling

                // Read in the resource
                Bitmap bitmap = BitmapFactory.decodeResource(context.getResources(), resourceId, options);

                // Bind to the texture in OpenGL
                GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, textureHandle[0]);

                // Set filtering
                GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MIN_FILTER, GLES20.GL_NEAREST);
                GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MAG_FILTER, GLES20.GL_NEAREST);

                // Load the bitmap into the bound texture.
                GLUtils.texImage2D(GLES20.GL_TEXTURE_2D, 0, bitmap, 0);

                // Recycle the bitmap, since its data has been loaded into OpenGL.
                bitmap.recycle();
            }

            if (textureHandle[0] == 0)
            {
                //throw new RuntimeException("Error loading texture.");
                throw null;
            }

            return textureHandle[0];
        }
    }
}
