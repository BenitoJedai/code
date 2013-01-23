using com.barliesque.agal;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display3D;
using ScriptCoreLib.ActionScript.flash.geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EasyAGALBasicRenderer.Library
{

    /**
         * A simple test shader that accepts xyz vertex data with rgb vertex colors.
         * @author David Barlia, david@barliesque.com
         */
    public class BasicRender : EasierAGAL
    {

        private VertexBuffer3D vertexBuffer;
        private IndexBuffer3D indexBuffer;

        /// x, y, z, r, g, b
        public const uint DATA32_PER_VERTEX = 6;


        public BasicRender()
            : base(true)
        {
            // Passing true here means we'll assemble the shader in debug mode
        }


        override protected void _vertexShader()
        {
            //            V:\web\EasyAGALBasicRenderer\Library\BasicRender.as(36): col: 13 Error: Access of undefined property EasyBase.

            //            EasyBase.comment("Apply a 4x4 matrix to transform vertices to clip-space");
            //            ^

            //V:\web\EasyAGALBasicRenderer\Library\BasicRender.as(38): col: 13 Error: Access of undefined property EasyBase.

            //            EasyBase.comment("Pass vertex color to fragment shader");
            //            ^

            // jsc no importing the ref?
            //comment("Apply a 4x4 matrix to transform vertices to clip-space");
            multiply4x4(OUTPUT, ATTRIBUTE[0], CONST[0]);

            //comment("Pass vertex color to fragment shader");
            move(VARYING[0], ATTRIBUTE[1]);
        }


        override protected void _fragmentShader()
        {
            // Output the interpolated vertex color for this pixel
            move(OUTPUT, VARYING[0]);
        }


        public void setGeometry(Vector<double> vertices, Vector<uint> indices)
        {
            // Upload vertex data
            if (vertexBuffer != null) vertexBuffer.dispose();
            vertexBuffer = context.createVertexBuffer((int)vertices.length / (int)DATA32_PER_VERTEX, (int)DATA32_PER_VERTEX);
            vertexBuffer.uploadFromVector(vertices, 0, (int)vertices.length / (int)DATA32_PER_VERTEX);

            // Upload polygon data (vertex indices)
            if (indexBuffer != null) indexBuffer.dispose();
            indexBuffer = context.createIndexBuffer((int)indices.length);
            indexBuffer.uploadFromVector(indices, 0, (int)indices.length);
        }


        public void render(Matrix3D viewMatrix)
        {
            // Tell the 3D context that this is the current shader program to be rendered
            context.setProgram(program);

            // Set ATTRIBUTE Registers to point at vertex data
            context.setVertexBufferAt(0, vertexBuffer, 0, Context3DVertexBufferFormat.FLOAT_3); // xyz
            context.setVertexBufferAt(1, vertexBuffer, 3, Context3DVertexBufferFormat.FLOAT_3); // rgb

            // Pass viewMatrix into constant registers
            context.setProgramConstantsFromMatrix(Context3DProgramType.VERTEX, 0, viewMatrix);

            // Render the shader!
            context.drawTriangles(indexBuffer);
        }


        override public void dispose()
        {
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
