// srtuss, 2013
// this pretty much was my first raymarching experience.
// big thanks to iq for his awesome articles!

float slices = cos(iGlobalTime * 0.8) * 0.3 + 0.4;

vec2 rotate(vec2 k,float t)
{
	return vec2(cos(t) * k.x - sin(t) * k.y, sin(t) * k.x + cos(t) * k.y);
}

float hexagon(vec3 p, vec2 h)
{
    vec3 q = abs(p);
    return max(q.z - h.y, max(q.x + q.y * 0.57735, q.y * 1.1547) - h.x);
}

float scene(vec3 pi)
{
	// twist the wires
	float a = sin(pi.z * 0.3) * 2.0;
	vec3 pr = vec3(rotate(pi.xy, a), pi.z);
	
	// move individual wires
	vec3 id = floor(pr);
	pr.z += sin(id.x * 10.0 + id.y * 20.0) * iGlobalTime * 2.0;
	
	// calculate distance
	vec3 p = fract(pr);
	p -= 0.5;
	
	// this makes hollow wires
	//return max(hexagon(p, vec2(0.3, slices)), -hexagon(p, vec2(0.2, slices + 0.05)));
	
	return hexagon(p, vec2(0.3, slices));
}

vec3 norm(vec3 p)
{
	// the normal is simply the gradient of the volume
	vec4 dim = vec4(1, 1, 1, 0) * 0.01;
	vec3 n;
	n.x = scene(p - dim.xww) - scene(p + dim.xww);
	n.y = scene(p - dim.wyw) - scene(p + dim.wyw);
	n.z = scene(p - dim.wwz) - scene(p + dim.wwz);
	return normalize(n);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 pos = fragCoord.xy / iResolution.xy;
	vec2 p = -1.0 + 2.0 * pos;
	vec3 dir = normalize(vec3(p * vec2(1.77, 1.0), 1.0));
	
	// camera
	dir.zx = rotate(dir.zx, sin(iGlobalTime * 0.5) * 0.4);
	dir.xy = rotate(dir.xy, iGlobalTime * 0.2);
	vec3 ray = vec3(0.0, 0.0, 0.0 - iGlobalTime * 0.9);
	
	// raymarching
	float t = 0.0;
	for(int i = 0; i < 90; i ++)
	{
		float k = scene(ray + dir * t);
		t += k * 0.75;
	}
	vec3 hit = ray + dir * t;
	
	// fog
	float fogFact = clamp(exp(-distance(ray, hit) * 0.3), 0.0, 1.0);
	
	if(fogFact < 0.05)
	{
		fragColor = vec4(0.0, 0.0, 0.0, 1.0);
		return;
	}
	
	// diffuse & specular light
	vec3 sun = normalize(vec3(0.1, 1.0, 0.2));
	vec3 n = norm(hit);
	vec3 ref = reflect(normalize(hit - ray), n);
	float diff = dot(n, sun);
	float spec = pow(max(dot(ref, sun), 0.0), 32.0);
	vec3 col = mix(vec3(0.0, 0.7, 0.9), vec3(0.0, 0.1, 0.2), diff);
	
	// enviroment map
	//col += textureCube(iChannel0, ref).xyz * 0.2;
	col = fogFact * (col + spec);
	
	// iq's vignetting
	col *= 0.1 + 0.8 * pow(16.0 * pos.x * pos.y * (1.0 - pos.x) * (1.0 - pos.y), 0.1);
	
    fragColor = vec4(col, 1.0);
}