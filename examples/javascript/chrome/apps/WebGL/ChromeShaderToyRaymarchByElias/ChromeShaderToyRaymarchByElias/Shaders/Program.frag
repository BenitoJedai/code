// [x] Pointlights
// [x] Spotlights
// [x] Reflection
// [x] Materials
// [x] Soft shadows
// [ ] Antialiasing
// [ ] Transparency

// Precision
#define P 0.001
// Steps
#define S 64
// Distance
#define D 15.
// Time in ms
#define T iGlobalTime
// Shadows softness
#define K 1.5
// Number of reflections
#define R 1

#define PI 3.1415
#define TAU 6.283
#define DEBUG false

/* ========================== */
/* ====== STRUCT SETUP ====== */
/* ========================== */

struct Ray
{
	vec3 p; // position
	vec3 d; // direction
};
	
struct PointLight
{
	vec3 p; // position
	vec3 c; // color
};
    
struct SpotLight
{
	vec3  p; // position
	vec3  d; // direction
    vec3  c; // color
    float a; // angle
};

struct Hit
{
	vec3  p; // position
	float t; // distance traveled
	float d; // distance to object
};

struct Material
{
    vec3  c; // color
    float d; // diffuse
    float s; // specular
    float r; // reflection
    float a; // absorbtion
};

/* ========================= */
/* ====== SCENE SETUP ====== */
/* ========================= */

// Camera
Ray cam;

// Distance to objects in scene
float d1, d2, dl;

// Flags for scene conditionals
bool IN_SHADOW_MARCH  = false;
bool IN_REFLECT_MARCH = false;

// Lights and materials
const int  numPointLights = 3;
const int  numSpotLights  = 0;
const int  numMaterials   = 2;
const vec3 ambientColor   = vec3(0);

// Setup arrays
PointLight pointLights [numPointLights +1];
SpotLight  spotLights  [numSpotLights  +1];
Material   materials   [numMaterials     ];

// Forward declarations
Hit castRay(Ray,vec2);
vec3 getNormal(vec3);

void initialize()
{  
    // Hit h = castRay(cam,(2.*iMouse.xy-iResolution.xy)/iResolution.xx);
    
    float y = 1.;
    
    vec3 p1 = 0.5*vec3(cos(TAU*1./3.-PI/6.),y,sin(TAU*1./3.-PI/6.));
    vec3 p2 = 0.5*vec3(cos(TAU*2./3.-PI/6.),y,sin(TAU*2./3.-PI/6.));
    vec3 p3 = 0.5*vec3(cos(TAU*3./3.-PI/6.),y,sin(TAU*3./3.-PI/6.));

    pointLights[0] = PointLight(p1,vec3(1,1,0));
    pointLights[1] = PointLight(p2,vec3(0,1,1));
    pointLights[2] = PointLight(p3,vec3(1,0,1));
   
    // pointLights[0] = PointLight(h.p+getNormal(h.p)*0.5,2.*vec3(1,0,0));
    // spotLights[0] = SpotLight(vec3(0,3,0),vec3(0,-1,0),vec3(10,0,10),PI/8.);
 
    materials[0] = Material(vec3(1),1.0,1.0,0.5,0.5);
    materials[1] = Material(vec3(1),1.0,1.0,0.5,0.0);
}

/* ============================= */
/* ====== SCENE UTILITIES ====== */
/* ============================= */

// Rotation
mat3 rotZ(float a){float s=sin(a),c=cos(a);return mat3(c,-s,0,s,c,0,0,0,1);}
mat3 rotX(float a){float s=sin(a),c=cos(a);return mat3(1,0,0,0,c,s,0,-s,c);}
mat3 rotY(float a){float s=sin(a),c=cos(a);return mat3(c,0,-s,0,1,0,s,0,c);} 

// Distance functoins
float udBox(vec3 p,vec3 s,float r){return length(max(abs(p)-s,0.))-r;}
float sdSphere(vec3 p,float r){return length(p)-r;}
float sdFloor(vec3 p,float h){return p.y-h;}
float sdCylinder(vec3 p,float r){return length(p.xz)-r;}

// Miscellaneous
vec3 repeat(vec3 p,vec3 s){return mod(p-s/2.,s)-s/2.;}
vec3 getNormal(vec3);

vec3 rrepeat(vec3 p, float r, float n, float s)
{
    float a = mod(s+atan(p.x,p.z),TAU/n)-PI/n;
    float l = length(p.xz);
    return vec3(l*cos(a)-r,p.y,l*sin(a));
}

float scene(vec3 p)
{
	float d = d2 = dl = 1e10;

	d1 = sdFloor(p,-0.15);
    d2 = min(d2,udBox(rrepeat(p,1.,3.,0.),vec3(0.15),0.01));
    d2 = min(d2,sdSphere(rrepeat(p,1.,3.,PI/3.),0.15));
    d2 = min(d2,sdCylinder(rrepeat(p,1.,3.,0.),0.1));
    
    if (IN_SHADOW_MARCH == false && DEBUG == true)
    {
    	// Render light sources as dots
	    for(int i = 0; i < numPointLights; i++)
	    {
	        dl = min(dl,sdSphere(p-pointLights[i].p,0.02));
	    }
    }
       
	d = min(d,d1);
	d = min(d,d2);
    d = min(d,dl);

	return d;
}

/* ====================== */
/* ====== MARCHING ====== */
/* ====================== */

Hit march(Ray r)
{
	// Position on ray
	vec3 p;
	// Distance traveled w/ offset to prevent self-occlusion
	float t = 0.001;
	// Distance to object in scene
	float d;
	
	for(int i = 0; i < S; i++)
	{
		p = r.p + r.d*t;
		d = scene(p);

		// Near object or too far away
		if (d<P||t>D) break;

		t += d;
	}

	// Applying the values manually instead of using the
	// constructor prevents a black screen on some devices
   	Hit h;

    h.p = p;
    h.t = t;
    h.d = d;

	return h;
}

float getShadow(vec3 source, vec3 target)
{
    IN_SHADOW_MARCH = true;
    
    // Light value (1=lit;0=unlit)
	float l = 1.0;
	// Distance traveled w/ offset to prevent self-occlusion
	float t = 0.1;
	// Distance between source and target
	float r = length(source-target);
	// Distance to object
	float d;
	// Marching direction
	vec3 dir = normalize(target-source);
	
	for(int i = 0; i < S; i++)
	{
		d = scene(source+dir*t);

		// Direct occlusion => no light
        if (d<P) return 0.0;
        // No occlusion or unreachable => light
        if (t>r||t>D) break;

		l = min(l,K*d/clamp(t,0.,.1));
		t += d;
	}
	
	return l/r;
}

vec3 getColor(Hit h)
{
    vec3 col;
    float ref;
    
    // Reflection loop
    for(int i = 0; i < R+1; i++)
    {
    	// No occlusion
        if (h.d>P) break;

        vec3 p = h.p;
        vec3 n = getNormal(p);
        vec3 r = normalize(reflect(p-cam.p,n));
        vec3 c = vec3(ambientColor);
        
        float d = 1e10; Material m;

        if (d1<d) { d = d1; m = materials[0]; /*m.c=vec3(floor(fract(p.x+0.5*floor(fract(p.z)+.5))+0.5));*/ }
        if (d2<d) { d = d2; m = materials[1]; }
        if (dl<d) { return vec3(1); }
        
        // First object determines the amount of added reflection
        if (i==0) { ref = m.r; }

        // Calculate point light
        for(int i = 0; i < numPointLights; i++)
        {
            PointLight l = pointLights[i];
            vec3 ln = normalize(l.p);
            
            c += getShadow(p,l.p) * mix(l.c*(
                
                m.d * max(dot(n,ln),0.)/exp(length(p-l.p)) +
                m.s * pow(max(dot(reflect(-ln,n),normalize(cam.p-p)),0.),50.)
                
           	), m.c, m.a);
        }
        
        // Calculate spot light
        for(int i = 0; i < numSpotLights; i++)
        {
            SpotLight l = spotLights[i];
            vec3 ln = normalize(l.p);
            float mask = pow(l.a/max(acos(dot(l.d,normalize(p-l.p))),l.a),20.);

            c += getShadow(p,l.p) * mask * mix(l.c * (
                
                m.d*max(dot(n,ln),0.)/exp(length(p-l.p)) +
                m.s*pow(max(dot(reflect(-ln,n),normalize(cam.p-p)),0.),50.)
                
            ), m.c, m.a);
        }

        // No reflection in the first iteration
        col = (i==0) ? c : mix(col,c,ref);
        
        // Only continue if the current object is reflective
        if (m.r == 0.0) break;
        h = march(Ray(p,r));
    }
    
	return clamp(col,0.,1.);
}

/* ================================ */
/* ====== MARCHING UTILITIES ====== */
/* ================================ */

vec3 getNormal(vec3 p)
{
	vec2 e = vec2(P,0);

	return normalize(vec3(
		scene(p+e.xyy)-scene(p-e.xyy),
		scene(p+e.yxy)-scene(p-e.yxy),
		scene(p+e.yyx)-scene(p-e.yyx)
	));
}

Ray lookAt(Ray r, vec2 c)
{
	vec3 di = normalize(r.d-r.p);
	vec3 up = vec3(0,1,0);
	vec3 ri = cross(up,di);

	up  = cross(di,ri);
    r.d = normalize(di + ri*c.x + up*c.y);

    return r;
}

Hit castRay(Ray src, vec2 p)
{
	Ray ray = lookAt(src,p);
    Hit hit = march(ray);
    
    return hit;
}

/* ================== */
/* ====== MAIN ====== */
/* ================== */

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    cam.p = 3.*vec3(0,0.7,1)*rotY(T*0.5);
    cam.d = vec3(0,0,0);

    initialize();
    
    vec2 uv = (2.*fragCoord.xy-iResolution.xy)/iResolution.xx;
	fragColor = vec4(getColor(march(lookAt(cam,uv))),1);
}