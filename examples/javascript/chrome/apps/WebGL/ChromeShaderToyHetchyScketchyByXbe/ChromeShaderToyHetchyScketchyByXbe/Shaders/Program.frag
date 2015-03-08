//////////////////
// XBE
// Halftone Sketch rendering

///////
// Distance Function and raymarching from IQ
float sdPlane( vec3 p )
{
	return p.y;
}

float sdSphere( vec3 p, float s )
{
    return length(p)-s;
}

float udRoundBox( vec3 p, vec3 b, float r )
{
  return length(max(abs(p)-b,0.0))-r;
}

float length8( vec2 p )
{
	p = p*p; p = p*p; p = p*p;
	return pow( p.x + p.y, 1.0/8.0 );
}

float sdTorus88( vec3 p, vec2 t )
{
  vec2 q = vec2(length8(p.xz)-t.x,p.y);
  return length8(q)-t.y;
}

float sdTorus882( vec3 p, vec2 t )
{
  vec2 q = vec2(length8(p.xy)-t.x,p.z);
  return length8(q)-t.y;
}

float sdTorus883( vec3 p, vec2 t )
{
  vec2 q = vec2(length8(p.yz)-t.x,p.x);
  return length8(q)-t.y;
}

//----------------------------------------------------------------------

float opS( float d1, float d2 )
{
    return max(-d2,d1);
}

vec2 opU( vec2 d1, vec2 d2 )
{
	return (d1.x<d2.x) ? d1 : d2;
}

//----------------------------------------------------------------------

vec2 map( in vec3 pos )
{
    vec2 res = opU( vec2( sdPlane(     pos), 1.0 ),
	                vec2( sdSphere(    pos-vec3( 1.0,0.25, -0.5), 0.5 ), 46.9 ) );
    res = opU( res, vec2( opS(
		             udRoundBox(  pos-vec3(-1.0,0.4, -0.5), vec3(0.30),0.1),
	                 sdSphere(    pos-vec3(-1.0,0.4, -0.5), 0.5)), 13.0 ) );
//	res = opU( res, vec2( sdTorus82(   pos-vec3( 0.0,0.25, 2.0), vec2(0.20,0.05) ),50.0 ) );
	res = opU( res, vec2( sdTorus88(   pos-vec3(0.0,0.5, 1.0), vec2(0.30,0.10) ),43.0 ) );
	res = opU( res, vec2( sdTorus882(   pos-vec3(0.0,0.5, 1.0), vec2(0.30,0.10) ),43.0 ) );
	res = opU( res, vec2( sdTorus883(   pos-vec3(0.0,0.5, 1.0), vec2(0.30,0.10) ),43.0 ) );
//	res = opU( res, vec2( 0.5*sdTorus82( opTwist(pos-vec3(-0.95,0.45, -0.95)),vec2(0.40,0.05)), 46.7 ) );
    return res;
}




vec2 castRay( in vec3 ro, in vec3 rd, in float maxd )
{
	float precis = 0.001;
    float h=precis*2.0;
    float t = 0.0;
    float m = -1.0;
    for( int i=0; i<60; i++ )
    {
        if( abs(h)<precis||t>maxd ) continue;//break;
        t += h;
	    vec2 res = map( ro+rd*t );
        h = res.x;
	    m = res.y;
    }

    if( t>maxd ) m=-1.0;
    return vec2( t, m );
}


float softshadow( in vec3 ro, in vec3 rd, in float mint, in float maxt, in float k )
{
	float res = 1.0;
    float t = mint;
    for( int i=0; i<30; i++ )
    {
		if( t>maxt ) break;

		float h = map( ro + rd*t ).x;
        res = min( res, k*h/t );
        t += 0.02;
    }
    return clamp( res, 0.0, 1.0 );

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

vec3 render( in vec3 ro, in vec3 rd, in vec3 inc, out float dist, out vec3 nor )
{ 
    vec3 col = vec3(0.0);
    vec2 res = castRay(ro,rd,20.0);
    float t = res.x;
	float m = res.y;
    if ( m>-0.5 )
    {
        vec3 pos = ro + t*rd;
        nor = calcNormal( pos );

		//col = vec3(0.6) + 0.4*sin( vec3(0.05,0.08,0.10)*(m-1.0) );
		col = vec3(0.65) + 0.35*sin( 10.*vec3(0.05,0.08,0.10)*(m+1.0) );
		
        float ao = calcAO( pos, nor );

		vec3 lig = normalize( vec3(-0.6, 0.7, -0.5) );
		float amb = clamp( 0.5+0.5*nor.y, 0.0, 1.0 );
        float dif = clamp( dot( nor, lig ), 0.0, 1.0 );
        float bac = clamp( dot( nor, normalize(vec3(-lig.x,0.0,-lig.z))), 0.0, 1.0 )*clamp( 1.0-pos.y,0.0,1.0);

		float sh = 1.0;
		if( dif>0.02 ) { sh = softshadow( pos, lig, 0.02, 10.0, 7.0 ); dif *= sh; }

		vec3 brdf = vec3(0.0);
		brdf += 0.20*amb*vec3(0.10,0.11,0.13)*ao;
        brdf += 0.20*bac*vec3(0.15,0.15,0.15)*ao;
        brdf += 1.20*dif*vec3(1.00,0.90,0.70);

		float pp = clamp( dot( reflect(rd,nor), lig ), 0.0, 1.0 );
		float spe = sh*pow(pp,16.0);
		float fre = ao*pow( clamp(1.0+dot(nor,rd),0.0,1.0), 2.0 );

		col = col*brdf + vec3(1.0)*col*spe + 0.2*fre*(0.5+0.5*col);
		
	}

	dist = t;
	// Edges
	float d = 1.0;
	vec2 rest = castRay(ro,rd+inc,20.0);
	d = rest.x-res.x < 0.09*res.x ? 1. : 0.;
   	rest = castRay(ro,rd-inc,20.0);
	d *= rest.x-res.x < 0.09*res.x ? 1. : 0.;
	col *= d;
	col *= exp( -0.01*t*t );

	return vec3( clamp(col,0.0,1.0) );
}

// procedural noise from IQ
vec2 hash( vec2 p )
{
	p = vec2( dot(p,vec2(127.1,311.7)),
			 dot(p,vec2(269.5,183.3)) );
	return -1.0 + 2.0*fract(sin(p)*43758.5453123);
}

float noise( in vec2 p )
{
	const float K1 = 0.366025404; // (sqrt(3)-1)/2;
	const float K2 = 0.211324865; // (3-sqrt(3))/6;
	
	vec2 i = floor( p + (p.x+p.y)*K1 );
	
	vec2 a = p - i + (i.x+i.y)*K2;
	vec2 o = (a.x>a.y) ? vec2(1.0,0.0) : vec2(0.0,1.0);
	vec2 b = a - o + K2;
	vec2 c = a - 1.0 + 2.0*K2;
	
	vec3 h = max( 0.5-vec3(dot(a,a), dot(b,b), dot(c,c) ), 0.0 );
	
	vec3 n = h*h*h*h*vec3( dot(a,hash(i+0.0)), dot(b,hash(i+o)), dot(c,hash(i+1.0)));
	
	return dot( n, vec3(70.0) );
}

/////////

float aastep(float frequency, float threshold, float value)
{
	float afwidth = frequency / 512.;
	return smoothstep(threshold-afwidth, threshold+afwidth, value);
}

float hetched(vec2 p, vec2 q)
{ 
	return (1.45*abs(p.y) + 0.25*noise(0.333*q));
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0+2.0*q;
	p.x *= iResolution.x/iResolution.y;
	vec3 inc = vec3(1.,0.,0.);
	inc.xy /= iResolution.xy;
	inc.x *= iResolution.x/iResolution.y;
		 
	float Time = 0.25 * (15.0 + iGlobalTime);

	// camera	
	vec3 ro = vec3( 3.0*cos(Time), 1.5, 3.0*sin(Time) );
	vec3 ta = vec3( -0.0, 0.15, 0.0 );
	
	// camera tx
	vec3 cw = normalize( ta-ro );
	vec3 cp = vec3( 0.0, 1.0, 0.0 );
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
	vec3 rd = normalize( p.x*cu + p.y*cv + 2.5*cw );

	float dist;
	vec3 nor = vec3(0.,0.,0.);
    vec3 col = render( ro, rd, inc, dist, nor );
	col = 1.1*sqrt( col );

	/////////////////////////////	
	// Sketching from here
	float gray = dot( col, vec3(0.299, 0.587, 0.114) );
	vec3 orig = col;
	col = vec3(gray, gray, gray);
	//
	float _Angle = 3.14159265/6.0; //PI/6.0;
	mat2 rotmat = mat2(cos(_Angle), -sin(_Angle), sin(_Angle), cos(_Angle));
	vec2 qr = rotmat * p;
//	float frequency = 392.0*(1.0+0.01*nor.x);
	float frequency = iResolution.y*(1.1+0.01*nor.x);
	qr *= frequency;
	float hd = hetched(2.0*fract(qr) - 1.0, qr);
	//
	float radius = 1.0-gray;
	//
	vec3 white = vec3(1.0, 1.0, 1.0);
	vec3 black = vec3(0.0, 0.0, 0.0);
	col *= mix( black, white, aastep(frequency, radius, hd) );
	
	col += 0.35*vec3(1.-exp(-128.*dist));
	float noi = noise(512.0*p);
	col += vec3(0.05+0.25*noi) ;
	col *= mix(vec3( 0.9592, 0.8088, 0.6284 ), orig, 0.25);

    fragColor=vec4( clamp(col,0.,1.), 1.0 );
}