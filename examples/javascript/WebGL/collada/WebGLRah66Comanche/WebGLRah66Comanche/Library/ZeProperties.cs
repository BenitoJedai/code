using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.CompilerServices;
using System.Runtime.CompilerServices;
using ScriptCoreLib.JavaScript.DOM;

namespace WebGLRah66Comanche.Library
{
    public partial class ZeProperties : Form
    {
        public ZeProperties()
        {
            InitializeComponent();
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //this.groupBox1.Text = new { e.Node.Text, e.Node.Tag }.ToString();
            this.groupBox1.Text = new { e.Node.Text, e.Node.Tag }.ToString();

            var xObject3D = e.Node.Tag as THREE.Object3D;
            if (xObject3D != null)
            {
                // can we drag n drop yet?
                textBox1.Text = JSON.stringify(xObject3D.toJSON());
            }

        }

        private void ZeProperties_Load(object sender, EventArgs e)
        {

        }

        public async void Add(string name, Func<bool> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} <- \{get_subject()}");


        }

        public async void Add(string name, Func<double> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add(name);

            var frame = 0;
            do
            {
                frame++;

                var v = get_subject();

                //n.Text = "\{name} = \{v} #\{frame}";
                n.Text = "\{name} = \{v}";

                await Task.Delay(1000 / 5);



                // until parent is detached?
            }
            while (true);

            // would calling self do a tail/jump?
        }

        async void Add(string name, Func<THREE.Group> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.Group)}");
            var x = get_subject();
            n.Text += " '\{x.name}' (\{x.children.Length})";

            await n.AsyncAfterExpand();


            Add("base", () => (THREE.Object3D)x, n.Nodes);
        }


        async void Add(string name, Func<THREE.Object3D> get_subject, TreeNodeCollection Nodes = null)
        {
            // overload seems to work nicely. yet we have to do manual base types /RTTI 

            var x = get_subject();

            var n = Nodes.Add("\{name} : \{nameof(THREE.Object3D)} '\{x.name}' (\{x.children.Length})");
            n.Tag = x;

            await n.AsyncAfterExpand();


            //Add(nameof(THREE.Camera.matrixWorld), () => xCamera.projectionMatrix, n.Nodes);


            // check type?
            Add(nameof(THREE.Object3D.name), () => (object)x.name, n.Nodes);
            Add(nameof(THREE.Object3D.userData), () => x.userData, n.Nodes);
            Add(nameof(THREE.Object3D.type), () => (object)x.type, n.Nodes);

            Add(nameof(THREE.Object3D.parent), () => (object)x.parent, n.Nodes);


            Add(nameof(THREE.Object3D.position), () => x.position, n.Nodes);
            Add(nameof(THREE.Object3D.rotation), () => x.rotation, n.Nodes);
            Add(nameof(THREE.Object3D.scale), () => x.scale, n.Nodes);

            Add(nameof(THREE.Object3D.matrix), () => x.matrix, n.Nodes);
            Add(nameof(THREE.Object3D.quaternion), () => x.quaternion, n.Nodes);

            Add(nameof(THREE.Object3D.visible), () => x.visible, n.Nodes);

            Add(nameof(THREE.Object3D.castShadow), () => x.castShadow, n.Nodes);
            Add(nameof(THREE.Object3D.receiveShadow), () => x.receiveShadow, n.Nodes);

            Add(nameof(THREE.Object3D.matrixWorld), () => x.matrixWorld, n.Nodes);

            Add(nameof(THREE.Object3D.children), () => x.children, n.Nodes);

        }

        async void Add(string name, Func<THREE.Line> get_subject, TreeNodeCollection Nodes = null)
        {
            // RTTi we have idl, and KnownTypes approach. also theres the linq approach.

            // X:\jsc.svn\examples\javascript\WebGL\WebGLDashedLines\WebGLDashedLines\Application.cs
            var x = get_subject();
            var n = Nodes.Add("\{name} : \{nameof(THREE.Line)} '\{x.name}' [\{x.geometry.vertices.Length}]");
            await n.AsyncAfterExpand();

            Add("base", () => (THREE.Object3D)x, n.Nodes);
            Add(nameof(THREE.Line.geometry), () => x.geometry, n.Nodes);
            Add(nameof(THREE.Line.material), () => x.material, n.Nodes);

            // can we get enum strings yet?
            Add(nameof(THREE.Line.mode), () => x.mode, n.Nodes);
        }


        async void Add(string name, Func<THREE.Camera> get_subject, TreeNodeCollection Nodes = null)
        {
            // overload seems to work nicely. yet we have to do manual base types /RTTI 

            var n = Nodes.Add("\{name} : \{nameof(THREE.Camera)}");
            await n.AsyncAfterExpand();

            var xCamera = get_subject();

            Add("base", () => (THREE.Object3D)xCamera, n.Nodes);

            //Add(nameof(THREE.Camera.matrixWorld), () => xCamera.projectionMatrix, n.Nodes);
            Add(nameof(THREE.Camera.projectionMatrix), () => xCamera.projectionMatrix, n.Nodes);
        }

        // http://threejs.org/docs/#Reference/Cameras/PerspectiveCamera
        async void Add(string name, Func<THREE.PerspectiveCamera> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.PerspectiveCamera)}");
            await n.AsyncAfterExpand();

            var xPerspectiveCamera = get_subject();
            Add("base", () => (THREE.Camera)xPerspectiveCamera, n.Nodes);

            //n.Nodes.Add("base : \{nameof(THREE.Camera)} : \{nameof(THREE.Object3D)}");
            // cant add margin in winfors?


            Add(nameof(THREE.PerspectiveCamera.aspect), () => xPerspectiveCamera.aspect, n.Nodes);
            Add(nameof(THREE.PerspectiveCamera.fov), () => xPerspectiveCamera.fov, n.Nodes);
            Add(nameof(THREE.PerspectiveCamera.far), () => xPerspectiveCamera.far, n.Nodes);
            Add(nameof(THREE.PerspectiveCamera.near), () => xPerspectiveCamera.near, n.Nodes);

            //Add(nameof(THREE.PerspectiveCamera.up), () => xPerspectiveCamera.up, n.Nodes);
            //Add(nameof(THREE.PerspectiveCamera.position), () => xPerspectiveCamera.position, n.Nodes);

        }

        async void Add(string name, Func<THREE.OrthographicCamera> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.OrthographicCamera)}");
            await n.AsyncAfterExpand();

            var xPerspectiveCamera = get_subject();
            Add("base", () => (THREE.Camera)xPerspectiveCamera, n.Nodes);

            Add(nameof(THREE.OrthographicCamera.bottom), () => xPerspectiveCamera.bottom, n.Nodes);
            Add(nameof(THREE.OrthographicCamera.left), () => xPerspectiveCamera.left, n.Nodes);
            Add(nameof(THREE.OrthographicCamera.right), () => xPerspectiveCamera.right, n.Nodes);
            Add(nameof(THREE.OrthographicCamera.top), () => xPerspectiveCamera.top, n.Nodes);
        }



        async void Add(string name, Func<THREE.Vector3> get_subject, TreeNodeCollection Nodes = null)
        {
            // overload seems to work nicely. yet we have to do manual base types /RTTI 

            var n = Nodes.Add(
                "\{name} : \{nameof(THREE.Vector3)}(x,y,z)"
            );

            await n.AsyncAfterExpand();


            var xVector3 = get_subject();

            Add(nameof(THREE.Vector3.x), () => xVector3.x, n.Nodes);
            Add(nameof(THREE.Vector3.y), () => xVector3.y, n.Nodes);
            Add(nameof(THREE.Vector3.z), () => xVector3.z, n.Nodes);

            var frame = 0;
            do
            {
                frame++;

                await Task.Delay(1000 / 5);

                //refresh
                xVector3 = (THREE.Vector3)get_subject();
            }
            while (true);
        }


        async void Add(string name, Func<THREE.Matrix4> get_subject, TreeNodeCollection Nodes = null)
        {
            // overload seems to work nicely. yet we have to do manual base types /RTTI 

            var n = Nodes.Add("\{name} : \{nameof(THREE.Matrix4)}");

            var xMatrix4 = get_subject();

            //Add("base", () => (THREE.Object3D)xCamera, n.Nodes);

            //Add(nameof(THREE.Camera.matrixWorld), () => xCamera.projectionMatrix, n.Nodes);
            //Add(nameof(THREE.Matrix4.projectionMatrix), () => xCamera.projectionMatrix, n.Nodes);
        }

        public async void Add(string name, Func<object[]> get_subject, TreeNodeCollection Nodes = null)
        {
            var a = get_subject();
            var n = Nodes.Add("+ \{name}[\{a.Length}]");

            await n.AsyncAfterExpand();

            n.Text = "\{name}[\{a.Length}]";

            for (int i = 0; i < a.Length; i++)
            {
                Add("[\{i}]", () => a[i], n.Nodes);
            }
        }


        async void Add(string name, Func<THREE.Light> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.Light)}");
            var x = get_subject();
            Add("base", () => (THREE.Object3D)x, n.Nodes);
        }

        async void Add(string name, Func<THREE.AmbientLight> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.AmbientLight)}");
            await n.AsyncAfterExpand();
            var x = get_subject();
            Add("base", () => (THREE.Light)x, n.Nodes);
        }

        async void Add(string name, Func<THREE.DirectionalLight> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.DirectionalLight)}");
            await n.AsyncAfterExpand();
            var x = get_subject();
            Add("base", () => (THREE.Light)x, n.Nodes);
            Add(nameof(THREE.DirectionalLight.position), () => x.position, n.Nodes);
        }

        async void Add(string name, Func<THREE.Mesh> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.Mesh)}");
            var x = get_subject();
            n.Text += " '\{x.name}' (\{x.children.Length})";

            await n.AsyncAfterExpand();
            Add("base", () => (THREE.Object3D)x, n.Nodes);
            Add(nameof(THREE.Mesh.geometry), () => (object)x.geometry, n.Nodes);
            Add(nameof(THREE.Mesh.material), () => (object)x.material, n.Nodes);
        }

        async void Add(string name, Func<THREE.Scene> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.Scene)}");
            await n.AsyncAfterExpand();
            var x = get_subject();
            Add("base", () => (THREE.Object3D)x, n.Nodes);
            Add(nameof(THREE.Scene.fog), () => x.fog, n.Nodes);
        }

        async void Add(string name, Func<THREE.Quaternion> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.Quaternion)}");
            await n.AsyncAfterExpand();
            var x = get_subject();
            //Add("base", () => (THREE.Object3D)x, n.Nodes);
            //Add(nameof(THREE.Scene.fog), () => x.fog, n.Nodes);
        }
        //THREE.Quaternion

        // http://threejs.org/docs/#Reference/Materials/MeshPhongMaterial
        async void Add(string name, Func<THREE.MeshPhongMaterial> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.MeshPhongMaterial)}");
            await n.AsyncAfterExpand();
            var x = get_subject();
            Add("base", () => (THREE.Material)x, n.Nodes);
            Add(nameof(THREE.MeshPhongMaterial.color), () => x.color, n.Nodes);
            Add(nameof(THREE.MeshPhongMaterial.ambient), () => x.ambient, n.Nodes);
            Add(nameof(THREE.MeshPhongMaterial.specular), () => x.specular, n.Nodes);
            Add(nameof(THREE.MeshPhongMaterial.shininess), () => x.shininess, n.Nodes);
            Add(nameof(THREE.MeshPhongMaterial.map), () => x.map, n.Nodes);
        }

        async void Add(string name, Func<THREE.MeshLambertMaterial> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.MeshLambertMaterial)}");
            await n.AsyncAfterExpand();
            var x = get_subject();
            Add("base", () => (THREE.Material)x, n.Nodes);
            Add(nameof(THREE.MeshLambertMaterial.color), () => x.color, n.Nodes);
            Add(nameof(THREE.MeshLambertMaterial.ambient), () => x.ambient, n.Nodes);
            Add(nameof(THREE.MeshLambertMaterial.emissive), () => x.emissive, n.Nodes);
            Add(nameof(THREE.MeshLambertMaterial.map), () => x.map, n.Nodes);
        }


        async void Add(string name, Func<THREE.Material> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.Material)}");
            await n.AsyncAfterExpand();
            var x = get_subject();
            Add(nameof(THREE.Material.program), () => x.program, n.Nodes);
            Add(nameof(THREE.Material.type), () => x.type, n.Nodes);
        }

        async void Add(string name, Func<THREE.BoxGeometry> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.BoxGeometry)}");
            await n.AsyncAfterExpand();
            var x = get_subject();
            Add("base", () => (THREE.Geometry)x, n.Nodes);
        }

        async void Add(string name, Func<THREE.Geometry> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.Geometry)}");
            await n.AsyncAfterExpand();
            var x = get_subject();
            Add(nameof(THREE.Geometry.dynamic), () => x.dynamic, n.Nodes);
            Add(nameof(THREE.Geometry.type), () => x.type, n.Nodes);
            Add(nameof(THREE.Geometry.vertices), () => x.vertices, n.Nodes);
            Add(nameof(THREE.Geometry.faces), () => x.faces, n.Nodes);
        }

        async void Add(string name, Func<THREE.Color> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.Color)}(r,g,b)");
            await n.AsyncAfterExpand();
            var x = get_subject();

            // looking at audio/color
            // X:\jsc.svn\examples\javascript\ColorPickerExperiment\ColorPickerExperiment\Library\ColorPicker.cs

            Add(nameof(THREE.Color.r), () => x.r, n.Nodes);
            Add(nameof(THREE.Color.g), () => x.g, n.Nodes);
            Add(nameof(THREE.Color.b), () => x.b, n.Nodes);
            //Add(nameof(THREE.Color.a), () => x.a, n.Nodes);
        }


        async void Add(string name, Func<THREE.Fog> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.Fog)}");
            await n.AsyncAfterExpand();
            var x = get_subject();
            Add(nameof(THREE.Fog.color), () => x.color, n.Nodes);
            Add(nameof(THREE.Fog.far), () => x.far, n.Nodes);
            Add(nameof(THREE.Fog.near), () => x.near, n.Nodes);

            //Add(nameof(THREE.Color.a), () => x.a, n.Nodes);
        }


        public void Add(Func<object> get_subject,
                // https://msdn.microsoft.com/en-us/library/hh534540.aspx
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0,
                [CallerFileLine] string sourceFileLine = ""
            )
        {
            Add(
                // could we reuse our IDL parser to parse inline C#?
                name: sourceLineNumber + "\t" + sourceFileLine.SkipUntilOrEmpty("=>").TakeUntilOrEmpty(")"),
                get_subject: get_subject,
                Nodes: null
            );
        }

        public async void Add(string name, Func<object> get_subject, TreeNodeCollection Nodes = null)
        {
            // should we do avalon treeview for syntax color and icons?


            // jsc reflector like code huh
            // this time not on avalon, but for forms and with async
            if (Nodes == null)
                Nodes = this.treeView1.Nodes;


            //if (get_subject == null)
            //{
            //    var n = Nodes.Add(name);

            //    return;
            //}




            var subject = get_subject();

            if (subject == null)
            {
                var n = Nodes.Add("\{name} = null");
                return;
            }

            #region double ? wont work yet
            // is numeric?
            //var xDouble = subject as double?;
            var isDouble = subject is double;
            if (isDouble)
            {
                //Error CS1001  Identifier expected WebGLRah66Comanche ZeProperties.cs 53
                //var n = Nodes.Add("\{name} : \{nameof(double)}");
                var n = Nodes.Add("\{name} : \{nameof(Double)}");


                return;
            }
            #endregion

            var xstring = subject as string;
            if (xstring != null)
            {
                Nodes.Add("\{name} = '\{xstring}'");
                return;
            }



            var xFog = subject as THREE.Fog;
            if (xFog != null)
            {
                Add(name, () => xFog, Nodes);
                return;
            }


            var xGroup = subject as THREE.Group;
            if (xGroup != null)
            {
                Add(name, () => xGroup, Nodes);
                return;
            }

            var xColor = subject as THREE.Color;
            if (xColor != null)
            {
                Add(name, () => xColor, Nodes);
                return;
            }

            var xQuaternion = subject as THREE.Quaternion;
            if (xQuaternion != null)
            {
                Add(name, () => xQuaternion, Nodes);
                return;
            }


            var xBoxGeometry = subject as THREE.BoxGeometry;
            if (xBoxGeometry != null)
            {
                Add(name, () => xBoxGeometry, Nodes);
                return;
            }


            var xGeometry = subject as THREE.Geometry;
            if (xGeometry != null)
            {
                Add(name, () => xGeometry, Nodes);
                return;
            }

            var xMeshLambertMaterial = subject as THREE.MeshLambertMaterial;
            if (xMeshLambertMaterial != null)
            {
                Add(name, () => xMeshLambertMaterial, Nodes);
                return;
            }

            var xMeshPhongMaterial = subject as THREE.MeshPhongMaterial;
            if (xMeshPhongMaterial != null)
            {
                Add(name, () => xMeshPhongMaterial, Nodes);
                return;
            }

            var xMaterial = subject as THREE.Material;
            if (xMaterial != null)
            {
                Add(name, () => xMaterial, Nodes);
                return;
            }

            var xMesh = subject as THREE.Mesh;
            if (xMesh != null)
            {
                Add(name, () => xMesh, Nodes);
                return;
            }

            var xLine = subject as THREE.Line;
            if (xLine != null)
            {
                Add(name, () => xLine, Nodes);
                return;
            }

            // THREE.AmbientLight
            var xAmbientLight = subject as THREE.AmbientLight;
            if (xAmbientLight != null)
            {
                Add(name, () => xAmbientLight, Nodes);
                return;
            }

            var xDirectionalLight = subject as THREE.DirectionalLight;
            if (xDirectionalLight != null)
            {
                Add(name, () => xDirectionalLight, Nodes);
                return;
            }


            #region Scene
            // http://threejs.org/docs/#Reference/Scenes/Scene
            var xScene = subject as THREE.Scene;
            if (xScene != null)
            {
                Add(name, () => xScene, Nodes);
                return;
            }
            #endregion


            #region WebGLRenderer
            var xWebGLRenderer = subject as THREE.WebGLRenderer;
            if (xWebGLRenderer != null)
            {
                var n = Nodes.Add("\{name} : \{nameof(THREE.WebGLRenderer)}");

                Add(nameof(THREE.WebGLRenderer.domElement), () => xWebGLRenderer.domElement, n.Nodes);
                Add(nameof(THREE.WebGLRenderer.autoClear), () => xWebGLRenderer.autoClear, n.Nodes);
                Add(nameof(THREE.WebGLRenderer.shadowMapEnabled), () => xWebGLRenderer.shadowMapEnabled, n.Nodes);
                Add(nameof(THREE.WebGLRenderer.shadowMapType), () => xWebGLRenderer.shadowMapType, n.Nodes);

                return;
            }
            #endregion

            #region xOrthographicCamera      
            var xOrthographicCamera = subject as THREE.OrthographicCamera;
            if (xOrthographicCamera != null)
            {
                Add(name, () => xOrthographicCamera, Nodes);
                return;
            }
            #endregion

            #region xPerspectiveCamera      
            var xPerspectiveCamera = subject as THREE.PerspectiveCamera;
            if (xPerspectiveCamera != null)
            {
                Add(name, () => xPerspectiveCamera, Nodes);
                return;
            }
            #endregion



            #region xOrbitControls      
            var xOrbitControls = subject as THREE.OrbitControls;
            if (xOrbitControls != null)
            {
                var n = Nodes.Add("\{name} : " + nameof(THREE.OrbitControls));
                await n.AsyncAfterExpand();
                Add(nameof(THREE.OrbitControls.center), () => xOrbitControls.center, n.Nodes);
                Add(nameof(THREE.OrbitControls.@object), () => xOrbitControls.@object, n.Nodes);
                Add(nameof(THREE.OrbitControls.domElement), () => xOrbitControls.domElement, n.Nodes);

                return;
            }
            #endregion

            var xVector3 = subject as THREE.Vector3;
            if (xVector3 != null)
            {
                Add(name, () => xVector3, Nodes);
                return;
            }

            var xObject3D = subject as THREE.Object3D;
            if (xObject3D != null)
            {
                Add(name, () => xObject3D, Nodes);
                return;
            }

            var xCallerFileLineAttribute = subject as CallerFileLineAttribute;
            if (xCallerFileLineAttribute != null)
            {
                var n = Nodes.Add("\{name} : \{nameof(CallerFileLineAttribute)}");
                await n.AsyncAfterExpand();
                Add(nameof(CallerFileLineAttribute.sourceFilePath), () => xCallerFileLineAttribute.sourceFilePath, n.Nodes);
                Add(nameof(CallerFileLineAttribute.sourceLineNumber), () => xCallerFileLineAttribute.sourceLineNumber, n.Nodes);
                Add(nameof(CallerFileLineAttribute.sourceFileLine), () => xCallerFileLineAttribute.sourceFileLine, n.Nodes);

                return;
            }


            Nodes.Add("\{name} : \{subject.GetType()} = \{subject}");
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void treeView1_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(new { e.KeyCode });

        }

        private void ZeProperties_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 0.9;
        }

        private void ZeProperties_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.7;
        }
    }
}
