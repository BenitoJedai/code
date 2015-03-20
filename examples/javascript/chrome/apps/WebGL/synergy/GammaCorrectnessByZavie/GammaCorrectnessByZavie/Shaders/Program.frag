/*

This shader demonstrates the effect of gamma
correction in rendering.

Move the mouse to see the difference between
shading with no gamma correction (on the left) and
in with gamma correction (on the right).

--
Zavie

*/

#define MAX_BOUNCES 2
float gamma = 2.2;

// ---8<----------------------------------------------------------------------
// Geometry

#define PI 3.14159275358979

struct Ray
{
    vec3 o;		// Origin
    vec3 d;		// Direction
};

struct Hit
{
    float t;	// ray solution
    vec3 n;		// normal
    int m;		// material
};
const Hit noHit = Hit(1e10, vec3(0.), -1);

struct Plane
{
    float d;	// offset
    vec3 n;		// normal
    int m;		// material
};

struct Sphere
{
	float r;	// radius
    vec3 p;		// center position
    int m;		// material
};

Hit intersectPlane(Plane p, Ray r)
{
    float dotnd = -dot(p.n, r.d);
    if (dotnd < 0.) return noHit;

 	float t = (dot(p.n, r.o) + p.d) / dotnd;
 	return Hit(t, p.n, p.m);
}

Hit intersectSphere(Sphere s, Ray r)
{
	vec3 op = s.p - r.o;
    float b = dot(op, r.d);
    float det = b * b - dot(op, op) + s.r * s.r;
    if (det < 0.) return noHit;
    
    det = sqrt(det);
    float t = b - det;
    if (t < 0.) t = b + det;
    if (t < 0.) return noHit;
    
    return Hit(t, (r.o + t*r.d - s.p) / s.r, s.m);
}

Hit intersectScene(Ray r)
{
    Sphere s = Sphere(1., vec3(0., 1., 0.), 0);
    Plane p  = Plane(0., vec3(0., 1., 0.), 1);

    Hit hit = Hit(1e5, vec3(0.), -1);
    Hit hitp = intersectPlane(p, r); if (hitp.m != -1 && hitp.t < hit.t) { hit = hitp; }
    Hit hits = intersectSphere(s, r); if (hits.m != -1 && hits.t < hit.t) { hit = hits; }
    return hit;
}

// ---8<----------------------------------------------------------------------
// Physics

struct Material
{
    vec3 c;		// diffuse color
    float f0;	// specular color (monochrome)
};

Material getMaterial(Hit hit)
{
    if (hit.m == 0)
    {
        return Material(vec3(1.0, 1.0, 1.0), 0.02);
    }
    else
    {
    	return Material(vec3(1.0, 1.0, 1.0), 0.02);
    }
}

vec3 sunCol = vec3(1.);
vec3 sunDir = normalize(vec3(-2., 1., 1.));
vec3 skyColor(vec3 d)
{
    return vec3(0.01);
}

float pow5(float x) { return x * x * x * x * x; }

// Schlick approximation
float fresnel(vec3 h, vec3 v, float f0)
{
  return pow5(1. - clamp(dot(h, v), 0., 1.)) * (1. - f0) + f0;
}

vec3 radiance(Ray r)
{
    float epsilon = 4e-4;

    vec3 accum = vec3(0.);
    vec3 filter = vec3(1.);

    for (int i = 0; i <= MAX_BOUNCES; ++i)
    {
        Hit hit = intersectScene(r);

        if (hit.m >= 0)
        {
            Material m = getMaterial(hit);
            float f = fresnel(hit.n, -r.d, m.f0);

            // Diffuse
            if (intersectScene(Ray(r.o + hit.t * r.d + epsilon * sunDir, sunDir)).m == -1)
            {
                accum += (1. - f) * filter * m.c * clamp(dot(hit.n, sunDir), 0., 1.) * sunCol;
            }
            
            // Specular: next bounce
            filter *= f;
            vec3 d = reflect(r.d, hit.n);
            r = Ray(r.o + hit.t * r.d + epsilon * d, d);
        }
        else
        {
            accum += filter * skyColor(r.d);
            break;
        }
    }
    return accum;
}

// ---8<----------------------------------------------------------------------
// Scene

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = 2. * fragCoord.xy / iResolution.xy - 1.;
    
    float o1 = 0.25;
    float o2 = 0.75;
    vec2 msaa[4];
    msaa[0] = vec2( o1,  o2);
    msaa[1] = vec2( o2, -o1);
    msaa[2] = vec2(-o1, -o2);
    msaa[3] = vec2(-o2,  o1);

    vec3 color = vec3(0.);
    for (int i = 0; i < 4; ++i)
    {
        vec3 p0 = vec3(0., 1.1, 4.);
        vec3 p = vec3(0., (2. * (iMouse.y==0.?.5*iResolution.y:iMouse.y) / iResolution.y - 1.), 0.) + p0;
        vec3 offset = vec3(msaa[i] / iResolution.y, 0.);
        vec3 d = normalize(vec3(iResolution.x/iResolution.y * uv.x, uv.y, -1.5) + offset);
        Ray r = Ray(p, d);
        color += radiance(r) / 4.;
    }

	if (iMouse.x < fragCoord.x)
        fragColor = vec4(pow(color, vec3(1./gamma)),1.0);
    else
        fragColor = vec4(color,1.0);
}
