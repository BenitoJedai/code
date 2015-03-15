// Tron light cycle - work in progress!
// @simesgreen
// 
// based on Carl Hoff's POVray model:
// http://www.wwwmwww.com/Matt/cyclev4z.pov
// http://www.tron-sector.com/forums/default.aspx?a=top&id=336281&pg=4

const float eps = 0.01;

float t = iGlobalTime*0.8;

// CSG operations
float _union(float a, float b)
{
    return min(a, b);
}

float _union(float a, float b, inout float m, float nm)
{
    m = (b < a) ? nm : m;
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

// primitive functions
// these all return the distance to the surface from a given point

float sdPlane( vec3 p, vec4 n )
{
  // n must be normalized
  return dot(p,n.xyz) + n.w;
}

float plane(vec3 p, vec3 n, vec3 pointOnPlane)
{	
  return dot(p, n) - dot(pointOnPlane, n);
}

// plane in z defined by 2d edge
float edge(vec3 p, vec2 a, vec2 b)
{
   vec2 e = b - a;
   vec3 n = normalize(vec3(e.y, -e.x, 0.0));
   return plane(p, n, vec3(a, 0.0));
   //return intersect( plane(p, n, vec3(a, 0.0)), plane(p, -n, vec3(a, 0.0))-0.1);
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

float box(vec3 p, vec3 bmin, vec3 bmax)
{
   vec3 c = (bmin + bmax)*0.5;
   vec3 size = (bmax - bmin)*0.5;
   return sdBox(p - c, size);
}

// http://www.povray.org/documentation/view/3.6.0/278/
float cylinder(vec3 p, vec3 a, vec3 b, float r)
{
    vec3 ab = b - a;
    // project c onto ab, computing parameterized position d(t) = a + t(b-a)
    float t = dot(p - a, ab) / dot(ab, ab);
    vec3 c = a + t*ab;
		
    float d = length(c - p) - r;
    
    vec3 n = normalize(ab);
    d = intersect(d, plane(p, n, b));
    d = intersect(d, plane(p, -n, a));
    return d;
}

float sdTorus( vec3 p, vec2 t )
{
  vec2 q = vec2(length(p.xz)-t.x,p.y);
  return length(q)-t.y;
}

float torus(vec3 p, float r, float r2)
{
  return sdTorus(p, vec2(r, r2));	
}

// http://www.povray.org/documentation/view/3.6.1/277/
float cone(vec3 p, vec3 a, float baseR, vec3 b, float capR)
{
    vec3 ab = b - a;
    // project c onto ab, computing parameterized position d(t) = a + t(b-a)
    float t = dot(p - a, ab) / dot(ab, ab);
    //t = clamp(t, 0.0, 1.0);
    vec3 c = a + t*ab;	// point on axis

    float r = mix(baseR, capR, t);
    float d = length(c - p) - r;
    d *= 0.25;
    vec3 n = normalize(ab);
    d = intersect(d, plane(p, n, b));
    d = intersect(d, plane(p, -n, a));
    return d;
}


// Light cycle model

float front_tire(vec3 p)
{
	float d;
	d = sphere(p - vec3(355,85,0), 85.0);
	if (t > 1.0) {
		d = difference(d, sphere(p - vec3(355,85,69.4092), 56.6367));
		d = difference(d, sphere(p - vec3(355,85,-69.4092), 56.6367));
	}
	return d;	
/*    return difference( difference(
            sphere(p - vec3(355,85,0), 85.0),
            sphere(p - vec3(355,85,69.4092), 56.6367) ),
            sphere(p - vec3(355,85,-69.4092), 56.6367) );
*/
}

float front_axle(vec3 p)
{
    return _union(
        sphere(p - vec3(355,85,-26), 17.0),
        sphere(p - vec3(355,85,26), 17.0));
}

float front_hub(vec3 p)
{
    return difference( 
        box(p, vec3(304.803,34.803,-43.1795), vec3(405.197,135.197,43.1795)),
        _union( sphere(p - vec3(355,85,69.4092), 56.636),
                sphere(p - vec3(355,85,-69.4092), 56.636)));
}

float rear_tire(vec3 p)
{
	float d = 1e10;
	if (t > 4.0) d = sphere((p - vec3(0,85,0)) * vec3(1,1,5), 85.0) * 0.2;
	if (t > 5.0) {
		d = difference(d, sphere(p - vec3(0,85,20.8732), 58.7155));
		d = difference(d, sphere(p - vec3(0,85,-20.8732), 58.7155));
	}
	return d;
	/*
    return difference( difference(
        sphere((p - vec3(0,85,0)) * vec3(1,1,5), 85.0) * 0.2,
        sphere(p - vec3(0,85,20.8732), 58.7155) ),
        sphere(p - vec3(0,85,-20.8732), 58.7155) );
	*/
}

float rear_axle(vec3 p)
{
    return sphere(p - vec3(0,85,0), 17.0);
}

float rear_hub(vec3 p)
{
    //return box(p, vec3(-55.251,29.749,-1), vec3(55.251,140.251,1));
    return cylinder(p, vec3(0,85,-1.0), vec3(0,85,1), 60.0);
}
	       
float upper_body(vec3 p)
{
    float d = 1e10;
    d = cylinder(p, vec3(192.447,-160,17.5), vec3(192.447,-160.0,-17.5), 389.721);
    d = _union(d, cone(p, vec3(192.447,-160,-17.5), 389.721, vec3(192.447,-160,-22.5), 373.721) );
    d = _union(d, cone(p, vec3(192.447,-160,17.5), 389.721, vec3(192.447,-160,22.5), 373.721) );

	if (t > 9.0) {
    d = intersect(d, edge(p, vec2(434.548,145.401), vec2(434.548,229.721) ));
    d = intersect(d, edge(p, vec2(35.372,145.401), vec2(434.548,145.401) ));
    //d = intersect(d, edge(p, vec2(35.372,60), vec2(434.548,20) ));
	
    //d = intersect(d, edge(p, vec2(6.02735,162.344), vec2(35.372,145.401) ));
    d = intersect(d, edge(p, vec2(6.02735,229.721), vec2(6.02735,162.344) ));	
	
    //d = torus(p.xzy - vec3(192.447,-160, 26.5).xzy, 367.221, 11.5);
    //d = torus(p.xzy - vec3(192.447,-160, -26.5).xzy, 367.221, 11.5);
	}
    return d;
}

float lower_body(vec3 p)
{
    float d;
    d = box(p, vec3(0,38.5,-22.5), vec3(278.689,145.401,22.5));
    //d = difference(d, cylinder(p, vec3(192.447,-160,26.5), vec3(192.447,-160,-26.5), 373.721));
    d = difference(d, cylinder(p, vec3(0,85,26.501), vec3(0,85,-26.501), 28.5));	// axle hole	
    return d;
}

float window(vec3 p)
{
    float d = 1e10;
    d = sphere((p - vec3(238.0,145.4,0.0))/vec3(1.83,0.75,1.0), 77.5)*0.5;
		
	if (t > 13.0) {
    d = _union(d, cylinder(p, vec3(192.447,-160,17.5), vec3(192.447,-160.0,-17.5), 389.721+0.01));
    d = _union(d, cone(p, vec3(192.447,-160,-17.5), 389.721+0.01, vec3(192.447,-160,-22.5), 373.721+0.01) );
    d = _union(d, cone(p, vec3(192.447,-160,17.5), 389.721+0.01, vec3(192.447,-160,22.5), 373.721+0.01) );
				
    d = intersect(d, edge(p, vec2(192.447,229.721), vec2(238,145.4)));
    d = intersect(d, edge(p, vec2(335.203,145.4), vec2(381.405,180.848)));
    //d = intersect(d, edge(p, vec2(381.405,229.721), vec2(192.447,229.721)));
    d = intersect(d, edge(p, vec2(192.447,145.4), vec2(381.405,145.4) ));	
//	d = edge(p, vec2(381.405,145.4), vec2(192.447,145.4));
	}
    return d;	
}

float jetwall(vec3 p)
{
	float d;
	d = box(p, vec3(-5000.0,0,-1), vec3(0.0,230.0,1));
    d = intersect(d, cylinder(p, vec3(-200,-160,1), vec3(-200,-160.0,-1), 389.721));
	d = _union(d, box(p, vec3(-5000.0,0,-1), vec3(-200.0,230.0,1)));
	return d;
}

// distance to scene
float scene(vec3 p, out float m)
{
#if 0
    // duplicate
    p += vec3(-4.0, 0.0, -4.0);
    p.xz = mod(p.xz, 8.0);
    p -= vec3(4.0, 0.0, 4.0);
#endif	
		
    float d;
    m = 3.0;	// material

    p += vec3(2.0, 1.0, 0.0);
    p = p * 100.0;

    d = sdPlane(p, vec4(0, 1, 0, 0)); 

    d = _union(d, front_tire(p), m, 0.0);
    if (t > 2.0) d = _union(d, front_axle(p), m, 2.0);
    if (t > 3.0) d = _union(d, front_hub(p), m, 1.0);
	
    d = _union(d, rear_tire(p),m, 0.0);
    if (t > 6.0) d = _union(d, rear_axle(p), m, 2.0);
    if (t > 7.0) d = _union(d, rear_hub(p), m, 1.0);
	
    if (t > 8.0) d = _union(d, upper_body(p), m, 0.0);
    if (t > 10.0) d = _union(d, lower_body(p), m, 0.0);	
	
    if (t > 11.0) d = _union(d, cone(p, vec3(293.0,85.0,0.0), 60.0, vec3(219.0,85.0,0.0), 26.5), m, 2.0);
	
    if (t > 12.0) d = _union(d, window(p), m, 1.0);

	if (t > 14.0) d = _union(d, jetwall(p), m, 4.0);
	
	
    d /= 100.0;

    return d;
}

// calculate scene normal
vec3 sceneNormal(in vec3 pos )
{
    float eps = 0.0001;
    vec3 n;
    float m;
    float d = scene(pos, m);
    n.x = scene( vec3(pos.x+eps, pos.y, pos.z), m ) - d;
    n.y = scene( vec3(pos.x, pos.y+eps, pos.z), m ) - d;
    n.z = scene( vec3(pos.x, pos.y, pos.z+eps), m ) - d;
    return normalize(n);
}

// edge detection from Kali's:
// https://www.shadertoy.com/view/4djGz1

vec3 sceneNormalEdge(vec3 p, out float edge)
{ 
	const float det = 0.02;
	vec3 e = vec3(0.0,det,0.0);

	float m;
	float d1=scene(p-e.yxx, m),d2=scene(p+e.yxx, m);
	float d3=scene(p-e.xyx, m),d4=scene(p+e.xyx, m);
	float d5=scene(p-e.xxy, m),d6=scene(p+e.xxy, m);
	float d=scene(p, m);
	
	edge=abs(d-0.5*(d2+d1))+abs(d-0.5*(d4+d3))+abs(d-0.5*(d6+d5));//edge finder
	//edge=min(1.0, pow(edge, 0.5)*20.0);
	edge *= 100.0;
	edge = clamp(0.0, 1.0, edge);
	return -normalize(vec3(d1-d2,d3-d4,d5-d6));
}


// ambient occlusion approximation
float ambientOcclusion(vec3 p, vec3 n)
{
    const int steps = 3;
    const float delta = 0.5;

    float a = 0.0;
    float weight = 1.0;
    float m;
    for(int i=1; i<=steps; i++) {
        float d = (float(i) / float(steps)) * delta; 
        a += weight*(d - scene(p + n*d, m));
        weight *= 0.5;
    }
    return clamp(1.0 - a, 0.0, 1.0);
}

// trace ray using sphere tracing
vec3 trace(vec3 ro, vec3 rd, out bool hit, out float m)
{
    const int maxSteps = 64;
    const float hitThreshold = 0.001;
    hit = false;
    vec3 pos = ro;
    vec3 hitPos = ro;

    for(int i=0; i<maxSteps; i++)
    {
		if (!hit) {
			float d = scene(pos, m);
			if (d < hitThreshold) {
				hit = true;
				hitPos = pos;
				//return pos;
			}
			pos += d*rd;
		}
    }
    return hitPos;
}

float smoothpulse(float a, float b, float w, float t)
{
	return smoothstep(a, a + w, t) - smoothstep(b - w, b, t);
}

// lighting
vec3 shade(vec3 pos, vec3 n, vec3 eyePos, float m, float edge)
{
    //const vec3 lightPos = vec3(5.0, 10.0, 5.0);
    //vec3 color = vec3(0.1, 0.3, 1.0);	// blue
	vec3 color = vec3(1.0, 0.7, 0.1);	// yellow
	const float ka = 0.2;
	vec3 specColor = vec3(0.75);
    const float shininess = 20.0;

    if (m==1.0) {
		color = vec3(0.05);		
    } else if (m==2.0) {
		color = vec3(1.0);
	} else if (m==3.0) {
		// floor
	    color = vec3(0.08, 0.12, 0.23);
		specColor = vec3(0.0);
		vec2 uv = pos.xz * 0.3;
		if (t > 14.0) uv.x += (t - 14.0)*10.0;
		vec2 g = fract(uv);
		//vec2 width = fwidth(uv); float w = max(width.x, width.y);
		const float w = 0.02;
		//color = ((g.x < 0.04) || (g.y < 0.04)) ? vec3(1.0): color;
		float l = smoothpulse(0.0, 0.05, w, g.x) + smoothpulse(0.0, 0.05, w, g.y);
		color = mix(color, vec3(1.0), min(l, 1.0));
		//color = vec3(w);
	} else if (m==4.0) {
		// jet wall
		float x = -(pos.x+2.0)*0.3;
		x = floor(x*15.0)/15.0;
		color = mix(vec3(1.2), color, clamp(x, 0.0, 1.0));
	}
	
    //vec3 l = normalize(lightPos - pos);
	const vec3 l = vec3(0.577, 0.577, -0.577);
	//const vec3 l = vec3(0, 1, 0);
    vec3 v = normalize(eyePos - pos);
    vec3 h = normalize(v + l);
    float diff = dot(n, l);
    float spec = pow(max(0.0, dot(n, h)), shininess) * float(diff > 0.0);
    diff = max(0.0, diff);
    //diff = 0.5+0.5*diff;

	bool shadowHit;
	pos += n*eps;
	vec3 shadowPos = trace(pos, l, shadowHit, m);
	diff *= shadowHit ? 0.25 : 1.0;
	spec *= shadowHit ? 0.0 : 1.0;

	// add edges:
	//color = mix(color, vec3(1.0), edge);
	
    //float fresnel = pow(1.0 - dot(n, v), 5.0);
    //float ao = ambientOcclusion(pos, n);

    return (ka + diff)*color + spec*specColor;
//    return vec3(diff*ao) * color + vec3(spec);
//    return vec3(diff*ao) * color + vec3(spec);
//    return vec3(ao);
//    return vec3(fresnel);
//    return n*0.5+0.5;
//	return vec3(edge);
}

vec3 background(vec3 rd)
{
     //return mix(vec3(1.0), vec3(0.0), rd.y);
     //return mix(vec3(1.0), vec3(0.0, 0.25, 1.0), rd.y);
     //return vec3(0.5);
	return vec3(0.0);
}

float rand( vec2 n )
{
	return fract(sin(dot(n, vec2(12.9898, 78.233)) + iGlobalTime*0.2)* 43758.5453);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 pixel = -1.0 + 2.0 * fragCoord.xy / iResolution.xy;

    // compute ray origin and direction
    float asp = iResolution.x / iResolution.y;
    vec3 rd = normalize(vec3(asp*pixel.x, pixel.y, -2.0));
    vec3 ro = vec3(0.0, 0.0, 5.0);

	vec2 mouse = iMouse.xy / iResolution.xy;
	float roty;
	float rotx;
	if (iMouse.z <= 0.0) {
		rotx = -0.6;
		roty = iGlobalTime;
	} else {
	    rotx = -(1.0-mouse.y)*3.0;
		roty = -(mouse.x-0.5)*6.0;
	}

    rd = rotateX(rd, rotx);
    ro = rotateX(ro, rotx);
		
    rd = rotateY(rd, roty);
    ro = rotateY(ro, roty);
		
    // trace ray
    bool hit;
    float m = 0.0;
    vec3 pos = trace(ro, rd, hit, m);

    vec3 rgb;
    if(hit)
    {
        // calc normal
        //vec3 n = sceneNormal(pos);
		float edge;
		vec3 n = sceneNormalEdge(pos, edge);
        // shade
        rgb = shade(pos, n, ro, m, edge);

#if 0
        // reflection
        vec3 v = normalize(ro - pos);
        float fresnel = 0.1 + 0.4*pow(max(0.0, 1.0 - dot(n, v)), 5.0);

        ro = pos + n*eps; // offset to avoid self-intersection
        rd = reflect(-v, n);
        pos = trace(ro, rd, hit, m);

        if (hit) {
            vec3 n = sceneNormal(pos);
            rgb += shade(pos, n, ro, m) * vec3(fresnel);
        } else {
            rgb += background(rd) * vec3(fresnel);
        }
#endif 

     } else {
        rgb = background(rd);
     }

    // vignetting
    //rgb *= 0.5+0.5*smoothstep(2.0, 0.5, dot(pixel, pixel));

	// film grain
	//float noise = rand(pixel);
	//noise = exp(-noise*noise);
	//noise = noise*0.2 + 0.8;
	//rgb *= noise;
	
    fragColor=vec4(rgb, 1.0);
	//fragColor = vec4(vec3(noise), 1);
}