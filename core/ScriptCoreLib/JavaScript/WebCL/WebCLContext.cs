using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    partial class IWindow
    {

        // 20140714 - webcl seems close. when can we test it?

        // http://developer.amd.com/community/blog/2014/03/19/chrome-web-browser-webcl-available-download/
        // https://www.khronos.org/registry/webcl/specs/1.0.0/
        // http://techcrunch.com/2014/03/19/webcl-will-soon-let-web-developers-harness-the-power-of-multi-core-gpus-and-cpus-from-the-browser/
        // http://webcl.nokiaresearch.com/
        // https://github.com/toaarnio/webcl-firefox
        // http://toaarnio.github.io/webcl-test-suite/run.html?device=DEFAULT&spec=Platform&debug=true
        // http://webcl.nokiaresearch.com/extensions/firefox/multiplatform/latest/webcl-1.0.xpi

        // WARNING! This WebCL implementation is experimental and is likely to introduce severe security vulnerabilities in your system. Use it cautiously and at your own risk. This setting is also available in Advanced Settings (about:config) as extensions.webcl.allowed.
        // Your browser supports WebCL in principle, but unfortunately no OpenCL driver was found.  You may want to try updating your display driver, or installing a CPU-based OpenCL implementation (such as the Intel OpenCL SDK or the AMD APP SDK).
        // ---------------------------
        //OpenCL™ runtime for Intel® Core™ and Xeon® Processors, and Intel® Xeon Phi™ coprocessors Setup
        //---------------------------
        //Intel® Manycore Platform Software Stack Driver (Intel® MPSS) is not installed. Install the driver to enable the Intel® Xeon Phi™ coprocessor support.
        //---------------------------
        //OK   
        //---------------------------


        // https://software.intel.com/en-us/articles/opencl-drivers
        // https://code.google.com/p/ocltoys/
        // https://www.youtube.com/watch?v=TurCVdaUTMY
        // https://github.com/markbecker/WebCL_Raytracer
        // https://github.com/toaarnio/CL.js

        // readonly attribute WebCL webcl;

        [Obsolete("hidden, will browsers actually want to implement it?")]
        public WebCL.WebCL webcl;


        // https://bugzilla.mozilla.org/show_bug.cgi?id=664147
        // http://en.wikipedia.org/wiki/WebCL
        // ?
        //  Mozilla does not plan to implement WebCL in favor of OpenGL ES 3.1 Compute Shaders.


    }
}

namespace ScriptCoreLib.JavaScript.WebCL
{
    [Obsolete("hidden, will browsers actually want to implement it?")]
    [Script(HasNoPrototype = true, InternalConstructor = true)]
    public class WebCL
    {
		// nvidea has 3072 cores!
		// https://twitter.com/nvidia/status/577873538700345344

		// http://www.gabrielecocco.it/fscl/what-is-fscl/

		// "X:\opensource\googlecode\ocltoys\juliagpu\rendering_kernel.cl"
		// jsc, hows your C generation skills? ready for kernel code? what about outputting GLSL yet?
		// X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Object.cs


		public WebCLContext createContext()
        {
            return default(WebCLContext);
        }
    }

    [Script(HasNoPrototype = true, InternalConstructor = true)]
    public class WebCLContext
    {
        // need redux to import type info from webidl?

    }
}
