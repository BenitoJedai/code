using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.com.adobe.utils
{
    // http://barliesque.com/easy_agal/docs/com/adobe/utils/PerspectiveMatrix3D.html
    /// <summary>
    /// 
    /// </summary>
    [Script(IsNative = true)]
    public class PerspectiveMatrix3D
    {

        #region CTOR

        public PerspectiveMatrix3D (Vector<double> v= null)
        {

        }
        #endregion

        #region METHODS

        public void lookAtLH(Vector3D eye, Vector3D at, Vector3D up)
        {

        }

        public void lookAtRH(Vector3D eye, Vector3D at, Vector3D up)
        {

        }

        public void orthoLH(double width, double height, double zNear, double zFar)
        {

        }

        public void orthoOffCenterLH(double left, double right, double bottom, double top, double zNear, double zFar)
        {

        }

        public void orthoOffCenterRH(double left, double right, double bottom, double top, double zNear, double zFar)
        {

        }

        public void orthoRH(double width, double height, double zNear, double zFar)
        {

        }


        public void perspectiveFieldOfViewLH(double fieldOfViewY, double aspectRatio, double zNear, double zFar)
        {

        }


        public void perspectiveFieldOfViewRH(double fieldOfViewY, double aspectRatio, double zNear, double zFar)
        {

        }

        public void perspectiveLH(double width, double height, double zNear, double zFar)
        {

        }


        public void perspectiveOffCenterLH(double left, double right, double bottom, double top, double zNear, double zFar)
        {

        }

        public void perspectiveOffCenterRH(double left, double right, double bottom, double top, double zNear, double zFar)
        {

        }


        public void perspectiveRH(double width, double height, double zNear, double zFar)
        {

        }

        #endregion


    }

    

}
