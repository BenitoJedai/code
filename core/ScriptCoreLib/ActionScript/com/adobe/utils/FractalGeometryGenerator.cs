using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.display3D;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.com.adobe.utils
{
    // http://barliesque.com/easy_agal/docs/com/adobe/utils/FractalGeometryGenerator.html
    /// <summary>
    /// The DisplayObject class is the base class for all objects that can be placed on the display list. The display list manages all objects displayed in Flash Player or Adobe AIR. Use the DisplayObjectContainer class to arrange the display objects in the display list. DisplayObjectContainer objects can have child display objects, while other display objects, such as Shape and TextField objects, are "leaf" nodes that have only parents and siblings, no children.
    /// </summary>
    [Script(IsNative = true)]
    public class FractalGeometryGenerator
    {

        #region CTOR

        public FractalGeometryGenerator(Context3D context3D, uint levels)
        {
        }

        #endregion

        #region PUBLIC_METHODS


        public void draw()
        {
        }

        public void setColor(double r, double g, double b, double a)
        {
        }
 	 	
        public void setMatrix(Matrix3D matrix)
        {

        }

        #endregion

        #region PROTECTED_METHODS

        protected void genGeom()
        {
        }


        protected object genLevel(int level, double ox, double oy, uint indexindex, uint vertexindex)
        {
            return default(object);
        }

 	 	
        protected void initProgram()
        {
        }


        #endregion

        #region PROPERTIES

        double m_alpha 
        {
            get;
            set;
        }

 	 	double m_blue
        {
            get;
            set;
        }

 	 	Context3D m_context3D
        {
            get;
            set;
        }

 	 	double m_green
        {
            get;
            set;
        }

 	 	IndexBuffer3D m_indexBuffer
        {
            get;
            set;
        }

 	 	uint m_indexBufferSize
        {
            get;
            set;
        }

 	 	Vector<uint> m_indexData
        {
            get;
            set;
        }

 	 	int m_levels
        {
            get;
            set;
        }

 	 	Matrix3D m_matrix
        {
            get;
            set;
        }

 	 	int m_nObjs
        {
            get;
            set;
        }

 	 	Program3D m_program
        {
            get;
            set;
        }

 	 	double m_red
        {
            get;
            set;
        }

 	 	VertexBuffer3D m_vertexBuffer
        {
            get;
            set;
        }

 	 	uint m_vertexBufferSize
        {
            get;
            set;
        }

 	 	Vector<double> m_vertexData
        {
            get;
            set;
        }

        #endregion
    }

    

}
