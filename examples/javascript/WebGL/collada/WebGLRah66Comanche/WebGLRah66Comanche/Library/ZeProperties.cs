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
            this.groupBox1.Text = new { e.Node.Text, e.Node.Tag }.ToString();

        }

        private void ZeProperties_Load(object sender, EventArgs e)
        {

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


        async void Add(string name, Func<THREE.Object3D> get_subject, TreeNodeCollection Nodes = null)
        {
            // overload seems to work nicely. yet we have to do manual base types /RTTI 

            var n = Nodes.Add("+ \{name} : \{nameof(THREE.Object3D)}");

            await n.AsyncAfterExpand();

            n.Text = "\{name} : \{nameof(THREE.Object3D)}";


            var xObject3D = get_subject();


            //Add(nameof(THREE.Camera.matrixWorld), () => xCamera.projectionMatrix, n.Nodes);



            // check type?
            Add(nameof(THREE.Object3D.parent), () => (object)xObject3D.parent, n.Nodes);


            Add(nameof(THREE.Object3D.position), () => xObject3D.position, n.Nodes);
            Add(nameof(THREE.Object3D.rotation), () => xObject3D.rotation, n.Nodes);
            Add(nameof(THREE.Object3D.scale), () => xObject3D.scale, n.Nodes);

            Add(nameof(THREE.Object3D.matrix), () => xObject3D.matrix, n.Nodes);
            Add(nameof(THREE.Object3D.quaternion), () => xObject3D.quaternion, n.Nodes);
            Add(nameof(THREE.Object3D.visible), () => xObject3D.visible, n.Nodes);

            Add(nameof(THREE.Object3D.castShadow), () => xObject3D.castShadow, n.Nodes);
            Add(nameof(THREE.Object3D.receiveShadow), () => xObject3D.receiveShadow, n.Nodes);

            Add(nameof(THREE.Object3D.matrixWorld), () => xObject3D.matrixWorld, n.Nodes);
            Add(nameof(THREE.Object3D.userData), () => xObject3D.userData, n.Nodes);

            Add(nameof(THREE.Object3D.children), () => xObject3D.children, n.Nodes);
        }


        async void Add(string name, Func<THREE.Camera> get_subject, TreeNodeCollection Nodes = null)
        {
            // overload seems to work nicely. yet we have to do manual base types /RTTI 

            var n = Nodes.Add("\{name} : \{nameof(THREE.Camera)}");

            var xCamera = get_subject();

            Add("base", () => (THREE.Object3D)xCamera, n.Nodes);

            //Add(nameof(THREE.Camera.matrixWorld), () => xCamera.projectionMatrix, n.Nodes);
            Add(nameof(THREE.Camera.projectionMatrix), () => xCamera.projectionMatrix, n.Nodes);
        }



        async void Add(string name, Func<THREE.Vector3> get_subject, TreeNodeCollection Nodes = null)
        {
            // overload seems to work nicely. yet we have to do manual base types /RTTI 

            var n = Nodes.Add(
                "+ \{name} : \{nameof(THREE.Vector3)}"
            );

            await n.AsyncAfterExpand();

            n.Text = "\{name} : \{nameof(THREE.Vector3)}";

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
            await n.AsyncAfterExpand();
            var x = get_subject();
            Add("base", () => (THREE.Object3D)x, n.Nodes);
            Add(nameof(THREE.Mesh.geometry), () => x.geometry, n.Nodes);
            Add(nameof(THREE.Mesh.material), () => x.material, n.Nodes);
        }

        async void Add(string name, Func<THREE.Scene> get_subject, TreeNodeCollection Nodes = null)
        {
            var n = Nodes.Add("\{name} : \{nameof(THREE.Scene)}");
            await n.AsyncAfterExpand();
            var x = get_subject();
            Add("base", () => (THREE.Object3D)x, n.Nodes);
            Add(nameof(THREE.Scene.fog), () => x.fog, n.Nodes);
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


            var xMesh = subject as THREE.Mesh;
            if (xMesh != null)
            {
                Add(name, () => xMesh, Nodes);
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


            #region xPerspectiveCamera      
            // http://threejs.org/docs/#Reference/Cameras/PerspectiveCamera
            var xPerspectiveCamera = subject as THREE.PerspectiveCamera;
            if (xPerspectiveCamera != null)
            {
                // EE

                // what about base props?
                var n = Nodes.Add("\{name} : \{nameof(THREE.PerspectiveCamera)}");

                Add("base", () => (THREE.Camera)xPerspectiveCamera, n.Nodes);

                //n.Nodes.Add("base : \{nameof(THREE.Camera)} : \{nameof(THREE.Object3D)}");
                // cant add margin in winfors?


                Add(nameof(THREE.PerspectiveCamera.aspect), () => xPerspectiveCamera.aspect, n.Nodes);
                Add(nameof(THREE.PerspectiveCamera.fov), () => xPerspectiveCamera.fov, n.Nodes);
                Add(nameof(THREE.PerspectiveCamera.far), () => xPerspectiveCamera.far, n.Nodes);
                Add(nameof(THREE.PerspectiveCamera.near), () => xPerspectiveCamera.near, n.Nodes);

                //Add(nameof(THREE.PerspectiveCamera.up), () => xPerspectiveCamera.up, n.Nodes);
                //Add(nameof(THREE.PerspectiveCamera.position), () => xPerspectiveCamera.position, n.Nodes);

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

            var xObject3D = subject as THREE.Object3D;
            if (xObject3D != null)
            {
                Add(name, () => xObject3D, Nodes);
                return;
            }

            Nodes.Add("\{name} : \{subject.GetType()} = \{subject}");
        }
    }
}
