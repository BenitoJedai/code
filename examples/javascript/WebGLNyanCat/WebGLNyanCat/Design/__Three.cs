#pragma warning disable 649

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace WebGLNyanCat.Design
{
    [Description("Future versions of JSC will enable seamless integration with JavaScript libraries")]
    internal class __Three : global::WebGLNyanCat.Design.Three
    {


    }

    #region THREE natives from Three.js
    namespace THREE
    {
        sealed class MeshLambertMaterialArguments
        {
            public int color;
        }


        [Script(HasNoPrototype = true, ExternalTarget = "MeshLambertMaterial")]
        class MeshLambertMaterial
        {
            public MeshLambertMaterial(MeshLambertMaterialArguments a)
            {

            }
        }


        [Script(HasNoPrototype = true, ExternalTarget = "CubeGeometry")]
        class CubeGeometry
        {

            public CubeGeometry(float w, float h, float d, int p, int p_2, int p_3)
            {

            }

        }

        [Script(HasNoPrototype = true, ExternalTarget = "Torus")]
        class Torus
        {

        }

        [Script(HasNoPrototype = true, ExternalTarget = "Object3D")]
        class Object3D
        {
            public Vector3 position;

            internal void add(Mesh mesh)
            {
                throw new NotImplementedException();
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

            internal void set(int p, int p_2, int p_3)
            {
                throw new NotImplementedException();
            }

            internal void normalize()
            {
                throw new NotImplementedException();
            }
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

            internal void addLight(AmbientLight ambient)
            {
                throw new NotImplementedException();
            }

            internal void add(Object3D poptart)
            {
                throw new NotImplementedException();
            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.WebGLRenderer")]
        class WebGLRenderer
        {
            public IHTMLElement domElement;
            public bool autoClear;

            internal void initMaterial(MeshShaderMaterial material_base, object p, object p_2)
            {
                throw new NotImplementedException();
            }

            internal void render(Scene scene, PerspectiveCamera camera)
            {
                throw new NotImplementedException();
            }

            internal void setSize(int size, int size_2)
            {
                throw new NotImplementedException();
            }

            internal void clear()
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
            public Vector3 scale;
            public Vector3 position;
            public Vector3 rotation;
            public bool overdraw;


            public Mesh(object value, object x)
            {

            }

            internal void updateMatrix()
            {
                throw new NotImplementedException();
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

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.PointLight")]
        class PointLight
        {
            public PointLight(int value)
            {

            }

            public Vector3 position;
            public object color;

        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.PerspectiveCamera")]
        class PerspectiveCamera
        {
            public PerspectiveCamera(int x, int y, float a, int b)
            {

            }

            public Vector3 position;
            public int aspect;

            internal void updateProjectionMatrix()
            {
                throw new NotImplementedException();
            }

            internal void updateMatrix()
            {
                throw new NotImplementedException();
            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.AmbientLight")]
        class AmbientLight
        {
            public AmbientLight(int x)
            {

            }

            public Vector3 position;

        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.RenderPass")]
        class RenderPass
        {

            public RenderPass(Scene scene, PerspectiveCamera camera)
            {
            }

        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.FilmPass")]
        class FilmPass
        {
            public bool renderToScreen;


            public FilmPass(double p, double p_2, int p_3, bool p_4)
            {

            }


        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.FogExp2")]
        class FogExp2
        {
            public FogExp2(float a, float b)
            {

            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.EffectComposer")]
        class EffectComposer
        {

            public EffectComposer(WebGLRenderer renderer)
            {
            }



            internal void addPass(RenderPass renderModel)
            {
                throw new NotImplementedException();
            }

            internal void addPass(FilmPass effectFilm)
            {
                throw new NotImplementedException();
            }

            internal void render(long delta)
            {
                throw new NotImplementedException();
            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.MeshFaceMaterial")]
        class MeshFaceMaterial
        {


        }

        sealed class JSONLoaderArguments
        {
            public string model;
            public IFunction callback;
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.JSONLoader")]
        class JSONLoader
        {


            internal void load(JSONLoaderArguments jSONLoaderArguments)
            {
                throw new NotImplementedException();
            }
        }

    }
    #endregion
}
