using com.barliesque.agal;
using com.barliesque.shaders.macro;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.display3D;
using ScriptCoreLib.ActionScript.flash.display3D.textures;
using ScriptCoreLib.ActionScript.flash.geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAGALLighting.Library
{
    /**
     * A shader that accepts xyz vertex position, normal data, and uv texture coordinates.
     * @author David Barlia, david@barliesque.com
     */
    public class LightedRender : EasierAGAL
    {

        private VertexBuffer3D vertexBuffer;
        private IndexBuffer3D indexBuffer;
        private Texture texture3D;

        // Aliases for the varying registers to be used to connect the vertex and fragment shaders
        private IRegister vertexUV;
        private IRegister vertexNormal;
        private IRegister vertexPos;
        private IRegister lightPos;


        /// x,y,z, nx,ny,nz, u,v
        public const uint DATA32_PER_VERTEX = 8;


        public LightedRender()
            : base(true)
        {
            // Passing true here means we'll assemble the shader in debug mode
        }


        override protected void _vertexShader()
        {
            vertexUV = (IRegister)assign(VARYING[0], "vertexUV");
            vertexNormal = (IRegister)assign(VARYING[1], "vertexNormal");
            vertexPos = (IRegister)assign(VARYING[2], "vertexPos");
            lightPos = (IRegister)assign(VARYING[3], "lightPos");

            var viewMatrix = (IRegister)assign(CONST[0], "viewMatrix");

            // Pass texture uv to the fragment shader
            move(vertexUV, ATTRIBUTE[2]);

            // Pass the light position to the fragment shader
            move(lightPos, CONST[4]);

            // Transform vertex normal and pass to the fragment shader
            multiply4x4(vertexNormal, ATTRIBUTE[1], viewMatrix);

            // Transform vertex position, pass to fragment and output
            multiply4x4(vertexPos, ATTRIBUTE[0], viewMatrix);
            multiply4x4(OUTPUT, ATTRIBUTE[0], viewMatrix);
        }


        override protected void _fragmentShader()
        {

            var textureRGB = (IRegister)assign(TEMP[0], "textureRGB");
            var lightNormal = (IField)assign(TEMP[1].xyz, "lightNormal");
            var keyValue = (IComponent)assign(TEMP[2].x, "keyValue");
            var attenuation = (IComponent)assign(TEMP[2].y, "attenuation");
            var pixelToLight = (IField)assign(TEMP[3].xyz, "pixelToLight");
            var finalLight = (IRegister)assign(TEMP[4], "finalLight");
            var finalColor = (IRegister)assign(TEMP[5], "finalColor");

            var lightColor = (IField)CONST[0].rgb;
            var lightStrenth = (IComponent)CONST[0].w;
            var ambient = (IField)CONST[1].rgb;
            var zero = (IComponent)CONST[2].x;
            var third = (IComponent)CONST[2].y;
            var one = (IComponent)CONST[2].z;
            var half = (IComponent)CONST[2].w;

            // Use UV coordinates passed from vertex shader to sample the texture
            sampleTexture(textureRGB, vertexUV, SAMPLER[0], new[] { TextureFlag.TYPE_2D, TextureFlag.MODE_CLAMP, TextureFlag.FILTER_LINEAR });

            //  Calculate the key value - hilight on surfaces that directly face the light source
            normalize(lightNormal, lightPos.xyz);
            dotProduct3(keyValue, vertexNormal, lightNormal);
            add(keyValue, keyValue, third);
            multiply(keyValue, keyValue, lightStrenth);
            max(keyValue, keyValue, zero);

            //  Attenuated diffuse light - the closer to the light a surface is, the stronger this value
            subtract(pixelToLight, vertexPos.xyz, lightPos.xyz);
            dotProduct3(attenuation, pixelToLight, pixelToLight);
            squareRoot(attenuation, attenuation);
            subtract(attenuation, attenuation, lightStrenth);
            subtract(attenuation, attenuation, lightStrenth);
            subtract(attenuation, attenuation, lightStrenth);
            max(attenuation, attenuation, zero);
            reciprocal(attenuation, attenuation);
            multiply(attenuation, attenuation, attenuation);

            // Blend the key and attenuation values with the light color
            add(finalLight, keyValue, attenuation);
            multiply(finalLight, finalLight, lightColor);
            add(finalLight, finalLight, ambient);

            // Finally, blend with the texture sample and output (Try using any of the following blends)
            Blend.hardLight(finalColor, textureRGB, finalLight, one, half, TEMP[1], TEMP[2], TEMP[3]);
            //Blend.softLight(finalColor, finalLight, textureRGB);
            //Blend.pinLight(finalColor, textureRGB, finalLight, one, half, TEMP[1], TEMP[2], TEMP[3]);
            //Blend.glow(finalColor, finalLight, textureRGB, one, TEMP[1]);
            //Blend.grainMerge(finalColor, textureRGB, finalLight, half);
            //Blend.linearLight(finalColor, textureRGB, finalLight, one);
            move(OUTPUT, finalColor.rgb);
        }


        /**
         * Upload geometry data, including a texture image
         * @param	vertices		Vertex attribute data: x,y,z, nx,ny,nz, u,v  (position, normal, texture)
         * @param	indices			Face data
         * @param	textureBitmap	An image to be used as the texture
         */
        public void setGeometry(Vector<double> vertices, Vector<uint> indices, BitmapData textureBitmap)
        {

            // Upload vertex data
            if (vertexBuffer != null) vertexBuffer.dispose();
            vertexBuffer = context.createVertexBuffer((int)(vertices.length / DATA32_PER_VERTEX), (int)DATA32_PER_VERTEX);
            vertexBuffer.uploadFromVector(vertices, 0, (int)(vertices.length / DATA32_PER_VERTEX));

            // Upload polygon data (vertex indices)
            if (indexBuffer != null) indexBuffer.dispose();
            indexBuffer = context.createIndexBuffer((int)indices.length);
            indexBuffer.uploadFromVector(indices, 0, (int)indices.length);

            // Upload texture
            if (texture3D != null) texture3D.dispose();
            texture3D = context.createTexture(textureBitmap.width, textureBitmap.height, Context3DTextureFormat.BGRA, false);
            texture3D.uploadFromBitmapData(textureBitmap);
        }


        public void render(Matrix3D viewMatrix, Vector3D lightPos, Vector3D lightColor, Vector3D ambient)
        {
            // Set ATTRIBUTE Registers to point at vertex data
            context.setVertexBufferAt(0, vertexBuffer, 0, Context3DVertexBufferFormat.FLOAT_3); // x,y,z
            context.setVertexBufferAt(1, vertexBuffer, 3, Context3DVertexBufferFormat.FLOAT_3); // nx,ny,nz
            context.setVertexBufferAt(2, vertexBuffer, 6, Context3DVertexBufferFormat.FLOAT_2); // u,v

            // Set SAMPLER Register
            context.setTextureAt(0, texture3D);

            // Pass viewMatrix into constant registers
            context.setProgramConstantsFromMatrix(Context3DProgramType.VERTEX, 0, viewMatrix);

            // Pass a vector for the (world space) location of the light
            context.setProgramConstantsFromVector(Context3DProgramType.VERTEX, 4, vec3DtoVec(lightPos), 1);

            // Pass light parameters
            context.setProgramConstantsFromVector(Context3DProgramType.FRAGMENT, 0, vec3DtoVec(lightColor), 1);
            context.setProgramConstantsFromVector(Context3DProgramType.FRAGMENT, 1, vec3DtoVec(ambient), 1);
            context.setProgramConstantsFromVector(Context3DProgramType.FRAGMENT, 2, new double[] { 0, 1 / 3, 1, 0.5 }, 1);

            // Tell the 3D context that this is the current shader program to be rendered
            context.setProgram(program);

            // Render the shader!
            context.drawTriangles(indexBuffer);
        }


        private Vector<double> vec3DtoVec(Vector3D vec3D)
        {
            return new[] { vec3D.x, vec3D.y, vec3D.z, vec3D.w };
        }


        override public void dispose()
        {
            if (texture3D != null)
            {
                texture3D.dispose();
                texture3D = null;
            }
            if (vertexBuffer != null)
            {
                vertexBuffer.dispose();
                vertexBuffer = null;
            }
            if (indexBuffer != null)
            {
                indexBuffer.dispose();
                indexBuffer = null;
            }
            base.dispose();
        }


    }
}
