using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGLMRAP.__AssetsLibrary__
{
    [Obsolete("jsc should generate this")]
    public class ColladaMRAP : THREE_ColladaAsset
    {
        // https://3dwarehouse.sketchup.com/model.html?id=3afce20492cb901a4fc117e4bba6a423
        // dae is 23MB
        // we need to zip em up~!
        // KMZ!

        //public string ID201 = "assets/WebGLNexus7/nexus-7_material_3.jpg";
        //public string ID222 = "assets/WebGLNexus7/nexus-7_big_ASUS_Nexus_7.jpg";

        //<image id="ID201">
        //    <init_from>nexus-7_material_3.jpg</init_from>
        //</image>
        //<image id="ID222">
        //    <init_from>nexus-7_big_ASUS_Nexus_7.jpg</init_from>
        //</image>

        public ColladaMRAP()
            : base(
                // EmbeddedResource !
                "assets/WebGLMRAP/MRAP3.dae"
                )
        {

        }
    }

    public class THREE_ColladaAsset
    {
        // X:\jsc.svn\examples\javascript\WebGL\WebGLColladaExperiment\WebGLColladaExperiment\Application.cs

        public readonly TaskCompletionSource<THREE.Object3D> Source = new TaskCompletionSource<THREE.Object3D>();



        public THREE_ColladaAsset(string uri)
        {
            var loader = new THREE.ColladaLoader();

            loader.options.convertUpAxis = true;

            // this does NOT work correctly?
            //loader.options.centerGeometry = true;

            loader.load(
                //"assets/WebGLColladaExperiment/truck.dae",

                uri,

                new Action<THREE.ColladaLoaderResult>(
                    collada =>
                    {
                        var dae = collada.scene;


                        ////o.position .y = -80;
                        //scene.add(dae);
                        //oo.Add(dae);

                        //dae.scale = new THREE.Vector3(5, 5, 5);

                        this.Source.SetResult(dae);

                    }
                )
            );
        }
    }

}
