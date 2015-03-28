#define time (iGlobalTime * 0.5 - 2.0)

#define dithering			4.01
#define antialiasing		0.50

#define samples 32

#define brightness	1.0
#define gamma		1.0

//#define crossEyeStereo
//#define nonlinearPerspective

struct material {
	vec3 color;
	vec3 light;
	float spec;
};

struct hit {
	vec3 p;
	vec3 n;
	float t;
	material m;
};


vec2 sphere (vec3 v, float f, vec3 p, vec3 d) {
	vec3 rp = p - v;
	
	float b = dot (d, rp);
	float c = dot (rp, rp) - f*f;
	
	float g = b*b - c;
	
	if (g < 0.0)
		return vec2 (-1);
	
	float root = sqrt (g);
	return vec2 (-root, root) - b;

}

hit scene (const in vec3 p, const in vec3 d) {
	hit h;h.t = 1e20;
	float c = sin (time * 2.0) * 0.5 + 1.5;
	
	float t;
	vec2 t2;
	vec3 t3;
	
	vec3 invd = 1.0 / d;
	
	material m1 = material (vec3 ( 1.0, 1.0, 1.0), vec3 ( 0.0, 0.0, 0.0),-1.0);
	material m2 = material (vec3 ( 0.8, 0.5, 0.2), vec3 ( 0.0, 0.0, 0.0),-1.0);
	material m3 = material (vec3 ( 0.2, 0.5, 0.8), vec3 ( 0.0, 0.0, 0.0),-1.0);
	material m4 = material (vec3 ( 0.5, 0.7, 0.8), vec3 ( 0.0, 0.0, 0.0),-1.0);
	material m5 = material (vec3 ( 1.0, 1.0, 1.0), vec3 ( 1.0, 1.0, 1.0),-1.0);
	material m6 = material (vec3 ( 1.0, 0.0, 0.0), vec3 ( 0.0, 0.0, 0.0), 2.0);
	
	t3 = vec3 (0,0.5,0);
	t2 = sphere (t3, 0.5, p, d);
	
	t = t2.x;
	if (all (greaterThan (vec2 (t, h.t), vec2 (0, t)))) {
		vec3 r = p + d * t;
		if (r.y < 0.25)
			h = hit (r, normalize (r - t3), t, m2);
	}
	t = t2.y;
	if (all (greaterThan (vec2 (t, h.t), vec2 (0, t)))) {
		vec3 r = p + d * t;
		if (r.y < 0.25) {
			h = hit (r, normalize (r - t3), t, m1);
			h.m.color *= mod (dot (fract (r * 4.0), vec3 (1)), 2.0) * 0.5 + 0.5;
		}
	}
	t2 = sphere (t3, 0.4, p, d);
	t = t2.x;
	if (all (greaterThan (vec2 (t, h.t), vec2 (0, t)))) {
		vec3 r = p + d * t;
		h = hit (r, normalize (r - t3), t, m1);
		h.m.spec = 
			dot (cos (h.p.yz * 20.0), vec2 (h.p.x*h.p.x)) +
			dot (cos (h.p.xz * 20.0), vec2 (h.p.y*h.p.y)) +
			dot (cos (h.p.xy * 20.0), vec2 (h.p.z*h.p.z));
		h.m.spec *= 10.0;
	}
	
	t2 = sphere (t3, 1.0, p, d);
	
	t = t2.x;
	if (all (greaterThan (vec2 (t, h.t), vec2 (0, t)))) {
		vec3 r = p + d * t;
		float m = dot (r.yz, r.yz) * 0.9999999;
		if (any (lessThan (vec2 (abs (r.z), m), vec2 (0.25, 0.5)))) {
			h = hit (r, normalize (r - t3), t, m3);
		}
	}
	t = t2.y;
	if (all (greaterThan (vec2 (t, h.t), vec2 (0, t)))) {
		vec3 r = p + d * t;
		float m = dot (r.yz, r.yz) * 0.9999999;
		if (any (lessThan (vec2 (abs (r.z), m), vec2 (0.25, 0.5)))) {
			h = hit (r, normalize (r - t3), t, m5);
			h.m.light = fract (floor (m * 6.0) * 0.5) * vec3 (0.0, 1.0, 1.0);
			h.m.color = vec3 (0.8, 0.2, 0.5);
		}
	}
	
	t = -p.y * invd.y;
	if (all (greaterThan (vec2 (t, h.t), vec2 (0, t)))) {
		vec3 r = p + d * t;
		h = hit (r, vec3 (0,1,0), t, m4);
		h.m.color *= mod (dot (floor (r.xz), vec2 (1)), 2.0) * 0.5 + 0.5;
	}
	t = (3.0 - p.y) * invd.y;
	if (all (greaterThan (vec2 (t, h.t), vec2 (0, t)))) {
		vec3 r = p + d * t;
		h = hit (r, vec3 (0,1,0), t, m5);
		h.m.light *= sin (time * 0.2) * 0.5 + 0.5;
	}
	t = (4.0 - p.x) * invd.x;
	if (all (greaterThan (vec2 (t, h.t), vec2 (0, t)))) {
		vec3 r = p + d * t;
		h = hit (r, vec3 (1,0,0), t, m1);
	}
	t = (-4.0 - p.x) * invd.x;
	if (all (greaterThan (vec2 (t, h.t), vec2 (0, t)))) {
		vec3 r = p + d * t;
		h = hit (r, vec3 (1,0,0), t, m1);
	}
	t = (4.0 - p.z) * invd.z;
	if (all (greaterThan (vec2 (t, h.t), vec2 (0, t)))) {
		vec3 r = p + d * t;
		h = hit (r, vec3 (0,0,1), t, m6);
	}
	t = (-4.0 - p.z) * invd.z;
	if (all (greaterThan (vec2 (t, h.t), vec2 (0, t)))) {
		vec3 r = p + d * t;
		h = hit (r, vec3 (0,0,1), t, m1);
	}
	
	h.n = faceforward (h.n, h.n, d);
	
	return h;
}

float seed1;
float rndValue () {
	seed1 = fract (seed1 * 3.01);
	return seed1;
}
vec2 rndUnit2D () {
	float angle = rndValue () * 6.28318530718;
	return vec2 (sin (angle), cos (angle));
}
vec2 rnd2D () {
	float radius = sqrt (rndValue ());
	return rndUnit2D () * radius;
}
vec3 rndUnit3D () {
	float z = rndValue () * 2.0 - 1.0;
	vec2 xy = rndUnit2D () * sqrt (1.0 - z*z);
	return vec3 (xy, z);
}

vec3 dir (const in vec3 n, const in vec3 d, const in float p) {
	vec3 r = reflect (d, n);
	vec3 b = rndUnit3D ();
	
	vec3 na = normalize (cross (n, b));
	vec3 nb = cross (n, na);
	
	vec3 ra = normalize (cross (r, b));
	vec3 rb = cross (r, ra);
	
	vec2 ng = rnd2D ();
	float nl = length (ng);
	
	float t = pow (nl, p);
	vec2 rg = ng * t;
	float rl = nl * t;
	
	vec3 o;
	if (p < 0.0)
		o = ng.x * na + ng.y * nb + n * sqrt (1.0 - nl*nl);
	else
		o = rg.x * ra + rg.y * rb + r * sqrt (1.0 - rl*rl);
	
	return o + n * clamp (-dot (o, n), 0.0, 1.0) * 2.0;
}

vec3 rayColor (const in vec3 p, const in vec3 d) {
	hit h;
	vec3 p1 = p;
	vec3 d1 = d;
	
	vec3 light = vec3 (0);
	vec3 color = vec3 (1);
	for (int i = 0; i < 3; i++) {
		h = scene (p1, d1);
		light += h.m.light * color;
		color *= h.m.color;
		
		d1 = dir (h.n, d1, h.m.spec);
		p1 = h.p + d1 * 0.001;
	}
	
	return light;
}

vec3 rayDirection (const in vec3 r, const in vec3 u, const in vec3 f, const in float fov, in vec2 cr) {
	vec2 rs = cr * 3.14159 * fov;
	vec2 sn = sin (rs);
	vec2 cs = cos (rs);
	
	#ifdef nonlinearPerspective
	return normalize (r * sn.x + u * sn.y + f * cs.x * cs.y);
	#else
	return normalize (r * cr.x * 4.0 * fov + u * cr.y * 4.0 * fov + f);
	#endif
}

void camera (inout vec3 right, inout vec3 up, inout vec3 forward, inout vec3 p, inout vec2 cr) {
	forward = normalize (forward);
	right = normalize (cross (up, forward));
	up = normalize (cross (forward, right));
	
	#ifdef crossEyeStereo
	float s = sign (cr.x);
	cr.x = (cr.x - s * 0.25);
	p -= right * s / iResolution.x * 200.0;
	#endif
	
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	seed1 = 1.0;
	
	vec2 tmp = mod (fragCoord.xy + time, dithering);
	seed1 += tmp.x + tmp.y / dithering;
	
	//seed1 += mod (fragCoord.x + fragCoord.y * 2.0, dithering);
	
	vec2 cr = (fragCoord.xy - iResolution.xy * 0.5) / iResolution.x;
	vec3 p = vec3 (sin (time) * 2.0, sin (time) + 1.25, cos (time) * 2.0);

	vec3 right, up = vec3 (0,1,0), forward = vec3 (0,0.75,0)-p;
	camera (right, up, forward, p, cr);
	
	vec3 color = vec3 (0);
	for (int i = 0; i < samples; i++) {
		vec3 d = rayDirection (
			right, up, forward, 0.5,
			cr + (vec2 (rndValue (), rndValue ()) * 2.0 - 1.0) * antialiasing / iResolution.x);
		
		color += rayColor (p, d);
	}
	color = pow (color / float (samples), vec3 (gamma));
	fragColor = vec4 (color * brightness, 0.0);
}