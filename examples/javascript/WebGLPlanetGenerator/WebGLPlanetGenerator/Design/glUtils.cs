using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;

namespace WebGLPlanetGenerator.Design
{
    using f = System.Single;


    internal class __glUtils : glUtils
    {
        // this should be generated via assets build :)

        // accessing this variable the first time could
        // trigger dynamic loading of this library
        // or actually just using this from Application should trigger the download
        // before app launch


        [Script(ExternalTarget = "window")]
        static public __glUtils_globals globals;
    }

    [Script(HasNoPrototype = true)]
    internal class __glUtils_globals : IWindow
    {
        // this seems a painful way to define a static method :)
        internal Matrix makePerspective(float fovy, float aspect, float znear, float zfar)
        {
            throw new NotImplementedException();
        }

        internal Matrix makeLookAt(f ex, f ey, f ez, f cx, f cy, f cz, f ux, f uy, f uz)
        {
            throw new NotImplementedException();
        }
    }

    internal partial class Matrix
    {
        // static
        internal Matrix Translation(Vector vector)
        {
            throw new NotImplementedException();
        }

        internal Matrix ensure4x4()
        {
            throw new NotImplementedException();
        }

        // static
        internal Matrix Rotation(double arad, Vector vector)
        {
            throw new NotImplementedException();
        }

        internal float[] flatten()
        {
            throw new NotImplementedException();
        }

        internal Matrix dup()
        {
            throw new NotImplementedException();
        }

        internal Matrix inverse()
        {
            throw new NotImplementedException();
        }

        internal Matrix transpose()
        {
            throw new NotImplementedException();
        }
    }

}
