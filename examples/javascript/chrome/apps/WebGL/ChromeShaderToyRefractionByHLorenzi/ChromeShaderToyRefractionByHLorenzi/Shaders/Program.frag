#define REFRACTION_INDEX 0.75






float rand(vec3 co)
{
    return -1.0 + fract(sin(dot(co.xy,vec2(12.9898 + co.z,78.233))) * 43758.5453) * 2.0;
}

float linearRand(vec3 uv)
{
	vec3 iuv = floor(uv);
	vec3 fuv = fract(uv);
	
	float v1 = rand(iuv + vec3(0,0,0));
	float v2 = rand(iuv + vec3(1,0,0));
	float v3 = rand(iuv + vec3(0,1,0));
	float v4 = rand(iuv + vec3(1,1,0));
	
	float d1 = rand(iuv + vec3(0,0,1));
	float d2 = rand(iuv + vec3(1,0,1));
	float d3 = rand(iuv + vec3(0,1,1));
	float d4 = rand(iuv + vec3(1,1,1));
	
	return mix(mix(mix(v1,v2,fuv.x),mix(v3,v4,fuv.x),fuv.y),
		       mix(mix(d1,d2,fuv.x),mix(d3,d4,fuv.x),fuv.y),
			   fuv.z);
}

float textureNoise(vec3 uv)
{
	float c = (linearRand(uv * 1.0) * 32.0 +
			   linearRand(uv * 2.0) * 16.0 + 
			   linearRand(uv * 4.0) * 8.0 + 
			   linearRand(uv * 8.0) * 4.0) / 32.0;
	return c * 0.5 + 0.5;
}

// Comment to turn off for faster rendering!
#define SHADOWS 1
#define GLOW 1
#define SPECULAR 1

// Reduce for accuracy-performance trade-off!
#define RAYMARCH_ITERATIONS 40
#define SHADOW_ITERATIONS 60

// Increase for accuracy-performance trade-off!
#define SHADOW_STEP 2.0




void fUnionMat(inout float curDist, inout float curMat, float dist, in float mat)
{
	if (dist < curDist) {
		curMat = mat;
		curDist = dist;
	}
}

float fSubtraction(float a, float b)
{
	return max(-a,b);
}

float fIntersection(float d1, float d2)
{
    return max(d1,d2);
}

float fUnion(float d1, float d2)
{
    return min(d1,d2);
}

float pSphere(vec3 p, float s)
{
	return length(p)-s;
}

float pRoundBox(vec3 p, vec3 b, float r)
{
 	return length(max(abs(p)-b,0.0))-r;
}

float pTorus(vec3 p, vec2 t)
{
  vec2 q = vec2(length(p.xz)-t.x,p.y);
  return length(q)-t.y;
}

bool water = false;
float distf(vec3 p, inout float m)
{
	float d = 100000.0;
	m = 0.0;
	
	fUnionMat(d, m, pRoundBox(vec3(0,0,10) + p, vec3(11,11,1), 1.0), 1.0);
	fUnionMat(d, m, pRoundBox(vec3(0,0,-2) + p, vec3(5,5,12), 1.0), 1.0);
	
	if (water)
		fUnionMat(d, m, pRoundBox(
			vec3(cos(p.y + iGlobalTime) * sin(p.z + iGlobalTime) * 0.1,
				 cos(p.x + iGlobalTime) * sin(p.z + iGlobalTime) * 0.1,
				 cos(p.x + iGlobalTime) * sin(p.y + iGlobalTime * 2.0) * 0.35) + p, vec3(10,10,10), 1.0), 2.0);
	
	return d;
}


vec3 normalFunction(vec3 p)
{
	const float eps = 0.01;
	float m;
    vec3 n = vec3( (distf(vec3(p.x-eps,p.y,p.z),m) - distf(vec3(p.x+eps,p.y,p.z),m)),
                   (distf(vec3(p.x,p.y-eps,p.z),m) - distf(vec3(p.x,p.y+eps,p.z),m)),
                   (distf(vec3(p.x,p.y,p.z-eps),m) - distf(vec3(p.x,p.y,p.z+eps),m))
				 );
    return normalize( n );
}

vec4 raymarch(vec3 from, vec3 increment)
{
	const float maxDist = 200.0;
	const float minDist = 0.1;
	const int maxIter = RAYMARCH_ITERATIONS;
	
	float dist = 0.0;
	
	float material = 0.0;
	
	for(int i = 0; i < maxIter; i++) {
		vec3 pos = (from + increment * dist);
		float distEval = distf(pos, material);
		
		if (distEval < minDist) {
			break;
		}
		
		dist += distEval;
	}
	
	
	if (dist >= maxDist) {
		material = 0.0;
	}
	
	return vec4(dist, material, 0, 0);
}

float shadow(vec3 from, vec3 increment)
{
	const float minDist = 1.0;
	
	float res = 1.0;
	float t = 1.0;
	for(int i = 0; i < SHADOW_ITERATIONS; i++) {
		float m;
        float h = distf(from + increment * t,m);
        if(h < minDist)
            return 0.0;
		
		res = min(res, 4.0 * h / t);
        t += SHADOW_STEP;
    }
    return res;
}

vec4 getPixel(out float mat, out vec3 hitPos, out vec3 normal, vec3 from, vec3 increment)
{
	vec4 c = raymarch(from, increment);
	
	hitPos = from + increment * c.x;
	normal = normalFunction(hitPos);
	vec3 lightPos = normalize(vec3(-1,-1,1));
	
	float diffuse = max(0.0, dot(normal, -lightPos)) * 0.5 + 0.5;
	float shade = 1.0;
		/*#ifdef SHADOWS
			shadow(hitPos, lightPos) * 0.3 + 0.7;
		#else
			1.0;
		#endif*/
	float specular = 0.0;	
		#ifdef SPECULAR
		if (dot(normal, -lightPos) < 0.0) {
			specular = 0.0;
		} else {
			specular = pow(max(0.0, dot(reflect(-lightPos, normal), normalize(from - hitPos))), 5.0);
		}
		#endif
	
	
	vec4 m = vec4(0,0,0,1);
	
	if (c.y == 0.0) {
		m = textureCube(iChannel1, hitPos.xzy);
		diffuse = 1.0;
	} else if (c.y == 1.0) {
		hitPos += textureNoise(hitPos) * 2.0;
		float g = cos(hitPos.x + hitPos.y) * 0.5 + 1.0;
		m = mix(vec4(g * 0.6,g * 0.3,g * 0.1,1),
				vec4(g * 0.2,g * 0.8,g * 0.2,1),
				clamp(dot(normal,vec3(0,0,-1)),0.0,1.0));
	} else if (c.y == 2.0) {
		m = vec4(0.5,0.9,1.0,0.4);
	}
	
	mat = c.y;
	
	return (vec4(m.rgb * diffuse,m.a) + vec4(1,1,1,1) * specular) * shade;
	
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	
	// Camera
	
	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0+2.0*q;
	p.x *= -iResolution.x/iResolution.y;
    vec2 mo = iMouse.xy/iResolution.xy;
	
	vec2 m = iMouse.xy / iResolution.xy;
	if (iMouse.x == 0.0 && iMouse.y == 0.0) {
		m = vec2(iGlobalTime * 0.06 + 0.14,0.3);	
	}
	m = -1.0 + 2.0 * m;
	m *= vec2(4.0,-1.5);

	// camera	
	float dist = 50.0;
	
	vec3 ta = vec3(0,0,0);
	vec3 ro = vec3(cos(m.x) * cos(m.y) * dist, sin(m.x) * cos(m.y) * dist, sin(m.y) * dist);
	
	// camera tx
	vec3 cw = normalize( ta-ro );
	vec3 cp = vec3( 0.0, 0.0, 1.0 );
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
	vec3 rd = normalize( p.x*cu + p.y*cv + 2.5*cw );
	
	float mat;
	vec3 hitPos, normal;
	
	water = true;
	vec4 col = getPixel(mat, hitPos, normal, ro, rd);
	
	
	if (mat == 2.0 && col.a < 1.0) {
		vec3 lastHit = hitPos;
		vec3 nrd = normalize(refract(rd, -normal, REFRACTION_INDEX));
		if (nrd.x == 0.0 && nrd.y == 0.0 && nrd.z == 0.0) {
			nrd = normalize(reflect(rd, normal));
		}
		water = false;
		col = mix(getPixel(mat, hitPos, normal, lastHit + nrd * 0.2, nrd),col,col.a);
	}
	fragColor = col;
	
}