//Parallax mapping by nimitz (twitter: @stormoid)

/*
	I was reading on parallax mapping and thought this would be
	of good use for cheap detailing. Yet as of this writing, 
	there isn't a single implementation of it on shadertoy.

	This implementation was written from scratch, 
	let me know if you see errors/optimizations.

	I tried getting the exact height for every bump mapping tap, but it
	is more expensive to compute and the results weren't better.

	Relevant code is at line 106
*/

//Show only the raymarched geometry (for comparison)
//#define RAYMARCHED_ONLY

//The amount of parallax
#define PARALLAX_SCALE .2

//Scale the texture offset as a function of incidence (much better results)
#define USE_OFFSET_SCALING
#define OFFSET_SCALE 2.

//Bump mapping intensity
#define BUMP_STRENGTH .1
#define BUMP_WIDTH 0.004

//Main texture scale
const float texscl = 2.5;

#define ITR 70
#define FAR 15.
#define time iGlobalTime

mat2 mm2(in float a){float c = cos(a), s = sin(a);return mat2(c,-s,s,c);}
float hash(vec2 x){	return fract(cos(dot(x.xy,vec2(2.31,53.21))*124.123)*412.0); }

float sdfsw = 0.; //Global mouse control

float length4(in vec3 p ){
	p = p*p; p = p*p;
	return pow( p.x + p.y + p.z, 1.0/4.0 );
}

float map(vec3 p)
{
    float d = mix(length(p)-1.1,length4(p)-1.,sdfsw-0.3);
    d = min(d, -(length4(p)-4.));
    return d*.95;
}

float march(in vec3 ro, in vec3 rd)
{
	float precis = 0.001;
    float h=precis*2.0;
    float d = 0.;
    for( int i=0; i<ITR; i++ )
    {
        if( abs(h)<precis || d>FAR ) break;
        d += h;
	    float res = map(ro+rd*d);
        h = res;
    }
	return d;
}

vec3 normal(in vec3 p)
{  
    vec2 e = vec2(-1., 1.)*0.005;   
	return normalize(e.yxx*map(p + e.yxx) + e.xxy*map(p + e.xxy) + 
					 e.xyx*map(p + e.xyx) + e.yyy*map(p + e.yyy) );   
}

//From TekF (https://www.shadertoy.com/view/ltXGWS)
float cells(in vec3 p)
{
    p = fract(p/2.0)*2.0;
    p = min(p, 2.0-p);
    return 1.-min(length(p),length(p-1.0));
}

float tex( vec3 p )
{
    p *= texscl;
    float rz= 0.;
    float z= 1.;
    for ( int i=0; i<2; i++ )
    { 
        #ifndef RAYMARCHED_ONLY
        rz += cells(p)/z;
        #endif
        p *= 1.5;
        z *= -1.1;
    }
    return clamp(rz*rz*2.5,0.,1.)*2.-1.;
}

/*
	The idea is to displace the shaded position along the surface normal towards
	the viewer,	the tgt vector is the displacement vector, then	I apply a scaling
	factor to the displacement and also have an incidence based	offset scaling set up.
*/
vec3 prlpos(in vec3 p, in vec3 n, in vec3 rd)
{
    //vec3 tgt = cross(cross(rd,n), n); //Naive method (easier to grasp?)
    vec3 tgt = n*dot(rd, n) - rd; //Optimized

#ifdef USE_OFFSET_SCALING
    tgt /= (abs(dot(tgt,rd)))+OFFSET_SCALE;
    
#endif
    
    p += tgt*tex(p)*PARALLAX_SCALE;
    return p;
}

float btex(in vec3 p)
{
    float rz=  tex(p);
    rz += tex(p*10.)*0.01; //Extra (non-parallaxed) bump mapping can be added
    return rz;
}

vec3 bump(in vec3 p, in vec3 n, in float ds)
{
    vec2 e = vec2(BUMP_WIDTH*sqrt(ds)*0.5, 0);
    float n0 = btex(p);
    vec3 d = vec3(btex(p+e.xyy)-n0, btex(p+e.yxy)-n0, btex(p+e.yyx)-n0)/e.x;
    vec3 tgd = d - n*dot(n ,d);
    n = normalize(n-tgd*BUMP_STRENGTH*8./(ds));
    return n;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{	
	vec2 bp = fragCoord.xy/iResolution.xy*2.-1.; 
    vec2 p = bp;
	p.x*=iResolution.x/iResolution.y;
	vec2 mo = iMouse.xy / iResolution.xy-.5;
    mo = (mo==vec2(-.5))?mo=vec2(0.4,-0.25):mo;
	mo.x *= iResolution.x/iResolution.y;
	p.x += mo.x*1.;
    sdfsw = mo.y*4.;
    
	vec3 ro = vec3(0.,0.,4.);
    vec3 rd = normalize(vec3(p,-3.+sin(time*0.9+sin(time))));
    mat2 mx = mm2(time*.1+sin(time*0.4)-0.2);
    mat2 my = mm2(time*0.07+cos(time*0.33)-0.1);
    ro.xz *= mx;rd.xz *= mx;
    ro.xy *= my;rd.xy *= my;
	
	float rz = march(ro,rd);
	
    vec3 col = vec3(0);
    
    if ( rz < FAR )
    {
        vec3 pos = ro+rz*rd;
        vec3 nor= normal( pos );
        pos = prlpos(pos,nor,rd);
        float d = distance(ro,pos);
        nor = bump(pos, nor, d);

        vec3 ligt = normalize( vec3(-.5, 0.5, -0.3) );
        float dif = clamp( dot( nor, ligt ), 0.0, 1.0 );
        float bac = clamp( dot( nor, normalize(vec3(-ligt))), 0.0, 1.0 );
        float spe = pow(clamp( dot( reflect(rd,nor), ligt ), 0.0, 1.0 ),70.);
        float fre = pow( clamp(1.0+dot(nor,rd),0.0,1.0), 2.0 );
        vec3 brdf = vec3(0.3);
        brdf += bac*vec3(0.3);
        brdf += dif*0.5;
        
        float tx=  tex(pos);
        col = sin(vec3(1.5+mo.x*0.4,2.2+mo.x*0.25,2.7)+tx*1.2+4.2)*0.6+0.55;
        col = col*brdf + spe*.5/sqrt(rz) +.25*fre;
        
        col = mix(col,vec3(.0),clamp(exp(rz*0.43-4.),0.,1.));
    }
    
    col = clamp(col*1.05,0.,1.);
    col *= pow(smoothstep(0.,.2,(bp.x + 1.)*(bp.y + 1.)*(bp.x - 1.)*(bp.y - 1.)),.3);
    col *= smoothstep(3.9,.5,sin(p.y*.5*iResolution.y+time*10.))+0.1;
    col -= hash(col.xy+p.xy)*.025;
	
	fragColor = vec4( col, 1.0 );
}