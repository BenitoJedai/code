float hash( float n ) { return fract(sin(n)*43758.5453123); }
vec2 hash2( float n )
{
    return fract(sin(vec2(n,n+1.0))*vec2(43758.5453123,22578.1459123));
}

vec3 hash3( float n )
{
    return fract(sin(vec3(n,n+1.0,n+2.0))*vec3(43758.5453123,22578.1459123,19642.3490423));
}
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
vec2 opU( vec2 d1, vec2 d2 )
{
	return (d1.x<d2.x) ? d1 : d2;
}
float opS( float d1, float d2 )
{
    return max(-d2,d1);
}
vec3 opRep( vec3 p, vec3 c )
{
    return mod(p,c)-0.5*c;
}

float sdCylinder( vec3 p, vec2 h )
{
  vec2 d = abs(vec2(length(p.xz),p.y)) - h;
  return min(max(d.x,d.y),0.0) + length(max(d,0.0));
}

vec2 mushroom(in vec3 p)
{
    vec2 cap = vec2(opS(sdSphere((p-vec3(0.,.2,0.))*vec3(1.20, 1.3, 1.20), .75),sdBox(p+vec3(0.,1.,0.), vec3(1.)))   , 1.);
    vec2 stock = vec2(sdCylinder(p, vec2(.2+(1./exp(p.y*2.3))*.03, .65)), 2.);
    return opU(cap,stock);
}

vec2 map(in vec3 p)
{
    vec3 q = p;
    return opU(vec2(sdBox(p, vec3(2., .01, 2.)), 0.), mushroom(q-vec3(0.,.65,0.)));
}

vec2 castRay( in vec3 ro, in vec3 rd, in float maxd )
{
	float precis = 0.0001;
    float h=precis*2.0;
    float t = 0.0;
    float m = -1.0;
    for( int i=0; i<130; i++ )
    {
        if( abs(h)<precis||t>maxd ) break;
        t += h;
        vec3 p = ro+rd*t;
	    vec2 res = map( p );
        h = res.x;
	    m = res.y;
    }

    if( t>maxd ) m=-1.0;
    return vec2( t, m );
}

vec3 calcNormal( in vec3 pos )
{
	vec3 eps = vec3( 0.001, 0.0, 0.0 );
	vec3 nor = vec3(
	    map(pos+eps.xyy).x - map(pos-eps.xyy).x,
	    map(pos+eps.yxy).x - map(pos-eps.yxy).x,
	    map(pos+eps.yyx).x - map(pos-eps.yyx).x );
	return normalize(nor);
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

vec3 surfcol(in float id, in vec3 p)
{
    if(id == 0.) return (vec3(.3, .2, .05)+vec3(noise(p*20.)*.06))*.8;
    if(id == 1.) 
    {
        vec3 q = opRep(p+vec3(noise(p)*.2), vec3(.4));
        float r = sdSphere(q, .01);
        return r < .1 ? vec3(.935, .92, .93)*.8 : vec3(.9, .1, .05)*.8;
    }
    if(id == 2.) return vec3(.8, .8, .5)*.8;
    return vec3(1.);
}

vec3 cosdir( const vec3 n ) {
	vec3  uu = normalize( cross( n, vec3(0.0,1.0,1.0) ) );
	vec3  vv = normalize( cross( uu, n ) );
	vec2 rv2 = hash2(n.x);
	float ra = sqrt(rv2.y);
	float rx = ra*cos(6.2831*rv2.x); 
	float ry = ra*sin(6.2831*rv2.x);
	float rz = sqrt( 1.0-rv2.y );
	vec3  rr = vec3( rx*uu + ry*vv + rz*n );

    return normalize( rr );
}

vec3 background(vec3 rd) { return vec3(0); }
float shadow(in vec3 p, in vec3 rd) { return castRay(p+rd*.001, rd, 1000.).y >= 0. ? 0. : 1.; }
vec3 lighting(in vec3 p, in vec3 n)
{
    vec3 c = vec3(0.);
    {
        vec3 pt = cosdir(n) * 1000.;
        vec3 lr = normalize(pt - p);
        c += shadow(p, lr);
    }
    return c;
}

vec3 brdf_ray(in vec3 p, in vec3 n, in vec3 rd, in float id)
{
    return cosdir(n);
}
#define HAX
vec3 render(vec3 ro, vec3 rd)
{
    #ifdef HAX
    vec2 r = castRay(ro, rd, 1000.);
    if(r.y >= 0.)
    {
        vec3 l = normalize(vec3(1., .5, 0.));
        vec3 p = ro+rd*r.x;
        vec3 n = calcNormal(p);
        vec3 sc = surfcol(r.y, p);
        float sh = 1.;
        if(castRay(p+l*.01, l, 100.).y >= 0.) sh = 0.;
        vec3 c = (sc*dot(n,l)*sh)+(sc*.2)*calcAO(p, n);
        return c;
    }
    else {return vec3(0);}
    #else
    vec3 fcol = vec3(1.);
    vec3 tcol = vec3(0.);
    for(int i = 0; i < 10; ++i)
    {
        vec2 r = castRay(ro, rd, 1000.);
        if(r.y < 0.)
        {
            if(i == 0) fcol = background(rd);
            else break;
        }
        vec3 pos = ro + rd*r.x;
        vec3 nor = calcNormal(pos);
        vec3 scol = surfcol(r.y, pos);
        vec3 dcol = lighting(pos, nor);
        ro = pos;
        rd = brdf_ray(pos, nor, rd, r.y);
        
        fcol *= scol;
        tcol += fcol*dcol;
    }
    return tcol;
    #endif
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0+2.0*q;
	p.x *= iResolution.x/iResolution.y;
    vec2 mo = iMouse.xy/iResolution.xy;
		 
	float time = 15.0 + iGlobalTime;

	// camera	
	vec3 ro = vec3( -0.5+3.2*cos(0.1*time + 6.0*mo.x), 2.0 + 10.0*mo.y, 0.5 + 3.2*sin(0.1*time + 6.0*mo.x) );
	vec3 ta = vec3( 0. );
	
	// camera tx
	vec3 cw = normalize( ta-ro );
	vec3 cp = vec3( 0.0, 1.0, 0.0 );
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
    
    vec3 col = vec3(0.);
    for(int i = 0; i < 10; ++i)
    {
        vec2 sp = p + hash2(float(i))*.005;
		vec3 rd = normalize( sp.x*cu + sp.y*cv + 2.5*cw );
    	col += render( ro, rd );
    }
    col /= 10.;
    col = sqrt(col);
	fragColor = vec4(col,1.0);
}