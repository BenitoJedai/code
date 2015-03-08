using ScriptCoreLib.GLSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromeShaderToyColumns.Shaders
{
	class __ProgramFragmentShader : FragmentShader
	{
		// all fields are inferred to be uniform?

		#region generic
		[uniform]
		vec3 iResolution;			// viewport resolution (in pixels)
		[uniform]
		float iGlobalTime;			 // shader playback time (in seconds)
									 //[uniform]
									 //float iChannelTime[4];       // channel playback time (in seconds)
									 //[uniform]
									 //vec3 iChannelResolution[4]; // channel resolution (in pixels)
		[uniform]
		vec4 iMouse;				// mouse pixel coords. xy: current (if MLB down), zw: click
									//[uniform]
									//samplerXX iChannel0..3;          // input channel. XX = 2D/Cube
		[uniform]
		vec4 iDate;					// (year, month, day, time in seconds)
		[uniform]
		float iSampleRate;			 // sound sample rate (i.e., 44100)
		#endregion


		// https://github.com/tparisi/3dsMaxWebGL/blob/master/exporter/webgl/webgl2.cpp


		float smin(float a, float b, float k)
		{
			float h = clamp(0.5f + 0.5f * (b - a) / k, 0.0f, 1.0f);
			return mix(b, a, h) - k * h * (1.0f - h);
		}

		float fog(float dist, float density)
		{
			var LOG2 = -1.442695f;
			float d = density * dist;
			return 1.0f - clamp(exp2(d * d * LOG2), 0.0f, 1.0f);
		}

		float udBox(vec3 p, vec3 b) => length(max(abs(p) - b, 0.0f));

		float sdBox(vec3 p, vec3 b)
		{
			vec3 d = abs(p) - b;
			return min(max(d.x, max(d.y, d.z)), 0.0f) +
				   length(max(d, 0.0f));
		}

		// n must be normalized
		float sdPlane(vec3 p, vec4 n) => dot(p, n.xyz) + n.w;

		//------------------------------------------------------------------------
		// Camera
		//
		// Move the camera. In this case it's using time and the mouse position
		// to orbitate the camera around the origin of the world (0,0,0), where
		// the yellow sphere is.
		//------------------------------------------------------------------------
		void doCamera(out vec3 camPos, out vec3 camTar, float time, float mouseX)
		{
			float an = 0.3f * iGlobalTime + 10.0f * mouseX;
			camPos = vec3(3.5f * sin(an) + sin(iGlobalTime * 0.1f) * 15.0f, 1.1f + sin(iGlobalTime * 0.25f), 3.5f * cos(an));
			camTar = vec3(0.0f, 0.0f, 0.0f);
		}


		//------------------------------------------------------------------------
		// Background 
		//
		// The background color. In this case it's just a black color.
		//------------------------------------------------------------------------
		vec3 doBackground()
		{
			vec3 c = vec3(1.2f, 0.9f, 0.975f) * 0.05f;

			c = mix(c, vec3(0.9f, 0.95f, 1.3f) * 0.3f, 1.0f - gl_FragCoord.y / iResolution.y * 1.0f);

			return c;
			//  - fragCoord.y/iResolution.y*0.01;
		}

		//------------------------------------------------------------------------
		// Modelling 
		//
		// Defines the shapes (a sphere in this case) through a distance field, in
		// this case it's a sphere of radius 1.
		//------------------------------------------------------------------------
		float doModel(vec3 p)
		{
			vec3 idx = vec3(floor(abs(p.xz - 0.5f)), 0.5f);
			p.xz = mod(p.xz + 0.5f, 1.0f) - 0.5f;
			float h = sin(length(idx.xy * 0.5f) + iGlobalTime * 1.5f) * 0.6f;
			float d = sdBox(p, vec3(0.05f, h, 0.05f));

			d = smin(d, sdPlane(p, normalize(vec4(0, 1, 0, 0))), 0.035f);

			return d;
		}

		//------------------------------------------------------------------------
		// Material 
		//
		// Defines the material (colors, shading, pattern, texturing) of the model
		// at every point based on its position and normal. In this case, it simply
		// returns a constant yellow color.
		//------------------------------------------------------------------------
		vec3 doMaterial(vec3 pos, vec3 nor) => vec3(0.15f, 0.2f, 0.23f);

		//------------------------------------------------------------------------
		// Lighting
		//------------------------------------------------------------------------
		//float calcSoftshadow( in vec3 ro, in vec3 rd);

		vec3 doLighting(vec3 pos, vec3 nor, vec3 rd, float dis, vec3 mal)
		{
			vec3 lin = vec3(0.0f);

			// key light
			//-----------------------------
			vec3 lig = normalize(vec3(1.0f, 0.7f, 0.9f));
			float dif = max(dot(nor, lig), 0.0f);
			float sha = 0.0f; if (dif > 0.01) sha = calcSoftshadow(pos + 0.01f * nor, lig);
			lin += dif * vec3(4.00f, 4.00f, 4.00f) * sha;

			// ambient light
			//-----------------------------
			lin += vec3(0.50f, 0.50f, 0.50f);


			// surface-light interacion
			//-----------------------------
			vec3 col = mal * lin;


			return col;
		}

		float calcIntersection(vec3 ro, vec3 rd)
		{
			const float maxd = 20.0f;			// max trace distance
			const float precis = 0.001f;		// precission of the intersection
			float h = precis * 2.0f;
			float t = 0.0f;
			float res = -1.0f;
			for (int i = 0; i < 90; i++)		  // max number of raymarching iterations is 90
			{
				if (h < precis || t > maxd) break;
				h = doModel(ro + rd * t);
				t += h;
			}

			if (t < maxd) res = t;
			return res;
		}

		vec3 calcNormal(vec3 pos)
		{
			const float eps = 0.002f;			  // precision of the normal computation

			var v1 = vec3(1.0f, -1.0f, -1.0f);
			var v2 = vec3(-1.0f, -1.0f, 1.0f);
			var v3 = vec3(-1.0f, 1.0f, -1.0f);
			var v4 = vec3(1.0f, 1.0f, 1.0f);

			return normalize(v1 * doModel(pos + v1 * eps) +
							  v2 * doModel(pos + v2 * eps) +
							  v3 * doModel(pos + v3 * eps) +
							  v4 * doModel(pos + v4 * eps));
		}

		float calcSoftshadow(vec3 ro, vec3 rd)
		{
			float res = 1.0f;
			float t = 0.0005f;				   // selfintersection avoidance distance
			float h = 1.0f;
			for (int i = 0; i < 40; i++)		 // 40 is the max numnber of raymarching steps
			{
				h = doModel(ro + rd * t);
				res = min(res, 48.0f * h / t);	 // 64 is the hardness of the shadows
				t += clamp(h, 0.02f, 2.0f);	  // limit the max and min stepping distances
			}
			return clamp(res, 0.0f, 1.0f);
		}

		mat3 calcLookAtMatrix(vec3 ro, vec3 ta, float roll)
		{
			vec3 ww = normalize(ta - ro);
			vec3 uu = normalize(cross(ww, vec3(sin(roll), cos(roll), 0.0f)));
			vec3 vv = normalize(cross(uu, ww));
			return mat3(uu, vv, ww);
		}

		void mainImage(out vec4 fragColor, vec2 fragCoord)
		{
			vec2 p = (-iResolution.xy + 2.0f * fragCoord.xy) / iResolution.y;
			vec2 m = iMouse.xy / iResolution.xy;

			//-----------------------------------------------------
			// camera
			//-----------------------------------------------------

			// camera movement
			vec3 ro, ta;
			doCamera(out ro, out ta, iGlobalTime, m.x);

			// camera matrix
			mat3 camMat = calcLookAtMatrix(ro, ta, 0.0f);  // 0.0 is the camera roll

			// create view ray
			vec3 rd = normalize(camMat * vec3(p.xy, 2.0f));	// 2.0 is the lens length

			//-----------------------------------------------------
			// render
			//-----------------------------------------------------

			vec3 col = doBackground();

			// raymarch
			float t = calcIntersection(ro, rd);
			if (t > -0.5)
			{
				// geometry
				vec3 pos = ro + t * rd;
				vec3 nor = calcNormal(pos);

				// materials
				vec3 mal = doMaterial(pos, nor);

				col = mix(doLighting(pos, nor, rd, t, mal), col, fog(t, max(0.0f, 0.2f - pos.y * 0.3f)));
			}

			//-----------------------------------------------------
			// postprocessing
			//-----------------------------------------------------
			// gamma
			col = pow(clamp(col, 0.0f, 1.0f), vec3(0.4545f));
			col.g = smoothstep(0.0f, 1.05f, col.g);
			col.r = smoothstep(0.1f, 1.1f, col.r);
			col *= 1.0f + dot(p, p * 0.08f);

			fragColor = vec4(col, 1.0f);
		}
	}
}
