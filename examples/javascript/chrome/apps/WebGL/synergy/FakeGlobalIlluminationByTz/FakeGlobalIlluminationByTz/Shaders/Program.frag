/////////////////////////////////////////////////
// Variable Conversions

vec2 resolution = iResolution.xy;
float aspect = resolution.x/resolution.y;
vec2 mouse = iMouse.xy;
float time = iGlobalTime;

/////////////////////////////////////////////////
// SD Objects

float sdPlane( vec3 p, vec4 n ){ return dot(p,n.xyz) + n.w + 0.1*sin(p.z*3.0)*sin(p.x*3.0); }

float sdBox( vec3 p, vec3 b ) {
	vec3 d = abs(p) - b;
	return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0)) - 0.1;
}

float curtain( vec3 p, vec3 b ) {
	vec2 c = vec2(sin(1.56), cos(1.56)); // Rotate!!!
	p.xz = vec2(c.y*p.x - c.x*p.z, c.x*p.x + c.y*p.z);
	vec3 d = abs(p + 0.1*sin(p.z*9.0 + sin(p.z*2.0))*sin(p.x*9.0)) - b;
	return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0)) - 0.1;
}

float sdSphere( vec3 p, float s ) { return length(p)-s; }

float sdTorus( vec3 p, vec2 t ) {
	vec2 c = vec2(sin(time), cos(time)); // Rotate!!!
	p.yz = vec2(c.y*p.y - c.x*p.z, c.x*p.y + c.y*p.z);
	vec2 q = vec2(length(p.xz)-t.x,p.y);
	return length(q)-t.y;
}

/////////////////////////////////////////////////
// Raymarching Functions

// Objects

float ground;
vec3 ground_pos = vec3(0.0, -0.55, 0.0);
const vec3 ground_color = vec3(0.8);

float box1;
vec3 box1_pos = vec3(sin(time - 2.5), 0.6 - sin(time)*0.2, -cos(time - 2.5));
const vec3 box1_dim = vec3(0.5);
const vec3 box1_color = vec3(0.1, 0.5, 1.0);

float box2;
vec3 box2_pos = vec3(-2.0, 0.0, 0.0);
const vec3 box2_dim = vec3(0.1, 5.5, 3.0);
const vec3 box2_color = vec3(1.0);

float box3;
vec3 box3_pos = vec3(-2.0, 0.0, -2.0);
const vec3 box3_dim = vec3(20.5, 10.5, 0.1);
const vec3 box3_color = vec3(0.0,0.0,0.0);

float box4;
vec3 box4_pos = vec3(5.0, 10., -1.5);
vec3 box4_dim = vec3(0.1, 5.5 + sin(time*0.5)*5.5, 15.0);
const vec3 box4_color = vec3(0.6, 0.0, 0.0);

float torus;
vec3 torus_pos = vec3(sin(time - 4.5), 0.3, -cos(time - 4.5));
const vec2 torus_dim = vec2(0.7, 0.1);
const vec3 torus_color = vec3(0.0, 0.6, 0.0);

float sphere1;
vec3 sphere1_pos = vec3(sin(time), 0.0, -cos(time));
const float sphere1_radius = 0.5;
const vec3 sphere1_color = vec3(1.0, 0.1, 0.1);

float sphere2;
vec3 sphere2_pos = vec3(sin(time)*5. + 2.5, 2.0 - sin(time), -cos(time));
const float sphere2_radius = 0.5;
const vec3 sphere2_color = vec3(0.9, 0.1, 1.0);

// Only Scene Distance Info
float scene(vec3 p){
	float d = 1e10;
	
	ground = sdPlane(p - ground_pos, vec4(0., 1., 0., 0.));
	box1 = sdBox(p - box1_pos, box1_dim);
	box2 = sdBox(p - box2_pos, box2_dim);
	box3 = sdBox(p - box3_pos, box3_dim);
	box4 = curtain(p - box4_pos, box4_dim);
	sphere1 = sdSphere(p - sphere1_pos, sphere1_radius);
	sphere2 = sdSphere(p - sphere2_pos, sphere2_radius);
	torus = sdTorus(p - torus_pos, torus_dim);
	
	d = min(d, ground);
	d = min(d, box1);
	d = min(d, box2);
	d = min(d, box3);
	d = min(d, box4);
	d = min(d, sphere1);
	d = min(d, sphere2);
	d = min(d, torus);
	
	return d;
}

vec3 scene_color(vec3 p){ // Disjoint function so you only evaluate colors once
	float d = 1e10;
	
	vec3 col = vec3(0.0);
	
	if (ground < d){
		col = ground_color;
		d = ground;
	}
	
	if (box1 < d){
		col = box1_color;
		d = box1;
	}
	
	if (box2 < d){
		col = box2_color;
		d = box2;
	}
	
	if (box3 < d){
		////////////////////////
		
		vec4 tex =  texture2D(iChannel0, mod(p.xy * 0.15 * vec2(1.0, aspect), 1.0));
		col = box3_color + (tex*tex + tex*0.5).xyz; // Higher Contrast!
		d = box3;
	}
	
	if (box4 < d){
		col = box4_color;
		d = box4;
	}
	
	if (sphere1 < d){
		col = sphere1_color;
		d = sphere1;
	}
	
	if (sphere2 < d){
		col = sphere2_color;
		d = sphere2;
	}
	
	if (torus < d){
		col = torus_color;
		d = torus;
	}
	
	return col;
}

// Interpolated Scene Color Info
vec3 scene_color_lerp(vec3 p){
	vec3 col;
	float d = 1e10;
	
	float ground_weight = 0.0;
	float box1_weight = 0.0;
	float box2_weight = 0.0;
	float box3_weight = 0.0;
	float box4_weight = 0.0;
	float sphere1_weight = 0.0;
	float sphere2_weight = 0.0;
	float torus_weight = 0.0;
	
	#define epsmod 0.0105
	#define GI 0.7

	// Conditions to prevent self illumination
	if (ground > epsmod + ground_pos.y) ground_weight = GI/(ground + 1.5);
	if (box1 > epsmod) box1_weight = GI/(box1 + 1.0);
	if (box2 > epsmod) box2_weight = GI/(box2 + 1.0);
	if (box3 > epsmod) box3_weight = GI/(box3 + 1.0);
	if (box4 > epsmod) box4_weight = GI/(box4 + 1.0);
	if (sphere1 > epsmod) sphere1_weight = GI/(sphere1 + 1.0);
	if (sphere2 > epsmod) sphere2_weight = GI/(sphere2 + 1.0);
	if (torus > epsmod) torus_weight = GI/(torus + 1.0);
	
	col =
		ground_color  * ground_weight  + 
		box1_color * box1_weight + 
		box2_color * box2_weight + 
		box3_color * box3_weight + 
		box4_color * box4_weight* 2.0 + 
		sphere1_color * sphere1_weight + 
		sphere2_color * sphere2_weight + 
		torus_color * torus_weight;
	
	return col*col*col*0.15;
}

#define epsilon 0.001
vec3 normal(vec3 p) { return normalize( vec3(scene( vec3(p.x + epsilon, p.y, p.z)) - scene( vec3(p.x - epsilon, p.y, p.z)) , scene( vec3(p.x, p.y + epsilon, p.z)) - scene( vec3(p.x, p.y - epsilon, p.z)), scene( vec3(p.x, p.y, p.z + epsilon)) - scene( vec3(p.x, p.y, p.z - epsilon)))); }


vec3 intersect(in vec3 ro, in vec3 rd) {
    float d, t = 3.0;
    for (int i = 0; i < 128; i++) {
		d = scene(t*rd + ro);
//		if (d < 0.01) return(t*rd + ro); 	// Bug: Return/Break doesn't work
		t += d;
    }
	return(t*rd + ro);
}

float ambientOcclusion(vec3 p, vec3 n){
    const int steps = 3;
    const float delta = 0.5;

    float a = 0.0;
    float weight = 0.75;
    float m;
    for(int i=1; i<=steps; i++) {
        float d = (float(i) / float(steps)) * delta; 
        a += weight*(d - scene(p + n*d));
        weight *= 0.5;
    }
    return clamp(1.0 - a, 0.0, 1.0);
}

float softshadow(in vec3 ro, in vec3 rd){
    float res = 1.0, t = 0.15; // t=0.15 -> no banding on my stock x.org drivers
    for(int s = 0; s < 16; ++s){
        float h = scene(ro + rd*t);
        if(h < 0.01) return 0.0;
        res = min( res, 2.0*h/t );
        t += h*0.9;
    }
    return res;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ){
	vec3 col = vec3(1.0);
	vec2 p = -1.0 + 2.0 * fragCoord.xy / resolution.xy;
	vec3 rd = normalize(vec3(aspect * p.x, p.y, -2.0));
	vec3 ro = vec3(1.0 + 0.5*(10.0*mouse.x/resolution.x), 0.0 + 0.5*(2.0*mouse.y/resolution.y), 5.0);
	
	vec3 pos, n;
	
	pos = intersect(ro, rd);
	n = normal(pos);
	col = scene_color(pos);
	vec3 gi = scene_color_lerp(pos);
	
	const vec3 lightPos = vec3(5.0, 10.0, 5.0);
	const float shininess = 100.0;
	vec3 l = normalize(lightPos - pos);
    vec3 v = normalize(ro - pos);
    vec3 h = normalize(v + l);
    float diff = dot(n, l);
    float spec = max(0.0, pow(dot(n, h), shininess)) * float(diff > 0.0);

    diff = 0.5+0.5*diff;
	float ao = ambientOcclusion(pos, n);
	float shadow = softshadow(pos, normalize(lightPos));


	//Final Color
	fragColor = vec4(vec3(diff*(0.5+0.5*shadow)*ao*(col+gi) + spec), 1.0);
	
	// View Fake GI
	//fragColor = vec4(gi, 1.0);
}