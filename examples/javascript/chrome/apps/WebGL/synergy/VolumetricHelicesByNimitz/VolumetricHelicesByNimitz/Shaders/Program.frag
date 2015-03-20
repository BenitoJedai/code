﻿//Helices by nimitz (twitter: @stormoid)

/*
	Started as an experiment to improve performance of volumetric raymarching of deformed
	geometry, turned out to be better than expected.

	The first optimization is "dual raymarching", the first raymarching loop is very coarse
	and lets the volumetric function be ran for only the needed portion of the image and
	with a better starting position (on the surface of the volumetric ray).

	Then the same distance field is used for volumetric drawing but instead of simply using
	a fixed step, the function is also incremented with dynamic step provided the ray is
	outside the geometry. This allows for stepping through long volumetric spaces
	with a relatively small computational cost.

	Also includes an experiment on spherically projected backgrounds, which allows to fake geometry
	without any intersection checking.

*/

//RANGE 0...1
#define RAY_THICKNESS 0.7
#define RAY_BRIGHTNESS .86
#define RAY_FUZZYNESS 0.09

#define POSTPROCESS

#define MAX_ITER 35
#define FAR 20.
#define time iGlobalTime

//const friendly mix()
#define MIX(X, Y, A) ((X) + (A)*((Y) - (X)))

const float tk = MIX(0.04, 0.05, RAY_THICKNESS);
const float fz = MIX(0.04, 0.005, RAY_FUZZYNESS);
const float ints = MIX(0.005, 0.0014, RAY_BRIGHTNESS);
const float intsfz = ints/fz;

float noise( in float x ){return texture2D(iChannel0, vec2(x*.01,1.)).x;}
float noise( in vec2 x ){return texture2D(iChannel0, x*.01).x;}
vec2 opU( vec2 d1, vec2 d2 ){ return (d1.x<d2.x) ? d1 : d2;}

//l.x = amplitude | l.y = frequency | l.z = speed | l.w = offset
//(Tried using a proper helix sdf, but it wasnt as deformable)
float helix( vec3 p, vec3 a, vec3 b, vec4 l)
{
  	p.x += sin(p.z*l.y+time*5.*l.z+l.w)*l.x;
    p.y += cos(p.z*l.y+time*5.*l.z+l.w)*l.x;
    vec3 pa = p - a;
	vec3 ba = b - a;
	float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
	
	return length( pa - ba*h )*.6;
}

vec3 path(float x)
{   
    return vec3(cos(time+x*0.5)*0.4, sin(time+x)*0.2,0.);
}

//the map is used for both marching loops
vec2 map(vec3 p)
{
    p -= path(p.z);
    vec2 rz = vec2(0);
    vec3 bg = vec3(0.,0.,-15.);
    vec3 en = vec3(0.,0.,1.);
    
    const float pw = 0.15;
    
    rz.xy = 		 vec2(helix(p, bg, en, vec4(pw,    4.,   1., 3.1415))-tk, 1.05);
    rz.xy = opU(rz, vec2(helix(p, bg, en, vec4(pw,    4.,   1., 0.))-tk, 3.2) );
    rz = opU(rz, vec2(helix(p, bg, en, vec4(0.003, 100., 20.,1.))-tk, 2.) );
    rz = opU(rz, vec2(helix(p, bg, en, vec4(0.07,  2., 1.,  0.))-tk, 5.) );
    
    return rz;
}

//first raymarching pass (very coarse)
float march(in vec3 ro, in vec3 rd, in float startf, in float maxd)
{
	float precis = 0.01;
    float h=precis*2.0;
    float d = startf;
    for( int i=0; i<MAX_ITER; i++ )
    {
        if( abs(h)<precis||d>maxd ) break;
        d += h;
	    float res = map(ro+rd*d).x;
        h = res;
    }
	return d;
}

//volumetric marching
vec3 vmarch(in vec3 ro, in vec3 rd)
{   
    vec3 p = ro;
    vec2 r = vec2(0.);
    vec3 sum = vec3(0);
    float tot = 0.;
    for( int i=0; i<200; i++ )
    {
        r = map(p);
        if (r.x > .5)break;
        vec3 col = sin(vec3(1.5,2.,1.8)*r.y*1.3+0.4)*.9+0.15;
        col.rgb *= smoothstep(fz,intsfz,-r.x);
        sum += abs(col) * (1.8-noise(p.x*1.+p.z*10.+time*16.)*1.3);
        //"hybrid" step
        p += rd*max(.015, max(r.x,0.)*3.);
    }
    return clamp(sum,0.,1.);
}

//-------------------------------------------------------
//-------------------background--------------------------
//-------------------------------------------------------
float f(vec3 p)
{
    //any metric can be used (using chebyshev here)
    float r = max(abs(p.x),abs(p.y));
    vec3 z = vec3(p)/(.8+r);
    
    const float frq = 10.;
    float rz = abs(fract(z.x*frq)-0.5);
    rz = min(abs(fract(z.y*frq)-0.5),rz);
    rz = max(abs(fract(z.z*frq)-0.5),rz)-0.04;
    
    return rz;
}

vec3 normal(const in vec3 p)
{  
	vec2 e = vec2(-1.0, 1.0)*0.001;   
	return normalize(e.yxx*f(p + e.yxx) + e.xxy*f(p + e.xxy) +
					 e.xyx*f(p + e.xyx) + e.yyy*f(p + e.yyy));
}

//ao from iq (for the background)
float calcAO(in vec3 p, in vec3 nor)
{
    float tot = 1.0;
    float sca = 20.0;
    for(int i=0; i<5; i++)
    {
        float hr = 0.001 + 0.005*float(i*i);
        vec3 pos =  nor * hr + p;
        float d = f(pos)*0.025;
        tot += (d-hr)*sca;
        sca *= .5;
    }
    return clamp( tot, 0.0, 1.0 );
}
//----------------------------------------------------
//----------------------------------------------------


//vector wheel (for camera switches)
vec3 wheel(in vec3 a, in vec3 b, in vec3 c, in float delta)
{
	return mix(mix(mix( a,b,clamp((delta-0.0000)*3., 0., 1.)),
						  c,clamp((delta-0.3333)*3., 0., 1.)),
						  a,clamp((delta-0.6666)*3., 0., 1.));
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{	
	vec2 p = fragCoord.xy/iResolution.xy*2.-1.;
    vec2 bp = p;
	p.x*=iResolution.x/iResolution.y;
    p*= .15+(sin(time*0.25)*0.5+0.5)*0.25;
	vec2 um = iMouse.xy/iResolution.xy-.5;
	um.x *= iResolution.x/iResolution.y;
    
	//cameras
    const float ptx = 0.9;
	vec3 ro1 = vec3(1.,sin(time*0.2),3.)+path(ptx);
	vec3 ta1 = vec3(0,0.,sin(time))+path(ptx);
    vec3 ro2 = vec3(sin(-time*1.2)*0.3,cos(-time*1.1)*0.3,-17.5)+path(ptx);
	vec3 ta2 = vec3(0,0.,-14.)+path(ptx);
    vec3 ro3 = vec3(5.,2.,sin(time*.4)*10.-9.)+path(ptx);
	vec3 ta3 = vec3(0,0.,sin(time*.4)*3.-9.)+path(ptx);
    
    #if 1
    float m = fract( (floor(time*.12)+smoothstep(0.4,.6,fract(time*.12)))*.3333 );
    vec3 ro = wheel(ro1,ro2,ro3,m);
    vec3 ta = wheel(ta1,ta2,ta3,m);
    #else
    vec3 ro = ro1; 
    vec3 ta = ta1;
    #endif
    
    vec3 ww = normalize( ta - ro);
    vec3 uu = normalize(cross( vec3(0.0,1.0,0.0), ww ));
    vec3 vv = normalize(cross(ww,uu));
    vec3 rd = normalize( p.x*uu + p.y*vv + 1.5*ww );
	
    
	float rz = march(ro,rd,0.,FAR);
    vec3 col = vec3(0);
    vec3 pos = ro+rz*rd;
    if (rz < FAR)
    {
    	col = vmarch(pos,rd);
    }
    
    //background
    vec3 nor = normal(rd);
    vec3 ligt = normalize(vec3(cos(time*0.4),sin(time*0.42),.5));
    float ao = calcAO(rd, nor);
    ao*= ao;
    
    float mat = f(rd);
    float dif = abs(dot(nor,ligt))*ao;
    float bac = ao*clamp( dot( nor, normalize(vec3(-ligt.x,0.0,-ligt.z))), 0., 1. )*clamp( 1.0-rd.y,0.0,1.0);
    float fre = pow( clamp(1.+dot(nor,rd), 0., 1.), 2. )*ao;
    float spe = pow(abs(dot(reflect(rd,nor),ligt)),10.)*ao;
    col += 2.2*(0.1*vec3(0.5,0.7,.9)*bac + 0.2*vec3(1.)*spe + 0.2*vec3(0.3,0.3,.4)*fre +
           		0.1*vec3(0.2,0.5,0.9)*dif + 0.2*vec3(0.5,0.5,.7)*mat);
    
    col = pow(col,vec3(1.5))*1.1;
    
    #ifdef POSTPROCESS
    col *= 1.-pow(length(bp*bp*bp*bp)*1.09,10.);
    col += (smoothstep(.0,.2,abs(fract(time*10.+p.y*iResolution.y*0.3)-0.5))*.03-0.04);
    #endif
    
    //col = vec3(pow(ao,1.)); //shows the background AO only
    
	fragColor = vec4( col, 1.0 );
}