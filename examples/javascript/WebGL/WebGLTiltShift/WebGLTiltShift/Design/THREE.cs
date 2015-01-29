#pragma warning disable 649

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace WebGLTiltShift.Design
{
    using f = System.Single;

    [Description("Future versions of JSC will enable seamless integration with JavaScript libraries")]
    internal class __THREE // : global::WebGLTiltShift.Design.ThreeTerrain
    {
        [Script(ExternalTarget = "THREE.ShaderExtras")]
        public static THREE.ShaderExtrasModule ShaderExtras;
        [Script(ExternalTarget = "THREE.ShaderTerrain")]
        public static THREE.ShaderTerrainModule ShaderTerrain;
        [Script(ExternalTarget = "THREE.Math")]
        public static THREE.MathModule Math;
        [Script(ExternalTarget = "THREE.ImageUtils")]
        public static THREE.ImageUtilsModule ImageUtils;
        [Script(ExternalTarget = "THREE.UniformsUtils")]
        public static THREE.UniformsUtilsModule UniformsUtils;


    }



    #region THREE natives from Three.js

    #region no namespace


    [Script(HasNoPrototype = true, ExternalTarget = "Torus")]
    class Torus
    {

    }

    [Script(HasNoPrototype = true, ExternalTarget = "Uniforms")]
    class Uniforms
    {
        public object clone(object e)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region modules
    namespace THREE
    {
        [Script(HasNoPrototype = true)]
        class MathModule
        {

            internal int clamp(f p1, int p2, int p3)
            {
                throw new NotImplementedException();
            }

            internal int clamp(double p, double fLow, double fHigh)
            {
                throw new NotImplementedException();
            }

            internal double mapLinear(double valNorm, int p1, int p2, double p3, double p4)
            {
                throw new NotImplementedException();
            }
        }


        [Script(HasNoPrototype = true)]
        class ImageUtilsModule
        {
            public THREE.WebGLRenderTarget loadTexture(string a, object b, IFunction c)
            {
                return null;
            }
        }



        [Script(HasNoPrototype = true)]
        class UniformsUtilsModule
        {


            internal ShaderExtrasModuleItem_uniforms clone(ShaderExtrasModuleItem_uniforms shaderExtrasModuleItem_uniforms)
            {
                throw new NotImplementedException();
            }
        }

        sealed class ShaderExtrasModuleItem_uniforms_item
        {
            public object texture;
            public object value;
            public string type;
        }

        sealed class ShaderExtrasModuleItem_uniforms
        {
            public ShaderExtrasModuleItem_uniforms_item
                uDiffuseColor,
                uSpecularColor,
                uAmbientColor,
                uRepeatOverlay,
                opacity,
                r,
                h,
                v,
                uShininess,
                uDisplacementScale,

                enableDiffuse1,
                enableDiffuse2,
                enableSpecular,
                uOffset,
                heightMap,
             resolution,
             scale,
             height,
             tDiffuse,
             tDiffuse1,
             tDiffuse2,
             tSpecular,
             tDetail,
             tDisplacement,
             tNormal,
             uNormalScale;
        }

        sealed class ShaderExtrasModuleItem
        {
            public string fragmentShader;
            public string vertexShader;
            public ShaderExtrasModuleItem_uniforms uniforms;
        }

        sealed class ShaderExtrasModule
        {

            public ShaderExtrasModuleItem
                horizontalTiltShift,
                verticalTiltShift,
                luminosity,
                bleachbypass,
                normalmap;
        }

        sealed class ShaderTerrainModule
        {
            public ShaderExtrasModuleItem terrain;
        }

    }
    #endregion

    namespace THREE
    {

        sealed class MeshLambertMaterialArguments
        {
            public int color;
            public bool morphTargets;
            public int vertexColors;
        }


        [Script(HasNoPrototype = true, ExternalTarget = "THREE.MeshLambertMaterial")]
        class MeshLambertMaterial
        {
            public MeshLambertMaterial(MeshLambertMaterialArguments a)
            {

            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.MorphAnimMesh")]
        class MorphAnimMesh
        {
            private MeshLambertMaterial material;
            public float speed;
            public float duration;
            public double time;

            public Vector3 position;
            public Vector3 rotation;
            public bool castShadow;
            public bool receiveShadow;

            public MorphAnimMesh(object geometry, MeshLambertMaterial material)
            {
                // TODO: Complete member initialization
                this.material = material;
            }


            internal void updateAnimation(f p)
            {
                throw new NotImplementedException();
            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.PlaneGeometry")]
        class PlaneGeometry
        {
            private int p1;
            private int p2;
            private int p3;
            private int p4;
            private int SCREEN_WIDTH;
            private int SCREEN_HEIGHT;

            public PlaneGeometry(int p1, int p2, int p3, int p4)
            {
                // TODO: Complete member initialization
                this.p1 = p1;
                this.p2 = p2;
                this.p3 = p3;
                this.p4 = p4;
            }

            public PlaneGeometry(int SCREEN_WIDTH, int SCREEN_HEIGHT)
            {
                // TODO: Complete member initialization
                this.SCREEN_WIDTH = SCREEN_WIDTH;
                this.SCREEN_HEIGHT = SCREEN_HEIGHT;
            }

            internal void computeFaceNormals()
            {
                throw new NotImplementedException();
            }

            internal void computeVertexNormals()
            {
                throw new NotImplementedException();
            }

            internal void computeTangents()
            {
                throw new NotImplementedException();
            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.CubeGeometry")]
        class CubeGeometry
        {

            public CubeGeometry(float w, float h, float d)
            { }
            public CubeGeometry(float w, float h, float d, int p, int p_2, int p_3)
            {

            }

        }


        [Script(HasNoPrototype = true, ExternalTarget = "THREE.Object3D")]
        class Object3D
        {
            public Vector3 position;
            public Vector3 scale;
            public Vector3 rotation;

            internal void add(Mesh mesh)
            {
                throw new NotImplementedException();
            }
        }


        [Script(HasNoPrototype = true, ExternalTarget = "THREE.Vector2")]
        class Vector2
        {
            public float x;
            public float y;
            private double p1;
            private double p2;

            public Vector2(double p1, double p2)
            {
                // TODO: Complete member initialization
                this.p1 = p1;
                this.p2 = p2;
            }



            internal void set(int rx, int ry)
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

            internal void set(f p, f p_2, f p_3)
            {
                throw new NotImplementedException();
            }

            internal void normalize()
            {
                throw new NotImplementedException();
            }

            internal void set(double p1, double p2, double p3)
            {
                throw new NotImplementedException();
            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.TrackballControls")]
        class TrackballControls
        {
            public Vector3 target;

            private PerspectiveCamera camera;
            public double rotateSpeed;
            public double zoomSpeed;
            public double panSpeed;
            public bool noZoom;
            public bool noPan;
            public bool staticMoving;
            public double dynamicDampingFactor;
            public int[] keys;

            public TrackballControls(PerspectiveCamera camera)
            {
                // TODO: Complete member initialization
                this.camera = camera;
            }

            internal void update()
            {
                throw new NotImplementedException();
            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.Fog")]
        class Fog
        {
            private int p1;
            private int p2;
            private int p3;

            public Fog(int p1, int p2, int p3)
            {
                // TODO: Complete member initialization
                this.p1 = p1;
                this.p2 = p2;
                this.p3 = p3;
            }

            public Color color;
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.Scene")]
        class Scene
        {
            public object lights;
            public Fog fog;
            public Mesh[] objects;
            public Vector3 position;

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

            internal void add(PointLight pointLight)
            {
                throw new NotImplementedException();
            }

            internal void add(OrthographicCamera cameraOrtho)
            {
                throw new NotImplementedException();
            }

            internal void add(PerspectiveCamera camera)
            {
                throw new NotImplementedException();
            }

            internal void add(AmbientLight ambientLight)
            {
                throw new NotImplementedException();
            }

            internal void add(SpotLight spotLight)
            {
                throw new NotImplementedException();
            }

            internal void add(MorphAnimMesh meshAnim)
            {
                throw new NotImplementedException();
            }

            internal void add(Mesh meshTmp)
            {
                throw new NotImplementedException();
            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.BloomPass")]
        class BloomPass
        {
            private double p;

            public BloomPass(double p)
            {
                // TODO: Complete member initialization
                this.p = p;
            }

        }

        sealed class WebGLRenderTargetArguments
        {
            public int minFilter;
            public int magFilter;
            public int format;
            public bool stencilBufer;
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.WebGLRenderTarget")]
        class WebGLRenderTarget
        {
            public int wrapS;
            public int wrapT;

            public WebGLRenderTarget(int SCREEN_WIDTH, int SCREEN_HEIGHT, object renderTargetParameters)
            {
            }
        }


        [Script(HasNoPrototype = true, ExternalTarget = "THREE.WebGLRenderer")]
        class WebGLRenderer
        {
            public IHTMLElement domElement;
            public bool autoClear;
            public bool gammaInput;
            public bool gammaOutput;

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

            internal void setClearColor(Color color, int p)
            {
                throw new NotImplementedException();
            }

            internal void initWebGLObjects(Scene scene)
            {
                throw new NotImplementedException();
            }

            internal void render(Scene sceneTmp, OrthographicCamera cameraOrtho, object target, bool p)
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

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.MeshPhongMaterial")]
        class MeshPhongMaterial
        {
            public MeshPhongMaterial(object value)
            {

            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.MeshShaderMaterial")]
        class MeshShaderMaterial
        {
            public object program;

            public MeshShaderMaterial(MeshShaderMaterialArguments value)
            {

            }
        }

        sealed class ShaderMaterialArguments
        {
            public object fragmentShader;
            public object vertexShader;
            public ShaderExtrasModuleItem_uniforms uniforms;
            public bool fog;
            public bool lights;

        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.ShaderPass")]
        class ShaderPass
        {
            private ShaderExtrasModuleItem shaderExtrasModuleItem;
            public ShaderExtrasModuleItem_uniforms uniforms;
            public bool renderToScreen;

            public ShaderPass(ShaderExtrasModuleItem shaderExtrasModuleItem)
            {
                // TODO: Complete member initialization
                this.shaderExtrasModuleItem = shaderExtrasModuleItem;
            }

        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.ShaderMaterial")]
        class ShaderMaterial
        {
            public ShaderMaterial(object value)
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
            public bool visible;
            public ShaderMaterial material;
            internal bool receiveShadow;
            internal bool castShadow;

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

            internal void setHSV(double p1, double p2, double p3)
            {
                throw new NotImplementedException();
            }

            internal void setHex(int p)
            {
                throw new NotImplementedException();
            }
        }


        [Script(HasNoPrototype = true, ExternalTarget = "THREE.Clock")]
        class Clock
        {
            public Clock()
            {

            }

            internal float getDelta()
            {
                throw new NotImplementedException();
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

            public PointLight(int p1, double p2)
            {
                // TODO: Complete member initialization
                this.p1 = p1;
                this.p2 = p2;
            }

            public Vector3 position;
            public object color;
            private int p1;
            private double p2;
            public double intensity;

        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.OrthographicCamera")]
        class OrthographicCamera
        {
            private int p1;
            private int p2;
            private int p3;
            private int p4;
            private int p5;
            private int p6;

            public OrthographicCamera(int p1, int p2, int p3, int p4, int p5, int p6)
            {
                // TODO: Complete member initialization
                this.p1 = p1;
                this.p2 = p2;
                this.p3 = p3;
                this.p4 = p4;
                this.p5 = p5;
                this.p6 = p6;
            }
            public Vector3 position;

        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.PerspectiveCamera")]
        class PerspectiveCamera
        {
            public PerspectiveCamera(int x, double y, float a, int b)
            {

            }

            public Vector3 position;
            public double aspect;

            internal void updateProjectionMatrix()
            {
                throw new NotImplementedException();
            }

            internal void updateMatrix()
            {
                throw new NotImplementedException();
            }

            internal void lookAt(Vector3 vector3)
            {
                throw new NotImplementedException();
            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.SpotLight")]
        class SpotLight
        {
            private int p1;
            private double p2;

            public Vector3 position;
            public bool castShadow;
            public double intensity;

            public SpotLight(int p1, double p2)
            {
                // TODO: Complete member initialization
                this.p1 = p1;
                this.p2 = p2;
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
            private WebGLRenderer renderer;
            private WebGLRenderTarget renderTarget;


            public EffectComposer(WebGLRenderer renderer)
            {
            }

            public EffectComposer(WebGLRenderer renderer, WebGLRenderTarget renderTarget)
            {
                // TODO: Complete member initialization
                this.renderer = renderer;
                this.renderTarget = renderTarget;
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

            internal void addPass(BloomPass effectBloom)
            {
                throw new NotImplementedException();
            }

            internal void addPass(ShaderPass hblur)
            {
                throw new NotImplementedException();
            }

            internal void render(double p)
            {
                throw new NotImplementedException();
            }
        }



        sealed class MeshBasicMaterialArguments
        {
            public int color;


        }

        [Script(HasNoPrototype = true, ExternalTarget = "THREE.MeshBasicMaterial")]
        class MeshBasicMaterial
        {

            public MeshBasicMaterial(object meshBasicMaterialArguments)
            {
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

            internal void load(string p, IFunction function)
            {
                throw new NotImplementedException();
            }
        }

    }
    #endregion
}
