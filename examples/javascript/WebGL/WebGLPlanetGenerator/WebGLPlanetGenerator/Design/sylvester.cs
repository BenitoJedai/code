using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace WebGLPlanetGenerator.Design
{
    internal class __sylvester : sylvester
    {
        // this should be generated via assets build :)

        // accessing this variable the first time could
        // trigger dynamic loading of this library
        // or actually just using this from Application should trigger the download
        // before app launch

        [Script(ExternalTarget = "Vector")]
        static public Vector Vector;

        [Script(ExternalTarget = "Matrix")]
        static public Matrix Matrix;
    }

    [Script(HasNoPrototype = true, ExternalTarget = "Vector")]
    internal partial class Vector
    {
        internal Vector create(float[] p)
        {
            throw new NotImplementedException();
        }
    }

    // http://sylvester.jcoglan.com/api/matrix
    [Script(HasNoPrototype = true, ExternalTarget = "Matrix")]
    internal partial class Matrix
    {

        // static
        internal Matrix I(int p)
        {
            throw new NotImplementedException();
        }

        internal Matrix x(Matrix m)
        {
            throw new NotImplementedException();
        }
    }
}
