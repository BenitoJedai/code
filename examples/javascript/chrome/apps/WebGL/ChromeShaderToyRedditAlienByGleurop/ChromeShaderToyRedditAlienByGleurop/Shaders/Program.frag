// Public domain
// Thanks to Inigo Quilez for the techniques and the inspiration

// Join the discussion at http://www.reddit.com/r/twotriangles

const int MAX_ITER = 50;
	
float distbox(vec3 p, vec3 b)
{
	return length(max(abs(p) - b, 0.0));
}

float distsphere(vec3 p, float r)
{
	return length(p) - r;
}

float distplane(vec3 p, vec3 n)
{
	return abs(dot(p, n));	
}

float disttube(vec2 p, float r)
{
	return length(p) - r;
}

float disttorus(vec3 p, vec2 t)
{
	vec2 q = vec2(length(p.xz) - t.x, p.y);
	return length(q) - t.y;
}

vec2 rotate(vec2 v, float a) {
	return vec2(cos(a)*v.x + sin(a)*v.y, -sin(a)*v.x + cos(a)*v.y);
}

vec2 distfunc(vec3 p)
{
	int material = 0;
	float floor = distplane(p - vec3(0, 1, 0), vec3(0, 1, 0));
	vec3 r1 = vec3(rotate(p.xy, 0.3), p.z);
	// first antenna section
	float d = max(distsphere(r1 + vec3(1.15, 3.0, 0), 1.0), disttube(r1.xz + vec2(0.8, 0), 0.1));
	// second antenna section
	d = min(d, max(distsphere(p + vec3(-0.95, 4.2, 0), 0.6), disttube(p.zy + vec2(0, 3.95), 0.1)));
	// antenna ball
	d = min(d, distsphere(p + vec3(-1.5, 3.95, 0), 0.3));
	// happy mouth
	d = min(d, max(distbox(p + vec3(0, 2.0, -0.9), vec3(0.4)), disttorus(p.yzx + vec3(3.0, -0.9, 0), vec2(1.0, 0.1))));
	// sad mouth
	d = min(d, max(distbox(p + vec3(0, 2.0, 0.9), vec3(0.4)), disttorus(p.yzx + vec3(1.0, 0.9, 0), vec2(1.0, 0.1))));
	
	// mirror so we can use less primitives
	p.x = abs(p.x);
	// feet
	d = min(d, distsphere(p + vec3(-0.9, -1, 0), 0.5));
	// body
	d = min(d, distsphere(p * vec3(1.1, 0.5, 1.1) + vec3(0, 0.2, 0), 1.0));
	// arms
	d = min(d, distsphere(p * vec3(1.8, 1.2, 1.8) + vec3(-1.3, 0.7, 0), 1.0)/1.6);
	// head
	d = min(d, distsphere(p * vec3(0.65, 1.1, 1.0) + vec3(0, 2.5, 0), 1.0));
	// ears
	d = min(d, distsphere(p + vec3(-1.3, 2.8, 0), 0.35));
	
	float eyes = distsphere(vec3(p.xy, abs(p.z)) + vec3(-0.4, 2.5, -0.9), 0.15);
	
	if (eyes < d) material = 1;
	if (floor < d) material = 2;
	d = min(d, floor);
	d = min(d, eyes);
	return vec2(d, material);
}

vec4 intersect(in vec3 rayOrigin, in vec3 rayDir)
{
	vec3 p = rayOrigin;
	vec2 d = vec2(1.0);
	
	for (int i = 0; i < MAX_ITER; i++) {
		if (d.x < 0.01)
			continue;

		d = distfunc(p);
		p += d.x * rayDir;
	}
	
	if (d.x < 0.01)
		return vec4(p, d.y);
	else
		return vec4(-1);
}

vec3 getmaterial(vec3 p, float mat)
{
	if (mat < 0.5)
		return vec3(1.3);
	else if (mat < 1.5)
		return vec3(1, 0, 0);
	else if (mat < 2.5)
		return vec3(floor(length(floor(mod(p, 2.0)+0.5))-0.5));

	return vec3(0);
}

vec3 getnormal(in vec3 p) {
	vec2 e = vec2(0.0, 0.001);
	
	return normalize(vec3(
		distfunc(p + e.yxx).x - distfunc(p - e.yxx).x,
		distfunc(p + e.xyx).x - distfunc(p - e.xyx).x,
		distfunc(p + e.xxy).x - distfunc(p - e.xxy).x));
}


vec3 getlighting(in vec3 pos, in vec3 normal, in vec3 lightDir, in vec3 color)
{
	float b = max(0.0, dot(normal, lightDir));
	return b * color;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	const vec3 upDirection = vec3(0, 1, 0);
	const vec3 cameraTarget = vec3(0, -1.5, 0);
	vec3 cameraOrigin = vec3(sin(iGlobalTime), -0.5, cos(iGlobalTime)) * 4.0;
	
	vec3 cameraDir = normalize(cameraTarget - cameraOrigin);
	vec3 u = normalize(cross(upDirection, cameraOrigin));
	vec3 v = normalize(cross(cameraDir, u));
	vec2 screenPos = -1.0 + 2.0 * fragCoord.xy / iResolution.xy;
	screenPos.x *= iResolution.x / iResolution.y;
	vec3 rayDir = normalize(u * screenPos.x + v * screenPos.y + cameraDir);
	
	vec4 i = intersect(cameraOrigin, rayDir);
	if (i.w > -0.5) {
		vec3 materialColor = getmaterial(i.xyz, i.w);
		vec3 normal = getnormal(i.xyz);
		vec3 lightDir = -rayDir;
		vec3 color = getlighting(i.xyz, normal, lightDir, materialColor);
		color *= pow(1.1, -distance(cameraOrigin, i.xyz));
	
		fragColor = vec4(color, 1.0);
	} else {
		fragColor = vec4(0);	
	}
}