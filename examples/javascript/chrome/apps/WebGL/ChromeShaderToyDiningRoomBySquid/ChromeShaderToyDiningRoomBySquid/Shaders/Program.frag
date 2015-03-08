
//define this for replicating the row of chairs w/ mod
//seems buggy, sometimes the back of the chairs don't render, but its way faster
#define USE_MOD_FOR_CHAIRS

vec2 raymarch(in vec3 ro, in vec3 rd, in float maxt);
vec3 calc_normal(in vec3 pos);

vec2 opU( vec2 d1, vec2 d2 )
{
	return (d1.x<d2.x) ? d1 : d2;
}
float opS( float d1, float d2 )
{
    return max(-d2,d1);
}

float sdPlane( vec3 p )
{
	return p.y;
}

float sdSphere( vec3 p, float s )
{
    return length(p)-s;
}

float sdBox( vec3 p, vec3 b )
{
  vec3 d = abs(p) - b;
  return min(max(d.x,max(d.y,d.z)),0.0) +
         length(max(d,0.0));
}


vec3 opRep( vec3 p, vec3 c )
{
    return mod(p,c)-0.5*c;
}


float hash( float n ) { return fract(sin(n)*43758.5453123); }
float noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
    f = f*f*(3.0-2.0*f);
	
    float n = p.x + p.y*157.0 + 113.0*p.z;
    return mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                   mix( hash(n+157.0), hash(n+158.0),f.x),f.y),
               mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                   mix( hash(n+270.0), hash(n+271.0),f.x),f.y),f.z);
}

const mat3 m = mat3( 0.00,  0.80,  0.60,
                    -0.80,  0.36, -0.48,
                    -0.60, -0.48,  0.64 );
float tbnoise(in vec3 pos)
{
    float f = 0.;
    vec3 q = 8.0*pos;
            f  = 0.5000*noise( q ); q = m*q*2.01;
            f += 0.2500*noise( q ); q = m*q*2.02;
            f += 0.1250*noise( q ); q = m*q*2.03;
            f += 0.0625*noise( q ); q = m*q*2.01;
    return f;
}

vec2 chair(in vec3 p)
{
    float t = sdBox(p, vec3(.03, .4, .25));//back
    t = opS(t, sdBox(p, vec3(.6, .2, .15)));
    t = min(t, sdBox(p+vec3(.27,.4,0.), vec3(.25, .03, .25)));//seat
    
    //legs
    t = min(t, sdBox(p+vec3(0.,.7,0.22), vec3(.03, .3, .03)));
    t = min(t, sdBox(p+vec3(0.,.7,-0.22), vec3(.03, .3, .03)));
    t = min(t, sdBox(p+vec3(0.49,.7,0.22), vec3(.03, .3, .03)));
    t = min(t, sdBox(p+vec3(0.49,.7,-0.22), vec3(.03, .3, .03)));
    return vec2(t, 4.);
}

vec2 table(in vec3 p)
{
    float t = sdBox(p, vec3(.75, .03, 1.6)); //table top
    //legs
    t = min(t, sdBox(p+vec3(.7, .5, 1.55), vec3(.04, .5, .04)));
    t = min(t, sdBox(p+vec3(.7, .5, -1.55), vec3(.04, .5, .04)));
    t = min(t, sdBox(p+vec3(-.7, .5, 1.55), vec3(.04, .5, .04)));
    t = min(t, sdBox(p+vec3(-.7, .5, -1.55), vec3(.04, .5, .04)));
    return vec2(t, 1.);
}

vec2 map(in vec3 p)
{
    vec2 res = vec2(sdPlane(p), 0.);
    
    {
        vec3 q = p;
    	q.x = abs(q.x);
    	#ifdef USE_MOD_FOR_CHAIRS
        q.z = mod(q.z, .8)-.4;
    	vec2 c = chair(q-vec3(1.1, 1., 0.));
        c.x = max(c.x, abs(p.z)-1.7);
        res = opU(res, c);//chair(q - vec3(1.1, 1., 0.)));
        #else
        q.z += .4;
        res = opU(res, chair(q - vec3(1.1, 1., -.8)));
    	res = opU(res, chair(q - vec3(1.1, 1., 0.)));
    	res = opU(res, chair(q - vec3(1.1, 1., .8)));
    	res = opU(res, chair(q - vec3(1.1, 1., 1.6)));
        #endif
    }
    
    res = opU(res, table(p-vec3(0., 1., 0.)));
    
    return res;
}

vec3 mat_color(float id, in vec3 p)
{
 	if(id == 0.) return vec3(.35, .35, .33);
    else if(id == 1.)
    {
        return vec3(.2, .1, .1)*tbnoise(p*vec3(8., .5, 1.));
    }
    else if(id == 4.) return vec3(.8, .4, 0.);
    else return vec3(0., 0., 1.);
}

float calcAO( in vec3 pos, in vec3 nor )
{
	float totao = 0.0;
    float sca = 1.0;
    for( int aoi=0; aoi<5; aoi++ )
    {
        float hr = 0.01 + 0.05*float(aoi);
        vec3 aopos =  nor * hr + pos;
        float dd = map( aopos ).x;
        totao += -(dd-hr)*sca;
        sca *= 0.75;
    }
    return clamp( 1.0 - 4.0*totao, 0.0, 1.0 );
}

vec3 shade(in vec3 p, in vec3 v, in vec3 n, in vec3 l, in float id)
{
	float shd = 1.;
    if(raymarch(p+l*.01, l, 100.).y >= 0.) shd = 0.;
    vec3 sp = vec3(0.);
    vec3 diffuse = mat_color(id,p)*dot(n,l);
    vec3 ambient = calcAO(p, n)*diffuse*.3;
    return ((diffuse+sp)*shd) + ambient;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{ 
	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0+2.0*q;
	p.x *= iResolution.x/iResolution.y;
    vec2 mo = iMouse.xy/iResolution.xy;
		 
	float time = 15.0 + iGlobalTime;

	// camera	
	vec3 ro = vec3( -0.5+4.2*cos(0.1*time + 6.0*mo.x), 2.0 + 4.0*mo.y, 0.5 + 4.2*sin(0.1*time + 6.0*mo.x) );
	vec3 ta = vec3( 0., 1., 0. );
	
	// camera tx
	vec3 cw = normalize( ta-ro );
	vec3 cp = vec3( 0.0, 1.0, 0.0 );
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
	vec3 rd = normalize( p.x*cu + p.y*cv + 2.5*cw );
    
    vec3 col = vec3(0.);
    
    vec2 hr = raymarch(ro, rd, 1000.);
    
    if(hr.y >= 0.)
    {
        vec3 p = ro + rd*hr.x;
        vec3 n = calc_normal(p);
        vec3 l = (vec3(sin(time*.01), 5., cos(time*.01))-p);
        float ld = length(l);
        l /= ld;
        col = shade(p, rd, n, l, hr.y) * (1./ld*ld);
    }
    
	fragColor = vec4(col, 1.);
}

vec3 calc_normal(in vec3 pos)
{
    	vec3 eps = vec3( 0.001, 0.0, 0.0 );
	vec3 nor = vec3(
	    map(pos+eps.xyy).x - map(pos-eps.xyy).x,
	    map(pos+eps.yxy).x - map(pos-eps.yxy).x,
	    map(pos+eps.yyx).x - map(pos-eps.yyx).x );
	return normalize(nor);
}

vec2 raymarch(in vec3 ro, in vec3 rd, in float maxt)
{
    const float prs = .001;
    float h = 2.*prs;
    vec2 res = vec2(0, -1.);
    for(int i = 0; i < 64; ++i)
    {
        if(res.x>maxt) break;
        res.x += h;
        vec2 r = map(ro+rd*res.x);
        h = r.x;
        res.y = r.y;
    }
    if(res.x>maxt) res.y = -1.0;
    return res;
}