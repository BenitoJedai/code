using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace WebGLEscherDrosteEffect.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __ChocoluxFragmentShader : ScriptCoreLib.GLSL.FragmentShader
    {
        [varying]
        vec3[] s = new vec3[4];


        void main()
        {
        	float t, b, c, h = 0.0f;
            vec3 m = default(vec3), n;
	        vec3 p = vec3(.2f);
	        vec3 d = normalize(.001f * gl_FragCoord.rgb - p);

	        for (int ii = 0; ii < 4; ii++)
	        {
		        t=2.0f;
		        for (int i = 0; i < 4; i++)
		        {
			        b = dot(d, n = s[i] - p);
			        c = b * b + .2f - dot(n, n);
			        if (b - c < t)
			        {
				        if (c > 0.0)
				        {
					        m = s[i];
					        t = b - c;
				        }
			        }
		        }
		        p += t * d;
		        d = reflect(d, n = normalize(p - m));
		        h += pow(n.x * n.x, 44.0f) + n.x * n.x * .2f;
	        }
	        gl_FragColor = vec4(h, h * h, h * h * h * h, h);
        }


    }
}
