precision mediump float;

#define PI_2 1.57079632679
#define PI 3.14159265359
#define TWOPI 6.28318530718

float time = iGlobalTime;

// Description : Array and textureless GLSL 3D simplex noise function
// Author : Ian McEwan, Ashima Arts.
// License : Copyright (C) 2011 Ashima Arts. All rights reserved.
// Distributed under the MIT License. See LICENSE file.
// https://github.com/ashima/webgl-noise

vec3 mod289( vec3 x ) { return x - floor(x * (1.0 / 289.0)) * 289.0; }

vec4 mod289( vec4 x ) { return x - floor(x * (1.0 / 289.0)) * 289.0; }

vec3 permute( vec3 x ) { return mod289(((x*34.0)+1.0)*x); }

vec4 permute( vec4 x ) { return mod289(((x*34.0)+1.0)*x); }

vec4 taylorInvSqrt( vec4 r ) { return 1.79284291400159 - 0.85373472095314 * r; }

float snoise( vec3 v ) {
	const vec2 C = vec2(1.0/6.0, 1.0/3.0) ;
	const vec4 D = vec4(0.0, 0.5, 1.0, 2.0);

	// First corner
	vec3 i = floor(v + dot(v, C.yyy) );
	vec3 x0 = v - i + dot(i, C.xxx);

	// Other corners
  	vec3 g = step(x0.yzx, x0.xyz);
  	vec3 l = 1.0 - g;
  	vec3 i1 = min( g.xyz, l.zxy );
  	vec3 i2 = max( g.xyz, l.zxy );

	vec3 x1 = x0 - i1 + C.xxx;
	vec3 x2 = x0 - i2 + C.yyy;
	vec3 x3 = x0 - D.yyy;

	// Permutations
 	i = mod289(i);
  	vec4 p = permute( permute( permute( 
		i.z + vec4(0.0, i1.z, i2.z, 1.0 )) +
		i.y + vec4(0.0, i1.y, i2.y, 1.0 )) + 
		i.x + vec4(0.0, i1.x, i2.x, 1.0 ));
	
	//p = permute(p + seed); // optional seed value

	// Gradients: 7x7 points over a square, mapped onto an octahedron.
	// The ring size 17*17 = 289 is close to a multiple of 49 (49*6 = 294)
  	float n_ = 0.142857142857; // 1.0/7.0
 	vec3 ns = n_ * D.wyz - D.xzx;

	vec4 j = p - 49.0 * floor(p * ns.z * ns.z);
	
	vec4 x_ = floor(j * ns.z);
	vec4 y_ = floor(j - 7.0 * x_ );
	
	vec4 x = x_ *ns.x + ns.yyyy;
	vec4 y = y_ *ns.x + ns.yyyy;
	vec4 h = 1.0 - abs(x) - abs(y);
	
	vec4 b0 = vec4( x.xy, y.xy );
	vec4 b1 = vec4( x.zw, y.zw );

  	vec4 s0 = floor(b0)*2.0 + 1.0;
  	vec4 s1 = floor(b1)*2.0 + 1.0;
  	vec4 sh = -step(h, vec4(0.0));

  	vec4 a0 = b0.xzyw + s0.xzyw*sh.xxyy ;
  	vec4 a1 = b1.xzyw + s1.xzyw*sh.zzww ;

  	vec3 p0 = vec3(a0.xy,h.x);
  	vec3 p1 = vec3(a0.zw,h.y);
  	vec3 p2 = vec3(a1.xy,h.z);
  	vec3 p3 = vec3(a1.zw,h.w);

	// Normalize gradients
  	vec4 norm = taylorInvSqrt(vec4(dot(p0,p0), dot(p1,p1), dot(p2, p2), dot(p3,p3)));
  	p0 *= norm.x;
  	p1 *= norm.y;
  	p2 *= norm.z;
  	p3 *= norm.w;

	// Mix final noise value
  	vec4 m = max(0.6 - vec4(dot(x0,x0), dot(x1,x1), dot(x2,x2), dot(x3,x3)), 0.0);
  	m = m * m;
  	return 42.0 * dot( m*m, vec4(dot(p0,x0), dot(p1,x1), dot(p2,x2), dot(p3,x3)) );
}

// Dynamic fBm algorithms intended to circumvent 
// the constant comparator requirement in a GLSL for-loop
// p = point * initial frequency; l = lucinarity; a = amplitude; g = gain
// octaves should be a positive int but int not used to avoid casting
float fBm( vec3 p, float l, float a, float g, float octaves ) {	
	float res = 0.0; // result
	float f = 1.0;
	for (float i=0.0; i<6.0; i++) {
		if (i<octaves) res += a * snoise(p * f); 
		a *= g; 
		f *= l;
	}
	return res;
}
// Modified implementation that creates more "ridge-like" features
float fBm_abs( vec3 p, float l, float a, float g, float octaves ) {	
	float res = 0.0; // result
	float f = 1.0;
	for (float i=0.0; i<6.0; i++) {
		if (i<octaves) res += a * abs( snoise(p * f) ); 
		a *= g; 
		f *= l;
	}
	return res;
}

// Function to build a quaternion
vec4 quaternion(vec3 axis, float angle) {
	axis = normalize(axis);
	angle *= 0.5;
	vec4 q = vec4(axis.xyz * sin(angle), cos(angle));
	return q;
}

// Quaternion-based rotations are relatively inexpensive compared to typicl methods
vec3 rotate( vec3 vec, vec4 quat ) {
	return vec + 2.0 * cross( cross(vec, quat.xyz) + quat.w * vec, quat.xyz );
}

// Function to multiply two quaternions 
// This may be possible with fewer operations - need to investigate further
vec4 mulQuat(vec4 q1, vec4 q2) {
	return vec4( q1.x * q2.x - dot( q1.yzw, q2.yzw ),
				 vec3(q1.x * q2.yzw + q2.x * q1.yzw + cross( q1.yzw, q2.yzw )) );
}
	
// Commonly used quaternions
vec4 qY_05t = quaternion( vec3(0.0, 1.0, 0.0), mod(0.05 * time, TWOPI) ); 
vec4 qYn_05t = quaternion( vec3(0.0, 1.0, 0.0), -mod(0.05 * time, TWOPI) ); 
vec4 qY_03t = quaternion( vec3(0.0, 1.0, 0.0), mod(0.03 * time, TWOPI) ); 
vec4 qYn_01t = quaternion( vec3(0.0, 1.0, 0.0), -mod(0.01 * time, TWOPI) );
vec4 qX_05 = quaternion( vec3(1.0, 0.0, 0.0), 0.05 );
vec4 qZ_1 = quaternion( vec3(0.0, 0.0, 1.0), 0.1 ); 

// Constant Material IDs
#define M_PLANET 0
#define M_MOON 1
#define M_SUN 2

// Data structure for Celestial objects (planets, moon, sun)
struct Celestial {
	int material; // material ID
	vec3 origin; // center of object (after rotations)
	float radius; // radius of object
};
	
// Globally define the Celestial objects
Celestial planet, moon1, moon2, moon3, sun;

// Initializes the Celestial objects in this scene
void initialize() {
	planet.material = M_PLANET;
	planet.origin = vec3(0.0);
	planet.radius = 1.0;
	
	// Typical moon orbit, slightly inclined
	moon1.material = M_MOON;
	moon1.origin = rotate(vec3(4.0, 0.0, 0.0), mulQuat(qZ_1, qY_03t)); 
	moon1.radius = 0.03;
	
	// Moon with highly inclined retro-grade orbit (like Triton)
	moon2.material = M_MOON;
	moon2.origin = rotate(vec3(0.0, 0.0, 14.0), mulQuat(qX_05, qYn_01t));
	moon2.radius = 0.055;
	
	// Distant moon with typical orbit
	moon3.material = M_MOON;
	moon3.origin = rotate(vec3(0.0, 0.0, 8.0), mulQuat(qX_05, qY_05t));
	moon3.radius = 0.02;
	
	// At scale, sun radius would be ~423.7 (just a dot - which is no fun)
	sun.material = M_SUN;
	sun.origin = vec3(182885.0, 0.0, 0.0);
	sun.radius = 2000.0; 
}
	
// Data structure for Result sets	
struct Result {
	vec3 position; // intersect position
	vec3 normal; // normal at intersect position
	float depth; // distance from ray origin
	float edge; // sphere edge value (used for anti-aliasing)
	Celestial celestial; // the Celestial object data
};

// Celestial-Ray intersection test
// params: ro=ray origin; rd=ray direction; c=Celestial object
// return: Result(position, normal, depth, edge, celestial)
Result hitTest(vec3 ro, vec3 rd, Celestial c) {	
	Result res = Result(vec3(0.0), vec3(0.0), 1e9, 0.0, c);
	vec3 pd = c.origin - ro;
	float b = dot(rd, pd);
	float disc = b*b + c.radius*c.radius - dot(pd, pd+0.0001);
	if (disc>0.0) { // if ray hit the object
		res.edge = sqrt(disc);
		// This will give you hit inside or outside of the sphere
		// but that may be useful for something later
		float d = b - ( res.edge*sign(b) );
		if (d>0.0) res.depth = d;
	}
	return res;
}

// Returns Result set that is nearest (qiuck depth sort)
Result nearest (Result res1, Result res2) {
	return (res1.depth<res2.depth) ? res1 : res2;
}

Result map (vec3 ro, vec3 rd) {
	// Test ray intersection with all objects - store closest result
	Result res = hitTest(ro, rd, sun);
	res = nearest(res, hitTest(ro, rd, planet));
	res = nearest(res, hitTest(ro, rd, moon1));
	res = nearest(res, hitTest(ro, rd, moon2));
	res = nearest(res, hitTest(ro, rd, moon3));
	// Store the hit position in world space
	res.position = ro + rd*res.depth;
	// Store the normal at the hit position
	res.normal = normalize(res.position - res.celestial.origin);
	return res;
}

// Simple algorithm for calculating soft shadows cast by a 
// Celestial occluder. Treats the sun as a spherical light
// source rather than a point light source. In theory, this
// should create decent-looking umbras and penumbras that 
// are observed with real celestial objects
float shadowMap (vec3 ro, vec3 nor, Celestial o) {
	
	// Light data
	float lRad = sun.radius;
	vec3 lDir = sun.origin - ro;
	float lDis = length(lDir);
	lDir = normalize(lDir);
	
	// Occluder data
	float oRad = o.radius;
	vec3 oDir = o.origin - ro;
	float oDis = length(oDir);
	oDir = normalize(oDir);
	
	// Determine light visible "around" the occluder
	float l = lDis * ( length(cross(lDir, oDir)) - (oRad / oDis) );
	l = smoothstep(-1.0, 1.0, -l / lRad);
    l *= smoothstep(0.0, 0.2, dot(lDir, oDir));
	l *= smoothstep(0.0, oRad, lDis - oDis);
	
	// Return a multiplier representing our softshadow
	return 1.0-l;
}

vec4 getColor (Result res) {
	vec3 pos = res.position;
	vec3 nor = res.normal;
	float rad = res.celestial.radius;
	int mat = res.celestial.material;
	
	vec3 oCol = vec3(0.0); // base object color is black
	
	// Sun light
	vec3 lDir = normalize(sun.origin-pos); // direction
	vec3 lCol = vec3(1.0, 1.0, 0.8); // color
	
	// First diffuse caclulation is the same, regardless of which branch
	// the shader takes below. Keeping the branch instructions as short 
	// as possible seems to help the frame rate quite a bit
	float lDif = clamp(dot(nor, lDir), 0.01, 1.0);
	
	// Background stars
	if (res.depth>=1e9) {
		// Simplex noise algorithm doesn't work that well with really large numbers
		float ns1 = snoise( pos * 2e-7 );
		
		oCol = vec3(1.2) * smoothstep(0.9, 1.0, ns1);; // white stars
		oCol += vec3(1.6, 1.0, 1.0) * smoothstep(0.8, 0.95, -ns1); // red-shift stars
		oCol += vec3(1.0, 1.0, 1.6) * smoothstep(0.7, 0.85, ns1*ns1); // blue-shift stars
		
		return vec4(oCol, 1.0);
	}	
	// Planet
	else if (mat == M_PLANET) {
		// Rotate noise maps in different directions to simulate fluid clouds
		vec3 npPos1 = rotate(pos, qY_05t);
		vec3 npPos2 = rotate(pos, qYn_01t);
		// Banded noise
		float nc1 = fBm( npPos1 * vec3(0.4, 4.01, 0.4), 2.0, 0.5, 0.5, 2.0 );
		// Cloud noise
		float nc2 = fBm( npPos1 * vec3(1.2, 12.01, 1.2), 3.0, 0.5, 0.5, 4.0 );
		float nc3 = fBm( npPos2 * vec3(0.8, 6.01, 0.8), 2.0, 0.5, 0.5, 2.0 );
		
		// Add some blue bands
		oCol = mix( vec3(0.4, 0.5, 1.0), vec3(0.3, 0.4, 1.0), smoothstep(-0.5, 0.5, nc1) );
		// Add some white clouds
		oCol = mix( oCol, vec3(1.0), smoothstep(0.3, 1.0, nc2-nc3) );
		
		// Apply some alpha to smooth the edges; otherwise there's a
		// lot of pixelation visible with just one ray cast per pixel
		float a = smoothstep(0.1, rad*0.15, res.edge);
		oCol *= a;
		
		// Fake bump mapping to give clouds some more depth
		// It's basically just another cloud noise map, offset
		// proportional to the angle to the light source
		vec3 axis = cross(nor, -lDir);
		// Aproximate theta - no need for perfect accuracy
		float costh = dot(nor, lDir);
		float theta = (1.0-costh*costh) * PI_2;
		vec4 q = quaternion(axis, theta*0.02);
		npPos1 = rotate(npPos1, q);
		nc2 = fBm( npPos1 * vec3(1.2, 12.01, 1.2), 3.0, 0.5, 0.5, 4.0 );
		float ncbump = smoothstep(0.3, 1.0, nc2-nc3);
		ncbump *= smoothstep(0.7, 0.05, nc2-nc3);
		
		oCol *= (1.0-ncbump);
		
		a = smoothstep(0.0, rad*0.25, res.edge); // smooth edges
		oCol = mix( oCol, vec3(0.7, 0.8, 2.4), 1.0-a); // atmosphere
		
		// Add soft shadows
		lDif *= clamp(shadowMap(pos, nor, moon1), 0.01, 1.0);
		lDif *= clamp(shadowMap(pos, nor, moon2), 0.01, 1.0);
		lDif *= clamp(shadowMap(pos, nor, moon3), 0.01, 1.0);

		oCol *= lDif * lCol;
		return vec4(oCol, a);
	}
	// Moons
	else if (mat == M_MOON) {
		// Translate back to world origin for noise mapping; otherwise,
		// the noise value will change with orbital rotation
		vec3 nmPos = pos - res.celestial.origin;
		float nm = fBm_abs( nmPos * 40.0, 2.0, 0.5, 0.5, 3.0 );
		
		// Dont need much detail since these moons will always
		// be pretty far from the camera
		oCol = mix(vec3(0.7), vec3(0.3), nm);
		lDif *= clamp(shadowMap(pos, nor, planet), 0.01, 1.0);
		oCol *= lDif * lCol;
		return vec4(oCol, smoothstep(0.0, rad*0.15, res.edge));
	}
	// Sun
	else if (mat == M_SUN) {
		// Calculate squared-distance from sphere edge for alpha blending
		float a = res.edge / sun.radius;
		a *= a;
		
		// Sun is far away and small; no need to make it fancy
		oCol = vec3(1.2, 0.8, 0.0) * a;
		oCol += vec3(1.2, 1.0, 0.5) * ( max((a - 0.8), 0.0) );
		oCol += vec3(1.2, 1.2, 1.2) * ( max((a - 0.7), 0.0) );
		
		return vec4(oCol, a);
	}
	return vec4(oCol, 1.0);	
}

vec4 render (vec3 ro, vec3 rd) {
	Result res = map(ro, rd);
	vec4 color = getColor(res);
	
	// This attempts to correct for aliasing with a simple blending
	// technique. For performance reasons, only the color value of 
	// the next object along the ray is retrieved.
	if (color.a<1.0) {
		// We don't want to hit the same object again, so we'll jump way
		// ahead along the ray. Our objects are spaced far enough apart
		// this works just fine for our purposes
		ro = res.position + rd*res.celestial.radius*2.0;
		res = map(ro, rd);
		vec4 color2 = getColor(res);
		color = vec4(mix(color2.rgb, color.rgb, color.a), 1.0);
	}
	return color;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	// Get aspect ratio
	float aspect = iResolution.x / iResolution.y; // aspect ratio
	// Normalize screen coords
	vec2 pos = (fragCoord.xy / iResolution.xy) - 0.5;
	pos.x *= aspect;
	// Normalize mouse coords
	vec2 mouse = (iMouse.xy / iResolution.xy) - 0.5;
	mouse.x *= aspect; 

	// Initialize Celestial scene objects
	initialize();
	
	vec3 cPos = vec3(0.0, 0.0, 4.5);
	vec3 cLook = planet.origin;
	vec3 cUp = vec3(0.0, 1.0, 0.0); // up vector 
		
	vec4 qy = quaternion(vec3(0.0, 1.0, 0.0), mouse.x*TWOPI);
	vec4 qx = quaternion(vec3(1.0, 0.0, 0.0), -mouse.y*PI);
	cPos = rotate(cPos, mulQuat(qy, qx));
	
	vec3 ww = normalize( cLook-cPos );
	vec3 uu = normalize( cross(ww, cUp) );
	vec3 vv = normalize( cross(uu, ww) );
	
	// Cast a ray from this pixel
	vec3 rd = normalize( pos.x*uu + pos.y*vv + 2.0*ww );
	fragColor = render(cPos, rd);
}