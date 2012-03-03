using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace WebGLClouds.Library.THREE
{
    internal class __ThreeExtras : ThreeExtras
    {
        // this should be generated via assets build :)

        // accessing this variable the first time could
        // trigger dynamic loading of this library
        // or actually just using this from Application should trigger the download
        // before app launch

        [Script(ExternalTarget = "THREE.ImageUtils")]
        static public ImageUtils ImageUtils;

        [Script(ExternalTarget = "GeometryUtils")]
        static public GeometryUtils GeometryUtils;
    }

    [Script(HasNoPrototype = true, ExternalTarget = "GeometryUtils")]
    class GeometryUtils
    {


        internal void merge(Geometry geometry, Mesh plane)
        {
            throw new NotImplementedException();
        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "ImageUtils")]
    class ImageUtils
    {

        internal Texture loadTexture(string p)
        {
            throw new NotImplementedException();
        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "THREE.Plane")]
    internal class Plane
    {
        
        public Plane(int p, int p_2)
        {
        }

    }

}
