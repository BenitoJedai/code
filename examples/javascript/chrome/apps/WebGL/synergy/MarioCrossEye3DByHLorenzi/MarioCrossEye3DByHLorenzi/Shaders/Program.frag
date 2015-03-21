// Comment to turn off for faster rendering!
#define SHADOWS 1
#define SPECULAR 1

// Reduce for accuracy-performance trade-off!
#define RAYMARCH_ITERATIONS 40
#define SHADOW_ITERATIONS 20

// Increase for accuracy-performance trade-off!
#define RAYMARCH_DOWNSTEP 1.0
#define SHADOW_STEP 1.5




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

float fSmoothUnion(float a, float b, float k)
{
    float h = clamp( 0.5+0.5*(b-a)/k, 0.0, 1.0 );
    return mix( b, a, h ) - k*h*(1.0-h);
}

float pSphere(vec3 p, float s)
{
	return length(p)-s;
}

float pRoundBox(vec3 p, vec3 b, float r)
{
 	return length(max(abs(p)-b,0.0))-r;
}

float pBox(vec3 p, vec3 b)
{
  return length(max(abs(p)-b,0.0));
}

vec2 pBlockBricks(vec3 p)
{
	const vec3 or = -vec3(0.8,0.86+0.2,0.8+0.2);
	// Top
	float d =     pBox(p+or+vec3(0,0,0), vec3(0.7,0.43,0.43));
	d = fUnion(d, pBox(p+or+vec3(0.7+0.2+0.7,0,0), vec3(0.7,0.43,0.43)));
	
	d = fUnion(d, pBox(p+or+vec3(-0.4,0.86+0.2,0), vec3(0.3,0.43,0.43)));
	d = fUnion(d, pBox(p+or+vec3(-0.4+0.5+0.7,0.86+0.2,0), vec3(0.7,0.43,0.43)));
	d = fUnion(d, pBox(p+or+vec3(0.2+0.4+1.4,0.86+0.2,0), vec3(0.3,0.43,0.43)));
	
	d = fUnion(d, pBox(p+or+vec3(0,0.86+0.86+0.4,0), vec3(0.7,0.43,0.43)));
	d = fUnion(d, pBox(p+or+vec3(0.7+0.2+0.7,0.86+0.86+0.4,0), vec3(0.7,0.43,0.43)));
	
	// Middle
	d = fUnion(d, pBox(p+or+vec3(-0.27,0.27,0.86+0.2), vec3(0.43,0.7,0.43)));
	d = fUnion(d, pBox(p+or+vec3(-0.27,0.27+0.2+1.4,0.86+0.2), vec3(0.43,0.7,0.43)));
	
	d = fUnion(d, pBox(p+or+vec3(-0.27+0.86+0.2,0.27+0.7+0.1,0.86+0.2), vec3(0.43,1.5,0.43)));
	
	d = fUnion(d, pBox(p+or+vec3(-0.27+0.86+0.86+0.4,0.27,0.86+0.2), vec3(0.43,0.7,0.43)));
	d = fUnion(d, pBox(p+or+vec3(-0.27+0.86+0.86+0.4,0.27+0.2+1.4,0.86+0.2), vec3(0.43,0.7,0.43)));
	
	
	// Bottom
	d = fUnion(d, pBox(p+or+vec3(0,0,0.86+0.86+0.4), vec3(0.7,0.43,0.43)));
	d = fUnion(d, pBox(p+or+vec3(0.7+0.2+0.7,0,0.86+0.86+0.4), vec3(0.7,0.43,0.43)));
	
	d = fUnion(d, pBox(p+or+vec3(-0.4,0.86+0.2,0.86+0.86+0.4), vec3(0.3,0.43,0.43)));
	d = fUnion(d, pBox(p+or+vec3(-0.4+0.5+0.7,0.86+0.2,0.86+0.86+0.4), vec3(0.7,0.43,0.43)));
	d = fUnion(d, pBox(p+or+vec3(0.2+0.4+1.4,0.86+0.2,0.86+0.86+0.4), vec3(0.3,0.43,0.43)));
	
	d = fUnion(d, pBox(p+or+vec3(0,0.86+0.86+0.4,0.86+0.86+0.4), vec3(0.7,0.43,0.43)));
	d = fUnion(d, pBox(p+or+vec3(0.7+0.2+0.7,0.86+0.86+0.4,0.86+0.86+0.4), vec3(0.7,0.43,0.43)));
	
	d = fIntersection(d, pRoundBox(p, vec3(1.3,1.3,1.3), 0.2));
	
	float m = 3.0;
	fUnionMat(d,m,pBox(p,vec3(1.4,1.4,1.4)),0.0);
	
	return vec2(d,m);
}

float distf(vec3 p, inout float m)
{
	float d = 10000.0;
	m = 0.0;
	
	float grass1 = pBox(p+vec3(0,0,abs(cos(p.x)*cos(p.y))),vec3(12.2,12.2,1.2));
	grass1 = fIntersection(grass1,pRoundBox(p+vec3(0,0,1.5),vec3(11,11,0.5), 1.0));
	fUnionMat(d,m,grass1,2.0);
	
	float ground1 = pRoundBox(p+vec3(0,0,4.0),vec3(10.7,10.7,2.5),1.0);
	fUnionMat(d,m,ground1,3.0);
	
	vec2 blocks = pBlockBricks((mod(p/2.0,3.0)-0.5*3.0));
	float blockd = blocks.x;
	float blockm = blocks.y;
	
	const float blocksize = 2.95;
	float blocki = pBox(p-vec3(3,3,3),vec3(blocksize,blocksize,blocksize));
	blocki = fUnion(blocki, pBox(p-vec3(3,9,3),vec3(blocksize,blocksize,blocksize)));
	blocki = fUnion(blocki, pBox(p-vec3(9,3,3),vec3(blocksize,blocksize,blocksize)));
	blocki = fUnion(blocki, pBox(p-vec3(-9,3,9),vec3(blocksize,blocksize,blocksize)));
	blocki = fUnion(blocki, pBox(p-vec3(9,-9,15),vec3(blocksize,blocksize,blocksize)));
	blocki = fUnion(blocki, pBox(p-vec3(9,-9,9),vec3(blocksize,blocksize,blocksize)));
	blocki = fUnion(blocki, pBox(p-vec3(-9,-9,3),vec3(blocksize,blocksize,blocksize)));
	
	blockd = fIntersection(blockd, blocki);
	
	fUnionMat(d,m,blockd,blockm);
	
	
	return d;
}

float distf2(vec3 p, inout float m)
{
	float d = 10000.0;
	
	float grass1 = pBox(p+vec3(0,0,abs(cos(p.x)*cos(p.y))),vec3(12.2,12.2,1.2));
	grass1 = fIntersection(grass1,pRoundBox(p+vec3(0,0,1.5),vec3(11,11,0.5), 1.0));
	d = fUnion(d,grass1);
	
	float ground1 = pRoundBox(p+vec3(0,0,4.0),vec3(10.7,10.7,2.5),1.0);
	d = fUnion(d,ground1);
	
	vec2 blocks = pBlockBricks((mod(p/2.0,3.0)-0.5*3.0));
	float blockd = blocks.x;
	
	const float blocksize = 2.95;
	float blocki = pBox(p-vec3(3,3,3),vec3(blocksize,blocksize,blocksize));
	blocki = fUnion(blocki, pBox(p-vec3(3,9,3),vec3(blocksize,blocksize,blocksize)));
	blocki = fUnion(blocki, pBox(p-vec3(9,3,3),vec3(blocksize,blocksize,blocksize)));
	blocki = fUnion(blocki, pBox(p-vec3(-9,3,9),vec3(blocksize,blocksize,blocksize)));
	blocki = fUnion(blocki, pBox(p-vec3(9,-9,15),vec3(blocksize,blocksize,blocksize)));
	blocki = fUnion(blocki, pBox(p-vec3(9,-9,9),vec3(blocksize,blocksize,blocksize)));
	blocki = fUnion(blocki, pBox(p-vec3(-9,-9,3),vec3(blocksize,blocksize,blocksize)));
	
	blockd = fIntersection(blockd, blocki);
	
	d = fUnion(d,blockd);
	
	
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
		
		dist += distEval * RAYMARCH_DOWNSTEP;
	}
	
	
	if (dist >= maxDist) {
		material = 0.0;
	}
	
	return vec4(dist, material, 0.0, 0.0);
}

float shadow(vec3 from, vec3 increment)
{
	const float minDist = 1.0;
	
	float res = 1.0;
	float t = 1.0;
	for(int i = 0; i < SHADOW_ITERATIONS; i++) {
		float m;
        float h = distf2(from + increment * t,m);
        if(h < minDist)
            return 0.0;
		
		res = min(res, 8.0 * h / t);
        t += SHADOW_STEP;
    }
    return res;
}

vec4 getPixel(vec3 fromcenter, vec3 from, vec3 to, vec3 increment)
{
	vec4 c = raymarch(from, increment);
	
	vec3 hitPos = from + increment * c.x;
	vec3 normal = normalFunction(hitPos);
	if (c.y == 2.0) {
		normal -= texture2D(iChannel1, (hitPos.yz + hitPos.zx) / 10.0).xyz * 0.8;
		normal = normalize(normal);
	} else if (c.y == 3.0) {
		normal += texture2D(iChannel1, (hitPos.yz + hitPos.zx) / 10.0).xyz * 0.8;
		normal = normalize(normal);
	}
	
	vec3 lightPos = -normalize(hitPos - (fromcenter + vec3(0,0,-10)));
	
	float diffuse = max(0.0, dot(normal, -lightPos)) * 0.3 + 0.7;
	float shade = 
		#ifdef SHADOWS
			shadow(hitPos, -normalize(hitPos - (fromcenter + vec3(0,0,10)))) * 0.5 + 0.5;
		#else
			1.0;
		#endif
	float specular = 0.0;	
		#ifdef SPECULAR
		if (dot(normal, -lightPos) < 0.0) {
			specular = 0.0;
		} else {
			specular = pow(max(0.0,
					dot(reflect(-lightPos, normal), normalize(from - hitPos))), 50.0);
		}
		#endif
	
	
	vec4 m = vec4(0,0,0,1);
	
	if (c.y == 1.0) {
		m = vec4(1,1,1,1) *
			clamp((40.0 - length(hitPos.xy)) / 40.0, 0.0, 1.0);
	} else if (c.y == 2.0) {
		m = vec4(0.3,0.9,0.1,1) * (texture2D(iChannel0, hitPos.xy / 20.0) * 0.3 + 0.7);
	} else if (c.y == 3.0) {
		m = vec4(1,0.55,0.3,1) * (texture2D(iChannel1, (hitPos.yz + hitPos.zx) / 10.0) * 0.3 + 0.7);;
	}
	
	
	return (m * diffuse + vec4(1,1,1,1) * specular) * shade;
	
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	
	// Camera
	
	vec2 resolution = vec2(iResolution.x / 2.0, iResolution.y);
	vec2 coord;
	int eye;
	if (fragCoord.x < resolution.x) {
		eye = 0;
		coord = fragCoord.xy;
	} else {
		coord = vec2(fragCoord.x - resolution.x,fragCoord.y);
		eye = 1;
	}
	
	vec2 q = (coord/resolution.xy);
    vec2 p = -1.0+2.0*q;
	p.x *= -(iResolution.x/2.0)/iResolution.y;
    vec2 mo = iMouse.xy/iResolution.xy;

	// camera	
	float dist = 32.0;
	
	float eyedisp = (eye == 0 ? -0.15 : 0.15);
	
	vec3 ta = vec3(0,0,6);
	vec3 ro = vec3(cos((iGlobalTime + eyedisp) / 2.0) * dist,
				   sin((iGlobalTime + eyedisp) / 2.0) * dist,
				   cos((iGlobalTime) / 4.0) * 10.0 + 10.0);
	vec3 roe = vec3(cos((iGlobalTime) / 2.0) * dist,
				   sin((iGlobalTime) / 2.0) * dist,
				   cos((iGlobalTime) / 4.0) * 10.0 + 10.0);
	
	
	// camera tx
	vec3 cw = normalize( ta-ro );
	vec3 cp = vec3( 0.0, 0.0, 1.0 );
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
	vec3 rd = normalize( p.x*cu + p.y*cv + 1.5*cw );
	
	if (length(coord - vec2(resolution.x / 2.0, resolution.y - 30.0)) < 10.0) {
		fragColor = vec4(1,1,1,1);
	} else {
		fragColor = getPixel(roe, ro, ta, rd);
	}
}