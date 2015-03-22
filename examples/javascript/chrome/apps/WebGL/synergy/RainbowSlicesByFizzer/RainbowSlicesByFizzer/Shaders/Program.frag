#define EPS vec2(1e-4, 0.0)

float time;

vec3 rotateX(float a, vec3 v)
{
   return vec3(v.x, cos(a) * v.y + sin(a) * v.z, cos(a) * v.z - sin(a) * v.y);
}

vec3 rotateY(float a, vec3 v)
{
   return vec3(cos(a) * v.x + sin(a) * v.z, v.y, cos(a) * v.z - sin(a) * v.x);
}

float sphere(vec3 p, float r)
{
   return length(p) - r;
}

float plane(vec3 p, vec4 n)
{
   return dot(p, n.xyz) - n.w;
}

float sceneDist(vec3 p)
{
   const int num_spheres = 32;

   float sd = 1e3;


   for(int i = 0; i < num_spheres; ++i)
   {
      float r = 0.22 * sqrt(float(i));
      vec3 p2 = rotateX(cos(time + float(i) * 0.2) * 0.15, p);
      float cd = -sphere(p2 + vec3(0.0, -0.9, 0.0), 1.3);
      sd = min(sd, max(abs(sphere(p2, r)), cd) - 1e-3);
   }

   return sd;
}

vec3 sceneNorm(vec3 p)
{
   float d = sceneDist(p);
   return normalize(vec3(sceneDist(p + EPS.xyy) - d, sceneDist(p + EPS.yxy) - d,
                           sceneDist(p + EPS.yyx) - d));
}

vec3 col(vec3 p)
{
   float a = length(p) * 20.0;
   return vec3(0.5) + 0.5 * cos(vec3(a, a * 1.1, a * 1.2));
}

// ambient occlusion approximation (thanks to simesgreen)
float ambientOcclusion(vec3 p, vec3 n)
{
    const int steps = 4;
    const float delta = 0.5;

    float a = 0.0;
    float weight = 3.0;
    for(int i=1; i<=steps; i++) {
        float d = (float(i) / float(steps)) * delta; 
        a += weight*(d - sceneDist(p + n*d));
        weight *= 0.5;
    }
    return clamp(1.0 - a, 0.0, 1.0);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
	vec2 t = uv * 2.0 - vec2(1.0);
	t.x *= iResolution.x / iResolution.y;
	
	time = iGlobalTime;
	
	vec3 ro = vec3(-0.4, sin(time * 2.0) * 0.05, 0.7), rd = rotateX(1.1, rotateY(0.5, normalize(vec3(t.xy, -0.8))));
	float f = 0.0;
	vec3 rp, n;
	
	for(int i = 0; i < 100; ++i)
	{
		rp = ro + rd * f;
		float d = sceneDist(rp);
		
		if(abs(d) < 1e-4)
			break;
		
		f += d;
	}
	
	n = sceneNorm(rp);
	
	vec3 l = normalize(vec3(1.0, 1.0, -1.0));
	
	float ao = ambientOcclusion(rp, n);
	
	fragColor.rgb = vec3(0.5 + 0.5 * clamp(dot(n, l), 0.0, 1.0)) * col(rp) * mix(0.1, 1.0, ao) * 1.6;
	fragColor.a = 1.0;
}

