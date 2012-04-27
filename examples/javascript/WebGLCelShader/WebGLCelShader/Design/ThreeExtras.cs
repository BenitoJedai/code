using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.ComponentModel;

namespace WebGLCelShader.Design.THREE
{
    [Description("Future versions of JSC will enable seamless integration with JavaScript libraries")]
    internal class __ThreeExtras : ThreeExtras
    {
        // this should be generated via assets build :)

        // accessing this variable the first time could
        // trigger dynamic loading of this library
        // or actually just using this from Application should trigger the download
        // before app launch

        [Script(ExternalTarget = "Uniforms")]
        static public Uniforms Uniforms;
    }

    [Script(HasNoPrototype = true, ExternalTarget = "Torus")]
    class Torus
    {
        public Torus(int x, int y, int a, int b)
        {

        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "Uniforms")]
    class Uniforms
    {
        public object clone(object e)
        {
            throw new NotImplementedException();
        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "THREE.Vector3")]
    class Vector3
    {
        public float x;
        public float y;
        public float z;
    }

    [Script(HasNoPrototype = true, ExternalTarget = "THREE.Scene")]
    class Scene
    {
        public object lights;
        public object fog;
        public Mesh[] objects;

        internal void addLight(DirectionalLight light)
        {
            throw new NotImplementedException();
        }

        internal void addObject(Mesh mesh)
        {
            throw new NotImplementedException();
        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "THREE.WebGLRenderer")]
    class WebGLRenderer
    {
        public IHTMLElement domElement;

        internal void initMaterial(MeshShaderMaterial material_base, object p, object p_2)
        {
            throw new NotImplementedException();
        }

        internal void render(Scene scene, Camera camera)
        {
            throw new NotImplementedException();
        }

        internal void setSize(int size, int size_2)
        {
            throw new NotImplementedException();
        }
    }

    sealed class MeshShaderMaterialArguments
    {
        public object uniforms;
        public string vertex_shader;
        public string fragment_shader;

    }

    [Script(HasNoPrototype = true, ExternalTarget = "THREE.MeshShaderMaterial")]
    class MeshShaderMaterial
    {
        public object program;

        public MeshShaderMaterial(MeshShaderMaterialArguments value)
        {

        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "THREE.Mesh")]
    class Mesh
    {
        public Vector3 position;
        public Vector3 rotation;


        public Mesh(object value, object x)
        {

        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "THREE.Color")]
    class Color
    {
        public Color(int value)
        {

        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "THREE.DirectionalLight")]
    class DirectionalLight
    {
        public DirectionalLight(int value)
        {

        }

        public Vector3 position;
        public object color;

    }

    [Script(HasNoPrototype = true, ExternalTarget = "THREE.Camera")]
    class Camera
    {
        public Camera(int x, int y, int a, int b)
        {

        }

        public Vector3 position;
        public int aspect;

        internal void updateProjectionMatrix()
        {
            throw new NotImplementedException();
        }
    }


}
