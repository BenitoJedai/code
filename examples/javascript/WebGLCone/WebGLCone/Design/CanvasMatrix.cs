using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace WebGLCone.Design
{
    
    [Script(HasNoPrototype = true, ExternalTarget = "CanvasMatrix4")]
    internal class CanvasMatrix4
    {
        // using this type in Application
        // shall mark it to be downloaded before application launch

        internal void perspective(int p, int p_2, double p_3, int p_4)
        {
            throw new NotImplementedException();
        }

        internal float[] getAsArray()
        {
            throw new NotImplementedException();
        }

        internal void makeIdentity()
        {
            throw new NotImplementedException();
        }

        internal void rotate(int p, int p_2, int p_3, int p_4)
        {
            throw new NotImplementedException();
        }

        internal void translate(double x, double y, double z)
        {
            throw new NotImplementedException();
        }

        internal void multRight(CanvasMatrix4 rotMat)
        {
            throw new NotImplementedException();
        }

        internal void load(CanvasMatrix4 rotMat)
        {
            throw new NotImplementedException();
        }
    }
}
