using com.barliesque.agal;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display3D;
using ScriptCoreLib.ActionScript.flash.geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

////#region flash natives does not do virtual methods yet
////namespace com.barliesque.agal
////{
////    [Script(IsNative = true)]
////    [global::EasyAGAL.SWCImport]
////    public class EasyBase
////    {
////        public EasyBase(System.Boolean debug = true, System.Boolean assemblyDebug = false)
////        {

////        }

////        public ScriptCoreLib.ActionScript.Vector<IRegister> ATTRIBUTE { get; set; }
////        public ScriptCoreLib.ActionScript.Vector<IRegister> CONST { get; set; }
////        public ScriptCoreLib.ActionScript.flash.display3D.Context3D context { get; set; }
////        public System.UInt32 fragmentInstructions { get; set; }
////        public IRegister OUTPUT { get; set; }
////        public ScriptCoreLib.ActionScript.flash.display3D.Program3D program { get; set; }
////        public ScriptCoreLib.ActionScript.Vector<ISampler> SAMPLER { get; set; }
////        public ScriptCoreLib.ActionScript.Vector<IRegister> TEMP { get; set; }
////        public ScriptCoreLib.ActionScript.Vector<IRegister> VARYING { get; set; }
////        public System.UInt32 vertexInstructions { get { throw null; } }

////        protected virtual void _fragmentShader() { throw null; }
////        protected virtual void _vertexShader() { throw null; }
////        protected System.Object assign(IIField field, System.String alias) { throw null; }
////        protected static void comment(System.Object rest) { throw null; }
////        protected static void comment(System.Object rest, System.Object rest1) { throw null; }
////        protected static void comment(System.Object rest, System.Object rest1, System.Object rest2) { throw null; }
////        protected static void comment(System.Object rest, System.Object rest1, System.Object rest2, System.Object rest3) { throw null; }
////        protected IRegister CONST_byIndex(IComponent index) { throw null; }
////        public virtual void dispose() { throw null; }
////        public System.String getFragmentOpcode(System.Boolean lineNumbering = false, System.Boolean formatAS3 = false) { throw null; }
////        public System.String getVertexOpcode(System.Boolean lineNumbering = false, System.Boolean formatAS3 = false) { throw null; }
////        protected void setContext(ScriptCoreLib.ActionScript.flash.display3D.Context3D context) { throw null; }
////        protected void setFragmentOpcode(System.String opcode, System.Boolean append = false) { throw null; }
////        protected void setProgram(ScriptCoreLib.ActionScript.flash.display3D.Program3D value) { throw null; }
////        protected void setVertexOpcode(System.String opcode, System.Boolean append = false) { throw null; }
////        protected System.Object unassign(IIField field) { throw null; }
////        public ScriptCoreLib.ActionScript.flash.display3D.Program3D upload(ScriptCoreLib.ActionScript.flash.display3D.Context3D context) { throw null; }

////    }

////    [Script(IsNative = true)]
////    [global::EasyAGAL.SWCImport]
////    public class EasierAGAL : EasyBase
////    {
////        public EasierAGAL(bool debug = true, bool assemblyDebug = false) { throw null; }

////        protected static void abs(IField dest, IField source) { throw null; }
////        protected static void add(IField dest, IField source1, IField source2) { throw null; }
////        protected static void cos(IField dest, IField source) { throw null; }
////        protected static void crossProduct(IField dest, IField source1, IField source2) { throw null; }
////        protected static void divide(IField dest, IField source1, IField source2) { throw null; }
////        protected static void dotProduct3(IField dest, IField source1, IField source2) { throw null; }
////        protected static void dotProduct4(IField dest, IField source1, IField source2) { throw null; }
////        protected static void exp(IField dest, IField source) { throw null; }
////        protected static void fractional(IField dest, IField source) { throw null; }
////        protected static void killFragment(IField dest, IField source) { throw null; }
////        protected static void log(IField dest, IField source) { throw null; }
////        protected static void max(IField dest, IField source1, IField source2) { throw null; }
////        protected static void min(IField dest, IField source1, IField source2) { throw null; }
////        protected static void move(IField dest, IField source) { throw null; }
////        protected static void multiply(IField dest, IField source1, IField source2) { throw null; }
////        protected static void multiply3x3(IField dest, IField source1, IField source2) { throw null; }
////        protected static void multiply3x4(IField dest, IField source1, IField source2) { throw null; }
////        protected static void multiply4x4(IField dest, IField source1, IField source2) { throw null; }
////        protected static void negate(IField dest, IField source) { throw null; }
////        protected static void normalize(IField dest, IField source) { throw null; }
////        protected static void pow(IField dest, IField source1, IField source2) { throw null; }
////        protected static void reciprocal(IField dest, IField source) { throw null; }
////        protected static void reciprocalRoot(IField dest, IField source) { throw null; }
////        protected static void sampleTexture(IField dest, IField source1, ISampler source2, object[] flags = null) { throw null; }
////        protected static void saturate(IField dest, IField source) { throw null; }
////        protected static void setIf_Equal(IField dest, IField source1, IField source2) { throw null; }
////        protected static void setIf_GreaterEqual(IField dest, IField source1, IField source2) { throw null; }
////        protected static void setIf_LessThan(IField dest, IField source1, IField source2) { throw null; }
////        protected static void setIf_NotEqual(IField dest, IField source1, IField source2) { throw null; }
////        protected static void sin(IField dest, IField source) { throw null; }
////        protected static void squareRoot(IField dest, IField source) { throw null; }
////        protected static void subtract(IField dest, IField source1, IField source2) { throw null; }
////    }
////}
////#endregion


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
