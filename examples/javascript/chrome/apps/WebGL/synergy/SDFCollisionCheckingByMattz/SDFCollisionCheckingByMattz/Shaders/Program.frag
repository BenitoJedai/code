vec2 tA = vec2(0);
float rA = 0.0;

vec2 tB = vec2(0);
float rB = 0.0;

float cycle(float period, float stops) {
    float u = fract(iGlobalTime/(period*stops));
    return floor(u*stops);
}

float whichF = cycle(2.0, 3.0);
float whichA = cycle(6.0, 4.0);
float whichB = cycle(24.0, 4.0);

vec2 xformFwd(vec2 p, vec2 t, float r) {
    float c = cos(r);
    float s = sin(r);
    return mat2(c, -s, s, c)*p + t;
}

vec2 xformInv(vec2 p, vec2 t, float r) {
    float c = cos(r);
    float s = sin(r);
    return mat2(c, s, -s, c)*(p - t);
}

vec3 hue(float h) {	
	vec3 c = mod(h*6.0 + vec3(2, 0, 4), 6.0);
	return clamp(min(c, -c+4.0), 0.0, 1.0);
}

float sd(vec2 p, vec3 c) {
    return c.z*length(p-c.xy);
}

vec3 cDisc(vec2 p) {           
    const float r = 2.5;
    float scl = r/length(p);
    vec2 pc = p*scl;
    return vec3(pc, scl > 1.0 ? -1.0 : 1.0);
}

vec3 cBox(vec2 p, vec2 b) {
    
    vec2 s = sign(p);
    vec2 d = abs(p) - b;
    float md = max(d.x, d.y);
    if (md < 0.0) { // inside
        if (md == d.x) { // 
            return vec3(p - s*vec2(d.x, 0), -1.);
        } else {
            return vec3(p - s*vec2(0, d.y), -1.);
        }
    } else {
       	return vec3(clamp(p, -b, b), 1.); // outside
    }
    
}


float lsign(vec2 p1, vec2 p2, vec2 p3) {
    return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
}

vec3 lclosest(vec2 p, vec2 a, vec2 b) {
    vec2 pc = mix(a, b, clamp(dot(p-a,b-a)/dot(b-a,b-a), 0., 1.));
    return vec3(pc, length(p-pc));
}

vec3 lselect(vec3 pc1, vec3 pc2) {
    return pc1.z < pc2.z ? pc1 : pc2;
}

vec3 cTri(vec2 p) {
    
    vec2 a = vec2(-3., -1.75);
    vec2 b = vec2( 0.,  3.5);
    vec2 c = vec2( 3., -1.75);
    
    float e = max(max(lsign(p, a, b), lsign(p, b, c)), lsign(p, c, a));
    
    vec3 pc1 = lclosest(p,a,b);
    vec3 pc2 = lclosest(p,b,c);
    vec3 pc3 = lclosest(p,c,a);
        
    vec3 pc = lselect(pc1, lselect(pc2,pc3));
            
    return vec3(pc.xy, sign(e));
    
}

vec3 cCapsule(vec2 p, vec2 lr) {
    vec2 a = vec2(0.5*lr.x, 0);
    vec3 lc = lclosest(p, -a, a);
    vec2 pd = p-lc.xy;
    vec2 pc = lc.xy + pd*lr.y/lc.z;
    return vec3(pc, sign(lc.z-lr.y));
}

vec3 cShape(vec2 p, float t) {
    return t == 0.0 ? cTri(p) :
    	t == 1.0 ? cDisc(p) :
    	t == 2.0 ? cBox(p, vec2(3.0, 2.0)) :
    	cCapsule(p, vec2(4.0, 2.0));
}
vec3 shapeA(vec2 p) {
    p = xformInv(p, tA, rA);
    vec3 c = cShape(p, whichA);
    return vec3(xformFwd(c.xy, tA, rA), c.z);
}

vec3 shapeB(vec2 p) {
    p = xformInv(p, tB, rB);
    vec3 c = cShape(p, whichB);
    return vec3(xformFwd(c.xy, tB, rB), c.z);
}

float triwave(float x) {
    return 2.0*abs(0.5*x-floor(0.5*x+0.5));
}

vec3 d2c(float d) {
    
    float n = floor(d*2.0); // ring spacing
    float s = mod(n, 2.0);
    float h;

    if (d > 1e-6) {
    	h = triwave(n/16.)*.33333;       
    } else {
        h = 0.83333 - (1.0-triwave(n/12.))*.1666;        
    }
    
    return mix(hue(h)*(0.5*s + 0.5), vec3(1), 0.6);    
    
}

vec2 projClosest(vec2 p, vec3 c) {
    return c.z < 0.0 ? p : c.xy;
}

#define GLYPH_A  vec2(2.0 + 8.0 + 32.0 + 64.0 + 128.0 + 256.0 + 512.0 + 2048.0 + 4096.0 + 16384.0, 4.0)
#define GLYPH_B  vec2(1.0 + 2.0 + 8.0 + 32.0 + 64.0 + 128.0 + 512.0 + 2048.0 + 4096.0 + 8192.0, 4.0)
#define GLYPH_d  vec2(4.0 + 32.0 + 64.0 + 128.0 + 256.0 + 512.0 + 2048.0 + 4096.0 + 8192.0 + 16384.0, 4.0)
#define GLYPH_M1 vec2(1.0 + 8.0 + 16.0 + 64.0 + 256.0 + 512.0 + 4096.0, 3.0)
#define GLYPH_M2 vec2(2.0 + 8.0 + 16.0 + 128.0 + 1024.0 + 8192.0, 3.0)
#define GLYPH_X  vec2(1.0 + 4.0 + 8.0 + 32.0 + 128.0 + 512.0 + 2048.0 + 4096.0 + 16384.0, 4.0)
#define GLYPH_P1 vec2(2.0 + 8.0 + 64.0 + 512.0 + 8192.0, 3.0)
#define GLYPH_P2 vec2(1.0 + 16.0 + 128.0 + 1024.0 + 4096.0, 3.0)
#define GLYPH_K  vec2(1024.0 + 4096.0, 4.0)


float glyphCover(inout vec2 p, vec2 gdata) {
    p = floor(p);
    float c;
    if (p.x >= 0.0 && p.x < 3.0 && p.y >= 0.0 && p.y < 5.0) {
        float bit = dot(p, vec2(1.0, -3.0)) + 12.0;
        c = mod(floor(gdata.x / pow(2.0, bit)), 2.0);
    } else {
        c = 0.0;
    }
    p.x -= gdata.y;
    return c;
}
            


float text_legend(vec2 p) {
    
    float c = 0.0;
    
    if (whichF == 2.0) {
        c += glyphCover(p, GLYPH_M1);
        c += glyphCover(p, GLYPH_M2);
        c += glyphCover(p, GLYPH_A); 
        c += glyphCover(p, GLYPH_X); 
        c += glyphCover(p, GLYPH_P1);
    } 
    if (whichF != 1.0) {
        c += glyphCover(p, GLYPH_d); 
        c += glyphCover(p, GLYPH_A); 
    } 
    if (whichF == 2.0) {
        c += glyphCover(p, GLYPH_K); 
    }
    if (whichF != 0.0) {
        c += glyphCover(p, GLYPH_d); 
        c += glyphCover(p, GLYPH_B); 
    }
    if (whichF == 2.0) {
        c += glyphCover(p, GLYPH_P2); 
    }
    return c;
        
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
    
    float px = 24.0 / iResolution.x;
    float txsz = 4.0;
    
	vec2 p = (fragCoord.xy  - 0.5*iResolution.xy) * px;
    
    rA =  0.1*iGlobalTime;
    rB = -0.2*iGlobalTime;

    tB = vec2(6.0*sin(iGlobalTime*0.5), -3.0*cos(iGlobalTime*0.3333));
    
    float dA = sd(p, shapeA(p));
    float dB = sd(p, shapeB(p));
    
    vec2 q = vec2(5.0, 3.0);
    
    vec3 cA, cB;
    float dqA=1., dqB=1., dq=1.;
    
    vec2 g = vec2(0);
    
    bool hit = false;
    
    for (int iter=0; iter<50; ++iter) {
            
        float ss = hit ? 0.1 : dq * 0.95 + 0.1;        

        q -= ss*g;

        cA = shapeA(q);
        cB = shapeB(q);

        dqA = length(q-cA.xy)*cA.z;
        dqB = length(q-cB.xy)*cB.z;
        dq = max(dqA, dqB);
        
        if (dq < 0.0) { 	
            hit = true;
        }

        if (dq == dqA) {
            g = cA.z * normalize(q-cA.xy);
        } else {
            g = cB.z * normalize(q-cB.xy);        
        }
        
    }    
    
            
    float d = whichF == 0.0 ? dA : whichF == 1.0 ? dB : max(dA, dB);
        
	vec3 c = d2c(d);
    
    float o = abs(dA);
    o = min(o, abs(dB));
    o = min(o, abs(length(p-q)-3.0*px));
    
    o = min(o, max(length(p-cA.xy)-3.0*px, 0.0));
    o = min(o, max(length(p-cB.xy)-3.0*px, 0.0));
    o = smoothstep(o, 0.0, 0.25*px);
    
    vec2 tc = fragCoord / txsz - vec2(1.0, 1.0);
        
    float txt = text_legend(tc);
    vec2 ac = (p - tA)/(txsz*px) + vec2(1.5, 2.5);
    float ca = glyphCover(ac, GLYPH_A);

    vec2 bc = (p - tB)/(txsz*px) + vec2(1.5, 2.5);
    float cb = glyphCover(bc, GLYPH_B);
    
    
    c = mix(c, vec3(0), 0.6*txt);
    
    c = o*c;

    c = mix(c, vec3(1), 0.7*max(ca,cb));



    fragColor = vec4(c,1.0);

    
}