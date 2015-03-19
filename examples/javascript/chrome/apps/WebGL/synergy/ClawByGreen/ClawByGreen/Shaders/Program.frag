// nvidia claw logo
// respect to the original designer!
// @simesgreen

const int maxSteps = 64;
const float hitThreshold = 0.001;
const int shadowSteps = 64;

// CSG operations
float _union(float a, float b)
{
    return min(a, b);
}

float intersect(float a, float b)
{
    return max(a, b);
}

float difference(float a, float b)
{
    return max(a, -b);
}

// transforms
vec3 rotateX(vec3 p, float a)
{
    float sa = sin(a);
    float ca = cos(a);
    return vec3(p.x, ca*p.y - sa*p.z, sa*p.y + ca*p.z);
}

vec3 rotateY(vec3 p, float a)
{
    float sa = sin(a);
    float ca = cos(a);
    return vec3(ca*p.x + sa*p.z, p.y, -sa*p.x + ca*p.z);
}

mat3 rotationMat(vec3 v, float angle)
{
	float c = cos(angle);
	float s = sin(angle);
	
	return mat3(c + (1.0 - c) * v.x * v.x, (1.0 - c) * v.x * v.y - s * v.z, (1.0 - c) * v.x * v.z + s * v.y,
		(1.0 - c) * v.x * v.y + s * v.z, c + (1.0 - c) * v.y * v.y, (1.0 - c) * v.y * v.z - s * v.x,
		(1.0 - c) * v.x * v.z - s * v.y, (1.0 - c) * v.y * v.z + s * v.x, c + (1.0 - c) * v.z * v.z
		);
}

// primitive functions
// these all return the distance to the surface from a given point

  // n must be normalized
float sdPlane( vec3 p, vec4 n )
{
  return dot(p,n.xyz) + n.w;
}

float sdBox( vec3 p, vec3 b )
{
  vec3  di = abs(p) - b;
  float mc = max(di.x, max(di.y, di.z));
  return min(mc,length(max(di,0.0)));
}

float sphere(vec3 p, float r)
{
    return length(p) - r;
}

float cylinder(vec3 p, vec2 c, float r)
{
  return length(p.xy - c) - r;
}

float claw(vec3 p)
{
	float d;
	d = cylinder(p - vec3(2.0, -2.0, 0.0), vec2(0.0), 4.0);
	d = intersect(d, cylinder(p - vec3(-2.0, -2.0, 0.0), vec2(0.0), 4.0));	

	float d2;
	d2 = cylinder(p - vec3(2.0, -3.0, 0.0), vec2(0.0), 4.0);
	d2 = intersect(d2, cylinder(p - vec3(-1.0, -5.4, 0.0), vec2(0.0), 6.0));
	//d2 = intersect(d2, cylinder(p - vec3(-0.5, -3.5, 0.0), vec2(0.0), 4.0));
	
	d = difference(d, d2);
	
	d = intersect(d, sdBox(p, vec3(2.0, 2.0, 0.4)));
	return d;
}

float hash( float n )
{
    return fract(sin(n)*43758.5453123);
}

// distance to scene
float scene(vec3 p)
{		
    float d;
	
    d = sdPlane(p, vec4(0, 1, 0, 1)); 
		
	p -= vec3(0.0, 1.0, 0.0);
	
	//d = _union(d, claw(p));
	
	const int n = 4;
	for(int i=0; i<n; i++) {
		float seed = float(i)*157.0 + floor(iGlobalTime*0.1);
		vec3 axis = normalize(vec3(hash(seed), hash(seed+1.0), hash(seed+2.0))*2.0-1.0);
		float ang = hash(seed+3.0) * 3.14159*2.0 + iGlobalTime*0.5;
		vec3 pr = p * rotationMat(axis, ang);
		pr -= axis*1.5;
		//float scale = 0.5 + 1.5*hash(seed+4.0);		
		//pr *= scale;
		d = _union(d, claw(pr));
		//d /= scale;
	}
	
    return d;
}

// calculate scene normal
vec3 sceneNormal(in vec3 pos )
{
    float eps = 0.0001;
    vec3 n;
    float d = scene(pos);
    n.x = scene( vec3(pos.x+eps, pos.y, pos.z) ) - d;
    n.y = scene( vec3(pos.x, pos.y+eps, pos.z) ) - d;
    n.z = scene( vec3(pos.x, pos.y, pos.z+eps) ) - d;
    return normalize(n);
}

// ambient occlusion approximation
float ambientOcclusion(vec3 p, vec3 n)
{
    const int steps = 4;
    const float delta = 0.5;

    float a = 0.0;
    float weight = 1.0;
    for(int i=1; i<=steps; i++) {
        float d = (float(i) / float(steps)) * delta; 
        a += weight*(d - scene(p + n*d));
        weight *= 0.5;
    }
    return clamp(1.0 - a, 0.0, 1.0);
}

float softShadow(vec3 ro, vec3 rd, float mint, float maxt, float k)
{
    float dt = (maxt - mint) / float(shadowSteps);
    float t = mint;
    float res = 1.0;
    for( int i=0; i<shadowSteps; i++ )
    {
        float h = scene(ro + rd*t);
        res = min(res, k*h/t);
        t += dt;
		//t += max( 0.05, dt );
    }
    return clamp(res, 0.0, 1.0);
}


// trace ray using sphere tracing
vec3 trace(vec3 ro, vec3 rd, out bool hit)
{
    hit = false;
    vec3 pos = ro;
    vec3 hitPos = ro;

    for(int i=0; i<maxSteps; i++)
    {
		if (!hit) {
			float d = scene(pos);
			if (abs(d) < hitThreshold) {
				hit = true;
				hitPos = pos;
			}
			pos += d*rd;
		}
    }
    return pos;
}

// lighting
vec3 shade(vec3 pos, vec3 n, vec3 eyePos)
{
	vec3 color = vec3(0.9);
		
	const vec3 lightDir = vec3(0.577, 0.577, 0.577);
    //vec3 v = normalize(eyePos - pos);
    //vec3 h = normalize(v + lightDir);
    float diff = dot(n, lightDir);
    //diff = max(0.0, diff);
    diff = 0.5+0.5*diff;
	
    float ao = ambientOcclusion(pos, n);
	
	vec3 c = diff*ao*color;
	
	// point light
	const vec3 lightPos = vec3(0.0, 0.5, -4.0);
	const vec3 lightColor = vec3(0.55, 0.76, 0.26);		// nv green
	
	vec3 l = lightPos - pos;
	float dist = length(l);
	l /= dist;
	diff = max(0.0, dot(n, l));
	//diff *= 3.0 / dist;	// attenutation
	
	float maxt = dist;
    float shadow = softShadow( pos, l, 0.1, maxt, 5.0 );
	diff *= shadow;
	
	c += diff*lightColor;
	
//    return vec3(ao);
//    return n*0.5+0.5;
	return c;
}

vec3 background(vec3 rd)
{
    //return mix(vec3(1.0), vec3(0.0), max(0.0, rd.y));
    return vec3(0.75);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 pixel = (fragCoord.xy / iResolution.xy)*2.0-1.0;

    // compute ray origin and direction
    float asp = iResolution.x / iResolution.y;
    vec3 rd = normalize(vec3(asp*pixel.x, pixel.y, -2.0));
    vec3 ro = vec3(0.0, 1.0, 6.0);

	vec2 mouse = iMouse.xy / iResolution.xy;
	float roty = 0.0;
	float rotx = 0.0;
	if (iMouse.z > 0.0) {
		rotx = -(mouse.y-0.5)*3.0;
		roty = -(mouse.x-0.5)*6.0;
	}

    rd = rotateX(rd, rotx);
    ro = rotateX(ro, rotx);
		
    rd = rotateY(rd, roty);
    ro = rotateY(ro, roty);
		
    // trace ray
    bool hit;
    vec3 pos = trace(ro, rd, hit);

    vec3 rgb;
    if(hit)
    {
        // calc normal
        vec3 n = sceneNormal(pos);
        // shade
        rgb = shade(pos, n, ro);

#if 0
        // reflection
        vec3 v = normalize(ro - pos);
        float fresnel = 0.1 + 0.4*pow(max(0.0, 1.0 - dot(n, v)), 5.0);

        ro = pos + n*0.001; // offset to avoid self-intersection
        rd = reflect(-v, n);
        pos = trace(ro, rd, hit);

        if (hit) {
            vec3 n = sceneNormal(pos);
            rgb += shade(pos, n, ro) * vec3(fresnel);
        } else {
            rgb += background(rd) * vec3(fresnel);
        }
#endif 

     } else {
        rgb = background(rd);
     }
	
    fragColor=vec4(rgb, 1.0);
}