using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace WebGLPlanetGenerator.Design
{
    internal class __planet : planet
    {
    }

    sealed class PlanetGenerateTuple
    {
        public float[] colors;
        public float[] vertices;
        public ushort[] indices;
        public float[] normals;
    }
    [Script(HasNoPrototype = true, ExternalTarget = "Planet")]
    internal partial class Planet
    {
        public Planet(int p)
        {
            throw new NotImplementedException();
        }


        internal PlanetGenerateTuple generate()
        {
            throw new NotImplementedException();
        }
    }

    
}
