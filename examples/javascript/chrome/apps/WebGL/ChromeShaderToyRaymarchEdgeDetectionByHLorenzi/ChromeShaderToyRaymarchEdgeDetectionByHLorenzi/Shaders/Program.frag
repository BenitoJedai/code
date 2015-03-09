// Raymarch Edge Detection by HLorenzi!
// Detects whether a ray that comes too close to a surface goes away.

#define EDGE_WIDTH 0.5
#define RAYMARCH_ITERATIONS 40

// The traditional method to finding raymarching edges
// Of course, mine has a different purpose (to outline in cartoonish style)
//#define TRADITIONAL




// This should prevent lines from getting thicker when close to the camera
// But it didn't work. Ugh.
//#define PERSPECTIVE_FIX





// Distance functions by www.iquilezles.org
float fSubtraction(float a, float b) {return max(-a,b);}
float fIntersection(float d1, float d2) {return max(d1,d2);}
void fUnion(inout float d1, float d2) {d1 = min(d1,d2);}
float pSphere(vec3 p, float s) {return length(p)-s;}
float pRoundBox(vec3 p, vec3 b, float r) {return length(max(abs(p)-b,0.0))-r;}
float pTorus(vec3 p, vec2 t) {vec2 q = vec2(length(p.xz)-t.x,p.y); return length(q)-t.y;}
float pCapsule(vec3 p, vec3 a, vec3 b, float r) {vec3 pa = p - a, ba = b - a;
	float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 ); return length( pa - ba*h ) - r;}

float distf(vec3 p)
{
	float d = 100000.0;
	
	fUnion(d, pRoundBox(vec3(0,0,10) + p, vec3(11,11,1), 1.0));
	fUnion(d, pRoundBox(vec3(0,0,-2) + p, vec3(5,5,12), 1.0));
	fUnion(d, pSphere(vec3(10,10,0) + p, 8.0));
	fUnion(d, pTorus(vec3(-10,-12,0) + p, vec2(9,5)));
	fUnion(d, pCapsule(p, vec3(-15,15,10), vec3(15,-15,-5), 1.5));
	fUnion(d, -pSphere(p, 80.0));
	
	return d;
}


vec4 raymarch(vec3 from, vec3 increment)
{
	const float maxDist = 200.0;
	const float minDist = 0.001;
	const int maxIter = RAYMARCH_ITERATIONS;
	
	float dist = 0.0;
	
	float lastDistEval = 1e10;
#ifdef TRADITIONAL
	float edge = 1.0;
#else
	float edge = 0.0;
#endif
	
	for(int i = 0; i < maxIter; i++) {
		vec3 pos = (from + increment * dist);
		float distEval = distf(pos);
		
#ifdef TRADITIONAL
		if (distEval < minDist) {
			if (i > RAYMARCH_ITERATIONS - 5) edge = 0.0;
			// Probably should put a break here, but it's not working with GL ES...
		}
#else
	#ifdef PERSPECTIVE_FIX
		// Could not figure out the math :P
		if (lastDistEval < (EDGE_WIDTH / dist) * 20.0 && distEval > lastDistEval + 0.001) {
			edge = 1.0;
		}
	#else
		if (lastDistEval < EDGE_WIDTH && distEval > lastDistEval + 0.001) {
			edge = 1.0;
			// Also should put a break here, but it's not working with GL ES...
		}
	#endif
		if (distEval < minDist) {
			break;
		}
#endif
		
		dist += distEval;
		if (distEval < lastDistEval) lastDistEval = distEval;
	}
	
	return vec4(dist, 0.0, edge, 0);
}

vec4 getPixel(vec3 from, vec3 increment)
{
	vec4 c = raymarch(from, increment);
	return mix(vec4(1,1,1,1),vec4(0,0,0,1),c.z);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{	
	// pixel position	
	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0+2.0*q;
	p.x *= -iResolution.x/iResolution.y;
	
	// mouse
    vec2 mo = iMouse.xy/iResolution.xy;
	vec2 m = iMouse.xy / iResolution.xy;
	if (iMouse.x == 0.0 && iMouse.y == 0.0) {
		m = vec2(iGlobalTime * 0.06 + 2.86, 0.38);	
	}
	m = -1.0 + 2.0 * m;
	m *= vec2(4.0,-1.5);

	// camera position
	float dist = 50.0;
	vec3 ta = vec3(0,0,0);
	vec3 ro = vec3(cos(m.x) * cos(m.y) * dist, sin(m.x) * cos(m.y) * dist, sin(m.y) * dist);
	
	// camera direction
	vec3 cw = normalize( ta-ro );
	vec3 cp = vec3( 0.0, 0.0, 1.0 );
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
	vec3 rd = normalize( p.x*cu + p.y*cv + 2.5*cw );

	// calculate color
	vec4 col = getPixel(ro, rd);
	fragColor = col;
	
}