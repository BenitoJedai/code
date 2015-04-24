using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGLLencoBearCat.__AssetsLibrary__
{
    [Obsolete("jsc should generate this")]
    public class ColladaLenco : THREE_ColladaAsset
    {
        //public string ID201 = "assets/WebGLNexus7/nexus-7_material_3.jpg";
        //public string ID222 = "assets/WebGLNexus7/nexus-7_big_ASUS_Nexus_7.jpg";

        //<image id="ID201">
        //    <init_from>nexus-7_material_3.jpg</init_from>
        //</image>
        //<image id="ID222">
        //    <init_from>nexus-7_big_ASUS_Nexus_7.jpg</init_from>
        //</image>

        public ColladaLenco()
            : base(
                // EmbeddedResource !
            // 44mb vs 4mb
                "assets/WebGLLencoBearCat/LencoKMZ.dae"
            // um, the file extracted from KMZ somewhat works.
            // if sketchup renders skp to dae either it is 44mb and works
            // or its 4mb and has some non friendly nodes for threejs.
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
