using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestIsInstNestedExternalTarget
{
    [Script(HasNoPrototype = true, ExternalTarget = "THREE.OrbitControls")]
    public class OrbitControls
    {
    }

    [Script]
    public class Class1
    {
        // "X:\jsc.svn\examples\javascript\Test\TestMissingNativeIsInstance\TestMissingNativeIsInstance.sln"
        // X:\jsc.svn\examples\javascript\WebGL\collada\WebGLRah66Comanche\WebGLRah66Comanche\Library\ZeProperties.cs
        public static void Add(object e)
        {
            // c = ('THREE' in __this && 'OrbitControls' in THREE && b instanceof THREE.OrbitControls);
            var x = e is OrbitControls;
            var z = e as OrbitControls;
        }
    }
}
