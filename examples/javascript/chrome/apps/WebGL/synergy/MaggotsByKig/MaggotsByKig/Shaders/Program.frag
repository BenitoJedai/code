float sphereDet(vec3 ray, vec3 dir, vec3 center, float radius, inout float b)
{
	vec3 rc = ray-center;  // 1
	float c = dot(rc, rc); // 1
	c -= radius*radius;    // 2?
	b = dot(dir, rc);      // 1
	return b*b - c;        // 2?
}

float sphere(vec3 ray, vec3 dir, vec3 center, float radius, float closestHit, inout vec3 nml)
{
	float b;
	float d = sphereDet(ray, dir, center, radius, b); // 7
	if (d < 0.0) { // 1
		return closestHit;
	}
	float t = -b - sqrt(d); // 3
	if (t > 0.0 && t < closestHit) { // 2
        nml = center - (ray+dir*t);
	} else {
		t = closestHit;
	}
    return t;
}

float square(float x) { return x*x; }

void bg( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = vec2(iResolution.x/iResolution.y,1.0) * (-1.0 + 2.0*fragCoord.xy / iResolution.xy);
   
    vec2 ouv = uv;
    
    float a = sin(iGlobalTime*0.5)*0.2;
    float ca = cos(a);
    float sa = sin(a);
    
    uv *= mat2(ca, sa, -sa, ca);

    float df = abs(uv.y*uv.y-1.3)*uv.x*uv.x;
    uv *= 3.0+1.9*df;
    uv.x += iGlobalTime*2.0;
    uv.y += iGlobalTime*2.0;
    
    uv *= 2.0;

    uv.x = pow(sin(uv.y+iGlobalTime*2.0) * cos(uv.y*0.5) * cos(uv.x*0.5+2.0), 4.0);
    uv.y = abs(cos(uv.x*12.0) * sin(uv.y*0.25+3.14159*0.5));

    uv = pow(uv, vec2(1.0, 4.0));
    
	fragColor = vec4(uv.x+uv.x+0.25, 0.05+uv.y+uv.x, 0.15+uv.y, 1.0);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    bg(fragColor, fragCoord);
	vec2 uv = -1.0 + 2.0*fragCoord.xy / iResolution.xy;
    uv.x *= iResolution.x/iResolution.y;
    vec3 ro = vec3(0.0, 0.0, -3.0);
    vec3 rd = normalize(vec3(uv, 1.0));
    float t0 = iGlobalTime*1.2;
    float t1 = iGlobalTime*2.32;
    float closest = 1e9;
    for (int i=0; i<20; i++) {
        vec3 p = vec3(sin(float(i)+t0), cos(float(i)+t1), sin(t0)+sin(t1+float(i)));
		vec3 rc = ro-p;
        float c = dot(rc, rc);
        c -= square(0.1+abs(sin(float(i)))*0.5);
        float b = dot(rd, rc);
        float d = b*b - c;
        float t = -b - sqrt(d);
        if (d >= 0.0 && t > 0.0 && t < closest) {
            vec3 nml = normalize(p-(ro+rd*t));
            vec3 nrd = reflect(rd, nml);
            bg(fragColor, (nrd.xy/nrd.z)*10.0);
            fragColor.rgb = fragColor.rgb*0.15 + nml;
            closest = t;
        }
    }
}