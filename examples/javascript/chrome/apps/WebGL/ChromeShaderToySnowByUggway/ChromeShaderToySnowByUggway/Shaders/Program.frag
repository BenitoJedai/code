#ifdef GL_ES
precision highp float;
#endif

#define resolution iResolution
#define time iGlobalTime
////include "tex_bomb.sh"

//uniform float time;
//uniform vec2 resolution;

//float time = 10.0;

//vec2 resolution = vec2(800,600);

struct Cam{
	vec3 pos;
	vec3 dir;
	vec3 up;
	vec3 ray;
};

struct Disp{
	vec3 origin;
	vec2 uvpos;
	float aspectRatio;
	vec3 hit;
};

struct CastRes {
	float dist;
	vec3 pos;
	vec3 color;
	float id;
};

#define MAX_DIST 80.0
#define MAX_ITERATIONS  100	
	
#define EPS 0.00001

#define ID_NONE 0.	
#define ID_PLANE 1.	
#define ID_BOX 2.
#define ID_SPHERE 3.
#define ID_SNEG 4.	
#define ID_CONE 5.
#define ID_EYE 6.	
#define ID_STVOL 7.
#define ID_ELKA 8.	
#define ID_STAR	9.
	
vec3 lightPos = vec3(10.0, 10.0, 10.0);
vec3 lightDirP = vec3(0,0,0);
vec3 lightDir = -normalize(lightDirP - lightPos);
vec3 lightCol = vec3(0.9, 0.7, 0.9);	

vec3 CamPos(float t){ // camera position func
	//return vec3(0., 1.0, 1.0);
	//return vec3(2., 2., 2.);
	//return vec3(sin(t*0.45)*2.0 +1.5 ,2.3, cos(t*0.45) * 2.0 +1.5);
	return vec3(1.0 + 2.5*cos(t*0.25), 1.5, 0.0 + 2.5 * sin(t*0.25)); // circle x = x0 + r * cos(phi); y = y0 + r * sin(phi) 
	//return vec3(sin(t + 1.0)*1.0, 1, cos(t)*1.0);
}

Cam setCam(vec2 fragCoord)
{
	Cam cam;
	Disp scr;
  	
	cam.dir = vec3(1,1,0);	 // direction point
	cam.pos = CamPos(time); // position 
	cam.up = vec3(0.0,1.0,0.0);
  	
	
	vec3 look_dir = normalize(cam.dir - cam.pos);
  	vec3 plane_left = cross(look_dir, cam.up);
 	cam.up = cross(plane_left, look_dir);
	
	scr.origin = (cam.pos + look_dir);
	scr.uvpos = -1.0 + 2.0 * fragCoord.xy/resolution.xy; // uv coord
 	scr.aspectRatio = resolution.x / resolution.y; // aspect ratio
	scr.hit = scr.origin + scr.uvpos.x * plane_left * scr.aspectRatio + scr.uvpos.y * cam.up;
  
	cam.ray = normalize(scr.hit - cam.pos);
	
	return cam;
}

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

float rand(vec3 co){
    return fract(sin(dot(co.xyz ,vec3(12.9898,78.233,47.985))) * 43758.5453);
}

// credit: iq/rgba
float hash( float n )
{
    return fract(sin(n)*43758.5453);
}


// credit: iq/rgba
float noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
    f = f*f*(3.0-2.0*f);
    float n = p.x + p.y*57.0 + 113.0*p.z;
    float res = mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                        mix( hash(n+ 57.0), hash(n+ 58.0),f.x),f.y),
                    mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                        mix( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
    return res;
}

float sdPlane( vec3 p, vec4 n )
{
  // n must be normalized
  return dot(p,n.xyz) + n.w;
}

float sdHPlane( vec3 p, float n)
{ 
  return p.y + n;
}
float sdBox( vec3 p, vec3 b )
{
  vec3 d = abs(p) - b;
  return min(max(d.x,max(d.y,d.z)),0.0) +
         length(max(d,0.0));
}

float sdSphere( vec3 p, float s )
{
  return length(p)-s;
}

float sdCone( vec3 p, vec2 c )
{
    // c must be normalized
    float q = length(p.xy);
    return dot(c,vec2(q,p.z));
}

float sdCone( in vec3 p, in vec3 c )
{
    vec2 q = vec2( length(p.xz), p.y);
		float t = sin(time)*sin(p.z);
        return max( max( dot(q,c.xy), p.y), -p.y-c.z );
}

float opU( float d1, float d2)
{
    return min(d1,d2);
}

float opI( float d1, float d2 )
{
    return max(d1,d2);
}

float sdCylinder( vec3 p, vec2 h )
{
  return max( length(p.xz)-h.x, abs(p.y)-h.y );
}


// Simple 2d noise algorithm contributed by Trisomie21 (Thanks!)
float snoise( vec2 p ) {
	vec2 f = fract(p);
	p = floor(p);
	float v = p.x+p.y*1000.0;
	vec4 r = vec4(v, v+1.0, v+1000.0, v+1001.0);
	r = fract(100000.0*sin(r*.001));
	f = f*f*(3.0-2.0*f);
	return 2.0*(mix(mix(r.x, r.y, f.x), mix(r.z, r.w, f.x), f.y))-1.0;
}

/*float disp(vec3 p)
{
	return noise(5.*p-vec3(1.5,2.5,3.5))+noise(p*0.4)*0.5;//*sin(time);
	//return -sin(0.5* p.x)*sin(0.5 * p.y)*sin(.5*p.z)*sin(time*0.5);
}*/
float opS( float d1, float d2 )
{
    return max(-d1,d2);
}

vec3 sp_pos;

CastRes DistFunc(CastRes d)
{
	#define SEL_OBJ(oid) if(dd < d.dist) {d.dist = dd; d.id = oid;}
	float dd;		
	d.id = ID_NONE;
	d.dist = length(d.pos);
	
	if(d.pos.y > 5.){
	
		return d;
	}
	
	dd = sdHPlane(d.pos, 0.0);
	float wave = cos(d.pos.z)*0.6 + 0.1, wave1 = cos(d.pos.x)*0.5+0.3;
	float rv = wave  + wave1 + noise(d.pos);
	dd += -1.5 + rv + rand(d.pos)*0.00007;//sin(1.1*d.pos.x) * sin(1.1*d.pos.y) * sin(0.7*d.pos.z);
	SEL_OBJ(ID_PLANE);
	sp_pos = vec3(1.+2.0*cos(time), 1.0 + 1.0*cos(time), 0.0+2.0*sin(time));
	dd = sdSphere(d.pos - sp_pos, 0.15);
	
	SEL_OBJ(ID_SPHERE);	
	
	vec3 dt = vec3(0);
	dd = opU(opU(sdSphere(d.pos - vec3(1.0, 0, 0.0)+dt, 0.5), sdSphere(dt+d.pos - vec3(1.0, 0.7, 0.0), 0.4)),sdSphere(dt + d.pos - vec3(1.0, 1.3, 0.0),0.3));
	dd += rand(d.pos)*0.00001;
	 
	SEL_OBJ(ID_SNEG);
	
	dd =  sdSphere(dt + d.pos - vec3(1.1, 1.3, 0.3), 0.05);
	dd = opU(dd, sdSphere(dt + d.pos - vec3(0.9, 1.3, 0.3), 0.05));		
	SEL_OBJ(ID_EYE);
		
	dd = opI( sdCone(dt + d.pos - vec3(1., 1.25, 0.6), normalize(vec2(0.5,0.1))), -sdPlane( dt + d.pos - vec3(2, 2.25, -0.1), normalize(vec4(0 ,0, 0.5, 0))) );
	dd = opI(opU(dd, sdCone(dt + d.pos - vec3(1., 2.4, 0), vec3(0.6,0.12, 0.9))), sdPlane(dt +  d.pos - vec3(2, 1.8, -0.1), normalize(vec4(0 ,1, 0.0, 0))));
	SEL_OBJ(ID_CONE);
	
	vec3 q = d.pos; // generate army of fir-tree
	q += vec3(-2.0, 0.0, -2.0);	
	q.x = mod(q.x, 4.);
	q.z = mod(q.z, 4.);
    q -= vec3(2.0, 0.0, 2.0);

	q.x = abs(q.x);  // mirror in X	

	
	dd = sdCylinder(q - vec3(2.0, 0.0, 0.0), vec2(.2, 2.0));
	SEL_OBJ(ID_STVOL);
	
	
	dd = sdCone(q - vec3(2.0, 2.4, 0.0), vec3(0.6,0.4,0.5));
	dd = opU(dd, sdCone(q - vec3(2.0, 2.14, 0.0), vec3(0.4,0.3,0.6)));
	dd = opU(dd, sdCone(q - vec3(2.0, 1.69, 0.0), vec3(0.3,0.3,0.6)));      
   		
	SEL_OBJ(ID_ELKA);
	
return d;
}
vec3 getNormal(CastRes res)
{
	CastRes r = res, l = res;
	#define DELTA 0.02
	vec2 t = vec2(DELTA, 0);
	vec3 d;
	l.pos = res.pos + t.xyy;
	r.pos = res.pos - t.xyy;
	d.x = DistFunc(l).dist - DistFunc(r).dist;
	
	l.pos = res.pos + t.yxy;
	r.pos = res.pos - t.yxy;
	d.y = DistFunc(l).dist - DistFunc(r).dist;

	l.pos = res.pos + t.yyx;
	r.pos = res.pos - t.yyx;
	d.z = DistFunc(l).dist - DistFunc(r).dist;	
	
	return normalize( d );
}

float ambientOcclusion(CastRes res, vec3 n) {
    float step = 1.;
    float ao = 0.0;
    float dist;
    CastRes cr = res;
    for (int i = 1; i <= 3; i++) {
        dist = step * float(i);
	 cr.pos = res.pos + n * dist;
		ao += max(0.0, (dist - DistFunc(cr).dist) / dist);  
    }
    return 1. - ao * 0.1;
}

vec3 sky(Cam cam, CastRes r)
{
	vec3 color = vec3(0.1,0.2,0.4);
	float sun = clamp(dot(cam.ray,lightDir),0.3,1.0); // sun orb
		color += 0.8 * lightCol * sun*sun;
		// something like te aurora
		float c = snoise(vec2((r.pos.xy)*0.5) * 0.2*(sin(time*0.25)*0.5)); //0.1+time*0.5,
		color = mix( color, vec3(0.2, 1.0, 0.7), smoothstep(0.0, 3.5, c) );
		return color;

}



float softshadow( in vec3 ro, in vec3 rd, in float mint, in float maxt, in float k )
{
    float r = 1.0;
    float dt = 0.02;
    float t = mint;
    CastRes cr;
    for( float i=0.; i<10.; i++ )
    {
        if( t < maxt )
        {
          cr.pos = ro + rd*t;
          float h = DistFunc(cr).dist;
          r = min( r, k*h/t );
          t += max( 0.02, r);
        }
    }
    return clamp( r, 0.0, 1.0 );

}

vec3 stars(vec3 d) //render stars 
{	
	float s = snoise(d.xy*123.)*snoise(d.xy*345.);
	s=pow(s,15.0)*8.0;
	return vec3(s);

}

vec3 GetColor(Cam cam, CastRes res)
{
	vec3 c = vec3(0.5, 0.5, 0.9);
	
	vec3 norm = getNormal(res);
	
	vec3 ambcol = vec3(0.05,0.07,0.12)*7.0;
	vec3 difcol = vec3(0.9);
	vec3 groundcol = (lightCol + ambcol) * 1.0;	
	
	float dif = clamp(dot(norm,lightDir),0.0,1.0);		// diffuse		
	vec3 refl = reflect(cam.ray,norm);
	vec3 spec = lightCol * pow(clamp(dot(lightDir,refl),0.0,1.0),2.0) * 0.2;		
	vec3 totalspec = vec3(0.0);	
	vec3 totaldif = vec3(0.0);	
	
	float ao = ambientOcclusion(res, norm);		//ambient oclusion
	float groundamb = pow(clamp(dot(norm, vec3(0.0,-1.0,0.0)),0.0,1.0),2.0); 
	float shadow = 0.01;
	
	shadow = softshadow(res.pos, lightDir, 1., 5., 15.);	
	
	dif *= shadow;
	
	vec3 splt = vec3(0.9, 0.4, 0.3);
	vec3 spdr = sp_pos - res.pos;
	float invDist= 0.5 / sqrt( dot(spdr,spdr) );
	 spdr= spdr * invDist; 	
	
	float shadow2 = 1.0;
	
	if(res.id == ID_PLANE || res.id == ID_SNEG){		
		for(float i=0.0;i < 10.0;i++){
			vec3 p2 = res.pos * 20. + vec3(i*20.0);
			vec3 nor2 = normalize((vec3(noise(p2*1.5),noise(p2*5.),noise(p2*10.))-vec3(0.5)));
			float facetint = 0.05 + rand(p2);			
			vec3 refl = reflect(cam.ray,nor2);	
			totalspec += pow(clamp(dot(lightDir,refl),0.0,1.0),200.0) * (clamp(dot(norm,lightDir)+0.1,0.0,1.0)) * facetint;
		}
		
		c =  splt * clamp(dot(spdr, norm),0.0,1.0) * shadow2 + c  * dif + totalspec*shadow + spec*shadow + ambcol * ao + groundcol * groundamb;	
		float whitelevel = 1.2;
		c = (c  * (vec3(1.0) + (c / (whitelevel * whitelevel))  ) ) / (vec3(1.0) + c);	
		
		c = pow(c,vec3(1.0/2.5))*exp(-res.dist*res.dist*0.005);//* (1.05 - res.dist * 0.05);               	
	
		return c;		
	
	}
	
	
	if(res.id == ID_SPHERE){
		c = vec3(0.9, 0.4, 0.3);
	}
	
	if(res.id == ID_BOX){
		c = vec3(0.3,0.4,0.5);
	}
	
	if(res.id == ID_EYE)
	{
		c = vec3(0.7, 0.7, 1.0);
	}
	
	if(res.id == ID_CONE)
	{
		c = vec3(0.9, 0.5, 0.2);
	}
	if(res.id == ID_STVOL)
	{
		float ns = noise(res.pos*123.)*noise(res.pos*59.);
		c = vec3(0.7, 0.4, 0.2)*ns;/* * rand(vec2(res.dist))*0.5*/
		
	}	
	if(res.id == ID_ELKA)
	{
		float ns = noise(res.pos*223.)*noise(res.pos*79.)*0.5;
		c = vec3(0.4, 0.9, 0.5);//*rand(res.pos)*0.2;		
		c *= ns;
		
		
	}	
	if(res.id == ID_STAR){
		//c = vec3(1.0);
		
		return c;
	}
	if(res.id == ID_NONE) {
		c = sky(cam, res);
		c += stars(cam.ray)*0.5;
		return c;
	}
	
	
	
	

	  
	
	c = splt * clamp(dot(spdr, norm),0.0,1.0)*0.2 + lightCol * c * dif + spec + ao*vec3(1.)*0.001;//	+ ao *.1 + groundamb * groundcol * 0.3;
    	
	c = pow(c,vec3(1.0/2.5)) * exp(-res.dist*res.dist*0.005);// fog
	
	return c;
	
	
	
}

CastRes rayCast(Cam cam)
{
	CastRes res;
	res.color = vec3(0.1);
	


	
	float dist = 0.0;

	for(int i = 0; i < MAX_ITERATIONS; ++i)
	{
		res.pos = cam.pos + cam.ray * dist;
		res = DistFunc(res);
		
		if(res.dist <= EPS)
		{
			break;
		}
		
		dist += res.dist;
				
		if(dist >= MAX_DIST)
		{
			break;
		}
	}
	
	res.dist = dist;
	
	res.color = GetColor(cam, res);

	return res;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	Cam camera = setCam(fragCoord);
	CastRes cr = rayCast(camera);
	fragColor = vec4(cr.color, 1.0);
}
